using SeaWarServer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;

namespace SeaWarServer.Models
{
    public class BattleSession
    {
        private string playerId;
        private double timeLeft;
        private Timer timer;
        private GameState state;
        public string Id { get; set; }
        public string GameName { get; set; }
        public string HostId { get; set; }
        public string PlayerId
        {
            get
            {
                return playerId;
            }
            set
            {
                State = GameState.Started;
                playerId = value;
            }
        }
        public GameState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                GameStarted(this, null);
            }
        }
        public double TimeLeft
        {
            get
            {
                return TimeLeft;
            }
            set
            {
                timeLeft = value;
                if (timeLeft <= 0)
                {
                    TurnEnded(this, null);
                }
            }
        }


        public BattleSession(SessionCreateDTO data)
        {
            this.Id = Guid.NewGuid().ToString();
            this.HostId = data.HostId;
            this.GameName = data.GameName;
            this.State = GameState.NotStarted;
            this.timer = new Timer();
            this.GameStarted += BattleSessionOnGameStarted;
            this.timer.Elapsed += TimerOnElapsed;
            this.TurnEnded += BattleSessionOnTurnEnded;
        }

        private void BattleSessionOnTurnEnded(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            this.TimeLeft -= 1;
        }

        private void BattleSessionOnGameStarted(object sender, EventArgs e)
        {
            this.TimeLeft = 60;
            this.timer.Interval = 60;
            this.timer.Start();
        }

        private event EventHandler GameStarted;
        private event EventHandler TurnEnded;

        public enum GameState { NotStarted, Started }
    }
}