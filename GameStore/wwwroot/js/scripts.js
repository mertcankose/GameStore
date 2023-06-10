$(document).ready(function () {
    const cards = [
        {
            title: 'Xbox Controller 1',
            price: '$40.99',
            image: 'https://via.placeholder.com/150',
            rating: Math.floor(Math.random() * 6)
        },
        {
            title: 'PlayStation Controller 2',
            price: '$45.99',
            image: 'https://via.placeholder.com/150',
            rating: Math.floor(Math.random() * 6)
        },
        {
            title: 'Nintendo Switch Controller 3',
            price: '$50.99',
            image: 'https://via.placeholder.com/150',
            rating: Math.floor(Math.random() * 6)
        },
        ,
        {
            title: 'PlayStation Controller 2',
            price: '$45.99',
            image: 'https://via.placeholder.com/150',
            rating: Math.floor(Math.random() * 6)
        },
        {
            title: 'Nintendo Switch Controller 3',
            price: '$50.99',
            image: 'https://via.placeholder.com/150',
            rating: Math.floor(Math.random() * 6)
        },
        {
            title: 'PlayStation Controller 2',
            price: '$45.99',
            image: 'https://via.placeholder.com/150',
            rating: Math.floor(Math.random() * 6)
        },
        {
            title: 'Nintendo Switch Controller 3',
            price: '$50.99',
            image: 'https://via.placeholder.com/150',
            rating: Math.floor(Math.random() * 6)
        },
        {
            title: 'PlayStation Controller 2',
            price: '$45.99',
            image: 'https://via.placeholder.com/150',
            rating: Math.floor(Math.random() * 6)
        },
        {
            title: 'Nintendo Switch Controller 3',
            price: '$50.99',
            image: 'https://via.placeholder.com/150',
            rating: Math.floor(Math.random() * 6)
        }
    ];

    const cardContainer = $('#card-container');
    const cartItemCount = $('#cart-item-count');
    const searchInput = $('#search-input');
    const searchSuggestions = $('.search-suggestions');
    const pagination = $('#pagination');
    const itemsPerPage = 7;
    let currentPage = 1;
    let cartItems = 0;

    function generateCard(card, index) {
        const cardElement = $(`
                        <div class="col-md-4">
                            <div class="card mb-4">
                                <img class="card-img-top" src="${card.image}" alt="Card image cap">
                                <div class="card-body">
                                    <h5 class="card-title">${card.title}</h5>
                                    <p class="card-text">${card.price}</p>
                                    <div class="rating" id="rating-${index}">
                                        ${Array(card.rating).fill('<span class="star" data-selected="true">&#9733;</span>').join('')}
                                        ${Array(5 - card.rating).fill('<span class="star" data-selected="false">&#9733;</span>').join('')}
                                    </div>
                                    <button type="button" class="btn btn-primary add-to-cart-btn">Add to Cart</button>
                                </div>
                            </div>
                        </div>`);

        return cardElement;
    }

    function renderCards(cards) {
        cardContainer.empty();
        const startIndex = (currentPage - 1) * itemsPerPage;
        const endIndex = startIndex + itemsPerPage;
        const paginatedCards = cards.slice(startIndex, endIndex);
        paginatedCards.forEach(function (card, index) {
            const cardElement = generateCard(card, index);
            cardContainer.append(cardElement);
        });
    }

    function showSearchSuggestions(suggestions) {
        searchSuggestions.empty();
        suggestions.forEach(function (suggestion) {
            const suggestionItem = $(`<div class="search-suggestion-item">${suggestion}</div>`);
            suggestionItem.on('click', function () {
                searchInput.val(suggestion);
                searchSuggestions.empty();
                performSearch(suggestion);
            });
            searchSuggestions.append(suggestionItem);
        });
        searchSuggestions.show();
    }

    function performSearch(query) {
        const filteredCards = cards.filter(function (card) {
            return card.title.toLowerCase().includes(query.toLowerCase());
        });
        renderCards(filteredCards);
        updatePagination(filteredCards.length);
    }

    function updatePagination(totalItems) {
        const totalPages = Math.ceil(totalItems / itemsPerPage);
        pagination.empty();
        for (let i = 1; i <= totalPages; i++) {
            const pageItem = $(`<li class="page-item"><a class="page-link" href="#">${i}</a></li>`);
            if (i === currentPage) {
                pageItem.addClass('active');
            }
            pageItem.on('click', function (e) {
                e.preventDefault();
                currentPage = i;
                renderCards(cards);
                updatePagination(totalItems);
            });
            pagination.append(pageItem);
        }
    }

    searchInput.on('input', function () {
        const searchValue = $(this).val().trim();
        if (searchValue === '') {
            searchSuggestions.empty();
            searchSuggestions.hide();
        } else {
            const suggestions = cards
                .map(function (card) {
                    return card.title;
                })
                .filter(function (title) {
                    return title.toLowerCase().includes(searchValue.toLowerCase());
                })
                .slice(0, 5);
            showSearchSuggestions(suggestions);
        }
    });

    searchInput.on('keypress', function (e) {
        if (e.which === 13) {
            const searchValue = $(this).val().trim();
            searchSuggestions.empty();
            searchSuggestions.hide();
            performSearch(searchValue);
        }
    });

    cardContainer.on('click', '.add-to-cart-btn', function () {
        cartItems++;
        cartItemCount.text(cartItems);
    });

    renderCards(cards);
    updatePagination(cards.length);
});