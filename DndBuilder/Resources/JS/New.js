/*
*   Function that wraps a GET request, taking a url and a callback
*   
*   url: The address to make the GET requst at
*   callback: The function to be called upon the completion of the request,
*   is is passed a results object that either contains an error or data
*/
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

/*
*   Function that initialises the page
*/
function init() {

    characterRace = null;
    characterClass = null;

    loadRaceOptions();
    loadClassOptions();
}

function updateHitPointsValue() {
    // Check if race and class info is known
    if (characterRace && characterClass) {
        let level = document.getElementById("level").value;
        
        // hitpoints = (level * class hit dice) + constitution score
        let hitpoints = (level * characterClass.hit_die) + characterRace.ability_bonuses[0];
        
        document.getElementById("hitpoints").innerHTML = "Hitpoints: " + hitpoints;
    }
}

function updateSpellcaster() {
    // Check if class info is known
    if (characterClass) {

        if (characterClass.spellcasting) {
            document.getElementById("spellcaster").innerHTML = "Spellcaster: Yes";
        } else {
            document.getElementById("spellcaster").innerHTML = "Spellcaster: No";
        }
    }
}

function getRaceInfo(url) {
    if (!url) {
        url = document.getElementById("races").value;
    }
    
    get(url, (res) => {
        if (res.error) {
            // TODO: Error handling
        } else {
            characterRace = res;
            updateHitPointsValue();
        }
    });
}

function getClassInfo(url) {
    if (!url) {
        url = document.getElementById("classes").value;
    }

    get(url, (res) => {
        if (res.error) {
            // TODO: Error handling
        } else {
            characterClass = res;
            updateSpellcaster();
            updateHitPointsValue();
        }
    });
}

function loadRaceOptions() {
    get("http://www.dnd5eapi.co/api/races", (res) => {
        if (res.error) {
            console.error(res.error);
        } else {
            
            let races = document.getElementById("races");
            
            // Clear loading option
            races.innerHTML = "";
            
            // Populate races options
            res.results.forEach((element, key) => {
                races[key] = new Option(element.name, element.url);
            });
            
            // Fetch race info
            getRaceInfo(races.value);
        }
    });
}


function loadClassOptions() {
    get("http://www.dnd5eapi.co/api/classes", (res) => {
        if (res.error) {
            console.error(res.error);
        } else {
            
            let classes = document.getElementById("classes");
            
            // Clear loading option
            classes.innerHTML = "";
            
            // Populate classes options
            res.results.forEach((element, key) => {
                classes[key] = new Option(element.name, element.url);
            });
            
            getClassInfo(classes.value);
        }
    });
}

function updateLevel() {
    let level = document.getElementById("level");
    
    if (level.value > 20) {
        level.value = 20;
    }
    
    if (level.value < 1) {
        level.value = 1;
    }
    
    updateHitPointsValue();
}