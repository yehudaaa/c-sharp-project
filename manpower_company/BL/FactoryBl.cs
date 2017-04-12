using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class FactoryBl
    {
        // singleton form, to access to all the functions on this class
        
        static IBL instance = null;
        public static IBL GetInstance()
        {
            if (instance == null)
                instance = new Bl_imp();
            return instance;
        }
    }
}
