$(document).ready(function () {

    var productionType = $("section.production").attr("data-type");

    var screenCssPixelRatio = (window.outerWidth) / window.innerWidth;
    var isZoomed = (screenCssPixelRatio < .98 || screenCssPixelRatio > 1.02);


    if (productionType === "") {
        var currentSlideIndex = 0;
        var slidesCount = $(".slider-item:last").index();
        $(".dot").eq(currentSlideIndex).addClass("current");

        var displaySlide = function (nextIndex, showDirection = "right", hideTime = 800, hideDelay = 200, showTime = 1000) {
            if ($(window).outerWidth() > 500 && !document.hidden) {

                var animationType = isZoomed ? "fade" : "slide";
                if (nextIndex > slidesCount) { nextIndex = 0; }
                else if (nextIndex < 0) { nextIndex = slidesCount; }

                $(".slider-item").eq(currentSlideIndex).delay(hideDelay).hide(animationType, { direction: showDirection === "left" ? "right" : "left" }, hideTime);
                $(".slider-item").eq(nextIndex).css("display", "flex").hide().show(animationType, { direction: showDirection }, showTime);

                $(".dots").fadeOut(showTime / 4, function () {
                    $(".dot").removeClass("current");
                    $(".dot").eq(nextIndex).addClass("current");
                }).delay(showTime / 2).fadeIn(showTime / 4);

                $(".slider-arrow a").addClass("not-active").fadeOut(showTime / 2).fadeIn(showTime / 2, function () {
                    $(this).removeClass("not-active");
                });

                currentSlideIndex = nextIndex;
            }
        }

        var sliderInterval = null;
        function setIntervalFunc() {
            if (sliderInterval == null && $(window).outerWidth() > 500)
                sliderInterval = setInterval(function () { displaySlide(currentSlideIndex + 1) }, 5000);
        }
        function clearIntervalFunc() {
            if (sliderInterval != null) {
                clearInterval(sliderInterval);
                sliderInterval = null;
            }
        }

        setIntervalFunc();
        $("section[role=slider]").hover(function () { clearIntervalFunc() }, function () { setIntervalFunc() });

        $(window).resize(function () {
            if ($(window).outerWidth() <= 500) {
                clearIntervalFunc();
                $(".slider-item").stop(true, true);
                $(".dots").stop(true, true);
                $(".slider-arrow").stop(true, true);
                $('section[role=slider] div').removeAttr('style');
                $(".dot").removeClass("current");
                $(".dot").eq(0).addClass("current");
                currentSlideIndex = 0;
            }
            else setIntervalFunc();
        });

        $(".dot").click(function () {
            var nextSlideIndex = $(this).index();
            if (currentSlideIndex === nextSlideIndex) return;
            var direction = nextSlideIndex > currentSlideIndex ? "right" : "left";
            displaySlide(nextSlideIndex, direction);
        });

        $(".slider-arrow.next a").click(function () {
            displaySlide(currentSlideIndex + 1, "right");
        });

        $(".slider-arrow.prev a").click(function () {
            displaySlide(currentSlideIndex - 1, "left");
        });
    }

    else {

        var productionTypeInfo = {
            "HfProcessing": 1,
            "HfCommunication": 2,
            "HfProtection": 3
        }
        const slideIndex = productionTypeInfo[productionType];

        $(".slider-item").eq(slideIndex).css("display", "flex");
        $(".slider-item").eq(slideIndex).click(function (event) { event.preventDefault(); });
        $(".slider-item").eq(slideIndex).find("a span").css("cursor", "default");
        $(".slider-arrow a").css("display", "none");
        $(".dots").css("display", "none");
    }
});