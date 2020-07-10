namespace Model.EnityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Menu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(500)]
        public string URL { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }

        public int? DisplayOrder { get; set; }

        public int? GroupID { get; set; }

        [StringLength(10)]
        public string Target { get; set; }

        public bool? Status { get; set; }

        public virtual MenuGroup MenuGroup { get; set; }
    }
}
