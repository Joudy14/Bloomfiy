// Shop functionality
document.addEventListener('DOMContentLoaded', function () {
    console.log('🛍️ Shop scripts loaded');

    initializeShopFilters();
    initializeProductInteractions();
    initializeAddToCart();
    initializeColorSwitching();
    initializeTabSystem();
    initializeLoadMore(); 
});

function initializeShopFilters() {
    // Color filter selection
    const colorOptions = document.querySelectorAll('.color-option');
    colorOptions.forEach(option => {
        option.addEventListener('click', function () {
            this.classList.toggle('active');
            filterProducts();
        });
    });

    // Price slider
    const priceSlider = document.querySelector('.price-slider');
    if (priceSlider) {
        priceSlider.addEventListener('input', function () {
            filterProducts();
        });
    }

    // Category checkboxes
    const categoryCheckboxes = document.querySelectorAll('.filter-option input[type="checkbox"]');
    categoryCheckboxes.forEach(checkbox => {
        checkbox.addEventListener('change', filterProducts);
    });

    // Sort select
    const sortSelect = document.querySelector('.sort-select');
    if (sortSelect) {
        sortSelect.addEventListener('change', function () {
            sortProducts(this.value);
        });
    }
}

function initializeColorSwitching() {
    // Catalog page color switching - SIMPLE VERSION
    const colorOptionsCatalog = document.querySelectorAll('.color-option-preview');
    colorOptionsCatalog.forEach(option => {
        option.addEventListener('click', function (e) {
            e.stopPropagation();
            const card = this.closest('.product-card');

            // Update active state
            card.querySelectorAll('.color-option-preview').forEach(opt => {
                opt.classList.remove('active');
            });
            this.classList.add('active');

            // Update price
            const price = this.dataset.price;
            const priceElement = card.querySelector('.current-price');
            if (priceElement) {
                priceElement.textContent = `$${price}`;
            }

            // Update main image
            const mainImage = card.querySelector('.main-product-image');
            if (mainImage) {
                mainImage.src = this.dataset.image;
            }
        });
    });

    // Detail page color switching - SIMPLE VERSION
    const colorOptionsDetail = document.querySelectorAll('.color-option-detail');
    colorOptionsDetail.forEach(option => {
        option.addEventListener('click', function () {
            // Remove active class from all options
            colorOptionsDetail.forEach(opt => opt.classList.remove('active'));

            // Add active class to clicked option
            this.classList.add('active');

            // Update main image only
            const mainImage = document.getElementById('mainProductImage');
            if (mainImage && this.dataset.image) {
                mainImage.src = this.dataset.image;
            }

            // Update price
            const price = this.dataset.price;
            updateProductPrice(price);

            // Update add to cart button
            updateAddToCartButton(this.dataset.color, price);
        });
    });
}

    function initializeLoadMore() {
        const loadMoreBtn = document.getElementById('loadMoreBtn');
        const noMoreProducts = document.querySelector('.no-more-products');
        const productsGrid = document.querySelector('.products-grid');

        if (!loadMoreBtn) return;

        let currentPage = 1;
        const productsPerPage = 6;
        let isLoading = false;

        loadMoreBtn.addEventListener('click', async function () {
            if (isLoading) return;

            isLoading = true;
            loadMoreBtn.classList.add('loading');

            try {
                // Simulate API call delay
                await new Promise(resolve => setTimeout(resolve, 1000));

                currentPage++;

                // Simulate loading more products
                const newProducts = generateMockProducts(currentPage);

                if (newProducts.length > 0) {
                    // Add new products to grid
                    newProducts.forEach(product => {
                        productsGrid.appendChild(createProductCard(product));
                    });

                    // Update results count
                    updateResultsCount();

                    // Check if we should hide the button
                    if (currentPage >= 3) { // Change this condition based on your actual data
                        loadMoreBtn.style.display = 'none';
                        noMoreProducts.style.display = 'block';
                    }
                } else {
                    // No more products
                    loadMoreBtn.style.display = 'none';
                    noMoreProducts.style.display = 'block';
                }
            } catch (error) {
                console.error('Error loading more products:', error);
            } finally {
                isLoading = false;
                loadMoreBtn.classList.remove('loading');
            }
        });
    }

    function generateMockProducts(page) {
        // Mock data for additional products - replace with actual API call
        const mockProducts = [
            {
                id: 7 + (page - 2) * 6,
                name: "Lavender Bundle",
                description: "Fragrant lavender stems perfect for relaxation",
                price: "32.99",
                image: "/Images/products_img/lavender_purple.jpg",
                badge: "Relaxing"
            },
            {
                id: 8 + (page - 2) * 6,
                name: "Carnation Mix",
                description: "Colorful carnations with long-lasting blooms",
                price: "28.99",
                image: "/Images/products_img/carnation_mixed.jpg"
            },
            {
                id: 9 + (page - 2) * 6,
                name: "Daisy Bouquet",
                description: "Cheerful daisies that brighten any room",
                price: "35.99",
                image: "/Images/products_img/daisy_white.jpg"
            }
        ];

        return page <= 3 ? mockProducts : []; // Only return products for first 3 pages
    }

    function createProductCard(product) {
        const card = document.createElement('div');
        card.className = 'product-card';
        card.innerHTML = `
        <div class="product-image">
            <img src="${product.image}" alt="${product.name}">
            <div class="product-actions">
                <button class="wishlist-btn" title="Add to Wishlist">♡</button>
                <button class="quick-view-btn" title="Quick View">👁️</button>
            </div>
            ${product.badge ? `<div class="product-badge">${product.badge}</div>` : ''}
        </div>
        <div class="product-info">
            <h3 class="product-title">${product.name}</h3>
            <p class="product-description">${product.description}</p>
            <div class="product-price">
                <span class="current-price">$${product.price}</span>
            </div>
            <div class="product-rating">
                ⭐⭐⭐⭐ (${Math.floor(Math.random() * 100) + 50})
            </div>
            <button class="add-to-cart-btn" data-product-id="${product.id}">Add to Cart</button>
        </div>
    `;

        // Re-attach event listeners to new buttons
        setTimeout(() => {
            initializeProductInteractionsForCard(card);
        }, 0);

        return card;
    }

    function initializeProductInteractionsForCard(card) {
        const wishlistBtn = card.querySelector('.wishlist-btn');
        const quickViewBtn = card.querySelector('.quick-view-btn');
        const addToCartBtn = card.querySelector('.add-to-cart-btn');

        if (wishlistBtn) {
            wishlistBtn.addEventListener('click', function (e) {
                e.preventDefault();
                e.stopPropagation();
                toggleWishlist(this.closest('.product-card').querySelector('.add-to-cart-btn').dataset.productId, this);
            });
        }

        if (quickViewBtn) {
            quickViewBtn.addEventListener('click', function (e) {
                e.preventDefault();
                e.stopPropagation();
                openQuickView(this.closest('.product-card').querySelector('.add-to-cart-btn').dataset.productId);
            });
        }

        if (addToCartBtn) {
            addToCartBtn.addEventListener('click', function (e) {
                e.preventDefault();
                e.stopPropagation();
                addToCart(this.dataset.productId);
            });
        }

        card.addEventListener('click', function (e) {
            if (!e.target.closest('button') && !e.target.closest('.product-actions')) {
                const productId = this.querySelector('.add-to-cart-btn').dataset.productId;
                window.location.href = `/Product/Details/${productId}`;
            }
        });
    }

    function updateResultsCount() {
        const resultsCount = document.querySelector('.results-count');
        const productCards = document.querySelectorAll('.product-card');
        if (resultsCount && productCards) {
            resultsCount.textContent = `Showing ${productCards.length} products`;
        }
    }

    // Detail page color switching
    const colorOptionsDetail = document.querySelectorAll('.color-option-detail');
    colorOptionsDetail.forEach(option => {
        option.addEventListener('click', function () {
            // Remove active class from all options
            colorOptionsDetail.forEach(opt => opt.classList.remove('active'));

            // Add active class to clicked option
            this.classList.add('active');

            // Update product images
            const images = JSON.parse(this.dataset.images);
            updateProductImages(images);

            // Update price
            const price = this.dataset.price;
            updateProductPrice(price);

            // Update add to cart button
            updateAddToCartButton(this.dataset.color, price);
        });
    });

    // Bouquet option price update
    const bouquetCheckbox = document.querySelector('.bouquet-checkbox');
    if (bouquetCheckbox) {
        bouquetCheckbox.addEventListener('change', function () {
            updateTotalPrice();
        });
    }

    // Quantity selector
    const minusBtn = document.querySelector('.quantity-btn.minus');
    const plusBtn = document.querySelector('.quantity-btn.plus');
    const quantityInput = document.querySelector('.quantity-input');

    if (minusBtn && plusBtn && quantityInput) {
        minusBtn.addEventListener('click', function () {
            let value = parseInt(quantityInput.value);
            if (value > 1) {
                quantityInput.value = value - 1;
            }
        });

        plusBtn.addEventListener('click', function () {
            let value = parseInt(quantityInput.value);
            if (value < 10) {
                quantityInput.value = value + 1;
            }
        });
    }
}

