using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;

namespace KWDMAktywnosc.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        #region Properties
        private PlotModel _model;
        public PlotModel Model 
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }

        public string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { SetProperty(ref _fileName, value); }
        }

        public string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { SetProperty(ref _filePath, value); }
        }
        #endregion

        #region Commands
        public IMvxAsyncCommand PickFilkeCommand { get; set; }
        #endregion

        public MainViewModel()
        {

        }

        public override Task Initialize()
        {
            this.Model = new PlotModel { Title = "Example 1" };
            this.Model.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            return base.Initialize();
        }

        public void HandleChosenFile(string fileName, string safeFileName)
        {
            FilePath = fileName;
            FileName = safeFileName;
        }
    }
}
