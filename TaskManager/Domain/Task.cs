namespace TaskManager.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Task")]
    public partial class Task
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(255)]
        public string Title { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime GenerationTimeStamp { get; set; }
    }
}
