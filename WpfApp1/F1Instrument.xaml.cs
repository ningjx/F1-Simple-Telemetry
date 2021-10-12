using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace F1Tools
{
    /// <summary>
    /// F1Instrument.xaml 的交互逻辑
    /// </summary>
    public partial class F1Instrument : UserControl
    {
        private int Gear;
        private bool DRS_On = true;
        private bool DRS_Nev = true;
        private bool DRS_Ena = true;
        private int RPM;
        private int SPEED;

        private DoubleAnimation DrsHiddenAc = new DoubleAnimation
        {
            From = 1,
            To = 0,
            Duration = TimeSpan.FromMilliseconds(200)
        };
        private DoubleAnimation DrsShowAc = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromMilliseconds(200)
        };

        private DoubleAnimation sizeBigAn = new DoubleAnimation
        {
            From = 50,
            To = 90,
            Duration = TimeSpan.FromMilliseconds(300)
        };
        private DoubleAnimation sizeSmln = new DoubleAnimation
        {
            From = 90,
            To = 50,
            Duration = TimeSpan.FromMilliseconds(300)
        };

        private ColorAnimation GearIn = new ColorAnimation
        {
            From = Colors.Gray,
            To = Colors.White,
            Duration = TimeSpan.FromMilliseconds(300),

        };
        private ColorAnimation GearOut = new ColorAnimation
        {
            From = Colors.White,
            To = Colors.Gray,
            Duration = TimeSpan.FromMilliseconds(300),

        };

        private ColorAnimation DRS_Enable_Ac = new ColorAnimation
        {
            From = Colors.White,
            To = Colors.Orange,
            Duration = TimeSpan.FromMilliseconds(200),

        };
        private ColorAnimation DRS_Disable_Ac = new ColorAnimation
        {
            From = Colors.Orange,
            To = Colors.White,
            Duration = TimeSpan.FromMilliseconds(200),

        };

        public F1Instrument()
        {
            InitializeComponent();
            Gear = -1;
            SetDRS(false);
            DRSNegative(false);
            
            //img_cr.Source = new BitmapImage(new Uri("/Resources/cr.png", UriKind.Relative));
            //img_lb.Source = new BitmapImage(new Uri("/Resources/lable.png", UriKind.Relative));
            lb_DRS.Foreground = new SolidColorBrush(Colors.White);
            DRSEnable(false);

            lb_R.Foreground = new SolidColorBrush(Colors.Gray);
            lb_N.Foreground = new SolidColorBrush(Colors.Gray);
            lb_1.Foreground = new SolidColorBrush(Colors.Gray);
            lb_2.Foreground = new SolidColorBrush(Colors.Gray);
            lb_3.Foreground = new SolidColorBrush(Colors.Gray);
            lb_4.Foreground = new SolidColorBrush(Colors.Gray);
            lb_5.Foreground = new SolidColorBrush(Colors.Gray);
            lb_6.Foreground = new SolidColorBrush(Colors.Gray);
            lb_7.Foreground = new SolidColorBrush(Colors.Gray);
            lb_8.Foreground = new SolidColorBrush(Colors.Gray);


            //需要放在Foreground初始化后面，因为xml中设置的Foreground已经被冻结，无法应用动画
            SetGear(0);
        }

        public void SetThrottle(float thr)
        {
            arc_tr.EndAngle = 128 + ((307 - 128) * thr);
        }

        public void SetBrake(float bre)
        {
            arc_break.EndAngle = 410 - ((410 - 311) * bre);
        }

        public void SetSpeed(int sp)
        {
            if (SPEED != sp)
            {
                arc_speed.EndAngle = ((421 - 120) * (sp / 360.0)) + 120;
                lb_speed.Content = sp.ToString();
                SPEED = sp;
            }
        }

        public void SetRPM(int rpm)
        {
            if (RPM != rpm)
            {
                lb_rpm.Content = rpm.ToString();
                RPM = rpm;
            }
        }

        public void SetDRS(bool drs)
        {
            if (DRS_On != drs)
            {
                if (drs)
                    dr.BeginAnimation(OpacityProperty, DrsShowAc);
                else
                    dr.BeginAnimation(OpacityProperty, DrsHiddenAc);
                DRS_On = drs;
            }
        }

        public void DRSEnable(bool enable)
        {
            if (DRS_Ena != enable)
            {
                if(enable)
                    lb_DRS.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, DRS_Enable_Ac);
                else
                    lb_DRS.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, DRS_Disable_Ac);
                DRS_Ena = enable;
            }
        }

        public void DRSNegative(bool negative)
        {
            if (DRS_Nev != negative)
            {
                lb_DRS.Foreground = negative ? new SolidColorBrush(Colors.Gray) : new SolidColorBrush(Colors.White);
                DRS_Nev = negative;
            }
        }

        public void SetGear(int gear)
        {
            if (gear < -1 || gear > 8)
                return;

            if (Gear == gear)
                return;

            int x = 0;
            switch (gear)
            {
                case -1:
                    x = 225;
                    break;
                case 0:
                    x = 175;
                    break;
                case 1:
                    x = 125;
                    break;
                case 2:
                    x = 75;
                    break;
                case 3:
                    x = 25;
                    break;
                case 4:
                    x = -50;
                    break;
                case 5:
                    x = -150;
                    break;
                case 6:
                    x = -250;
                    break;
                case 7:
                    x = -350;
                    break;
                case 8:
                    x = -450;
                    break;
            }

            ThicknessAnimation an = new ThicknessAnimation
            {
                From = gear_grid.Margin,
                To = new Thickness(x, 0, 0, 0),
                //FillBehavior = FillBehavior.HoldEnd,
                Duration = TimeSpan.FromMilliseconds(300)
            };
            gear_grid.BeginAnimation(MarginProperty, an);



            switch (gear)
            {
                case -1:
                    lb_R.BeginAnimation(FontSizeProperty, sizeBigAn);
                    lb_R.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearIn);
                    break;
                case 0:
                    lb_N.BeginAnimation(FontSizeProperty, sizeBigAn);
                    lb_N.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearIn);

                    break;
                case 1:
                    lb_1.BeginAnimation(FontSizeProperty, sizeBigAn);
                    lb_1.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearIn);

                    break;
                case 2:
                    lb_2.BeginAnimation(FontSizeProperty, sizeBigAn);
                    lb_2.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearIn);
                    break;
                case 3:
                    lb_3.BeginAnimation(FontSizeProperty, sizeBigAn);
                    lb_3.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearIn);
                    break;
                case 4:
                    lb_4.BeginAnimation(FontSizeProperty, sizeBigAn);
                    lb_4.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearIn);
                    break;
                case 5:
                    lb_5.BeginAnimation(FontSizeProperty, sizeBigAn);
                    lb_5.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearIn);
                    break;
                case 6:
                    lb_6.BeginAnimation(FontSizeProperty, sizeBigAn);
                    lb_6.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearIn);
                    break;
                case 7:
                    lb_7.BeginAnimation(FontSizeProperty, sizeBigAn);
                    lb_7.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearIn);
                    break;
                case 8:
                    lb_8.BeginAnimation(FontSizeProperty, sizeBigAn);
                    lb_8.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearIn);
                    break;
            }

            switch (Gear)
            {
                case -1:
                    lb_R.BeginAnimation(FontSizeProperty, sizeSmln);
                    lb_R.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearOut);

                    break;
                case 0:
                    lb_N.BeginAnimation(FontSizeProperty, sizeSmln);
                    lb_N.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearOut);
                    break;
                case 1:
                    lb_1.BeginAnimation(FontSizeProperty, sizeSmln);
                    lb_1.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearOut);
                    break;
                case 2:
                    lb_2.BeginAnimation(FontSizeProperty, sizeSmln);
                    lb_2.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearOut);
                    break;
                case 3:
                    lb_3.BeginAnimation(FontSizeProperty, sizeSmln);
                    lb_3.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearOut);
                    break;
                case 4:
                    lb_4.BeginAnimation(FontSizeProperty, sizeSmln);
                    lb_4.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearOut);
                    break;
                case 5:
                    lb_5.BeginAnimation(FontSizeProperty, sizeSmln);
                    lb_5.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearOut);
                    break;
                case 6:
                    lb_6.BeginAnimation(FontSizeProperty, sizeSmln);
                    lb_6.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearOut);
                    break;
                case 7:
                    lb_7.BeginAnimation(FontSizeProperty, sizeSmln);
                    lb_7.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearOut);
                    break;
                case 8:
                    lb_8.BeginAnimation(FontSizeProperty, sizeSmln);
                    lb_8.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, GearOut);
                    break;
            }


            Gear = gear;
        }
    }
}
