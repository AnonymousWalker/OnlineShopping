$(document).ready(function () {
    var products = $("[name='productlink']");
    for (var i = 0; i < products.length; i++) {

        if (products[i].text.length >= 24) {
            products[i].text = products[i].text.substring(0, 22) +"...";
        }
    }

});