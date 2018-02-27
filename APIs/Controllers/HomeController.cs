using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net; //added this so we could do web requests
using System.IO;  //added this so we could use the Streamreader to read/hold the results of the response
using Newtonsoft.Json.Linq; //added this so that we could parse the result 

namespace APIs.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult StarWarsInfo()
        {
            ViewBag.Message = "Star Wars!";

            return View();
        }

        public ActionResult ShowInfoForPlanet(string ID)  //this is a search for planets in the Star Wars api based on the ID
        {
            HttpWebRequest request = WebRequest.CreateHttp("https://swapi.co/api/planets/" + ID);

            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
            //the browser making the request. this is just one example that we googled User Agent for correct syntax. 

            //request.Headers  (if needed, can request it. depends on the api documentation)


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //we haven't yet defined exactly what we want to get BACK in the response; only saying we want to hear back from an http page

            if (response.StatusCode == HttpStatusCode.OK)  //if all is well
            {
                StreamReader rd = new StreamReader(response.GetResponseStream());
                //use Streamreader to hold these large chunks of data

                string output = rd.ReadToEnd();  //read the whole response back, not just one line


                //now we need to parse the data. above namespace, add Using NewtonSoft.Json.Linq

                //1 declare a var to hold the info
                JObject JParser = JObject.Parse(output);  //JParser is a variable we just declared to hold the info

                //2 view it using the variable and call the field we want to see
                ViewBag.RawData = JParser["name"];
                //this "name" field is from the api. check the api at https://resttesttest.com/ and then parse the output in http://jsonviewer.stack.hu/ to see the available fields 
                //just declared this variable called rawdata and output; have to create the view later so it will be displayed

                ViewBag.RawData1 = JParser["residents"];  //can also add [0] etc to access arrays by their position


                //3 return view
                return View("ShowResults"); //just declared this view on the spot; now go create the view
            }

            else // if something is wrong (recieved a 404 or 500 error) go to this page and show the error
            {
                return View("../Shared/Error");
            }

        }


    }
}