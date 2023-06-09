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
        data: { FirstName: firstName, LastName: lastName, SignedUp: true, redirect: false },
        success: function () { recalculateTeams() }
    });
}

function addExistingPlayer(player) {

    var playerId = player.getAttribute("value");

    $.ajax({
        type: 'POST',
        url: '../Players/UpdateSignedUp',
        cache: false,
        data: { id: playerId, signUp: true },
        success: function () { recalculateTeams() }
    });

    player.remove();
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
        0: 'green',
        1: 'non-bibs',
        2: 'yellow',
        3: 'orange'
    };

    players.forEach(player => {
        addTeamPlayerElement(player, teams)
    });

    if (players.length >= 20) {
        displayFourTeams();
    }
    else {
        displayTwoTeams();
    }
}

function displayFourTeams() {
    document.getElementById("yellow").style.display = "inline-block";
    document.getElementById("orange").style.display = "inline-block";
    document.getElementById("green").getElementsByTagName("h3")[0].innerHTML = "Greens";

}

function displayTwoTeams() {
    document.getElementById("yellow").style.display = "none";
    document.getElementById("orange").style.display = "none";
    document.getElementById("green").getElementsByTagName("h3")[0].innerHTML = "Bibs";

}

function addTeamPlayerElement(player, teams) {
    const team = teams[player.team];
    const para = document.createElement("p");
    const node = document.createTextNode(`${player.firstName} ${player.lastName}`)
    para.appendChild(node);
    para.classList.add("teamPlayer");
    para.onclick = function () {
        cancelSignUp(player.id);
    }
    document.getElementById(team).appendChild(para);
}

function cancelSignUp(playerId) {
    $.ajax({
        type: 'POST',
        url: '../Players/UpdateSignedUp',
        cache: false,
        data: { id: playerId, signUp: false },
        success: function () { recalculateTeams() }
    });
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

function recalculateTeams() {
    $.ajax({
        url: '../Home/RecalculateTeams',
        success: function (players) { displayTeams(players) }
    });
}