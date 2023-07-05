function filterFunction(playerSearchId = "playerSearch", dropdownContentId = "dropdown-content") {
    var input, filter, ul, li, a, i;
    input = document.getElementById(playerSearchId);
    filter = input.value.toUpperCase();
    dropdownContent = document.getElementById(dropdownContentId);
    showDropdown(dropdownContent, filter);

    dropdownElements = dropdownContent.getElementsByClassName("dropdown-element");
    for (i = 0; i < dropdownElements.length; i++) {
        txtValue = dropdownElements[i].textContent || dropdownElements[i].innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            dropdownElements[i].style.display = "";
        } else {
            dropdownElements[i].style.display = "none";
        }
    }
}

function showDropdown(dropdownContent, input) {
    if (input.length > 0) {
        dropdownContent.style.display = 'block';
    } else {
        dropdownContent.style.display = 'none';
    }
}