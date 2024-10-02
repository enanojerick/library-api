using System.ComponentModel.DataAnnotations;

namespace Library.Data.Entities
{
    public class Books
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Isbn { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
