    // FANCY JAVASCRIPT FOR PRODUCT DETAILS PAGE
    document.addEventListener('DOMContentLoaded', function() {
        console.log('🌸 Bloomfiy Product Details - Fancy Edition Loaded!');

    // ===== PAGE LOAD ANIMATIONS =====
    // Animate hero section
    const hero = document.querySelector('.product-detail-hero');
    if (hero) {
        hero.style.opacity = '0';
    hero.style.transform = 'translateY(30px)';
            
            setTimeout(() => {
        hero.style.transition = 'all 0.8s ease';
    hero.style.opacity = '1';
    hero.style.transform = 'translateY(0)';
            }, 200);
        }

    // Stagger animation for product grid
    const productImages = document.querySelector('.product-images');
    const productInfo = document.querySelector('.product-info-detail');

    if (productImages) {
        productImages.style.opacity = '0';
    productImages.style.transform = 'translateX(-30px)';
            
            setTimeout(() => {
        productImages.style.transition = 'all 0.6s ease 0.3s';
    productImages.style.opacity = '1';
    productImages.style.transform = 'translateX(0)';
            }, 400);
        }

    if (productInfo) {
        productInfo.style.opacity = '0';
    productInfo.style.transform = 'translateX(30px)';
            
            setTimeout(() => {
        productInfo.style.transition = 'all 0.6s ease 0.5s';
    productInfo.style.opacity = '1';
    productInfo.style.transform = 'translateX(0)';
            }, 600);
        }

    // ===== IMAGE GALLERY INTERACTIONS =====
    // Thumbnail click with smooth transition
    const thumbnails = document.querySelectorAll('.thumbnail');
    const mainImage = document.getElementById('mainProductImage');
        
        thumbnails.forEach(thumb => {
        thumb.addEventListener('click', function () {
            // Remove active class from all thumbnails
            thumbnails.forEach(t => t.classList.remove('active'));

            // Add active class to clicked thumbnail
            this.classList.add('active');

            // Update main image with fade effect
            if (mainImage) {
                const newSrc = this.querySelector('img').src;

                // Fade out
                mainImage.style.opacity = '0';
                mainImage.style.transform = 'scale(0.95)';

                setTimeout(() => {
                    mainImage.src = newSrc;

                    // Fade in with scale
                    mainImage.style.opacity = '1';
                    mainImage.style.transform = 'scale(1)';

                    // Add floating effect
                    mainImage.style.animation = 'pulse 0.6s ease';
                    setTimeout(() => mainImage.style.animation = '', 600);
                }, 200);
            }
        });
        });

    // ===== COLOR SELECTION ANIMATIONS =====
    const colorOptions = document.querySelectorAll('.color-option-detail');
        
        colorOptions.forEach(option => {
        option.addEventListener('click', function () {
            // Remove active class from all options
            colorOptions.forEach(opt => {
                opt.classList.remove('active');
                opt.style.transform = 'translateX(0)';
            });

            // Add active class to clicked option with animation
            this.classList.add('active');
            this.style.transform = 'translateX(10px)';

            // Update main image if data-image exists
            const newImage = this.dataset.image;
            if (newImage && mainImage) {
                // Create ripple effect
                createRippleEffect(this);

                // Fade transition for image
                mainImage.style.opacity = '0';
                mainImage.style.transform = 'scale(0.9) rotate(-2deg)';

                setTimeout(() => {
                    mainImage.src = newImage;
                    mainImage.style.opacity = '1';
                    mainImage.style.transform = 'scale(1) rotate(0deg)';
                }, 300);
            }

            // Update price with animation
            const newPrice = this.dataset.price;
            const priceElement = document.querySelector('.current-price');
            const addToCartBtn = document.querySelector('.add-to-cart-btn.large');

            if (priceElement && newPrice) {
                priceElement.style.transform = 'scale(1.2)';
                priceElement.style.color = '#8b7355';

                setTimeout(() => {
                    priceElement.textContent = `$${newPrice}`;
                    priceElement.style.transform = 'scale(1)';
                    priceElement.style.color = '';
                }, 150);
            }

            if (addToCartBtn && newPrice) {
                addToCartBtn.textContent = `Add to Cart - $${newPrice}`;
                addToCartBtn.dataset.price = newPrice;

                // Button pulse animation
                addToCartBtn.style.animation = 'pulse 0.5s ease';
                setTimeout(() => addToCartBtn.style.animation = '', 500);
            }
        });
        });

    // ===== QUANTITY SELECTOR ANIMATIONS =====
    const minusBtn = document.querySelector('.quantity-btn.minus');
    const plusBtn = document.querySelector('.quantity-btn.plus');
    const quantityInput = document.querySelector('.quantity-input');

    if (minusBtn && plusBtn && quantityInput) {
        // Button click animations
        [minusBtn, plusBtn].forEach(btn => {
            btn.addEventListener('click', function () {
                createRippleEffect(this);
                this.style.transform = 'scale(0.9)';
                setTimeout(() => this.style.transform = 'scale(1)', 150);
            });
        });

    // Minus button
    minusBtn.addEventListener('click', function() {
        let value = parseInt(quantityInput.value);
                if (value > 1) {
        quantityInput.value = value - 1;
    animateQuantityChange(quantityInput, -1);
                } else {
        // Shake animation for minimum value
        quantityInput.style.animation = 'shake 0.5s ease';
                    setTimeout(() => quantityInput.style.animation = '', 500);
                }
            });

    // Plus button
    plusBtn.addEventListener('click', function() {
        let value = parseInt(quantityInput.value);
    if (value < 10) {
        quantityInput.value = value + 1;
    animateQuantityChange(quantityInput, 1);
                } else {
        // Shake animation for maximum value
        quantityInput.style.animation = 'shake 0.5s ease';
                    setTimeout(() => quantityInput.style.animation = '', 500);
                }
            });

    // Input focus effects
    quantityInput.addEventListener('focus', function() {
        this.parentElement.style.boxShadow = '0 0 0 2px rgba(90, 124, 90, 0.3)';
    this.parentElement.style.borderColor = '#5a7c5a';
            });

    quantityInput.addEventListener('blur', function() {
        this.parentElement.style.boxShadow = '';
    this.parentElement.style.borderColor = '#e0e0e0';
            });
        }

    // ===== ADD TO CART FANCY ANIMATION =====
    const addToCartBtn = document.querySelector('.add-to-cart-btn.large');
    if (addToCartBtn) {
        addToCartBtn.addEventListener('click', function (e) {
            e.preventDefault();

            // Create ripple effect
            createRippleEffect(this);

            // Button success animation
            this.style.transform = 'scale(0.95)';
            this.innerHTML = '🌸 Adding to Cart...';
            this.style.background = '#8b7355';

            // Get product details for animation
            const productImage = document.querySelector('.main-image img');
            const productName = document.querySelector('.product-info-detail h1').textContent;
            const quantity = parseInt(quantityInput?.value) || 1;
            const price = this.dataset.price;

            // Create flying flower animation
            if (productImage) {
                createFlyingFlowerAnimation(productImage, quantity);
            }

            // Update cart count with animation
            const cartCount = document.querySelector('.cart-count');
            if (cartCount) {
                const currentCount = parseInt(cartCount.textContent) || 0;
                const newCount = currentCount + quantity;

                // Count up animation
                animateCountUp(cartCount, currentCount, newCount);

                // Bounce animation
                cartCount.style.transform = 'scale(1.5)';
                setTimeout(() => cartCount.style.transform = 'scale(1)', 300);
            }

            // Success message after animation
            setTimeout(() => {
                this.innerHTML = `✓ ${quantity} Added to Cart!`;
                this.style.background = '#27ae60';

                // Create confetti effect
                createConfettiEffect(this);
            }, 800);

            // Reset button after delay
            setTimeout(() => {
                this.innerHTML = `Add to Cart - $${price}`;
                this.style.background = '';
                this.style.transform = 'scale(1)';
            }, 2500);
        });
        }

    // ===== INTERACTIVE CARE GUIDE =====
    const careTips = document.querySelectorAll('.care-tip');
    const careSteps = document.querySelectorAll('.care-step-expanded');
        
        careTips.forEach(tip => {
        tip.addEventListener('click', function () {
            const step = this.dataset.step;

            // Remove active states
            careTips.forEach(t => t.classList.remove('active'));
            careSteps.forEach(s => s.classList.remove('active'));

            // Add active states with animation
            this.classList.add('active');
            const targetStep = document.querySelector(`.care-step-expanded[data-step="${step}"]`);
            if (targetStep) {
                targetStep.classList.add('active');

                // Animate in
                targetStep.style.opacity = '0';
                targetStep.style.transform = 'translateX(-20px)';

                setTimeout(() => {
                    targetStep.style.transition = 'all 0.5s ease';
                    targetStep.style.opacity = '1';
                    targetStep.style.transform = 'translateX(0)';
                }, 50);
            }

            // Create ripple effect on care image
            const careImage = document.querySelector('.care-image-large');
            if (careImage) {
                careImage.style.transform = 'scale(1.02)';
                setTimeout(() => careImage.style.transform = 'scale(1)', 300);
            }
        });
        });

    // ===== OCCASION TAGS INTERACTION =====
    const occasionTags = document.querySelectorAll('.occasion-tag');
        occasionTags.forEach(tag => {
        tag.addEventListener('click', function () {
            // Toggle active state
            this.classList.toggle('active');

            if (this.classList.contains('active')) {
                this.style.background = '#5a7c5a';
                this.style.color = 'white';

                // Create floating effect
                createFloatingHearts(this);
            } else {
                this.style.background = '';
                this.style.color = '';
            }
        });
        });

    // ===== SCROLL ANIMATIONS FOR SECTIONS =====
    const detailSections = document.querySelectorAll('.detail-section');
        
        const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.opacity = '1';
                entry.target.style.transform = 'translateY(0)';
                entry.target.style.transition = 'all 0.8s ease';
            }
        });
        }, {threshold: 0.1 });
        
        detailSections.forEach(section => observer.observe(section));

    // ===== HELPER FUNCTIONS =====
    function createRippleEffect(element) {
            const ripple = document.createElement('span');
    ripple.classList.add('ripple-effect');

    const rect = element.getBoundingClientRect();
    const size = Math.max(rect.width, rect.height);
    const x = e.clientX - rect.left - size / 2;
    const y = e.clientY - rect.top - size / 2;

    ripple.style.width = ripple.style.height = size + 'px';
    ripple.style.left = x + 'px';
    ripple.style.top = y + 'px';

    element.style.position = 'relative';
    element.style.overflow = 'hidden';
    element.appendChild(ripple);
            
            setTimeout(() => {
        element.removeChild(ripple);
            }, 600);
        }

    function animateQuantityChange(input, direction) {
        input.style.transform = `scale(1.1) translateX(${direction * 5}px)`;
            input.style.color = direction > 0 ? '#27ae60' : '#e74c3c';
            
            setTimeout(() => {
        input.style.transform = 'scale(1) translateX(0)';
    input.style.color = '';
            }, 300);
        }

    function createFlyingFlowerAnimation(productImage, quantity) {
            for (let i = 0; i < quantity; i++) {
        setTimeout(() => {
            const flyingFlower = document.createElement('div');
            flyingFlower.innerHTML = '🌸';
            flyingFlower.style.position = 'fixed';
            flyingFlower.style.left = productImage.getBoundingClientRect().left + 'px';
            flyingFlower.style.top = productImage.getBoundingClientRect().top + 'px';
            flyingFlower.style.fontSize = '24px';
            flyingFlower.style.zIndex = '10000';
            flyingFlower.style.pointerEvents = 'none';
            flyingFlower.style.transition = 'all 0.8s cubic-bezier(0.68, -0.55, 0.265, 1.55)';

            document.body.appendChild(flyingFlower);

            const cartIcon = document.querySelector('.cart-icon') || document.querySelector('.cart-count');
            if (cartIcon) {
                const cartRect = cartIcon.getBoundingClientRect();

                setTimeout(() => {
                    flyingFlower.style.left = cartRect.left + 'px';
                    flyingFlower.style.top = cartRect.top + 'px';
                    flyingFlower.style.transform = 'scale(0.3) rotate(360deg)';
                    flyingFlower.style.opacity = '0';
                }, 50);

                setTimeout(() => {
                    document.body.removeChild(flyingFlower);
                }, 850);
            }
        }, i * 200);
            }
        }

    function animateCountUp(element, start, end) {
        let current = start;
            const increment = end > start ? 1 : -1;
    const stepTime = Math.abs(Math.floor(200 / (end - start)));
            
            const timer = setInterval(() => {
        current += increment;
    element.textContent = current;

    if (current === end) {
        clearInterval(timer);
                }
            }, stepTime);
        }

    function createConfettiEffect(element) {
            const colors = ['#5a7c5a', '#8b7355', '#d4af87', '#27ae60', '#e74c3c'];

    for (let i = 0; i < 20; i++) {
        setTimeout(() => {
            const confetti = document.createElement('div');
            confetti.innerHTML = ['🌸', '💐', '🌷', '🌹', '🥀'][Math.floor(Math.random() * 5)];
            confetti.style.position = 'fixed';
            confetti.style.left = element.getBoundingClientRect().left + 'px';
            confetti.style.top = element.getBoundingClientRect().top + 'px';
            confetti.style.fontSize = '16px';
            confetti.style.zIndex = '10000';
            confetti.style.pointerEvents = 'none';
            confetti.style.opacity = '1';

            document.body.appendChild(confetti);

            // Random animation
            const randomX = (Math.random() - 0.5) * 200;
            const randomY = -Math.random() * 100 - 50;
            const randomRotation = (Math.random() - 0.5) * 360;

            confetti.style.transition = 'all 1s ease';
            setTimeout(() => {
                confetti.style.transform = `translate(${randomX}px, ${randomY}px) rotate(${randomRotation}deg)`;
                confetti.style.opacity = '0';
            }, 10);

            setTimeout(() => {
                if (confetti.parentNode) {
                    document.body.removeChild(confetti);
                }
            }, 1000);
        }, i * 50);
            }
        }

    function createFloatingHearts(element) {
            for (let i = 0; i < 3; i++) {
                const heart = document.createElement('div');
    heart.innerHTML = '💚';
    heart.style.position = 'absolute';
    heart.style.fontSize = '14px';
    heart.style.pointerEvents = 'none';
    heart.style.zIndex = '1';
    heart.style.opacity = '1';

    const rect = element.getBoundingClientRect();
    heart.style.left = rect.left + rect.width / 2 + 'px';
    heart.style.top = rect.top + 'px';

    document.body.appendChild(heart);

    const randomX = (Math.random() - 0.5) * 60;
    const randomY = -Math.random() * 40 - 20;

    heart.style.transition = 'all 0.8s ease';
                setTimeout(() => {
        heart.style.transform = `translate(${randomX}px, ${randomY}px)`;
    heart.style.opacity = '0';
                }, 10);
                
                setTimeout(() => {
                    if (heart.parentNode) {
        document.body.removeChild(heart);
                    }
                }, 800);
            }
        }
    });

    // Add shake animation for invalid inputs
    const style = document.createElement('style');
    style.textContent = `
    @keyframes shake {
        0 %, 100 % { transform: translateX(0); }
            25% {transform: translateX(-5px); }
    75% {transform: translateX(5px); }
        }

    @keyframes float {
        0 %, 100 % { transform: translateY(0px); }
            50% {transform: translateY(-10px); }
        }

    .floating-element {
        animation: float 3s ease-in-out infinite;
        }

    /* Enhance existing hover effects */
    .product-images img {
        transition: all 0.3s ease !important;
        }

    .color-option-detail {
        transition: all 0.3s ease !important;
        }

    .feature-item {
        transition: all 0.3s ease !important;
        }

    .occasion-tag {
        transition: all 0.3s ease !important;
        }
    `;
