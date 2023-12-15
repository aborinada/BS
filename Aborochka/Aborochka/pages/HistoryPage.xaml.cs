using Aborochka.classes;
using Aborochka.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Aborochka.pages
{
    /// <summary>
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        private ObservableCollection<LoginHistory> _filteredLoginHistory;
        public HistoryPage()
        {
            InitializeComponent();
            LoadLoginHistory();
        }
        private void LoadLoginHistory()
        {
            try
            {
                // Получаем историю входов из базы данных
                var loginHistory = connect.modelbd.LoginHistory.ToList();

                // Используем ObservableCollection для автоматического обновления интерфейса при изменении коллекции
                _filteredLoginHistory = new ObservableCollection<LoginHistory>(loginHistory);

                // Отображаем историю входов на странице
                LoginHistoryDataGrid.ItemsSource = _filteredLoginHistory;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке истории входов: " + ex.Message.ToString(),
                                "Критическая работа приложения", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void SuccessCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void SuccessCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            // Получаем изначальную коллекцию
            var originalCollection = connect.modelbd.LoginHistory.ToList();

            // Фильтруем
            var filteredCollection = originalCollection
                .Where(entry => string.IsNullOrEmpty(SearchTextBox.Text) || (entry.users != null && entry.users.login.Contains(SearchTextBox.Text)))
                .ToList();

            if (SuccessCheckBox.IsChecked == true)
            {
                // Фильтруем только успешные входы
                filteredCollection = filteredCollection
                    .Where(entry => entry.TypeVxod == "Успешно")
                    .ToList();
            }

            // Обновляем отображаемую коллекцию
            _filteredLoginHistory.Clear();
            foreach (var item in filteredCollection)
            {
                _filteredLoginHistory.Add(item);
            }
        }

        private void ResetFilters(object sender, RoutedEventArgs e)
        {
            // Просто перезагружаем изначальную коллекцию
            LoadLoginHistory();
            // Сбрасываем значения фильтров
            SearchTextBox.Text = string.Empty;
            SuccessCheckBox.IsChecked = false;
        }
    }
}
