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
            Tiedot array = new Tiedot();
            Kysely valinta = new Kysely();
            for (int i = 1; i < 3; i++) {
                string[] test = array.data(i);
                valinta.Valitse(test);
            }
        }
        public class Kysely
        {
            public string valinta;
            public Kysely()
            {
                valinta = "4";
            }
            public void Valitse(string[] data)
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine((i + 1) + ": " + data[i]);
                }
                Console.WriteLine("valitse numero 1-3");
                string numero;
                if (valinta == "4")
                {
                    numero = Console.ReadLine();
                    valinta = numero;
                }
                else
                {
                    numero = Console.ReadLine();
                }
                

                switch (valinta)
                {
                    case "1":
                        string[] peli = {
                            "select * from Peli",
                            "Kirjoita yhden pelin nimi",
                            "peli",
                            valinta,
                            numero,
                        };
                        Sql(peli, data);
                        break;
                    case "2":
                        string[] pelistudio = {
                            "select * from Pelistudio",
                            "Kirjoita yhden pelistudion nimi",
                            "pelistudio",
                            valinta,
                            numero,
                        };
                        Sql(pelistudio, data);
                        break;
                    case "3":
                        string[] pelaaja = {
                            "select * from Pelaaja",
                            "Kirjoita yhden pelaajan nimi",
                            "pelaaja",
                            valinta,
                            numero,
                        };
                        Sql(pelaaja, data);
                        break;
                    default:
                        break;


                }
            }
            public void Sql(string[] b, string[] data)
            {
                //b sirtää dataa functioiden välillä
                //b[0]vaihtoehdot mistä valita pelejä
                //b[1]vaihtoehtoiset tekstit vastauksen pyytämiseen
                //b[3]pää vaihtoehdon nimi peli, pelistudio, pelaaja
                //b[3]pää vaihtoehdon numero peli, pelistudio, pelaaja
                //b[4]toisen valinnan numero
                string connetionString = null;
                MySqlConnection cnn;
                connetionString = "server=localhost;database=Pelitietokanta;uid=root;pwd=moi;";
                cnn = new MySqlConnection(connetionString);
                cnn.Open();
                if (data[0] != "Pelit")
                {
                    MySqlCommand cmd = new MySqlCommand(b[0], cnn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (b[2] == "pelaaja")
                            {
                                string etunimi = reader.GetString(reader.GetOrdinal("etunimi"));
                                string sukunimi = reader.GetString(reader.GetOrdinal("sukunimi"));
                                Console.WriteLine(etunimi + " " + sukunimi);
                            }
                            else {
                                string nimi = reader.GetString(reader.GetOrdinal("nimi"));
                                Console.WriteLine(nimi);
                            }
                        }
                        cnn.Close();
                        FinalPrint(b, data);
                    }
                    
                }
            }
            public void FinalPrint(string[] b, string[] data)
            {
                string connetionString = null;
                MySqlConnection cnn;
                connetionString = "server=localhost;database=Pelitietokanta;uid=root;pwd=moi;";
                cnn = new MySqlConnection(connetionString);
                cnn.Open();
                Console.WriteLine(" ");
                Console.WriteLine(b[1]);
                string hakusana = Console.ReadLine();
                int dataPaikka = Convert.ToInt32(b[3]) + 2;
                MySqlCommand cmd = new MySqlCommand(data[dataPaikka] + hakusana + "\";", cnn);
                MySqlDataReader reader = cmd.ExecuteReader();
                switch (b[2])
                {
                    case "peli":
                        switch (b[4])
                        {
                            case "1":
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        string nimi = reader.GetString(reader.GetOrdinal("nimi"));
                                        string globaaliPeliaika = reader.GetString(reader.GetOrdinal("GlobaaliPeliaika"));
                                        Console.WriteLine("Pelin nimi: " + nimi + " ja Globaali peliaika: " + globaaliPeliaika);
                                    }
                                }
                                break;
                            case "2":

                                break;
                            case "3":

                                break;
                            default:
                                break;

                        }
                        break;
                    case "pelistudio":

                        break;
                    case "pelaaja":

                        break;
                    default:
                        break;

                }
                Console.WriteLine("Poistu painamalla näppäintä");
                Console.ReadLine();
                cnn.Close();
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
                "Pelin sisäisen tiettyjen tapahtumien määrä",
                "select Peli.nimi, sum(datediff(Pelisessio.loppuaika, Pelisessio.alkuaika)) as GlobaaliPeliaika from Pelisessio, Peli where Peli.id = Pelisessio.peli_ID and Peli.nimi = \"",
                "select sum(Rahasiirto.summa), Peli.nimi from Rahasiirto, Pelisessio, Peli where Rahasiirto.sessio_ID = Pelisessio.id and Pelisessio.peli_ID = Peli.id and Peli.nimi = \"",
                ""
            };

            string[] pelistudiot = {
                "Hae kaikki tietyn pelistudion julkaistut pelit",
                "Eniten pelaajia omaava pelistudio",
                "Eniten rahaa tekevä studio",
                "",
                "",
                ""
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

