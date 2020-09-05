using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSValidator
{
    public class ValidationItem
    {
        public ValidationItem()
        { 
        }
        public ValidationItem(string ValidationCode)
        {
            this.ValidationCode = ValidationCode;
        }
        public string ValidationCode { get; set; }
        public string Expression { get; set; }
        public string ErrorMessage{ get; set; } 
        public string ValidationType { get; set; }
        public bool Validated { get; set; }
        public Func<string, bool> ValidationMethod { get; set; } 
        private string _DelegateArgument = ""; 
        public string DelegateArgument
        {
            get { return _DelegateArgument; }
            set { _DelegateArgument = value; }
        }

    } 
}
