using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventorA.Models
{
    public class PersonViewModel
    {
        public int PersonID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Pasword { get; set; }

        public List<CheckEventViewModel> Eventi { get; set; }
    }
}