$(document).ready(function () {
    $("#tArea").on("keyup", function () {
        var counter = $("#counter")
        counter.text(140 - $("#tArea").val().length);
    })

});