namespace KOLOS_A.Dtos;

public class AppointmentDetailsDto
{
    public DateTime date { get; set; }
    public PatientDto patient { get; set; } = default!;
    public DoctorDto doctor { get; set; } = default!;
    public List<AppointmentServiceDto> appointmentServices { get; set; } = new();
}

public class PatientDto
{
    public string firstName { get; set; } = default!;
    public string lastName { get; set; } = default!;
    public DateTime dateOfBirth { get; set; }
}

public class DoctorDto
{
    public int doctorId { get; set; }
    public string pwz { get; set; } = default!;
}

public class AppointmentServiceDto
{
    public string name { get; set; } = default!;
    public decimal serviceFee { get; set; }
}