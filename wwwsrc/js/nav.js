$(function () {
    $('.nav-toggle').first().on('click', function () {
        $('nav .collapse-mobile').each(function () {
            $(this).slideToggle();
        });
    });
});