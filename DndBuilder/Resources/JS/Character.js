/*
 * Character class
 * Params: 
 *   name, string, the characters name
 *   level, int, the characters level TODO
 *   age, int, character age, between >= 0 and <= 500
 *   characterClass, 
 *   characterRace,
 *   bonuses,
 */
var Character = class Character {
    constructor(name, gender, biography, level, age, characterClass, characterRace, bonuses) {
        this.setName(name);
        this.setGender(gender);
        this.setBiography(biography);
        this.setLevel(level);
        this.setAge(age);
        this.setCharacterClass(characterClass);
        this.setCharacterRace(characterRace);
        this.setBonuses(bonuses);
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
     * Bonuses setter
     * Params: bonusesArray, array of ints of size 6, adds up to a max of 20
     * Throws: TypeError, RangeError
     */
    setBonuses(bonusesArray) {
        // Ensure the array is of size 6
        console.log(bonusesArray.length);
        if(bonusesArray.length != 6) {
            throw RangeError("Bonuses array must have a length of 6");
        }
        
        // Ensure each value in the array is a number
        bonusesArray.forEach((value, index) => {
            if(isNaN(value)) {
                throw TypeError("Bonus points at index " + index + " must be a number");
            }
        });
        
        // Calculate total points in array
        let total = 0;
    
        bonusesArray.forEach((value) => {
           total = total + value; 
        });
        
        if(total > 20) {
            throw RangeError("Total bonus points cannot be larger than 20");
        }
        
        this.bonuses = bonusesArray;
    }
    
    /*
     * Total Bonus Points getter
     * Returns the total number of assigned bonus points
     * Params: none
     * Throws: none
     */
    getBonusesTotal() {
        let total = 0;
    
        this.bonuses.forEach((value) => {
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
    
        if(this.characterClass.spellcasting) {
            caster = true;
        }
        
        return caster;
    }
}