$(document).ready(function () {
    $("#searchtextbox").keyup(function (e) {
        if (e.keyCode == 13) {
            var text = $(this).val();
            var url = $(this).data("url");
            window.location.href = url+"/?query="+text;
        }

    });


});