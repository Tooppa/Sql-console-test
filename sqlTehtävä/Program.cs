using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TietokantaTesti
{
    class Program
    {
        static void Main(string[] args)
        {
            string connetionString = null;
            MySqlConnection cnn;
            connetionString = "server=localhost;database=Pelitietokanta;uid=root;pwd=moi;";
            cnn = new MySqlConnection(connetionString);

            /*
            // Tietokantayhteyden luominen
            string connetionString = null;
            MySqlConnection cnn;
            connetionString = "server=localhost;database=Pelitietokanta;uid=root;pwd=moi;";
            cnn = new MySqlConnection(connetionString);
            try
            {
                cnn.Open();
                Console.WriteLine("Connection Open ! ");

                // Tietokantakyselyn tekeminen
                MySqlCommand cmd = new MySqlCommand("select * from Pelaaja", cnn);
                MySqlDataReader reader = cmd.ExecuteReader();

                // Check is the reader has any rows at all before starting to read.
                if (reader.HasRows)
                {
                    // Read advances to the next row.
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string etunimi = reader.GetString(reader.GetOrdinal("etunimi"));
                        string sukunimi = reader.GetString(reader.GetOrdinal("sukunimi"));

                        Console.WriteLine(id + " " + etunimi + " " + sukunimi);
                    }
                }
                cnn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "Can not open connection ! ");
            }
            Console.ReadLine();
            */
        }
        public class Kysely
        {
            //Testi class ei vielä täysin toiminnassa
            string[] data;

            public Kysely(string[] a)
            {
                data = a;
            }

            public void valitse()
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine(data[i]);
                }
                Console.WriteLine("valitse numero 1-3");
                int valinta = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("hakusana");
                string hakusana = Console.ReadLine();
                switch (valinta)
                {
                    case 1:
                       //sql koodi plus hakusana
                       break;
                    case 2:
                        //sql koodi plus hakusana
                        break;
                    case 3:
                        //sql koodi plus hakusana
                        break;
                    default:
                        break;
                }
            }
        }
        public class Tiedot
        {
            string[] aloitus = { 
            };

            string[] peli = { 
            };

            string[] pelistudiot = {
            };
            public string[] data(int numero)
            {
                switch (numero)
                {
                    case 1:
                        return (aloitus);
                    case 2:
                        return (peli);
                    case 3:
                        return (pelistudiot);
                    default:
                        return (null);
                }
            }
        }
    }
}

