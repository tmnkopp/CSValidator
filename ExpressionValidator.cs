using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSValidator
{
    internal class ExpressionValidator : BaseValidator, IValidator
    {
        private string _ValidationCode;
        public ExpressionValidator(string ValidationCode)
        {
            _ValidationCode = ValidationCode;
        }
        public override IValidator Validate(string Input)
        {
            ValidationDefinitionProvider _ValidationProvider = new ValidationDefinitionProvider();
            if (string.IsNullOrEmpty(_ValidationCode))
                throw new ArgumentNullException("Validation Code cannot be null or empty.");

            var validationItem = _ValidationProvider.ValidationDefinitions
                .Where(v => v.ValidationCode == _ValidationCode).Single();

            if (validationItem == null)
                throw new Exception("Validation Code not found.");

            this.IsValid = Regex.Match(Input, validationItem.Expression).Success;
            if (!this.IsValid) 
                this.ErrorMessage = validationItem.ErrorMessage;
           
            return this;
        }
    }
}
