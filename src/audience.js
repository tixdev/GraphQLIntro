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

        if (data.type === 'poll-results') {
            updatePollBars(data.votes);
        }
    };
}

// ============================================
// Navigation (no user interaction)
// ============================================

function navigateToSlide(slideIndex, step = 0) {
    if (slideIndex < 0 || slideIndex >= slides.length) return;

    const prevSlide = currentSlide;
    currentSlide = slideIndex;
    currentStep = step;

    // Update slide counter
    currentSlideNum.textContent = slideIndex + 1;

    // Transition slides
    slides.forEach((slide, i) => {
        slide.classList.remove('active', 'exit-up');
        if (i === slideIndex) {
            slide.classList.add('active');
        } else if (i < slideIndex) {
            slide.classList.add('exit-up');
        }
    });

    // Apply steps
    applySteps(slideIndex, step);

    // Trigger slide-specific animations
    triggerSlideAnimations(slideIndex, step);
}

// ============================================
// Step System
// ============================================

function applySteps(slideIndex, step) {
    const slide = slides[slideIndex];
    const steppedElements = slide.querySelectorAll('[data-step]');

    steppedElements.forEach((el) => {
        const elStep = parseInt(el.getAttribute('data-step'), 10);
        if (elStep === 0 || elStep <= step) {
            el.classList.add('step-visible');
            // Also add 'visible' class for panels, bars, etc.
            el.classList.add('visible');
        } else {
            el.classList.remove('step-visible', 'visible');
        }
    });
}

// ============================================
// Slide-Specific Animations
// ============================================

function triggerSlideAnimations(slideIndex, step) {
    const slide = slides[slideIndex];

    // Over-fetching: animated bars
    if (slide.classList.contains('slide-overfetch') && step >= 3) {
        animateBars(slide);
    }

    // Under-fetching: waterfall bars
    if (slide.classList.contains('slide-underfetch')) {
        if (step >= 1) animateWaterfall(slide, '.rest-panel');
        if (step >= 2) animateWaterfall(slide, '.graphql-panel');
    }

    // Endpoints: cascade items
    if (slide.classList.contains('slide-endpoints') && step >= 1) {
        animateEndpoints(slide);
    }
}

// ============================================
// Poll System
// ============================================

let hasVoted = false;

function updatePollBars(votes) {
    const total = votes.a + votes.b + votes.c;
    ['a', 'b', 'c'].forEach(key => {
        const pct = total > 0 ? (votes[key] / total * 100) : 0;
        const bar = document.getElementById(`poll-bar-${key}`);
        const count = document.getElementById(`poll-count-${key}`);
        if (bar) bar.style.width = `${pct}%`;
        if (count) count.textContent = votes[key];
    });
}

// Handle poll option clicks
document.querySelectorAll('.poll-option').forEach(btn => {
    btn.addEventListener('click', (e) => {
        e.stopPropagation();
        if (hasVoted || !ws || ws.readyState !== 1) return;
        hasVoted = true;
        const option = btn.getAttribute('data-option');
        ws.send(JSON.stringify({ type: 'vote', option }));
        // Highlight selected
        document.querySelectorAll('.poll-option').forEach(b => b.classList.remove('poll-voted'));
        btn.classList.add('poll-voted');
    }, true);
});

// Animated comparison bars (over-fetching)
function animateBars(slide) {
    const fills = slide.querySelectorAll('.bar-fill');
    fills.forEach((fill) => {
        const targetWidth = fill.getAttribute('data-width');
        setTimeout(() => {
            fill.style.setProperty('--target-width', `${targetWidth}%`);
            fill.classList.add('animate');
        }, 200);
    });
}

// Waterfall animation (under-fetching)
function animateWaterfall(slide, panelSelector) {
    const panel = slide.querySelector(panelSelector);
    if (!panel) return;
    const bars = panel.querySelectorAll('.wf-bar');
    bars.forEach((bar, i) => {
        const row = bar.closest('.waterfall-row');
        const delay = row ? parseInt(row.getAttribute('data-delay') || i, 10) : i;
        setTimeout(() => {
            bar.style.width = bar.getAttribute('data-width');
        }, delay * 200 + 300);
    });
}

// Endpoint cascade animation
function animateEndpoints(slide) {
    const items = slide.querySelectorAll('.endpoint-item');
    items.forEach((item, i) => {
        setTimeout(() => {
            item.classList.add('visible');
        }, i * 80);
    });
}

// ============================================
// Block ALL user interaction
// ============================================

document.addEventListener('keydown', (e) => e.preventDefault(), true);
document.addEventListener('wheel', (e) => e.preventDefault(), { passive: false });
document.addEventListener('touchmove', (e) => e.preventDefault(), { passive: false });
document.addEventListener('contextmenu', (e) => e.preventDefault());

// Allow touch/click ONLY on poll buttons
document.addEventListener('touchstart', (e) => {
    if (!e.target.closest('.poll-option')) {
        e.preventDefault();
    }
}, { passive: false });

document.addEventListener('mousedown', (e) => {
    if (!e.target.closest('.poll-option')) {
        e.preventDefault();
    }
}, true);

// ============================================
// Init
// ============================================

// Show first slide immediately
slides[0].classList.add('active');
connect();
