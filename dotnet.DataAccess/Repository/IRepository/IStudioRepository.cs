using dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.DataAccess.Repository.IRepository;

public interface IStudioRepository : IRepository<Studio>
{
    void Update(Studio studio);
}