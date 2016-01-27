﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ApplicationWPF.Frames;

namespace ApplicationWPF
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IFrameNavigator m_currentFrame;

        public IFrameNavigator CurrentFrame
        {
            get { return m_currentFrame; }
            set { m_currentFrame = value; }
        }

        public MainWindow()
        {
            InitializeComponent();

            this.MainFrame.NavigationService.Navigate(new System.Uri("Frames/MainMenu.xaml", UriKind.Relative));
            MainFrame.NavigationService.LoadCompleted += FrameLoadCompleted;
        }

        void ChangeFrame (object sender, FrameChangedEventArgs e)
        {
            // Unsubscribe to event handler
            CurrentFrame.OnFrameChanged -= ChangeFrame;

            // Update de frame
            this.MainFrame.NavigationService.Navigate(new System.Uri(e.nextFramePath, UriKind.Relative));
        }

        void FrameLoadCompleted (object sender, EventArgs e)
        {
            CurrentFrame = MainFrame.NavigationService.Content as IFrameNavigator;
            if (CurrentFrame != null)
            {
                CurrentFrame.OnFrameChanged += ChangeFrame;
            }
        }
    }
}
