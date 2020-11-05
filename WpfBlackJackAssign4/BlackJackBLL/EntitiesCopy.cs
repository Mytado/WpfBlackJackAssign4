using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBlackJackAssign4.BlackJackDAL;

namespace WpfBlackJackAssign4.BlackJackBLL
{
    class EntitiesCopy
    {
        public static void UpdatePlayer(Player _player)
        {
            BlackJackSQLCopy.UpdatePlayer(_player);
        }

        public static void AddWin(Player _player)
        {
            _player.Scores.Wins++;
            BlackJackSQLCopy.SetPlayerWins(_player.PlayerId, _player.Scores.Wins);
        }
        public static void AddLoss(Player _player)
        {
            _player.Scores.Losses++;
            BlackJackSQLCopy.SetPlayerLosses(_player.PlayerId, _player.Scores.Losses);
        }
        public static void ChangeFunds(Player _player, int _change)
        {
            _player.PlayerFunds.Funds += _change;
            BlackJackSQLCopy.SetPlayerFunds(_player.PlayerId, _player.PlayerFunds.Funds);
        }

        public static void DeletePlayer(Player _player)
        {
            BlackJackSQLCopy.deletePlayer(_player.PlayerName);
        }

        public static Player GetPlayer(string _name)
        {
            return BlackJackSQLCopy.GetPlayer(_name);
        }

        public static PlayerFunds GetFunds(Player _player)
        {
            return BlackJackSQLCopy.GetPlayerFunds(_player.PlayerId);
        }

        public static Score GetScore(Player _player)
        {
            return BlackJackSQLCopy.getPlayerScore(_player.PlayerId);
        }

        public static LinkedList<Player> GetCurrentPlayers(LinkedList<Player> _players)
        {
            LinkedList<Player> players = new LinkedList<Player>();
            foreach (Player player in _players)
            {
                players.AddFirst(BlackJackSQLCopy.GetPlayer(player.PlayerId));
            }
            return players;
        }

        public static LinkedList<Player> GetAllPlayers()
        {
            LinkedList<Player> players = BlackJackSQLCopy.GetAllPlayers();
            LinkedList<Player> res = new LinkedList<Player>();
            foreach (Player p in players)
            {
                res.AddLast(p);
            }
            return res;
        }
        public static LinkedList<Player> FilterAllPlayers(LinkedList<Player> players, string _nameParam, int _minFunds)
        {
            LinkedList<Player> res = new LinkedList<Player>();
            foreach (Player p in players)
            {
                if (p.PlayerName.Contains(_nameParam))
                {
                    if (p.PlayerFunds.Funds >= _minFunds)
                    {
                        res.AddLast(p);
                    }
                }
            }
            return res;
        }
    }

    public class Players
    {
        public LinkedList<Player> list;

        public Players(LinkedList<string> names)
        {
            list = new LinkedList<Player>();

            foreach (string name in names)
            {
                list.AddLast(EntitiesCopy.GetPlayer(name));
            }
        }

        public LinkedList<Player> FilterPlayers(string _nameParam, int _minFunds)
        {
            LinkedList<Player> res = new LinkedList<Player>();
            foreach (Player p in list)
            {
                if (p.PlayerName.Contains(_nameParam))
                {
                    if (p.PlayerFunds.Funds >= _minFunds)
                    {
                        res.AddLast(p);
                    }
                }
            }
            return res;
        }
        public LinkedList<Player> GetList()
        {
            return list;
        }
        public void AddWin(Player _player)
        {
            //Entities.AddWin(_player);
        }
        public void AddLoss(Player _player)
        {
            //Entities.AddLoss(_player);
        }
        public void ChangeFunds(Player _player, int _change)
        {
            //Entities.ChangeFunds(_player, _change);
        }
        public void Remove(Player _player)
        {
            list.Remove(_player);
            //Entities.DeletePlayer(_player);
        }
    }
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        //public int PlayerListId { get; set; }

        public virtual PlayerFunds PlayerFunds { get; set; }

        public virtual Score Scores { get; set; }
    }

    public class Score
    {
        /*public Score(Player _player)
        {
            Player = _player;
        }*/
        [Key]
        [ForeignKey("Player")]
        public int ScoreId { get; set; }

        public virtual Player Player { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
    }
    public class PlayerFunds
    {
        [Key]
        [ForeignKey("Player")]
        public int PlayerFundsId { get; set; }
        public virtual Player Player { get; set; }
        public int Funds { get; set; }


    }

    public class PlayersContext : DbContext
    {
        public PlayersContext() : base("BlackJackDAL.PlayersContext")
        {
            Database.Connection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BlackJackDAL.PlayersContext;Integrated Security=True;MultipleActiveResultSets=True";
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<PlayerFunds> PlayerFunds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasRequired(s => s.PlayerFunds).WithRequiredPrincipal(ad => ad.Player);
            modelBuilder.Entity<Player>().HasRequired(s => s.Scores).WithRequiredPrincipal(ad => ad.Player);
        }
    }
}
