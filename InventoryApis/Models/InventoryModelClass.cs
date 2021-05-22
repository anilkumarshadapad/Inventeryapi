using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryApis.Models
{
    public class InventoryInsertRequest
    {
        [Required(ErrorMessage = "Please enter Names")]
        public string Names { get; set; }

        [Required(ErrorMessage = "Please enter Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter Price")]
        public string Price { get; set; }

    }

    public class InventoryInsertResponse
    {
        public bool Result { get; set; }
        public string Meassage { get; set; }
        public string InventeryID { get; set; }
    }

    public class InventoryUpdateRequest
    {

        public string Names { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public int InventeryID { get; set; }
    }

    public class InventoryUpdateResponse
    {
        public bool Result { get; set; }
        public string Meassage { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }

    }


    public class InventoryResponse
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public int InventeryID { get; set; }

    }


    public class InventorydetailsResponse
    {
        public bool Result { get; set; }
        public string Meassage { get; set; }
        public List<InventoryResponse> List { get; set; }
        
    }


    
    public class InventorydetailsUseingFiltersRequest
    {

        public string Names { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public int InventeryID { get; set; }
    }

    public class InventorydetailsDeleteByIdRequest
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid InventeryID")]
        public int InventeryID { get; set; }      
    }


}