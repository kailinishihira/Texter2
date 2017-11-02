using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Texter.Models;

namespace Texter.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetMessages()
        {
            var allMessages = Message.GetMessages();
            return View(allMessages);                              
        }

        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message newMessage)
        {
            string[] groupText = newMessage.To.Split(',');
            for (int i = 0; i < groupText.Length; i ++)
            {
                newMessage.To = groupText[i];
                newMessage.Send();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]


        public IActionResult Error()
        {
            return View();
        }
    }
}
