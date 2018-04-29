$(document).ready(function () {

    var productionType = $("section.production").attr("data-type");

    if (productionType == "") {
        var currentSlideIndex = 0;
        var slidesCount = $(".slider-item:last").index();
        $(".slider-item").eq(currentSlideIndex).css("display", "flex");
        $(".dot").eq(currentSlideIndex).addClass("current");

        var displaySlide = function (nextIndex, direction = "right", hideTime = 800, hideDelay = 200, showTime = 1000) {

            reverceDirection = direction == "left" ? "right" : "left";
            $(".slider-item").eq(currentSlideIndex).delay(hideDelay).hide('slide', { direction: reverceDirection }, hideTime);
            $(".dot").eq(currentSlideIndex).removeClass("current");

            if (nextIndex > slidesCount) {
                nextIndex = 0;
            }
            else if (nextIndex < 0) {
                nextIndex = slidesCount;
            }
            var newSlideIndex = $(".slider-item").eq(nextIndex);
            currentSlideIndex = nextIndex;

            $(".dot").eq(nextIndex).delay(showTime / 2).queue(function () {
                $(".dot").eq(nextIndex).addClass("current").dequeue();
            });
            $("section[role=slider] .dots").fadeOut(showTime / 4).delay(showTime / 2).fadeIn(showTime / 4);
            $("section[role=slider] .prev a, section[role=slider] .next a").addClass("not-active").fadeOut(showTime / 2).fadeIn(showTime / 2, function () {
                $(this).removeClass("not-active");
            });

            newSlideIndex.css("display", "flex").hide().show('slide', { direction: direction }, showTime);

        }

        var sliderInterval = null;
        function setIntervalFunc() {
            if (sliderInterval == null)
                sliderInterval = setInterval(function () { displaySlide(currentSlideIndex + 1) }, 5000);
        }
        function clearIntervalFunc() {
            if (sliderInterval != null) {
                clearInterval(sliderInterval);
                sliderInterval = null;
            }
        }

        if ($(window).width() > 500) {
            setIntervalFunc();
            $("section[role=slider]").hover(function () { clearIntervalFunc() }, function () { setIntervalFunc() });
        }

        $(window).resize(function () {
            if ($(window).width() <= 500) {
                clearIntervalFunc();
                $(".slider-item").eq(currentSlideIndex).css("display", "none");
                $(".slider-item").eq(0).css("display", "flex");
                $(".dot").eq(currentSlideIndex).removeClass("current");
                $(".dot").eq(0).addClass("current");
                currentSlideIndex = 0;
            }
            else setIntervalFunc();
        });
        $("section[role=slider] .dot").click(function () {
            var nextSlideIndex = $(this).index();
            if (currentSlideIndex == nextSlideIndex) return;
            var direction = nextSlideIndex > currentSlideIndex ? "right" : "left"
            displaySlide(nextSlideIndex, direction);
        });

        $("section[role=slider] .next a").click(function () {
            displaySlide(currentSlideIndex + 1, "right");
        });

        $("section[role=slider] .prev a").click(function () {
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
        $(".slider-item a span").eq(slideIndex).css("cursor", "default");
        $("section[role=slider] .prev a, section[role=slider] .next a").css("display", "none");
        $("section[role=slider] .dots").css("display", "none");
    }
});