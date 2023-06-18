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

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public void Update(Game game)
    {
        _dbContext.Games.Update(game);
    }
}