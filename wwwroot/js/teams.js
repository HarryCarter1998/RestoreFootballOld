
document.addEventListener("DOMContentLoaded", function () {
    $(document).ajaxStart(function () {
        $('#teams').hide();
        $('#loading').show();
    });
    $(document).ajaxStop(function () {
        $('#loading').hide();
        $('#teams').show();
    });
});

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