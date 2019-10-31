using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CsharpProject.Models
{
    public class Message
    {
        public int MessageId {get;set;}

        public int UserId{get;set;}

        public User User {get;set;}

        public int PetId{get;set;}

        public Pet Pet {get;set;}

        public string MessageBody{get;set;}

        public DateTime CreatedAt {get;set;}= DateTime.Now;
        public DateTime UpdatedAt {get;set;}= DateTime.Now;

    }
}