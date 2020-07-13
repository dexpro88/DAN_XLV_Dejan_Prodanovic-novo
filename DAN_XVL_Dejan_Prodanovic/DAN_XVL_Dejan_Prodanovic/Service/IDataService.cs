using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XVL_Dejan_Prodanovic.Service
{
    interface IDataService
    {
        List<tblProduct> GetProducts();
        void AddProduct(tblProduct product);
        void EditProduct(tblProduct product);
        void RemoveProduct(int productId);
        tblProduct GetProductByName(string name);
        tblProduct GetProductByCode(string code);
    }
}
