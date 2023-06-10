$(document).ready(function () {
    const cards = [
        {
            title: 'Xbox Controller',
            price: '$40.99',
            image: 'https://via.placeholder.com/150',
            rating: Math.floor(Math.random() * 6)
        },
        {
            title: 'PlayStation Controller',
            price: '$45.99',
            image: 'https://via.placeholder.com/150',
            rating: Math.floor(Math.random() * 6)
        },
        {
            title: 'Nintendo Switch Controller',
            price: '$50.99',
            image: 'https://via.placeholder.com/150',
            rating: Math.floor(Math.random() * 6)
        }
    ];

    const cardContainer = $('#card-container');
    const cartItemCount = $('#cart-item-count');
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
        cards.forEach(function (card, index) {
            const cardElement = generateCard(card, index);
            cardContainer.append(cardElement);
        });
    }

    $('#search-input').on('keyup', function () {
        const searchValue = $(this).val().toLowerCase();
        const filteredCards = cards.filter(function (card) {
            return card.title.toLowerCase().includes(searchValue);
        });
        renderCards(filteredCards);
    });

    cardContainer.on('click', '.add-to-cart-btn', function () {
        cartItems++;
        cartItemCount.text(cartItems);
    });

    renderCards(cards);
});
