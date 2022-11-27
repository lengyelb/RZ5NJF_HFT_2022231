using Castle.DynamicProxy.Generators;
using ConsoleTools;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using RZ5NJF_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;

namespace RZ5NJF_HFT_2022231.Client
{
    class Program
    {
        static RestService rest;

        #region Helper methods
        enum data_types_enum { Company, Phone, SmartPhoneOS};

        static bool IsNumber(string str)
        {
            if (str != "")
            {
                try
                {
                    int.Parse(str);
                    return true;
                }
                catch (FormatException)
                {
                    return false;
                }

            }
            return false;
        }

        static bool IsBool(string str)
        {
            if (str != "")
            {
                try
                {
                    bool.Parse(str);
                    return true;
                }
                catch (FormatException)
                {
                    return false;
                }

            }
            return false;
        }

        static bool IsDate(string str)
        {
            if (str != "")
            {
                try
                {
                    DateTime.Parse(str);
                    return true;
                }
                catch (FormatException)
                {
                    return false;
                }

            }
            return false;
        }

        static string AskForData(string message, string error_message, Predicate<string> check_correction, bool allow_empty = false)
        {
            Console.WriteLine(message);
            string answer = Console.ReadLine();
            if (!(answer == "" && allow_empty))
            {
                while (!check_correction(answer))
                {
                    Console.WriteLine(error_message);
                    answer = Console.ReadLine();
                }
                return answer;
            }
            return null;
        }
        #endregion

        #region CRUD methods
        static void Create(data_types_enum record_to_create)
        {
            switch (record_to_create)
            {
                case data_types_enum.Company:
                    #region Case Company
                    try
                    {
                        rest.Post(new Company() 
                        {
                            Name = AskForData("Please enter the name of the company:", "You need to enter something, please try again:", (t => t != "")),
                            CEO = AskForData("Please enter the name of the CEO:", "You need to enter something, please try again:", (t => t != "")),
                            NetWorth = int.Parse(AskForData("Please enter the net worth of the company (in whole billion dollars):", "You need to enter a number, please try again:", (t => IsNumber(t)))),
                            Headquarters = AskForData("Please enter the headquarter location of the company:", "You need to enter something, please try again:", (t => t != "")),
                            NumberOfEmployees = int.Parse(AskForData("Please enter the number of employyes working at the company:", "You need to enter a number, please try again:", (t => IsNumber(t)))),
                            Founded = DateTime.Parse(AskForData("Please enter the founding date of the company (yyyy, mm, dd):", "You need to enter a correct date format, please try again:", (t => IsDate(t))))
                        }, "Company");
                        Console.WriteLine("Company sucesfully created!");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("And error occured, company could not be created :/");
                    }
                    break;
                    #endregion
                case data_types_enum.Phone:
                    #region Case Phone
                    try
                    {
                        rest.Post(new Phone()
                        {
                            Name = AskForData("Please enter the name of the phone:", "You need to enter something, please try again:", (t => t != "")),
                            Series = AskForData("Please enter the series of the phone:", "You need to enter something, please try again:", (t => t != "")),
                            ReleaseDate = DateTime.Parse(AskForData("Please enter the release date of the phone (yyyy, mm, dd):", "You need to enter a correct date format, please try again:", (t => IsDate(t)))),
                            DataInput = AskForData("Please enter the connection method used for data (i.e. UDB-C):", "You need to enter something, please try again:", (t => t != "")),
                            BatterySize = int.Parse(AskForData("Please enter the battery size of the phone (in mAh):", "You need to enter a number, please try again:", (t => IsNumber(t)))),
                            WirelessCharging = bool.Parse(AskForData("Please enter if the phone has wireless charging (0=no, 1=yes):", "You need to enter the correct format, please try again:", (t => IsBool(t)))),
                        }, "Phone");
                        Console.WriteLine("Phone sucesfully created!");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("And error occured, phone could not be created :/");
                    }
                    break;
                    #endregion
                case data_types_enum.SmartPhoneOS:
                    #region Case SmartPhoneOS
                    try
                    {
                        rest.Post(new SmartPhoneOS()
                        {
                            Name = AskForData("Please enter the name of the os:", "You need to enter something, please try again:", (t => t != "")),
                            Kernel = AskForData("Please enter the kernel of the os:", "You need to enter something, please try again:", (t => t != "")),
                            OSFamily = AskForData("Please enter the family of the os:", "You need to enter something, please try again:", (t => t != "")),
                            ReleaseDate = DateTime.Parse(AskForData("Please enter the release date of the os (yyyy, mm, dd):", "You need to enter a correct date format, please try again:", (t => IsDate(t)))),
                            PackageManager = AskForData("Please enter the package manager of the os (i.e. Google Play Store):", "You need to enter something, please try again:", (t => t != "")),
                            IsSupported = bool.Parse(AskForData("Please enter if the os is still supported (0=no, 1=yes):", "You need to enter the correct format, please try again:", (t => IsBool(t))))
                        }, "SmartPhoneO");
                        Console.WriteLine("Operating system sucesfully created!");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("And error occured, operating system could not be created :/");
                    }
                    break;
                    #endregion
                default:
                    #region Case Default
                    Console.WriteLine("If you can see this message something went horribly wrong!");
                    Console.ReadLine();
                    break;
                    #endregion
            }
            Console.ReadLine();
        }

