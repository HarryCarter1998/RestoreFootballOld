function addNewPlayer() {
    var firstName = document.getElementById("firstName").value;
    var lastName = document.getElementById("lastName").value;

    //var player = { FirstName: firstName, LastName: lastName };

    //let myform = document.createElement("form");
    //myform.action = "example.php";
    //myform.method = "post";

    //let input1 = document.createElement("input");
    //input1.type = "text";
    //input1.name = "FirstName";
    //input1.value = "Johnathan";

    //let input2 = document.createElement("input");
    //input2.type = "text";
    //input2.name = "LastName";
    //input2.value = "Luxembourg";

    //myform.appendChild(input1);
    //myform.appendChild(input2);

    //document.body.appendChild(myform);

    //let fd = new FormData(myform);
    //$.ajax({
    //    url: "Players/Create",
    //    data: fd,
    //    cache: false,
    //    processData: false,
    //    contentType: false,
    //    type: 'POST',
    //    success: function (response) {
    //        // do something with the result
    //    }
    //});


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