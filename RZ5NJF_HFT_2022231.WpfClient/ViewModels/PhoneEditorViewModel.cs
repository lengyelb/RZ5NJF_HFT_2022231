using Microsoft.Toolkit.Mvvm.Input;
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
using Newtonsoft.Json.Linq;

namespace RZ5NJF_HFT_2022231.WpfClient.ViewModels
{
    public class PhoneEditorViewModel: ObservableRecipient
    {
        public RestCollection<Phone> Phones { get; set; }

        private Phone selectedPhone;

        public Phone SelectedPhone
        {
            get { return selectedPhone; }
            set
            {
                if (value != null)
                {
                    selectedPhone = new Phone()
                    { 
                        PhoneID = value.PhoneID,
                        Name = value.Name, 
                        Series = value.Series,
                        ReleaseDate = value.ReleaseDate,
                        DataInput = value.DataInput,
                        BatterySize = value.BatterySize, 
                        WirelessCharging = value.WirelessCharging, 
                        CompanyID = value.CompanyID,
                        Company = value.Company,
                        SmartPhoneOSID = value.SmartPhoneOSID,
                        SmartPhoneOS = value.SmartPhoneOS
                    };
                    OnPropertyChanged();
                    (DeletePhoneCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdatePhoneCommand as RelayCommand).NotifyCanExecuteChanged();
                    (CreatePhoneCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreatePhoneCommand { get; set; }
        public ICommand DeletePhoneCommand { get; set; }
        public ICommand UpdatePhoneCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public PhoneEditorViewModel()
        {
            if (!IsInDesignMode)
            {
                Phones = new RestCollection<Phone>("http://localhost:22184/", "phone", "hub");

                CreatePhoneCommand = new RelayCommand(() =>
                {
                    Phones.Add(new Phone()
                    {
                        Name = SelectedPhone.Name,
                        Series = SelectedPhone.Series,
                        ReleaseDate = SelectedPhone.ReleaseDate,
                        DataInput = SelectedPhone.DataInput,
                        BatterySize = SelectedPhone.BatterySize,
                        WirelessCharging = SelectedPhone.WirelessCharging,
                        CompanyID = SelectedPhone.CompanyID,
                        Company = SelectedPhone.Company,
                        SmartPhoneOSID = SelectedPhone.SmartPhoneOSID,
                        SmartPhoneOS = SelectedPhone.SmartPhoneOS
                    });
                },
                () =>
                {
                    return SelectedPhone != null;
                });

                UpdatePhoneCommand = new RelayCommand(() =>
                {
                    Phones.Update(SelectedPhone);
                },
                () =>
                {
                    return SelectedPhone != null;
                });

                DeletePhoneCommand = new RelayCommand(() =>
                {
                    Phones.Delete(SelectedPhone.PhoneID);
                },
                () =>
                {
                    return SelectedPhone != null;
                });
            }
        }
    }
}
