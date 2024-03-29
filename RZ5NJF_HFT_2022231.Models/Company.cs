﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Reflection;

namespace RZ5NJF_HFT_2022231.Models
{
    public class Company
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

        public DateTime Founded { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Phone> Phones { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return $"{CompanyID} - {Name}: The company provides jobs for {NumberOfEmployees} people, and has a net worth of: {NetWorth}";
        }

        public override bool Equals(object obj)
        {
            Company other = obj as Company;
            return other != null && this.CompanyID==other.CompanyID
                && this.Name==other.Name && this.CEO==other.CEO && this.NetWorth == other.NetWorth
                && this.Headquarters == other.Headquarters && this.Founded == other.Founded;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CompanyID.GetHashCode(), Name.GetHashCode(), CEO.GetHashCode(),
                NetWorth.GetHashCode(), Headquarters.GetHashCode(), Founded.GetHashCode());
        }
        #endregion
    }
}
