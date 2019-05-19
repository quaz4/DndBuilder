using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace DndBuilder.Model
{
    public abstract class Api
    {
        public string baseUrl;

        /* 
         * Constructor, ensure url is in correct format
         * Params: string baseUrl, base route for all API requests
         * Throws: ArgumentException        
         */
        public Api(string baseUrl) 
        { 
            // Check that the provided url is at least in the correct format
            if (!Uri.IsWellFormedUriString(baseUrl, UriKind.Absolute))
            {
                throw new ArgumentException();
            }

            this.baseUrl = baseUrl;
        }

        /* 
         * Perform an HTTP request to the specified path of the base path
         * Params: string path, route to get particular
         * Throws:        
         */
         // TODO: Exceptions?
        protected JObject GetRequest(string path)
        {
            string url;

            if (Uri.IsWellFormedUriString(path, UriKind.Absolute))
            {
                url = path;
            }
            else
            {
                url = this.baseUrl + path;
            }

            // Create the request object
            HttpWebRequest request = WebRequest.CreateHttp(url);

            // Extract the response and convert it to a JObject
            WebResponse response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            JObject res = JObject.Parse(responseString);

            return res;
        }

        // Character CRUD
        public abstract JObject CreateCharacter(Character character);
        public abstract JObject GetCharacter(string name);
        public abstract JObject UpdateCharacter(Character character);
        public abstract void DeleteCharacter(string name);

        // Race
        public abstract JObject GetRaces();
        public abstract JObject GetRace(int id);
        public abstract JObject GetRace(string name);

        // Class
        public abstract JObject GetClasses();
        public abstract JObject GetClass(int id);
        public abstract JObject GetClass(string name);
    }
}