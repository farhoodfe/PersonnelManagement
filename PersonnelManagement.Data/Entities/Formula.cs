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
    /// جدول ذخیره مقادیر فرمول بصورت رشته
    /// </summary>
    [Table("Formula", Schema = "MIS")]
    public class Formula
    {
        [Key]
        [Column("pk_Id")]
        public long Id { get; set; }

        [Column("FormulaName")]
        [DefaultValue("")]
        public string? FormulaName { get; set; }

        [Column("FormulaText")]
        [DefaultValue(" ")]
        public string FormulaText { get; set; } = "";

    }

}
