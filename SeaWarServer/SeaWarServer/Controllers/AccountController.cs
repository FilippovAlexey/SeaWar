using SeaWarServer.Models;
using System;
using System.Linq;
using System.Web.Http;
using SeaWarServer.DTO;

namespace SeaWarServer.Controllers
{
    public class AccountController : ApiController
    {
        public DataBaseContext dbContext = new DataBaseContext();
        
        [HttpPost]
        public IHttpActionResult Register(LoginDTO data)
        {
            try
            {
                User tempUser = new User(data.Name, data.Password);
                dbContext.Users.Add(tempUser);
                dbContext.SaveChanges();
                return Ok(tempUser.Id);
            }
            catch (Exception)
            {
                return  BadRequest(Messages.WrongRequest);
            }
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
                    return NotFound();
                }
                else
                {
                    int role = data.Role;
                    if (tempUser.Role != 0)
                    {
                        return BadRequest("Not possible");
                    }
                    else
                    {
                        IHttpActionResult result; 
                        switch (role)
                        {
                            case 1:
                                tempUser.Role = Models.User.RoleEnum.Pirate;
                                var tempShip = Statics.ShipList.First(s => s.Name == "Log");
                                tempShip.Id = Guid.NewGuid().ToString();
                                tempUser.Ships.Add(tempShip);
                                result = Ok(Messages.Success);
                                break;
                            case 2:

                                result = BadRequest("not yet");
                                break;
                            default:
                                result = BadRequest("This role is missing");
                                break;
                        }
                        dbContext.SaveChanges();
                        return result;
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
            if(tempUser==null)
            {
                return NotFound();
            }
            else
            {
                return Ok(tempUser.Id);
            }
        }

    }
}
