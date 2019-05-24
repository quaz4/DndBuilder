/*
 * Character class
 * Params: 
 *   name, string, the characters name
 *   level, int, the characters level TODO
 *   age, int, character age, between >= 0 and <= 500
 *   characterClass, 
 *   characterRace,
 *   userPoints,
 */
var Character = class Character {
    constructor(name, gender, biography, level, age, characterClass, characterRace, userPoints) {
        this.setName(name);
        this.setGender(gender);
        this.setBiography(biography);
        this.setLevel(level);
        this.setAge(age);
        this.setCharacterClass(characterClass);
        this.setCharacterRace(characterRace);
        this.setUserPoints(userPoints);
    }
    
    /*
     * Hitpoints getter
     * Params: none
     * Throws: Error
     */
    get hitpoints() {
        try {
            return (this.level * characterClass.hit_die) + characterRace.ability_bonuses[0];
        } catch(e) {
            throw new Error("Unable to access variables");
        } 
    }
    
    /*
     * Name setter
     * Params: name, a string of length > 0
     * Throws: TypeError
     */
    setName(name) {
        if(name == undefined || name == null) {
            throw TypeError("Name must exist");
        }
        
        if(name.length <= 0) {
            throw TypeError("Name must not be empty");
        }
        
        this.name = name;
    }
    
    /*
     * Age setter
     * Params: name, a string of length > 0
     * Throws: TypeError
     */
    setAge(age) {
        if(isNaN(age)) {
            throw TypeError("Age must be a number");
        }
        
        if(age < 0 || age > 500) {
            throw RangeError("Age must be be >= 0 and <= 500");
        }
        
        this.age = age;
    }
    
    getClass() {
        return this.characterClass.name;
    }
  
    getRace() {
        return this.characterRace.name;
    }
    
    /*
     * Level setter
     * Params: level, an int >= 1 and <= 20
     * Throws: TypeError, RangeError
     */
    setLevel(level) {
        if(isNaN(level)) {
            throw TypeError("Level must be a number");
        }
        
        if(level < 1 || level > 20) {
            throw RangeError("Level must be be >= 1 and <= 20");
        }
        
        this.level = level;
    }
    
    /*
     * userPoints setter
     * Params: userPointsArray, array of ints of size 6, adds up to a max of 20
     * Throws: TypeError, RangeError
     */
    setUserPoints(userPointsArray) {
        // Ensure the array is of size 6
        if(userPointsArray.length != 6) {
            throw RangeError("userPointsArray array must have a length of 6");
        }
        
        // Ensure each value in the array is a number
        userPointsArray.forEach((value, index) => {
            if(isNaN(value)) {
                throw TypeError("userPointsArray points at index " + index + " must be a number");
            }
        });
        
        // Calculate total points in array
        let total = 0;
    
        userPointsArray.forEach((value) => {
           total = total + value;
        });
        
        if(total > 20) {
            throw RangeError("Total ability points cannot be larger than 20");
        }
        
        this.userPoints = userPointsArray;
    }
    
    /*
     * Total Ability Points getter
     * Returns the total number of assigned points
     * Params: none
     * Throws: none
     */
    getUserPointsTotal() {
        let total = 0;
    
        this.userPoints.forEach((value) => {
           total = total + value; 
        });
        
        return total;
    }
    
    setCharacterClass(characterClass) {
        if(characterClass == undefined || characterClass == null) {
            throw TypeError("CharacterClass must exist");
        }
        
        // Ensure .name exists
        if(characterClass.name == undefined || characterClass.name == null) {
            throw TypeError("CharacterClass name must exist");
        }
        
        // Ensure hit_die value exists and is a number
        if(isNaN(characterClass.hit_die)) {
            throw TypeError("hit_die must exist in CharacterClass");
        }
        
        this.characterClass = characterClass;
    }
    
    setCharacterRace(characterRace) {
        if(characterRace == undefined || characterRace == null) {
            throw TypeError("CharacterRace must exist");
        }
        
        // Ensure .name exists
        if(characterRace.name == undefined || characterClass.name == null) {
            throw TypeError("CharacterRace name must exist");
        }
        
        // Ensure .ability_bonuses exists and is of length 6
        if(characterRace.ability_bonuses.length != 6) {
            throw TypeError("CharacterRace must have an ability_bonuses array of length 6");
        }
        
        // Ensure .ability_bonuses values are all ints >= 0
        characterRace.ability_bonuses.forEach((value, index) => {
            if(isNaN(value)) {
                throw TypeError("Invalid value in ability_bonuses array at index " + index);
            }
            
            if(value < 0) {
                throw RangeError("Value in ability_bonuses array at index " + index + " must be >= 0"); 
            }
        });
        
        this.characterRace = characterRace;
    }
    
    setGender(gender) {
        if(gender == undefined || gender == null) {
            throw TypeError("Gender must exist");
        }
        
        if(gender.length <= 0) {
            throw TypeError("Gender string can't be empty");
        }
        
        this.gender = gender;
    }
    
    setBiography(biography) {
        if(biography == undefined || biography == null) {
            throw TypeError("Biography must exist");
        }
        
        if(biography.length <= 0 || biography.length > 500) {
            throw TypeError("Biography string can't be empty or larger than 500 characters");
        }
        
        this.biography = biography;
    }
    
    get spellcaster() {
        let caster = false;

        if(this.characterClass.spellcasting == "True") {
            caster = true;
        }
        
        return caster;
    }
    
    output() {
        return {
            "name": this.name,
            "level": this.level,
            "age": this.age,
            "gender": this.gender,
            "biography": this.biography,
            "characterClass": this.characterClass.name,
            "characterRace": this.characterRace.name,
            "userPoints": this.userPoints
        }
    }
    
    toXML() {
        let xmlString = 
        '<?xml version="1.0" encoding="UTF-8"?>' +
        "<Character>" +
            "<Name>" + this.name + "</Name>" +
            "<Age>" + this.age + "</Age>" +
            "<Gender>" + this.gender + "</Gender>" +
            "<Biography>" + this.biography + "</Biography>" +
            "<Level>" + this.level + "</Level>" +
            "<Class>" + this.characterClass.name + "</Class>" +
            "<Race>" + this.characterRace.name + "</Race>" +
            "<Spellcaster>" + this.spellcaster + "</Spellcaster>" +
            "<Hitpoints>" + this.hitpoints + "</Hitpoints>" +
            "<AbilityPoints>" +
                "<Constitution>" +
                    "<Points>" + userAbilityPoints[0] + "</Points>" +
                    "<Bonus>" + characterRace.ability_bonuses[0] + "</Bonus>" +
                "</Constitution>" +
                "<Dexterity>" +
                    "<Points>" + userAbilityPoints[1] + "</Points>" +
                    "<Bonus>" + characterRace.ability_bonuses[1] + "</Bonus>" +
                "</Dexterity>" +
                "<Strength>" +
                    "<Points>" + userAbilityPoints[2] + "</Points>" +
                    "<Bonus>" + characterRace.ability_bonuses[2] + "</Bonus>" +
                "</Strength>" +
                "<Charisma>" +
                    "<Points>" + userAbilityPoints[3] + "</Points>" +
                    "<Bonus>" + characterRace.ability_bonuses[3] + "</Bonus>" +
                "</Charisma>" +
                "<Intelligence>" +
                    "<Points>" + userAbilityPoints[4] + "</Points>" +
                    "<Bonus>" + characterRace.ability_bonuses[4] + "</Bonus>" +
                "</Intelligence>" +
                "<Wisdom>" +
                    "<Points>" + userAbilityPoints[5] + "</Points>" +
                    "<Bonus>" + characterRace.ability_bonuses[5] + "</Bonus>" +
                "</Wisdom>" +
            "</AbilityPoints>" +
        "</Character>";
        
        var oParser = new DOMParser();
        var oDOM = oParser.parseFromString(xmlString, "application/xml");
        
        let blob = new Blob(
            [ xmlString ],
            {
                type : "text/xml;charset=utf-8"
            }
        );
        
        let a = document.createElement("a");
        document.body.appendChild(a);
        a.style = "display: none";
        
        let url = window.URL.createObjectURL(blob);
        
        a.href = url;
        a.download = "character.xml";
        a.click();
        
        window.URL.revokeObjectURL(url);
        //console.log(oDOM.documentElement);    
    }
}