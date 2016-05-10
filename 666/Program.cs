using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _666
{
    internal class Program
    {
        private enum Names
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
        private enum LastNames
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
        private enum Weapons
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
            Mssqlconnect();           
            Console.ReadKey(true);
        }
        private static void Mssqlconnect()
        {
            var con = new SqlConnection();
            var cs = @"user id=username;" +
                     @"password=password;server=VK\SQLEXPRESS;" +
                     @"Trusted_Connection=yes;" +
                     @"database=mydb; " +
                     @"connection timeout=15";

            con.ConnectionString = cs;
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                if (con.State == ConnectionState.Open)
                {
                    Console.WriteLine(@"connected");
                    FillSqLdatabaseTable(con);
                    SelectFromMssqlbase(con);
                }
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                con.Close();
            }

        }
        private static void FillSqLdatabaseTable(SqlConnection con)
        {
            using (var trans = con.BeginTransaction())
            {
                const string insertcmd = @"INSERT INTO guild (name, title, weapon) Values(@name,@title,@weapon);";
                var rnd = new Random();
                for (var i = 0; i < 100; i++)
                {
                    var cmd = new SqlCommand(insertcmd, con, trans);
                    var name = (Names) rnd.Next(0, 18);
                    var lastname = (LastNames) rnd.Next(0, 18);
                    var weapons = (Weapons) rnd.Next(0, 18);
                    cmd.Parameters.AddWithValue("@name", $@"{name}");
                    cmd.Parameters.AddWithValue("@title", $@"{lastname}");
                    cmd.Parameters.AddWithValue("@weapon", $@"{weapons}");

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                trans.Commit();
                Console.WriteLine("Done");
            }
        }
        private static void SelectFromMssqlbase(SqlConnection con)
        {
            const string selectcmd = @"SELECT name, title " +
                                     @"FROM guild " +
                                     @"WHERE name LIKE 'Satan';";
            var cmd = new SqlCommand(selectcmd,con);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(@"{0} {1}",reader["name"],reader["title"]);
            }


        }
        private static void Sqliteconnect()
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
                        var name = (Names)rnd.Next(0, 18);
                        var lastname = (LastNames)rnd.Next(0, 18);
                        var weapons = (Weapons)rnd.Next(0, 18);
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
        }
    }
}
