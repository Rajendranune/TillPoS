using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TillPoS.Models
{
    public class CategoryModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
    public class SubCategoryModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string Categoryid { get; set; }

    }
    public class ProductModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "SubCategory Name")]
        public string SubCategoryid { get; set; }
        public List<Dropdownlist> dep { get; set; }
    }

    public class Dropdownlist
    {
        public string Name { get; set; }
        public string Categoryid { get; set; }
    }
    public class Dropdownlist1
    {
        public string Name { get; set; }
        public string SubCategoryid { get; set; }
    }
}