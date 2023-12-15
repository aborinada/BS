using Aborochka.classes;
using Aborochka.models;
using Aborochka.pages;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Aborochka.windows
{
    /// <summary>
    /// Логика взаимодействия для OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        private int remainingTimeInSeconds = 15 * 60; // 15 минут в секундах
        private DispatcherTimer timer;
        public OrdersWindow(users user)
        {
            InitializeComponent();
            DataContext = user;
            MainFrame1.Navigate(new OrdersPage());
            manager.MainFrame1 = MainFrame1;
            InitializeTimer();
        }
        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            remainingTimeInSeconds--;

            if (remainingTimeInSeconds <= 0)
            {
                timer.Stop();
                // Действия по истечении времени (например, закрытие приложения)
                Close();
            }

            // Обновление текстового блока с отображением времени
            TimerTextBlock.Text = TimeSpan.FromSeconds(remainingTimeInSeconds).ToString(@"mm\:ss");

            if (remainingTimeInSeconds == 2 * 60) // Оповещение за 2 минуты до конца
            {
                MessageBox.Show("До конца сессии осталось 2 минуты!");
            }
            else if (remainingTimeInSeconds == 60) // Оповещение за 1 минуту до конца
            {
                MessageBox.Show("До конца сессии осталась 1 минута! Приложение будет выключено.");
            }
        }

        private void ReturnToMainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
