﻿using Microsoft.Toolkit.Mvvm.Input;
using RZ5NJF_HFT_2022231.Models;
using RZ5NJF_HFT_2022231.WpfClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace RZ5NJF_HFT_2022231.WpfClient.ViewModels
{
    public class CompanyEditorViewModel: ObservableRecipient
    {
        private void CanExecuteChanged()
        {
            (DeleteCompanyCommand as RelayCommand).NotifyCanExecuteChanged();
            (UpdateCompanyCommand as RelayCommand).NotifyCanExecuteChanged();
            (CreateCompanyCommand as RelayCommand).NotifyCanExecuteChanged();
        }
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
                    CanExecuteChanged();
                }
                else
                {
                    selectedCompany = null;
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

        public CompanyEditorViewModel()
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
                    SelectedCompany = null;
                    CanExecuteChanged();
                },
                () =>
                {
                    return SelectedCompany != null;
                });
            }
        }
    }
}