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

namespace WpfApp1
{
    /// <summary>
    /// F1Instrument.xaml 的交互逻辑
    /// </summary>
    public partial class F1Instrument : UserControl
    {
        private int Gear;
        public F1Instrument()
        {
            InitializeComponent();
            Gear = -1;
            SetGear(0);
            SetDRS(false);
            //img_cr.Source = new BitmapImage(new Uri("/Resources/cr.png", UriKind.Relative));
            //img_lb.Source = new BitmapImage(new Uri("/Resources/lable.png", UriKind.Relative));
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
            arc_speed.EndAngle = ((421 - 120) * (sp / 360.0)) + 120;
            lb_speed.Content = sp.ToString("F0");
        }

        public void SetRPM(float rpm)
        {
            lb_rpm.Content = rpm.ToString("F0");
        }

        public void SetDRS(bool drs)
        {
            dr.Visibility = drs ? Visibility.Visible : Visibility.Hidden;
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
                    x = 366;
                    break;
                case 0:
                    x = 286;
                    break;
                case 1:
                    x = 214;
                    break;
                case 2:
                    x = 135;
                    break;
                case 3:
                    x = 55;
                    break;
                case 4:
                    x = -57;
                    break;
                case 5:
                    x = -216;
                    break;
                case 6:
                    x = -375;
                    break;
                case 7:
                    x = -530;
                    break;
                case 8:
                    x = -691;
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

            var sizeBigAn = new DoubleAnimation
            {
                From = 40,
                To = 75,
                Duration = TimeSpan.FromMilliseconds(300)
            };

            var sizeSmln = new DoubleAnimation
            {
                From = 75,
                To = 40,
                Duration = TimeSpan.FromMilliseconds(300)
            };

            switch (gear)
            {
                case -1:
                    lb_R.BeginAnimation(FontSizeProperty, sizeBigAn);
                    break;
                case 0:
                    lb_N.BeginAnimation(FontSizeProperty, sizeBigAn);
                    break;
                case 1:
                    lb_1.BeginAnimation(FontSizeProperty, sizeBigAn);
                    break;
                case 2:
                    lb_2.BeginAnimation(FontSizeProperty, sizeBigAn); break;
                case 3:
                    lb_3.BeginAnimation(FontSizeProperty, sizeBigAn); break;
                case 4:
                    lb_4.BeginAnimation(FontSizeProperty, sizeBigAn); break;
                case 5:
                    lb_5.BeginAnimation(FontSizeProperty, sizeBigAn); break;
                case 6:
                    lb_6.BeginAnimation(FontSizeProperty, sizeBigAn); break;
                case 7:
                    lb_7.BeginAnimation(FontSizeProperty, sizeBigAn); break;
                case 8:
                    lb_8.BeginAnimation(FontSizeProperty, sizeBigAn); break;
            }

            switch (Gear)
            {
                case -1:
                    lb_R.BeginAnimation(FontSizeProperty, sizeSmln);
                    break;
                case 0:
                    lb_N.BeginAnimation(FontSizeProperty, sizeSmln);
                    break;
                case 1:
                    lb_1.BeginAnimation(FontSizeProperty, sizeSmln);
                    break;
                case 2:
                    lb_2.BeginAnimation(FontSizeProperty, sizeSmln); break;
                case 3:
                    lb_3.BeginAnimation(FontSizeProperty, sizeSmln); break;
                case 4:
                    lb_4.BeginAnimation(FontSizeProperty, sizeSmln); break;
                case 5:
                    lb_5.BeginAnimation(FontSizeProperty, sizeSmln); break;
                case 6:
                    lb_6.BeginAnimation(FontSizeProperty, sizeSmln); break;
                case 7:
                    lb_7.BeginAnimation(FontSizeProperty, sizeSmln); break;
                case 8:
                    lb_8.BeginAnimation(FontSizeProperty, sizeSmln); break;
            }


            Gear = gear;
        }
    }
}
