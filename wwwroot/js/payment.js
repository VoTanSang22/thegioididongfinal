function getCookie(name) {
    let match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
    if (match) return decodeURIComponent(match[2]);
}


let cart = JSON.parse(getCookie("cart") || "[]");
let cartFormat = [];
let total = 0;
for (const item of cart) {
    const quantity = 1;
    const price = parseFloat(item.Price);
    const desc = item.Des ? (item.Des.length > 30 ? item.Des.substr(0, 30) + "..." : item.Des) : undefined;
    total += price * quantity;

    cartFormat.push({
        name: item.Pro_Name,
        quantity: quantity,
        description: desc,
        unit_amount: {
            currency_code: "USD",
            value: price,
        }
    })
}

let paypalBtn = paypal.Buttons({
    style: {
        layout: "vertical",
        color: "blue",
        shape: "rect",
    },

    onInit: function (data, actions) {
        console.log("Paypal READY");
    },

    createOrder: function (data, actions) {
        // Set up the transaction
        return actions.order.create({
            purchase_units: [
                {
                    amount: {
                        value: total,

                        breakdown: {
                            item_total: {
                                currency_code: "USD",
                                value: total,
                            },
                        },
                    },

                    items: cartFormat,
                },
            ],
        });
    },

    onApprove: function (data, actions) {
        return actions.order.capture().then(function (details) {
            let submitBtn = document.getElementById("submit-btn");
            submitBtn.click();
        });
    },

    onCancel: function (data) {
        console.log("on cancel:", data);
    },

    onError: function (err) {
        console.log("on error:", data);
    },
});
paypalBtn.render("#paypal-btn-container");