using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using RZ5NJF_HFT_2022231.Models;
using RZ5NJF_HFT_2022231.WpfClient.Services;
using RZ5NJF_HFT_2022231.WpfClient.Windows;
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
         public ICommand OpenCompanyEditorWindowCommand { get; set; }
        public ICommand OpenPhoneEditorWindowCommand { get; set; }
        public ICommand OpenOSEditorWindowCommand { get; set; }

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
                OpenCompanyEditorWindowCommand = new RelayCommand(() =>
                {
                    CompanyEditorWindow companyEditorVindow = new CompanyEditorWindow();
                    companyEditorVindow.Show();
                });

                OpenPhoneEditorWindowCommand = new RelayCommand(() =>
                {
                    PhoneEditorWindow phoneEditorVindow = new PhoneEditorWindow();
                    phoneEditorVindow.Show();
                });

                OpenOSEditorWindowCommand = new RelayCommand(() =>
                {
                    OSEditorWindow osEditorVindow = new OSEditorWindow();
                    osEditorVindow.Show();
                });
            }
        }
    }
}
