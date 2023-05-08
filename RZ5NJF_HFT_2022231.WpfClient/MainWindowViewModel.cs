using RZ5NJF_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZ5NJF_HFT_2022231.WpfClient
{
    public class MainWindowViewModel
    {
        public RestCollection<Company> Companies { get; set; }

        public MainWindowViewModel()
        {
            Companies = new RestCollection<Company>("http://localhost:22184/", "company");
        }
    }
}
