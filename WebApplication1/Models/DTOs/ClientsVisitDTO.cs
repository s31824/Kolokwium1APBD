namespace WebApplication1.Models.DTOs;

public class ClientsVisitDTO
{
    public int Id { get; set; } 

    public string Imie { get; set; } 

    public string Nazwisko { get; set; } 
    
    public DateTime DataUrodzenia  { get; set; } 
    public ClientsMechanicDTO MechanicDTO { get; set; }
    public ClientsServiceVisitDTO VisitDtos { get; set; }
    
}

public class ClientsMechanicDTO
{
    public int MechanicId { get; set; }
    public string LicenseNumber { get; set; }
}

public class ClientsServiceVisitDTO
{
    public string NazwaSerwisu { get; set; }
    public float  ServiceFee { get; set; }
}
