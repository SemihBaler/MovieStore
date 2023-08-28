using MovieStore_ApplicationCore.DTO_s.DirectorDTO;
using MovieStore_ApplicationCore.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore_ApplicationCore.Interfaces
{
    public interface IDirectorService : IRepositoryService<Director>
    {
        Task<List<GetDirectorDTO>> GetDirectors();
    }
}
