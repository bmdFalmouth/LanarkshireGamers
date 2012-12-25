using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

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
            ConcurrentBag<Game> collection = new ConcurrentBag<Game>();
            //Get users collection
            string queryURL = string.Format("{0}{1}{2}{3}", BASE_URL, COLLECTION_URL,geekID, OWN_PARAM);
            XDocument xml=WebHelpers.GetXMLFromServer(queryURL);

            //Need to inform caller of progress of task
            var gameEntries = xml.Root.Elements("item");
            var cancellationToken = new CancellationTokenSource();
            Task parentTask = new Task(() =>
                {
                    while (collection.Count!=gameEntries.Count())
                    {
                        if (collection.Count==gameEntries.Count())
                            cancellationToken.Cancel();
                    }
                });
            foreach (var gameEntry in gameEntries)
            {
                var currentEntry = gameEntry;
                //Spin off a thread to do this
                //Parallel.Invoke(() => GetGameByGeekID(gameEntry.Attribute("objectid").Value));
                Task t = Task.Factory.StartNew<Game>(() => GetGameByGeekID(currentEntry.Attribute("objectid").Value))
                    .ContinueWith(x => collection.Add(x.Result));
            }
            try
            {
                parentTask.Start();
                parentTask.Wait(cancellationToken.Token);
            }
            catch (TaskCanceledException tce)
            {
                return collection; 
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
