using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

/* 
 * Abstract class defining what methods can be used when fetching dnd information
 * from a 3rd party
 */
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
         * Throws: InvalidDataException
         */
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

            try
            {
                // Create the request object
                HttpWebRequest request = WebRequest.CreateHttp(url);

                // Extract the response and convert it to a JObject
                WebResponse response = (HttpWebResponse)request.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                JObject res = JObject.Parse(responseString);

                return res;
            }
            catch
            {
                throw new InvalidDataException();
            }

        }

        // Race
        public abstract JObject GetRaces();
        public abstract JObject GetRace(string name);

        // Class
        public abstract JObject GetClasses();
        public abstract JObject GetClass(string name);
    }
}