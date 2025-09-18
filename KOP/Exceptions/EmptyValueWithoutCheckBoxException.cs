using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOP.Exceptions;

partial class EmptyValueWithoutCheckBoxException : Exception
{
    public EmptyValueWithoutCheckBoxException() : base("String is empty, bur CheckBox isn't checked") { }
}
