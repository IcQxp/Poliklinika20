using Poliklinika20.DB;
using System;
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

namespace Poliklinika20.Pages
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        private bool firstTry;
        Random _random = new Random();
        string stringCaptcha;
        private DateTime blockDate;

        public Auth()
        {
            InitializeComponent();
            UpdateCaptcha();
            firstTry = true;
        }

        private void UpdateCaptcha()
        {
            stringCaptcha = " ";
            SymnolsPanel.Children.Clear();
            NoiseCanvas.Children.Clear();
            GenerateSymbols(4);
            GenerateNoise(_random.Next(10, 15));
        }
        private void GenerateSymbols(int count)
        {
            string alphabet = "QWERTYUPASDFGHJKLZXCVBNM1234567890";
            for (int i = 0; i < count; i++)
            {
                string symbol = alphabet.ElementAt(_random.Next(0, alphabet.Length)).ToString();
                TextBlock lbl = new TextBlock();
                lbl.Text = symbol;
                lbl.FontSize = _random.Next(20, 50);
                lbl.RenderTransform = new RotateTransform(_random.Next(-45, 45));
                lbl.Margin = new Thickness(10, 10, 10, 0);

                stringCaptcha += symbol;
                SymnolsPanel.Children.Add(lbl);

            }

        }

        private void GenerateNoise(int volumeNoise)
        {
            for (int i = 0; i < volumeNoise; i++)
            {
                Border border = new Border();
                border.Background = new SolidColorBrush(Color.FromArgb((byte)_random.Next(50, 150),
                                                                        (byte)_random.Next(0, 256),
                                                                        (byte)_random.Next(0, 256),
                                                                        (byte)_random.Next(0, 256)));
                border.Height = _random.Next(2, 10);
                border.Width = _random.Next(20, 100);
                border.RenderTransform = new RotateTransform(_random.Next(0, 90));

                NoiseCanvas.Children.Add(border);
                Canvas.SetLeft(border, _random.Next(0, 200));
                Canvas.SetTop(border, _random.Next(0, 70));


                Ellipse ellipse = new Ellipse();
                ellipse.Fill = new SolidColorBrush(Color.FromArgb((byte)_random.Next(50, 150),
                                                                        (byte)_random.Next(0, 256),
                                                                        (byte)_random.Next(0, 256),
                                                                        (byte)_random.Next(0, 256)));
                ellipse.Height = ellipse.Width = _random.Next(5, 50);
                ellipse.RenderTransform = new RotateTransform(_random.Next(0, 90));

                NoiseCanvas.Children.Add(ellipse);
                Canvas.SetLeft(ellipse, _random.Next(0, 200));
                Canvas.SetTop(ellipse, _random.Next(0, 70));
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateCaptcha();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Login.Text.Trim() == "" || Password.Password.Trim() == "" || (!firstTry && CaptchaBox.Text.Trim() == ""))
            {
                MessageBox.Show("Необходимо заполнить все поля");
                return;
            }

            users user = polikEntities6.GetContext().users.FirstOrDefault(u => u.login == Login.Text && u.password == Password.Password);

            if (firstTry)
            {
                if (user != null)
                {
                    var lastVisit = polikEntities6.GetContext().visit_logs.OrderByDescending(x => x.id_log).FirstOrDefault(visit => visit.users.id_user == user.id_user);
                    DateTime now = DateTime.Now;
                    // 20:00                                 ! 20:01 <= 20:02 && 20:02 <= 20:03
                    if (lastVisit == null || !(lastVisit.visit_time.AddMinutes(1) <= now && now <= lastVisit.visit_time.AddMinutes(3)))
                    {

                        MessageBox.Show("Успешный вход");
                        ((MainWindow)Application.Current.MainWindow).user = user;
                        ((MainWindow)Application.Current.MainWindow).StartSessionTimer();

                        Random rnd = new Random();
                        string ip = rnd.Next(255) + "." + rnd.Next(255) + "." + rnd.Next(255) + "." + rnd.Next(255);

                        polikEntities6.GetContext().visit_logs.Add(new visit_logs { id_user = user.id_user, ip = ip, is_archived = false, visit_time = now });
                        polikEntities6.GetContext().SaveChanges();
                        NavigationService.Navigate(new Main());
                    }
                    else
                    {
                        MessageBox.Show("Идёт кварцевание");
                    }
                }
                else
                {
                    MessageBox.Show("Неправильный логин/пароль/капча");
                    CaptchaContainer.Visibility = Visibility.Visible;
                    firstTry = false;
                    UpdateCaptcha();
                    Login.Text = "";
                    Password.Password = ""; 
                }
            }
            else
            {
                if (blockDate < DateTime.Now)
                {
                    if (user != null && CaptchaBox.Text.Trim().ToLower() == stringCaptcha.Trim().ToLower())
                    {
                        var lastVisit = polikEntities6.GetContext().visit_logs.OrderByDescending(x => x.id_log).FirstOrDefault(visit => visit.users.id_user == user.id_user);
                        DateTime now = DateTime.Now;
                        // 20:00                                            ! 20:01 <= 20:02 && 20:02 <= 20:03
                        if (lastVisit == null || !(lastVisit.visit_time.AddMinutes(1) <= now && now <= lastVisit.visit_time.AddMinutes(3)))
                        {

                            MessageBox.Show("Успешный вход");
                            ((MainWindow)Application.Current.MainWindow).user = user;
                            ((MainWindow)Application.Current.MainWindow).StartSessionTimer();

                            Random rnd = new Random();
                            string ip = rnd.Next(255) + "." + rnd.Next(255) + "." + rnd.Next(255) + "." + rnd.Next(255);

                            polikEntities6.GetContext().visit_logs.Add(new visit_logs { id_user = user.id_user, ip = ip, is_archived = false, visit_time = now });
                            polikEntities6.GetContext().SaveChanges();
                            NavigationService.Navigate(new Main());
                        }
                        else
                        {
                            MessageBox.Show("Идёт кварцевание");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неправильный логин/пароль/капча");
                        UpdateCaptcha();
                        Login.Text = "";
                        Password.Password = "";
                        blockDate = DateTime.Now.AddSeconds(10);
                    }

                }
                else
                {
                    MessageBox.Show("Вход заблокирован до: " + blockDate.ToString());
                }

            }


        }

        private void ShowPwd_Click(object sender, RoutedEventArgs e)
        {

            if (Password.Visibility == Visibility.Visible)
            {
                PasswordTB.Text = Password.Password;
                Password.Visibility = Visibility.Hidden;
                PasswordTB.Visibility = Visibility.Visible;
            }
            else
            {
                Password.Password = PasswordTB.Text;
                Password.Visibility = Visibility.Visible;
                PasswordTB.Visibility = Visibility.Hidden;

            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var mw = ((MainWindow)Application.Current.MainWindow);

            mw.user = null;
            mw.BackBtn.Visibility = Visibility.Hidden;

            var _TimerCalc = mw._TimerCalc;
            if (_TimerCalc != null)
            {
                _TimerCalc.Stop();
                _TimerCalc = null;
            }

            mw._TimerSeconds = 60;
            mw.TimerBlock.Text = "";


        }
    }
}
