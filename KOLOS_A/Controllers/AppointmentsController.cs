using KOLOS_A.Dtos;
using KOLOS_A.Services;
using Microsoft.AspNetCore.Mvc;

namespace KOLOS_A.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentAppService _service;

    public AppointmentsController(IAppointmentAppService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var appointment = await _service.GetAppointmentDetailsAsync(id);
        if (appointment == null)
            return NotFound("Appointment not found");

        return Ok(appointment);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentRequest request)
    {
        var (success, error) = await _service.CreateAppointmentAsync(request);
        if (!success)
            return BadRequest(error);

        return CreatedAtAction(nameof(Get), new { id = request.AppointmentId }, null);
    }
}