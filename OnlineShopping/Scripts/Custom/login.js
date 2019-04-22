$(document).ready(function () {
    $("#Username").focus();
    $("#sign-up-btn").click(function (e) {
        var elem = $("#confirm-password");
        if (elem.val() !== $("#Password").val()) {
            e.preventDefault();
            elem.focus();
            elem.next().text("Password confirmation does not match");
        } else {
            elem.next().text("");
        }
    });
});
