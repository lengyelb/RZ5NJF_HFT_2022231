using Moq;
using NUnit.Framework;
using RZ5NJF_HFT_2022231.Logic;
using RZ5NJF_HFT_2022231.Models;
using RZ5NJF_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;

namespace RZ5NJF_HFT_2022231.Test
{
    [TestFixture]
    public class TestClass
    {
        CompanyLogic companyLogic;
        PhoneLogic phoneLogic;
        SmartPhoneOSLogic smartPhoneOSLogic;

        Mock<IRepository<Company>> mockCompanyRepo;
        Mock<IRepository<Phone>> mocPhoneRepo;
        Mock<IRepository<SmartPhoneOS>> mockSmartPhoneOSRepo;


        [SetUp]
        public void Init()
        {
            #region Init Company
            mockCompanyRepo = new Mock<IRepository<Company>>();

            Company google = new Company() { CompanyID = 1, Name = "Google LLC", CEO = "Sundar Pichai", NetWorth = 1135, Headquarters = "Mountain View, California, U.S.", NumberOfEmployees = 139995, Founded = new DateTime(1998, 9, 24) };
            Company microsoft = new Company() { CompanyID = 2, Name = "Microsoft Corporation", CEO = "Satya Nadella", NetWorth = 2135, Headquarters = "Redmond, Washington, U.S.", NumberOfEmployees = 221000, Founded = new DateTime(1975, 4, 4) };
            Company apple = new Company() { CompanyID = 3, Name = "Apple Inc.", CEO = "Tim Cook", NetWorth = 2389, Headquarters = "Cupertino, California, U.S.", NumberOfEmployees = 164000, Founded = new DateTime(1976, 4, 1) };
            Company huawei = new Company() { CompanyID = 4, Name = "Huawei Technologies Co., Ltd.", CEO = "Zsen Cseng-fej", NetWorth = 108, Headquarters = "Shenzhen, China", NumberOfEmployees = 19500, Founded = new DateTime(1987, 9, 15) };
            Company samsung = new Company() { CompanyID = 5, Name = "Samsung Eelectronics Co., Ltd", CEO = "Lee Jae-yong", NetWorth = 500, Headquarters = "Seocho District, Seoul, South Korea", NumberOfEmployees = 266673, Founded = new DateTime(1938, 3, 1) };
            Company xiaomi = new Company() { CompanyID = 6, Name = "Xiaomi Corporation", CEO = "Lej Csün", NetWorth = 32, Headquarters = "Haidian District, Beijing, China", NumberOfEmployees = 33427, Founded = new DateTime(2010, 4, 10) };

            mockCompanyRepo.Setup(f => f.ReadAll()).Returns(new List<Company>()
            {
                google,
                microsoft,
                apple,
                huawei,
                samsung,
                xiaomi
            }.AsQueryable());

            companyLogic = new CompanyLogic(mockCompanyRepo.Object);
            #endregion

            #region Init SmartPhoneOS
            mockSmartPhoneOSRepo = new Mock<IRepository<SmartPhoneOS>>();

            SmartPhoneOS android_5 = new SmartPhoneOS() { SmartPhoneOSID = 1, Name = "Android 5", Kernel = "Monolithic Kernel", OSFamily = "Android", ReleaseDate = new DateTime(2014, 11, 4), PackageManager = "Google Play Store", IsSupported = false };
            SmartPhoneOS android_13 = new SmartPhoneOS() { SmartPhoneOSID = 2, Name = "Android 13", Kernel = "Monolithic Kernel", OSFamily = "Android", ReleaseDate = new DateTime(2022, 8, 15), PackageManager = "Google Play Store", IsSupported = true };
            SmartPhoneOS windows_phone_8 = new SmartPhoneOS() { SmartPhoneOSID = 3, Name = "Windows Phone 8.1", Kernel = "Hybrid NT Kernel", OSFamily = "Windows", ReleaseDate = new DateTime(2014, 8, 4), PackageManager = "APPX", IsSupported = false };
            SmartPhoneOS windows_phone_10 = new SmartPhoneOS() { SmartPhoneOSID = 4, Name = "Windows 10 mobile", Kernel = "Hybrid Microsoft", OSFamily = "Windows", ReleaseDate = new DateTime(2016, 3, 17), PackageManager = "Microsoft Store", IsSupported = false };
            SmartPhoneOS iOS_6 = new SmartPhoneOS() { SmartPhoneOSID = 5, Name = "iOS 6", Kernel = "Hybrid XNU", OSFamily = "iOS", ReleaseDate = new DateTime(2012, 9, 19), PackageManager = "App Store", IsSupported = false };
            SmartPhoneOS iOS_15 = new SmartPhoneOS() { SmartPhoneOSID = 6, Name = "iOS 15", Kernel = "Hybrid XNU", OSFamily = "iOS", ReleaseDate = new DateTime(2021, 9, 20), PackageManager = "App Store", IsSupported = true };
            SmartPhoneOS android_10 = new SmartPhoneOS() { SmartPhoneOSID = 7, Name = "Android 10", Kernel = "Monolithic Kernel", OSFamily = "Android", ReleaseDate = new DateTime(2019, 9, 3), PackageManager = "Google Play Store", IsSupported = false };
            SmartPhoneOS harmony_os_2 = new SmartPhoneOS() { SmartPhoneOSID = 8, Name = "HarmonyOS 2", Kernel = "HarmonyOS microkernel", OSFamily = "HarmonyOS", ReleaseDate = new DateTime(2021, 5, 13), PackageManager = "Ark Compiler", IsSupported = true };
            SmartPhoneOS android_12 = new SmartPhoneOS() { SmartPhoneOSID = 9, Name = "Android 12", Kernel = "Monolithic Kernel", OSFamily = "Android", ReleaseDate = new DateTime(2021, 10, 13), PackageManager = "Google Play Store", IsSupported = true };

            mockSmartPhoneOSRepo.Setup(f => f.ReadAll()).Returns(new List<SmartPhoneOS>()
            {
                android_5,
                android_13,
                windows_phone_8,
                windows_phone_10,
                iOS_6,
                iOS_15,
                android_10,
                harmony_os_2,
                android_12,
            }.AsQueryable());

            smartPhoneOSLogic = new SmartPhoneOSLogic(mockSmartPhoneOSRepo.Object);
            #endregion

            #region Init Phone
            mocPhoneRepo = new Mock<IRepository<Phone>>();

            mocPhoneRepo.Setup(f => f.ReadAll()).Returns(new List<Phone>()
            {
                new Phone() {PhoneID = 1, Name = "Nexus 6", Series = "Nexus", ReleaseDate = new DateTime(2014, 11, 1), DataInput = "Micro USB", BatterySize = 3220, WirelessCharging = true, SmartPhoneOS = android_5, Company = google},
                new Phone() {PhoneID = 2, Name = "Pixel 7", Series = "Pixel", ReleaseDate = new DateTime(2022, 10, 13), DataInput = "USB-C", BatterySize = 4355, WirelessCharging = true, SmartPhoneOS= android_13, Company = google},
                new Phone() {PhoneID = 3, Name = "Microsoft Lumia 535", Series = "Lumia", ReleaseDate = new DateTime(2015, 5, 16), DataInput = "Micro USB 2.0", BatterySize = 1905, WirelessCharging = false, SmartPhoneOS = windows_phone_8, Company = microsoft},
                new Phone() {PhoneID = 4, Name = "Microsoft Lumia 650", Series = "Lumia", ReleaseDate = new DateTime(2016, 2, 15), DataInput = "Micro USB 2.0", BatterySize = 2000, WirelessCharging = false, SmartPhoneOS = windows_phone_10, Company = microsoft},
                new Phone() {PhoneID = 5, Name = "iPhone 5", Series = "iPhone", ReleaseDate = new DateTime(2012, 9, 21), DataInput = "Lightning", BatterySize = 1440, WirelessCharging = false, SmartPhoneOS = iOS_6, Company = apple},
                new Phone() {PhoneID = 6, Name = "iPhone 13", Series = "iPhone", ReleaseDate = new DateTime(2021, 9, 24), DataInput = "Lightning", BatterySize = 3227, WirelessCharging = true, SmartPhoneOS = iOS_15, Company = apple},
                new Phone() {PhoneID = 7, Name = "Huawei Mate 10", Series = "Mate ", ReleaseDate = new DateTime(2017, 10, 16), DataInput = "USB-C", BatterySize = 4000, WirelessCharging = true, SmartPhoneOS = android_10, Company = huawei},
                new Phone() {PhoneID = 8, Name = "Huawei P50", Series = "Huawei P Series", ReleaseDate = new DateTime(2021, 12, 4), DataInput = "USB-C", BatterySize = 4100, WirelessCharging = true, SmartPhoneOS = harmony_os_2, Company = huawei},
                new Phone() {PhoneID = 9, Name = "Samsung Galaxy S20", Series = "Samsung S series", ReleaseDate = new DateTime(2020, 3, 6), DataInput = "USB-C", BatterySize = 4000, WirelessCharging = true, SmartPhoneOS = android_10, Company = samsung},
                new Phone() {PhoneID = 10, Name = "Samsung Galaxy A04", Series = "Samsung A series", ReleaseDate = new DateTime(2022, 8, 24), DataInput = "USB-C", BatterySize = 5000, WirelessCharging = false, SmartPhoneOS = android_12, Company = samsung},
                new Phone() {PhoneID = 11, Name = "Xiaomi Mi 9 Pro", Series = "Xiaomi Mi series", ReleaseDate = new DateTime(2019, 9, 10), DataInput = "USB-C", BatterySize = 4000, WirelessCharging = true, SmartPhoneOS = android_10, Company = xiaomi},
                new Phone() {PhoneID = 12, Name = "POCO F3", Series = "Xiaomi POCO series", ReleaseDate = new DateTime(2021, 7, 12), DataInput = "USB-C", BatterySize = 4520, WirelessCharging = true, SmartPhoneOS = android_12, Company = xiaomi},
            }.AsQueryable());

            phoneLogic = new PhoneLogic(mocPhoneRepo.Object);
            #endregion
        }

