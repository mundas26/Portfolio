$('.nav__trigger').on('click', function (e) {
    e.preventDefault();
    $(this).parent().toggleClass('nav--active');
});
$(document).ready(function () {
	$('#nav-icon').click(function () {
		$(this).toggleClass('open');
	});
});