using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Data;

namespace VendingMachine
{
    abstract public class Factory
    {
        protected static Catalog cat;
        protected static Register reg;
    }
}