        #region CRUD Method Tests
        //1
        [Test]
        public void CorrectCreateCompanyTest()
        {
            Company new_company = new Company() { CompanyID = 7, Name = "Nokia Corporation", CEO = "Pekka Lundmark", NetWorth = 27, Headquarters = "Espoo, Finland", NumberOfEmployees = 87930, Founded = new DateTime(1865, 5, 12) };

            companyLogic.Create(new_company);
            mockCompanyRepo.Verify(t => t.Create(new_company), Times.Once());
        }

        //2
        [Test]
        public void ShortNameCreateCompanyTest()
        {
            Company new_company = new Company() { CompanyID = 7, Name = "a", CEO = "Pekka Lundmark", NetWorth = 27, Headquarters = "Espoo, Finland", NumberOfEmployees = 87930, Founded = new DateTime(1865, 5, 12) };

            Assert.Throws<ArgumentException>(() => companyLogic.Create(new_company));
        }

        //3
        [Test]
        public void CorrectCreatePhoneTest()
        {
            Phone new_phone = new Phone() {PhoneID = 13, Name = "Nothoing Phone(1)", Series = "Nothing", ReleaseDate = new DateTime(2022, 7, 12), DataInput = "USB-C", BatterySize = 4500, WirelessCharging = true};

            phoneLogic.Create(new_phone);
            mocPhoneRepo.Verify(t => t.Create(new_phone), Times.Once());
        }

