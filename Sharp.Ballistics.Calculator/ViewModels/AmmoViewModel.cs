﻿using Caliburn.Micro;
using Sharp.Ballistics.Abstractions;
using Sharp.Ballistics.Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class AmmoViewModel : FunctionScreen
    {
        private readonly IEventAggregator eventAggregator;

        public override string IconFilename => "ammo.png";

        public override int Order => 4;

        private readonly AmmoModel ammoModel;
        private readonly IWindowManager windowManager;
        private readonly ConfigurationModel configurationModel;
        public AmmoViewModel(AmmoModel ammoModel, 
                             ConfigurationModel configurationModel, 
                             IWindowManager windowManager,
                             IEventAggregator eventAggregator)
            :base(eventAggregator)
        {            
#pragma warning disable CC0021 // Use nameof
            DisplayName = "Cartridges";
#pragma warning restore CC0021 // Use nameof
            this.ammoModel = ammoModel;
            this.configurationModel = configurationModel;
            this.windowManager = windowManager;

            configurationModel.Initialize();
            this.eventAggregator = eventAggregator;
        }

        public UnitsConfiguration Units => configurationModel.Units;

        public IEnumerable<Cartridge> Cartridges => ammoModel.All();

        public bool IsBusy { get; set; }

        public string BusyText { get; set; }      

        public void AddCartridge()
        {
            var newCartridgeViewModel = new EditCartridgeViewModel(configurationModel);
            if (windowManager.ShowDialog(newCartridgeViewModel) ?? false)
            {
                Task.Run(() =>
                {
                    IsBusy = true;
                    BusyText = "Saving New Cartridge...";
                    NotifyOfPropertyChange(() => IsBusy);
                    NotifyOfPropertyChange(() => BusyText);

                    ammoModel.InsertOrUpdate(newCartridgeViewModel.Cartridge);
                    NotifyOfPropertyChange(() => Cartridges);
                    Refresh();

                    IsBusy = false;
                    NotifyOfPropertyChange(() => IsBusy);
                });
            }
        }

        public void EditCartridge(Cartridge cartridge)
        {
            var editCartridgeViewModel = new EditCartridgeViewModel(configurationModel,cartridge);
            if (windowManager.ShowDialog(editCartridgeViewModel) ?? false)
            {
                Task.Run(() =>
                {
                    IsBusy = true;
                    BusyText = "Saving Cartridge Changes...";
                    NotifyOfPropertyChange(() => IsBusy);
                    NotifyOfPropertyChange(() => BusyText);

                    ammoModel.InsertOrUpdate(editCartridgeViewModel.Cartridge);
                    NotifyOfPropertyChange(() => Cartridges);
                    Refresh();

                    IsBusy = false;
                    NotifyOfPropertyChange(() => IsBusy);
                });
            }
        }

        public void RemoveCartridge(Cartridge cartridge)
        {
            ammoModel.Delete(cartridge);
            NotifyOfPropertyChange(() => Cartridges);
        }

       
    }
}
