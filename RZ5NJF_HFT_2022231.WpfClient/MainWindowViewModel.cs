using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using RZ5NJF_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RZ5NJF_HFT_2022231.WpfClient
{
    public class MainWindowViewModel: ObservableRecipient
    {
        public RestCollection<Company> Companies { get; set; }

        private Company selectedCompany;

        public Company SelectedCompany
        {
            get { return selectedCompany; }
            set 
            {
                if (value != null)
                {
                    selectedCompany = new Company()
                    {
                        CompanyID = value.CompanyID,
                        Name = value.Name,
                        CEO = value.CEO,
                        NetWorth = value.NetWorth,
                        Headquarters = value.Headquarters,
                        NumberOfEmployees = value.NumberOfEmployees,
                        Founded = value.Founded
                    };
                    OnPropertyChanged();
                    (DeleteCompanyCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateCompanyCommand as RelayCommand).NotifyCanExecuteChanged();
                    (CreateCompanyCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateCompanyCommand { get; set; }
        public ICommand DeleteCompanyCommand { get; set; }
        public ICommand UpdateCompanyCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Companies = new RestCollection<Company>("http://localhost:22184/", "company", "hub");

                CreateCompanyCommand = new RelayCommand(() =>
                {
                    Companies.Add(new Company()
                    {
                        Name = SelectedCompany.Name,
                        CEO = SelectedCompany.CEO,
                        NetWorth = SelectedCompany.NetWorth,
                        Headquarters = SelectedCompany.Headquarters,
                        NumberOfEmployees = SelectedCompany.NumberOfEmployees,
                        Founded = SelectedCompany.Founded
                    });
                },
                () =>
                {
                    return SelectedCompany != null;
                });

                UpdateCompanyCommand = new RelayCommand(() =>
                {
                    Companies.Update(SelectedCompany);
                },
                () =>
                {
                    return SelectedCompany != null;
                });

                DeleteCompanyCommand = new RelayCommand(() =>
                {
                    Companies.Delete(SelectedCompany.CompanyID);
                },
                () =>
                {
                    return SelectedCompany != null;
                });
            }
        }
    }
}
