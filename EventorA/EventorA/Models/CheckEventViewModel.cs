using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventorA.Models
{
    public class CheckEventViewModel
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public bool Checked { get; set; }
    }
}