function initializeTabSystem() {
    const tabBtns = document.querySelectorAll('.tab-btn');
    const tabContents = document.querySelectorAll('.tab-content');

    tabBtns.forEach(btn => {
        btn.addEventListener('click', function () {
            const tabId = this.dataset.tab;

            // Remove active class from all buttons and contents
            tabBtns.forEach(tab => tab.classList.remove('active'));
            tabContents.forEach(content => content.classList.remove('active'));

            // Add active class to clicked button and corresponding content
            this.classList.add('active');
            const targetContent = document.getElementById(tabId);
            if (targetContent) {
                targetContent.classList.add('active');
            }
        });
    });
}

function updateProductImages(images) {
    const mainImage = document.getElementById('mainProductImage');
    const thumbnails = document.querySelectorAll('.image-thumbnails .thumbnail');

    if (mainImage && images.length > 0) {
        // Fade out
        mainImage.style.opacity = '0';

        setTimeout(() => {
            mainImage.src = images[0];
            mainImage.alt = mainImage.alt.split(' - ')[0] + ' - ' + images[0].split('/').pop().split('.')[0];
            // Fade in
            mainImage.style.opacity = '1';
        }, 200);
    }

    // Update thumbnails
    thumbnails.forEach((thumb, index) => {
        if (images[index]) {
            thumb.querySelector('img').src = images[index];
        }
    });
}