document.head.appendChild(style);

// Helper function for quantity animation
function animateQuantityChange(input, direction) {
    input.style.transform = `scale(1.1) translateX(${direction * 5}px)`;
    input.style.color = direction > 0 ? '#27ae60' : '#e74c3c';

    setTimeout(() => {
        input.style.transform = 'scale(1) translateX(0)';
        input.style.color = '';
    }, 300);
}

// Helper function for ripple effect
function createRippleEffect(element) {
    const ripple = document.createElement('span');
    ripple.classList.add('ripple-effect');

    const rect = element.getBoundingClientRect();
    const size = Math.max(rect.width, rect.height);
    const x = size / 2;
    const y = size / 2;

    ripple.style.width = ripple.style.height = size + 'px';
    ripple.style.left = '0px';
    ripple.style.top = '0px';

    element.style.position = 'relative';
    element.style.overflow = 'hidden';
    element.appendChild(ripple);

    setTimeout(() => {
        if (ripple.parentNode === element) {
            element.removeChild(ripple);
        }
    }, 600);
}

// Helper function for count animation
function animateCountUp(element, start, end) {
    let current = start;
    const increment = end > start ? 1 : -1;
    const stepTime = Math.abs(Math.floor(200 / (end - start)));

    const timer = setInterval(() => {
        current += increment;
        element.textContent = current;

        if (current === end) {
            clearInterval(timer);
        }
    }, stepTime);
}

