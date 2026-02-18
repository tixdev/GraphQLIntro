// ============================================
// Audience View — WebSocket Client + Animation Engine
// ============================================

import { parse, print } from 'graphql';
import { CodeJar } from 'codejar';
import Prism from 'prismjs';
import 'prismjs/components/prism-graphql';


const slides = document.querySelectorAll('.slide');
const currentSlideNum = document.getElementById('current-slide-num');
const totalSlideNum = document.getElementById('total-slide-num');
const connectionStatus = document.getElementById('connection-status');
const audienceCountEl = document.getElementById('audience-count');

let currentSlide = 0;
let currentStep = 0;
let ws = null;

// Total slides
totalSlideNum.textContent = slides.length;

// ============================================
// WebSocket Connection
// ============================================

function connect() {
    const protocol = location.protocol === 'https:' ? 'wss' : 'ws';
    const host = location.host; // includes port
    ws = new WebSocket(`${protocol}://${host}/ws?role=audience`);

    ws.onopen = () => {
        connectionStatus.className = 'connected';
    };

    ws.onclose = () => {
        connectionStatus.className = 'disconnected';
        setTimeout(connect, 2000);
    };

    ws.onerror = () => {
        ws.close();
    };

    ws.onmessage = (event) => {
        const data = JSON.parse(event.data);

        if (data.type === 'init') {
            if (data.audienceCount !== undefined) audienceCountEl.textContent = data.audienceCount;
            navigateToSlide(data.slide, data.step);
        }

        if (data.type === 'audience-count') {
            audienceCountEl.textContent = data.count;
        }

        if (data.type === 'navigate') {
            navigateToSlide(data.slide, data.step);
        }

        if (data.type === 'demo-result') {
            updateLiveDemo(data.query, data.result);
        }
    };
}

// ============================================
// Demo & Try It Logic
// ============================================

function updateLiveDemo(query, result) {
    const qEl = document.getElementById('live-query');
    const rEl = document.getElementById('live-result');
    const sEl = document.getElementById('live-status');

    if (qEl) qEl.innerHTML = highlightGraphQL(query);
    if (rEl) rEl.innerHTML = highlightJSON(JSON.stringify(result, null, 2));
    if (sEl) sEl.textContent = 'Received update from presenter.';
}

// Try It Slide Logic
const tryRunBtn = document.getElementById('try-run-btn');
const tryInput = document.getElementById('try-input');
const tryResult = document.getElementById('try-result');
const tryExamples = document.getElementById('try-examples');

let jar;

if (tryInput) {
    const highlight = (editor) => {
        const code = editor.textContent;
        // Simple manual highlighting for now, or use Prism if available
        // But better to use Prism's highlightElement if we want full Prism power
        //CodeJar expects us to update the innerHTML
        editor.innerHTML = Prism.highlight(code, Prism.languages.graphql, 'graphql');
    };

    jar = CodeJar(tryInput, highlight, { tab: '  ' });

    // Initial highlight & format
    try {
        const initialCode = tryInput.textContent;
        // Only format if it looks like a query
        if (initialCode.trim().length > 0) {
            // Attempt to pretty print using GraphQL parser
            try {
                const ast = parse(initialCode);
                const pretty = print(ast);
                jar.updateCode(pretty);
            } catch (parseErr) {
                // Fallback: simple dedent
                const lines = initialCode.split('\n');
                // Find min indentation (ignoring empty lines)
                let minIndent = Infinity;
                lines.forEach(line => {
                    if (line.trim().length > 0) {
                        const indent = line.search(/\S/);
                        if (indent !== -1 && indent < minIndent) minIndent = indent;
                    }
                });

                if (minIndent !== Infinity && minIndent > 0) {
                    const dedented = lines.map(l => l.length >= minIndent ? l.substring(minIndent) : l).join('\n').trim();
                    jar.updateCode(dedented);
                } else {
                    jar.updateCode(initialCode.trim());
                }
            }
        }
    } catch (e) {
        console.warn('Initial CodeJar setup failed:', e);
        jar.updateCode(tryInput.textContent.trim());
    }
}

if (tryRunBtn) {
    tryRunBtn.addEventListener('click', async (e) => {
        e.stopPropagation(); // prevent navigation
        if (!tryInput || !tryResult) return;

        const query = jar ? jar.toString() : tryInput.textContent;
        tryResult.innerHTML = '<span class="cmt">// Loading...</span>';

        try {
            const response = await fetch('/graphql', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ query })
            });
            const result = await response.json();
            tryResult.innerHTML = highlightJSON(JSON.stringify(result, null, 2));
        } catch (err) {
            tryResult.innerHTML = highlightJSON(JSON.stringify({ error: err.message }, null, 2));
        }
    });
}

// ============================================
// Simple Syntax Highlighters
// ============================================

