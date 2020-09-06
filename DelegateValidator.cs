using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSValidator
{
    internal class DelegateValidator : BaseValidator, IValidator
    {
        private Func<string, bool> _ValidationDelegate;
        private string _ErrorMessage = "";
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }
        public DelegateValidator(Func<string, bool> ValidationDelegate)
        {
            _ValidationDelegate = ValidationDelegate;
        }
        public DelegateValidator(Func<string, bool> ValidationDelegate, string ErrorMessage)
        {
            _ValidationDelegate = ValidationDelegate;
            _ErrorMessage = ErrorMessage;
        }
        public override ValidationResult Validate(string Input)
        {
            _ValidationResult.IsValid = _ValidationDelegate(Input);
            _ValidationResult.ErrorMessage = _ErrorMessage;
            return _ValidationResult;
        }
    }
}
