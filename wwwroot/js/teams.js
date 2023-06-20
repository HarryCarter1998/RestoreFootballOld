

function cancelSignUp(playerId) {
    if(!confirm("Are you sure you want to remove this player from the teams?")) return;

    $.ajax({
        type: 'POST',
        url: '../Players/CancelSignUp',
        cache: false,
        data: { id: playerId },
        success: function () { recalculateTeams() }
    });

    $.ajax({
        url: '../Players/GetPlayerFromGameweekPlayerId',
        data: { id: playerId },
        success: function (player) { addPlayerToDropdown(player) }
    });
}