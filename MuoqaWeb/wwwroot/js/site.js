document.addEventListener('DOMContentLoaded', function () {
    ControlVisibilityContainer()
    AnimateButton();
    ControlResponsive();

});
let widthMin = 768;
function AnimateButton() {
    let btn = $('#responsiveBtn');

    $(btn).click(function () {
        let nav = $('#responsiveCollapse');
        let uls = $('.navbar-nav');
        $(nav).css({
            'display':'flex',
        });

        // Calcula la altura total que el elemento debería tener
        let targetHeight = $(nav).prop('scrollHeight');

        // Anima la altura hasta el valor calculado
        $(nav).animate({ height: targetHeight }, 300, function () {
            // Al final de la animación, establecemos la altura en 'auto'
            $(this).css('height', 'auto');
        });
    });
}
function ControlResponsive() {
    $(window).resize(function () {
        let width = window.innerWidth;
        let uls = $('.navbar-nav');
        let nav = $('#responsiveCollapse');
        let btn = $('#responsiveBtn');
        let btnContainer = $('.responsive__btn__containter');
        if (width > widthMin)
        {
            $(nav).css({
                'width': '100%',
                'display': 'flex',
                'background-color': 'transparent',
                'align-items': 'flex-end',
                'flex-direction': 'initial',
                'height': '50%',
                'justify-content': 'space-between',
                'font-size': '1.1em'
            });
            $(btnContainer).css({ // Asegúrate de que el botón contenedor también esté visible
                'display': 'none'
            });
            $(uls).css({
                'width': 'auto',
                'padding': '0',
                'flex-direction': 'initial',
            });
        }
        else
        {
            $(nav).css({
                'width': '10em',
                'display': 'none', // Asegúrate de que se oculte correctamente
                'background-color': '#0009',
                'flex-direction': 'column',
                'height': '0',
                'font-size': 'inherit',
                'overflow': 'hidden'
            });
            $(uls).css({
                'width': '100%',
                'padding': '7px',
                'display': 'flex',
                'flex-direction': 'column'
            });
            $(btnContainer).css({
                'display': 'flex',
                'width': '100%',
                'height': '50%',
                'align-items': 'flex-end'
            });
            $(btn).css({
                'display': 'block'
            });
        }
    }).trigger('resize'); // Esto ejecutará la función al cargar la página
}
function ControlVisibilityContainer() {
    $(document).click(function (event) {
        let width = window.innerWidth;
        let nav = $('#responsiveCollapse');
        let btn = event.target.closest('button')
        if (width <= widthMin) {
            // Si se hace clic fuera del botón o del menú, oculta el menú
            if (!btn || btn.id !== 'responsiveBtn') {
                if ($(nav).css('display') === 'flex') {
                    $(nav).css({
                        'display': 'none'
                    });
                }
            }
        }
    })
}
