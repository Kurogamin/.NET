using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using dotnet.DataAccess.Data;
using dotnet.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace dotnet.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
	private readonly ApplicationDbContext _dbContext;
	internal DbSet<T> dbSet;

	public Repository(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
		this.dbSet = _dbContext.Set<T>();
		_dbContext.Games.Include(x => x.Studio).Include(x => x.StudioId);
		_dbContext.Games.Include(x => x.Genre).Include(x => x.GenreId);
	}

	public void Add(T entity)
	{
		dbSet.Add(entity);
	}

	public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
	{
		IQueryable<T> query = dbSet;
		query = query.Where(filter);

		if (string.IsNullOrEmpty(includeProperties))
		{
			return query.FirstOrDefault();
		}

		foreach (var inclueProperty in includeProperties
			.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
		{
			query = query.Include(inclueProperty);
		}

		return query.FirstOrDefault();
	}

	public IEnumerable<T> GetAll(string? includeProperties = null)
	{
		IQueryable<T> query = dbSet;

		if (string.IsNullOrEmpty(includeProperties))
		{
			return query.ToList();
		}

		foreach (var includeProperty in includeProperties
			.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
		{
			query = query.Include(includeProperty);
		}

		return query.ToList();
	}

	public void Remove(T entity)
	{
		dbSet.Remove(entity);
	}

	public void RemoveRange(IEnumerable<T> entity)
	{
		dbSet.RemoveRange(entity);
	}
}