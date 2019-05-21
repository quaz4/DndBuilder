using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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

        public override JObject CreateCharacter(Character character)
        {
            throw new NotImplementedException();
        }

        public override void DeleteCharacter(string name)
        {
            throw new NotImplementedException();
        }

        public override JObject GetCharacter(string name)
        {
            // Ensure name is not empty
            if (name.Length == 0)
            {
                throw new ArgumentException("Character name cannot be empty");
            }

            // TODO: DB request

            return null;
        }

        public override JObject GetClass(int id)
        {
            throw new NotImplementedException();
        }

        /*
         * 
         * Throws: ArgumentException, KeyNotFoundException
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

            // Search for objects with specified name
            JToken selected = classes.SelectToken("$.results[?(@.name == '" + name + "')]");

            // Throw exception if no object found
            if (selected == null)
            {
                throw new KeyNotFoundException("Unable to find requested class");
            }

            // Using url from object, get the resource at the url
            JObject res = this.GetRequest((string)selected["url"]);

            Console.WriteLine(res);

            return res;
        }

        public override JObject GetClasses()
        {
            JObject res = this.GetRequest("/classes");
            return res;
        }

        public override JObject GetRace(int id)
        {
            throw new NotImplementedException();
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

            return res;
        }

        public override JObject GetRaces()
        {
            JObject res = this.GetRequest("/races");
            return res;
        }

        public override JObject UpdateCharacter(Character character)
        {
            throw new NotImplementedException();
        }
    }
}