﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EventorA.Models
{
    public class Person
    {
        public int PersonID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
       [DataType(DataType.Password)]
        public string Pasword { get; set; }

        public virtual ICollection<PersonToEvent> PersonsToEvents { get; set; }
    }
}