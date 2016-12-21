using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventorA.Models
{
    public class PersonToEvent
    {
        public int PersonToEventID { get; set; }
        public int PersonID { get; set; }
        public int EventID { get; set; }

        public virtual Event Event { get; set; }
        public virtual Person Person { get; set; }
    }
}