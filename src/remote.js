// ============================================
// Presenter Remote — WebSocket Controller
// ============================================

const slideNum = document.getElementById('slide-num');
const slideTotal = document.getElementById('slide-total');
const stepNum = document.getElementById('step-num');
const stepTotal = document.getElementById('step-total');
const btnPrev = document.getElementById('btn-prev');
const btnNext = document.getElementById('btn-next');
const connectionEl = document.getElementById('remote-connection');
const slideItems = document.querySelectorAll('.slide-item');
const audienceCountEl = document.getElementById('audience-count');

// Slide steps configuration (must match index.html data-steps)
const SLIDE_STEPS = [2, 3, 3, 3, 3, 2, 6, 3, 3, 1, 1]; // steps per slide
const TOTAL_SLIDES = SLIDE_STEPS.length;

let currentSlide = 0;
let currentStep = 0;
let ws = null;

slideTotal.textContent = TOTAL_SLIDES;

// ============================================
// WebSocket
// ============================================

function connect() {
    const protocol = location.protocol === 'https:' ? 'wss' : 'ws';
    const host = location.hostname;
    ws = new WebSocket(`${protocol}://${host}:3001/?role=presenter`);

    ws.onopen = () => {
        connectionEl.className = 'connected';
        connectionEl.querySelector('.rc-label').textContent = 'Connected';

        // Register slide steps with server
        ws.send(JSON.stringify({
            type: 'register-steps',
            slideSteps: SLIDE_STEPS,
            totalSlides: TOTAL_SLIDES,
        }));
    };

    ws.onclose = () => {
        connectionEl.className = 'disconnected';
        connectionEl.querySelector('.rc-label').textContent = 'Disconnected';
        setTimeout(connect, 2000);
    };

    ws.onerror = () => ws.close();

    ws.onmessage = (event) => {
        const data = JSON.parse(event.data);

        if (data.type === 'init') {
            currentSlide = data.slide;
            currentStep = data.step;
            if (data.audienceCount !== undefined) {
                audienceCountEl.textContent = data.audienceCount;
            }
            updateUI();
        }

        if (data.type === 'audience-count') {
            audienceCountEl.textContent = data.count;
        }

        if (data.type === 'navigate') {
            currentSlide = data.slide;
            currentStep = data.step;
            updateUI();
        }
    };
}

// ============================================
// Navigation Logic
// ============================================

function sendNavigate() {
    if (ws && ws.readyState === 1) {
        ws.send(JSON.stringify({
            type: 'navigate',
            slide: currentSlide,
            step: currentStep,
        }));
    }
}

function next() {
    const maxSteps = SLIDE_STEPS[currentSlide] || 0;

    if (currentStep < maxSteps) {
        // Next step within current slide
        currentStep++;
    } else if (currentSlide < TOTAL_SLIDES - 1) {
        // Next slide
        currentSlide++;
        currentStep = 0;
    }

    sendNavigate();
    updateUI();
}

function prev() {
    if (currentStep > 0) {
        // Previous step
        currentStep--;
    } else if (currentSlide > 0) {
        // Previous slide — go to last step
        currentSlide--;
        currentStep = SLIDE_STEPS[currentSlide] || 0;
    }

    sendNavigate();
    updateUI();
}

function goToSlide(index) {
    currentSlide = index;
    currentStep = 0;
    sendNavigate();
    updateUI();
}

// ============================================
// UI Update
// ============================================

function updateUI() {
    slideNum.textContent = currentSlide + 1;
    stepNum.textContent = currentStep;
    stepTotal.textContent = SLIDE_STEPS[currentSlide] || 0;

    // Button states
    btnPrev.disabled = currentSlide === 0 && currentStep === 0;
    btnNext.disabled = currentSlide === TOTAL_SLIDES - 1 && currentStep >= (SLIDE_STEPS[currentSlide] || 0);

    // Active slide in list
    slideItems.forEach((item) => {
        const idx = parseInt(item.getAttribute('data-slide'), 10);
        item.classList.toggle('active', idx === currentSlide);
    });
}

// ============================================
// Event Listeners
// ============================================

btnNext.addEventListener('click', next);
btnPrev.addEventListener('click', prev);

slideItems.forEach((item) => {
    item.addEventListener('click', () => {
        const idx = parseInt(item.getAttribute('data-slide'), 10);
        goToSlide(idx);
    });
});

// Swipe gesture support
let touchStartX = 0;
let touchStartY = 0;

document.addEventListener('touchstart', (e) => {
    // Don't capture touches on buttons/slide list
    if (e.target.closest('#nav-controls') || e.target.closest('#slide-list')) return;
    touchStartX = e.touches[0].clientX;
    touchStartY = e.touches[0].clientY;
});

document.addEventListener('touchend', (e) => {
    if (e.target.closest('#nav-controls') || e.target.closest('#slide-list')) return;
    const dx = e.changedTouches[0].clientX - touchStartX;
    const dy = e.changedTouches[0].clientY - touchStartY;

    if (Math.abs(dx) > 50 && Math.abs(dx) > Math.abs(dy)) {
        if (dx < 0) next();
        else prev();
    }
});

// Keyboard (for desktop testing)
document.addEventListener('keydown', (e) => {
    if (e.key === 'ArrowRight' || e.key === ' ') { e.preventDefault(); next(); }
    if (e.key === 'ArrowLeft') { e.preventDefault(); prev(); }
});

// Poll reset
const btnResetPoll = document.getElementById('btn-reset-poll');
if (btnResetPoll) {
    btnResetPoll.addEventListener('click', () => {
        if (ws && ws.readyState === 1) {
            ws.send(JSON.stringify({ type: 'poll-reset' }));
        }
    });
}

// ============================================
// Init
// ============================================

updateUI();
connect();
