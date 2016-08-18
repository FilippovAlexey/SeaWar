using SeaWarServer.DTO;
using System;
using System.Timers;

namespace SeaWarServer.Models
{
    public class BattleSession
    {
        private PlayerInBattle playerId;
        private double timeLeft;
        private Timer timer;
        private GameState state;
        public string Id { get; set; }
        public string GameName { get; set; }
        public PlayerInBattle Host { get; set; }
        public PlayerInBattle Player
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
                return timeLeft;
            }
            private set
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
            this.Host = new PlayerInBattle() { Id = data.HostId };
            this.GameName = data.GameName;
            this.State = GameState.NotStarted;
            this.timer = new Timer();
            this.GameStarted += BattleSessionOnGameStarted;
            this.timer.Elapsed += TimerOnElapsed;
            this.TurnEnded += BattleSessionOnTurnEnded;
            this.Host.ReadyChanged += UserOnReadyChanged;
            this.Player.ReadyChanged += UserOnReadyChanged;
        }

        private void UserOnReadyChanged(object sender, EventArgs e)
        {
            if(Player.Ready&&Host.Ready)
            {
                BattleSessionOnTurnEnded(this, null);
                Player.Ready = false;
                Host.Ready = false;
            }
        }

        //TODO:
        private void BattleSessionOnTurnEnded(object sender, EventArgs e)
        {
            this.timer.Stop();

        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            this.TimeLeft -= 1;
        }

        private void BattleSessionOnGameStarted(object sender, EventArgs e)
        {
            this.TimeLeft = 60;
            this.timer.Interval = 1;
            this.timer.Start();
        }

        private event EventHandler GameStarted;
        private event EventHandler TurnEnded;

        public enum GameState { NotStarted, Started }
    }
}