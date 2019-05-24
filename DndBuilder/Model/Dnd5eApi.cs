using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json.Linq;

namespace DndBuilder.Model
{
    /*
     * The Dnd5eApi class is used to communicate with the
     */
    public class Dnd5eApi: Api
    {
        public Dnd5eApi(string baseUrl): base(baseUrl)
        {
            // Check that the provided url is at least in the correct format
            if (!Uri.IsWellFormedUriString(baseUrl, UriKind.Absolute))
            {
                throw new ArgumentException("URL format is incorrect");
            }

            this.baseUrl = baseUrl;
        }

        /*
         * Throws: ArgumentException
         */
        public override JObject GetClass(string name)
        {
            // Ensure name is not empty
            if (name.Length == 0)
            {
                throw new ArgumentException("Character name cannot be empty");
            }

            // Get list of classes
            JObject classes = this.GetClasses();

            // Search for object with specified name
            JToken selected = classes.SelectToken("$.results[?(@.name == '" + name + "')]");

            // Throw exception if no object found
            if (selected == null)
            {
                throw new ArgumentException("Unable to find requested class");
            }

            // Using url from object, get the resource at the url
            JObject res = this.GetRequest((string)selected["url"]);

            bool spellcaster = true;

            JObject rVal = new JObject();

            try
            {
                if (res["spellcasting"] == null)
                {
                    spellcaster = false;
                }

                // Sanitise data
                rVal = new JObject(
                    new JProperty("name", HttpUtility.HtmlEncode(res["name"])),
                    new JProperty("hit_die", HttpUtility.HtmlEncode(res["hit_die"])),
                    new JProperty("spellcasting", HttpUtility.HtmlEncode(spellcaster))
                );
            }
            catch (InvalidCastException e)
            {
                throw new InvalidCastException("Error fetching class: " + e.Message);
            }

            return rVal;
        }

        public override JObject GetClasses()
        {
            try
            {
                JObject res = this.GetRequest("/classes");
                JObject rVal = new JObject();
                JArray arr = new JArray();
                JProperty prop = new JProperty("results", arr);

                // Sanitise the links
                for (int i = 0; i < (int)res["count"]; i++)
                {
                    arr.Add( new JValue( HttpUtility.HtmlEncode(res["results"][i]["url"] )));
                }

                rVal.Add(prop);

                return res;
            }
            catch(SystemException e)
            {
                throw new ArgumentException("There was a problem getting classes: " + e.Message);
            }

        }

        public override JObject GetRace(string name)
        {
            // Ensure name is not empty
            if (name.Length == 0)
            {
                throw new ArgumentException("Name cannot be empty");
            }

            // Get list of classes
            JObject classes = this.GetRaces();

            // Search for objects with specified name
            JToken selected = classes.SelectToken("$.results[?(@.name == '" + name + "')]");

            // Throw exception if no object found
            if (selected == null)
            {
                throw new KeyNotFoundException("Unable to find requested class");
            }

            // Using url from object, get the resource at the url
            JObject res = this.GetRequest((string)selected["url"]);

            JObject rVal = new JObject();

            try
            {
                // Sanitise and reduce data
                rVal = new JObject(
                    new JProperty("name", HttpUtility.HtmlEncode(res["name"])),
                    new JProperty("ability_bonuses", new JArray(
                       new JValue(HttpUtility.HtmlEncode(res["ability_bonuses"][0])),
                       new JValue(HttpUtility.HtmlEncode(res["ability_bonuses"][1])),
                       new JValue(HttpUtility.HtmlEncode(res["ability_bonuses"][2])),
                       new JValue(HttpUtility.HtmlEncode(res["ability_bonuses"][3])),
                       new JValue(HttpUtility.HtmlEncode(res["ability_bonuses"][4])),
                       new JValue(HttpUtility.HtmlEncode(res["ability_bonuses"][5]))
                   ))
                );
            }
            catch (SystemException e)
            {
                throw new ArgumentException("There was a problem getting classes: " + e.Message);
            }


            return rVal;
        }

        public override JObject GetRaces()
        {
            try
            {
                JObject res = this.GetRequest("/races");
                JObject rVal = new JObject();
                JArray arr = new JArray();
                JProperty prop = new JProperty("results", arr);

                // Sanitise the links
                for (int i = 0; i < (int)res["count"]; i++)
                {
                    arr.Add(new JValue(HttpUtility.HtmlEncode(res["results"][i]["url"])));
                }

                rVal.Add(prop);

                return res;
            }
            catch (SystemException e)
            {
                throw new ArgumentException("There was a problem getting classes: " + e.Message);
            }
        }
    }
}