namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public int CategoryID { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

        [StringLength(50)]
        public string MataTitle { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(10)]
        public string Target { get; set; }

        public bool Status { get; set; }
    }
}
