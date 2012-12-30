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
                gamesViewModel.Add(gvm);
            }

            return gamesViewModel;
        }
    }
}
