using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace F1Tools
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataReciver.ReciveEvent += DataReciver_ReciveEvent; ;
        }

        private delegate void F1InstrumentDelegate(F1Control f1, object packet);
        private void DataReciver_ReciveEvent(object packet)
        {
            ShowDataHandle.F1Handle(f1, packet);
            ///f1.Dispatcher.RunAsync(new F1InstrumentDelegate(ShowDataHandle.F1Handle), f1, packet);
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {

            if (rr != null && st != null && en != null && stro != null)
                f1.SetSpeed(st.Value);
        }

        private void Slider_ValueChanged_1(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (rr != null && st != null && en != null && stro != null)
                f1.SetThrottle(en.Value / 100.0);
        }

        private void Slider_ValueChanged_2(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (rr != null && st != null && en != null && stro != null)
                f1.SetBrake(rr.Value/100.0);
        }

        private void stro_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            //if (rr != null && st != null && en != null && stro != null)
                //f1.SetTh((int)rr.Value, (int)st.Value, (int)en.Value, (int)stro.Value);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
