using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryDal
    {
        // singleton form, to access to all the functions on this class
        static Dal_Xml_imp instance = null;
        public static IDAL GetInstance()
        {
            if (instance == null)
                instance = new Dal_Xml_imp();
            return instance;
        }
    }
}
