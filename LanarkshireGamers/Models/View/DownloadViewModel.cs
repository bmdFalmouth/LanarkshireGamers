using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanarkshireGamers.Models.View
{
    public class DownloadViewModel
    {
        public int CurrentItemCount { get; set; }
        public int TotalItemCount { get; set; }
        public string CurrentName { get; set; }
        public int PercentageComplete { get; set; }
    }
}