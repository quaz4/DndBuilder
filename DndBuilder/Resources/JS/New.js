// Globals
characterRace = null;
characterClass = null;
userAbilityPoints = [0, 0, 0, 0, 0, 0];
character = null;
uniqueName = false;

/*
 * Function that initialises the page
 */
function init() {
    let params = new URLSearchParams(document.location.search);
    let name = params.get("name"); 
    
    if(name != null) {
        name = unescape(params.get("name"));
        createNew().then(() => {
            loadExisting(name);
        });
        document.getElementById("save").innerHTML = "Update";
        
    } else {
        document.getElementById("delete").setAttribute("hidden", true);
        
        createNew();
    }
}

function checkIfUnique(name) {
    checkIfExists(name).then((res) => {
        if (res) {
            alert("That name is already taken, please enter another");
            uniqueName = false;
        } else {
            uniqueName = true;
        }
    });
}

function loadExisting(name) {
    // Load exists character data
    getCharacter(decodeHtml(name)).then((res) => {
    
        res.userPoints.forEach((value, index) => {
            res.userPoints[index] = parseInt(value);
        });
    
        character = new Character(
            decodeHtml(res.name), //Name
            decodeHtml(res.gender), // Gender
            decodeHtml(res.biography), // Biography 
            parseInt(decodeHtml(res.level)), // Level
            parseInt(decodeHtml(res.age)), // Age
            characterClass, // Class
            characterRace, // Race
            res.userPoints // Ability Points
        );
    
        document.getElementById("name").value = decodeHtml(res.name);
        document.getElementById("age").value = decodeHtml(res.age);
        document.getElementById("gender").value = decodeHtml(res.gender);
        document.getElementById("biography").value = decodeHtml(res.biography);
        document.getElementById("level").value = decodeHtml(res.level);
        
        document.getElementById("races").value = decodeHtml(res.characterRace);
        document.getElementById("classes").value = decodeHtml(res.characterClass);
        userAbilityPoints = res.userPoints;
        raceChanged();
        classChanged();
        displayAbilityPointsTotal();
    });
}

function update() {
    updateCharacter(character).then(() => {
        
    });
}

function createNew() {
    return new Promise(async (resolve, reject) => {
        Promise.all([
            await loadRaceOptions(),
            await loadClassOptions()
        ]).then(() => {
            try {
                character = new Character(
                    "John Smith", //Name
                    "Male", // Gender
                    "I love adventure", // Biography 
                    1, // Level
                    1, // Age
                    characterClass, // Class
                    characterRace, // Race
                    userAbilityPoints // Ability Points
                );
                
                updateHitPointsValue();
                updateSpellcaster();
                
                resolve();
                
            } catch(error) {
                if (typeof error == "TypeError") { 
                } else {
                }
            } 
        });
    });
}

function updateHitPointsValue() {
    let hitpoints = character.hitpoints;
    document.getElementById("hitpoints").innerHTML = "Hitpoints: " + hitpoints;
}

function updateSpellcaster() {
    if (character.spellcaster) {
        document.getElementById("spellcaster").innerHTML = "Spellcaster: Yes";
    } else {
        document.getElementById("spellcaster").innerHTML = "Spellcaster: No";
    }
}

async function getClassInfo(url) {
    if (!url) {
        url = document.getElementById("classes").value;
    }

    await get("/class/" + url).then((res) => {
        if (res.error) {
            // TODO: Error handling
        } else {
            res = JSON.parse(res);
            characterClass = res;
        }
    });
}

function loadClassOptions() {
    return new Promise((resolve, reject) => {
        get("/classes").then(async (res) => {
        
            res = JSON.parse(res);
        
            if (res.error) {
            } else {
                
                let classes = document.getElementById("classes");
                
                // Clear loading option
                classes.innerHTML = "";
                
                // Populate classes options
                res.results.forEach((element, key) => {
                    classes[key] = new Option(element.name);
                });
                
                await getClassInfo(classes.value);
                resolve();
            }
        });
    });
}

