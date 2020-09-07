using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("CSTests")]
namespace CSValidator
{
    internal class DelegateValidator : BaseValidator, IValidator
    {
        ValidationDefinitionProvider _provider = new ValidationDefinitionProvider();
        private Func<string, bool> _ValidationDelegate; 
        public DelegateValidator(Func<string, bool> ValidationDelegate)
        {
            _ValidationDelegate = ValidationDelegate;
        }
        public DelegateValidator(Func<string, bool> ValidationDelegate, string ErrorMessage)
        { 
            _ValidationDelegate = ValidationDelegate;
            this.ErrorMessage = ErrorMessage;
        }
        public DelegateValidator(Func<string, bool> ValidationDelegate, string ValidationCode, string ValidationParam )
        {
            var vdef = _provider.Get((v) => (v.ValidationCode == ValidationCode));
            if (vdef.SingleOrDefault() != null)
            {
                this.ErrorMessage = (vdef != null) ? vdef.SingleOrDefault().ErrorMessage.Replace("{1}", ValidationParam) : ErrorMessage;
            }
            _ValidationDelegate = ValidationDelegate;
            this.ErrorMessage = ErrorMessage;
        }
        public override IValidator Validate(string Input)
        { 
            this.IsValid = _ValidationDelegate(Input); 
            return this;
        }
    }
} 