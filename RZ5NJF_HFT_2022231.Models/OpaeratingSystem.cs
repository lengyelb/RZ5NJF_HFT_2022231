using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZ5NJF_HFT_2022231.Models
{
    internal class OpaeratingSystem
    {
        public OpaeratingSystem()
        {
        }

        #region DataFields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OpaeratingSystemID { get; set; }

        [StringLength(240)]
        public string Name { get; set; }

        [StringLength(240)]
        public string Kernel { get; set; }

        [StringLength(240)]
        public string OSFamily { get; set; }

        public DateTime ReleaseDate { get; set; }

        [StringLength(240)]
        public string PackageManager { get; set; }

        public bool IsSupported { get; set; }
        #endregion
    }
}