function loadRaceOptions() {
    return new Promise((resolve, reject) => {
        get("/races").then(async (res) => {
            res = JSON.parse(res);
            
            if (res.error) {
            } else {
                
                let races = document.getElementById("races");
                
                // Clear loading option
                races.innerHTML = "";
                
                // Populate races options
                res.results.forEach((element, key) => {
                    races[key] = new Option(element.name);
                });
                
                // Fetch race info
                await getRaceInfo(races.value);
                
                resolve();
            }
        });
    });
}

async function getRaceInfo(url) {
    if (!url) {
        url = document.getElementById("races").value;
    }
    
    await get("/race/" + url).then((res) => {
        if (res.error) {
            // TODO: Error handling
        } else {
            res = JSON.parse(res);
            characterRace = res;
            updateAllAbilityPoints();
        }
    });
}

function displayAbilityPoints(index, id) {
    let total = parseInt(characterRace.ability_bonuses[index]) + parseInt(userAbilityPoints[index]);
    document.getElementById(id).innerHTML = userAbilityPoints[index] + " + " + characterRace.ability_bonuses[index]  + " = " + total;
}

// Update card displaying the total remaining ability points
function displayAbilityPointsTotal() {
    document.getElementById("remainingPoints").innerHTML = "<b>Remaining Points: " + (20 - character.getUserPointsTotal());
}

function incAbilityPoint(index, id) {
    if (calcTotalAbilityPoints() < 20) {
        userAbilityPoints[index]++;
        character.setUserPoints(userAbilityPoints);
        displayAbilityPoints(index, id);
        displayAbilityPointsTotal();
    }
}

function decAbilityPoint(index, id) {
    if (userAbilityPoints[index] > 0) {
        userAbilityPoints[index]--;
        character.setUserPoints(userAbilityPoints);
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

function nameChanged() {
    // Try change, if fails must be invalid
    try {
        checkIfUnique(document.getElementById("name").value);
        this.character.setName(document.getElementById("name").value);
    } catch(error) {
        if(error.name == "TypeError") {
            alert(error.message);
        }
    }
}

function ageChanged() {
    // Try change, if fails must be invalid
    try {
        this.character.setAge(document.getElementById("age").value);
    } catch(error) {
        if(error.name == "TypeError") {
            alert(error.message);
        }
        
        if(error.name == "RangeError") {
            alert(error.message);
        }
    }
}

function genderChanged() {
    // Try change, if fails must be invalid
    try {
        this.character.setGender(document.getElementById("gender").value);
    } catch(error) {
        if(error.name == "TypeError") {
            alert(error.message);
        }
    }
}

function biographyChanged() {
    // Try change, if fails must be invalid
    try {
        this.character.setBiography(document.getElementById("biography").value);
    } catch(error) {
        if(error.name == "TypeError") {
            alert(error.message);
        }
        
        if(error.name == "RangeError") {
            alert("Biography can't be empty or larger than 500 characters");
        }
    }
}

function levelChanged() {
    // Try change, if fails must be invalid
    try {
        let level = document.getElementById("level");
        
        if (level.value > 20) {
            level.value = 20;
        }
        
        if (level.value < 1) {
            level.value = 1;
        }

        this.character.setLevel(document.getElementById("level").value);
        
        updateHitPointsValue();
    } catch(error) {
        if(error.name == "TypeError") {
            alert(error.message);
        }
        
        if(error.name == "RangeError") {
            alert("Level must be between 1 and 20 inclusive");
        }
    }
}

function raceChanged() {
    getRaceInfo().then(() => {
        character.setCharacterRace(characterRace);
        
        updateHitPointsValue();
        updateAllAbilityPoints();
    });
}

function classChanged() {
    getClassInfo().then(() => {
        character.setCharacterClass(characterClass);
        
        updateHitPointsValue();
        updateAllAbilityPoints();
        updateSpellcaster();
    });
}

function onSave() {
    let params = new URLSearchParams(document.location.search);
    let name = params.get("name");
    
    if (name != null) {
        update();
    } else {
        createCharacter(character);
    }
}

function onDelete() {
    deleteCharacter(character.name);
}