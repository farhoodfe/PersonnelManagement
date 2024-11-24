using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Data.Entities
{
    /// <summary>
    /// جدول ذخیره مقادیر فیلد های داینامیک
    /// </summary>
    [Table("FieldSubmission", Schema = "MIS")]
    public class FieldSubmission
    {
        [Key]
        [Column("pk_Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("FieldValue")]
        public string? FieldValue { get; set; }

        [Column("fk_FieldDefinition")]
        public long? Fk_FieldDefinition { get; set; }

        [ForeignKey("Fk_FieldDefinition")]
        public virtual DynamicFieldDefinition? fieldDefinition { get; set; }

        [Column("fk_PersonInfo")]
        public long Fk_PersonInfo { get; set; } 

        [ForeignKey("Fk_PersonInfo")]
        public virtual PersonInfo PersonInfo { get; set; }

        


    }
}
