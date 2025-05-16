using KOLOS_A.Models;
using Microsoft.EntityFrameworkCore;

namespace KOLOS_A.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentService> AppointmentServices { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().ToTable("Appointment");
            modelBuilder.Entity<AppointmentService>().ToTable("Appointment_Service");
            modelBuilder.Entity<Doctor>().ToTable("Doctor");
            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<Service>().ToTable("Service");
            
            modelBuilder.Entity<Appointment>()
                .HasKey(a => a.AppointmentId);

            modelBuilder.Entity<AppointmentService>()
                .HasKey(x => new { x.ServiceId, x.AppointmentId });
            
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentId).HasColumnName("appoitment_id");
                entity.Property(e => e.PatientId).HasColumnName("patient_id");
                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
                entity.Property(e => e.Date).HasColumnName("date");

                entity.HasOne(a => a.Patient)
                      .WithMany()
                      .HasForeignKey(a => a.PatientId);

                entity.HasOne(a => a.Doctor)
                      .WithMany()
                      .HasForeignKey(a => a.DoctorId);
            });
            
            modelBuilder.Entity<AppointmentService>(entity =>
            {
                entity.Property(e => e.AppointmentId).HasColumnName("appoitment_id");
                entity.Property(e => e.ServiceId).HasColumnName("service_id");
                entity.Property(e => e.ServiceFee).HasColumnName("service_fee");

                entity.HasOne(x => x.Appointment)
                      .WithMany(a => a.AppointmentServices)
                      .HasForeignKey(x => x.AppointmentId);

                entity.HasOne(x => x.Service)
                      .WithMany()
                      .HasForeignKey(x => x.ServiceId);
            });
            
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
                entity.Property(e => e.FirstName).HasColumnName("first_name");
                entity.Property(e => e.LastName).HasColumnName("last_name");
                entity.Property(e => e.PWZ).HasColumnName("PWZ");
            });
            
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.PatientId).HasColumnName("patient_id");
                entity.Property(e => e.FirstName).HasColumnName("first_name");
                entity.Property(e => e.LastName).HasColumnName("last_name");
                entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            });
            
            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.ServiceId).HasColumnName("service_id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.BaseFee).HasColumnName("base_fee");
            });
        }
    }
}
