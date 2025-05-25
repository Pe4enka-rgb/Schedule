using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.DB.Entity;

namespace Schedule.Configurations {
	public class SubjectConfiguration : IEntityTypeConfiguration<Subject> {
		public void Configure(EntityTypeBuilder<Subject> builder) {
			builder.HasKey(s => s.Id);
		}
	}
	public class BellConfiguration : IEntityTypeConfiguration<Bell> {
		public void Configure(EntityTypeBuilder<Bell> builder) {
			builder.HasKey(b => b.Id);
		}
	}
	public class DayConfiguration : IEntityTypeConfiguration<Day> {
		public void Configure(EntityTypeBuilder<Day> builder) {
			builder.HasKey(day => day.Id);
		}
	}
}
