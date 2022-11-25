using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RZ5NJF_HFT_2022231.Models
{
    public class Phone
    {
        public Phone()
        {
        }
        #region DataFields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhoneID { get; set; }

        [StringLength(240)]
        public string Name { get; set; }

        [StringLength(240)]
        public string Series { get; set; }

        [StringLength(240)]
        public string IPProtection { get; set; }

        public DateTime ReleaseDate { get; set; }

        [StringLength(240)]
        public string DataInput { get; set; }

        [Range(0, 1000)]
        public int ChargingSpeed { get; set; }

        public bool WirelessCharging { get; set; }


        [ForeignKey(nameof(SmartPhoneOS))]
        public int SmartPhoneOSID { get; set; }

        [NotMapped]
        public virtual SmartPhoneOS SmartPhoneOS { get; set; }

        [ForeignKey(nameof(Company))]
        public int CompanyID { get; set; }

        [NotMapped]
        public virtual Company Company { get; set; }
        #endregion
    }
}