// ===== COLOR SELECTION FIX - WORKING VERSION =====
document.addEventListener('DOMContentLoaded', function () {
    console.log('🌸 Color switching initialized');

    const colorOptions = document.querySelectorAll('.color-option-detail');
    const mainImage = document.getElementById('mainProductImage');

    colorOptions.forEach(option => {
        option.addEventListener('click', function () {
            console.log('Color clicked:', this.dataset.color);
            console.log('Image path:', this.dataset.image);

            // Remove active class from all options
            colorOptions.forEach(opt => {
                opt.classList.remove('active');
            });

            // Add active class to clicked option
            this.classList.add('active');

            // Update main image
            const newImage = this.dataset.image;
            if (newImage && mainImage) {
                // Simple image change without complex animations
                mainImage.src = newImage;
                mainImage.alt = `${this.dataset.color} Peony`;
                console.log('Image updated to:', newImage);
            }

            // Update price
            const newPrice = this.dataset.price;
            const priceElement = document.querySelector('.product-price-detail .current-price');
            const addToCartBtn = document.querySelector('.add-to-cart-btn.large');

            if (priceElement && newPrice) {
                priceElement.textContent = `$${newPrice}`;
            }

            if (addToCartBtn && newPrice) {
                addToCartBtn.textContent = `Add to Cart - $${newPrice}`;
                addToCartBtn.dataset.price = newPrice;
            }
        });
    });

    // Test: Force first color to be active on load
    const firstColor = document.querySelector('.color-option-detail');
    if (firstColor) {
        firstColor.classList.add('active');
    }
});