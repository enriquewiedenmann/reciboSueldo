$(document).ready(function () {
    $(SendPass).click(function () {
        var user = $("#userName").val();
        alert(user);
        $.ajax({
        data: { userName : user },
        type: 'GET',
        url: '/Login/SendPass', //'@Url.Action("getDataResumen", "EMPLEADOs")',
        success: function (resultado) {
            if (resultado !== null) {
                alert("recibira pass por WP");
            } else {
                alert("No recibira pass por WP");
            }
        }
        });
        });
});
