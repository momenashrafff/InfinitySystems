var isContainerOpen = false;
function openNav() {
    document.getElementById("mySidebar").style.width = "400px";
}

function closeNav() {
    document.getElementById("mySidebar").style.width = "0";
}

function toggleContainer(Name, event) {
    if (event) {
        event.preventDefault();
    }
    var container = document.getElementById(Name);
    if (container.classList.contains('show')) {
        container.classList.remove('show');
        container.classList.add('hide');
        isContainerOpen = false;
    } else if (!isContainerOpen) {
        container.classList.remove('hide');
        container.classList.add('show');
        isContainerOpen = true;
    }
}
