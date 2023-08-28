using MovieStore_ApplicationCore.Entities.Abstract;

namespace MovieStore_WEB.Areas.Admin.Models.ViewModels
{
    public class GetDirectorVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Status Status { get; set; }
    }
}
