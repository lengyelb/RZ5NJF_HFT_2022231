using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

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

        public DateTime ReleaseDate { get; set; }

        [StringLength(240)]
        public string DataInput { get; set; }

        [Range(0, 10000)]
        public int BatterySize { get; set; }

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

        #region Overrides
        public override string ToString()
        {
            return $"{PhoneID} - {Name}: This phone was created by: {Company.Name}, it runs on {SmartPhoneOS.Name} and was released in: {ReleaseDate.Year}";
        }

        public override bool Equals(object obj)
        {
            Phone other = obj as Phone;
            return other != null && this.PhoneID == other.PhoneID
                && this.Name == other.Name && this.Series == other.Series && this.ReleaseDate == other.ReleaseDate
                && this.DataInput == other.DataInput && this.BatterySize == other.BatterySize
                && this.WirelessCharging == other.WirelessCharging;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PhoneID.GetHashCode(), Name.GetHashCode(), Series.GetHashCode(),
                ReleaseDate.GetHashCode(), DataInput.GetHashCode(), BatterySize.GetHashCode(),
                WirelessCharging.GetHashCode());
        }
        #endregion
    }
}
