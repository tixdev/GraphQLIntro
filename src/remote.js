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
const notesTextEl = document.getElementById('notes-text');
const notesStatusEl = document.getElementById('notes-step-status');
const audienceStatsPanel = document.getElementById('audience-stats-panel');
const audienceStatsList = document.getElementById('audience-stats-list');

// Slide steps configuration (must match index.html data-steps)
const SLIDE_STEPS = [2, 3, 3, 3, 3, 2, 6, 3, 3, 1, 1]; // steps per slide
const TOTAL_SLIDES = SLIDE_STEPS.length;

// Italian Speaker Notes Data
const SPEAKER_NOTES = [
    // Slide 0
    [
        "Benvenuti ragazzi, vi propongo un breve tuffo in GraphQL, un approccio alternativo al REST che stiamo valutando per le nostre API. Vedremo come può aiutarci a sciogliere alcuni nodi di integrazione e ridurre le latenze tra i nostri servizi di backend, tenendo presente anche gli indubbi vantaggi lato frontend.",
        "Iniziamo dal lato client. A sinistra c'è un esempio di 'Query': chi consuma l'API definisce con esattezza l'albero e i campi di cui ha bisogno per la sua vista.",
        "Sotto c'è il lato server: lo 'Schema'. È il contratto strongly-typed che mettiamo a disposizione, così chiunque ci chiami sa a priori quali entità e relazioni può leggere."
    ],
    // Slide 1
    [
        "Entrando più nel dettaglio, diamo una definizione formale. GraphQL non è una semplice libreria, ma un vero e proprio linguaggio di interrogazione strutturato per le API, accompagnato dal relativo motore di esecuzione a runtime.",
        "La genesi è interessante: nasce in Facebook nel 2012 per risolvere precise inefficienze legate al traffico dati. Con il classico approccio REST, per popolare un'interfaccia complessa erano costretti o a scaricare mastodontici documenti JSON pieni di dati inutili al momento, o a subire spaventose latenze a causa di multiple chiamate HTTP in cascata (il famoso waterfall). Serviva un paradigma che invertisse il controllo: a prescindere dal tipo di client, che sia una SPA desktop o un crontask backend, doveva poter richiedere un aggregato esatto con una singola istruzione.",
        "Il risultato strategico è proprio questo: il servizio chiamante richiede un subset preciso di dati, e riceve in risposta una proiezione esatta. Nessun kilobyte viene serializzato e inviato per errore.",
        "Sgomberiamo subito il campo da un malinteso comune, chiarendo cosa NON è GraphQL. Non è un database a grafo, non sostituisce il vostro SQL o NoSQL. Non è un ORM magico che scrive query per voi, e non è un layer di logica di business. Si tratta esclusivamente di un livello logico di trasporto e validazione formale, che delega ai servizi sottostanti l'onere del calcolo e del recupero dei dati."
    ],
    // Slide 2
    [
        "Scendiamo ora nell'architettura. Come funziona a livello operativo? Si basa interamente sull'interazione tra tre componenti core.",
        "1. Partendo dal riquadro di sinistra, abbiamo lo Schema. È il contratto formale su cui si regge l'intera integrazione: dichiara preventivamente entità, campi e relazioni, garantendo a chi consuma l'API una struttura dati fortemente tipizzata e sicura.",
        "2. La Query: ogni client o worker formula le proprie richieste usando rigorosamente quel vocabolario. Il parser GraphQL respingerà istantaneamente qualsiasi richiesta non conforme.",
        "3. I Resolvers: qui passiamo al calcolo effettivo. Sono funzioni dedicate al recupero fisico del singolo campo, che avvenga tramite query a un SQL Server locale, o un'integrazione REST / gRPC verso altri microservizi aziendali."
    ],
    // Slide 3
    [
        "Questa flessibilità architetturale ci permette di superare due grandi antipattern propri dell'integrazione REST. Il primo, di cui siamo tutti colpevoli, è l'Over-fetching.",
        "Immaginate lo scenario tipico: al client serve mostrare in UI solo il nome della persona e una lista testuale sintetica delle sue relazioni. L'endpoint REST `/api/persons/search` però ci scarica un mastodontico array JSON in cui ogni persona porta con sé anche tutti gli array espansi metadata, assets e balances.",
        "Con GraphQL, la request modella esattamente e unicamente questa esigenza: chiediamo il nodo 'name' e il numero identificativo delle sue 'relationships', scartando del tutto i pesanti metadata, assets e balances. La risposta del server sarà mirata, con un payload irrisorio.",
        "Se moltiplicate questo risparmio per migliaia di messaggi generati tra i nostri cluster interni, capite subito come l'uso di banda e il carico CPU per la serializzazione crollino drasticamente. Senza contare che, nel primo scenario REST, il backend avrebbe effettuato una quantità drammatica di costosi roundtrip verso il Database per recuperare tutti quegli array relazionali inutilizzati."
    ],
    // Slide 4
    [
        "Il problema diametralmente opposto è l'Under-fetching, il male oscuro che emerge quando cerchiamo di alleggerire REST iper-frammentando gli endpoint.",
        "Considerate una dashboard: recupera l'anagrafica base (`/persons`), estrae l'ID, fa una chiamata a `/relationships`, poi usa quegli ID per caricare gli `/assets` e infine i loro `/balances`. È il famigerato N+1 HTTP, un waterfall fatale per le latenze.",
        "Grazie all'attraversamento del grafo di GraphQL, il client invia una singola request massiva che naviga i collegamenti logici tra persona, relazioni e conti, in profondità, delegando al motore GraphQL tutto l'onere del recupero e dell'aggregazione.",
        "Questo colpo di spugna elimina i roundtrip di rete sequenziali tra client e server. Ma è importante sottolineare che riusciamo a navigare domini diversi in un solo colpo unicamente grazie alla Schema Federation: person, relationship e asset sono in realtà microservizi separati che il nostro Gateway cuce insieme dietro le quinte."
    ],
    // Slide 5
    [
        "Tirando le somme di queste inefficienze classiche, arriviamo al problema di governance: i famosi 'endpoint ad hoc'.",
        "Oggi un client o un partner team ci chiede un aggregato specifico. Domani ne serve un altro, e noi continuiamo a deployare e manutenere dozzine di interfacce REST fragili.",
        "GraphQL azzera questa operatività offrendo un singolo punto d'accesso universale. Ribaltiamo la responsabilità: è chi consuma le API a definire dinamicamente, tramite la propria query, l'esatta struttura dati di cui ha bisogno, e lo fa in modo strettamente tipizzato."
    ],
    // Slide 6
    [
        "Facendo un punto della situazione su ciò che ci portiamo a casa implementandolo:",
        "Efficienza netta: le comunicazioni trasferiscono payload ottimizzati, azzerando le informazioni superflue.",
        "Performance in latenza riducendo l'overhead dovuto alle connessioni TCP e handshake ripetuti.",
        "Developer Experience eccellente: lo Schema è autodescrittivo, e tramite interfaccia (come Nitro) abbiamo type ahead per navigare l'API intuitivamente.",
        "Disaccoppiamento quasi assoluto: chi invoca i dati vive un ciclo di sviluppo svincolato dalle release backend, decidendo in autonomia quali dati consumare.",
        "Type Safety by design: le strutture opzionali o non nullable garantiscono integrità formale prima dell'avvio della transazione.",
        "Versioning morbido: si possono espandere e deprecare porzioni di schema informando dinamicamente i client, mettendo fine ai break derivanti dalle v1, v2, v3."
    ],
    // Slide 7
    [
        "Siamo di fronte alla soluzione definitiva e priva di difetti? Certamente no. Con la complessità arbitraria della Query lato client, il rischio si sposta violentemente sul Database.",
        "Se iteriamo una lista di 50 persone chiedendone in innesto le relative relazioni, un set passivo di resolvers genererà una singola query sull'entità root e 50 sub-query in loop (il famelico N+1 DB).",
        "Ci viene in salvataggio un potente middleware pattern: il DataLoader. Agisce come un buffer temporale, collezionando le chiavi invocate senza procedere istantaneamente all'I/O.",
        "Non appena il branch logico si dirama, DataLoader attua il dispatch iniettando una sola macro-interrogazione batchata (WHERE IN). Passare da 51 query a 2 è la chiave di volta prestazionale."
    ],
    // Slide 8
    [
        "Arriviamo all'ultima grande sfida architetturale. L'ambiente in cui lavoriamo noi è a microservizi. E GraphQL suona tanto come un collo di bottiglia centralizzato, no? Qui entra la Federation.",
        "Costruiamo una facciata unificata, un Gateway router che riceve tutto il traffico, senza ospitare alcuna logica di tracciamento dati.",
        "Parallelamente, ogni dominio (il team ordini, il team billing) proietta in upstream il proprio GraphQL ristretto (Subgraph). Il gateway lo cuce con gli altri a runtime operando un 'supergrafo' coerente per chi sta a monte.",
        "L'industria offre ecosistemi robusti. Apollo è lo standard per NodeJS, ma considerando la nostra base .NET, possiamo fare grande affidamento su Hot Chocolate Fusion per risultati eccellenti."
    ],
    // Slide 9
    [
        "A questo punto stacchiamo dalla teoria. Concedetemi un attimo e passiamo alla verifica sul campo.",
        "Attraverso quest'interfaccia simuleremo delle transazioni reali ai nostri nodi e guarderemo come risponde il motore alle nostre richieste di lettura.",
    ],
    // Slide 10
    [
        "Ma in realtà queste cose rendono solo se mettete le mani in pasta. Per farvi un'idea pratica,",
        "il server di dev è in esecuzione all'indirizzo che vedete in homepage. Giocateci, c'è una lista di casi d'uso copiosa a destra, provateli e analizzatene le reazioni termiche sulla console."
    ]
];
// SLIDE_STEPS configuration and TOTAL_SLIDES are already defined above

