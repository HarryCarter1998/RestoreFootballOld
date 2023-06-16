function displayScores() {
    $.ajax({
        url: '../Home/GetNumTeams',
        success: function (numTeams) {
            if (numTeams == 2) { displayTwoScores() } else { displayFourScores() }
        }
    });
}

function displayTwoScores() {
    document.getElementById("yellow-score").style.display = "";
    document.getElementById("orange-score").style.display = "";
    document.getElementById("bibs-label").innerHTML = "Bibs";
}

function displayFourScores() {
    document.getElementById("yellow-score").style.display = "flex";
    document.getElementById("orange-score").style.display = "flex";
    document.getElementById("bibs-label").innerHTML = "Green";
}