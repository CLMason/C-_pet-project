using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CsharpProject.Models
{
    public class Pet
    {
        public string PetId {get;set;}

        [Required(ErrorMessage="Animal's Name is required")]
        public string PetName {get;set;}

        [Required(ErrorMessage="Animal's breed is required")]
        public string PetBreed {get;set;}

        [Required(ErrorMessage="Animal age is required")]
        public int PetAge {get;set;}

        [Required(ErrorMessage="Description of Animal is required")]
        public string Description {get;set;}

        public byte [] Image {get;set;}



    }
}