$(document).ready(function () {
    $(':input[type="submit"]').prop('disabled', true);

    $(Document).on('change', 'input[type=file]', function (e) {
        $(':input[type="submit"]').prop('disabled', true);
        var TmpPath = URL.createObjectURL(e.target.files[0]);
       
        $('#ReciboView').attr('src', TmpPath);
        $(':input[type="submit"]').prop('disabled', false);
    });
});