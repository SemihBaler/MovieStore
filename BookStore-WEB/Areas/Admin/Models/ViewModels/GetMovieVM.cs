using MovieStore_ApplicationCore.Entities.Abstract;

namespace MovieStore_WEB.Areas.Admin.Models.ViewModels
{
    public class GetMovieVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DirectorName { get; set; }
        public string Image { get; set; }
        public int Year { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Status Status { get; set; }
        public List<string> Categories { get; set; }
    }
}
