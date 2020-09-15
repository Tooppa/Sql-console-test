using System;
using MySql.Data.MySqlClient;

namespace TietokantaTesti
{
    class Program
    {
        static void Main()
        {
            string ekaValinta = null;
            string tokaValinta = null;
            int numero1 = 0;
            int numero2 = 0;
            Tiedot array = new Tiedot();
            Kysely valinta = new Kysely();

            //Ensimmäinen kysely valitaan data(1) joka sisältää peli, pelistudio, pelaaja 
            string[] data = array.data("0");
            //Tulostetaan vaihtoehdot
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine((i + 1) + ": " + data[i]);
            }
            while (numero1 > 3 || numero1 < 1)
            {
                Console.WriteLine("\nvalitse numero 1-3");
                ekaValinta = Console.ReadLine();
                numero1 = Convert.ToInt32(ekaValinta);
            }
            //Valitaan datasta ensimmäisen valinnan mukaan
            data = array.data(ekaValinta);
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine((i + 1) + ": " + data[i]);
            }
            while (numero2 > 3 || numero2 < 1)
            {
                Console.WriteLine("\nvalitse numero 1-3");
                tokaValinta = Console.ReadLine();
                numero2 = Convert.ToInt32(tokaValinta);
            }
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
                string ensimmainen;
                string toinen;
                string kolmas;
                string neljas;
                string viides;
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
                    Console.WriteLine("\nKirjoita yksi vaihtoehto\n");
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
                                        ensimmainen = reader.GetString(reader.GetOrdinal("nimi"));
                                        toinen = reader.GetString(reader.GetOrdinal("Peliaika"));
                                        Console.WriteLine("Pelin nimi: " + ensimmainen + ", käytetty peliaika: " + toinen);
                                        break;
                                    case "2":
                                        ensimmainen = reader.GetString(reader.GetOrdinal("nimi"));
                                        toinen = reader.GetString(reader.GetOrdinal("summa"));
                                        Console.WriteLine("Pelin nimi: " + ensimmainen + ", käytetty summa: " + toinen);
                                        break;
                                    case "3":
                                        ensimmainen = reader.GetString(reader.GetOrdinal("nimi"));
                                        toinen = reader.GetString(reader.GetOrdinal("tyyppi"));
                                        kolmas = reader.GetString(reader.GetOrdinal("aikaleima"));
                                        Console.WriteLine("Pelin nimi: " + ensimmainen + ", tapahtuman nimi: " + toinen + ", aikaleima: " + kolmas);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "2":
                                switch (tokaValinta)
                                {
                                    case "1":
                                        ensimmainen = reader.GetString(reader.GetOrdinal("pelistudio"));
                                        toinen = reader.GetString(reader.GetOrdinal("peli"));
                                        Console.WriteLine("Pelistudion nimi: " + ensimmainen + ", pelin nimi: " + toinen);
                                        break;
                                    case "2":
                                        ensimmainen = reader.GetString(reader.GetOrdinal("pelistudio"));
                                        toinen = reader.GetString(reader.GetOrdinal("pelaajia"));
                                        Console.WriteLine("Pelistudion nimi: " + ensimmainen + ", pelaajien määrä: " + toinen);
                                        break;
                                    case "3":
                                        ensimmainen = reader.GetString(reader.GetOrdinal("nimi"));
                                        toinen = reader.GetString(reader.GetOrdinal("voitot"));
                                        Console.WriteLine("Pelistudion nimi: " + ensimmainen + ", voittojen määrä: " + toinen);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "3":
                                switch (tokaValinta)
                                {
                                    case "1":
                                        ensimmainen = reader.GetString(reader.GetOrdinal("etunimi"));
                                        toinen = reader.GetString(reader.GetOrdinal("sukunimi"));
                                        kolmas = reader.GetString(reader.GetOrdinal("summa"));
                                        Console.WriteLine("Pelaajan nimi: " + ensimmainen + " " + toinen + ", pelaajan käyttämä rahamäärä: " + kolmas);
                                        break;
                                    case "2":
                                        ensimmainen = reader.GetString(reader.GetOrdinal("etunimi"));
                                        toinen = reader.GetString(reader.GetOrdinal("sukunimi"));
                                        kolmas = reader.GetString(reader.GetOrdinal("nimimerkki"));
                                        neljas = reader.GetString(reader.GetOrdinal("peli"));
                                        viides = reader.GetString(reader.GetOrdinal("peliaika"));
                                        Console.WriteLine("Pelaajan nimi: " + ensimmainen + " " + toinen + ", nimimerkki: " + kolmas + 
                                            ", pelin nimi: " + neljas + ", käytetty peliaika: " + viides
                                        );
                                        break;
                                    case "3":
                                        ensimmainen = reader.GetString(reader.GetOrdinal("etunimi"));
                                        toinen = reader.GetString(reader.GetOrdinal("sukunimi"));
                                        kolmas = reader.GetString(reader.GetOrdinal("sessiomaara"));
                                        Console.WriteLine("Pelaajan nimi: " + ensimmainen + " " + toinen + ", pelaajan pelatut sessiot: " + kolmas);
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

                "select sum(Rahasiirto.summa) as voitot, Pelistudio.Nimi from Rahasiirto, Pelisessio, pelistudio, Peli where " +
                "Rahasiirto.sessio_ID = Pelisessio.id and Pelisessio.peli_ID = Peli.id " +
                "and Peli.studio_ID = Pelistudio.id group by Pelistudio.nimi order by Voitot desc;"
            };
            string[] pelaajat = {
                "Hae eniten peliin rahaa käyttäneet pelaajat",

                "Käytetty peliaika per pelaaja per peli",

                "Pelisessioiden määrä per pelaaja",

                "select Pelaaja.etunimi, Pelaaja.sukunimi, sum(Rahasiirto.summa) as summa from Rahasiirto, Pelisessio, Pelaaja where " +
                "Rahasiirto.sessio_ID = Pelisessio.id and Pelisessio.pelaaja_ID = Pelaaja.id group " +
                "by Pelaaja.etunimi order by sum(Rahasiirto.summa) desc;",

                "select Pelaa.nimimerkki as nimimerkki, Pelaaja.etunimi, Pelaaja.sukunimi," +
                "timediff(Pelisessio.loppuaika, Pelisessio.alkuaika) as peliaika, Peli.nimi as peli from " +
                "Pelaaja, Pelisessio, Peli, Pelaa where Peli.id = Pelisessio.peli_ID and Pelisessio.pelaaja_ID = Pelaaja.id and " +
                "Pelaa.peli_id = Peli.id and Pelaa.pelaaja_id = Pelaaja.id group by Pelaaja.etunimi;",

                "select Pelaaja.etunimi, Pelaaja.sukunimi, count(Pelisessio.id) as sessiomaara from Pelaaja, " +
                "Pelisessio, Peli where Peli.id = Pelisessio.peli_ID and Pelisessio.pelaaja_ID = Pelaaja.id " +
                "group by Pelaaja.etunimi order by count(Pelisessio.id) desc;"
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