namespace MuhammadBugTracker.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MuhammadBugTracker.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MuhammadBugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MuhammadBugTracker.Models.ApplicationDbContext context)
        {
            #region Example Code
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            #endregion

            #region Role Creation Section
            //Instantiate an object of type RoleManager to work with Roles within our system (define role, check existence of role, if we don't see it we'll add one)
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //Check for the existence of a role named "Admin"
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "ProjectManager"))
            {
                roleManager.Create(new IdentityRole { Name = "ProjectManager" });
            }

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }
            #endregion

            #region User Creation Section
            //Instantiate an object of type UserManager to work with Users in our system
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            //Check for the existence of a user in our system by email address 
            if (!context.Users.Any(u => u.Email == "mmsamara@ncsu.edu"))
            {
                //If there isn't one we'll create it
                userManager.Create(new ApplicationUser
                {
                    UserName = "mmsamara@ncsu.edu",
                    Email = "mmsamara@ncsu.edu",
                    FirstName = "Muhammad",
                    LastName = "Samara",
                    DisplayName = "Momo"
                }, "Abc#123");
            }

            if (!context.Users.Any(u => u.Email == "DemoProjectManager@mailinator.com"))
            {
                //If there isn't one we'll create it
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoProjectManager@mailinator.com",
                    Email = "DemoProjectManager@mailinator.com",
                    FirstName = "Mr.",
                    LastName = "PM",
                    DisplayName = "Mr. PM"
                }, "Abc#123");
            }

            if (!context.Users.Any(u => u.Email == "DemoDeveloper@mailinator.com"))
            {
                //If there isn't one we'll create it
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloper@mailinator.com",
                    Email = "DemoDeveloper@mailinator.com",
                    FirstName = "Mr.",
                    LastName = "Developer",
                    DisplayName = "Mr. Developer"
                }, "Abc#123");
            }

            if (!context.Users.Any(u => u.Email == "DemoSubmitter@mailinator.com"))
            {
                //If there isn't one we'll create it
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitter@mailinator.com",
                    Email = "DemoSubmitter@mailinator.com",
                    FirstName = "Submitter",
                    LastName = "Man",
                    DisplayName = "Submitter"
                }, "Abc#123");
            }

            if (!context.Users.Any(u => u.Email == "Schmilk@mailinator.com"))
            {
                //If there isn't one we'll create it
                userManager.Create(new ApplicationUser
                {
                    UserName = "Schmilk@mailinator.com",
                    Email = "Schmilk@mailinator.com",
                    FirstName = "Schmilk",
                    LastName = "Guy",
                    DisplayName = "Schmilk"
                }, "Abc#123");
            }

            if (!context.Users.Any(u => u.Email == "SgtSwarm@mailinator.com"))
            {
                //If there isn't one we'll create it
                userManager.Create(new ApplicationUser
                {
                    UserName = "SgtSwarm@mailinator.com",
                    Email = "SgtSwarm@mailinator.com",
                    FirstName = "Sgt.",
                    LastName = "Swarm",
                    DisplayName = "Sgt. Swarm"
                }, "Abc#123");
            }

            #endregion

            #region Seed Projects
            context.Projects.AddOrUpdate(
                            p => p.Name,
                                new Project
                                {
                                    Id = 10010, 
                                    Name = "Project 1"
                                },

                                new Project
                                {
                                    Id = 10020,
                                    Name = "Project 2"
                                },

                                new Project
                                {
                                    Id = 10030,
                                    Name = "Project 3"
                                }
                            );
            #endregion       

            #region Seed Ticket Type Table
            context.TicketTypes.AddOrUpdate(
                t => t.Name,
                    new TicketType { Id=100, Name = "Bug"},
                    new TicketType { Id=200, Name = "Requested Change"},
                    new TicketType { Id=300, Name = "Useless Work"}
                );
            #endregion

            #region Seed Ticket Status 
            context.TicketStatuses.AddOrUpdate(
                t => t.Name,
                    new TicketStatus { Id=100, Name = "Unassigned" },
                    new TicketStatus { Id=200, Name = "Assigned" },
                    new TicketStatus { Id=300, Name = "Completed" }
                );
            #endregion

            #region Seed Ticket Priority 
            context.TicketPriorities.AddOrUpdate(
                t => t.Name,
                    new TicketPriority { Id=100, Name = "High"},
                    new TicketPriority { Id=200, Name = "Medium" },
                    new TicketPriority { Id=300, Name = "Low" }
                );
            #endregion

            #region Seed Tickets 
            context.Tickets.AddOrUpdate(
                t => t.Title,
                    new Ticket
                    {
                        Title = "Ticket 1, Project 1",
                        Created = DateTime.Now,
                        Description = "This is a ticket description",
                        OwnerUserId = userManager.FindByEmail("DemoSubmitter@mailinator.com").Id,
                        ProjectId = 10010,
                        TicketTypeId = 100,
                        TicketPriorityId = 100,
                        TicketStatusId = 100
                    },

                    new Ticket
                    {
                        Title = "Ticket 2, Project 1",
                        Created = DateTime.Now,
                        Description = "This is a ticket description",
                        OwnerUserId = userManager.FindByEmail("DemoSubmitter@mailinator.com").Id,
                        ProjectId = 10010,
                        TicketTypeId = 300,
                        TicketPriorityId = 200,
                        TicketStatusId = 200,
                    },

                    new Ticket
                    {
                        Title = "Ticket 1, Project 2",
                        Created = DateTime.Now,
                        Description = "This is a ticket description",
                        OwnerUserId = userManager.FindByEmail("DemoSubmitter@mailinator.com").Id,
                        ProjectId = 10020,
                        TicketTypeId = 100,
                        TicketPriorityId = 100,
                        TicketStatusId = 100,
                    },

                    new Ticket
                    {
                        Title = "Ticket 2, Project 2",
                        Created = DateTime.Now,
                        Description = "This is a ticket description",
                        OwnerUserId = userManager.FindByEmail("DemoSubmitter@mailinator.com").Id,
                        ProjectId = 10020,
                        TicketTypeId = 300,
                        TicketPriorityId = 200,
                        TicketStatusId = 200,
                    },

                    new Ticket
                    {
                        Title = "Ticket 3, Project 2",
                        Created = DateTime.Now,
                        Description = "This is a ticket description",
                        OwnerUserId = userManager.FindByEmail("DemoSubmitter@mailinator.com").Id,
                        ProjectId = 10020,
                        TicketTypeId = 300,
                        TicketPriorityId = 300,
                        TicketStatusId = 100,
                    },

                    new Ticket
                    {
                        Title = "Ticket 1, Project 3",
                        Created = DateTime.Now,
                        Description = "This is a ticket description",
                        OwnerUserId = userManager.FindByEmail("DemoSubmitter@mailinator.com").Id,
                        ProjectId = 10030,
                        TicketTypeId = 100,
                        TicketPriorityId = 200,
                        TicketStatusId = 200,
                    }
                );
            #endregion

            #region Role Assignment Section 
            //Now that we have both a Role and a User , we need to associate the two
            var userId = userManager.FindByEmail("mmsamara@ncsu.edu").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("DemoSubmitter@mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            userId = userManager.FindByEmail("DemoProjectManager@mailinator.com").Id;
            userManager.AddToRole(userId, "ProjectManager");

            userId = userManager.FindByEmail("DemoDeveloper@mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            #endregion 
        }
    }
}
