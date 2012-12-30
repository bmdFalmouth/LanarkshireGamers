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
            return View();
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

    }
}
