$(document).ready(function () {
    $("#searchtextbox").keyup(function (e) {
        if (e.keyCode == 13) {
            var text = $(this).val();
            $.ajax({
                url: $(this).data("url"),
                type: "get",
                data: {
                    query: text
                }
            });
        }

    });
});