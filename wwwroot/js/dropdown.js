function filterFunction() {
    var input, filter, ul, li, a, i;
    input = document.getElementById("playerSearch");
    filter = input.value.toUpperCase();
    dropdownContent = document.getElementById("dropdown-content");
    showDropdown(dropdownContent);

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

function showDropdown(dropdownContent) {
    var input = document.querySelector('.dropdown input');

    if (input.value.length > 0) {
        dropdownContent.style.display = 'block';
    } else {
        dropdownContent.style.display = 'none';
    }
}