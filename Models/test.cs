using System.ComponentModel.DataAnnotations;

namespace puma.Models
{
    public class test
    {
        [Key]
        public int Id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
