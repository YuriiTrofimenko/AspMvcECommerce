// Single application framework
(function ($, window) {
    //Ìàññèâ äëÿ ôóíêöèé-êîíòðîëëåðîâ ïðåäñòàâëåíèé ñòðàíèö
    var pageHandlers = {};
    //Äåðåâî òåêóùåé ñòðàíèöû
    var currentPage;

    // show the "page" with optional parameter
    function show(pageName, param) {

        preloaderShow();
        //console.log($(".loader"));
        // invoke page handler
        var ph = pageHandlers[pageName];
        if (ph) {
            var $page = $("section#" + pageName);
            ph.call($page.length ? $page[0] : null, param); // call "page" handler
        }
        // activate the page  
        $("nav a.active, ul.sidenav a.active").removeClass("active");
        $("a[href='#" + pageName + "']").addClass("active");

        $(document.body).attr("page", pageName)
            .find("section").fadeOut(600).removeClass("active")
            .filter("section#" + pageName).addClass("active").fadeIn(600);
    }

    //Ôóíêöèÿ-îáúåêò, ïðèíèìàþùàÿ íàçâàíèå ñòðàíèöû
    function app(pageName, param) {

        var $page = $(document.body).find("section#" + pageName);

        var src = $page.attr("src");
        if (src && $page.find(">:first-child").length == 0) {
            $.get(src, "html") // it has src and is empty - load it
                .done(function (html) {
                    currentPage = pageName;
                    $page.html(html);
                    show(pageName, param);
                })
                .fail(function () { $page.html("failed to get:" + src); });
        } else
            show(pageName, param);
    }

    // register page handler  
    app.handler = function (handler) {
        var $page = $(document.body).find("section#" + currentPage);
        pageHandlers[currentPage] = handler.call($page[0]);
    }

    //Òî÷êà âõîäà -
    //ñðàáîòàåò âïåðâûå, êîãäà â ïåðâûé ðàç óñòàíîâèòñÿ URL
    function onhashchange() {

        
        var hash = location.hash || "#home";
        console.log(hash);
        var re = /#([-0-9A-Za-z]+)(\:(.+))?/;
        var match = re.exec(hash);
        hash = match[1];
        var param = match[3];
        app(hash, param); // navigate to the page
    }

    $(window).hashchange(onhashchange); // attach hashchange handler

    window.app = app; // setup the app as global object

    $(function () { $(window).hashchange() }); // initial state setup

})(jQuery, this);






