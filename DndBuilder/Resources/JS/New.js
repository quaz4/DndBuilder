function get(url, callback) {
    let request = new XMLHttpRequest();
    
    request.open("GET", url, true);

    request.onreadystatechange = () => {

        if (request.readyState == 4) {
            if (request.status == 200) {
                callback(JSON.parse(request.responseText));
            } else {
                callback({ error: request.status});
            }
        }
    };
    
    request.send();
}

function init() {

    characterRace = null;
    characterClass = null;

    setRaceOptions();
    setClassOptions();
}

function updateHitPoints() {
    // Check if race and class info is known
    if (characterRace && characterClass) {
        // Do update
    }
}

function updateSpellcaster() {
    // Check if class info is known
    if (characterClass) {
        console.log("Character Class found");
        console.log(characterClass);
        if (characterClass.spellcasting) {
            document.getElementById("spellcaster").innerHTML = "Spellcaster: Yes";
        } else {
            document.getElementById("spellcaster").innerHTML = "Spellcaster: No";
        }
    }
}

function getRaceInfo(url) {
    get(url, (res) => {
        if (res.error) {
            // TODO: Error handling
        } else {
            console.log(res);
            characterRace = res;
        }
    });
}

function getClassInfo(url) {
    get(url, (res) => {
        if (res.error) {
            // TODO: Error handling
        } else {
            console.log(res);
            characterClass = res;
            updateSpellcaster();
        }
    });
}

function setRaceOptions() {
    get("http://www.dnd5eapi.co/api/races", (res) => {
        if (res.error) {
            console.error(res.error);
        } else {
            
            let races = document.getElementById("races");
            
            // Clear loading option
            races.innerHtml = "";
            
            // Populate races options
            res.results.forEach((element, key) => {
                races[key] = new Option(element.name, element.url);
            });
            
            // Fetch race info
            getRaceInfo(races.value);
        }
    });
}

function setClassOptions() {
    get("http://www.dnd5eapi.co/api/classes", (res) => {
        if (res.error) {
            console.error(res.error);
        } else {
            
            let classes = document.getElementById("classes");
            
            // Clear loading option
            classes.innerHtml = "";
            
            // Populate classes options
            res.results.forEach((element, key) => {
                classes[key] = new Option(element.name, element.url);
            });
            
            getClassInfo(classes.value);
        }
    });
}