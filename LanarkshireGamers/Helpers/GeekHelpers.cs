using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;

namespace LanarkshireGamers
{
    public class GeekHelpers
    {
        //http://www.boardgamegeek.com/xmlapi/collection/zefquaavius?own=1

        const string BASE_URL = @"http://www.boardgamegeek.com/xmlapi/";
        const string COLLECTION_URL = @"collection/";
        const string OWN_PARAM = @"?own=1";

        const string GAME_URL=@"boardgame/";


        public static IEnumerable<Game> GetCollectionFromID(string geekID)
        {
            List<Game> collection = new List<Game>();
            //Get users collection
            string queryURL = string.Format("{0}{1}{2}{3}", BASE_URL, COLLECTION_URL,geekID, OWN_PARAM);
            XDocument xml=WebHelpers.GetXMLFromServer(queryURL);

            var gameEntries = xml.Root.Elements("item");
            foreach (var gameEntry in gameEntries)
            {
                collection.Add(GetGameByGeekID(gameEntry.Attribute("objectid").Value));
            }
            return collection;
        }

        public static Game GetGameByGeekID(string geekID)
        {
            Game g = new Game();

            string queryURL = string.Format("{0}{1}{2}", BASE_URL, GAME_URL, geekID);
            XDocument xml = WebHelpers.GetXMLFromServer(queryURL);
            var gameDetails = xml.Root.Element("boardgame");

            g.GeekID = geekID;
            g.Name = gameDetails.Elements("name").Where(x => (x.Attribute("primary") != null && x.Attribute("primary").Value == "true")).Select(x => x.Value).Single();
            g.MaxNumberOfPlayers = int.Parse(gameDetails.Element("maxplayers").Value);
            g.MinNumberOfPlayers = int.Parse(gameDetails.Element("minplayers").Value);
            g.PlayTime = int.Parse(gameDetails.Element("playingtime").Value);
            g.ThumbNailURL = gameDetails.Element("thumbnail").Value;
            g.Description = gameDetails.Element("description").Value;
            return g;
        }
    }
}
