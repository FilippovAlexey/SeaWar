using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SeaWarServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SeaWarServer.DTO;

namespace SeaWarServer.Controllers
{
    public class AccountController : ApiController
    {
        public DataBaseContext dbContext = new DataBaseContext();
        
        [HttpPost]
        public string Register(LoginDTO data)
        {
            try
            {
                User tempUser = new User(data.Name, data.Password);
                dbContext.Users.Add(tempUser);
                dbContext.SaveChanges();
                return tempUser.Id;
            }
            catch (Exception)
            {
                return Messages.WrongRequest;
            }
        }

        [HttpGet]
        public User GetUser(string Id)
        {
            var tempUser = dbContext.Users.FirstOrDefault(user => user.Id == Id);
            return tempUser;
        }

        [HttpPost]
        public string SetRole(SetRoleDTO data)
        {
            try
            {
                User tempUser = dbContext.Users.FirstOrDefault(user => user.Id == data.PlayerId);
                if (tempUser == null)
                {
                    return Messages.UserNotFound;
                }
                else
                {
                    int role = data.Role;
                    if (tempUser.Role != 0)
                    {
                        return "Not possible";
                    }
                    else
                    {
                        string result = "";
                        switch (role)
                        {
                            case 1:
                                tempUser.Role = Models.User.RoleEnum.Pirate;
                                var tempShip = Statics.ShipList.First(s => s.Name == "Log");
                                tempShip.Id = Guid.NewGuid().ToString();
                                tempUser.Ships.Add(tempShip);
                                result = Messages.Success;
                                break;
                            case 2:

                                result = "not yet";
                                break;
                            default:
                                result = "This role is missing";
                                break;
                        }
                        dbContext.SaveChanges();
                        return result;
                    }
                }
            }
            catch (Exception e)
            {
                return Messages.WrongRequest;
            }
        }

        [HttpPost]
        public string Login(LoginDTO data)
        {
            var tempUser = dbContext.Users.FirstOrDefault(U => U.Name == data.Name && U.Password == data.Password);
            return tempUser?.Id ?? Messages.UserNotFound;
        }

    }
}
