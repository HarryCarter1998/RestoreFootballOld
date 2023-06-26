function addToList() {
    var dropdown = document.getElementById("dropdown");
    var selectedOption = dropdown.options[dropdown.selectedIndex].value;
    var list = document.getElementById("list");
    var li = document.createElement("li");
    li.appendChild(document.createTextNode(selectedOption));
    list.appendChild(li);
}