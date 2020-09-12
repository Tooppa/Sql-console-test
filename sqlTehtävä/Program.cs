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
        static void Main()
        {
            string ekaValinta;
            string tokaValinta;
            Tiedot array = new Tiedot();
            Kysely valinta = new Kysely();

            //Ensimmäinen kysely valitaan data(1) joka sisältää peli, pelistudio, pelaaja 
            string[] data = array.data("0");
            //Tulostetaan vaihtoehdot
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine((i + 1) + ": " + data[i]);
            }
            Console.WriteLine("valitse numero 1-3");
            ekaValinta = Console.ReadLine();

            //Valitaan datasta seuraava 
            data = array.data(ekaValinta);
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine((i + 1) + ": " + data[i]);
            }
            Console.WriteLine("valitse numero 1-3");
            tokaValinta = Console.ReadLine();
            valinta.FinalPrint(ekaValinta, tokaValinta, data);
        }
        public class Kysely
        {
            private MySqlConnection cnn;
            private string connetionString;
            public Kysely()
            {
                connetionString = "server=localhost;database=Pelitietokanta;uid=root;pwd=moi;";
                cnn = new MySqlConnection(connetionString);
            }
            public void FinalPrint(string ekaValinta, string tokaValinta, string[] data)
            {
                /*data sisältö
                 *
                 *valittu pää alue
                 *jossa kolme käskyä
                 *ja vastaava sql koodi
                */
                int dataPaikka = (Convert.ToInt32(tokaValinta) + 2);
                MySqlDataReader reader;
                MySqlCommand cmd;
                cnn.Open();
                if (ekaValinta == "1")//pyörii kun eka valinta oli pelit
                {
                    cmd = new MySqlCommand("select * from Peli", cnn);
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string nimi = reader.GetString(reader.GetOrdinal("nimi"));
                            Console.WriteLine(nimi);
                        }
                    }
                    Console.WriteLine("\nKirjoita yhden pelin nimi");
                    string hakusana = Console.ReadLine();
                    cnn.Close();
                    cnn.Open();

                    cmd = new MySqlCommand($"{ data[dataPaikka] }{ hakusana }\";", cnn);
                }
                else
                {
                    cmd = new MySqlCommand(data[dataPaikka], cnn);
                }
                reader = cmd.ExecuteReader();
                //valitsee mikä komento pyörii
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        switch (ekaValinta)
                        {
                            case "1":
                                switch (tokaValinta)
                                {
                                    case "1":
                                        Console.WriteLine("pelit eka");
                                        break;
                                    case "2":
                                        Console.WriteLine("pelit toka");
                                        break;
                                    case "3":
                                        Console.WriteLine("pelit kolmas");
                                        break;
                                    default:
                                        break;

                                }
                                break;
                            case "2":
                                switch (tokaValinta)
                                {
                                    case "1":
                                        Console.WriteLine("pelistudio eka");
                                        break;
                                    case "2":
                                        Console.WriteLine("pelistudio toka");
                                        break;
                                    case "3":
                                        Console.WriteLine("pelistudio kolmas");
                                        break;
                                    default:
                                        break;

                                }
                                break;
                            case "3":
                                switch (tokaValinta)
                                {
                                    case "1":
                                        Console.WriteLine("pelaaja eka");
                                        break;
                                    case "2":
                                        Console.WriteLine("pelaaja toka");
                                        break;
                                    case "3":
                                        Console.WriteLine("pelaaja kolmas");
                                        break;
                                    default:
                                        break;

                                }
                                break;
                            default:
                                break;

                        }
                    }
                }
                Console.WriteLine("Poistu painamalla näppäintä");
                Console.ReadLine();
                cnn.Close();
            }
        }
        public class Tiedot
        {
            /*
             * täällä säilytetään data
             * jokaista kysymystä vastaava sql löytyy kolme pistettä alempaa
             * kaikki ei ole vielä täytetty
            */
            string[] aloitus = {
                "Pelit",
                "Pelistudiot",
                "Pelaajat"
            };

            string[] peli = {
                "Globaali käytetty peliaika per peli",

                "Käytetty raha per peli",

                "Pelin sisäisen tiettyjen tapahtumien määrä",

                "select Peli.nimi, sum(datediff(Pelisessio.loppuaika, Pelisessio.alkuaika)) as " +
                "GlobaaliPeliaika from Pelisessio, Peli where Peli.id = Pelisessio.peli_ID and Peli.nimi = \"",

                "select Peli.nimi, sum(datediff(Pelisessio.loppuaika, Pelisessio.alkuaika)) as " +
                "GlobaaliPeliaika from Pelisessio, Peli where Peli.id = Pelisessio.peli_ID and Peli.nimi = \"",

                "select Peli.nimi, sum(datediff(Pelisessio.loppuaika, Pelisessio.alkuaika)) as " +
                "GlobaaliPeliaika from Pelisessio, Peli where Peli.id = Pelisessio.peli_ID and Peli.nimi = \""
            };

            string[] pelistudiot = {
                "Hae kaikki tietyn pelistudion julkaistut pelit",

                "Eniten pelaajia omaava pelistudio",

                "Eniten rahaa tekevä studio",

                "select * from Pelaaja",

                "select count(Pelaaja.id) as Pelaajia, Pelistudio.nimi as Pelistudion_nimi from Pelaaja, Pelistudio, Pelaa, Peli " +
                "where Pelaa.pelaaja_ID = Pelaaja.id and Pelaa.peli_ID = Peli.id and Peli.studio_ID = Pelistudio.id group by " +
                "Pelistudio.nimi order by count(pelaaja.id) desc;",

                "select * from Pelaaja"
            };
            string[] pelaajat = {
                "Hae eniten peliin rahaa käyttäneet pelaajat",

                "Käytetty peliaika per pelaaja per peli",

                "Käytetty raha per pelitunti ",

                "select * from Pelaaja",

                "select * from Pelaaja",

                "select * from Pelaaja"
            };
            public string[] data(string numero)
            {
                switch (numero)
                {
                    case "0":
                        return (aloitus);
                    case "1":
                        return (peli);
                    case "2":
                        return (pelistudiot);
                    case "3":
                        return (pelaajat);
                    default:
                        return (null);
                }
            }

        }
    }
}

