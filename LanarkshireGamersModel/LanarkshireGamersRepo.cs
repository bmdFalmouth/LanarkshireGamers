using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LanarkshireGamersData.Model;
using System.Data;

namespace LanarkshireGamersData
{
    public class LanarkshireGamersRepo
    {
        private LanarkshireGamersContext context;

        static readonly LanarkshireGamersRepo instance = new LanarkshireGamersRepo();

        static LanarkshireGamersRepo()
        {

        }

        public static LanarkshireGamersRepo Instance
        {
            get
            {
                return instance;
            }
        }

        LanarkshireGamersRepo()
        {
            context = new LanarkshireGamersContext();
        }

        public User GetUserByUsername(string username)
        {
            User user = context.User.FirstOrDefault(u => u.UserName == username);
            return user;
        }

        public bool AddGame(Game g)
        {
            if (context.Game.FirstOrDefault(x => x.Name == g.Name) == null)
            {
                context.Game.Add(g);
                try
                {
                    context.SaveChanges();
                }
                catch (InvalidOperationException ioe)
                {
                    //rethrow as an add user exception
                    return false;
                }
            }
            return true;
        }

        public bool AddGameToUser(User u, Game g)
        {
            Game game=context.Game.FirstOrDefault(x => x.Name == g.Name);
            if (game == null)
            {
                AddGame(g);
                u.Games.Add(g);
            }
            else
            {
                u.Games.Add(game);
            }
            try
            {
                context.Entry(u).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (InvalidOperationException ioe)
            {
                //rethrow as an add user exception
                return false;
            }

            return true;
        }

        public bool AddUser(User u)
        {
            u.Games=new List<Game>();
            context.User.Add(u);
            try
            {
                context.SaveChanges();
            }
            catch (InvalidOperationException ioe)
            {
                //rethrow as an add user exception
                return false;
            }
            return true;
        }

        public bool UpdateUser(User u)
        {
            context.Entry(u).State = EntityState.Modified;
            context.SaveChanges();
            return true;
        }

        public IEnumerable<Game> GetUsersGame(string username)
        {
            User u = GetUserByUsername(username);

            return u.Games;
        }

        public IEnumerable<Game> RetrieveAllGames()
        {
            return context.Game;
        }

        public Game RetrieveGameByName(string name)
        {
            return context.Game.FirstOrDefault(g => g.Name == name);
        }

        public Game RetrieveGameByGeekID(string geekID)
        {
            return context.Game.FirstOrDefault(g => g.GeekID == geekID);
        }

        public IEnumerable<Game> RetrieveAllGamesByPlaytime(int minTime,int maxTime)
        {
            return context.Game.Where(x => x.PlayTime >= minTime || x.PlayTime <= maxTime);
        }

        public IEnumerable<Game> RetrieveAllGamesByPlayerNumber(int minPlayers, int maxPlayers)
        {
            return context.Game.Where(x => x.MinNumberOfPlayers >= minPlayers || x.MaxNumberOfPlayers <= maxPlayers);
        }
    }
}
