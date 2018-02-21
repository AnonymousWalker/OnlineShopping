
$("#browserbtn").change(function (e) {
    readURL(this);
});

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#image')
                .attr('src', e.target.result);
        };
        $(".image-preview-filename").val(input.files[0].name);
        reader.readAsDataURL(input.files[0]);
    }
}
    //$("#clearbtn").click(function (e) {
    //    e.preventDefault();
    //    $("#img").attr('src', "");
    //});
