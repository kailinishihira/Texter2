using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Texter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Texter.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		public ContactsController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
		{
			_userManager = userManager;
			_db = db;
		}

		public IActionResult Index()
		{
			var contactList = _db.Contacts.ToList();
			return View(contactList);
		}

		public IActionResult Details(int contactId)
		{
			var thisContact = _db.Contacts
							.FirstOrDefault(x => x.ContactId == contactId);
            return View(thisContact);
		}

        [HttpPost]
        public IActionResult NewMessage(int contactId)
        {
            Message newMessage = new Message(Request.Form["sendTo"], Request.Form["sentFrom"], Request.Form["messageBody"]);
            newMessage.Send();
            return RedirectToAction("Details", new{id=contactId});
        }

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Contact contact)
		{
			if (ModelState.IsValid)
			{
                _db.Contacts.Add(contact);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			else
			{
				return View("Error");
			}

		}

		public IActionResult Edit(int contactId)
		{
			var thisContact = _db.Contacts.FirstOrDefault(x => x.ContactId == contactId);
			return View(thisContact);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Contact contact)
		{
			if (ModelState.IsValid)
			{
                _db.Entry(contact).State = EntityState.Modified;
                _db.SaveChanges();
				return RedirectToAction("Details", new { contactId = contact.ContactId });
			}
			else
			{
				return View("Error");
			}

		}

		public IActionResult Delete(int contactId)
		{
			var thisContact = _db.Contacts.FirstOrDefault(x => x.ContactId == contactId);
			return View(thisContact);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmation(int contactId)
		{
			try
			{
				var thisContact = _db.Contacts.FirstOrDefault(x => x.ContactId == contactId);
				_db.Remove(thisContact);
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

    }
}
