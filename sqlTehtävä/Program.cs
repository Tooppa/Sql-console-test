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
            Console.WriteLine("\nvalitse numero 1-3");
            ekaValinta = Console.ReadLine();

            //Valitaan datasta ensimmäisen valinnan mukaan
            data = array.data(ekaValinta);
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine((i + 1) + ": " + data[i]);
            }
            Console.WriteLine("\nvalitse numero 1-3");
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
                if (ekaValinta == "1" || (ekaValinta == "2" && tokaValinta == "1"))//pyörii kun eka valinta oli pelit. iffiin lisä ehto jos haluaa jonkun muunkin sql koodin käyttävän hakusanaa
                {
                    if (ekaValinta == "1")
                    {
                        cmd = new MySqlCommand("select * from Peli", cnn);
                    }else
                    {
                        cmd = new MySqlCommand("select * from Pelistudio", cnn);
                    }
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string nimi = reader.GetString(reader.GetOrdinal("nimi"));
                            Console.WriteLine(nimi);
                        }
                    }
                    Console.WriteLine("\nKirjoita yhden pelin nimi\n");
                    string hakusana = Console.ReadLine();
                    cnn.Close();
                    cnn.Open();

                    cmd = new MySqlCommand($"{ data[dataPaikka] }{ hakusana }\";", cnn);
                }
                else
                {
                    cmd = new MySqlCommand(data[dataPaikka], cnn);
                }
                Console.WriteLine("");
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
                                        string peliaikanimi = reader.GetString(reader.GetOrdinal("nimi"));
                                        string peliaika = reader.GetString(reader.GetOrdinal("Peliaika"));
                                        Console.WriteLine(peliaikanimi + " " + peliaika);
                                        break;
                                    case "2":
                                        string summanimi = reader.GetString(reader.GetOrdinal("nimi"));
                                        string summa = reader.GetString(reader.GetOrdinal("summa"));
                                        Console.WriteLine(summanimi + " " + summa);
                                        break;
                                    case "3":
                                        string aikanimi = reader.GetString(reader.GetOrdinal("nimi"));
                                        string tyyppi = reader.GetString(reader.GetOrdinal("tyyppi"));
                                        Console.WriteLine(aikanimi + " " + tyyppi);
                                        break;
                                    default:
                                        break;

                                }
                                break;
                            case "2":
                                switch (tokaValinta)
                                {
                                    case "1":
                                        string pelistudio = reader.GetString(reader.GetOrdinal("pelistudio"));
                                        string peli = reader.GetString(reader.GetOrdinal("peli"));
                                        Console.WriteLine(pelistudio + " " + peli);
                                        break;
                                    case "2":
                                        string studionimi = reader.GetString(reader.GetOrdinal("pelistudio"));
                                        string pelaajia = reader.GetString(reader.GetOrdinal("pelaajia"));
                                        Console.WriteLine(studionimi + " " + pelaajia);
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
                                        string etunimi = reader.GetString(reader.GetOrdinal("etunimi"));
                                        string sukunimi = reader.GetString(reader.GetOrdinal("sukunimi"));
                                        string rahasumma = reader.GetString(reader.GetOrdinal("summa"));
                                        Console.WriteLine(etunimi + " " + sukunimi + " " + rahasumma);
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
                Console.WriteLine("\nPoistu painamalla näppäintä");
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

                "select Peli.nimi, timediff(Pelisessio.loppuaika, Pelisessio.alkuaika) as " +
                "Peliaika from Pelisessio, Peli where Peli.id = Pelisessio.peli_ID and Peli.nimi = \"",

                "select sum(Rahasiirto.summa) as summa, Peli.nimi from Rahasiirto, Pelisessio, Peli " +
                "where Rahasiirto.sessio_ID = Pelisessio.id and Pelisessio.peli_ID = Peli.id and Peli.nimi = \"",

                "select Peli.nimi, Pelitapahtuma_Tyyppi.tyyppi_nimi as tyyppi, Pelitapahtuma.aikaleima from " +
                "Pelitapahtuma, Pelitapahtuma_Tyyppi, Pelisessio, Peli where Pelitapahtuma.tyyppi_ID = Pelitapahtuma_Tyyppi.id " +
                "and sessio_ID = Pelisessio.id and Pelisessio.peli_ID = Peli.id and Peli.nimi = \""
            };
            //tämä viiva \ ja lainausmerkki tekee lainausmerkin stringin sisään
            //loppu on siis formaattia Peli.nimi = "hakusana";

            string[] pelistudiot = {
                "Hae kaikki tietyn pelistudion julkaistut pelit",

                "Eniten pelaajia omaava pelistudio",

                "Eniten rahaa tekevä studio",

                "select Pelistudio.nimi as pelistudio, Peli.nimi as Peli from Pelistudio, Peli where Peli.studio_ID = Pelistudio.id and Pelistudio.nimi = \"",

                "select count(Pelaaja.id) as pelaajia, Pelistudio.nimi as pelistudio from Pelaaja, Pelistudio, Pelaa, Peli " +
                "where Pelaa.pelaaja_ID = Pelaaja.id and Pelaa.peli_ID = Peli.id and Peli.studio_ID = Pelistudio.id group by " +
                "Pelistudio.nimi order by count(pelaaja.id) desc;",

                "select * from Pelaaja"
            };
            string[] pelaajat = {
                "Hae eniten peliin rahaa käyttäneet pelaajat",

                "Käytetty peliaika per pelaaja per peli",

                "Käytetty raha per pelitunti ",

                "select Pelaaja.etunimi, Pelaaja.sukunimi, sum(Rahasiirto.summa) as summa from Rahasiirto, Pelisessio, Pelaaja where " +
                "Rahasiirto.sessio_ID = Pelisessio.id and Pelisessio.pelaaja_ID = Pelaaja.id group " +
                "by Pelaaja.etunimi order by sum(Rahasiirto.summa) desc;",

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

