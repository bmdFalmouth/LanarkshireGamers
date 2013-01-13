using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LanarkshireGamers.ViewModel;
using LanarkshireGamersData.Model;
using LanarkshireGamersData;
using LanarkshireGamesBusinessLogic.Helpers;

namespace LanarkshireGamesBusinessLogic
{
    public class GameBusinessLogic
    {
        public GameBusinessLogic()
        {
        }

        public IEnumerable<GameViewModel> GetAllGames()
        {
            List<GameViewModel> games = new List<GameViewModel>();
            foreach (Game g in LanarkshireGamersRepo.Instance.RetrieveAllGames())
            {
                games.Add(BusinessLogicHelper.ConvertGametoGameViewModel(g));
            }

            return games;
        }

        public IEnumerable<SearchGameViewModel> SearchGamesOnGeek(string searchTerm)
        {
            List<BoardGameGeekSearchResult> gamesFromGeek = new List<BoardGameGeekSearchResult>(GeekHelpers.SearchGeekForGame(searchTerm));
            List<SearchGameViewModel> gamesViewModel = new List<SearchGameViewModel>();

            foreach (BoardGameGeekSearchResult result in gamesFromGeek)
            {
                SearchGameViewModel gvm = BusinessLogicHelper.ConvertGeekGameSearchToGameViewModel(result);
                gvm.GameURL = SearchGameViewModel.BoardGameURL + gvm.GeekID;
                gamesViewModel.Add(gvm);
            }

            return gamesViewModel;
        }

        public bool SaveAllGames(IEnumerable<SearchGameViewModel> games,string username)
        {
            User user=LanarkshireGamersRepo.Instance.GetUserByUsername(username);
            foreach (SearchGameViewModel sgvm in games)
            {
                BoardGameGeekGame geekGame = GeekHelpers.GetGameByGeekID(sgvm.GeekID);
                LanarkshireGamersRepo.Instance.AddGameToUser(user, BusinessLogicHelper.ConverGeekGameToGame(geekGame));
            }
            return true;
        }
    }
}
