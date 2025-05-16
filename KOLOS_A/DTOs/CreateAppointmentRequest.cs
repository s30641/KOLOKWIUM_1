namespace KOLOS_A.Dtos;

public class CreateAppointmentRequest
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public string Pwz { get; set; } = default!;
    public List<ServiceDto> Services { get; set; } = new();
}

public class ServiceDto
{
    public string ServiceName { get; set; } = default!;
    public decimal ServiceFee { get; set; }
}