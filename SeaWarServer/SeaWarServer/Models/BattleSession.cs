﻿using SeaWarServer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace SeaWarServer.Models
{
    public class BattleSession
    {
        private PlayerInBattle player;
        private int timeLeft;
        private Timer timer;
        private GameState state;
        private int roundTime = 60;
        private Timer removeTimer;
        public bool shouldRemove;
        public string Id { get; set; }
        public string GameName { get; set; }
        public PlayerInBattle Host { get; set; }
        public List<TurnData> DataOfTurn { get; set; }
        public string WinnerId { get; set; }
        public int TurnNumber { get; set; }
        public PlayerInBattle Player
        {
            get
            {
                return player;
            }
            set
            {
                player = value;
                this.Player.ReadyChanged += UserOnReadyChanged;
                if (player.Id != "" && player.Id != null)
                {
                    State = GameState.Started;
                }
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
                if (state == GameState.Started)
                {
                    if(GameStarted!=null)
                    GameStarted(this, null);
                }
            }
        }
        public int TimeLeft
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
            this.Host = new PlayerInBattle(data.HostId);
            this.GameName = data.GameName;
            this.State = GameState.NotStarted;
            this.TurnNumber = 0;
            this.WinnerId = "";
            this.timer = new Timer();
            this.Player = new PlayerInBattle("");
            this.shouldRemove = true;
            this.removeTimer = new Timer();
            this.removeTimer.Interval = 90000;
            this.removeTimer.Elapsed += RemoveTimerOnElapsed;
            this.removeTimer.Start();
            this.DataOfTurn = new List<TurnData>();
            this.GameStarted += BattleSessionOnGameStarted;
            this.timer.Elapsed += TimerOnElapsed;
            this.TurnEnded += BattleSessionOnTurnEnded;
            this.Host.ReadyChanged += UserOnReadyChanged;
        }

        private void RemoveTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            if(this.shouldRemove)
            {
                Statics.BattleSessionList.Remove(this);
            }
            else
            {
                this.shouldRemove = true;
            }
        }

        private void UserOnReadyChanged(object sender, EventArgs e)
        {
            if (Player.Ready && Host.Ready)
            {
                BattleSessionOnTurnEnded(this, null);
            }
        }

        //TODO:
        private void BattleSessionOnTurnEnded(object sender, EventArgs e)
        {
            this.TurnNumber++;
            this.timer.Stop();
            if (this.State == GameState.Started)
            {

                if (!this.Host.Ready)
                {
                    this.Host.AutoAddShips();
                }
                if (!this.Player.Ready)
                {
                    this.Player.AutoAddShips();
                }
                this.Player.Ready = false;
                this.Host.Ready = false;
                this.State = GameState.ShipsSelected;
                this.timer.Interval = this.roundTime;
            }
            else if (this.State == GameState.ShipsSelected)
            {
                DataOfTurn.Clear();
                List<ShipInBattle> battleQuiue = new List<ShipInBattle>();
                if (this.Host.ShipList.Sum(s => s.Health) <= 0)
                {
                    EndGame(this.Player);
                }
                else if (this.Player.ShipList.Sum(s => s.Health) <= 0)
                {
                    EndGame(this.Host);
                }
                else
                {
                    battleQuiue.AddRange(this.Host.ShipList);
                    battleQuiue.AddRange(this.Player.ShipList);
                    battleQuiue.OrderByDescending(a=>a.Action).ThenByDescending(s=>s.Speed);
                    foreach (var ship in battleQuiue)
                    {
                        switch (ship.Action)
                        {
                            case ShipInBattle.ShipAction.Nothing:
                                {
                                    break;
                                }
                            case ShipInBattle.ShipAction.Attack:
                                {
                                    TurnData tData = new TurnData();
                                    tData.Action = ShipInBattle.ShipAction.Attack;
                                    tData.ShipId = ship.Id;
                                    tData.TargetPosition = ship.TargetPosition;
                                    tData.Damage = Attack(ship);
                                    DataOfTurn.Add(tData);
                                    ship.Restore();
                                    break;
                                }
                            case ShipInBattle.ShipAction.Defend:
                                {
                                    TurnData tData = new TurnData();
                                    tData.Action = ShipInBattle.ShipAction.Attack;
                                    tData.ShipId = ship.Id;
                                    Defend(ship);
                                    DataOfTurn.Add(tData);
                                    ship.Restore();
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                    }
                }
            }
            else if(this.State == GameState.End)
            {
                Statics.BattleSessionList.Remove(this);

            }
            this.TimeLeft = this.roundTime;
            this.timer.Start();
        }

        private void Defend(ShipInBattle ship)
        {
            ship.coeff = 0.5;
        }

        private void EndGame(PlayerInBattle player)
        {
            this.State = GameState.End;
            this.WinnerId = player.Id;
        }

        private double Attack(ShipInBattle ship)
        {
            double damage = 0;
            Random rnd = new Random();
            if (ship.Health > 0)
            {
                if (ship.Owner == ShipInBattle.BattleOwner.Host)
                {
                    if (ship.TargetPosition < 5 && ship.TargetPosition >= 0)
                    {
                        double random = rnd.Next(Convert.ToInt32(ship.Damage * -25), Convert.ToInt32(ship.Damage * 25));
                        random /= 100;
                        damage = (ship.Damage + random)* this.Player.ShipList[ship.TargetPosition].coeff;
                        this.Player.ShipList[ship.TargetPosition].Health -= damage;
                    }
                }
                else
                {
                    if (ship.TargetPosition < 5 && ship.TargetPosition >= 0)
                    {
                        double random = rnd.Next(Convert.ToInt32(ship.Damage * -25), Convert.ToInt32(ship.Damage * 25));
                        random /= 100;
                        damage = (ship.Damage + random)*this.Host.ShipList[ship.TargetPosition].coeff;
                        this.Host.ShipList[ship.TargetPosition - 1].Health -= damage;
                    }
                }
            }
            return damage;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            this.TimeLeft -= 1;
        }

        private void BattleSessionOnGameStarted(object sender, EventArgs e)
        {
            this.TimeLeft = this.roundTime;
            this.timer.Interval = 1000;
            this.timer.Start();
        }

        private event EventHandler GameStarted;
        private event EventHandler TurnEnded;

        public enum GameState { NotStarted, Started, ShipsSelected , End}
    }
}