        static void Read(data_types_enum record_to_read)
        {
            switch (record_to_read)
            {
                case data_types_enum.Company:
                    #region Case Company
                    List<Company> company_list = rest.Get<Company>("Company");
                    foreach (Company company in company_list)
                    {
                        Console.WriteLine(company);
                    }
                    break;
                    #endregion
                case data_types_enum.Phone:
                    #region Case Phone
                    List<Phone> phone_list = rest.Get<Phone>("Phone");
                    foreach (Phone phone in phone_list)
                    {
                        Console.WriteLine(phone);
                    }
                    break;
                    #endregion
                case data_types_enum.SmartPhoneOS:
                    #region Case SmartPhoneOS
                    List<SmartPhoneOS> os_list = rest.Get<SmartPhoneOS>("SmartPhoneOS");
                    foreach (SmartPhoneOS os in os_list)
                    {
                        Console.WriteLine(os);
                    }
                    break;
                    #endregion
                default:
                    #region Case Default
                    Console.WriteLine("If you can see this message something went horribly wrong!");
                    break;
                    #endregion
            }
            Console.ReadLine();
        }

        static void Update(data_types_enum record_to_update)
        {
            switch (record_to_update)
            {
                case data_types_enum.Company:
                    #region Case Company
                    int company_to_update_id = int.Parse(AskForData("Please enter the id of the company you wish to update:", "You need to enter a number, please try again:", (t => IsNumber(t))));

                    Company company_to_update = rest.Get<Company>(company_to_update_id, "Company");

                    Console.WriteLine();
                    Console.WriteLine("Enter no data if you want to keep the current one");
                    company_to_update.Name = AskForData(
                        $"Please enter the new name of the company [current: {company_to_update.Name}]:",
                        "You need to enter something, please try again:",
                        (t => t != ""), true) ?? company_to_update.Name;
                    company_to_update.CEO = AskForData(
                        $"Please enter the new name of the CEO [current: {company_to_update.CEO}]:",
                        "You need to enter something, please try again:",
                        (t => t != ""), true) ?? company_to_update.CEO;
                    company_to_update.NetWorth = int.Parse(AskForData(
                        $"Please enter the updated net worth of the company (in whole billion dollars) [current: {company_to_update.NetWorth}]:",
                        "You need to enter a number, please try again:",
                        (t => IsNumber(t)), true) ?? company_to_update.NetWorth.ToString());
                    company_to_update.Headquarters = AskForData(
                        $"Please enter the new location of the company [current: {company_to_update.Headquarters}]:",
                        "You need to enter something, please try again:",
                        (t => t != ""), true) ?? company_to_update.Headquarters;
                    company_to_update.NumberOfEmployees = int.Parse(AskForData(
                        $"Please enter the updated number of employyes working at the company [current: {company_to_update.NumberOfEmployees}]:",
                        "You need to enter a number, please try again:",
                        (t => IsNumber(t)), true) ?? company_to_update.NumberOfEmployees.ToString());
                    company_to_update.Founded = DateTime.Parse(AskForData(
                        $"Please enter the updated founding date of the company (yyyy, mm, dd) [current: {company_to_update.Founded}]:",
                        "You need to enter a correct date format, please try again:",
                        (t => IsDate(t)), true) ?? company_to_update.Founded.ToString());

                    try
                    {
                        rest.Put(company_to_update, "Company");
                        Console.WriteLine("Company sucesfully updated!");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("And error occured, company could not be updated :/");
                    }
                    break;
                #endregion
                case data_types_enum.Phone:
                    #region Case Phone
                    int phone_to_update_id = int.Parse(AskForData("Please enter the id of the phone you wish to update:", "You need to enter a number, please try again:", (t => IsNumber(t))));

                    Phone phone_to_update = rest.Get<Phone>(phone_to_update_id, "Phone");

                    Console.WriteLine();
                    Console.WriteLine("Enter no data if you want to keep the current one");
                    phone_to_update.Name = AskForData(
                        $"Please enter the new name of the phone [current: {phone_to_update.Name}]:",
                        "You need to enter something, please try again:",
                        (t => t != ""), true) ?? phone_to_update.Name;
                    phone_to_update.Series = AskForData(
                        $"Please enter the new series of the phone [current: {phone_to_update.Series}]:",
                        "You need to enter something, please try again:",
                        (t => t != ""), true) ?? phone_to_update.Series;
                    phone_to_update.ReleaseDate = DateTime.Parse(AskForData(
                        $"Please enter the updated release date of the phone (yyyy, mm, dd) [current: {phone_to_update.ReleaseDate}]:",
                        "You need to enter a correct date format, please try again:",
                        (t => IsDate(t)), true) ?? phone_to_update.ReleaseDate.ToString());
                    phone_to_update.DataInput = AskForData(
                        $"Please enter the new data input type of the phone [current: {phone_to_update.DataInput}]:",
                        "You need to enter something, please try again:",
                        (t => t != ""), true) ?? phone_to_update.DataInput;
                    phone_to_update.BatterySize = int.Parse(AskForData(
                        $"Please enter the updated size of the battery [current: {phone_to_update.BatterySize}]:",
                        "You need to enter a number, please try again:",
                        (t => IsNumber(t)), true) ?? phone_to_update.BatterySize.ToString());
                    phone_to_update.WirelessCharging = bool.Parse(AskForData(
                        $"Please enter if the phone has wireless charging (0=no, 1=yes) [current: {phone_to_update.WirelessCharging}]:",
                        "You need to enter a number, please try again:",
                        (t => IsNumber(t)), true) ?? phone_to_update.WirelessCharging.ToString());

                    try
                    {
                        rest.Put(phone_to_update, "Company");
                        Console.WriteLine("Phone sucesfully updated!");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("And error occured, phone could not be updated :/");
                    }
                    break;
                #endregion
                case data_types_enum.SmartPhoneOS:
                    #region Case SmartPhoneOS
                    int os_to_update_id = int.Parse(AskForData("Please enter the id of the operating system you wish to update:", "You need to enter a number, please try again:", (t => IsNumber(t))));

                    SmartPhoneOS os_to_update = rest.Get<SmartPhoneOS>(os_to_update_id, "SmartPhoneOS");

                    Console.WriteLine();
                    Console.WriteLine("Enter no data if you want to keep the current one");
                    os_to_update.Name = AskForData(
                        $"Please enter the new name of the operating system [current: {os_to_update.Name}]:",
                        "You need to enter something, please try again:",
                        (t => t != ""), true) ?? os_to_update.Name;
                    os_to_update.Kernel = AskForData(
                        $"Please enter the new kernel of the operating system [current: {os_to_update.Kernel}]:",
                        "You need to enter something, please try again:",
                        (t => t != ""), true) ?? os_to_update.Kernel;
                    os_to_update.OSFamily = AskForData(
                        $"Please enter the new family of the oparting system [current: {os_to_update.OSFamily}]:",
                        "You need to enter something, please try again:",
                        (t => t != ""), true) ?? os_to_update.OSFamily;
                    os_to_update.ReleaseDate = DateTime.Parse(AskForData(
                        $"Please enter the updated release date of the operating system (yyyy, mm, dd) [current: {os_to_update.ReleaseDate}]:",
                        "You need to enter a correct date format, please try again:",
                        (t => IsDate(t)), true) ?? os_to_update.ReleaseDate.ToString());
                    os_to_update.PackageManager = AskForData(
                        $"Please enter the new package manager of the oparting system [current: {os_to_update.PackageManager}]:",
                        "You need to enter something, please try again:",
                        (t => t != ""), true) ?? os_to_update.PackageManager;
                    os_to_update.IsSupported = bool.Parse(AskForData(
                        $"Please enter if the os is still supported (0=no, 1=yes) [current: {os_to_update.IsSupported}]:",
                        "You need to enter a number, please try again:",
                        (t => IsNumber(t)), true) ?? os_to_update.IsSupported.ToString());

                    try
                    {
                        rest.Put(os_to_update, "SmartPhoneOS");
                        Console.WriteLine("Operating system sucesfully updated!");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("And error occured, operating system could not be updated :/");
                    }
                    break;
                    #endregion
                default:
                    #region Case Default
                    Console.WriteLine("If you can see this message something went horribly wrong!");
                    break;
                    #endregion
            }
            Console.ReadLine();
        }

