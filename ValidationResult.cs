using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSValidator
{
    public class ValidationResult
    {
        public ValidationResult()
        {
        } 
        public string ErrorMessage { get; set; }   
        public bool IsValid { get; set; }
    } 
}
