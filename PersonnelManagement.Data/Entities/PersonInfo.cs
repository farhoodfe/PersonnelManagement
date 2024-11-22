using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace PersonnelManagement.Data.Entities
{
    /// <summary>
    /// Class for static PersonInfo fields
    /// </summary>
    [Table("PersonInfo", Schema = "MIS")]
    public class PersonInfo
    {
        #region Propertys
        [Key]
        [Column("pk_Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Pk_Id { get; set; }

        [Column("FName")]
        [DefaultValue(" ")]
        public string? FName { get; set; }

        [Column("LName")]
        [DefaultValue(" ")]
        public string? LName { get; set; }

        [Column("PersonnelCode")]
        [DefaultValue(" ")]
        public string? PersonnelCode { get; set; }
        #endregion

        #region ForeignKeys
        /// <summary>
        /// لیست فیلدهای داینامیک و مقادیر انها
        /// </summary>
        public virtual ICollection<FieldSubmission>? FieldSubmissions { get; set; }

        #endregion
    }
}
