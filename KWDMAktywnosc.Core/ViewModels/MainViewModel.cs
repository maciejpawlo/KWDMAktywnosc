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

        public PlotModel Model { get; set; }

        public MainViewModel()
        {

        }

        public override Task Initialize()
        {
            this.Model = new PlotModel { Title = "Example 1" };
            this.Model.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            return base.Initialize();
        }
    }
}
