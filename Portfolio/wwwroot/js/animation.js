$(document).ready(function () {
    function checkFadeIn() {
        $(".fade-in").each(function () {
            var elementTop = $(this).offset().top;
            var viewportTop = $(window).scrollTop() + $(window).height();

            if (elementTop < viewportTop) {
                $(this).addClass("show");
            }
        });
    }

    // Initial check when the page loads
    checkFadeIn();

    // Check again when the user scrolls
    $(window).on("scroll", checkFadeIn);
});
