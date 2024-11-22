using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PersonnelManagement.Data.Statics.SD;

namespace PersonnelManagement.Data.Entities
{
    /// <summary>
    /// Class for Defining dynamic PersonInfo fields
    /// </summary>
    [Table("DynamicFieldDefinition", Schema = "MIS")]
    public class DynamicFieldDefinition
    {
        #region Propertys
        [Key]
        [Column("pk_Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("FieldName")]
        [DefaultValue(" ")]
        public string? FieldName { get; set; }

        [Column("DisplayName")]
        [DefaultValue(" ")]
        public string? DisplayName { get; set; }

        [Column("FieldType")]
        public FieldType? Type { get; set; }

        [Column("IsRequired")]
        [DefaultValue(0)]
        public bool? IsRequired { get; set; }

        [Column("IsDeleted")]
        [DefaultValue(0)]
        public bool? IsDeleted { get; set; }
        #endregion
    }
}
