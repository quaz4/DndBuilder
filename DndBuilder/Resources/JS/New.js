// Globals
characterRace = null;
characterClass = null;
userAbilityPoints = [0, 0, 0, 0, 0, 0];

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
    
    get("/race/" + url, (res) => {
        if (res.error) {
            // TODO: Error handling
        } else {
            res = JSON.parse(res);
            characterRace = res;
            updateHitPointsValue();
            updateAllAbilityPoints();
        }
    });
}

function getClassInfo(url) {
    if (!url) {
        url = document.getElementById("classes").value;
    }

    get("/class/" + url, (res) => {
        if (res.error) {
            // TODO: Error handling
        } else {
            res = JSON.parse(res);
            characterClass = res;
            updateSpellcaster();
            updateHitPointsValue();
        }
    });
}

function loadRaceOptions() {
    get("/races", (res) => {
        console.log(res);
        res = JSON.parse(res);
        console.log(res);
        
        if (res.error) {
            console.error(res.error);
        } else {
            
            let races = document.getElementById("races");
            
            // Clear loading option
            races.innerHTML = "";
            
            // Populate races options
            res.results.forEach((element, key) => {
                races[key] = new Option(element.name);
            });
            
            // Fetch race info
            getRaceInfo(races.value);
        }
    });
}


function loadClassOptions() {
    get("/classes", (res) => {
    
        res = JSON.parse(res);
    
        if (res.error) {
            console.error(res.error);
        } else {
            
            let classes = document.getElementById("classes");
            
            // Clear loading option
            classes.innerHTML = "";
            
            // Populate classes options
            res.results.forEach((element, key) => {
                classes[key] = new Option(element.name);
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

function displayAbilityPoints(index, id) {
    let total = characterRace.ability_bonuses[index] + userAbilityPoints[index];
    document.getElementById(id).innerHTML = characterRace.ability_bonuses[index] + " + " + userAbilityPoints[index] + " = " + total;
}

// Update card displaying the total remaining ability points
function displayAbilityPointsTotal() {
    document.getElementById("remainingPoints").innerHTML = "<b>Remaining Points: " + (20 - calcTotalAbilityPoints());
}

function incAbilityPoint(index, id) {
    if (calcTotalAbilityPoints() < 20) {
        userAbilityPoints[index]++;
        displayAbilityPoints(index, id);
        displayAbilityPointsTotal();
    }
}

function decAbilityPoint(index, id) {
    if (userAbilityPoints[index] > 0) {
        userAbilityPoints[index]--;
        displayAbilityPoints(index, id);
        displayAbilityPointsTotal();
    }
}

function calcTotalAbilityPoints() {
    let total = 0;

    userAbilityPoints.forEach((value) => {
        total = total + value;
    });
    
    return total;
}

function updateAllAbilityPoints() {
    displayAbilityPoints(0, "constitution");
    displayAbilityPoints(1, "dexterity");
    displayAbilityPoints(2, "strength");
    displayAbilityPoints(3, "charisma");
    displayAbilityPoints(4, "intelligence");
    displayAbilityPoints(5, "wisdom");
}