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
        public override ValidationResult Validate(string Input)
        {
            ValidationTypeProvider _ValidationProvider = new ValidationTypeProvider();
            if (string.IsNullOrEmpty(_ValidationCode))
                throw new ArgumentNullException("Validation Code cannot be null or empty.");

            var validationItem = _ValidationProvider.ValidationTypes
                .Where(v => v.ValidationCode == _ValidationCode).Single();

            if (validationItem == null)
                throw new Exception("Validation Code not found.");

            _ValidationResult.IsValid = Regex.Match(Input, validationItem.Expression).Success;
            if (!_ValidationResult.IsValid)
            {
                _MessageBuilder.Append(validationItem.ErrorMessage);
                _ValidationResult.ErrorMessage = validationItem.ErrorMessage;
            }
            return _ValidationResult;
        }
    }
}
