const navLinks = document.querySelectorAll('.nav-link');

function navLinkActive() {
    navLinks.forEach(n => n.classList.remove('active'));
    this.classList.add('active');
    localStorage.setItem('activeNavHref', this.getAttribute('href'));
}

navLinks.forEach(n => n.addEventListener('click', navLinkActive));

function setActiveNav() {
    const currentPath = window.location.pathname;
    const currentHash = window.location.hash;
    let activeLink = null;
    navLinks.forEach(link => {
        const linkHref = link.getAttribute('href');
        const fullPath = new URL(linkHref, window.location.href).href;
        if (linkHref === currentHash || fullPath === window.location.href) {
            activeLink = link;
        }
    });

    if (!activeLink) {
        const savedHref = localStorage.getItem('activeNavHref');
        if (savedHref) {
            activeLink = document.querySelector(`.nav-link[href="${savedHref}"]`);
        }
    }
    if (activeLink) {
        activeLink.classList.add('active');
    }
}

setActiveNav();
