using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSValidator
{
    public class ValidationExpression
    {
        public string CODE { get; set; }
        public string Expression { get; set; }
        public string ErrorMessage{ get; set; } 
        public string ValidationType { get; set; }
        public bool Validated { get; set; }
    } 
}