        static void Delete(data_types_enum record_to_delete)
        {
            switch (record_to_delete)
            {
                case data_types_enum.Company:
                    #region
                    int company_to_delete_id = int.Parse(AskForData("Plase give the id of the company you wish to delete:",
                        "You need to enter a number, please try again:", (t => IsNumber(t))));

                    rest.Delete(company_to_delete_id, "Company");
                    Console.WriteLine("Company was succesfully delteted!");
                    break;
                    #endregion
                case data_types_enum.Phone:
                    #region
                    int phone_to_delete_id = int.Parse(AskForData("Plase give the id of the phone you wish to delete:",
                        "You need to enter a number, please try again:", (t => IsNumber(t))));

                    rest.Delete(phone_to_delete_id, "Phone");
                    Console.WriteLine("Phone was succesfully delteted!");
                    break;
                    #endregion
                case data_types_enum.SmartPhoneOS:
                    #region
                    int os_to_delete_id = int.Parse(AskForData("Plase give the id of the oerating system you wish to delete:",
                        "You need to enter a number, please try again:", (t => IsNumber(t))));

                    rest.Delete(os_to_delete_id, "SmartPhoneOS");
                    Console.WriteLine("Operating system was succesfully delteted!");
                    break;
                    #endregion
                default:
                    #region Case Default
                    Console.WriteLine("If you can see this message something went horribly wrong!");
                    Console.ReadLine();
                    break;
                    #endregion
            }
            Console.ReadLine();
        }
        #endregion