function updateProductPrice(price) {
    const priceElement = document.querySelector('.product-price-detail .current-price');
    if (priceElement) {
        priceElement.textContent = `$${price}`;
    }
    updateTotalPrice();
}

function updateAddToCartButton(color, price) {
    const addToCartBtn = document.querySelector('.add-to-cart-btn.large');
    if (addToCartBtn) {
        addToCartBtn.dataset.color = color;
        addToCartBtn.dataset.basePrice = price;
        updateTotalPrice();
    }
}

function updateTotalPrice() {
    const addToCartBtn = document.querySelector('.add-to-cart-btn.large');
    const bouquetCheckbox = document.querySelector('.bouquet-checkbox');
    const quantityInput = document.querySelector('.quantity-input');

    if (addToCartBtn && bouquetCheckbox) {
        let totalPrice = parseFloat(addToCartBtn.dataset.basePrice);
        let quantity = quantityInput ? parseInt(quantityInput.value) : 1;

        if (bouquetCheckbox.checked) {
            totalPrice += 15.00; // Bouquet premium
        }

        totalPrice = totalPrice * quantity;

        addToCartBtn.textContent = `Add to Cart - $${totalPrice.toFixed(2)}`;
        addToCartBtn.dataset.finalPrice = totalPrice.toFixed(2);
    }
}

function initializeProductInteractions() {
    // Wishlist buttons
    const wishlistBtns = document.querySelectorAll('.wishlist-btn');
    wishlistBtns.forEach(btn => {
        btn.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();

            const productId = this.closest('.product-card').querySelector('.add-to-cart-btn').dataset.productId;
            toggleWishlist(productId, this);
        });
    });

    // Quick view buttons
    const quickViewBtns = document.querySelectorAll('.quick-view-btn');
    quickViewBtns.forEach(btn => {
        btn.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();

            const productId = this.closest('.product-card').querySelector('.add-to-cart-btn').dataset.productId;
            openQuickView(productId);
        });
    });

    // Add to Cart buttons
    const addToCartBtns = document.querySelectorAll('.add-to-cart-btn');
    addToCartBtns.forEach(btn => {
        btn.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();

            const productId = this.dataset.productId;
            addToCart(productId);
        });
    });

    // Color option clicks
    const colorOptions = document.querySelectorAll('.color-option-preview');
    colorOptions.forEach(option => {
        option.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();

            const card = this.closest('.product-card');

            // Update active state
            card.querySelectorAll('.color-option-preview').forEach(opt => {
                opt.classList.remove('active');
            });
            this.classList.add('active');

            // Update price
            const price = this.dataset.price;
            const priceElement = card.querySelector('.current-price');
            if (priceElement) {
                priceElement.textContent = `$${price}`;
            }

            // Update main image
            const mainImage = card.querySelector('.main-product-image');
            if (mainImage && this.dataset.image) {
                mainImage.src = this.dataset.image;
            }
        });
    });

    // Product card clicks (go to product page) - SIMPLE VERSION
    const productCards = document.querySelectorAll('.product-card');
    productCards.forEach(card => {
        card.addEventListener('click', function (e) {
            // Don't navigate if clicking buttons or color options
            if (e.target.closest('button') ||
                e.target.closest('.product-actions') ||
                e.target.closest('.color-option-preview') ||
                e.target.closest('.color-variant')) {
                return;
            }

            const productId = this.querySelector('.add-to-cart-btn').dataset.productId;
            window.location.href = `/Product/Details/${productId}`;
        });
    });
}

// Add this simple navigation function
function goToProductDetails(productId) {
    window.location.href = `/Product/Details/${productId}`;
}

function initializeAddToCart() {
    const addToCartBtns = document.querySelectorAll('.add-to-cart-btn');
    addToCartBtns.forEach(btn => {
        btn.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();

            const productId = this.dataset.productId;
            addToCart(productId);
        });
    });
}

function filterProducts() {
    console.log('Filtering products...');
    // Filter logic will be implemented later
}

function sortProducts(sortBy) {
    console.log('Sorting products by:', sortBy);
    // Sort logic will be implemented later
}

function toggleWishlist(productId, button) {
    console.log('Toggling wishlist for product:', productId);
    button.textContent = button.textContent === '♡' ? '♥' : '♡';
    // Wishlist logic will be implemented later
}

function openQuickView(productId) {
    console.log('Opening quick view for product:', productId);
    // Quick view logic will be implemented later
}

function addToCart(productId) {
    console.log('Adding to cart:', productId);

    // Update cart count in header
    const cartCount = document.querySelector('.cart-count');
    if (cartCount) {
        const currentCount = parseInt(cartCount.textContent) || 0;
        cartCount.textContent = currentCount + 1;
    }

    // Show success message
    alert('Product added to cart!');

    // Cart logic will be implemented later
}