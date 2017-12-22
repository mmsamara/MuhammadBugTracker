using MuhammadBugTracker;
using MuhammadBugTracker.Helpers;
using MuhammadBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MuhammadBugTracker.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper roleHelper = new UserRolesHelper();
        private ProjectsHelper projectHelper = new ProjectsHelper();

        // GET: Admin
        public ActionResult ProjectAssign(string id)
        {
            var myProjectIds = projectHelper.ListUserProjects(id).Select(p => p.Id);
            ViewBag.UserId = id;
            ViewBag.AllProjects = new MultiSelectList(db.Projects, "Id", "Name", myProjectIds);
            return View();
        }

        [HttpPost]
        public ActionResult ProjectAssign(string userId, List<int> AllProjects)
        {
            //First use Project helper to remove this user from all Project
            foreach(var project in projectHelper.ListUserProjects(userId))
            {
                projectHelper.RemoveUserFromProject(userId, project.Id);
            }
            
            //Next add the user to the selected Projects (AllProject)
            if(AllProjects != null)
            {
                foreach(var projectId in AllProjects)
                {
                    projectHelper.AddUserToProject(userId, projectId);
                }
            }
            return RedirectToAction("UserIndex");
        }

        // GET: Admin
        [Authorize(Roles ="Admin")]
        public ActionResult RoleAssign(string id)
        {
            //Get a listing of project I am already assigned to
            ViewBag.UserId = id;
            var occupiedRole = roleHelper.ListUserRoles(id).FirstOrDefault();
            var mostRoles = db.Roles.Where(r => r.Name != "Admin");
            ViewBag.AllRoles = new SelectList(mostRoles, "Name", "Name", occupiedRole);

            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAssign(string UserId, string AllRoles)
        {
             if (String.IsNullOrEmpty(UserId))
             {
                return RedirectToAction("RoleAssign", new { id = UserId });
             }
             //"Spin" thru all roles currently occupied removing the user from them all
             foreach (var role in roleHelper.ListUserRoles(UserId))
             {
                roleHelper.RemoveUserFromRole(UserId, role);
             }

             if(!String.IsNullOrEmpty(AllRoles))
             {
                roleHelper.AddUserToRole(UserId, AllRoles);
             }

             //We will add code to assign the user to the selected role, prob gonna find some way to push in user id of the person 
             //I've selected & after that I'll have all the pieces I need to push into user roles helper
             return RedirectToAction("UserIndex");
          
        }

        public ActionResult UserIndex()
        {
            return View(db.Users.ToList());
        }

        public ActionResult ClearRoles(string userId)
        {
            var roles = roleHelper.ListUserRoles(userId);
            
            foreach (var role in roles)
            {
                roleHelper.RemoveUserFromRole(userId, role);
            }
            return RedirectToAction("UserIndex");
        }
        
        #region Old Code
        // GET: Admin/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Admin/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Admin/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Admin/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: Admin/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Admin/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Admin/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        #endregion
    }
}
