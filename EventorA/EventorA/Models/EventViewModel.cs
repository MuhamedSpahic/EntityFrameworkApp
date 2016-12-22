using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventorA.Models
{
    public class EventViewModel
    {
        public int EventID { get; set; }
        public string Naziv { get; set; }
        public string Grad { get; set; }
        public string Opis { get; set; }
        public string Adresa { get; set; }
        public DateTime Datum { get; set; }
        public bool Checked { get; set; }

        public List<CheckPersonViewModel> Persons { get; set; }
   
    }
}