
document.addEventListener("DOMContentLoaded", function () {
    $(document).ajaxStart(function () {
        $('#teams').hide();
        $('#loading').show();
    });

    $(document).ajaxStop(function () {
        $('#loading').hide();
        $('#teams').show();
    });

    var currentDate = (new Date()).addDays(3);
    var nextWednesday = new Date();
    nextWednesday.setDate(currentDate.getDate() + (7 - currentDate.getDay() + 3) % 7 + 1);
    nextWednesday.setHours(21, 30, 0, 0);
    if (currentDate < nextWednesday) {
        var targetButton = document.getElementById('enter-results');
        targetButton.style.display = 'none';
        setTimeout(function () {
            targetButton.style.display = 'inline-block';
        }, nextWednesday - currentDate);
    }
});

Date.prototype.addDays = function (days) {
    var date = new Date(this.valueOf());
    date.setDate(date.getDate() + days);
    return date;
}

function cancelSignUp(playerId) {
    if (!confirm("Are you sure you want to remove this player from the teams?")) return;

    $.ajax({
        url: '../Players/GetPlayerFromGameweekPlayerId',
        data: { id: playerId },
        success: function (player) {
            addPlayerToDropdown(player);
            $.ajax({
                type: 'POST',
                url: '../Players/CancelSignUp',
                cache: false,
                data: { id: playerId },
                success: function () { recalculateTeams() }
            });
        }
    });
}


