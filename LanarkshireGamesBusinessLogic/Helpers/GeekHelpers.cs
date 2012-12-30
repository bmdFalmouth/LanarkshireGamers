using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using LanarkshireGamersData.Model;

namespace LanarkshireGamesBusinessLogic.Helpers
{
    public class BoardGameGeekSearchResult
    {
        public string GeekID { get; set; }
        public string Name { get; set; }
    }

    public class BoardGameGeekGame
    {
        public string GeekID { get; set; }
        public string Name { get; set; }
        public string ThumbNailURL { get; set; }
        public string Description { get; set; }
        public int MinNumberOfPlayers { get; set; }
        public int MaxNumberOfPlayers { get; set; }
        public int PlayTime { get; set; }
        public string YearPublished { get; set; }
    }

    public class GeekHelpers
    {
        //http://www.boardgamegeek.com/xmlapi/collection/zefquaavius?own=1

        const string BASE_URL = @"http://www.boardgamegeek.com/xmlapi/";
        const string COLLECTION_URL = @"collection/";
        const string OWN_PARAM = @"?own=1";

        const string GAME_URL=@"boardgame/";
        const string SEARCH_URL = @"search?search=";

        static int currentCount = 0;
        static int totalCount = 0;

        public delegate void DownloadBoardGame(object source, DownloadEventArgs args);

        public static event EventHandler<DownloadEventArgs> DownloadBoardGameEvent;

        public static IEnumerable<BoardGameGeekGame> GetCollectionFromID(string geekID)
        {
            currentCount = 0;
            ConcurrentBag<BoardGameGeekGame> collection = new ConcurrentBag<BoardGameGeekGame>();
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

            totalCount = gameEntries.Count();
            foreach (var gameEntry in gameEntries)
            {
                var currentEntry = gameEntry;
                //Spin off a thread to do this
                //Parallel.Invoke(() => GetGameByGeekID(gameEntry.Attribute("objectid").Value));
                Task t = Task.Factory.StartNew<BoardGameGeekGame>(() => GetGameByGeekID(currentEntry.Attribute("objectid").Value))
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

        public static IEnumerable<BoardGameGeekSearchResult> SearchGeekForGame(string gameName)
        {
            List<BoardGameGeekSearchResult> collection = new List<BoardGameGeekSearchResult>();

            string queryURL = string.Format("{0}{1}{2}", BASE_URL, SEARCH_URL, gameName);
            XDocument xml = WebHelpers.GetXMLFromServer(queryURL);
            //Need to inform caller of progress of task
            var gameEntries = xml.Root.Elements("boardgame");
            foreach (var gameEntry in gameEntries)
            {
                BoardGameGeekSearchResult result=new BoardGameGeekSearchResult{
                    GeekID=gameEntry.Attribute("objectid").Value,
                    Name=gameEntry.Element("name").Value
                };

                collection.Add(result);
            }
            return collection;

        }

        public static BoardGameGeekGame GetGameByGeekID(string geekID)
        {
            BoardGameGeekGame g = new BoardGameGeekGame();

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
            g.YearPublished = gameDetails.Element("yearpublished").Value;
            
            if (DownloadBoardGameEvent != null)
            {
                EventHandler<DownloadEventArgs> downloadEvent = DownloadBoardGameEvent;
                downloadEvent(g, new DownloadEventArgs { CurrentItemCount = currentCount, TotalItemCount = totalCount, CurrentName = g.Name });
            }
            currentCount++;
            return g;
        }
    }
}
