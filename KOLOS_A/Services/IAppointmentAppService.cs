using KOLOS_A.Dtos;

namespace KOLOS_A.Services;

public interface IAppointmentAppService
{
    Task<AppointmentDetailsDto?> GetAppointmentDetailsAsync(int id);
    Task<(bool Success, string? Error)> CreateAppointmentAsync(CreateAppointmentRequest request);
}