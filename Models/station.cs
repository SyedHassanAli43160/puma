using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace puma.Models
{
    public class stationMaster
    {
        [Key]
        public int StationId { get; set; }
        public string Name { get; set; }
        public string area { get; set; }
        public string Description { get; set; }
        public string city { get; set; }
        public double latitude { get; set; }
        public double longtitude { get;set;}

    }
    public class categoryMaster
    {
        [Key]
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public bool isdisabled { get; set; }

    }
    public class stationCategory
    {
        [Key]
        public int stationCategoryId { get; set; }
        [ForeignKey("stationMaster")]
        public int StationId { get; set; }
        [ForeignKey("category")]
        public int categoryId { get; set; }

       
        //public ICollection<stationMaster> stationMasters { get; set; }
        //public ICollection<categoryMaster> categoryMasters { get; set; }
    }
 

}
