using Medical.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medical.Infra.Data.Configurations
{

    /// <summary>
    /// Doctor configuration class object
    /// </summary>
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {

        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {

            builder.ToTable(nameof(Doctor));

            #region PK

            builder.HasKey(k => k.Id);

            #endregion

            #region Columns

            builder.Property(c => c.FirstName)
                .HasColumnName(nameof(Doctor.FirstName))
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(c => c.LastName)
                .HasColumnName(nameof(Doctor.LastName))
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(c => c.Crm)
                .HasColumnName(nameof(Doctor.Crm))
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsRequired();

            #endregion

            #region Indexes

            builder.HasIndex(i => i.Crm)
                .HasName($"AK_{nameof(Doctor)}_{nameof(Doctor.Crm)}")
                .IsUnique();

            builder.HasIndex(i => i.FirstName)
                .HasName($"IX_{nameof(Doctor)}_{nameof(Doctor.FirstName)}");

            builder.HasIndex(i => i.LastName)
                .HasName($"IX_{nameof(Doctor)}_{nameof(Doctor.LastName)}");

            #endregion

        }
    }
}
