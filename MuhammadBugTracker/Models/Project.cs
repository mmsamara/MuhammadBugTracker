using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MuhammadBugTracker.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Display(Name = "Project")]
        public string Name { get; set; }

        #region
        //Children
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<ApplicationUser> ProjectUsers { get; set; }
        public object Users { get; internal set; }

        public Project()
        {
            Tickets = new HashSet<Ticket>();
            ProjectUsers = new HashSet<ApplicationUser>();
        }
        #endregion
    }
}