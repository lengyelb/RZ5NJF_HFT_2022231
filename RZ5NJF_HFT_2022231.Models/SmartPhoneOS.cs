﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace RZ5NJF_HFT_2022231.Models
{
    public class SmartPhoneOS
    {
        public SmartPhoneOS()
        {
        }

        #region DataFields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SmartPhoneOSID { get; set; }

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

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Phone> Phones { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return $"{SmartPhoneOSID} - {Name}: This operating system was created in {ReleaseDate.Year}, it was built on {Kernel} and is {(IsSupported? "still": "no longer")} supported";
        }

        public override bool Equals(object obj)
        {
            SmartPhoneOS other = obj as SmartPhoneOS;
            return other != null && this.SmartPhoneOSID == other.SmartPhoneOSID
                && this.Name == other.Name && this.Kernel == other.Kernel && this.OSFamily == other.OSFamily
                && this.ReleaseDate == other.ReleaseDate && this.PackageManager == other.PackageManager
                && this.IsSupported == other.IsSupported;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SmartPhoneOSID.GetHashCode(), Name.GetHashCode(), Kernel.GetHashCode(),
                OSFamily.GetHashCode(), ReleaseDate.GetHashCode(), PackageManager.GetHashCode(),
                IsSupported.GetHashCode());
        }
        #endregion
    }
}
