// globals
page = -1;
next = true;
previous = false;

function init() {
    nextPage();
}

function nextPage() {
    if(next) {
        page++;
        previous = true;
        
        clearList();
        getList();
    }
}

function previousPage() {
    if(previous && page > 0) {
        page--;
        
        clearList();
        getList();
    }
}

function getList() {
    get("/character/page/" + page).then((res) => {
        res = JSON.parse(res);
             
        if(res.results.length == 0) {
            next = false;
        }
        
        res.results.forEach((value) => {
            insertIntoList(
                value.name,
                value.level,
                value.class,
                value.race
            );
        });
    });
}

function clearList() {
    document.getElementById("list").innerHTML = "";
}

function insertIntoList(name, level, characterClass, race) {
    let str =   "<div class='card'>" +
                    "<p>Name: " + name + "</p>" +
                    "<p>Level: " + level + "</p>" +
                    "<p>Class: " + characterClass + "</p>" +
                    "<p>Race: " + race + "</p>" +
                    "<button onclick=\"navigateTo('/CharacterManager/new.html?name=" + escape(name) + "')\">View Character</button>" +
                "</div>";
    
    document.getElementById("list").innerHTML = document.getElementById("list").innerHTML + str;
}

function go() {
    let name = document.getElementById("name").value;

    checkIfExists(name).then((res) => {
        if (res) {
            navigateTo("/CharacterManager/new.html?name=" + escape(name));
        } else {
            alert("Could not find that character");
        }
    });
}