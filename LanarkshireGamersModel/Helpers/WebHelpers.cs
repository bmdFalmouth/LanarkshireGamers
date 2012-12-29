using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Net;

namespace LanarkshireGamersData.Helpers
{
    public class WebHelpers
    {
        public static XDocument GetXMLFromServer(string address)
        {
            WebClient wc = new WebClient();
            XDocument doc = XDocument.Parse(wc.DownloadString(address));
            return doc;
        }
    }
}
