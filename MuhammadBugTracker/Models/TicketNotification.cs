using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MuhammadBugTracker.Models
{
    public class TicketNotification
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int TicketId { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public DateTime Created { get; set; }

        #region
        //Parents 
        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser Recipient { get; set; }
        #endregion 
    }
}