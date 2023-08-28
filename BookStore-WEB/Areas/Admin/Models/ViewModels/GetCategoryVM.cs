using MovieStore_ApplicationCore.Entities.Abstract;

namespace MovieStore_WEB.Areas.Admin.Models.ViewModels
{
    public class GetCategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate{ get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Status Status { get; set; }
    }
}
