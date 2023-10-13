    function updateExperience() {
        // Calculate the updated experience on the client side
        var currentDate = new Date();
        var totalExperience = currentDate - programmingStartDate;

        var years = Math.floor(totalExperience / (365 * 24 * 60 * 60 * 1000));
        totalExperience %= (365 * 24 * 60 * 60 * 1000);
        var months = Math.floor(totalExperience / (30 * 24 * 60 * 60 * 1000));
        totalExperience %= (30 * 24 * 60 * 60 * 1000);
        var days = Math.floor(totalExperience / (24 * 60 * 60 * 1000));
        totalExperience %= (24 * 60 * 60 * 1000);
        var hours = Math.floor(totalExperience / (60 * 60 * 1000));
        totalExperience %= (60 * 60 * 1000);
        var minutes = Math.floor(totalExperience / (60 * 1000));
        totalExperience %= (60 * 1000);
        var seconds = Math.floor(totalExperience / 1000);

        // Update the displayed time
        $('#years').text(years);
        $('#months').text(months);
        $('#days').text(days);
        $('#hours').text(hours);
        $('#minutes').text(minutes);
        $('#seconds').text(seconds);
    }

    // Update the experience every second
    setInterval(updateExperience, 1000);
