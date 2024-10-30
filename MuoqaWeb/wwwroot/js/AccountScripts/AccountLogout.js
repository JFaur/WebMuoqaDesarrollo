document.addEventListener('DOMContentLoaded', function () {
    UnLogUser();
});

function UnLogUser() {
    let formUL = $('#formLogout');
    $(formUL).submit(function (event) {
        event.preventDefault();
        let btn = $('#unLogBtn');
        $(btn).prop('disabled', true);
        $.ajax({
            url: `${urlWeb}${urlLogout}`,
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': token
            },
            success: function (resToJson) {
                urlReturn = resToJson['urlReturn'];
                window.location.href = `${urlWeb}${urlReturn}`
            },
            Error: function (error) {
                console.log(`Error: ${error}`);
            },
            complete: function () {
                $(btn).prop('disabled', false);
            }
        });
    });
}