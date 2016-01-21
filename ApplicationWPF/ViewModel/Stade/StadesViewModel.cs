﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioWPF.ViewModel;

namespace ApplicationWPF.ViewModel.Stade
{
    class StadesViewModel : ViewModelBase
    {
        private ObservableCollection<StadeViewModel> m_stades;
        private StadeViewModel m_selectedItem;

        private RelayCommand m_addCommand;
        private RelayCommand m_removeCommand;
        private RelayCommand m_closeCommand;

        public ObservableCollection<StadeViewModel> Stades
        {
            get { return m_stades; }
            private set
            {
                m_stades = value;
                OnPropertyChanged("Stades");
            }
        }


        public StadeViewModel SelectedItem
        {
            get { return m_selectedItem; }
            set
            {
                m_selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public StadesViewModel(IList<EntitiesLayer.Stade> stadeModel)
        {
            m_stades = new ObservableCollection<StadeViewModel>();
            foreach (EntitiesLayer.Stade s in stadeModel)
            {
                m_stades.Add(new StadeViewModel(s));
            }
        }



        public System.Windows.Input.ICommand AddCommand
        {
            get
            {
                if (m_addCommand == null)
                {
                    m_addCommand = new RelayCommand(
                        () => this.Add(),
                        () => this.CanAdd()
                        );
                }
                return m_addCommand;
            }
        }

        private bool CanAdd()
        {
            return true;
        }

        private void Add()
        {
            EntitiesLayer.Stade s = new EntitiesLayer.Stade(0, "", null);
            this.SelectedItem = new StadeViewModel(s);
            m_stades.Add(this.SelectedItem);
        }

        public System.Windows.Input.ICommand RemoveCommand
        {
            get
            {
                if (m_removeCommand == null)
                {
                    m_removeCommand = new RelayCommand(
                        () => this.Remove(),
                        () => this.CanRemove()
                        );
                }
                return m_removeCommand;
            }
        }

        private bool CanRemove()
        {
            return (this.SelectedItem != null);
        }

        private void Remove()
        {
            if (this.SelectedItem != null) m_stades.Remove(this.SelectedItem);
        }
    }
}
