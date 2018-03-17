function AddToCart(obj) {
    var d = new Date();
    d.setTime(d.getTime() + (24 * 60 * 60 * 1000)); //cookie lasts 24h
    var expires = "expires=" + d.toUTCString();
    document.cookie = "product" + obj.value + "=1"+ ";" + expires + ";path=/";
}

function getQuantity(id) {
    var name = "product"+id+ "=";
    var listPairs = document.cookie.split(';');
    for (var i = 0; i < listPairs.length; i++) {
        var pair = listPairs[i];
        while (pair.charAt(0) == ' ') {
            pair = pair.substring(1);
        }
        if (pair.indexOf(name) == 0) {
            return pair.substring(name.length, pair.length);
        }
    }
    return "";
}

function RemoveFromCart(obj) {
    var d = new Date();
    d.setTime(d.getTime() - 1000); //cookie expires last 1 sec
    var expires = "expires=" + d.toUTCString();
    document.cookie = "product" + obj.value + "=1" + ";" + expires + ";path=/";
    var cartUrl = $("#CartUrl").val();
    $.ajax({
        url: cartUrl,
        type: "get",
        success: function (html) {
            $("#cartitems").html(html);
        }
    });
}
//function checkCookie() {
//    var user = getCookie("username");
//    if (user != "") {
//        alert("Welcome again " + user);
//    } else {
//        user = prompt("Please enter your name:", "");
//        if (user != "" && user != null) {
//            setCookie("username", user, 365);
//        }
//    }
//}