using Castle.DynamicProxy.Generators;
using ConsoleTools;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using RZ5NJF_HFT_2022231.Repository;
using System;
using System.Linq;

namespace RZ5NJF_HFT_2022231.Client
{
    class Program
    {
        #region Helper methods
        enum data_types_enum { Company, Phone, SmartPhoneOS};

        static bool IsNumber(string str)
        {
            if (str != null)
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
            if (str != null)
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
            if (str != null)
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

        static string AskForData(string message, string error_message, Predicate<string> check_correction)
        {
            Console.WriteLine(message);
            string answer = Console.ReadLine();
            while (!check_correction(answer))
            {
                Console.WriteLine(error_message);
                answer = Console.ReadLine();
            }
            return answer;
        }
        #endregion

        #region CRUD methods
        static void Create(data_types_enum record_to_create)
        {
            switch (record_to_create)
            {
                case data_types_enum.Company:
                    string new_company_name = AskForData("Please enter the name of the company:", "You need to enter something, please try again:", (t => t != ""));
                    string new_company_CEO = AskForData("Please enter the name of the CEO:", "You need to enter something, please try again:", (t => t != ""));
                    int new_company_net_worth = int.Parse(AskForData("Please enter the net worth of the company (in whole billion dollars):", "You need to enter a number, please try again:", (t => IsNumber(t))));
                    string new_company_hq = AskForData("Please enter the headquarter location of the company:", "You need to enter something, please try again:", (t => t != ""));
                    int new_company_employye_num = int.Parse(AskForData("Please enter the number of employyes working at the company:", "You need to enter a number, please try again:", (t => IsNumber(t))));
                    DateTime new_compamy_founded = DateTime.Parse(AskForData("Please enter the founding date of the company (yyyy, mm, dd):", "You need to enter a correct date format, please try again:", (t => IsDate(t))));
                    try
                    {
                        Console.WriteLine("Company sucesfully created!");
                        Console.ReadLine();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("And error occured, company could not be created :/");
                        Console.ReadLine();
                    }
                    break;
                case data_types_enum.Phone:
                    string new_phone_name = AskForData("Please enter the name of the phone:", "You need to enter something, please try again:", (t => t != ""));
                    string new_phone_series = AskForData("Please enter the series of the phone:", "You need to enter something, please try again:", (t => t != ""));
                    DateTime new_phone_release_date = DateTime.Parse(AskForData("Please enter the release date of the phone (yyyy, mm, dd):", "You need to enter a correct date format, please try again:", (t => IsDate(t))));
                    string new_phone_data_input = AskForData("Please enter the connection method used for data (i.e. UDB-C):", "You need to enter something, please try again:", (t => t != ""));
                    int new_phone_battery_size = int.Parse(AskForData("Please enter the battery size of the phone (in mAh):", "You need to enter a number, please try again:", (t => IsNumber(t))));
                    bool new_phone_wireless_charging = bool.Parse(AskForData("Please enter if the phone has wireless charging (0=no, 1=yes):", "You need to enter the correct format, please try again:", (t => IsBool(t))));
                    try
                    {
                        Console.WriteLine("Phone sucesfully created!");
                        Console.ReadLine();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("And error occured, company could not be created :/");
                        Console.ReadLine();
                    }
                    break;
                case data_types_enum.SmartPhoneOS:
                    string new_os_name = AskForData("Please enter the name of the os:", "You need to enter something, please try again:", (t => t != ""));
                    string new_os_kernel = AskForData("Please enter the kernel of the os:", "You need to enter something, please try again:", (t => t != ""));
                    string new_os_family = AskForData("Please enter the family of the os:", "You need to enter something, please try again:", (t => t != ""));
                    DateTime new_os_release_date = DateTime.Parse(AskForData("Please enter the release date of the os (yyyy, mm, dd):", "You need to enter a correct date format, please try again:", (t => IsDate(t))));
                    string new_os_package_manager = AskForData("Please enter the package manager of the os (i.e. Google Play Store):", "You need to enter something, please try again:", (t => t != ""));
                    bool new_os_is_supported = bool.Parse(AskForData("Please enter if the os is still supported (0=no, 1=yes):", "You need to enter the correct format, please try again:", (t => IsBool(t))));
                    try
                    {
                        Console.WriteLine("Operating system sucesfully created!");
                        Console.ReadLine();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("And error occured, company could not be created :/");
                        Console.ReadLine();
                    }
                    break;
                default:
                    Console.WriteLine("If you can see this message something went horribly wrong!");
                    break;
            }
        }

        static void Read(data_types_enum record_to_read)
        {
            Console.WriteLine(record_to_read);
        }

        static void Update(data_types_enum record_to_update)
        {
            Console.WriteLine(record_to_update);
        }

        static void Delete(data_types_enum record_to_delete)
        {
            Console.WriteLine(record_to_delete);
        }
        #endregion

        static void Main(string[] args)
        {
            //http://localhost:22184/

            #region Menu
            var phones_menu = new ConsoleMenu(args, level: 1)
                .Add("Create", () => Create(data_types_enum.Phone))
                .Add("List", () => Read(data_types_enum.Phone))
                .Add("Delete", () => Delete(data_types_enum.Phone))
                .Add("Update\n", () => Update(data_types_enum.Phone))
                .Add("Exit", ConsoleMenu.Close);

            var companys_menu = new ConsoleMenu(args, level: 1)
                .Add("Create", () => Create(data_types_enum.Company))
                .Add("List", () => Read(data_types_enum.Company))
                .Add("Delete", () => Delete(data_types_enum.Company))
                .Add("Update\n", () => Update(data_types_enum.Company))
                .Add("Exit", ConsoleMenu.Close);

            var os_menu = new ConsoleMenu(args, level: 1)
                .Add("Create", () => Create(data_types_enum.SmartPhoneOS))
                .Add("List", () => Read(data_types_enum.SmartPhoneOS))
                .Add("Delete", () => Delete(data_types_enum.SmartPhoneOS))
                .Add("Update", () => Update(data_types_enum.SmartPhoneOS))
                .Add("Exit", ConsoleMenu.Close);


            var main_menu = new ConsoleMenu(args, level: 0)
               .Add("Phones", () => phones_menu.Show())
               .Add("Companys", () => companys_menu.Show())
               .Add("Operating systems", () => os_menu.Show())
               .Add("Exit", ConsoleMenu.Close);

            main_menu.Show();
            #endregion
        }
    }
}
