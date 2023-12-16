function openNav() {
    document.getElementById("mySidebar").style.width = "400px";
}

function closeNav() {
    document.getElementById("mySidebar").style.width = "0";
}
function toggleContainer() {
    var container = document.getElementById('myContainer');
    if (container.classList.contains('show')) {
        container.classList.remove('show');
        container.classList.add('hide');
    } else {
        container.classList.remove('hide');
        container.classList.add('show');
    }
}

