using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebsiteShopQuanAo.Models
{
    public class TaiKhoanValid
    {
        [Required(ErrorMessage="Enter username")]
        public string UserName { get; set; }

        [RegularExpression("^(?=.*[0-9])(?=.*[a-z]).{8,20}$",
        ErrorMessage = "Your password must contain at least 1 numberic and non numberic and a length of at least 8 characters and a maximum of 20 characters!")]
        [Required(ErrorMessage="Enter Password")]
        public string Pass { get; set; }

        
        [Required(ErrorMessage = "Comfirm your password")]
        public string RePass { get; set; }

        [Required(ErrorMessage = "Enter your name")]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Enter your date of birth")]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Enter your address")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Enter your email")]
        [EmailAddress(ErrorMessage = "You have entered an invalid email address!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your phone number")]
        [RegularExpression("(84|0[3|5|7|8|9])+([0-9]{8})", ErrorMessage = "You have entered an invalid phone number!")]
        public string SDT { get; set; }
    }
}