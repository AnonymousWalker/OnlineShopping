$(document).ready(function () {
    $("#searchtextbox").keyup(function (e) {
        if (e.keyCode == 13) {
            var text = $(this).val();
            var url = $(this).data("url");
            window.location.href = url+"/?query="+text;
            //$.ajax({
            //    url: $(this).data("url"),
            //    type: "get",
            //    data: {
            //        query: text
            //    },
            //    success: function (result) {
            //        window.location = result;
            //    }
            //});
        }

    });
});