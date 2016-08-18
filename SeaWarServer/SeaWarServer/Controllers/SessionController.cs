using SeaWarServer.DTO;
using SeaWarServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SeaWarServer.Controllers
{
    //TODO:naher perepisat' vsyo^&
    public class SessionController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Create(SessionCreateDTO data)
        {
            Statics.BattleSessionList.Add(new BattleSession(data));
            return this.Ok(data);
        }
        [HttpGet]
        public IHttpActionResult List()
        {
            return this.Ok(Statics.BattleSessionList.Select(a => new { Id = a.Id, GameName = a.GameName, Host = a.HostId }).ToList());
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
                tempSession.PlayerId = data.PlayerId;
                return Ok(tempSession);
            }
        }

        [HttpGet]
        public IHttpActionResult Info(string id)
        {
            var tempSession = Statics.BattleSessionList.FirstOrDefault(s => s.Id == id);
            if (tempSession == null)
                return BadRequest(Messages.UserNotFound);
            else
                return Ok(new { Id=tempSession.Id, PlayerId=tempSession.PlayerId});
        }

    }
}
