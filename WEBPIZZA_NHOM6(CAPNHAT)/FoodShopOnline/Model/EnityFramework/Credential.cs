namespace Model.EnityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Credential
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string UserGroupID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string RoleID { get; set; }
    }
}
