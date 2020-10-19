using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Adribot.src.entities;
using System.Security.Authentication;
using System.Linq;

namespace Adribot.src.services.dbGateway
{
    public class Database
    {
        private const string _connectionString = "Data Source=localhost;Initial Catalog=AdribotDB;Integrated Security=True";

        /// <summary>
        /// Main method that controlls the dataflow between the data and application layer
        /// </summary>
        /// <param name="type">Type of objects to get from the database</param>
        /// <param name="amount">Defines how many objects to retrieve (new -> old). This is applicable to Ban objects.</param>
        /// <returns>List of objects of the requested type</returns>
        public List<object> GetObjectList(Type type, int amount = 10) {
            if(type == typeof(Ban)) {
                var bans = new List<object>();

                var connection = new SqlConnection(_connectionString);
                var command = new SqlCommand($"SELECT TOP {((amount > 0) ? amount : 10)} guildId, userId, banExpired FROM dbo.bans ORDER BY banExpired DESC", connection);

                try {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read()) {
                        bans.Add(new Ban((ulong)reader["guildId"], (ulong)reader["userId"], (DateTime)reader["banExpired"]));
                    }
                } catch(Exception e) {
                    Console.WriteLine($"Database query failed!\nException: {e.Message}.");
                }

                connection.Close();
                return bans;
            }

            return null;
        }

        public void InsertObjectList(IEnumerable<object> objects) {

            // What type is this lol
            switch(objects) {
                case IEnumerable<Ban> b:
                    var connection = new SqlConnection(_connectionString);
                    StringBuilder commandString = new StringBuilder();

                    commandString.Append("INSERT INTO dbo.bans (guildId, userId, banExpired) VALUES ");

                    for(int i = 0; i < b.Count(); i++) {
                        commandString.Append($"({b.ElementAt(i).GuildId}, {b.ElementAt(i).UserId}, {b.ElementAt(i).BanExpired})");

                        if(i++ < b.Count()) {
                            commandString.Append(',');
                        }
                    }

                    var command = new SqlCommand(commandString.ToString(), connection);

                    try {
                        connection.Open();
                        command.ExecuteNonQuery();
                    } catch(Exception e) {
                        Console.WriteLine($"Database query failed!\nException: {e.Message}.");
                    }

                    connection.Close();
                    break;
            }
        }
    }
}
