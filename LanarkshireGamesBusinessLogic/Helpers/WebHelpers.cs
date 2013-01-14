using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Net;

namespace LanarkshireGamesBusinessLogic.Helpers
{
    //http://www.codeproject.com/Articles/422572/Exception-Handling-in-ASP-NET-MVC
    public class WebHelpers
    {
        public static XDocument GetXMLFromServer(string address)
        {
            try
            {
                WebClient wc = new WebClient();
                XDocument doc = XDocument.Parse(wc.DownloadString(address));
                return doc;
            }
            catch (Exception e)
            {
                //rethrow
                throw new Exception("Can't download data, website may be down");
            }

        }
    }
}
