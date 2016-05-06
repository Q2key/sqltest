using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _666
{
    internal class Program
    {
        public enum Names
        {
            Dimka,
            Sashka,
            Dyatlow,
            Gaysin,
            Smolencev,
            Habarov,
            Petrov,
            Nikolka,
            Chernova,
            Pokemon,
            UkrainBoy,
            Metelev,
            Leshenka,
            Grozov,
            Demidov,
            RedHeadGay,
            Satan,
            Solomonich
        }
        public enum LastNames
        {
            Demonizer,
            Perturbator,
            Annihilator,
            Tormentor,
            NightShadow,
            SilentKilla,
            CrackDiller,
            ChickenEater,
            Nigga,
            Gay,
            BackDoorLover,
            StupidKochka,
            DickEater,
            BrainBreaker,
            TrueViking,
            NiceGuy,
            FatBasterd,
            Geek
        }
        public enum Weapons
        {
            Intellect,
            Boobs,
            Beauty,
            Chlen,
            ChlenSashki,
            PowerOfBiceps,
            ZaryazhenniTAZ,
            Dagger,
            Ak47,
            Bazzoka,
            Bfg,
            M16,
            AnalHole,
            ShinyBalls,
            LongLongLongLongLongNumber,
            ShinySmile,
            AxeAndHammer,
            PowerOfProgramming
        }

        static void Main(string[] args)
        {
            const string constring = @"Data Source =D:\db.db; Version = 3;";
            using (var con = new SQLiteConnection(constring))
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                const string cmdstring = @"DROP TABLE IF EXISTS guildmembers; CREATE TABLE guildmembers (ID INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Title TEXT, Weapon TEXT, UNIQUE(Name, Title, Weapon) ON CONFLICT REPLACE);";
                var cmd = new SQLiteCommand(cmdstring, con);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                const string insertstring = @"INSERT INTO guildmembers(Name, Title, Weapon) Values(@Name,@Title,@Weapon);";
                cmd.CommandText = insertstring;
                cmd.Connection = con;

                using (var trans = con.BeginTransaction())
                {
                    var rnd = new Random();
                    
                    for (var i = 0; i < 30000; i++)
                    {
                        var name = (Names) rnd.Next(0, 18);
                        var lastname = (LastNames) rnd.Next(0, 18);
                        var weapons = (Weapons) rnd.Next(0, 18);
                        cmd.Parameters.AddWithValue("@Name", $@"{name}");
                        cmd.Parameters.AddWithValue("@Title", $@"{lastname}");
                        cmd.Parameters.AddWithValue("@Weapon", $@"{weapons}");
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (SQLiteException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    trans.Commit();
                    Console.WriteLine("Done");
                }

            }
            Console.ReadKey(true);
        }
    }
}
