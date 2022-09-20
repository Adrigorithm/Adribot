using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

public class DataManager{
    private readonly SqlConnection _connection;

    public DataManager(string connectionString){
        _connection = new SqlConnection(connectionString);
    }

    public async Task<List<DGuild>> GetAllDataAsync(){
        var data = new List<DGuild>();

        var command = new SqlCommand();
        command.Connection = _connection;
        command.CommandText = @"SELECT GuildMembers.GuildId, Users.UserId FROM Guilds 
            INNER JOIN GuildMembers ON Guilds.GuildId = GuildMembers.GuildId 
            RIGHT JOIN Users ON GuildMembers.UserId = Users.UserId 
            INNER JOIN Reminders ON GuildMembers.GuildMemberId = Reminders.GuildMemberId 
            INNER JOIN Tags ON GuildMembers.GuildMemberId = Tags.GuildMemberId";
        
        await _connection.OpenAsync();
        var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            data.Add(new DGuild{GuildId = (ulong)reader["guildId"]});
        };

        await _connection.CloseAsync();

        return data;
    }

    public async Task AddDataAsync(Object data){

    }

    public async Task AddDataAsync(IList<Object> data){

    }
}
