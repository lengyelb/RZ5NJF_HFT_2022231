using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZ5NJF_HFT_2022231.Models
{
    internal class Company
    {
        public Company()
        {
        }

        #region DataFields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyID { get; set; }

        [StringLength(240)]
        public string Name { get; set; }

        [StringLength(240)]
        public string CEO { get; set; }

        [Range(0, int.MaxValue)]
        public int NetWorth { get; set; }

        [StringLength(240)]
        public string Headquarters { get; set; }

        [Range(0, 1000000)]
        public int NumberOfEmployees { get; set; }

        [StringLength(240)]
        public string Website { get; set; }

        public DateTime Founded { get; set; }
        #endregion


    }
}
