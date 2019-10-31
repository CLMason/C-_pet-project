using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CsharpProject.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}

        [Required(ErrorMessage="First Name is required")]
        public string FirstName {get;set;}

        [Required(ErrorMessage="Last Name is required")]
        public string LastName {get;set;}


        [Required(ErrorMessage="Email address is required")]
        [EmailAddress]
        public string Email{get;set;}

        [Required(ErrorMessage="You need a password to register")]
        [MinLength(8)]
        public string Password {get;set;}

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}

        public DateTime CreatedAt {get;set;}= DateTime.Now;
        public DateTime UpdatedAt {get;set;}= DateTime.Now;

        public bool IsAdmin {get;set;}

        public List<Pet> LikedPets {get;set;} //a user could have a list of pets that they like
        
        
        // //users are able to join you in going to see a movie so we need a join 
    }
}