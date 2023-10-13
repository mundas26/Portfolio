// Attach a click event handler to the certification links
$('.certification-link').click(function (e) {
    e.preventDefault();
    // Store the URL of the PDF to be downloaded
    var pdfUrl = $(this).attr('href');

    // Attach a click event handler to the download button in the modal
    $('#downloadPdfButton').click(function () {
        // Trigger the download of the PDF
        window.location.href = pdfUrl;
        // Close the modal
        $('#downloadModal').modal('hide');
    });

    // Show the download modal when a link is clicked
    $('#downloadModal').modal('show');
});
