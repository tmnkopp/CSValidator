using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("CSTests")]
[assembly: InternalsVisibleTo("VBTest")]
namespace CSValidator
{   
    public interface IValidator
    {
        bool IsValid { get; set; }
        string ErrorMessage { get; set; }
        IValidator Validate(string Input); 
    }  
}
