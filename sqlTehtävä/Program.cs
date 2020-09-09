﻿using System;
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

            Tiedot array = new Tiedot();
            for (int i = 1; i < 3; i++) {
                string[] test = array.data(i);
                Kysely valinta = new Kysely(test);
                valinta.valitse();
            }
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
            public string[] data;

            public Kysely(string[] a)
            {
                data = a;
            }

            public void valitse()
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine((i + 1) + ": " + data[i]);
                }
                Console.WriteLine("valitse numero 1-3");
                int valinta = Convert.ToInt32(Console.ReadLine());

                switch (valinta)
                {
                    case 1:
                        //sql haulla vaihtoehdot näkyviin
                        if (data[0] == "Pelit")
                        {
                            Console.WriteLine("Minkä pelin tiedot haluat nähdä");
                            string hakusana = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("test");
                            Console.ReadLine();
                            //sql koodi
                        }
                        break;
                    case 2:
                        //sql haulla vaihtoehdot näkyviin
                        if (data[0] == "Pelit")
                        {
                            Console.WriteLine("Minkä pelistudion tiedot haluat nähdä");
                            string hakusana = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("test");
                            Console.ReadLine();
                            //sql koodi
                        }
                        break;
                    case 3:
                        //sql haulla vaihtoehdot näkyviin
                        if (data[0] == "Pelit")
                        {
                            Console.WriteLine("Kenen pelaajan tiedot haluat nähdä");
                            string hakusana = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("test");
                            Console.ReadLine();
                            //sql koodi
                        }
                        break;
                    default:
                        break;

                }
            }
        }
        public class Tiedot
        {
            string[] aloitus = {
                "Pelit",
                "Pelistudiot",
                "Pelaajat"
            };

            string[] peli = {
                "Globaali käytetty peliaika per peli",
                "Käytetty raha per peli",
                "Pelin sisäisen tiettyjen tapahtumien määrä"
            };

            string[] pelistudiot = {
                "Hae kaikki tietyn pelistudion julkaistut pelit",
                "Eniten pelaajia omaava pelistudio",
                "Eniten rahaa tekevä studio"
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

