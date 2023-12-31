﻿using dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.DataAccess.Repository.IRepository;

public interface IGameRepository : IRepository<Game>
{
    void Update(Game game);
}