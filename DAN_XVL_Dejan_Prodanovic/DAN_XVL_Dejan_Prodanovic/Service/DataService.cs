using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XVL_Dejan_Prodanovic.Service
{
    class DataService:IDataService
    {
        public void AddProduct(tblProduct product)
        {
            try
            {
                using (StoreDBEntities context = new StoreDBEntities())
                {

                    tblProduct newProduct = new tblProduct();
                    newProduct.ProductName = product.ProductName;
                    newProduct.Price = product.Price;
                    newProduct.Amount = product.Amount;
                    newProduct.Code = product.Code;
                    newProduct.Stored = product.Stored;

                    context.tblProducts.Add(newProduct);
                    context.SaveChanges();


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());

            }
        }

        public void EditProduct(tblProduct product)
        {
            try
            {
                using (StoreDBEntities context = new StoreDBEntities())
                {

                    tblProduct productToEdit = (from p in context.tblProducts
                                                where p.ID == product.ID
                                                select p).First();


                    //oldUserData.LastName = userToEdit.LastName;
                    //oldUserData.JMBG = userToEdit.JMBG;
                    //oldUserData.Gender = userToEdit.Gender;
                    //oldUserData.JMBG = userToEdit.JMBG;
                    //oldUserData.Gender = userToEdit.Gender;
                    //oldUserData.JMBG = userToEdit.JMBG;
                    //oldUserData.Gender = userToEdit.Gender;
                    //oldUserData.JMBG = userToEdit.JMBG;

                    productToEdit.ProductName = product.ProductName;
                    productToEdit.Price = product.Price;
                    productToEdit.Amount = product.Amount;
                    productToEdit.Code = product.Code;
                    productToEdit.Stored = product.Stored;

                    context.SaveChanges();

                    //FileLoging fileLog = FileLoging.Instance();
                    //fileLog.LogEditUserToFilevwUser(user, oldUserData);


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());

            }
        }

        public tblProduct GetProductByCode(string code)
        {
            try
            {
                using (StoreDBEntities context = new StoreDBEntities())
                {


                    tblProduct product = (from x in context.tblProducts
                                          where x.Code == code
                                          select x).First();

                    return product;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblProduct GetProductByName(string name)
        {
            try
            {
                using (StoreDBEntities context = new StoreDBEntities())
                {


                    tblProduct product = (from x in context.tblProducts
                                          where x.ProductName == name
                                          select x).First();

                    return product;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<tblProduct> GetProducts()
        {
            try
            {
                using (StoreDBEntities context = new StoreDBEntities())
                {
                    List<tblProduct> list = new List<tblProduct>();
                    list = (from x in context.tblProducts select x).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public void RemoveProduct(int productId)
        {
            try
            {
                using (StoreDBEntities context = new StoreDBEntities())
                {
                    tblProduct productToDelete = (from u in context.tblProducts
                                                  where u.ID == productId
                                                  select u).First();

                    context.tblProducts.Remove(productToDelete);

                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        }
}
