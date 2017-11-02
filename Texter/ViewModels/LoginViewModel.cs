
using System.ComponentModel.DataAnnotations;
using Texter.Models;

namespace Texter.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}