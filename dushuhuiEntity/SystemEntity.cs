using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dushuhuiEntity
{

    public class Category
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryInfo { get; set; }
        public int CategoryParentID { get; set; }
        public bool CategoryStatus { get; set; }
        public int CategorySort { get; set; }

        [NotMapped]
        public string CategoryParentName { get; set; }
        [NotMapped]
        public List<Category> ChildCategory { get; set; }
    }

    public class bvnode
    {
        public string text;
        public int pid;
        public int id;
        public List<bvnode> nodes;
    }

    public class Quanxian
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "权限名称")]
        public string QuanxianName { get; set; }
        [Display(Name = "权限说明")]
        public string QuanxianInfo { get; set; }
    }


}
