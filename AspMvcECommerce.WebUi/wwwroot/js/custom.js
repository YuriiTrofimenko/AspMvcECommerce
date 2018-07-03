var preloaderHide = function () {
    $('.preloader-wrapper').css('display', 'none');
}

var preloaderShow = function () {
    $('.preloader-wrapper').css('display', 'block');
}

//
var onSignIn = function (login) {

    $("a[href='#home:out']").text('Signout (' + login + ')');
    $("a[href='#home:out']").css('display', 'block');

    $("a[href='#signin']").css('display', 'none');
    $("a[href='#signup']").css('display', 'none');
}

var onSignOut = function () {

    $("a[href='#home:out']").text('');
    $("a[href='#home:out']").css('display', 'none');

    $("a[href='#signin']").css('display', 'block');
    $("a[href='#signup']").css('display', 'block');
}

$(document).ready(function () {
    $('.sidenav').sidenav();
});