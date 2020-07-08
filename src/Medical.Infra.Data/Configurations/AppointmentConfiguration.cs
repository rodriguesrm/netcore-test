using Medical.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medical.Infra.Data.Configurations
{

    /// <summary>
    /// Appointment configuration class object
    /// </summary>
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {

        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {

            builder.ToTable(nameof(Appointment));

            #region PK

            builder.HasKey(k => k.Id);

            #endregion

            #region Columns

            builder.Property(c => c.DoctorId)
                .HasColumnName(nameof(Appointment.DoctorId))
                .IsRequired();

            builder.Property(c => c.PatientId)
                .HasColumnName(nameof(Appointment.PatientId))
                .IsRequired();

            builder.Property(c => c.DateTime)
                .HasColumnName(nameof(Appointment.DateTime))
                .IsRequired();

            #endregion

            #region FKs

            builder.HasOne(o => o.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(fk => fk.DoctorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK_{nameof(Appointment)}_{nameof(Doctor)}");

            builder.HasOne(o => o.Patient)
                .WithMany(d => d.Appointments)
                .HasForeignKey(fk => fk.PatientId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK_{nameof(Appointment)}_{nameof(Patient)}");

            #endregion

            #region Indexes

            builder.HasIndex(i => i.DoctorId)
                .HasName($"IX_{nameof(Appointment)}_{nameof(Appointment.DoctorId)}");

            builder.HasIndex(i => i.PatientId)
                .HasName($"IX_{nameof(Appointment)}_{nameof(Appointment.PatientId)}");

            builder.HasIndex(i => i.DateTime)
                .HasName($"IX_{nameof(Appointment)}_{nameof(Appointment.DateTime)}");

            #endregion

        }
    }
}
