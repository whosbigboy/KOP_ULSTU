using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOP.Exceptions;

public class PatternIsNullOrEmptyException : Exception
{
    public PatternIsNullOrEmptyException() : base("Pattern is null or empty") { }
}
