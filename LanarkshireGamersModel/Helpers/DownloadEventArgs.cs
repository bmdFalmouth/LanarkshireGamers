using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanarkshireGamersData.Helpers
{
    public class DownloadEventArgs:EventArgs
    {
        public int CurrentItemCount { get; set; }
        public int TotalItemCount { get; set; }
        public string CurrentName { get; set; }
    }
}