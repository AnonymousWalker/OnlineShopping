var timeOutHiding;
$(document).ready(function () {
    $("[name='AddtoCart']").click(TimeOut);
    $(".close").click(function () {
        clearTimeout(timeOutHiding);
        $(this).parent().hide();
    });
});

function AlertDismissTimeOut() {
    timeOutHiding = setTimeout(function () {
        $("#addToCartSuccess").hide();
    }, 2500);
}

function TimeOut() {
    clearTimeout(timeOutHiding);
    $("#addToCartSuccess").show();
    AlertDismissTimeOut();
}