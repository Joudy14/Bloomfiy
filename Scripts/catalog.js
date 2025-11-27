// Minimal shop.js - TEST VERSION
document.addEventListener('DOMContentLoaded', function () {
    console.log('✅ Shop JS loaded');

    // Test: Add click listener directly
    const productCard = document.querySelector('.product-card');
    if (productCard) {
        productCard.addEventListener('click', function (e) {
            console.log('🖱️ Card clicked - navigating to details');
            window.location.href = '/Product/Details/7';
        });
    } else {
        console.log('❌ No product card found');
    }
});


// CSS for ripple animation
const style = document.createElement('style');
style.textContent = `
        @keyframes ripple {
            to {
                transform: scale(4);
                opacity: 0;
            }
        }

        .product-card {
            transition: transform 0.3s ease, box-shadow 0.3s ease !important;
        }

        .product-card:hover {
            transform: translateY(-10px) scale(1.02) !important;
            box-shadow: 0 20px 40px rgba(0,0,0,0.15) !important;
        }

        .filter-option {
            transition: all 0.3s ease !important;
        }

        .color-option {
            transition: all 0.3s ease !important;
        }

        .load-more-btn {
            transition: all 0.3s ease !important;
        }
    `;
document.head.appendChild(style);

    // Fancy JavaScript for interactive elements
    document.addEventListener('DOMContentLoaded', function () {
        console.log('🌸 Peony details loaded with fancy effects!');

        // Color switching
        const colorOptions = document.querySelectorAll('.color-option-detail');
        colorOptions.forEach(option => {
            option.addEventListener('click', function () {
                colorOptions.forEach(opt => opt.classList.remove('active'));
                this.classList.add('active');

                const mainImage = document.getElementById('mainProductImage');
                if (mainImage && this.dataset.image) {
                    // Fade effect for image change
                    mainImage.style.opacity = '0';
                    setTimeout(() => {
                        mainImage.src = this.dataset.image;
                        mainImage.style.opacity = '1';
                    }, 200);
                }

                const price = this.dataset.price;
                const priceElement = document.querySelector('.current-price');
                const addToCartBtn = document.querySelector('.add-to-cart-btn.large');

                if (priceElement) {
                    priceElement.style.transform = 'scale(1.1)';
                    setTimeout(() => {
                        priceElement.textContent = `$${price}`;
                        priceElement.style.transform = 'scale(1)';
                    }, 150);
                }
                if (addToCartBtn) {
                    addToCartBtn.textContent = `Add to Cart - $${price}`;
                    addToCartBtn.dataset.price = price;
                }
            });
        });

        // Quantity selector with animation
        const minusBtn = document.querySelector('.quantity-btn.minus');
        const plusBtn = document.querySelector('.quantity-btn.plus');
        const quantityInput = document.querySelector('.quantity-input');

        if (minusBtn && plusBtn && quantityInput) {
            [minusBtn, plusBtn].forEach(btn => {
                btn.addEventListener('click', function () {
                    this.style.transform = 'scale(0.9)';
                    setTimeout(() => {
                        this.style.transform = 'scale(1)';
                    }, 150);
                });
            });

            minusBtn.addEventListener('click', function () {
                let value = parseInt(quantityInput.value);
                if (value > 1) {
                    quantityInput.value = value - 1;
                    quantityInput.style.transform = 'scale(1.1)';
                    setTimeout(() => quantityInput.style.transform = 'scale(1)', 150);
                }
            });

            plusBtn.addEventListener('click', function () {
                let value = parseInt(quantityInput.value);
                if (value < 10) {
                    quantityInput.value = value + 1;
                    quantityInput.style.transform = 'scale(1.1)';
                    setTimeout(() => quantityInput.style.transform = 'scale(1)', 150);
                }
            });
        }

        // Interactive care guide
        const careTips = document.querySelectorAll('.care-tip');
        const careSteps = document.querySelectorAll('.care-step-expanded');

        careTips.forEach(tip => {
            tip.addEventListener('click', function () {
                const step = this.dataset.step;

                // Update active states
                careTips.forEach(t => t.classList.remove('active'));
                careSteps.forEach(s => s.classList.remove('active'));

                this.classList.add('active');
                document.querySelector(`.care-step-expanded[data-step="${step}"]`).classList.add('active');
            });
        });

        // Scroll animations for sections
        const sections = document.querySelectorAll('.detail-section');

        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.classList.add('visible');
                }
            });
        }, { threshold: 0.1 });

        sections.forEach(section => observer.observe(section));

        // Add to cart animation
        const addToCartBtn = document.querySelector('.add-to-cart-btn.large');
        if (addToCartBtn) {
            addToCartBtn.addEventListener('click', function () {
                this.innerHTML = '✓ Added to Cart!';
                this.style.background = '#27ae60';

                // Cart count animation
                const cartCount = document.querySelector('.cart-count');
                if (cartCount) {
                    const currentCount = parseInt(cartCount.textContent) || 0;
                    cartCount.textContent = currentCount + 1;
                    cartCount.style.transform = 'scale(1.5)';
                    setTimeout(() => cartCount.style.transform = 'scale(1)', 300);
                }

                setTimeout(() => {
                    this.innerHTML = `Add to Cart - $${this.dataset.price}`;
                    this.style.background = '';
                }, 2000);
            });
        }

        // Hover effects for product features
        const featureItems = document.querySelectorAll('.feature-item');
        featureItems.forEach(item => {
            item.addEventListener('mouseenter', function () {
                this.style.transform = 'translateX(10px)';
                this.style.transition = 'transform 0.3s ease';
            });

            item.addEventListener('mouseleave', function () {
                this.style.transform = 'translateX(0)';
            });
        });

        // Floating animation for care guide image
        const careImage = document.getElementById('careImage');
        if (careImage) {
            setInterval(() => {
                careImage.style.transform = 'translateY(-5px)';
                setTimeout(() => {
                    careImage.style.transform = 'translateY(0)';
                }, 2000);
            }, 4000);
        }
    });