        //4
        [Test]
        public void SmallBatteryCreatePhoneTest()
        {
            Phone new_phone = new Phone() { PhoneID = 13, Name = "Nothoing Phone(1)", Series = "Nothing", ReleaseDate = new DateTime(2022, 7, 12), DataInput = "USB-C", BatterySize = 10, WirelessCharging = true };

            Assert.Throws<ArgumentException>(() => phoneLogic.Create(new_phone));
        }

        //5
        [Test]
        public void CorrectCreateSmartPhoneOSTest()
        {
            SmartPhoneOS new_os = new SmartPhoneOS() {SmartPhoneOSID = 10, Name = "iOS 13", Kernel = "Hybrid XNU", OSFamily = "iOS", ReleaseDate = new DateTime(2019, 9, 3), PackageManager = "App Store", IsSupported = true };

            smartPhoneOSLogic.Create(new_os);
            mockSmartPhoneOSRepo.Verify(t => t.Create(new_os), Times.Once());
        }

        //6
        [Test]
        public void TooOldCreateSmartPhoneOSTest()
        {
            SmartPhoneOS new_os = new SmartPhoneOS() { SmartPhoneOSID = 10, Name = "iOS 13", Kernel = "Hybrid XNU", OSFamily = "iOS", ReleaseDate = new DateTime(1919, 9, 3), PackageManager = "App Store", IsSupported = true };

            Assert.Throws<ArgumentException>(() => smartPhoneOSLogic.Create(new_os));
        }
        #endregion

