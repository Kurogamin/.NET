using dotnet.DataAccess.Data;
using dotnet.DataAccess.Repository.IRepository;
using dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.DataAccess.Repository;

public class GenreRepository : Repository<Genre>, IGenreRepository
{
	private readonly ApplicationDbContext _dbContext;

	public GenreRepository(ApplicationDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}

	public void Update(Genre genre)
	{
		_dbContext.Genres.Update(genre);
	}
}