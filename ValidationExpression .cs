﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSValidator
{ 
    public class ValidationExpression   {
        public ValidationExpression ()  { }
        public ValidationExpression (string ValidationCode)
        {
            this.ValidationCode = ValidationCode;
        }
        public string ValidationCode { get; set; }
        public string ValidationType { get; set; }
        public string Expression { get; set; }
        public string ErrorMessage{ get; set; }   
    }  
}