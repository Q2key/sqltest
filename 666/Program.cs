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
            Dagger,
            Knife,
            DualDaggers,
            Mace,
            Blunt,
            Polearm,
            Pistol,
            DualPistols,
            Rifle,
            DualSwords,
            TwoHandedSwords,
            Axe,
            Poleaxe,
            Katar,
            ShortBow,
            LongBow,
            LaserRifle,
            CrossBow,
            Sai
        }
        static void Main(string[] args)
        {


            var msk = new Dictionary<string,Dictionary<string,int>>();
            var spb = new Dictionary<string, Dictionary<string, int>>();
            var ekb = new Dictionary<string, Dictionary<string, int>>();

            var frommskto = new Dictionary<string,int>();
            var fromspbto = new Dictionary<string, int>();
            var fromekbto = new Dictionary<string, int>();

            frommskto.Add(@"Saint Petersburg",350);
            frommskto.Add(@"Ekaterinburg",810);

            fromspbto.Add(@"Moscow",350);
            fromspbto.Add(@"Ekaterinburg",1200);

            fromekbto.Add(@"Moscow",810);
            fromekbto.Add(@"Saint Petersburg",1200);
            
            msk.Add(@"Moscow",frommskto);
            spb.Add(@"Saint Petersburg",fromspbto);
            ekb.Add(@"Ekaterinburg",fromekbto);

            var list = new List<Dictionary<string, Dictionary<string, int>>> {msk, spb, ekb};
            var path = new Dictionary<string,int>();

            foreach (var dict in list)
            {   
                foreach (var subdict in dict)
                {
                    foreach (var curdict in subdict.Value)
                    {

                    }
                }
            }


            



            /*
            var list = new int[4][];

            list[0] = moscow;
            list[1] = spb;
            list[2] = ekb;
            list[3] = kazan;

            var mydict = new Dictionary<string, int[]>
            {
                {@"Moscow", moscow},
                {@"Saint Petersburg", spb},
                {@"Ekaterinburg", ekb},
                {@"Kazan", kazan}
            };
            mydict.ToList().ForEach(t=>Console.WriteLine(@"{0} {1}",t.Key, PrFoo(t.Value)));
             */


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
                    FillSqLdatabaseTableTwo(con);
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
                for (var i = 0; i < 18; i++)
                {
                    var cmd = new SqlCommand(insertcmd, con, trans);
                    var name = (Names) i;
                    var lastname = (LastNames) i;
                    var weapons = (Weapons) i;
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
        private static void FillSqLdatabaseTableTwo(SqlConnection con)
        {
            using (var trans = con.BeginTransaction())
            {
                const string insertcmd = @"INSERT INTO guildmembers (name, age, strength, dexterity, intellect,vitality)" +
                                         "values (@name, @age, @strength, @dexterity, @intellect, @vitality);";
                var rnd = new Random();
                for (var i = 0; i < 18; i++)
                {
                    var cmd = new SqlCommand(insertcmd, con, trans);
                    var name = (Names)i;
                    var age = rnd.Next(18, 40);
 
                    cmd.Parameters.AddWithValue("@name", $@"{name}");
                    cmd.Parameters.AddWithValue(@"age", $@"{age}");
                    cmd.Parameters.AddWithValue(@"strength", $@"{rnd.Next(5, 20)}");
                    cmd.Parameters.AddWithValue(@"dexterity", $@"{rnd.Next(5, 20)}");
                    cmd.Parameters.AddWithValue(@"intellect", $@"{rnd.Next(5, 20)}");
                    cmd.Parameters.AddWithValue(@"vitality", $@"{rnd.Next(5, 20)}");

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
        private static int PrFoo(int[] array)
        {
            var min = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (min > array[i])
                {
                    min = array[i];
                }
            }
            return min;
        }
    }
}
