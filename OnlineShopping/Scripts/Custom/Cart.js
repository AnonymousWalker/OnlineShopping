
var timeOutHidingAlert;
$(document).ready(function () {
    getCartCounter();

    $("[name='AddtoCart']").click(function (e) {
        e.preventDefault();
        addToCart(this);
    });  

    $(".close").click(function () {
        clearTimeout(timeOutHidingAlert);
        $(this).parent().hide();
    });

    $("#check-out-btn").click(function (e) {
        if ($("#countItems").text() == '0') {
            e.preventDefault();
        }
    });
});

//Pop-up alert 
// <<
function AlertDismissTimeOut() {
    timeOutHidingAlert = setTimeout(function () {
        $("#addToCartSuccess").hide();
    }, 2500);
}

function SetTimeOut() {     //show pop-up alert
    clearTimeout(timeOutHidingAlert);
    $("#addToCartSuccess").show();
    AlertDismissTimeOut();  //set timeout to hide
}

// >>

function getCartCounter() {
    var urlString = $("#CartCounterUrl").val();
    $.ajax({
        url: urlString,
        type: "get",
        success: function (response) {
            $("#cart-counter").text(response);
        }
    });
}
function addToCart(obj) {   //add to cookie
    var id = obj.value;
    if (id == null || typeof id === "undefined") return;
    var urlString = $("#AddToCartUrl").val();
    $.ajax({
        url: urlString,
        type: "get",
        data: {
            productId: id,
        },
        success: function (response) {    //should return with success or failure -> pop-up
            if (response == "True") {
                SetTimeOut();
            }
            getCartCounter();
        }
    });

}

function removeFromCart(obj) {
    //var d = new Date();
    //d.setTime(d.getTime() - 1000); //cookie expires last 1 sec
    //var expires = "expires=" + d.toUTCString();
    //document.cookie = "product" + obj.value + "=1" + ";" + expires + ";path=/";
    var urlAction = $("#RemoveFromCartUrl").val();
    var productId = obj.value;
    $.ajax({
        url: urlAction,
        type: "get",
        data: {
            productId: productId
        },
        success: function (response) {
            //$("#cartitems").html(response);
            window.location.reload();
        }
    });
}