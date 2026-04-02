
// Client-side code is only for extra user experience.
// Real validation + spam detection must be done server-side (Controller + ModelState).

// window.addEventListener('contextmenu', e => e.preventDefault());
// Removed from active code: this does not improve security .

// window.addEventListener('keydown', e => {
//     if (e.key === 'Tab') {
//         e.preventDefault();
//     }
// });
// Removed from active code: Tab/Shift+Tab must work for keyboard accessibility (WCAG).

// function naiveEmailCheck(email) {
//     return /@/.test(email);
// }
// simple client-side check. Not required, and server-side validation remains leading.

function setupValidation() {
    const form = document.getElementById('contactForm');
    const status = document.getElementById('liveStatus');

    if (!form || !status) return;

    // const hp = document.getElementById('website');
    // not needed honeypot is checked server-side, because client-side checks can be bypassed.

    // const email = document.getElementById('Email');
    // const name = document.getElementById('Name');
    // const msg = document.getElementById('Message');
    // Not needed here.... field validation is handled server-side via ModelState

    // const echo = (id, value) => {
    //     document.getElementById(id).innerHTML = `\n <span>Probleem met: ${value}</span>\n `;
    // };
    // Not using innerHTML with user input is unsafe (DOM XSS risk). Use server-side validation messages instead.

    // [email, name, msg].forEach(el => { ... });
    // Not used: client-side validation is optional. The application must remain correct without JavaScript.

    form.addEventListener('submit', () => {
        // Status message (accessible when liveStatus has aria-live="polite")
        status.textContent = 'Formulier wordt verzonden...';
    });

    // form.addEventListener('submit', (e) => {
    //     if (hp.value) {
    //         e.preventDefault();
    //         alert('Spam gedetecteerd (client-side)!');
    //         return false;
    //     }
    //     return true;
    // });
    // Not using dit spam detection is implemented server-side.
}

window.addEventListener('DOMContentLoaded', setupValidation);