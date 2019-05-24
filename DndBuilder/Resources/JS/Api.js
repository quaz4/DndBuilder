function getCharacter(name) {
    return new Promise((resolve, reject) => {
        get("/character/" + name).then((res) => {
            res = JSON.parse(res);
            resolve(res);
        }).catch((error) => {
            if(error) {
                alert("Unable to get character, please try again later");
            }
        });
    });
}

function checkIfExists(name) {
    return new Promise((resolve, reject) => {
        get("/character/exists/" + name).then((res) => {
            res = JSON.parse(res);
            resolve(res);
        }).catch((error) => {
            if(error) {
                alert("Unable to get character, please try again later");
            }
        });
    });
}

function createCharacter(newCharacter) {
    post("/character", newCharacter.output()).then(() => {
        alert("Character saved");
    }).catch((error) => {
        if(error) {
            alert("Unable to save character, please try again later");
        }
    });
}

function updateCharacter(character) {
    return new Promise((resolve, reject) => {
        put("/character", character.output()).then(() => {
            alert("Character updated");
        }).catch((error) => {
            if(error) {
                console.log(error)
                alert("Unable to save character, please try again later");
            }
        });
    });
}

function deleteCharacter(name) {
    if(confirm("Are you sure you want to delete " + name + "?")) {
        deleteRequest("/character/" + name).then(() => {
            alert("Character deleted");
        }).catch((error) => {
            if(error) {
                alert("Unable to delete character, please try again later");
            }
        });
    }
}