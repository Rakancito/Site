using System;
using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Site.Models
{
    public class Players
    {
        public int pId { get; set; }
        public string sName { get; set; }
        public int iJob { get; set; }
        public int iLevel { get; set; }
        public int iExp { get; set; }
    }
    public class PlayerViewModel
    {
        public Players Players { get; set; }
        public string PageTitle { get; set; }

    }

}
