using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMJA.Inventory.Exceptions
{
  class InvalidInventoryCellException : Exception
  {
    public override string ToString()
    {
      return "This cell of inventory is not able to use or not valid";
    }
  }
}
