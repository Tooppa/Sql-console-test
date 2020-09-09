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
        }
    }
}

