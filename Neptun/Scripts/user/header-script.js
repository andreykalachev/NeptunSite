$(document).ready(
    function () {
        var lastScrollTop = 0;
        $(window).scroll(function (event) {
            var st = $(this).scrollTop();
            if (st > lastScrollTop && st > 66 && $(window).width() > 740) {
                // downscroll code
                $("header").addClass("collapsed");
            } else {
                // upscroll code
                $("header").removeClass("collapsed");
            }
            lastScrollTop = st;
        });
        $("body").on("mousemove", function (event) {
            if (event.screenY < 150) {
                $("header").removeClass("collapsed");
            }
        });
        $(document).click(function (event) {
            if (!$(event.target).is("nav .dropdown div a, nav .dropdown label, input#dropdown-checkbox")) {
                $("#dropdown-checkbox").prop('checked', false);
            }
        });
    }
);