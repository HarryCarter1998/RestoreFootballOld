document.addEventListener("DOMContentLoaded", function () {
    const gameweekId = window.location.href.split('/').pop();
    getGameweekPlayers(gameweekId);
});


function addToList() {
    var dropdown = document.getElementById("dropdown");
    var selectedOption = dropdown.options[dropdown.selectedIndex].value;
    var list = document.getElementById("list");
    var li = document.createElement("li");
    li.appendChild(document.createTextNode(selectedOption));
    list.appendChild(li);
}

function getGameweekPlayers(gameweekId) {
    $.ajax({
        url: '../GetGameweekPlayers',
        data: {gameweekId: gameweekId},
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

function addTeamPlayerElement(gameweekPlayer, teams) {
    const team = teams[gameweekPlayer.team];
    const para = document.createElement("p");
    const node = document.createTextNode(`${gameweekPlayer.player.firstName} ${gameweekPlayer.player.lastName}`)
    para.appendChild(node);
    para.classList.add("teamPlayer");
    para.onclick = function () {
        cancelSignUp(gameweekPlayer.id);
    }
    document.getElementById(team).appendChild(para);
}

function addPlayerToDropdown(player) {
    const para = document.createElement("p");
    const node = document.createTextNode(`${player.firstName} ${player.lastName}`)
    para.appendChild(node);
    para.classList.add("dropdown-element");
    para.setAttribute("value", player.id);
    para.onclick = function () {
        console.log(this);
        addExistingPlayer(this);
    }
    document.getElementById("dropdown-content").appendChild(para);
}