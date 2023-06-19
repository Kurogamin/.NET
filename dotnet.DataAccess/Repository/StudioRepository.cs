using dotnet.DataAccess.Data;
using dotnet.DataAccess.Repository.IRepository;
using dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.DataAccess.Repository;

public class StudioRepository : Repository<Studio>, IStudioRepository
{
    private readonly ApplicationDbContext _dbContext;

    public StudioRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public void Update(Studio studio)
    {
        _dbContext.Studios.Update(studio);
    }
}