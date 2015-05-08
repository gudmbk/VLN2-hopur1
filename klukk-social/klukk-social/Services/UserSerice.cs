﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using klukk_social.Models;
using Microsoft.AspNet.Identity;

namespace klukk_social.Services
{
    public class UserSerice
    {
        public User FindById(string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var user = (from u in dbContext.Users
                    where u.Id == userId
                    select u).FirstOrDefault();
                return user;
            }
        }

        public List<User> Search(string prefix)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var user = (from u in dbContext.Users
                            where u.FirstName.Contains(prefix)
                            orderby u.FirstName
                            select u).ToList();
                return user;
            }
        }
    }
}