function getCharacter(name) {
    get("/character/" + name).then((res) => {
        // TODO navigate to new page
        return res;
    }).catch((error) => {
        if(error) {
            alert("Unable to save character, please try again later");
        }
    });
}

function createCharacter(newCharacter) {
    post("/character", newCharacter).then(() => {
        // TODO navigate to new page
    }).catch((error) => {
        if(error) {
            console.error(error);
            alert("Unable to save character, please try again later");
        }
    });
}

function updateCharacter(character) {
    put("/character", character).then(() => {
        // TODO navigate to new page
    }).catch((error) => {
        if(error) {
            alert("Unable to save character, please try again later");
        }
    });
}

function deleteCharacter(name) {
    if(confirm("Are you sure you want to delete " + name + "?")) {
        deleteRequest("/character/" + name).then(() => {
            // TODO navigate to new page
        }).catch((error) => {
            if(error) {
                alert("Unable to delete character, please try again later");
            }
        });
    }
}