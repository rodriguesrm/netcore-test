using Medical.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medical.Infra.Data.Configurations
{

    /// <summary>
    /// Patient configuration class object
    /// </summary>
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {

        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<Patient> builder)
        {

            builder.ToTable(nameof(Patient));

            #region PK

            builder.HasKey(k => k.Id);

            #endregion

            #region Columns

            builder.Property(c => c.FirstName)
                .HasColumnName(nameof(Patient.FirstName))
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(c => c.LastName)
                .HasColumnName(nameof(Patient.LastName))
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(c => c.Cpf)
                .HasColumnName(nameof(Patient.Cpf))
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsRequired();

            #endregion

            #region Indexes

            builder.HasIndex(i => i.Cpf)
                .HasName($"AK_{nameof(Patient)}_{nameof(Patient.Cpf)}")
                .IsUnique();

            builder.HasIndex(i => i.FirstName)
                .HasName($"IX_{nameof(Patient)}_{nameof(Patient.FirstName)}");

            builder.HasIndex(i => i.LastName)
                .HasName($"IX_{nameof(Patient)}_{nameof(Patient.LastName)}");

            #endregion

        }
    }
}
