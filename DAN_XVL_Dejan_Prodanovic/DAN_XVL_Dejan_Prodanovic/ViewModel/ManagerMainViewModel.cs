using DAN_XVL_Dejan_Prodanovic.Commands;
using DAN_XVL_Dejan_Prodanovic.Service;
using DAN_XVL_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DAN_XVL_Dejan_Prodanovic.ViewModel
{
    class ManagerMainViewModel:ViewModelBase
    {
        ManagerMainView view;
        IDataService dataService;
        EventClass eventObject = new EventClass();

        #region Constructors
        public ManagerMainViewModel(ManagerMainView managerMainOpen)
        {
            view = managerMainOpen;
            dataService = new DataService();
            ProductList = dataService.GetProducts();
            eventObject.ActionPerformed += ActionPerformed;
        }
        #endregion

        #region Properties
        private tblProduct selectetProduct;
        public tblProduct SelectetProduct
        {
            get
            {
                return selectetProduct;
            }
            set
            {
                selectetProduct = value;
                OnPropertyChanged("SelectetProduct");
            }
        }
        private List<tblProduct> productList;
        public List<tblProduct> ProductList
        {
            get
            {
                return productList;
            }
            set
            {
                productList = value;
                OnPropertyChanged("ProductList");
            }
        }
        #endregion

        #region Commands
        private ICommand logout;
        public ICommand Logout
        {
            get
            {
                if (logout == null)
                {
                    logout = new RelayCommand(param => LogoutExecute(), param => CanLogoutExecute());
                }
                return logout;
            }
        }

        private void LogoutExecute()
        {
            try
            {
                LoginView loginView = new LoginView();
                loginView.Show();
                view.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanLogoutExecute()
        {
            return true;
        }

        private ICommand deleteProduct;
        public ICommand DeleteProduct
        {
            get
            {
                if (deleteProduct == null)
                {
                    deleteProduct = new RelayCommand(param => DeleteProductExecute(),
                        param => CanDeleteProductExecute());
                }
                return deleteProduct;
            }
        }

        private void DeleteProductExecute()
        {
            try
            {
                if (SelectetProduct != null)
                {

                    MessageBoxResult result = MessageBox.Show("Are you sure that you want to delete this product?"
                       , "My App",
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

                    int productId = selectetProduct.ID;

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            string textToWrite = String.Format("You deleted {0}.",
                            SelectetProduct.ProductName);
                            eventObject.OnActionPerformed(textToWrite);
                            dataService.RemoveProduct(productId);
                            ProductList = dataService.GetProducts();

                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanDeleteProductExecute()
        {
            if (SelectetProduct == null || SelectetProduct.Stored == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }

        private void CloseExecute()
        {
            try
            {
                view.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanCloseExecute()
        {
            return true;
        }



        private ICommand addProduct;
        public ICommand AddProduct
        {
            get
            {
                if (addProduct == null)
                {
                    addProduct = new RelayCommand(param => AddProductExecute(),
                        param => CanAddProductExecute());
                }
                return addProduct;
            }
        }

        private void AddProductExecute()
        {
            try
            {
                AddProduct addProduct = new AddProduct();
                addProduct.ShowDialog();

                if ((addProduct.DataContext as AddProductViewModel).IsUpdateProduct == true)
                {
                    string productName = (addProduct.DataContext as AddProductViewModel).Product.ProductName;
                    int amount = (int)(addProduct.DataContext as AddProductViewModel).Product.Amount;
                    string textToWrite = String.Format("You added {0} of product {1}."
                          , amount, productName);
                    eventObject.OnActionPerformed(textToWrite);
                    ProductList = dataService.GetProducts();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddProductExecute()
        {

            return true;
        }

        private ICommand editProduct;
        public ICommand EditProduct
        {
            get
            {
                if (editProduct == null)
                {
                    editProduct = new RelayCommand(param => EditProductExecute(),
                        param => CanEditProductExecute());
                }
                return editProduct;
            }
        }

        private void EditProductExecute()
        {
            try
            {
                EditProduct editProduct = new EditProduct(SelectetProduct);
                editProduct.ShowDialog();

                if ((editProduct.DataContext as EditProductViewModel).IsUpdateProduct == true)
                {
                    string productName = (editProduct.DataContext as EditProductViewModel).OldProduct.ProductName;
                    int amount = (int)(editProduct.DataContext as EditProductViewModel).OldProduct.Amount;
                    string code = (editProduct.DataContext as EditProductViewModel).OldProduct.Code;
                    decimal price = (decimal)(editProduct.DataContext as EditProductViewModel).OldProduct.Price;

                    string newProductName = (editProduct.DataContext as EditProductViewModel).Product.ProductName;
                    int newAmount = (int)(editProduct.DataContext as EditProductViewModel).Product.Amount;
                    string newCode = (editProduct.DataContext as EditProductViewModel).Product.Code;
                    decimal newPrice = (decimal)(editProduct.DataContext as EditProductViewModel).Product.Price;
                    string textToWrite = String.Format("You changed product {0} {1} {2} {3} to " +
                        " {4} {5} {6} {7}."
                          , productName, amount, code, price, newProductName, newAmount, newCode, newPrice);
                    eventObject.OnActionPerformed(textToWrite);
                    ProductList = dataService.GetProducts();
                }
                else
                {
                    ProductList = dataService.GetProducts();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanEditProductExecute()
        {
            if (SelectetProduct == null)
            {
                return false;
            }
            return true;
        }
        #endregion

        void ActionPerformed(object source, TextToWriteEventArgs args)
        {
            using (StreamWriter sw = File.AppendText("../../Log.txt"))
            {
                sw.WriteLine(args.TextToWrite);
            }
        }
    }
}
