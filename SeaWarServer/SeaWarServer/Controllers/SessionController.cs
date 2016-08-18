using SeaWarServer.DTO;
using SeaWarServer.Models;
using System.Linq;
using System.Web.Http;

namespace SeaWarServer.Controllers
{
    public class SessionController : ApiController
    {
        DataBaseContext dbContext = new DataBaseContext();

        [HttpPost]
        public IHttpActionResult Create(SessionCreateDTO data)
        {
            if (dbContext.Users.FirstOrDefault(u => u.Id == data.HostId) != null)
            {
                Statics.BattleSessionList.Add(new BattleSession(data));
                return this.Ok(data);
            }
            else
            {
                return this.BadRequest(Messages.UserNotFound);
            }
        }
        [HttpGet]
        public IHttpActionResult List()
        {
            return this.Ok(Statics.BattleSessionList.Select(a => new { Id = a.Id, GameName = a.GameName, Host = a.Host.Id }).ToList());
        }

        [HttpPost]
        public IHttpActionResult Join(SessionJoinDTO data)
        {
            var tempSession = Statics.BattleSessionList.FirstOrDefault(s => s.Id == data.Id);
            if (tempSession == null)
            {
                return BadRequest(Messages.NotFound);
            }
            else
            {
                if (dbContext.Users.FirstOrDefault(u => u.Id == data.PlayerId) != null)
                {
                    tempSession.Player.Id = data.PlayerId;
                    return Ok(tempSession);
                }
                else
                {
                    return this.BadRequest(Messages.UserNotFound);
                }
            }
        }

        [HttpGet]
        public IHttpActionResult Info(string id)
        {
            var tempSession = Statics.BattleSessionList.FirstOrDefault(s => s.Id == id);
            if (tempSession == null)
                return BadRequest(Messages.UserNotFound);
            else
                return Ok(new { Id = tempSession.Id, State = tempSession.State });
        }

        [HttpPost]
        public IHttpActionResult SelectShips(ShipListDTO data)
        {
            var tempSession = Statics.BattleSessionList.FirstOrDefault(s => s.Id == data.SessionId);
            if (tempSession == null)
            {
                return BadRequest("Session not found");
            }
            else
            {
                if (tempSession.Host.Id == data.UserId)
                {
                    tempSession.Host.Ready = true;
                    //TODO: mb fix
                    foreach (var s in data.ShipList)
                    {
                        var tempShip = dbContext.Users.FirstOrDefault(u => u.Id == data.UserId).Ships.FirstOrDefault(sh=>sh.Id==s);
                        if(tempShip!=null)
                        {
                            tempSession.Host.ShipList.Add(tempShip);
                        }
                    }
                    return Ok(Messages.Success);
                }
                else if (tempSession.Player.Id == data.UserId)
                {
                    tempSession.Player.Ready = true;
                    //TODO: mb fix
                    foreach (var s in data.ShipList)
                    {
                        var tempShip = dbContext.Users.FirstOrDefault(u => u.Id == data.UserId).Ships.FirstOrDefault(sh => sh.Id == s);
                        if (tempShip != null)
                        {
                            tempSession.Player.ShipList.Add(tempShip);
                        }
                    }
                    return Ok(Messages.Success);
                }
                else
                {
                    return BadRequest("User not found");
                }
            }
        }

    }
}
