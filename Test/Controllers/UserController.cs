using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    public class UserController : Controller
    {        
        public ActionResult List()
        {
            using (var db = new UserModel())
            {
                var users = db.UserProfiles.ToList();

                ViewBag.Cities = db.UserProfiles.GroupBy(u => u.City).Select(g => g.Key).ToList();

                if (!users.Any())
                {
                    db.UserProfiles.Add(new UserProfile { Name = "Саша", City = "Київ", Age = 18 });
                    db.UserProfiles.Add(new UserProfile { Name = "Аня", City = "Львів", Age = 25 });
                    db.UserProfiles.Add(new UserProfile { Name = "Віка", City = "Харків", Age = 22 });
                    db.UserProfiles.Add(new UserProfile { Name = "Ігор", City = "Київ", Age = 40 });
                    db.UserProfiles.Add(new UserProfile { Name = "Ваня", City = "Львів", Age = 16 });

                    db.SaveChanges();
                }

                return View(users); 
            }
        }

        public ActionResult FilterAndSort(string city = "", string ageOrder = "")
        {
            using (var db = new UserModel())
            {
                var users = db.UserProfiles.ToList();

                ViewBag.Cities = users.GroupBy(u => u.City).Select(g => g.Key).ToList();

                if (!string.IsNullOrWhiteSpace(city))
                {
                    if (ageOrder == "desc")
                    {
                        return View("List", users.Where(c => c.City == city).OrderByDescending(u => u.Age).ToList());
                    }
                    else if (ageOrder == "asc")
                    {
                        return View("List", users.Where(c => c.City == city).OrderBy(u => u.Age).ToList());
                    }

                    return View("List", users.Where(c => c.City == city).ToList());
                }
                else
                {
                    if (ageOrder == "desc")
                    {
                        return View("List", users.OrderByDescending(u => u.Age).ToList());
                    }
                    else if (ageOrder == "asc")
                    {
                        return View("List", users.OrderBy(u => u.Age).ToList());
                    }

                    return View("List", users);
                }
            }
        }
    }
}
