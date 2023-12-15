using Aborochka.classes;
using Aborochka.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();
            List<orders> ordersList = salonEntities.GetContext().orders.ToList();
            dataGrid.ItemsSource = ordersList;
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDialog dialog = new PrintDialog();

                if (dialog.ShowDialog() != true)
                    return;

                // Устанавливаем размеры страницы (например, в миллиметрах)
                dialog.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);
                dialog.PrintTicket.PageOrientation = PageOrientation.Landscape;

                // Вы можете также установить другие параметры, такие как PageMargin, и др.

                // Печать содержимого DataGrid
                dialog.PrintVisual(dataGrid, "Печать отчета");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Печать отчета", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCode_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame1.Navigate(new CodePage());
        }

        private void EditOrder(object sender, RoutedEventArgs e)
        {
            manager.MainFrame1.Navigate(new EditOrders((sender as Button).DataContext as orders));
        }
    }
}
