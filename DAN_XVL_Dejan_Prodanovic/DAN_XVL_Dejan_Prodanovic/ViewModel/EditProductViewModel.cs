using DAN_XVL_Dejan_Prodanovic.Commands;
using DAN_XVL_Dejan_Prodanovic.Service;
using DAN_XVL_Dejan_Prodanovic.Validation;
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
    class EditProductViewModel:ViewModelBase
    {
        EditProduct editProduct;
        IDataService dataService;
        string oldName;
        string oldCode;
        public tblProduct OldProduct { get; set; }

        public EditProductViewModel(EditProduct editProductOpen, tblProduct productToEdit)
        {
            editProduct = editProductOpen;
            dataService = new DataService();
            Product = productToEdit;
            oldName = productToEdit.ProductName;
            oldCode = productToEdit.Code;

            OldProduct = new tblProduct();
            OldProduct.ProductName = productToEdit.ProductName;
            OldProduct.Code = productToEdit.Code;
            OldProduct.Amount = productToEdit.Amount;
            OldProduct.Price = productToEdit.Price;

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
                if (Product.Price <= 0)
                {
                    MessageBox.Show("Price must be greater than 0");
                    return;
                }
                if (Product.Amount <= 0)
                {
                    MessageBox.Show("Amount must be greater than 0");
                    return;
                }

                if (!Product.ProductName.Equals(oldName) && dataService.GetProductByName(Product.ProductName) != null)
                {
                    MessageBox.Show("Product with this name already exists.");
                    return;
                }
                if (!Product.Code.Equals(oldCode) && dataService.GetProductByCode(Product.Code) != null)
                {
                    MessageBox.Show("Product with this code already exists.");
                    return;
                }
                if (!ValidationClass.IsProductCodeValid(Product.Code))
                {
                    MessageBox.Show("Product code is not valid. It has to be 7 characters long\n" +
                        "and it can contain only digits");
                    return;
                }


                dataService.EditProduct(Product);

                isUpdateProduct = true;

                editProduct.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute()
        {

            if (String.IsNullOrEmpty(Product.ProductName) || String.IsNullOrEmpty(Product.Code)
                || Product.Price == null || Product.Amount == null)
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
                editProduct.Close();
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
    }
}
