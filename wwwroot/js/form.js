document.addEventListener("DOMContentLoaded", function () {
    getSignedUpPlayers();
});

function addNewPlayer() {
    createNewPlayer();
    toggleNewPlayerFormVisibility();
    toggleIntro();
}

function createNewPlayer() {
    var firstName = document.getElementById("firstName").value;
    var lastName = document.getElementById("lastName").value;

    $.ajax({
        type: 'POST',
        url: '../Players/Create',
        cache: false,
        data: { FirstName: firstName, LastName: lastName, SignedUp: true },
        success: function (players) { getSignedUpPlayers() }
    });
}

function addExistingPlayer(player) {
    var dropdownContent = document.getElementById("dropdown-content");
    var search = document.getElementById("playerSearch");
    var cantSeeName = document.getElementById("cant-see-name");

    var playerId = player.getAttribute("value");

    $.ajax({
        type: 'POST',
        url: '../Players/UpdateSignedUp',
        cache: false,
        data: { id: playerId, signUp: true },
        success: function (players) { getSignedUpPlayers() }
    });

    dropdownContent.style.display = 'none';
    search.classList.toggle("hide");
    cantSeeName.classList.toggle("hide");

    toggleIntro();
}

function getSignedUpPlayers() {
    $.ajax({
        url: '../Home/GetSignedUpPlayers',
        success: function (players) { displayTeams(players) }
    });
}

function displayTeams(players) {
    var yellowTeam = [];
    var greenTeam = [];
    var orangeTeam = [];
    var nonBibsTeam = [];

    players.forEach(player => {
        switch (player.team) {
            case 0:
                yellowTeam.push(player);
                break;
            case 1:
                greenTeam.push(player);
                break;
            case 2:
                orangeTeam.push(player);
                break;
            default:
                nonBibsTeam.push(player);
        }
    });

    document.querySelectorAll('.teamPlayer').forEach(e => e.remove());

    yellowTeam.forEach(player => {
        const para = document.createElement("p");
        const node = document.createTextNode(`${player.firstName} ${player.lastName}`)
        para.appendChild(node);
        para.classList.add("teamPlayer");
        document.getElementById("yellow").appendChild(para);

    })
    greenTeam.forEach(player => {
        const para = document.createElement("p");
        const node = document.createTextNode(`${player.firstName} ${player.lastName}`)
        para.appendChild(node);
        para.classList.add("teamPlayer");
        document.getElementById("green").appendChild(para);
    })
    orangeTeam.forEach(player => {
        const para = document.createElement("p");
        const node = document.createTextNode(`${player.firstName} ${player.lastName}`)
        para.appendChild(node);
        para.classList.add("teamPlayer");
        document.getElementById("orange").appendChild(para);
    })
    nonBibsTeam.forEach(player => {
        const para = document.createElement("p");
        const node = document.createTextNode(`${player.firstName} ${player.lastName}`)
        para.appendChild(node);
        para.classList.add("teamPlayer");
        document.getElementById("non-bibs").appendChild(para);
    })

}

function switchForm() {
    toggleExistingPlayerFormVisibility();
    toggleNewPlayerFormVisibility();
}

function resetForm() {
    toggleExistingPlayerFormVisibility();
    toggleIntro();
    document.getElementById("playerSearch").value = "";
}

function toggleIntro() {
    var enterName = document.getElementById("enter-name");
    var success = document.getElementById("success");
    var addAnother = document.getElementById("add-another");
    enterName.classList.toggle('hide');
    success.classList.toggle('hide');
    addAnother.classList.toggle('hide');
}

function toggleExistingPlayerFormVisibility() {
    var search = document.getElementById("playerSearch");
    var cantSeeName = document.getElementById("cant-see-name");
    search.classList.toggle("hide");
    cantSeeName.classList.toggle("hide");
}

function toggleNewPlayerFormVisibility() {
    var newPlayerInputs = document.querySelectorAll('.newPlayerInput');
    var addButton = document.getElementById("add-button");
    for (var i = 0; i < newPlayerInputs.length; i++) {
        newPlayerInputs[i].classList.toggle("showInlineBlock");
    }
    addButton.classList.toggle("showInlineBlock");
}