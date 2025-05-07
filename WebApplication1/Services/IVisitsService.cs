using WebApplication1.Models.DTOs;

namespace WebApplication1.Services;

public interface IVisitsService
{
    Task<List<ClientsVisitDTO>> GetClientsVisits(int idClient);
}
