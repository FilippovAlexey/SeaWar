using SeaWarServer.DTO;
using SeaWarServer.Models;
using System.Collections.Generic;
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
            {
                if (tempSession.State == BattleSession.GameState.NotStarted)
                {
                    return Ok(new { HostId = tempSession.Host.Id, State = tempSession.State });
                }
                else
                {
                    return Ok(new { });
                }
            }

        }

        [HttpGet]
        public IHttpActionResult GetTimeLeft(string Id)
        {
            var tempSesion = Statics.BattleSessionList.FirstOrDefault(s => s.Id == Id);
            if (tempSesion == null)
            {
                return BadRequest("Session not found");
            }
            else
            {
                return Ok(tempSesion.TimeLeft);
            }
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
                    if (tempSession.Host.Ready == true)
                    {
                        return this.BadRequest();
                    }
                    else
                    {
                        tempSession.Host.Ready = true;
                        //TODO: mb fix
                        for (int i = 0, j = 0; i < data.ShipList.Count || j < 5; i++, j++)
                        {
                            var tempShip = dbContext.Users.FirstOrDefault(u => u.Id == data.UserId).Ships.FirstOrDefault(sh => sh.Id == data.ShipList[i]);
                            if (tempShip != null)
                            {
                                tempSession.Host.ShipList.Add(tempShip as ShipInBattle);
                            }
                        }
                        return Ok(tempSession.Host.ShipList);
                    }
                }
                else if (tempSession.Player.Id == data.UserId)
                {
                    tempSession.Player.Ready = true;
                    //TODO: mb fix
                    for (int i = 0, j = 0; i < data.ShipList.Count || j < 5; i++, j++)
                    {
                        var tempShip = dbContext.Users.FirstOrDefault(u => u.Id == data.UserId).Ships.FirstOrDefault(sh => sh.Id == data.ShipList[i]);
                        if (tempShip != null)
                        {
                            tempSession.Player.ShipList.Add(tempShip as ShipInBattle);
                        }
                    }
                    return Ok(tempSession.Player.ShipList);
                }
                else
                {
                    return BadRequest(Messages.UserNotFound);
                }
            }
        }

        [HttpPost]
        public IHttpActionResult EndTurn(TurnDataDTO data)
        {
            var tempSession = Statics.BattleSessionList.FirstOrDefault(s => s.Id == data.SessionId);
            if (tempSession == null)
            {
                return BadRequest("Session not found");
            }
            else
            {
                if (tempSession.Host.Id == data.PlayerId)
                {

                }
                else if(tempSession.Player.Id==data.PlayerId)
                {

                }
                else
                {
                    return BadRequest(Messages.UserNotFound);
                }
            }


    }
}
