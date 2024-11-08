document.addEventListener('DOMContentLoaded', function () {
    LoginUser();
});
function LoginUser() {
    let formL = $('#loginForm');
    $(formL).submit(function (event) {
        event.preventDefault();
        let btn = $('#formButton');
        $(btn).prop('disabled', true);

        const formData = new FormData(this);
        const data = {};
        formData.forEach((value, key) => {
            data[key] = value;
        });

        $.ajax({
            url: `${urlWeb}${urlLog}`,
            method: 'POST',
            data: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': token
            },
            success: function (resToJson) {
                if (resToJson['errorMessage'] == null || resToJson['errorMessage'] == "") {
                    urlReturn = resToJson['urlReturn'];
                    window.location.href = `${urlWeb}${urlReturn}`;
                }
                else if (resToJson['urlReturn'] == null || resToJson['urlReturn'] == "") {
                    console.log(resToJson['errorMessage'] + "hola")
                    $('#errorText').text(resToJson['errorMessage']);
                }
                else {
                    urlReturn = resToJson['urlReturn'];
                    window.location.href = `${urlWeb}${urlReturn}`;
                }
            },
            error: function () {
                console.log("hay un error en la respuesta");
                $('#errorText').text("Hubo un error inesperado");
            },
            complete: function () {
                $(btn).prop('disabled', false);
            }
        });
    });
}