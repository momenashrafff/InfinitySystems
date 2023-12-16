function openNav() {
    document.getElementById("mySidebar").style.width = "400px";
}

function closeNav() {
    document.getElementById("mySidebar").style.width = "0";
}
function toggleContainer(button) {
    var container = document.getElementById('myContainer');
    var closeButtonAndImage = '<button id="closeButton" onclick="toggleContainer()" class="close-button">X</button><img src="/images/Events/Elogo.png" class="myImage">';
    if (container.classList.contains('show')) {
        container.classList.remove('show');
        container.classList.add('hide');
        container.innerHTML = closeButtonAndImage;
    } else {
        container.classList.remove('hide');
        container.classList.add('show');
        switch (button) {
            case 'Create':
                container.innerHTML = closeButtonAndImage + '<div class="event-container"><div class="field"><input type="text" placeholder="Event Name"/><div class="line"></div></div><div class="field"><input type="text" placeholder="Description"/><div class="line"></div></div><div class="field"><input type="text" placeholder="Location"/><div class="line"></div></div><div><input type="Date" name="Date" value="Date" /></div><div class="field"><input type="number" placeholder="Other User ID"/><div class="line"></div></div><input type="submit" class="submit" value="Submit" name="SubmitC" /></div > ';
                break;  
            case 'Assign':
                container.innerHTML = closeButtonAndImage + '<button>Button for assign</button><input type="text" placeholder="Input for assign">';
                break;
            case 'Uninvite':
                container.innerHTML = closeButtonAndImage + '<button>Button for Uninvite</button><input type="text" placeholder="Input for assign">';
                break;
            case 'SearchEvent':
                container.innerHTML = closeButtonAndImage + '<button>Button for SearchEvent</button><input type="text" placeholder="Input for assign">';
                break;
            case 'Remove':
                container.innerHTML = closeButtonAndImage + '<button>Button for Remove</button><input type="text" placeholder="Input for assign">';
                break;
        }
    }
}
