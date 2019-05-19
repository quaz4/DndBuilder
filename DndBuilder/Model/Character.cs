using System;
using Newtonsoft.Json.Linq;

namespace DndBuilder.Model
{
    /*
     * The Character class is used to handle all of the logic around validation
     */
    public class Character
    {
        string Name;
        int Age;
        string Gender;
        string Biography;
        int Level;
        string CharacterRace;
        string CharacterClass;
        bool SpellCaster;
        int HitPoints;
        int Constitution;
        int Dexterity;
        int Strength;
        int Charisma;
        int Inteligence;
        int Wisdom;

        public Character(
            string Name,
            int Age,
            string Gender,
            string Biography,
            int Level,
            string CharacterRace,
            string CharacterClass,
            bool SpellCaster,
            int HitPoints,
            int Constitution,
            int Dexterity,
            int Strength,
            int Charisma,
            int Inteligence,
            int Wisdom
        )
        {
            // Name can be any non empty string
            if (Name.Length <= 0)
            {
                throw new ArgumentOutOfRangeException("Name has an invalid length of " + Name.Length);
            }

            // Age can be any number from 0 to 500
            if (Age < 0 || Age > 500)
            {
                throw new ArgumentOutOfRangeException("Age has an invalid length of " + Age);
            }

            // A string descriptor, can't be empty
            if (Gender.Length <= 0)
            {
                throw new ArgumentOutOfRangeException("Gender has an invalid length of " + Gender.Length);
            }

            // Biography must exist
            // TODO: Check if it actually needs to contain anything, as it is up to 500
            if (Biography.Length <= 0)
            {
                throw new ArgumentOutOfRangeException("Biography has an invalid length of " + Biography.Length);
            }

            // Level must be an int from 1 to 20
            if (Level < 1 || Level > 20)
            {
                throw new ArgumentOutOfRangeException("Biography has an invalid length of " + Biography.Length);
            }

            // CharacterRace is a non empty string
            if (CharacterRace.Length <= 0)
            {
                throw new ArgumentOutOfRangeException("CharacterRace has an invalid length of " + CharacterRace.Length);
            }

            // CharacterClass is a non empty string
            if (CharacterClass.Length <= 0)
            {
                throw new ArgumentOutOfRangeException("CharacterClass has an invalid length of " + CharacterClass.Length);
            }

            // TODO: Validation
            //if (SpellCaster hmmmm)
            //{
            //    throw new ArgumentOutOfRangeException("CharacterClass has an invalid length of " + CharacterClass.Length);
            //}

            // See if value is valid
            //if (HitPoints <= 0)
            //{
            //    throw new ArgumentOutOfRangeException("CharacterClass has an invalid length of " + CharacterClass.Length);
            //}

            // Ability scores
            //if (HitPoints <= 0)
            //{
            //    throw new ArgumentOutOfRangeException("CharacterClass has an invalid length of " + CharacterClass.Length);
            //}
        }

        public void FetchAbilityScores() {
            throw new NotImplementedException();
        }

        public JObject ToJson()
        {
            return new JObject();
        }
    }
}
