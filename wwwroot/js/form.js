document.addEventListener("DOMContentLoaded", function () {
    getSignedUpPlayers();
});

function addNewPlayer() {
    createNewPlayer();
    document.getElementById("add-new").style.display = "none";
    document.getElementById("success-view").style.display = "block";
}

function createNewPlayer() {
    var firstName = document.getElementById("firstName").value;
    var lastName = document.getElementById("lastName").value;

    $.ajax({
        type: 'POST',
        url: '../Players/Create',
        cache: false,
        data: { FirstName: firstName, LastName: lastName, SignedUp: true },
        success: function () { getSignedUpPlayers() }
    });
}

function addExistingPlayer(player) {

    var playerId = player.getAttribute("value");

    $.ajax({
        type: 'POST',
        url: '../Players/UpdateSignedUp',
        cache: false,
        data: { id: playerId, signUp: true },
        success: function () { getSignedUpPlayers() }
    });

    document.getElementById("add-existing").style.display = "none";
    document.getElementById("success-view").style.display = "block";
}

function getSignedUpPlayers() {
    $.ajax({
        url: '../Home/GetSignedUpPlayers',
        success: function (players) { displayTeams(players) }
    });
}

function displayTeams(players) {

    document.querySelectorAll('.teamPlayer').forEach(e => e.remove());

    const teams = {
        0: 'yellow',
        1: 'green',
        2: 'orange',
        3: 'non-bibs'
    };

    players.forEach(player => {
        addTeamPlayerElement(player, teams)
    });
}

function addTeamPlayerElement(player, teams) {
    const team = teams[player.team];
    const para = document.createElement("p");
    const node = document.createTextNode(`${player.firstName} ${player.lastName}`)
    para.appendChild(node);
    para.classList.add("teamPlayer");
    document.getElementById(team).appendChild(para);
}

function switchForm() {
    document.getElementById("add-existing").style.display = "none";
    document.getElementById("add-new").style.display = "block";
}

function resetForm() {
    document.getElementById("add-existing").style.display = "";
    document.getElementById("success-view").style.display = "";
    document.getElementById("dropdown-content").style.display = "";

    document.getElementById("playerSearch").value = "";
}