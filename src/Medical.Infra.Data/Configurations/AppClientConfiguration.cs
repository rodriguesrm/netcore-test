using Medical.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medical.Infra.Data.Configurations
{

    /// <summary>
    /// AppClient configuration class object
    /// </summary>
    public class AppClientConfiguration : IEntityTypeConfiguration<AppClient>
    {

        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<AppClient> builder)
        {

            builder.ToTable(nameof(AppClient));

            #region PK

            builder.HasKey(k => k.Id);

            #endregion

            #region Columns

            builder.Property(c => c.Name)
                .HasColumnName(nameof(AppClient.Name))
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(c => c.Key)
                .HasColumnName(nameof(AppClient.Key))
                .HasMaxLength(32)
                .IsUnicode(false)
                .IsRequired();

            #endregion

        }
    }
}
