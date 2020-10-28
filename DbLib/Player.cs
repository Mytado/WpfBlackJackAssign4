using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DbLib
{
    public class Program
    {
        public static void Start()
        {
            using (var db = new PlayersContext())
            {
                Console.Write("Enter a name for a new Player: ");
                var name = Console.ReadLine();
                var player = new Player { PlayerName = name };
                db.Players.Add(player);
                db.SaveChanges();

                var query = from b in db.Players orderby b.PlayerName select b;
                foreach(var item in query)
                {
                    Console.WriteLine(item.PlayerName);
                }
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }


    public class Player
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string Address { get; set; }
        public int PlayerListId { get; set; }

        public virtual List<PlayerFunds> PlayerFunds { get; set; }
        public virtual List<Score> Scores { get; set; }
    }

    public class Score
    {
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
    }
    public class PlayerFunds
    {
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public int Funds { get; set; }
    }

    public class PlayersContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<PlayerFunds> PlayerFunds { get; set; }
    }
}
