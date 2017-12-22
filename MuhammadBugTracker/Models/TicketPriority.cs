using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MuhammadBugTracker.Models
{
    public class TicketPriority
    {
        public int Id { get; set; }

        [Display(Name = "Priority")]
        public string Name { get; set; }

        #region 
        //Children
        public virtual ICollection<Ticket> Tickets { get; set; }

        public TicketPriority()
        {
            Tickets = new HashSet<Ticket>();
        }
        #endregion
    }
}