        #region non-CRUD Method Tests
        //7
        [Test]
        public void MostpplAndroidMakerTest()
        {
            Company expected = new Company() { CompanyID = 5, Name = "Samsung Eelectronics Co., Ltd", CEO = "Lee Jae-yong", NetWorth = 500, Headquarters = "Seocho District, Seoul, South Korea", NumberOfEmployees = 266673, Founded = new DateTime(1938, 3, 1) };
            Company actual = phoneLogic.MostpplAndroidMaker();

            Assert.AreEqual(expected, actual);
        }
        //8
        [Test]
        public void SupportedAppleTest()
        {
            List<Phone> expected = new List<Phone>() { new Phone() { PhoneID = 6, Name = "iPhone 13", Series = "iPhone", ReleaseDate = new DateTime(2021, 9, 24), DataInput = "Lightning", BatterySize = 3227, WirelessCharging = true, SmartPhoneOSID = 6, CompanyID = 3 } };

            List<Phone> actual = phoneLogic.SupportedApple().ToList();
            Assert.AreEqual(expected, actual);
        }

        //9
        [Test]
        public void OldestWirelessSamsungTest()
        {
            Phone expected = new Phone() { PhoneID = 9, Name = "Samsung Galaxy S20", Series = "Samsung S series", ReleaseDate = new DateTime(2020, 3, 6), DataInput = "USB-C", BatterySize = 4000, WirelessCharging = true, SmartPhoneOSID = 7, CompanyID = 5 };
            Phone actual = phoneLogic.OldestWirelessSamsung();

            Assert.AreEqual(expected, actual);
        }

        //10
        [Test]
        public void LargeBatteryOSTest()
        {
            List<SmartPhoneOS> expected = new List<SmartPhoneOS>()
            {
                new SmartPhoneOS() {SmartPhoneOSID = 2, Name ="Android 13", Kernel="Monolithic Kernel", OSFamily="Android", ReleaseDate = new DateTime(2022, 8, 15), PackageManager="Google Play Store", IsSupported=true},
                new SmartPhoneOS() {SmartPhoneOSID = 7, Name ="Android 10", Kernel="Monolithic Kernel", OSFamily="Android", ReleaseDate = new DateTime(2019, 9, 3), PackageManager="Google Play Store", IsSupported=false},
                new SmartPhoneOS() {SmartPhoneOSID = 8, Name ="HarmonyOS 2", Kernel="HarmonyOS microkernel", OSFamily="HarmonyOS", ReleaseDate = new DateTime(2021, 5, 13), PackageManager="Ark Compiler", IsSupported=true},
                new SmartPhoneOS() {SmartPhoneOSID = 9, Name ="Android 12", Kernel="Monolithic Kernel", OSFamily="Android", ReleaseDate = new DateTime(2021, 10, 13), PackageManager="Google Play Store", IsSupported=true}
            };

            List<SmartPhoneOS> actual = phoneLogic.LargeBatteryOS().ToList();
            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}
