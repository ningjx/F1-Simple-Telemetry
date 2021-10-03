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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// F1Instrument.xaml 的交互逻辑
    /// </summary>
    public partial class F1Instrument : UserControl
    {
        public F1Instrument()
        {
            InitializeComponent();
        }

        public void SetThr(float thr)
        {
            arc_tr.EndAngle = 128 + ((307 - 128) * thr);
        }

        public void SetBreak(float bre)
        {
            arc_break.EndAngle = 410 - ((410 - 311) * bre);
        }

        public void SetSpeed(float sp)
        {
            arc_speed.EndAngle = sp + 120;
        }

        public void SetRPM(float rpm)
        {

        }

        public void SetDRS(bool drs)
        {
            dr.Visibility = drs ? Visibility.Visible : Visibility.Hidden;
        }

        public void SetGear(int gear)
        {

        }
    }
}
