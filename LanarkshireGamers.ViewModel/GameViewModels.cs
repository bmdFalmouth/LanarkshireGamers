using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LanarkshireGamers.ViewModel
{
    public class GameViewModel
    {
        [Required]
        public string Name { get; set; }
        [ReadOnly(true)]
        public string GeekID { get; set; }
        public string ThumbNailURL { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public int MinNumberOfPlayers { get; set; }
        public int MaxNumberOfPlayers { get; set; }
        public int PlayTime { get; set; }
    }

    public class SearchGameViewModel
    {
        public const string BoardGameURL = "http://boardgamegeek.com/boardgame/";
        [Required]
        public string Name { get; set; }
        public string GeekID { get; set; }
        public bool Selected { get; set; }
        public string GameURL { get; set; }
    }

    public class SearchGameViewModelResults
    {
        
        public SearchGameViewModelResults()
        {
            Games = new List<SearchGameViewModel>();
        }
        [Required]
        [Display(Name = "Game Name")]
        public string searchTerm { get; set; }
        public IList<SearchGameViewModel> Games { get; set; }
    }
}
