$(document).ready(function () {
    $("#RegionId").change(function () {
        $("#CiudadId").empty();
        $("#CiudadId").append('<option value="0">[Seleccionar una Ciudad...]</option>');
        $.ajax({
            type: 'POST',
            url: UrlM,
            dataType: 'json',
            data: { regionId: $("#RegionId").val() },
            success: function (data) {
                $.each(data, function (i, data) {
                    $("#CiudadId").append('<option value="'
                        + data.CiudadId + '">'
                        + data.Nombre + '</option>');
                });
            },
            error: function (ex) {
                alert('Falló al obtener las Ciudades.' + ex);
            }
        });
        return false;
    })
});