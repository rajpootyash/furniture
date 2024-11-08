using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sgnfurniture.Models
{
    public class MasterModel
    {
        public string category_id { get; set; }
        public string category_name { get; set; }
        public string color_id { get; set; }
        public string color_name { get; set; }
        public string hex_code { get; set; }
        public string material_id { get; set; }
        public string material_name { get; set; }
        public string shape_id { get; set; }
        public string shape_name { get; set; }
        public string subcategory_id { get; set; }
        public string subcategory_name { get; set; }
        public string type_id { get; set; }
        public string type_name { get; set; }
        public string description { get; set; }
        public string AddedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string mode { get; set; }
        public List<MasterModel> lstMasterModel { get; set; }
        public MasterModel() { }
    }
}