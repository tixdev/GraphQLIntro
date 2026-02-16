// ============================================
// Audience View — WebSocket Client + Animation Engine
// ============================================

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
    const host = location.hostname;
    ws = new WebSocket(`${protocol}://${host}:3001/?role=audience`);

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

    if (qEl) qEl.textContent = query;
    if (rEl) rEl.textContent = JSON.stringify(result, null, 2);
    if (sEl) sEl.textContent = 'Received update from presenter.';
}

// Try It Slide Logic
const tryRunBtn = document.getElementById('try-run-btn');
const tryInput = document.getElementById('try-input');
const tryResult = document.getElementById('try-result');
const tryExamples = document.getElementById('try-examples');

if (tryRunBtn) {
    tryRunBtn.addEventListener('click', async (e) => {
        e.stopPropagation(); // prevent navigation
        if (!tryInput || !tryResult) return;

        const query = tryInput.value;
        tryResult.textContent = 'Loading...';

        try {
            const response = await fetch('http://localhost:5095/graphql', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ query })
            });
            const result = await response.json();
            tryResult.textContent = JSON.stringify(result, null, 2);
        } catch (err) {
            tryResult.textContent = JSON.stringify({ error: err.message }, null, 2);
        }
    });
}

if (tryExamples) {
    tryExamples.addEventListener('change', (e) => {
        e.stopPropagation();
        if (tryInput && e.target.value) {
            tryInput.value = e.target.value;
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
}


// ============================================
// Block ALL user interaction
// ============================================

document.addEventListener('keydown', (e) => {
    // Allow typing in textarea
    if (e.target.tagName === 'TEXTAREA') return;
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
