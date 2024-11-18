using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TidyDataFrame;

public class InvalidDataTypeException : Exception
{
    public InvalidDataTypeException() { }

    public InvalidDataTypeException(string message) : base(message) { }
}
