function addNewPlayer(firstName, lastName) {
    toggleNewPlayerFormVisibility();
    toggleIntro();
}

function addExistingPlayer(playerId) {
    var dropdownContent = document.getElementById("dropdown-content");
    var search = document.getElementById("playerSearch");
    var cantSeeName = document.getElementById("cant-see-name");
    dropdownContent.style.display = 'none';
    search.classList.toggle("hide");
    cantSeeName.classList.toggle("hide");

    toggleIntro();
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