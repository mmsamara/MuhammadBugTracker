﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MuhammadBugTracker.Models
{
    public class TicketAttachment
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string UserId { get; set; }

        #region
        //Parents
        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }
        #endregion
    }
}