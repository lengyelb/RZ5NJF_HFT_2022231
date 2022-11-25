using ConsoleTools;
using System;

namespace RZ5NJF_HFT_2022231.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            #region CRUD Methods
            static void Create(string record_to_create)
            {
                Console.WriteLine(record_to_create);
            }

            static void Read(string record_to_read)
            {
                Console.WriteLine(record_to_read);
            }

            static void Update(string record_to_update)
            {
                Console.WriteLine(record_to_update);
            }

            static void Delete(string record_to_delete)
            {
                Console.WriteLine(record_to_delete);
            }
            #endregion

            var phones_menu = new ConsoleMenu(args, level: 1)
                .Add("List", () => Read("phone"))
                .Add("Create", () => Create("phone"))
                .Add("Delete", () => Delete("phone"))
                .Add("Update\n", () => Update("phone"))
                .Add("Exit", ConsoleMenu.Close);

            var companys_menu = new ConsoleMenu(args, level: 1)
                .Add("List", () => Read("company"))
                .Add("Create", () => Create("company"))
                .Add("Delete", () => Delete("company"))
                .Add("Update\n", () => Update("company"))
                .Add("Exit", ConsoleMenu.Close);

            var os_menu = new ConsoleMenu(args, level: 1)
                .Add("List", () => Read("os"))
                .Add("Create", () => Create("os"))
                .Add("Delete", () => Delete("os"))
                .Add("Update", () => Update("os"))
                .Add("Exit", ConsoleMenu.Close);


            var main_menu = new ConsoleMenu(args, level: 0)
               .Add("Phones", () => phones_menu.Show())
               .Add("Companys", () => companys_menu.Show())
               .Add("Operating ystems", () => os_menu.Show())
               .Add("Exit", ConsoleMenu.Close);

            main_menu.Show();
        }
    }
}
