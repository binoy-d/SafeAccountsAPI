﻿using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using SafeAccountsAPI.Helpers;
using SafeAccountsAPI.Models;

namespace SafeAccountsAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(APIContext context, IConfiguration config)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (!context.Users.Any())
            {
                // set key and IV
                string[] keyAndIV = new string[] { config.GetValue<string>("UserEncryptionKey"), config.GetValue<string>("UserEncryptionIV") };

                // add base users if data base not populated
                User[] users = new User[]
                {
                    new User {
                        First_Name=HelperMethods.EncryptStringToBytes_Aes("John", keyAndIV),
                        Last_Name=HelperMethods.EncryptStringToBytes_Aes("Doe", keyAndIV),
                        Email=HelperMethods.EncryptStringToBytes_Aes("john@doe.com", keyAndIV),
                        Password=HelperMethods.ConcatenatedSaltAndSaltedHash("useless"),
                        Role=HelperMethods.EncryptStringToBytes_Aes(UserRoles.User, keyAndIV),
                        EmailVerified=true
                    },
                    new User {
                        First_Name=HelperMethods.EncryptStringToBytes_Aes("Edwin", keyAndIV),
                        Last_Name=HelperMethods.EncryptStringToBytes_Aes("May", keyAndIV),
                        Email=HelperMethods.EncryptStringToBytes_Aes("edwin@may.com", keyAndIV),
                        Password=HelperMethods.ConcatenatedSaltAndSaltedHash("useless"),
                        Role=HelperMethods.EncryptStringToBytes_Aes(UserRoles.User, keyAndIV),
                        EmailVerified=true
                    },
                    new User {
                        First_Name=HelperMethods.EncryptStringToBytes_Aes("Lucy", keyAndIV),
                        Last_Name=HelperMethods.EncryptStringToBytes_Aes("Vale", keyAndIV),
                        Email=HelperMethods.EncryptStringToBytes_Aes("lucy@vale.com", keyAndIV),
                        Password=HelperMethods.ConcatenatedSaltAndSaltedHash("useless"),
                        Role=HelperMethods.EncryptStringToBytes_Aes(UserRoles.User, keyAndIV),
                        EmailVerified=true
                    },
                    new User {
                        First_Name=HelperMethods.EncryptStringToBytes_Aes("Pam", keyAndIV),
                        Last_Name=HelperMethods.EncryptStringToBytes_Aes("Willis", keyAndIV),
                        Email=HelperMethods.EncryptStringToBytes_Aes("pam@willis.com", keyAndIV),
                        Password=HelperMethods.ConcatenatedSaltAndSaltedHash("useless"),
                        Role=HelperMethods.EncryptStringToBytes_Aes(UserRoles.User, keyAndIV),
                        EmailVerified=true
                    },
                    new User {
                        First_Name=HelperMethods.EncryptStringToBytes_Aes("Game", keyAndIV),
                        Last_Name=HelperMethods.EncryptStringToBytes_Aes("Stonk", keyAndIV),
                        Email=HelperMethods.EncryptStringToBytes_Aes("game@stonk.com", keyAndIV),
                        Password=HelperMethods.ConcatenatedSaltAndSaltedHash("useless"),
                        Role=HelperMethods.EncryptStringToBytes_Aes(UserRoles.User, keyAndIV),
                        EmailVerified=true
                    }
                };

                foreach (User person in users)
                    context.Users.Add(person); // add each user to the table

                context.SaveChanges(); // execute changes

                // create a key and iv for these base users
                foreach (User person in context.Users)
                    HelperMethods.CreateUserKeyandIV(person.ID);
            }

            if (!context.Accounts.Any())
            {
                // add 2 base accounts to each user for testing
                Account[] accs = new Account[]
                {
                    new Account {
                        UserID=1,
                        Title=HelperMethods.EncryptStringToBytes_Aes("gmail", HelperMethods.GetUserKeyAndIV(1)),
                        Login=HelperMethods.EncryptStringToBytes_Aes("johndoe", HelperMethods.GetUserKeyAndIV(1)),
                        Password=HelperMethods.EncryptStringToBytes_Aes("useless", HelperMethods.GetUserKeyAndIV(1)),
                        Url=HelperMethods.EncryptStringToBytes_Aes("testurl.com", HelperMethods.GetUserKeyAndIV(1)),
                        Description=HelperMethods.EncryptStringToBytes_Aes("description...", HelperMethods.GetUserKeyAndIV(1)),
                        LastModified=HelperMethods.EncryptStringToBytes_Aes(DateTime.Now.ToString(), HelperMethods.GetUserKeyAndIV(1)),
                        IsFavorite=false
                    },
                    new Account {
                        UserID=1,
                        Title=HelperMethods.EncryptStringToBytes_Aes("yahoo", HelperMethods.GetUserKeyAndIV(1)),
                        Login=HelperMethods.EncryptStringToBytes_Aes("johndoe", HelperMethods.GetUserKeyAndIV(1)),
                        Password=HelperMethods.EncryptStringToBytes_Aes("useless", HelperMethods.GetUserKeyAndIV(1)),
                        Url=HelperMethods.EncryptStringToBytes_Aes("testurl.com", HelperMethods.GetUserKeyAndIV(1)),
                        Description=HelperMethods.EncryptStringToBytes_Aes("description...", HelperMethods.GetUserKeyAndIV(1)),
                        LastModified=HelperMethods.EncryptStringToBytes_Aes(DateTime.Now.ToString(), HelperMethods.GetUserKeyAndIV(1)),
                        IsFavorite=false
                    },
                    new Account {
                        UserID=2,
                        Title=HelperMethods.EncryptStringToBytes_Aes("paypal", HelperMethods.GetUserKeyAndIV(2)),
                        Login=HelperMethods.EncryptStringToBytes_Aes("edwinmay", HelperMethods.GetUserKeyAndIV(2)),
                        Password=HelperMethods.EncryptStringToBytes_Aes("useless", HelperMethods.GetUserKeyAndIV(2)),
                        Url=HelperMethods.EncryptStringToBytes_Aes("testurl.com", HelperMethods.GetUserKeyAndIV(2)),
                        Description=HelperMethods.EncryptStringToBytes_Aes("description...", HelperMethods.GetUserKeyAndIV(2)),
                        LastModified=HelperMethods.EncryptStringToBytes_Aes(DateTime.Now.ToString(), HelperMethods.GetUserKeyAndIV(2)),
                        IsFavorite=false
                    },
                    new Account {
                        UserID=2,
                        Title=HelperMethods.EncryptStringToBytes_Aes("zoom", HelperMethods.GetUserKeyAndIV(2)),
                        Login=HelperMethods.EncryptStringToBytes_Aes("edwinmay", HelperMethods.GetUserKeyAndIV(2)),
                        Password=HelperMethods.EncryptStringToBytes_Aes("useless", HelperMethods.GetUserKeyAndIV(2)),
                        Url=HelperMethods.EncryptStringToBytes_Aes("testurl.com", HelperMethods.GetUserKeyAndIV(2)),
                        Description=HelperMethods.EncryptStringToBytes_Aes("description...", HelperMethods.GetUserKeyAndIV(2)),
                        LastModified=HelperMethods.EncryptStringToBytes_Aes(DateTime.Now.ToString(), HelperMethods.GetUserKeyAndIV(2)),
                        IsFavorite=false
                    },
                    new Account {
                        UserID=3,
                        Title=HelperMethods.EncryptStringToBytes_Aes("chase", HelperMethods.GetUserKeyAndIV(3)),
                        Login=HelperMethods.EncryptStringToBytes_Aes("lucyvale", HelperMethods.GetUserKeyAndIV(3)),
                        Password=HelperMethods.EncryptStringToBytes_Aes("useless", HelperMethods.GetUserKeyAndIV(3)),
                        Url=HelperMethods.EncryptStringToBytes_Aes("testurl.com", HelperMethods.GetUserKeyAndIV(3)),
                        Description=HelperMethods.EncryptStringToBytes_Aes("description...", HelperMethods.GetUserKeyAndIV(3)),
                        LastModified=HelperMethods.EncryptStringToBytes_Aes(DateTime.Now.ToString(), HelperMethods.GetUserKeyAndIV(3)),
                        IsFavorite=false
                    },
                    new Account {
                        UserID=3,
                        Title=HelperMethods.EncryptStringToBytes_Aes("netflix", HelperMethods.GetUserKeyAndIV(3)),
                        Login=HelperMethods.EncryptStringToBytes_Aes("lucyvale", HelperMethods.GetUserKeyAndIV(3)),
                        Password=HelperMethods.EncryptStringToBytes_Aes("useless", HelperMethods.GetUserKeyAndIV(3)),
                        Url=HelperMethods.EncryptStringToBytes_Aes("testurl.com", HelperMethods.GetUserKeyAndIV(3)),
                        Description=HelperMethods.EncryptStringToBytes_Aes("description...", HelperMethods.GetUserKeyAndIV(3)),
                        LastModified=HelperMethods.EncryptStringToBytes_Aes(DateTime.Now.ToString(), HelperMethods.GetUserKeyAndIV(3)),
                        IsFavorite=false
                    },
                    new Account {
                        UserID=4,
                        Title=HelperMethods.EncryptStringToBytes_Aes("hulu", HelperMethods.GetUserKeyAndIV(4)),
                        Login=HelperMethods.EncryptStringToBytes_Aes("pamwillis", HelperMethods.GetUserKeyAndIV(4)),
                        Password=HelperMethods.EncryptStringToBytes_Aes("useless", HelperMethods.GetUserKeyAndIV(4)),
                        Url=HelperMethods.EncryptStringToBytes_Aes("testurl.com", HelperMethods.GetUserKeyAndIV(4)),
                        Description=HelperMethods.EncryptStringToBytes_Aes("description...", HelperMethods.GetUserKeyAndIV(4)),
                        LastModified=HelperMethods.EncryptStringToBytes_Aes(DateTime.Now.ToString(), HelperMethods.GetUserKeyAndIV(4)),
                        IsFavorite=false
                    },
                    new Account {
                        UserID=4,
                        Title=HelperMethods.EncryptStringToBytes_Aes("amazon", HelperMethods.GetUserKeyAndIV(4)),
                        Login=HelperMethods.EncryptStringToBytes_Aes("pamwillis", HelperMethods.GetUserKeyAndIV(4)),
                        Password=HelperMethods.EncryptStringToBytes_Aes("useless", HelperMethods.GetUserKeyAndIV(4)),
                        Url=HelperMethods.EncryptStringToBytes_Aes("testurl.com", HelperMethods.GetUserKeyAndIV(4)),
                        Description=HelperMethods.EncryptStringToBytes_Aes("description...", HelperMethods.GetUserKeyAndIV(4)),
                        LastModified=HelperMethods.EncryptStringToBytes_Aes(DateTime.Now.ToString(), HelperMethods.GetUserKeyAndIV(4)),
                        IsFavorite=false
                    },
                    new Account {
                        UserID=5,
                        Title=HelperMethods.EncryptStringToBytes_Aes("spotify", HelperMethods.GetUserKeyAndIV(5)),
                        Login=HelperMethods.EncryptStringToBytes_Aes("gamestonk", HelperMethods.GetUserKeyAndIV(5)),
                        Password=HelperMethods.EncryptStringToBytes_Aes("useless", HelperMethods.GetUserKeyAndIV(5)),
                        Url=HelperMethods.EncryptStringToBytes_Aes("testurl.com", HelperMethods.GetUserKeyAndIV(5)),
                        Description=HelperMethods.EncryptStringToBytes_Aes("description...", HelperMethods.GetUserKeyAndIV(5)),
                        LastModified=HelperMethods.EncryptStringToBytes_Aes(DateTime.Now.ToString(), HelperMethods.GetUserKeyAndIV(5)),
                        IsFavorite=false
                    },
                    new Account {
                        UserID=5,
                        Title=HelperMethods.EncryptStringToBytes_Aes("bestbuy", HelperMethods.GetUserKeyAndIV(5)),
                        Login=HelperMethods.EncryptStringToBytes_Aes("gamestonk", HelperMethods.GetUserKeyAndIV(5)),
                        Password=HelperMethods.EncryptStringToBytes_Aes("useless", HelperMethods.GetUserKeyAndIV(5)),
                        Url=HelperMethods.EncryptStringToBytes_Aes("testurl.com", HelperMethods.GetUserKeyAndIV(5)),
                        Description=HelperMethods.EncryptStringToBytes_Aes("description...", HelperMethods.GetUserKeyAndIV(5)),
                        LastModified=HelperMethods.EncryptStringToBytes_Aes(DateTime.Now.ToString(), HelperMethods.GetUserKeyAndIV(5)),
                        IsFavorite=false
                    },
                };

                foreach (Account acc in accs) { context.Accounts.Add(acc); } // add each account to the table
                context.SaveChanges(); // execute changes
            }

            if (!context.Folders.Any())
            {
                // add base folders
                Folder[] base_folds = new Folder[]
                {
                    new Folder { UserID=1, FolderName=HelperMethods.EncryptStringToBytes_Aes("Folder", HelperMethods.GetUserKeyAndIV(1)), HasChild =true },
                    new Folder { UserID=2, FolderName=HelperMethods.EncryptStringToBytes_Aes("Folder", HelperMethods.GetUserKeyAndIV(2)), HasChild=true },
                    new Folder { UserID=3, FolderName=HelperMethods.EncryptStringToBytes_Aes("Folder", HelperMethods.GetUserKeyAndIV(3)), HasChild=true },
                    new Folder { UserID=4, FolderName=HelperMethods.EncryptStringToBytes_Aes("Folder", HelperMethods.GetUserKeyAndIV(4)), HasChild=true },
                    new Folder { UserID=5, FolderName=HelperMethods.EncryptStringToBytes_Aes("Folder", HelperMethods.GetUserKeyAndIV(5)), HasChild=true }
                };

                Folder[] sub_folds = new Folder[]
                {
                    new Folder { UserID=1, FolderName=HelperMethods.EncryptStringToBytes_Aes("Sub-Folder", HelperMethods.GetUserKeyAndIV(1)), HasChild=false, ParentID=5 },
                    new Folder { UserID=2, FolderName=HelperMethods.EncryptStringToBytes_Aes("Sub-Folder", HelperMethods.GetUserKeyAndIV(2)), HasChild=false, ParentID=4 },
                    new Folder { UserID=3, FolderName=HelperMethods.EncryptStringToBytes_Aes("Sub-Folder", HelperMethods.GetUserKeyAndIV(3)), HasChild=false, ParentID=3 },
                    new Folder { UserID=4, FolderName=HelperMethods.EncryptStringToBytes_Aes("Sub-Folder", HelperMethods.GetUserKeyAndIV(4)), HasChild=false, ParentID=2 },
                    new Folder { UserID=5, FolderName=HelperMethods.EncryptStringToBytes_Aes("Sub-Folder", HelperMethods.GetUserKeyAndIV(5)), HasChild=false, ParentID=1 }
                };

                foreach (Folder fold in base_folds) { context.Folders.Add(fold); } // add each account to the table
                context.SaveChanges();
                foreach (Folder fold in sub_folds) { context.Folders.Add(fold); } // add each account to the table
                context.SaveChanges(); // execute changes
            }
        }
    }
}
