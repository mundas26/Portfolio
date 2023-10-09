$(document).ready(function () {
    function checkAnimate() {
        $(".fade-in, .slide-in-left, .slide-in-right").each(function () {
            var elementTop = $(this).offset().top;
            var viewportTop = $(window).scrollTop() + $(window).height();

            if (elementTop < viewportTop) {
                if ($(this).hasClass("fade-in")) {
                    $(this).addClass("show");
                }
                if ($(this).hasClass("slide-in-left")) {
                    $(this).css("transform", "translateX(0)");
                }
                if ($(this).hasClass("slide-in-right")) {
                    $(this).css("transform", "translateX(0)");
                }
            }
        });
    }

    // Initial check when the page loads
    checkAnimate();

    // Check again when the user scrolls
    $(window).on("scroll", checkAnimate);
});
