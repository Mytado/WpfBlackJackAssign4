using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBlackJackAssign4.BlackJackBLL;

namespace WpfBlackJackAssign4.BlackJackDAL
{
    class BlackJackSQLCopy
    {
        public static void deletePlayer(string _playerToRemove)
        {
            using (var db = new BlackJackBLL.PlayersContext())
            {
                Player player = GetPlayer(_playerToRemove);
                db.Players.Attach(player);
                db.Players.Remove(db.Players.Find(player.PlayerId));
                db.PlayerFunds.Remove(db.PlayerFunds.Find(player.PlayerId));
                db.Scores.Remove(db.Scores.Find(player.PlayerId));
                //db.Entry(player).State = EntityState.Deleted;
                db.SaveChanges();
                Console.WriteLine(_playerToRemove + " deleted.");
            }
        }

        private static Player CreateNewPlayer(string _newPlayerName)
        {
            Player newPlayer = new Player();
            newPlayer.PlayerName = _newPlayerName;
            PlayerFunds playerFunds = new PlayerFunds();
            playerFunds.Player = newPlayer;
            playerFunds.Funds = 100;
            Score playerScore = new Score();
            playerScore.Player = newPlayer;
            playerScore.Wins = 0;
            playerScore.Losses = 0;
            newPlayer.PlayerFunds = playerFunds;
            newPlayer.Scores = playerScore;

            using (var db = new BlackJackBLL.PlayersContext())
            {
                db.Players.Add(newPlayer);
                db.PlayerFunds.Add(playerFunds);
                db.Scores.Add(playerScore);
                db.SaveChanges();
            }
            return newPlayer;
        }

        public static Player GetPlayer(string _playerName)
        {
            using (var db = new BlackJackBLL.PlayersContext())
            {
                var query1 = from b in db.Players
                             orderby b.PlayerName
                             select b;

                foreach (Player item in query1)
                {
                    if (item.PlayerName.Equals(_playerName))
                    {
                        return GetAllData(item.PlayerId);
                    }
                }
                Console.WriteLine("No player of name " + _playerName + " found. Creating new player.");
                return CreateNewPlayer(_playerName);
            }
        }

        public static void UpdatePlayer(Player _player)
        {
            SetPlayerFunds(_player.PlayerId, _player.PlayerFunds.Funds);
            SetPlayerWins(_player.PlayerId, _player.Scores.Wins);
            SetPlayerLosses(_player.PlayerId, _player.Scores.Losses);
        }

        public static LinkedList<Player> GetAllPlayers()
        {
            LinkedList<Player> players = new LinkedList<Player>();
            using (var db = new BlackJackBLL.PlayersContext())
            {
                var query = from b in db.Players
                            orderby b.PlayerId
                            select b;

                foreach (Player item in query)
                {
                    players.AddLast(GetAllData(item.PlayerId));
                }
            }
            return players;
        }

        private static Player GetAllData(int _playerId)
        {
            Player player = new BlackJackBLL.Player();
            using (var db = new BlackJackBLL.PlayersContext())
            {
                player = db.Players.Find(_playerId);
                player.PlayerFunds = db.PlayerFunds.Find(_playerId);
                player.Scores = db.Scores.Find(_playerId);
            }
            return player;
        }

        public static Player GetPlayer(int _playerId)
        {
            using (var db = new BlackJackBLL.PlayersContext())
            {
                var query1 = from b in db.Players
                             orderby b.PlayerId
                             select b;

                foreach (Player item in query1)
                {
                    if (item.PlayerId == _playerId) return GetAllData(_playerId);
                }
            }
            return null;
        }

        public static int GetPlayerId(string _PlayerName)
        {
            using (var db = new BlackJackBLL.PlayersContext())
            {
                var query1 = from b in db.Players
                             orderby b.PlayerName
                             select b;

                foreach (Player item in query1)
                {
                    if (item.PlayerName.Equals(_PlayerName)) return item.PlayerId;
                }
            }
            return -1;
        }

        public static int SetPlayerFunds(int _PlayerId, int _funds)
        {
            PlayerFunds pf = null;
            using (var db = new BlackJackBLL.PlayersContext())
            {
                pf = db.PlayerFunds.Find(_PlayerId);
                if (pf != null)
                {
                    pf.Funds = _funds;
                    db.SaveChanges();
                }
            }
            return pf.Funds;
        }

        public static int SetPlayerWins(int _PlayerId, int _wins)
        {
            Score sc = null;
            using (var db = new BlackJackBLL.PlayersContext())
            {
                sc = db.Scores.Find(_PlayerId);
                if (sc != null)
                {
                    sc.Wins = _wins;
                    db.SaveChanges();
                }
            }
            return sc.Wins;
        }
        public static int SetPlayerLosses(int _PlayerId, int _losses)
        {
            Score sc = null;
            using (var db = new BlackJackBLL.PlayersContext())
            {
                sc = db.Scores.Find(_PlayerId);
                if (sc != null)
                {
                    sc.Losses = _losses;
                    db.SaveChanges();
                }
            }
            return sc.Losses;
        }
        public static PlayerFunds GetPlayerFunds(int _playerId)
        {
            PlayerFunds pf = null;
            using (var db = new BlackJackBLL.PlayersContext())
            {
                pf = db.PlayerFunds.Find(_playerId);
            }
            return pf;
        }
        public static Score getPlayerScore(int _playerId)
        {
            Score sc = null;
            using (var db = new BlackJackBLL.PlayersContext())
            {
                sc = db.Scores.Find(_playerId);
            }
            return sc;
        }
    }
}