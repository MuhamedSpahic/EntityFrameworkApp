using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventorA.Models
{
    public class CheckPersonViewModel
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Username{ get; set; }

        public bool Checked { get; set; }
    }
}