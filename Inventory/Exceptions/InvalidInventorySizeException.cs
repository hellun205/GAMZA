using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMJA.Inventory.Exceptions
{
  class InvalidInventorySizeException : Exception
  {
    public override string ToString()
    {
      return "The size of the inventory is not valid number";
    }
  }
}
