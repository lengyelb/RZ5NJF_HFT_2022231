using Microsoft.EntityFrameworkCore;
using System;
using RZ5NJF_HFT_2022231.Models;
using System.Xml.Linq;
using System.Runtime.ConstrainedExecution;

namespace RZ5NJF_HFT_2022231.Repository
{
    public class SmartPhonesDbContext : DbContext
    {
        public SmartPhonesDbContext()
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<SmartPhoneOS> SmartPhoneOSes { get; set; }
        public DbSet<Phone> Phones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseInMemoryDatabase("smartphones");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>(phone => phone
            .HasOne<Company>(phone => phone.Company)
            .WithMany(company => company.Phones)
            .HasForeignKey(phone => phone.CompanyID)
            .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Phone>(phone => phone
            .HasOne<SmartPhoneOS>(phone => phone.SmartPhoneOS)
            .WithMany(os => os.Phones)
            .HasForeignKey(phone => phone.SmartPhoneOSID)
            .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Company>().HasData(new Company[] 
            {
                new Company() {CompanyID = 1, Name = "Google LLC", CEO = "Sundar Pichai", NetWorth = 1135, Headquarters="Mountain View, California, U.S.", NumberOfEmployees=139995, Founded= new Date(1998,9,24)},
                new Company() {CompanyID = 2, Name = "Microsoft Corporation", CEO = "Satya Nadella", NetWorth = 2135, Headquarters="Redmond, Washington, U.S.", NumberOfEmployees=221000, Founded= new Date(1975,4,4)},
                new Company() {CompanyID = 3, Name = "Apple Inc.", CEO = "Tim Cook", NetWorth = 2389, Headquarters="Cupertino, California, U.S.", NumberOfEmployees=164000 , Founded= new Date(1976,4,1)},
                new Company() {CompanyID = 4, Name = "Huawei Technologies Co., Ltd.", CEO = "Zsen Cseng-fej", NetWorth = 108, Headquarters="Shenzhen, China", NumberOfEmployees=19500, Founded= new Date(1987,9,15)},
                new Company() {CompanyID = 5, Name = "Samsung Eelectronics Co., Ltd", CEO = "Lee Jae-yong", NetWorth = 500, Headquarters="Seocho District, Seoul, South Korea", NumberOfEmployees=266673, Founded= new Date(1938,3,1)},
                new Company() {CompanyID = 6, Name = "Xiaomi Corporation", CEO = "Lej Csün", NetWorth = 32, Headquarters="Haidian District, Beijing, China", NumberOfEmployees=33427, Founded= new Date(2010,4,10)}
            });

            modelBuilder.Entity<Phone>().HasData(new Phone[]
            {
                new Phone() {PhoneID = 1, Name = "Nexus 6", Series = "Nexus", ReleaseDate = new Date(2014, 11, 1), DataInput = "Micro USB", BatterySize = 3220, WirelessCharging = true, SmartPhoneOSID = 1, CompanyID=1},
                new Phone() {PhoneID = 2, Name = "Pixel 7", Series = "Pixel", ReleaseDate = new Date(2022, 10, 13), DataInput = "USB-C", BatterySize = 4355, WirelessCharging = true, SmartPhoneOSID = 2, CompanyID=1},
                new Phone() {PhoneID = 3, Name = "Microsoft Lumia 535", Series = "Lumia", ReleaseDate = new Date(2015, 5, 16), DataInput = "Micro USB 2.0", BatterySize = 1905, WirelessCharging = false, SmartPhoneOSID = 3, CompanyID=2},
                new Phone() {PhoneID = 4, Name = "Microsoft Lumia 650", Series = "Lumia", ReleaseDate = new Date(2016, 2, 15), DataInput = "Micro USB 2.0", BatterySize = 2000, WirelessCharging = false, SmartPhoneOSID = 4, CompanyID=2},
                new Phone() {PhoneID = 5, Name = "iPhone 5", Series = "iPhone", ReleaseDate = new Date(2012, 9, 21), DataInput = "Lightning", BatterySize = 1440, WirelessCharging = false, SmartPhoneOSID = 5, CompanyID=3},
                new Phone() {PhoneID = 6, Name = "iPhone 13", Series = "iPhone", ReleaseDate = new Date(2021, 9, 24), DataInput = "Lightning", BatterySize = 3227, WirelessCharging = true, SmartPhoneOSID = 6, CompanyID=3},
                new Phone() {PhoneID = 7, Name = "Huawei Mate 10", Series = "Mate ", ReleaseDate = new Date(2017, 10, 16), DataInput = "USB-C", BatterySize = 4000, WirelessCharging = true, SmartPhoneOSID = 7, CompanyID=4},
                new Phone() {PhoneID = 8, Name = "Huawei P50", Series = "Huawei P Series", ReleaseDate = new Date(2021, 12, 4), DataInput = "USB-C", BatterySize = 4100, WirelessCharging = true, SmartPhoneOSID = 8, CompanyID=4},
                new Phone() {PhoneID = 9, Name = "Samsung Galaxy S20", Series = "Samsung S series", ReleaseDate = new Date(2020, 3, 6), DataInput = "USB-C", BatterySize = 4000, WirelessCharging = true, SmartPhoneOSID = 7, CompanyID = 5},
                new Phone() {PhoneID = 10, Name = "Samsung Galaxy A04", Series = "Samsung A series", ReleaseDate = new Date(2022, 8, 24), DataInput = "USB-C", BatterySize = 5000, WirelessCharging = false, SmartPhoneOSID = 9, CompanyID = 5},
                new Phone() {PhoneID = 11, Name = "Xiaomi Mi 9 Pro", Series = "Xiaomi Mi series", ReleaseDate = new Date(2019, 9, 10), DataInput = "USB-C", BatterySize = 4000, WirelessCharging = true, SmartPhoneOSID = 7, CompanyID = 6},
                new Phone() {PhoneID = 12, Name = "POCO F3", Series = "Xiaomi POCO series", ReleaseDate = new Date(2021, 7, 12), DataInput = "USB-C", BatterySize = 4520, WirelessCharging = true, SmartPhoneOSID = 9, CompanyID = 6},
            });

            modelBuilder.Entity<SmartPhoneOS>().HasData(new SmartPhoneOS[]
            {
                new SmartPhoneOS() {SmartPhoneOSID = 1, Name ="Android 5", Kernel="Monolithic Kernel", OSFamily="Android", ReleaseDate = new Date(2014, 11, 4), PackageManager="Google Play Store", IsSupported=false},
                new SmartPhoneOS() {SmartPhoneOSID = 2, Name ="Android 13", Kernel="Monolithic Kernel", OSFamily="Android", ReleaseDate = new Date(2022, 8, 15), PackageManager="Google Play Store", IsSupported=true},
                new SmartPhoneOS() {SmartPhoneOSID = 3, Name ="Windows Phone 8.1", Kernel="Hybrid NT Kernel", OSFamily="Windows", ReleaseDate = new Date(2014, 8, 4), PackageManager="APPX", IsSupported=false},
                new SmartPhoneOS() {SmartPhoneOSID = 4, Name ="Windows 10 mobile", Kernel="Hybrid Microsoft", OSFamily="Windows", ReleaseDate = new Date(2016, 3, 17), PackageManager="Microsoft Store", IsSupported=false},
                new SmartPhoneOS() {SmartPhoneOSID = 5, Name ="iOS 6", Kernel="Hybrid XNU", OSFamily="iOS", ReleaseDate = new Date(2012, 9, 19), PackageManager="App Store", IsSupported=false},
                new SmartPhoneOS() {SmartPhoneOSID = 6, Name ="iOS 15", Kernel="Hybrid XNU", OSFamily="iOS", ReleaseDate = new Date(2021, 9, 20), PackageManager="App Store", IsSupported=true},
                new SmartPhoneOS() {SmartPhoneOSID = 7, Name ="Android 10", Kernel="Monolithic Kernel", OSFamily="Android", ReleaseDate = new Date(2019, 9, 3), PackageManager="Google Play Store", IsSupported=false},
                new SmartPhoneOS() {SmartPhoneOSID = 8, Name ="HarmonyOS 2", Kernel="HarmonyOS microkernel", OSFamily="HarmonyOS", ReleaseDate = new Date(2021, 5, 13), PackageManager="Ark Compiler", IsSupported=true},
                new SmartPhoneOS() {SmartPhoneOSID = 9, Name ="Android 12", Kernel="Monolithic Kernel", OSFamily="Android", ReleaseDate = new Date(2021, 10, 13), PackageManager="Google Play Store", IsSupported=true},
            });
        }
    }
}
