using Microsoft.EntityFrameworkCore;
using Schedule.DB.Context;
using Schedule.DB.Entity.Base;
using Schedule.Interfaces;

namespace Schedule.DB.Repositories {
	internal class DbRepository<T> : IRepository<T> where T : BaseEntity, new() {
		private readonly ScheduleDbContext _db;
		private readonly DbSet<T> _dbSet;
		public bool AutoSaveChanges { get; set; } = true;

		public DbRepository(ScheduleDbContext db) {
			_db = db;
			_dbSet = db.Set<T>();
		}

		public virtual IQueryable<T> Items => _dbSet;

		public T Get(int id) => Items.SingleOrDefault(item => item.Id == id);

		public async Task<T> GetAsync(int id, CancellationToken Cancel = default) => await Items
			.SingleOrDefaultAsync(item => item.Id == id)
			.ConfigureAwait(false);

		public T Add(T item) {
			if (item == null)
				throw new ArgumentNullException(nameof(item));
			_db.Entry(item).State = EntityState.Added;
			if (AutoSaveChanges)
				_db.SaveChanges();
			return item;
		}

		public async Task<T> AddAsync(T item, CancellationToken Cancel = default) {
			if (item == null)
				throw new ArgumentNullException(nameof(item));
			_db.Entry(item).State = EntityState.Added;
			if (AutoSaveChanges)
				await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
			return item;
		}

		public void Update(T item) {
			if (item == null)
				throw new ArgumentNullException(nameof(item));
			_db.Entry(item).State = EntityState.Modified;
			if (AutoSaveChanges)
				_db.SaveChanges();
		}

		public async Task UpdateAsync(T item, CancellationToken Cancel = default) {
			if (item == null)
				throw new ArgumentNullException(nameof(item));
			_db.Entry(item).State = EntityState.Modified;
			if (AutoSaveChanges)
				await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
		}

		public void Remove(int id) {
			_db.Remove(new T { Id = id });
			if (AutoSaveChanges)
				_db.SaveChanges();
		}

		public async Task RemoveAsync(int id, CancellationToken Cancel = default) {
			_db.Remove(new T { Id = id });
			if (AutoSaveChanges)
				await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
		}

		public void Remove(T item) {
			_db.Remove(item);
			if (AutoSaveChanges)
				_db.SaveChanges();

		}

		public async Task RemoveAsync(T item, CancellationToken Cancel = default) {
			_db.Remove(item);
			if (AutoSaveChanges)
				await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
		}

	}
}
