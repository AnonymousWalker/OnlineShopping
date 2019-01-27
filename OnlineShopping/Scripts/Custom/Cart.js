﻿//Pop-up alert 
// <<
var timeOutHidingAlert;
$(document).ready(function () {
    $("[name='AddtoCart']").click(function (e) {
        e.preventDefault();
        SetTimeOut();
        addToCart(this);
    });  

    $(".close").click(function () {
        clearTimeout(timeOutHidingAlert);
        $(this).parent().hide();
    });
});

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

function addToCart(obj) {   //add to cookie
    var d = new Date();
    var id = obj.value;
    if (id == null || typeof id === "undefined") return;
    d.setTime(d.getTime() + (24 * 60 * 60 * 1000)); //cookie lasts 1h
    var timeExpire = "expires=" + d.toUTCString();
    document.cookie = "product" + id + "=1" + ";" + timeExpire + ";path=/";

}

function addToCartSession(obj) {   //add to session
    //obj.value is the productId
    var urlAction = $("#AddToCartUrl").val();
    $.ajax({
        url: urlAction,
        type: "get",
        data: {
            productId: obj.value,
        },
        success: function (result) {    //should return with success or failure -> pop-up
            //
        }
    });
}

function removeFromCart(obj) {
    var d = new Date();
    d.setTime(d.getTime() - 1000); //cookie expires last 1 sec
    var expires = "expires=" + d.toUTCString();
    document.cookie = "product" + obj.value + "=1" + ";" + expires + ";path=/";
    var urlAction = $("#RemoveFromCart").val();
    $.ajax({
        url: urlAction,
        type: "get",
        success: function (html) {
            $("#cartitems").html(html);
        }
    });
}