function highlightGraphQL(code) {
    if (!code) return '';

    try {
        // 1. Parse and print to format
        const ast = parse(code);
        const pretty = print(ast);

        // 2. Syntax highlighting (reuse existing logic on formatted code)
        let html = pretty
            .replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;');

        // Comments
        html = html.replace(/(#.*)$/gm, '<span class="cmt">$1</span>');

        // Strings
        html = html.replace(/(".*?")/g, '<span class="str">$1</span>');

        // Keywords
        html = html.replace(/\b(query|mutation|subscription|fragment|type|input|interface|union|scalar|directive|enum)\b/g, '<span class="kw">$1</span>');

        // Field arguments
        html = html.replace(/(\w+)(?=:)/g, '<span class="arg">$1</span>');

        // Numbers
        html = html.replace(/\b(\d+)\b/g, '<span class="num">$1</span>');

        // Functions/Fields
        html = html.replace(/(\w+)(?=\()/g, '<span class="fn">$1</span>');

        return html;
    } catch (e) {
        console.warn('GraphQL formatting failed:', e);
        return code; // Fallback to unformatted code if parsing fails (e.g. partial query)
    }
}

function highlightJSON(json) {
    if (!json) return '';

    // HTML-escape first
    let html = json
        .replace(/&/g, '&amp;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;');

    // Strings (keys and values)
    html = html.replace(/(".*?")/g, (match) => {
        if (match.startsWith('"') && match.endsWith(':')) {
            return `<span class="arg">${match}</span>`; // Key
        }
        return `<span class="str">${match}</span>`; // Value
    });

    // Numbers
    html = html.replace(/\b(\d+)\b/g, '<span class="num">$1</span>');

    // Booleans/Null
    html = html.replace(/\b(true|false|null)\b/g, '<span class="kw">$1</span>');

    return html;
}

if (tryExamples) {
    tryExamples.addEventListener('change', (e) => {
        e.stopPropagation();
        const newVal = e.target.value;
        if (tryInput && newVal) {
            let formattedVal = newVal;
            try {
                if (newVal.trim().length > 0) {
                    const ast = parse(newVal);
                    formattedVal = print(ast);
                }
            } catch (err) {
                console.warn('Failed to format example:', err);
                // Fallback to original value if parsing fails
            }

            if (jar) {
                jar.updateCode(formattedVal);
            } else {
                tryInput.textContent = formattedVal;
            }
        }
    });
}



function navigateToSlide(slideIndex, stepIndex) {
    if (slideIndex < 0 || slideIndex >= slides.length) return;

    currentSlide = slideIndex;
    currentStep = stepIndex;

    // Update active slide
    slides.forEach((slide, index) => {
        slide.classList.toggle('active', index === slideIndex);
    });

    // Update slide counter
    currentSlideNum.textContent = slideIndex + 1;

    // Handle steps
    const activeSlide = slides[slideIndex];
    if (activeSlide) {
        const steppedElements = activeSlide.querySelectorAll('[data-step]');
        steppedElements.forEach(el => {
            const elStep = parseInt(el.getAttribute('data-step'), 10);
            if (activeSlide.classList.contains('active')) {
                if (stepIndex >= elStep) {
                    el.classList.add('visible');
                } else {
                    el.classList.remove('visible');
                }
            }
        });
    }

    if (activeSlide && (activeSlide.classList.contains('slide-try-it') || activeSlide.classList.contains('slide-live-demo'))) {
        document.body.classList.add('cursor-visible');
    } else {
        document.body.classList.remove('cursor-visible');
    }
}


// ============================================
// Block ALL user interaction
// ============================================

document.addEventListener('keydown', (e) => {
    // Allow typing in textarea or editor
    if (e.target.tagName === 'TEXTAREA' || e.target.closest('.editor')) return;
    e.preventDefault();
}, true);

document.addEventListener('wheel', (e) => {
    // Allow scrolling in result areas
    if (e.target.closest('.try-result-area') || e.target.closest('.try-input-area') || e.target.closest('.demo-pane')) return;
    e.preventDefault();
}, { passive: false });

document.addEventListener('touchmove', (e) => {
    if (e.target.closest('.try-result-area') || e.target.closest('.try-input-area') || e.target.closest('.demo-pane')) return;
    e.preventDefault();
}, { passive: false });

document.addEventListener('contextmenu', (e) => e.preventDefault());

// Allow touch/click ONLY on interactive elements in Try It slide
function isInteractive(target) {
    return target.closest('.try-input-area') || target.closest('.try-result-area') || target.closest('.demo-pane');
}

document.addEventListener('touchstart', (e) => {
    if (!isInteractive(e.target)) {
        e.preventDefault();
    }
}, { passive: false });

document.addEventListener('mousedown', (e) => {
    if (!isInteractive(e.target)) {
        e.preventDefault();
    }
}, true);

// ============================================
// Init
// ============================================

// Show first slide immediately
slides[0].classList.add('active');
connect();
