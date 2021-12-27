using KWDMAktywnosc.Core.Models;
using KWDMAktywnosc.Core.ViewModels;
using Microsoft.Win32;
using MvvmCross.Platforms.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KWDMAktywnosc.Wpf.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : MvxWpfView
    {
        public MainView()
        {
            InitializeComponent();
        }
        protected MainViewModel MainViewModel => ViewModel as MainViewModel;

        private void LoadDataButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "TXT files (*.txt)|*.txt";
            if (dialog.ShowDialog() == true)
            {
                MainViewModel.HandleChosenFile(dialog.FileName, dialog.SafeFileName);
            }
            //set combobox value to first item
            //ComboBox_SelectionChanged should be invoked
            if (sensorTypeComboBox.SelectedIndex == -1)
            {
                sensorTypeComboBox.SelectedIndex = 0;
            }
            else
            {
                var selectedPlotType = sensorTypeComboBox.SelectedItem as ReadingPlotType;
                MainViewModel.HandleReadingPlotTypeSelectionChanged(selectedPlotType);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainViewModel.Readings != null)
            {
                //Draw plot
                var combobox = sender as ComboBox;
                var selectedPlotType = combobox.SelectedItem as ReadingPlotType;
                MainViewModel.HandleReadingPlotTypeSelectionChanged(selectedPlotType);
            }
        }
    }
}
