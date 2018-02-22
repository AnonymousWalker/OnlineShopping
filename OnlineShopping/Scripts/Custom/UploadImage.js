﻿$(document).ready(function () {
    $("#fakeuploadbtn").click(function (e) {
        e.preventDefault();
        $("#browsebtn").click();
    });

    function UploadFile(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#image')
                    .attr('src', e.target.result);
            };

            reader.readAsDataURL(input.files[0]);
        }
    }

    $("#browsebtn").change(function () {
        UploadFile(this);
    })
});