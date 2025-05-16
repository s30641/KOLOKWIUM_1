using KOLOS_A.Data;
using KOLOS_A.Dtos;
using KOLOS_A.Models;
using Microsoft.EntityFrameworkCore;

namespace KOLOS_A.Services;

public class AppointmentAppService : IAppointmentAppService
{
    private readonly AppDbContext _context;

    public AppointmentAppService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AppointmentDetailsDto?> GetAppointmentDetailsAsync(int id)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Include(a => a.AppointmentServices)
                .ThenInclude(s => s.Service)
            .FirstOrDefaultAsync(a => a.AppointmentId == id);

        if (appointment == null) return null;

        return new AppointmentDetailsDto
        {
            date = appointment.Date,
            patient = new PatientDto
            {
                firstName = appointment.Patient.FirstName,
                lastName = appointment.Patient.LastName,
                dateOfBirth = appointment.Patient.DateOfBirth
            },
            doctor = new DoctorDto
            {
                doctorId = appointment.Doctor.DoctorId,
                pwz = appointment.Doctor.PWZ
            },
            appointmentServices = appointment.AppointmentServices.Select(s => new AppointmentServiceDto
            {
                name = s.Service.Name,
                serviceFee = s.ServiceFee
            }).ToList()
        };
    }

    public async Task<(bool Success, string? Error)> CreateAppointmentAsync(CreateAppointmentRequest request)
    {
        if (await _context.Appointments.AnyAsync(a => a.AppointmentId == request.AppointmentId))
            return (false, "Appointment already exists.");

        var patient = await _context.Patients.FindAsync(request.PatientId);
        if (patient == null)
            return (false, "Patient not found.");

        var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.PWZ == request.Pwz);
        if (doctor == null)
            return (false, "Doctor not found.");

        var appointmentServices = new List<AppointmentService>();
        foreach (var s in request.Services)
        {
            var service = await _context.Services.FirstOrDefaultAsync(x => x.Name == s.ServiceName);
            if (service == null)
                return (false, $"Service '{s.ServiceName}' not found.");

            appointmentServices.Add(new AppointmentService
            {
                AppointmentId = request.AppointmentId,
                ServiceId = service.ServiceId,
                ServiceFee = s.ServiceFee
            });
        }

        var appointment = new Appointment
        {
            AppointmentId = request.AppointmentId,
            PatientId = request.PatientId,
            DoctorId = doctor.DoctorId,
            Date = DateTime.Now,
            AppointmentServices = appointmentServices
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return (true, null);
    }
}
