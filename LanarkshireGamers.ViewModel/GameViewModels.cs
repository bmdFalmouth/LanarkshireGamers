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
        [Required]
        public string Name { get; set; }
        [ReadOnly(true)]
        public string GeekID { get; set; }
        [ReadOnly(true)]
        public bool Selected { get; set; }
    }

    public class SearchGameViewModelResults
    {
        public string searchTerm { get; set; }
        public IEnumerable<SearchGameViewModel> Games { get; set; }
    }
}
