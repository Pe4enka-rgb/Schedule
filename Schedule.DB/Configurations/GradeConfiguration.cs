using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.DB.Entity;

namespace Schedule.Configurations {
	public class GradeConfiguration : IEntityTypeConfiguration<Grade> {
		public void Configure(EntityTypeBuilder<Grade> builder) {
			builder
				.HasKey(g => g.Id)
				
			;
		}
	}
}
