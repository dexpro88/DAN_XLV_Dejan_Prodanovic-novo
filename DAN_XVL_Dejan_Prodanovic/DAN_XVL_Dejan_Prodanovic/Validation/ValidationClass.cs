using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XVL_Dejan_Prodanovic.Validation
{
    class ValidationClass
    {
        public static bool IsProductCodeValid(string code)
        {
            if (code.Length != 7)
                return false;


            for (int i = 0; i < code.Length; i++)
            {
                if (!Char.IsNumber(code, i))
                    return false;
            }
            return true;
        }
    }
}