let currentSlide = 0;
let currentStep = 0;
let ws = null;
let latestClientQueries = {};

slideTotal.textContent = TOTAL_SLIDES;

// ============================================
// WebSocket
// ============================================

function connect() {
    const protocol = location.protocol === 'https:' ? 'wss' : 'ws';
    const host = location.host; // includes port
    ws = new WebSocket(`${protocol}://${host}/ws?role=presenter`);

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
            if (data.clientQueries) {
                latestClientQueries = data.clientQueries;
                renderQueryStats();
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

        if (data.type === 'query-stats') {
            latestClientQueries = data.stats;
            renderQueryStats();
        }
    };
}

function renderQueryStats() {
    if (!audienceStatsList) return;

    const clients = Object.keys(latestClientQueries);
    if (clients.length === 0) {
        audienceStatsList.innerHTML = '<div class="stats-empty">No queries executed yet.</div>';
        return;
    }

    // Sort by count descending
    clients.sort((a, b) => latestClientQueries[b] - latestClientQueries[a]);

    audienceStatsList.innerHTML = clients.map(clientId => `
        <div class="stat-item updated">
            <span class="stat-client">${clientId}</span>
            <span class="stat-count">${latestClientQueries[clientId]}</span>
        </div>
    `).join('');

    // Remove highlight class after animation
    setTimeout(() => {
        audienceStatsList.querySelectorAll('.stat-item').forEach(el => el.classList.remove('updated'));
    }, 500);
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

    // Update Speaker Notes
    if (notesTextEl && notesStatusEl) {
        notesStatusEl.textContent = `Slide ${currentSlide + 1} — Step ${currentStep}`;
        if (SPEAKER_NOTES[currentSlide] && SPEAKER_NOTES[currentSlide][currentStep]) {
            notesTextEl.textContent = SPEAKER_NOTES[currentSlide][currentStep];
        } else {
            notesTextEl.textContent = "No notes for this step.";
        }
    }
    // Show stats panel only on the last slide "Try It Yourself" (index 10)
    if (audienceStatsPanel) {
        audienceStatsPanel.style.display = currentSlide === 10 ? 'block' : 'none';
        if (currentSlide === 10) {
            renderQueryStats(); // Force re-render incase panel was hidden
        }
    }
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
