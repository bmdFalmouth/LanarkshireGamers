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
        // GET: /Game/Search/
        public ActionResult Search()
        {
            return View(new SearchGameViewModelResults());
        }

        //
        //POST: /Game/Search/
        [HttpPost]
        public ActionResult Search(SearchGameViewModelResults search)
        {
            if (Request.IsAjaxRequest())
            {
                search.Games = new List<SearchGameViewModel>(gameLogic.SearchGamesOnGeek(search.searchTerm));
                return View(search);
            }
            else
            {
                search.Games = new List<SearchGameViewModel>(gameLogic.SearchGamesOnGeek(search.searchTerm));
                return View(search);
            }
        }

        //
        //Post: /Game/SaveSelection
        [HttpPost]
        public ActionResult SaveSelection(IList<SearchGameViewModel> games)
        {
            //get the results - get a filtered list of only those selected and then pass to 
            //logic to convert and save
            var saveResults = games.Where(x => x.Selected == true);
            gameLogic.SaveAllGames(saveResults.ToList(), HttpContext.User.Identity.Name);
            return Redirect("Index");
        }

    }
}
