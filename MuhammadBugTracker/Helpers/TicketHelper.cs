using Microsoft.AspNet.Identity;
using MuhammadBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MuhammadBugTracker.Helpers
{
    public class TicketHelper
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public List<Ticket> ListProjectTickets(int projectId)
        {
           return db.Tickets.Where(t => t.ProjectId == projectId).ToList();
        }

        public ICollection<Ticket> ListUserTickets(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);

            var tickets = user.Tickets.ToList();
            return (tickets);
        }
        
        //public void AddUserToTicket(string userId, int ticketId)
        //{
        //    if (!IsUserOnTicket(userId, ticketId))
        //    {
        //        Ticket tick = db.Tickets.Find(ticketId);
        //        var newUser = db.Users.Find(userId);

        //        tick.AssignedToUser
        //    }
        //}

        public List<Ticket> GetOwnedTickets()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return (db.Tickets.Where(t => t.OwnerUserId == userId).ToList());
        }

        public void AddTicketHistory (Ticket oldTicket, Ticket newTicket)
        {
            //Each of these properties can trigger a history if they change
            var propList = new List<String> {
                "Title",
                "Description",
                "ProjectId",
                "TicketTypeId",
                "TicketPriorityId",
                "TicketStatusId",
                "AssignedToUserId" };

            //Write a for loop that loops through the properties of a Ticket
            foreach (var property in propList)
            {
                var newValue = newTicket.GetType().GetProperty(property) == null ? "" : newTicket.GetType().GetProperty(property).GetValue(newTicket).ToString();
                var oldValue = oldTicket.GetType().GetProperty(property) == null ? "" : oldTicket.GetType().GetProperty(property).GetValue(oldTicket).ToString();

                if(newValue != oldValue)
                {
                    //Add TicketHistory...
                    var newTicketHistory = new TicketHistory();
                    newTicketHistory.UserId = HttpContext.Current.User.Identity.GetUserId();
                    newTicketHistory.ChangeDate = DateTime.Now;
                    newTicketHistory.TicketId = newTicket.Id;

                    //Record property name and values
                    newTicketHistory.Property = property;
                    newTicketHistory.OldValue = oldValue;
                    newTicketHistory.NewValue = newValue;

                    db.TicketHistories.Add(newTicketHistory);
                    db.SaveChanges();
                }
            }
        }

        public void AddTicketNotification(int ticketId, string oldAssignedToId, string newAssignedToId)
        {
            if (String.IsNullOrEmpty(oldAssignedToId) && String.IsNullOrEmpty(newAssignedToId))
                return;

            //Assigning
            if (String.IsNullOrEmpty(oldAssignedToId) && !String.IsNullOrEmpty(newAssignedToId))
            {
                //Grab a copy of the ticket
                var ticket = db.Tickets.AsNoTracking().Include("Project").FirstOrDefault(t => t.Id == ticketId);

                //Create a new TicketNotification and set some default properties 
                var userId = HttpContext.Current.User.Identity.GetUserId();
                var ticketNotification = new TicketNotification();
                ticketNotification.SenderId = userId;
                ticketNotification.Created = DateTime.Now;
                ticketNotification.TicketId = ticketId;
                ticketNotification.RecipientId = newAssignedToId;

                //Assemble body of the message
                var msgBody = new StringBuilder();
                msgBody.AppendFormat("Hello {0},", db.Users.FirstOrDefault(u => u.Id == newAssignedToId).FirstName);
                msgBody.AppendFormat("");
                msgBody.AppendFormat("You have been assigned a freakin Ticket! Here's the info: ");
                msgBody.AppendFormat("  Ticket Id: " + ticketId);
                msgBody.AppendFormat("  Ticket Title: " + ticket.Title);
                msgBody.AppendFormat("  Project Id: " + ticket.ProjectId);
                msgBody.AppendFormat("  Project Title: " + ticket.Project.Name);
                msgBody.AppendFormat("");
                msgBody.AppendFormat("Let me know if you have any questions!");
                msgBody.AppendFormat(db.Users.FirstOrDefault(u => u.Id == userId).FirstName);

                //Set Body 
                ticketNotification.Body = msgBody.ToString();

                db.TicketNotifications.Add(ticketNotification);
                db.SaveChanges();

                //TODO: Here I can also send an email notification...
                //Start code: Add code here to email password reset link
            }
        }
    }
}