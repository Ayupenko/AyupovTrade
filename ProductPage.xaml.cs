using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace AyupovTrade
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            InitializeComponent();
            
            var currentProduct=AyupovTradeEntities.GetContext().Product.ToList();
            ProductListView.ItemsSource = currentProduct;

            ComboType.SelectedIndex = 0;

            UpdateProduct();
        }
        private void UpdateProduct()
        {
            var currentProduct = AyupovTradeEntities.GetContext().Product.ToList();
            if(ComboType.SelectedIndex == 0)
            {
                currentProduct=currentProduct.Where(p=>(p.ProductCurrentSale>=0 && p.ProductCurrentSale<=100)).ToList();

            }
            if(ComboType.SelectedIndex == 1)
            {
                currentProduct=currentProduct.Where(p=>(p.ProductCurrentSale>=0 && p.ProductCurrentSale<=9.99)).ToList();
            }
            if (ComboType.SelectedIndex == 2)
            {
                currentProduct = currentProduct.Where(p => (p.ProductCurrentSale >= 10 && p.ProductCurrentSale <= 14.99)).ToList();
            }
            if (ComboType.SelectedIndex == 3)
            {
                currentProduct = currentProduct.Where(p => (p.ProductCurrentSale >= 15 && p.ProductCurrentSale <= 100)).ToList();
            }

            currentProduct = currentProduct.Where(p => p.ProductName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            if(RButtonDown.IsChecked.Value)
            {
                currentProduct=currentProduct.OrderByDescending(p=> p.ProductCost).ToList();

            }

            if(RButtonUp.IsChecked.Value)
            {
                currentProduct=currentProduct.OrderBy(P=>P.ProductCost).ToList();
            }

            ProductListView.ItemsSource = currentProduct;

            currentProduct = currentProduct.Where(p => p.ProductName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            ProductListView.ItemsSource = currentProduct;

            AllQuantity.Text = AyupovTradeEntities.GetContext().Product.ToList().Count().ToString();
            CurrentQuantity.Text = currentProduct.Count.ToString();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProduct();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProduct();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
        }

        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
        }
    }
}
