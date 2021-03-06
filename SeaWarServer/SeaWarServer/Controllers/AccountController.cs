﻿using SeaWarServer.Models;
using System;
using System.Linq;
using System.Web.Http;
using SeaWarServer.DTO;
using System.Net;

namespace SeaWarServer.Controllers
{
    public class AccountController : ApiController
    {
        public DataBaseContext dbContext = new DataBaseContext();

        [HttpPost]
        public IHttpActionResult Register(LoginDTO data)
        {
            if (data.Name == "" || data.Password == "")
            {
                return this.Ok(Messages.NotEmpty);
            }
            User tempUser = new User(data.Name, data.Password);
            dbContext.Users.Add(tempUser);
            dbContext.SaveChanges();
            return this.Ok(tempUser.Id);
        }

        [HttpGet]
        public IHttpActionResult GetUser(string Id)
        {
            var tempUser = dbContext.Users.FirstOrDefault(user => user.Id == Id);
            return Ok(tempUser);
        }

        [HttpPost]
        public IHttpActionResult SetRole(SetRoleDTO data)
        {
            try
            {
                User tempUser = dbContext.Users.FirstOrDefault(user => user.Id == data.PlayerId);
                if (tempUser == null)
                {
                    return this.Ok(Messages.UserNotFound);
                }
                else
                {
                    var role = data.Role;
                    if (tempUser.Role != Models.User.RoleEnum.None)
                    {
                        return this.Ok(Messages.NotPos);
                    }
                    else
                    {
                        string result = tempUser.SetRole(data.Role);
                        dbContext.SaveChanges();
                        return this.Ok(result);
                    }
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        public IHttpActionResult Login(LoginDTO data)
        {
            var tempUser = dbContext.Users.FirstOrDefault(U => U.Name == data.Name && U.Password == data.Password);
            if (tempUser == null)
            {
                return this.Ok(Messages.BadData);
            }
            else
            {
                return Ok(tempUser.Id);
            }
        }

    }
}
