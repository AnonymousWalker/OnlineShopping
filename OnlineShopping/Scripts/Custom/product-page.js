$(document).ready(function () {
    $("#buy-btn").click(function () {
        var pId = $("#ProductId").val();
        var urlString = $("#AddToCartUrl").val();
        var cartLink = $("#cart-link").attr("href")
        $.ajax({
            url: urlString,
            type: "get",
            data: { productId: pId },
            success: function (response) {
                window.location.replace(cartLink);
            }
        });
    });
});