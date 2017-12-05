// Write your JavaScript code.
//Main navigation scroll spy for shadow
$(window).scroll(function () {
    var y = $(window).scrollTop();
    if (y > 0) {
        $("#header").addClass('--not-top');
    } else {
        $("#header").removeClass('--not-top');
    }
});