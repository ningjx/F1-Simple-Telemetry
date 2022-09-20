using System;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace F1Tools
{
    public sealed partial class F1Instrument : UserControl
    {
        private int Gear;
        private bool DRS_On = true;
        private bool DRS_Nev = true;
        private bool DRS_Ena = true;
        private int RPM;
        private double SPEED;
        private double THROTTLE;
        private double BRAKE;

        private Storyboard DrsHiddenStory = new Storyboard();
        private Storyboard DrsShowStory = new Storyboard();
        private Storyboard SizeBigStory = new Storyboard();
        private Storyboard SizeSmallStory = new Storyboard();
        private Storyboard GearInStory = new Storyboard();
        private Storyboard GearOutStory = new Storyboard();
        private Storyboard DRS_Enable_AcStory = new Storyboard();
        private Storyboard DRS_Disable_AcStory = new Storyboard();
        private Storyboard Gear_Move_Story = new Storyboard();

        private DoubleAnimation DrsHidden = new DoubleAnimation
        {
            From = 1,
            To = 0,
            Duration = TimeSpan.FromMilliseconds(200)
        };
        private DoubleAnimation DrsShow = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromMilliseconds(200)
        };

        private DoubleAnimation SizeBig = new DoubleAnimation
        {
            From = 50,
            To = 90,
            Duration = TimeSpan.FromMilliseconds(300)
        };
        private DoubleAnimation SizeSmall = new DoubleAnimation
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
            this.InitializeComponent();

            DrsHiddenStory.Children.Add(DrsHidden);
            Storyboard.SetTargetProperty(DrsHidden, "Opacity");
            Storyboard.SetTarget(DrsHidden, drs_bac);

            DrsShowStory.Children.Add(DrsShow);
            Storyboard.SetTargetProperty(DrsShow, "Opacity");
            Storyboard.SetTarget(DrsShow, drs_bac);

            SizeBigStory.Children.Add(SizeBig);
            Storyboard.SetTargetProperty(SizeBig, "FontSize");
            //Storyboard.SetTarget(sizeBigAn, drs_bac); 

            SizeSmallStory.Children.Add(SizeSmall);
            Storyboard.SetTargetProperty(SizeSmall, "FontSize");
            //Storyboard.SetTarget(sizeSmln, drs_bac);

            GearInStory.Children.Add(GearIn);
            Storyboard.SetTargetProperty(GearIn, "(Border.Foreground).(SolidColorBrush.Color)");

            GearOutStory.Children.Add(GearOut);
            Storyboard.SetTargetProperty(GearOut, "(Border.Foreground).(SolidColorBrush.Color)");

            DRS_Enable_AcStory.Children.Add(DRS_Enable_Ac);
            Storyboard.SetTargetProperty(DRS_Enable_Ac, "(Border.Foreground).(SolidColorBrush.Color)");
            Storyboard.SetTarget(DRS_Enable_Ac, lb_DRS);

            DRS_Disable_AcStory.Children.Add(DRS_Disable_Ac);
            Storyboard.SetTargetProperty(DRS_Disable_Ac, "(Border.Foreground).(SolidColorBrush.Color)");
            Storyboard.SetTarget(DRS_Disable_Ac, lb_DRS);


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

        public void SetSpeed(double speed)
        {
            if (SPEED != speed)
            {
                arc_speed.Data = Arc.DrawArc(181, 30, ((speed / 360.0) * 301.0 + 30));
                lb_speed.Text = Convert.ToInt32(speed).ToString();
                SPEED = speed;
            }
            //30-331
        }

        public void SetThrottle(double throttle)
        {
            //0-1
            if (throttle != THROTTLE)
            {
                arc_th.Data = Arc.DrawArc(138, 38, (throttle * 179 + 38));
                THROTTLE = throttle;
            }
        }

        public void SetBrake(double brake)
        {
            if (brake != BRAKE)
            {
                arc_br.Data = Arc.DrawArc(138, 321, (321 - brake * 100));
                BRAKE = brake;
            }
        }

        public void SetRPM(int rpm)
        {
            if (RPM != rpm)
            {
                lb_rpm.Text = rpm.ToString();
                RPM = rpm;
            }
        }

        public void SetDRS(bool drs)
        {
            try
            {
                if (DRS_On != drs)
                {
                    if (drs)
                        DrsShowStory.Begin();
                    else
                        DrsHiddenStory.Begin();
                    DRS_On = drs;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void DRSEnable(bool enable)
        {
            if (DRS_Ena != enable)
            {
                if (enable)
                    DRS_Enable_AcStory.Begin();
                else
                    DRS_Disable_AcStory.Begin();
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

            BindableMargin margin = new BindableMargin(gear_grid);
            DoubleAnimation gear_animation = new DoubleAnimation
            {
                From = gear_grid.Margin.Left,
                To = x,
                //FillBehavior = FillBehavior.HoldEnd,
                Duration = TimeSpan.FromMilliseconds(300),
                EnableDependentAnimation = true
            };

            Storyboard.SetTarget(gear_animation, margin);
            Storyboard.SetTargetProperty(gear_animation, "Left");
            Gear_Move_Story.Children.Add(gear_animation);
            Gear_Move_Story.Begin();



            switch (gear)
            {
                case -1:
                    Storyboard.SetTarget(SizeBig, lb_R);
                    SizeBigStory.Begin();
                    Storyboard.SetTarget(GearIn, lb_R);
                    GearInStory.Begin();
                    break;
                case 0:

                    Storyboard.SetTarget(GearIn, lb_N);
                    GearInStory.Begin();
                    Storyboard.SetTarget(SizeBig, lb_N);
                    SizeBigStory.Begin();
                    break;
                case 1:
                    Storyboard.SetTarget(SizeBig, lb_1);
                    SizeBigStory.Begin();
                    Storyboard.SetTarget(GearIn, lb_1);
                    GearInStory.Begin();
                    break;
                case 2:
                    Storyboard.SetTarget(SizeBig, lb_2);
                    SizeBigStory.Begin();
                    Storyboard.SetTarget(GearIn, lb_2);
                    GearInStory.Begin();
                    break;
                case 3:
                    Storyboard.SetTarget(SizeBig, lb_3);
                    SizeBigStory.Begin();
                    Storyboard.SetTarget(GearIn, lb_3);
                    GearInStory.Begin();
                    break;
                case 4:
                    Storyboard.SetTarget(SizeBig, lb_4);
                    SizeBigStory.Begin();
                    Storyboard.SetTarget(GearIn, lb_4);
                    GearInStory.Begin();
                    break;
                case 5:
                    Storyboard.SetTarget(SizeBig, lb_5);
                    SizeBigStory.Begin();
                    Storyboard.SetTarget(GearIn, lb_5);
                    GearInStory.Begin();
                    break;
                case 6:
                    Storyboard.SetTarget(SizeBig, lb_6);
                    SizeBigStory.Begin();
                    Storyboard.SetTarget(GearIn, lb_6);
                    GearInStory.Begin();
                    break;
                case 7:
                    Storyboard.SetTarget(SizeBig, lb_7);
                    SizeBigStory.Begin();
                    Storyboard.SetTarget(GearIn, lb_7);
                    GearInStory.Begin();
                    break;
                case 8:
                    Storyboard.SetTarget(SizeBig, lb_8);
                    SizeBigStory.Begin();
                    Storyboard.SetTarget(GearIn, lb_8);
                    GearInStory.Begin();
                    break;
            }

            switch (Gear)
            {
                case -1:
                    Storyboard.SetTarget(SizeSmall, lb_R);
                    SizeSmallStory.Begin();
                    Storyboard.SetTarget(GearOut, lb_R);
                    GearOutStory.Begin();
                    break;
                case 0:
                    Storyboard.SetTarget(SizeSmall, lb_N);
                    SizeSmallStory.Begin();
                    Storyboard.SetTarget(GearOut, lb_N);
                    GearOutStory.Begin();
                    break;
                case 1:
                    Storyboard.SetTarget(SizeSmall, lb_1);
                    SizeSmallStory.Begin();
                    Storyboard.SetTarget(GearOut, lb_1);
                    GearOutStory.Begin();
                    break;
                case 2:
                    Storyboard.SetTarget(SizeSmall, lb_2);
                    SizeSmallStory.Begin();
                    Storyboard.SetTarget(GearOut, lb_2);
                    GearOutStory.Begin();
                    break;
                case 3:
                    Storyboard.SetTarget(SizeSmall, lb_3);
                    SizeSmallStory.Begin();
                    Storyboard.SetTarget(GearOut, lb_3);
                    GearOutStory.Begin();
                    break;
                case 4:
                    Storyboard.SetTarget(SizeSmall, lb_4);
                    SizeSmallStory.Begin();
                    Storyboard.SetTarget(GearOut, lb_4);
                    GearOutStory.Begin();
                    break;
                case 5:
                    Storyboard.SetTarget(SizeSmall, lb_5);
                    SizeSmallStory.Begin();
                    Storyboard.SetTarget(GearOut, lb_5);
                    GearOutStory.Begin();
                    break;
                case 6:
                    Storyboard.SetTarget(SizeSmall, lb_6);
                    SizeSmallStory.Begin();
                    Storyboard.SetTarget(GearOut, lb_6);
                    GearOutStory.Begin();
                    break;
                case 7:
                    Storyboard.SetTarget(SizeSmall, lb_7);
                    SizeSmallStory.Begin();
                    Storyboard.SetTarget(GearOut, lb_7);
                    GearOutStory.Begin();
                    break;
                case 8:
                    Storyboard.SetTarget(SizeSmall, lb_8);
                    SizeSmallStory.Begin();
                    Storyboard.SetTarget(GearOut, lb_8);
                    GearOutStory.Begin();
                    break;
            }


            Gear = gear;
        }
    }
}
