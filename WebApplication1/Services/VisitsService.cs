using Microsoft.Data.SqlClient;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services;

public class VisitsService : IVisitsService
{
    private readonly string _connectionString; 
 
    public VisitsService(IConfiguration configuration) 
    { 
        _connectionString = configuration.GetConnectionString("Default"); 
    }

    public async Task<List<ClientsVisitDTO>> GetClientsVisits(int idClient)
    {
       var visits = new List<ClientsVisitDTO>();
       const string query = @"select v.visit_id, c.first_name,c.last_name,c.date_of_birth,m.mechanic_id,m.licence_number,s.name,s.base_fee\nfrom Visit v\njoin Client c on c.client_id=v.client_id\njoin Mechanic m on m.mechanic_id=v.mechanic_id\njoin Visit_Service vs on vs.visit_id=v.visit_id\njoin Service s on s.service_id=vs.service_id"
           ;

       using var conn = new SqlConnection(_connectionString);
       using var cmd = new SqlCommand(query, conn);
       
       cmd.Parameters.AddWithValue("@client_id", idClient);
       await conn.OpenAsync();
       
       using var reader = await cmd.ExecuteReaderAsync();
       
       var visitsMap=new Dictionary<int, ClientsVisitDTO>();
       
       while (await reader.ReadAsync())
       {
           int visitId = reader.GetInt32(0);
           if (!visitsMap.ContainsKey(visitId))
           {
               visitsMap[visitId] = new ClientsVisitDTO
               {
                   Imie = reader.GetString(1),
                   Nazwisko = reader.GetString(2),
                   DataUrodzenia = reader.GetDateTime(3), 
                   MechanicDTO = new ClientsMechanicDTO(),
                   VisitDtos = new ClientsServiceVisitDTO()
               };
           }

           if (!reader.IsDBNull(4))
           {
               var mechanic = new ClientsMechanicDTO
               {
                   MechanicId = reader.GetInt32(4),
                   LicenseNumber = reader.GetString(5),
               };
           }

           if (!reader.IsDBNull(6))
           {
               var serwisy = new ClientsServiceVisitDTO()
               {
                   NazwaSerwisu = reader.GetString(6),
                   ServiceFee = reader.GetFloat(7),
               };
           }
       }
       



       return visitsMap.Values.ToList();




    }
    
}