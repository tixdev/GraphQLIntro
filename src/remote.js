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
        currentStep--;
    } else if (currentSlide > 0) {
        currentSlide--;
        const maxSteps = SLIDE_STEPS[currentSlide] || 0;
        currentStep = maxSteps;
    }

    sendNavigate();
    updateUI();
}

function goToSlide(index) {
    if (index >= 0 && index < TOTAL_SLIDES) {
        currentSlide = index;
        currentStep = 0;
        sendNavigate();
        updateUI();
    }
}

function updateUI() {
    slideNum.textContent = currentSlide + 1;
    stepNum.textContent = currentStep;

    // Update total steps for current slide
    const maxSteps = SLIDE_STEPS[currentSlide] || 0;
    stepTotal.textContent = maxSteps;

    // Update buttons
    if (btnPrev) btnPrev.disabled = currentSlide === 0 && currentStep === 0;

    if (btnNext) btnNext.disabled = currentSlide === TOTAL_SLIDES - 1 && currentStep >= maxSteps;

    // Update active state in list
    slideItems.forEach((item, index) => {
        item.classList.toggle('active', index === currentSlide);
        if (index === currentSlide) {
            item.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
        }
    });
}

// Event Listeners
if (btnPrev) btnPrev.addEventListener('click', prev);
if (btnNext) btnNext.addEventListener('click', next);

slideItems.forEach((item, index) => {
    item.addEventListener('click', () => goToSlide(index));
});

// Keyboard controls
document.addEventListener('keydown', (e) => {
    if (e.target.tagName === 'TEXTAREA') return;

    if (e.key === 'ArrowRight' || e.key === ' ' || e.key === 'Enter') {
        e.preventDefault();
        next();
    } else if (e.key === 'ArrowLeft') {
        e.preventDefault();
        prev();
    }
});


// ============================================
// Live Demo Logic
// ============================================

const btnRunQuery = document.getElementById('btn-run-query');
const queryInput = document.getElementById('remote-query-input');
const examplesSelect = document.getElementById('remote-examples');

if (btnRunQuery) {
    btnRunQuery.addEventListener('click', () => {
        if (!ws || ws.readyState !== 1) return;
        const query = queryInput.value;
        ws.send(JSON.stringify({
            type: 'graphql-exec',
            query,
            variables: {}
        }));
    });
}

if (examplesSelect) {
    examplesSelect.addEventListener('change', (e) => {
        if (queryInput && e.target.value) {
            queryInput.value = e.target.value;
        }
    });
}

// ============================================
// Init
// ============================================

updateUI();
connect();
