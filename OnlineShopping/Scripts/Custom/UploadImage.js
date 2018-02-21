$(document).ready(function () {
    $("#fakeuploadbtn").click(function (e) {
        e.preventDefault();
        //$("#browsebtn").click();
        
    })
})

$("#browsebtn").on('click',UploadFile(this));

function UploadFile() {
    if (this.files && this.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#image')
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(this.files[0]);
    }
}