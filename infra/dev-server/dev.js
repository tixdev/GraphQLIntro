import { createServer } from 'vite';
import { WebSocketServer } from 'ws';
import { networkInterfaces } from 'os';

function getLocalIP() {
    const nets = networkInterfaces();
    for (const name of Object.keys(nets)) {
        for (const net of nets[name]) {
            if (net.family === 'IPv4' && !net.internal) {
                return net.address;
            }
        }
    }
    return 'localhost';
}

async function start() {
    const vite = await createServer({
        root: 'apps/presentation',
        // server config is now in vite.config.js
    });
    await vite.listen();
    vite.printUrls();

    const localIP = getLocalIP();
    const publicUrl = process.env.PUBLIC_URL || `http://${localIP}:3000`;
    const remoteUrl = `${publicUrl}/remote.html`;

    // State
    let currentSlide = 0;
    let currentStep = 0;
    let totalSlides = 11;
    let slideSteps = {}; // { slideIndex: totalSteps }

    // Poll state removed

    const audiences = new Set();
    const presenters = new Set();
    const clientQueries = {}; // { clientId: count }

    // WebSocket server integrated with Vite server
    const wss = new WebSocketServer({ noServer: true });

    vite.httpServer.on('upgrade', (request, socket, head) => {
        if (request.url.startsWith('/ws')) {
            wss.handleUpgrade(request, socket, head, (ws) => {
                wss.emit('connection', ws, request);
            });
        }
    });

    function broadcast(data, targets) {
        const msg = JSON.stringify(data);
        for (const client of targets) {
            if (client.readyState === 1) {
                client.send(msg);
            }
        }
    }

    function broadcastAll(data) {
        broadcast(data, audiences);
        broadcast(data, presenters);
    }

    function getUniqueAudienceCount() {
        const uniqueClients = new Set();
        for (const ws of audiences) {
            if (ws.clientId) uniqueClients.add(ws.clientId);
        }
        return uniqueClients.size;
    }

    function broadcastAudienceCount() {
        broadcastAll({ type: 'audience-count', count: getUniqueAudienceCount() });
    }

    wss.on('connection', (ws, req) => {
        const url = new URL(req.url, `http://${req.headers.host}`);
        const role = url.searchParams.get('role');
        const clientId = url.searchParams.get('clientId');

        if (role === 'presenter') {
            presenters.add(ws);
            ws.send(JSON.stringify({
                type: 'init',
                slide: currentSlide,
                step: currentStep,
                totalSlides,
                slideSteps,
                audienceCount: getUniqueAudienceCount(),
                clientQueries
            }));
        } else {
            ws.clientId = clientId || Math.random().toString(36).substring(2); // fallback for untracked
            audiences.add(ws);
            ws.send(JSON.stringify({
                type: 'init',
                slide: currentSlide,
                step: currentStep,
                audienceCount: getUniqueAudienceCount(),
            }));
            broadcastAudienceCount();
        }

        ws.on('message', async (raw) => {
            try {
                const data = JSON.parse(raw);

                if (data.type === 'navigate') {
                    currentSlide = data.slide;
                    currentStep = data.step ?? 0;
                    broadcastAll({ type: 'navigate', slide: currentSlide, step: currentStep });
                }

                if (data.type === 'register-steps') {
                    slideSteps = data.slideSteps;
                    totalSlides = data.totalSlides;
                }

                if (data.type === 'graphql-exec') {
                    const { query, variables } = data;

                    try {
                        const response = await fetch('http://localhost:5095/graphql', {
                            method: 'POST',
                            headers: { 'Content-Type': 'application/json' },
                            body: JSON.stringify({ query, variables })
                        });

                        const result = await response.json();

                        // Broadcast query and result to everyone (audience sees what presenter does)
                        broadcastAll({
                            type: 'demo-result',
                            query,
                            variables,
                            result
                        });
                    } catch (err) {
                        broadcastAll({
                            type: 'demo-result',
                            query,
                            variables,
                            result: { errors: [{ message: 'Failed to connect to GraphQL server: ' + err.message }] }
                        });
                    }
                }

                if (data.type === 'audience-query-exec') {
                    const cid = data.clientId;
                    if (cid) {
                        clientQueries[cid] = (clientQueries[cid] || 0) + 1;
                        broadcast({ type: 'query-stats', stats: clientQueries }, presenters);
                    }
                }
            } catch (e) {
                // ignore
            }
        });

        ws.on('close', () => {
            const wasAudience = audiences.delete(ws);
            presenters.delete(ws);
            if (wasAudience) broadcastAudienceCount();
        });
    });

    console.log(`\n  🎮 Remote control: ${remoteUrl}`);
    console.log(`  📡 WebSocket server running on ws://${localIP}:3000/ws\n`);
}

start();
