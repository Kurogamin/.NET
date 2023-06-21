using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
	IGameRepository GameRepository { get; }
	IStudioRepository StudioRepository { get; }
	IGenreRepository GenreRepository { get; }

	void Save();
}