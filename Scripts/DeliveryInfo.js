// Delivery Information Page JavaScript
document.addEventListener('DOMContentLoaded', function() {
    // Add simple animations to delivery cards
    const deliveryCards = document.querySelectorAll('.delivery-card');
    
    const cardObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.opacity = '1';
                entry.target.style.transform = 'translateY(0)';
            }
        });
    }, { threshold: 0.2 });

    deliveryCards.forEach((card, index) => {
        card.style.opacity = '0';
        card.style.transform = 'translateY(30px)';
        card.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
        card.style.transitionDelay = `${index * 0.1}s`;
        cardObserver.observe(card);
    });

    // Animate detail items
    const detailItems = document.querySelectorAll('.detail-item');
    
    const detailObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.opacity = '1';
                entry.target.style.transform = 'translateX(0)';
            }
        });
    }, { threshold: 0.3 });

    detailItems.forEach((item, index) => {
        item.style.opacity = '0';
        item.style.transform = `translateX(${index % 2 === 0 ? '-30px' : '30px'})`;
        item.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
        item.style.transitionDelay = `${index * 0.1}s`;
        detailObserver.observe(item);
    });
});