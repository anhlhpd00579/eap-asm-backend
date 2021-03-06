﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Account
    {
        public Account()
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.Status = AccountStatus.Active;
        }
        [Key]
        [Required]
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public AccountStatus Status { get; set; }
        public GeneralInformation GeneralInformation { get; set; }
    }

    public enum AccountStatus
    {
        Active = 1,
        Deactive = 0
    }
}
