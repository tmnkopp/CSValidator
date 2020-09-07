using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSValidator
{
    internal interface IValidator
    {
        bool IsValid { get; set; }
        string ErrorMessage { get; set; }
        IValidator Validate(string Input); 
    }  
}
