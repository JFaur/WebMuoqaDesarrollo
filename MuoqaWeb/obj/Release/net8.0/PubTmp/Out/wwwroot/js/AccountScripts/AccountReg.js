document.addEventListener('DOMContentLoaded', function () {
    RegisterUser();
});
function RegisterUser() {

    let formR = $('#registrationForm');
    $(formR).submit( function (event) {
        event.preventDefault();
        let btn = $('#formButton');
        $(btn).prop('disabled', true);

        const formData = new FormData(this);
        const data = {};
        formData.forEach((value, key) => {
            data[key] = value;
        });

        $.ajax({
            url: `${urlWeb}${urlReg}`,
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
                    $('#errorText').text(resToJson['errorMessage']);
                }
                else {
                    urlReturn = resToJson['urlReturn'];
                    window.location.href = `${urlWeb}${urlReturn}`;
                }
            },
            error: function (error) {
                $('#errorText').text(`Error: ${error}`);
            },
            complete: function () {
                $(btn).prop('disabled', false);
            }
        });
    });
}