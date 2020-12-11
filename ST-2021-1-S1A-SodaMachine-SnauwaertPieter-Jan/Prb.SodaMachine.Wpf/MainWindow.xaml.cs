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

namespace Prb.SodaMachine.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> newSoda = new List<string>();
        List<decimal> inputPrice = new List<decimal>();
        List<decimal> inputCoin = new List<decimal>();

        int index;
        string products;
        decimal prices;

        enum WorkingStates { 
            canAddProduct, 
            editProduct, 
            readyToOrder, 
            productSelected, 
            productDelivered }

        public MainWindow()
        {
            InitializeComponent();
            SodaAlreadyToUse();
        }

        void ChangeWorkingMode(WorkingStates workingState)
        {
            List<FrameworkElement> allFrameWorkElements = new List<FrameworkElement>
            {grdSodaMachine, grdChoiseSoda, grdNewSodaInput, btnCancel, btnCancelOrder, btnCheers, btnInput, btnManagement};
            // geef de namen van de grids en controls tussen de accolades            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillSoda();
            SodaPrice();
            Hidden();
        }

        void SodaAlreadyToUse()
        {
            AddNewSoda("Cola - € 1,5", 1.5M);
            AddNewSoda("Sprite - € 1,5", 1.5M);
            AddNewSoda("Ice-Tea - € 2", 2M);           
        }

        void SodaPrice()
        {
            lstPrice.Items.Add(0.10M);
            lstPrice.Items.Add(0.20M);
            lstPrice.Items.Add(0.50M);
            lstPrice.Items.Add(1M);
            lstPrice.Items.Add(2M);
        }


        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            Hidden();
            lstSoda.IsEnabled = true;
        }

        void Hidden()
        {
            grdNewSodaInput.IsEnabled = false;
            lstPrice.IsEnabled = false;
            btnCancelOrder.IsEnabled = false;
            btnCheers.IsEnabled = false;
            lblChange.Visibility = Visibility.Hidden;
            lblSaldo.Visibility = Visibility.Hidden;
        }

        private void btnManagement_Click(object sender, RoutedEventArgs e)
        {
            grdNewSodaInput.IsEnabled = true;
            txtProduct.Focus();
            grdChoiseSoda.IsEnabled = false;
        }

        void FillSoda()
        {
            lstSoda.ItemsSource = newSoda;
            lstSoda.Items.Refresh();
        }

        void AddNewSoda(string name, decimal price)
        {
            newSoda.Add(name);
            inputPrice.Add(price);
        }


        private void btnInput_Click(object sender, RoutedEventArgs e)
        {
           

            products = txtProduct.Text;
            prices = decimal.Parse(txtPrice.Text);

            
            newSoda[index] = products;
            inputPrice[index] = prices;
    
            txtProduct.Focus();

            index = newSoda.Count;

            AddNewSoda("", 0M);
;

            FillSoda();
        }
        private void lstSoda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstPrice.IsEnabled = true;
            btnCancelOrder.IsEnabled = true;
            lblChange.Visibility = Visibility.Visible;
            lblSaldo.Visibility = Visibility.Visible;

            if (lstSoda.SelectedItem != null)
            {
                //string nameOfSoda = "";
                decimal coins = 0M;

                index = lstSoda.SelectedIndex;

                //nameOfSoda = newSoda[index];
                coins = inputPrice[index];

                lblSaldo.Content = "Balance: € " + coins;

                lstSoda.IsEnabled = false;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtProduct.Clear();
            txtPrice.Clear();
            txtProduct.Focus();
        }

        private void lstPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Calculation();

        }

        void Calculation()
        {
            decimal sodaPrice;
            decimal coinPrice;

            sodaPrice = inputPrice[index];
            coinPrice = inputCoin[index];

            lblSaldo.Content = "Saldo: € " + (sodaPrice - coinPrice);

        }
    }
}
