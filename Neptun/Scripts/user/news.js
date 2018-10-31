$(document).ready(function () {
    var d = new Date();
    var year = d.getFullYear();
    var month = d.getMonth() + 1;
    var day = d.getDate();

    $("#date").html("Сегодня " + day + "/" + month + "/" + year);
});