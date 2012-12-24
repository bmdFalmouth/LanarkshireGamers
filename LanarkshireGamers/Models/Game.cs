﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LanarkshireGamers
{
    public class Game
    {
        public int Id { get; set; }
        public string GeekID { get; set; }
        public string Name { get; set; }
        public string ThumbNailURL { get; set; }

        [MaxLength]
        public string Description { get; set; }
        public int MinNumberOfPlayers { get; set; }
        public int MaxNumberOfPlayers { get; set; }
        public int PlayTime { get; set; }
    }
}
