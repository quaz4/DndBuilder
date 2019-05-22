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
        int[] UserPoints;

        public Character(
            string Name,
            int Age,
            string Gender,
            string Biography,
            int Level,
            string CharacterRace,
            string CharacterClass,
            int[] UserPoints
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

            // Points must add up to 20
            int total = 0;

            foreach (var point in UserPoints)
            {
                total = total + point;
            }

            if (total != 20) 
            {
                throw new ArgumentException("Total bonus points must add up to 20");
            }

            if (UserPoints.Length != 6)
            {
                throw new ArgumentException("UserPoints array must be of size 6");
            }

            this.Name = Name;
            this.Age = Age;
            this.Gender = Gender;
            this.Biography = Biography;
            this.Level = Level;
            this.CharacterRace = CharacterRace;
            this.CharacterClass = CharacterClass;
            this.UserPoints = UserPoints;
        }

        public string GetGender()
        {
            return this.Gender;
        }

        internal string GetBiography()
        {
            return this.Biography;
        }

        internal string GetClass()
        {
            return this.CharacterClass;
        }

        internal string GetRace()
        {
            return this.CharacterRace;
        }

        internal int GetLevel()
        {
            return this.Level;
        }

        internal int GetAge()
        {
            return this.Age;
        }

        public string GetName()
        {
            return this.Name;
        }

        public int GetConstitution() 
        {
            return this.UserPoints[0];
        }

        public int GetDexterity()
        {
            return this.UserPoints[1];
        }

        public int GetStrength()
        {
            return this.UserPoints[2];
        }

        public int GetCharisma()
        {
            return this.UserPoints[3];
        }

        public int GetIntelligence()
        {
            return this.UserPoints[4];
        }

        public int GetWisdom()
        {
            return this.UserPoints[5];
        }

        // TODO
        public JObject ToJson()
        {
            return new JObject();
        }
    }
}
