using dotnet.DataAccess.Data;
using dotnet.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
	public IGameRepository GameRepository { get; private set; }
	public IStudioRepository StudioRepository { get; private set; }
	public IGenreRepository GenreRepository { get; private set; }

	private readonly ApplicationDbContext _dbContext;

	public UnitOfWork(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
		GameRepository = new GameRepository(_dbContext);
		StudioRepository = new StudioRepository(_dbContext);
		GenreRepository = new GenreRepository(_dbContext);
	}

	public void Save()
	{
		_dbContext.SaveChanges();
	}
}