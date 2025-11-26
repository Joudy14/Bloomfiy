// Common functionality for all pages
function initCommon() {
    // Add current year to copyright
    const copyrightElements = document.querySelectorAll('.copyright p');
    copyrightElements.forEach(element => {
        element.innerHTML = element.innerHTML.replace('2024', new Date().getFullYear());
    });

    // Add loading animation
    window.addEventListener('load', function() {
        document.body.style.opacity = '0';
        document.body.style.transition = 'opacity 0.3s ease';
        
        setTimeout(() => {
            document.body.style.opacity = '1';
        }, 100);
    });

    
    // Animate cards and grid items across all pages
    animateCards();
    
    // Animate buttons across all pages
    animateButtons();
}


// Animate cards and grid items
function animateCards() {
    const cards = document.querySelectorAll('.event-card, .blog-post, .value-item, .why-card, .flower-card');
    const cardObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.opacity = '1';
                entry.target.style.transform = 'translateY(0) scale(1)';
            }
        });
    }, { threshold: 0.2 });

    cards.forEach((card, index) => {
        card.style.opacity = '0';
        card.style.transform = 'translateY(40px) scale(0.95)';
        card.style.transition = 'opacity 0.6s ease, transform 0.5s ease';
        
        // Stagger the animation
        card.style.transitionDelay = `${index * 0.1}s`;
        
        cardObserver.observe(card);
    });
}

// Animate buttons with effect
function animateButtons() {
    const buttons = document.querySelectorAll('.button, .subscribe-btn, .discover-link, .read-more, .view-details');
    const buttonObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.opacity = '1';
                entry.target.style.transform = 'translateY(0)';
            }
        });
    }, { threshold: 0.5 });

    buttons.forEach(button => {
        button.style.opacity = '0';
        button.style.transform = 'translateY(20px)';
        button.style.transition = 'opacity 0.5s ease, transform 0.5s ease';
        buttonObserver.observe(button);
    });
}

// Main page functionality
function initMainPage() {
    // Smooth scrolling for navigation links
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });

    document.addEventListener('DOMContentLoaded', function () {
        initParallax();
    });

    // Flower image slider
    const flowerSliders = document.querySelectorAll('.flower-slider');
    if (flowerSliders.length > 0) {
        flowerSliders.forEach((slider) => {
            const slides = slider.querySelector('.flower-slides');
            let currentSlide = 0;
            
            function nextSlide() {
                currentSlide = (currentSlide + 1) % 3;
                slides.style.transform = `translateX(-${currentSlide * 33.333}%)`;
            }
            
            setInterval(nextSlide, 3000);
        });
    }
}

// About page functionality
function initAboutPage() {
    // Fade in timeline items
    const timelineItems = document.querySelectorAll('.timeline-item');
    if (timelineItems.length > 0) {
        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.style.opacity = '1';
                    entry.target.style.transform = 'translateY(0)';
                }
            });
        }, { threshold: 0.3 });

        timelineItems.forEach(item => {
            item.style.opacity = '0';
            item.style.transform = 'translateY(30px)';
            item.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
            observer.observe(item);
        });
    }

    // Fade in team members
    const teamMembers = document.querySelectorAll('.team-member');
    if (teamMembers.length > 0) {
        const teamObserver = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.style.opacity = '1';
                    entry.target.style.transform = 'translateY(0) scale(1)';
                }
            });
        }, { threshold: 0.2 });

        teamMembers.forEach((member, index) => {
            member.style.opacity = '0';
            member.style.transform = 'translateY(40px) scale(0.95)';
            member.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
            member.style.transitionDelay = `${index * 0.1}s`;
            teamObserver.observe(member);
        });
    }


    // Animate intro section
    const introContent = document.querySelector('.intro-text');
    const introImage = document.querySelector('.intro-image');
    if (introContent && introImage) {
        [introContent, introImage].forEach((element, index) => {
            element.style.opacity = '0';
            element.style.transform = 'translateX(' + (index === 0 ? '-50px' : '50px') + ')';
            element.style.transition = 'opacity 0.8s ease, transform 0.8s ease';
            
            setTimeout(() => {
                element.style.opacity = '1';
                element.style.transform = 'translateX(0)';
            }, 300 + (index * 200));
        });
    }
}

// Contact page functionality
function initContactPage() {

    const form = document.getElementById("contactForm");
    const successMessage = document.getElementById("successMessage");
    const sendAnother = document.getElementById("sendAnother");

    form.addEventListener("submit", function (e) {
        e.preventDefault();
        form.style.display = "none";
        successMessage.style.display = "block";
        form.reset();
    });

    sendAnother.addEventListener("click", function (e) {
        e.preventDefault();
        successMessage.style.display = "none";
        form.style.display = "flex";
    });

    const contactForm = document.getElementById('contactForm');
    if (!contactForm) return;

    const inputs = contactForm.querySelectorAll('input, textarea');


    // Enhanced form submission
    contactForm.addEventListener('submit', function(event) {
        event.preventDefault();

        const submitBtn = this.querySelector('.send-btn');
        const originalText = submitBtn.textContent;
        
        // Show loading state
        submitBtn.textContent = 'Sending...';
        submitBtn.disabled = true;

        const name = document.getElementById('name').value.trim();
        const email = document.getElementById('email').value.trim();
        const message = document.getElementById('message').value.trim();
        const successMsg = document.getElementById('successMessage');

        if (name && email && message) {
            // Simulate API call delay
            setTimeout(() => {
                successMsg.classList.add('show');
                this.reset();
                
                // Reset button after delay
                setTimeout(() => {
                    submitBtn.textContent = originalText;
                    submitBtn.disabled = false;
                    successMsg.classList.remove('show');
                }, 3000);
            }, 1000);
        } else {
            // Reset button if validation fails
            submitBtn.textContent = originalText;
            submitBtn.disabled = false;
        }
    });

    // Animate contact form and details
    const contactElements = document.querySelectorAll('.contact-form, .contact-details');
    contactElements.forEach((element, index) => {
        element.style.opacity = '0';
        element.style.transform = 'translateY(30px)';
        element.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
        
        setTimeout(() => {
            element.style.opacity = '1';
            element.style.transform = 'translateY(0)';
        }, 500 + (index * 200));
    });
}

// Initialize everything when page loads
document.addEventListener('DOMContentLoaded', function() {
    initCommon();
    
    // Check which page we're on and initialize specific functionality
    const currentPage = window.location.pathname.split('/').pop();
    
    if (currentPage === 'index.html' || currentPage === '' || currentPage === 'index1.html') {
        initMainPage();
    } else if (currentPage === 'about.html') {
        initAboutPage();
    } else if (currentPage === 'contact.html') {
        initContactPage();
    }
    
    // Fallback: Check for specific elements to determine page
    if (document.querySelector('.flower-slider')) {
        initMainPage();
    }
    if (document.querySelector('.timeline-item')) {
        initAboutPage();
    }
    if (document.getElementById('contactForm')) {
        initContactPage();
    }
});