using MovieStore_ApplicationCore.DTO_s.DirectorDTO;
using MovieStore_ApplicationCore.Entities.Concrete;
using MovieStore_ApplicationCore.Interfaces;
using MovieStore_Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore_Infrastructure.Data
{
    public class DirectorService : EfRepository<Director>, IDirectorService
    {
        private readonly AppDbContext _context;

        public DirectorService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<GetDirectorDTO>> GetDirectors()
        {
            var directors = await _context.Directors.Where(x => x.Status != MovieStore_ApplicationCore.Entities.Abstract.Status.Passive).ToListAsync();
            var model = new List<GetDirectorDTO>();

            foreach (var director in directors)
            {
                var dto = new GetDirectorDTO { Id = director.Id, FullName = director.FirstName + " " + director.LastName };
                model.Add(dto);
            }

            return model;
        }
    }
}
