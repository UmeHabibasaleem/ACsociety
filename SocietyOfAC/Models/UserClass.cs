using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace SocietyOfAC.Models
{
    public class UserClass
    {
        [Key]
        [Required(ErrorMessage = "Please Enter Registration Number")]
        [Display(Name = "Registration Number: ")]
        public string RegistrationNumber { get; set; }

        [Required(ErrorMessage = "Enter username!")]
        [Display(Name = "Enter UserName:")]
        [StringLength(maximumLength: 15, MinimumLength = 3, ErrorMessage = "UserName length must be maximum 15 & min 3")]
        public string Name { get; set; }
        [Required(ErrorMessage = "please Enter the Email address!")]
        [Display(Name = "Email Id:")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "please Enter the Password!")]
        [Display(Name = "Password:")]
        public string Password { get; set; }
        [Required(ErrorMessage = "please Enter the  Conform Password!")]
        [Display(Name = "Conform Password:")]
        [Compare("Password")]
        /*  [CompareAttribute("Password",ErrorMessage ="Password does not match")] */
        public string Conform_password { get; set; }
        [Required(ErrorMessage = "Upload Profile Image")]
        [Display(Name = "Profile Image:")]
        public string ImagePath { get; set; }

        /*  public HttpPostedFileBase uploadfile { get; set; }*/
    }
}