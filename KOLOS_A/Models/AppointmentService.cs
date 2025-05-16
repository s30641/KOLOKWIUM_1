namespace KOLOS_A.Models
{
    public class AppointmentService
    {
        public int AppointmentId { get; set; }
        public int ServiceId { get; set; }
        public decimal ServiceFee { get; set; }

        public Appointment Appointment { get; set; }
        public Service Service { get; set; }
    }
}