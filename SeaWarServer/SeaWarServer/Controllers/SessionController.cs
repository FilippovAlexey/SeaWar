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
                BattleSession tempBS = new BattleSession(data);
                Statics.BattleSessionList.Add(tempBS);
                return this.Ok(tempBS.Id);
            }
            else
            {
                return this.Ok(Messages.UserNotFound);
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
                return this.Ok(Messages.SessionNotFound);
            }
            else
            {
                if (dbContext.Users.FirstOrDefault(u => u.Id == data.PlayerId) != null)
                {
                    tempSession.Player.Id = data.PlayerId;
                    return this.Ok(tempSession);
                }
                else
                {
                    return this.Ok(Messages.UserNotFound);
                }
            }
        }

        [HttpGet]
        public IHttpActionResult Info(string id)
        {
            var tempSession = Statics.BattleSessionList.FirstOrDefault(s => s.Id == id);
            if (tempSession == null)
                return this.Ok(Messages.SessionNotFound);
            else
            {
                return this.Ok(new { HostId = tempSession.Host.Id, State = tempSession.State });
            }

        }

        [HttpGet]
        public IHttpActionResult GetTimeLeft(string Id)
        {
            var tempSesion = Statics.BattleSessionList.FirstOrDefault(s => s.Id == Id);
            if (tempSesion == null)
            {
                return this.Ok(Messages.SessionNotFound);
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
                return this.Ok(Messages.SessionNotFound);
            }
            else
            {
                if (tempSession.Host.Id == data.UserId)
                {
                    return SetSetShipsData(tempSession.Host, data);
                }
                else if (tempSession.Player.Id == data.UserId)
                {
                    return SetSetShipsData(tempSession.Player, data);
                }
                else
                {
                    return this.Ok(Messages.UserNotFound);
                }
            }
        }

        [HttpPost]
        public IHttpActionResult EndTurn(TurnDataDTO data)
        {
            var tempSession = Statics.BattleSessionList.FirstOrDefault(s => s.Id == data.SessionId);
            if (tempSession == null)
            {
                return this.Ok(Messages.SessionNotFound);
            }
            else
            {
                if (tempSession.Host.Id == data.PlayerId)
                {
                    return SetTurnData(tempSession.Host, data, ShipInBattle.BattleOwner.Host);
                }
                else if (tempSession.Player.Id == data.PlayerId)
                {
                    return SetTurnData(tempSession.Player, data, ShipInBattle.BattleOwner.Player);
                }
                else
                {
                    return this.Ok(Messages.UserNotFound);
                }
            }
        }

        [HttpGet]
        public IHttpActionResult GetBttleData(string id)
        {
            var tempBattleSession = Statics.BattleSessionList.FirstOrDefault(bs => bs.Id == id);
            if (tempBattleSession == null)
            {
                return this.Ok(Messages.SessionNotFound);
            }
            else
            {
                return this.Ok(tempBattleSession);
            }
        }

        private IHttpActionResult SetSetShipsData(PlayerInBattle player, ShipListDTO data)
        {
            if (player.Ready == true)
            {
                return this.Ok(Messages.AlreadyTurns);
            }
            else
            {
                player.Ready = true;
                //TODO: mb fix
                for (int i = 0, j = 0; i < data.ShipList.Count || j < 5; i++, j++)
                {
                    var tempShip = dbContext.Users.FirstOrDefault(u => u.Id == data.UserId).Ships.FirstOrDefault(sh => sh.Id == data.ShipList[i]);
                    if (tempShip != null)
                    {
                        player.ShipList.Add(tempShip as ShipInBattle);
                    }
                }
                return Ok(player.ShipList);
            }
        }
        private IHttpActionResult SetTurnData(PlayerInBattle player, TurnDataDTO data, ShipInBattle.BattleOwner owner)
        {
            if (player.Ready == true)
            {
                return this.Ok(Messages.AlreadyTurns);
            }
            else
            {
                player.Ready = true;
                for (int i = 0, j = 0; i < data.ShipList.Count || j < 5; i++, j++)
                {
                    var tempShip = player.ShipList.FirstOrDefault(sh => sh.Id == data.ShipList[i].Id);
                    if (tempShip != null)
                    {
                        tempShip.TargetPosition = data.ShipList[i].TargetPosition;
                        tempShip.Action = data.ShipList[i].Action;
                        tempShip.Owner = owner;
                    }
                }
                return this.Ok(Messages.Success);
            }
        }
    }
}
