using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MuhammadBugTracker.Models
{
    public class TicketType
    {
        public int Id { get; set; }

        [Display(Name = "Type")]
        public string Name { get; set; }

        #region 
        //Children
        public virtual ICollection<Ticket> Tickets { get; set; }

        public TicketType()
        {
            Tickets = new HashSet<Ticket>();
        }
        #endregion
    }
}