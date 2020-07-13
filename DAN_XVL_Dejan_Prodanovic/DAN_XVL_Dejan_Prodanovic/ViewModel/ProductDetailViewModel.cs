using DAN_XVL_Dejan_Prodanovic.Commands;
using DAN_XVL_Dejan_Prodanovic.Service;
using DAN_XVL_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DAN_XVL_Dejan_Prodanovic.ViewModel
{
    class ProductDetailViewModel:ViewModelBase
    {
        ProductDetail productDetail;
        IDataService dataService;
        public int StoreCount { get; set; }

        bool oldStoredValue;
        EventClass eventObject = new EventClass();

        public ProductDetailViewModel(ProductDetail productDetailOpen, tblProduct productToShow,
            int storeCount)
        {
            productDetail = productDetailOpen;
            dataService = new DataService();
            Product = productToShow;
            oldStoredValue = (bool)productToShow.Stored;
            eventObject.ActionPerformed += ActionPerformed;
            StoreCount = storeCount;
        }

        private tblProduct product;
        public tblProduct Product
        {
            get
            {
                return product;
            }
            set
            {
                product = value;
                OnPropertyChanged("Product");
            }
        }

        private bool stored;
        public bool Stored
        {
            get { return stored; }
            set
            {
                stored = value;
                OnPropertyChanged("Stored");
            }
        }

        private bool isUpdateProduct;
        public bool IsUpdateProduct
        {
            get
            {
                return isUpdateProduct;
            }
            set
            {
                isUpdateProduct = value;
            }
        }


        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private void SaveExecute()
        {
            try
            {

                Product.Stored = Stored;

                if ((bool)Product.Stored == oldStoredValue)
                {

                    string textToWrite1 = String.Format("You didn't make any changes.");
                    eventObject.OnActionPerformed(textToWrite1);
                    productDetail.Close();
                    Stored = false;
                    return;
                }

                if ((bool)Product.Stored)
                {
                    if (StoreCount + (int)Product.Amount > 100)
                    {
                        string textToWrite1 = String.Format("You can't store this product there is not" +
                            " enough space in the store.");
                        eventObject.OnActionPerformed(textToWrite1);
                        Stored = false;
                        return;
                    }
                }
                dataService.EditProduct(Product);
                IsUpdateProduct = true;
                if ((bool)Product.Stored)
                {
                    string textToWrite = String.Format("You succesfully stored {0} {1}.",
                        Product.Amount, Product.ProductName);
                    eventObject.OnActionPerformed(textToWrite);
                    StoreCount += (int)Product.Amount;
                    Stored = false;
                    productDetail.Close();

                    return;
                }

                if (!(bool)Product.Stored && oldStoredValue == true)
                {
                    string textToWrite = String.Format("You succesfully unstored {0} {1}.",
                        Product.Amount, Product.ProductName);
                    eventObject.OnActionPerformed(textToWrite);
                    StoreCount -= (int)Product.Amount;
                    Stored = false;
                    productDetail.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute()
        {
            return true;
        }

        private ICommand chooseStored;
        public ICommand ChooseStored
        {
            get
            {
                if (chooseStored == null)
                {
                    chooseStored = new RelayCommand(ChooseStoredExecute, CanChooseStoredExecute);
                }
                return chooseStored;
            }
        }

        private void ChooseStoredExecute(object parameter)
        {
            Stored = (bool)parameter;
        }

        private bool CanChooseStoredExecute(object parameter)
        {
            return true;
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
                productDetail.Close();
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

        void ActionPerformed(object source, TextToWriteEventArgs args)
        {
            MessageBox.Show(args.TextToWrite);
        }
    }
}
