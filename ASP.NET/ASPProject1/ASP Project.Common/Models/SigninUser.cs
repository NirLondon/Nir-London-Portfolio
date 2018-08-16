using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using FluentValidation.Attributes;
//using FluentValidation;
//using ASP_Project.DAL;

namespace ASP_Project.Common.Models
{
    //[Validator(typeof(SigninUserValidator))]
    public class SigninUser
    {
        [DisplayName("First Name:")]
        [Required(ErrorMessage = "Pealse enter your first name.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Put a name with a length between 2 to 50 characters.")]
        public string FirstName { get; set; }

        [DisplayName("Last Name:")]
        [Required(ErrorMessage = "Pealse enter your first name.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Put a name with a length between 2 to 50 characters.")]
        public string LastName { get; set; }

        [DisplayName("Username:")]
        [Required(ErrorMessage = "Enter a username.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Put a name with a length between 2 to 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your email adress.")]
        [DisplayName("Email Adress:"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your birh date.")]
        [DisplayName("Birth Date:"), DataType(DataType.Date)]
        [Column(TypeName = "DateTime2")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Enter a password.")]
        [MinLength(6, ErrorMessage = "Your password should contain at least 6 characters.")]
        [DisplayName("Password:"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm your password.")]
        [DisplayName("Confirm Password:"), DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password are not identical")]
        public string ConfirmPassword { get; set; }
    }

    //public class SigninUserValidator : AbstractValidator<SigninUser>
    //{
    //    public SigninUserValidator()
    //    {
    //        RuleFor(su => su.Username).Must(IsUsernameUnique).WithMessage("This username is taken. Chosse an other one.");
    //        RuleFor(su => su.Email).Must(IsEmailUnique).WithMessage("This email is already signed in.");
    //    }

    //    private bool IsEmailUnique(string email) => !DataAccessor.Exists(email, ExistingCheckOption.ByEmail);

    //    private bool IsUsernameUnique(string username) => !DataAccessor.Exists(username, ExistingCheckOption.ByUsername);
    //}
    // 
}