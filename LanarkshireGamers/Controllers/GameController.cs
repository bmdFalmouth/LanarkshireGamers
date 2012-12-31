using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanarkshireGamesBusinessLogic;
using LanarkshireGamers.ViewModel;

namespace LanarkshireGamers.Controllers
{
    public class GameController : Controller
    {
        GameBusinessLogic gameLogic = new GameBusinessLogic();
        //
        // GET: /Game/
        public ActionResult Index()
        {
            return View(gameLogic.GetAllGames());
        }

        //
        //Get: /Game/Search
        public ActionResult Search()
        {
            SearchGameViewModelResults search = new SearchGameViewModelResults();
            search.searchTerm = "Catan";
            search.Games = new List<SearchGameViewModel>(gameLogic.SearchGamesOnGeek(search.searchTerm));
            return View(search);
        }

        //
        //Post: /Game/Search/term
        [HttpPost]
        public ActionResult Search(SearchGameViewModelResults search)
        {
            if (Request.IsAjaxRequest())
            {
                search.Games = new List<SearchGameViewModel>(gameLogic.SearchGamesOnGeek(search.searchTerm));
                return PartialView(search);
            }
            else
            {
                search.Games = new List<SearchGameViewModel>(gameLogic.SearchGamesOnGeek(search.searchTerm));
                return PartialView(search);
            }
        }

        //
        //Post: /Game/SaveSelection
        [HttpPost]
        public ActionResult SaveSelection(SearchGameViewModelResults results)
        {
            //get the results - get a filtered list of only those selected and then pass to 
            //logic to convert and save
            var saveResults = results.Games.Where(x => x.Selected == true);
            return Redirect("Index");
        }

    }
}
