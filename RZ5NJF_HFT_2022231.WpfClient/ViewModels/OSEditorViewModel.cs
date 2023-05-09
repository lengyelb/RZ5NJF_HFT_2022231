using Microsoft.Toolkit.Mvvm.ComponentModel;
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
using Newtonsoft.Json.Linq;

namespace RZ5NJF_HFT_2022231.WpfClient.ViewModels
{
    public class OSEditorViewModel : ObservableRecipient
    {
        public RestCollection<SmartPhoneOS> SmartPhoneOSes { get; set; }

        private SmartPhoneOS selectedSmartPhoneOS;

        public SmartPhoneOS SelectedSmartPhoneOS
        {
            get { return selectedSmartPhoneOS; }
            set
            {
                if (value != null)
                {
                    selectedSmartPhoneOS = new SmartPhoneOS()
                    {
                        SmartPhoneOSID = value.SmartPhoneOSID,
                        Name = value.Name,
                        Kernel = value.Kernel,
                        OSFamily = value.OSFamily,
                        ReleaseDate = value.ReleaseDate,
                        PackageManager = value.PackageManager,
                        IsSupported= value.IsSupported
                    };
                    OnPropertyChanged();
                    (DeleteSmartPhoneOSCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateSmartPhoneOSCommand as RelayCommand).NotifyCanExecuteChanged();
                    (CreateSmartPhoneOSCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateSmartPhoneOSCommand { get; set; }
        public ICommand DeleteSmartPhoneOSCommand { get; set; }
        public ICommand UpdateSmartPhoneOSCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public OSEditorViewModel()
        {
            if (!IsInDesignMode)
            {
                SmartPhoneOSes = new RestCollection<SmartPhoneOS>("http://localhost:22184/", "SmartPhoneOS", "hub");

                CreateSmartPhoneOSCommand = new RelayCommand(() =>
                {
                    SmartPhoneOSes.Add(new SmartPhoneOS()
                    {
                        Name = SelectedSmartPhoneOS.Name,
                        Kernel = SelectedSmartPhoneOS.Kernel,
                        OSFamily = SelectedSmartPhoneOS.OSFamily,
                        ReleaseDate = SelectedSmartPhoneOS.ReleaseDate,
                        PackageManager = SelectedSmartPhoneOS.PackageManager,
                        IsSupported = SelectedSmartPhoneOS.IsSupported
                    });
                },
                () =>
                {
                    return SelectedSmartPhoneOS != null;
                });

                UpdateSmartPhoneOSCommand = new RelayCommand(() =>
                {
                    SmartPhoneOSes.Update(SelectedSmartPhoneOS);
                },
                () =>
                {
                    return SelectedSmartPhoneOS != null;
                });

                DeleteSmartPhoneOSCommand = new RelayCommand(() =>
                {
                    SmartPhoneOSes.Delete(SelectedSmartPhoneOS.SmartPhoneOSID);
                },
                () =>
                {
                    return SelectedSmartPhoneOS != null;
                });
            }
        }
    }
}
