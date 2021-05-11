﻿using System;
using System.Collections.Generic;

namespace SafeAccountsAPI.Models
{
    public class User
    {
        //public User() {
            
        //    this.RefreshTokens = new List<RefreshToken>();
        //    this.Accounts = new List<Account>();
        //}

        public int ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public int NumAccs { get; set; }
        public string Role { get; set; }
        public virtual List<Account> Accounts { get; set; }
        public virtual List<RefreshToken> RefreshTokens { get; set; }
        public virtual List<Folder> Folders { get; set; }
    }

    // this class exists so we can easily send the needed user data, but have more data server side
    public class ReturnableUser
    {
        public int ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public int NumAccs { get; set; }
        public string Role { get; set; }

        // constructor create a safe returnable user
        public ReturnableUser(User user)
        {
            ID = user.ID;
            First_Name = user.First_Name;
            Last_Name = user.Last_Name;
            Email = user.Email;
            NumAccs = user.NumAccs;
            Role = user.Role;
        }
    }

    public static class UserRoles
    {
        public static string User = "user";
        public static string Admin = "admin";
    }
}