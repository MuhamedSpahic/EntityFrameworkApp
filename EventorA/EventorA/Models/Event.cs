using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EventorA.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string Naziv { get; set; }
        public string Grad { get; set; }
        public string Adresa { get; set; }
        public string Opis { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }
       
        public virtual ICollection<PersonToEvent> PersonsToEvents { get; set; }




    }
}