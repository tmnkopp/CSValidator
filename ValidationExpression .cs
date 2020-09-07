using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("CSTests")]
[assembly: InternalsVisibleTo("VBTest")]
namespace CSValidator
{ 
    public class ValidationDefinition   {
        public ValidationDefinition ()  { }
        public ValidationDefinition (string ValidationCode)
        {
            this.ValidationCode = ValidationCode;
        }
        public string ValidationCode { get; set; }
        public string ValidationType { get; set; }
        public string Expression { get; set; }
        public string ErrorMessage{ get; set; }   
    }  
}
