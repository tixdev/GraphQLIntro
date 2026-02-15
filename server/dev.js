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
        server: { port: 3000, host: true },
    });
    await vite.listen();
    vite.printUrls();

    const localIP = getLocalIP();
    const remoteUrl = `http://${localIP}:3000/remote.html`;

    // State
    let currentSlide = 0;
    let currentStep = 0;
    let totalSlides = 11;
    let slideSteps = {}; // { slideIndex: totalSteps }

    // Poll state
    const pollVotes = { a: 0, b: 0, c: 0 };
    const votedClients = new Set();

    const audiences = new Set();
    const presenters = new Set();

    // WebSocket server on a different port
    const wss = new WebSocketServer({ port: 3001 });

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

    function broadcastAudienceCount() {
        broadcastAll({ type: 'audience-count', count: audiences.size });
    }

    wss.on('connection', (ws, req) => {
        const url = new URL(req.url, `http://${req.headers.host}`);
        const role = url.searchParams.get('role');

        if (role === 'presenter') {
            presenters.add(ws);
            ws.send(JSON.stringify({
                type: 'init',
                slide: currentSlide,
                step: currentStep,
                totalSlides,
                slideSteps,
                audienceCount: audiences.size,
            }));
        } else {
            audiences.add(ws);
            ws.send(JSON.stringify({
                type: 'init',
                slide: currentSlide,
                step: currentStep,
                audienceCount: audiences.size,
            }));
            broadcastAudienceCount();
        }

        ws.on('message', (raw) => {
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

                if (data.type === 'vote' && !votedClients.has(ws)) {
                    const option = data.option;
                    if (pollVotes[option] !== undefined) {
                        pollVotes[option]++;
                        votedClients.add(ws);
                        broadcastAll({ type: 'poll-results', votes: { ...pollVotes } });
                    }
                }

                if (data.type === 'poll-reset') {
                    pollVotes.a = 0;
                    pollVotes.b = 0;
                    pollVotes.c = 0;
                    votedClients.clear();
                    broadcastAll({ type: 'poll-results', votes: { ...pollVotes } });
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
    console.log(`  📡 WebSocket server running on ws://${localIP}:3001\n`);
}

start();
