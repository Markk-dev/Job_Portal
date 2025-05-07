document.addEventListener('DOMContentLoaded', function() {
    const mainContent = document.querySelector('main');
    if (mainContent) {
        mainContent.classList.add('page-transition');
        setTimeout(() => {
            mainContent.classList.add('fade-in');
        }, 50);
    }

    // Handle navigation clicks
    document.querySelectorAll('.nav-links a').forEach(link => {
        link.addEventListener('click', function(e) {
            e.preventDefault();
            const href = this.getAttribute('href');
            
            // Add loading state to body and html
            document.body.classList.add('loading');
            document.documentElement.classList.add('loading');
            
            // Add fade-out class
            if (mainContent) {
                mainContent.classList.remove('fade-in');
                mainContent.classList.add('fade-out');
            }

            // Navigate after transition
            setTimeout(() => {
                window.location.href = href;
            }, 400);
        });
    });
}); 