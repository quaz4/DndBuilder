using System;
using System.Web;
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

            // Biography must exist and not be greater than 500 characters
            if (Biography.Length <= 0 || Biography.Length > 500)
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

        public string GetGender() => this.Gender;

        internal string GetBiography() => this.Biography;

        internal string GetClass() => this.CharacterClass;

        internal string GetRace() => this.CharacterRace;

        internal int GetLevel() => this.Level;

        internal int GetAge() => this.Age;

        public string GetName() => this.Name;

        public int GetConstitution() => this.UserPoints[0];

        public int GetDexterity() => this.UserPoints[1];

        public int GetStrength() => this.UserPoints[2];

        public int GetCharisma() => this.UserPoints[3];

        public int Intelligence => this.UserPoints[4];

        public int GetWisdom() => this.UserPoints[5];

        // Converts object to JSON so it can be sent over the web and used in
        // other contexts
        public JObject ToJson()
        {
            JObject character = new JObject(
                new JProperty("name", HttpUtility.HtmlEncode(this.Name)),
                new JProperty("age", HttpUtility.HtmlEncode(this.Age)),
                new JProperty("level", HttpUtility.HtmlEncode(this.Level)),
                new JProperty("gender", HttpUtility.HtmlEncode(this.Gender)),
                new JProperty("biography", HttpUtility.HtmlEncode(this.Biography)),
                new JProperty("characterClass", HttpUtility.HtmlEncode(this.CharacterClass)),
                new JProperty("characterRace", HttpUtility.HtmlEncode(this.CharacterRace)),
                new JProperty("userPoints", new JArray(
                    new JValue(HttpUtility.HtmlEncode(this.UserPoints[0])),
                    new JValue(HttpUtility.HtmlEncode(this.UserPoints[1])),
                    new JValue(HttpUtility.HtmlEncode(this.UserPoints[2])),
                    new JValue(HttpUtility.HtmlEncode(this.UserPoints[3])),
                    new JValue(HttpUtility.HtmlEncode(this.UserPoints[4])),
                    new JValue(HttpUtility.HtmlEncode(this.UserPoints[5]))
                ))
            );

            return character;
        }
    }
}
