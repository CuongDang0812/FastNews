namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        public int PostID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(250)]
        public string MataTitle { get; set; }

        [StringLength(1000)]
        public string Decription { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [Column(TypeName = "ntext")]
        public string ContentDetail { get; set; }

        public int? CategoryID { get; set; }

        public DateTime? DatetimeCreate { get; set; }
    }
}
