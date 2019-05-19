using System.Web;
using System.IO;
using System.Web.Http;
using System.Data;
using Mono.Data.Sqlite;
using DndBuilder.Model;

namespace DndBuilder
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Connect to the DB and check that it has the correct tables
            Database db = new Database();

            if (!db.IsSetup())
            {
                db.SetupDatabase();
            }

            // TODO: REMOVE ME
            Dnd5eApi api = new Dnd5eApi("http://www.dnd5eapi.co/api");
            api.GetRace("Human");
        }
    }
}