        #region non-CRUD Methods
        static void SupportedApple()
        {
            List<Phone> supported_apple_phones = rest.Get<Phone>("NonCrud/SupportedApple");

            foreach (Phone phone in supported_apple_phones)
            {
                Console.WriteLine(phone);
            }
            Console.ReadLine();
        }

        static void OldestWirelessSamsung()
        {
            Phone oldest_wireless_samsung = rest.GetSingle<Phone>("NonCrud/OldestWirelessSamsung");

            Console.WriteLine(oldest_wireless_samsung);
            Console.ReadLine();
        }

        static void MostpplAndroidMaker()
        {
            Company most_ppl_android_maker = rest.GetSingle<Company>("NonCrud/MostpplAndroidMaker");

            Console.WriteLine(most_ppl_android_maker);
            Console.ReadLine();
        }

        static void LatestOsCompany()
        {
            List<Company> latest_os_company = rest.Get<Company>("NonCrud/LatestOsCompany");

            foreach (Company company in latest_os_company)
            {
                Console.WriteLine(company);
            }

            Console.ReadLine();
        }

        static void SmallestCompanyLatestOS()
        {
            SmartPhoneOS smallest_company_latest_os = rest.GetSingle<SmartPhoneOS>("NonCrud/SmallestCompanyLatestOS");

            Console.WriteLine(smallest_company_latest_os);
            Console.ReadLine();
        }

        static void LargeBatteryOS()
        {
            List<SmartPhoneOS> large_battery_oses = rest.Get<SmartPhoneOS>("NonCrud/LargeBatteryOS");

            foreach (SmartPhoneOS large_battery_os in large_battery_oses)
            {
                Console.WriteLine(large_battery_os);
            }
            Console.ReadLine();
        }
        #endregion

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:22184/", "Company");

            #region Menu
            var phones_menu = new ConsoleMenu(args, level: 1)
                .Add("Create", () => Create(data_types_enum.Phone))
                .Add("List", () => Read(data_types_enum.Phone))
                .Add("Delete", () => Delete(data_types_enum.Phone))
                .Add("Update\n", () => Update(data_types_enum.Phone))

                .Add("Still supported phones made by apple", () => SupportedApple())
                .Add("Oldest samsung phone that supports wirelesss charging\n", () => OldestWirelessSamsung())

                .Add("Exit", ConsoleMenu.Close);

            var companys_menu = new ConsoleMenu(args, level: 1)
                .Add("Create", () => Create(data_types_enum.Company))
                .Add("List", () => Read(data_types_enum.Company))
                .Add("Delete", () => Delete(data_types_enum.Company))
                .Add("Update\n", () => Update(data_types_enum.Company))

                .Add("The largest head count company that made at least one android phone", () => MostpplAndroidMaker())
                .Add("Companys that made a phone with the latest operating system\n", () => LatestOsCompany())

                .Add("Exit", ConsoleMenu.Close);

            var os_menu = new ConsoleMenu(args, level: 1)
                .Add("Create", () => Create(data_types_enum.SmartPhoneOS))
                .Add("List", () => Read(data_types_enum.SmartPhoneOS))
                .Add("Delete", () => Delete(data_types_enum.SmartPhoneOS))
                .Add("Update\n", () => Update(data_types_enum.SmartPhoneOS))

                .Add("The latest operating system of the lowest net worth company", () => SmallestCompanyLatestOS())
                .Add("Every operating system that was released on phones with a battery at least 4000 mAh\n", () => LargeBatteryOS())

                .Add("Exit", ConsoleMenu.Close);


            var main_menu = new ConsoleMenu(args, level: 0)
               .Add("Phones", () => phones_menu.Show())
               .Add("Companys", () => companys_menu.Show())
               .Add("Operating systems\n", () => os_menu.Show())
               .Add("Exit", ConsoleMenu.Close);

            main_menu.Show();
            #endregion
        }
    }
}
