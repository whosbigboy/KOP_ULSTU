using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOP.Exceptions;

public class IncorrectStringException : Exception
{
    public IncorrectStringException() : base("String isn't match the pattern") { }
}