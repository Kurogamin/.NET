using dotnet.DataAccess.Data;
using dotnet.DataAccess.Repository.IRepository;
using dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.DataAccess.Repository;

public class GameRepository : Repository<Game>, IGameRepository
{
	private readonly ApplicationDbContext _dbContext;

	public GameRepository(ApplicationDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}

	public void Update(Game game)
	{
		var gameFromDb = _dbContext.Games.FirstOrDefault(x => x.Id == game.Id);

		if (gameFromDb is null)
		{
			return;
		}

		gameFromDb.Title = game.Title;
		gameFromDb.Description = game.Description;
		gameFromDb.StudioId = game.StudioId;
		gameFromDb.Genre = game.Genre;

		if (game.ImageUrl is not null)
		{
			gameFromDb.ImageUrl = game.ImageUrl;
		}
	}
}