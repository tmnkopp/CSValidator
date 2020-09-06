using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace CSValidator
{
    public class Validator
    {
         
        #region CTOR
         
        private List<IValidator> Validators; 
        public Validator()
        { 
            Validators = new List<IValidator>(); 
        }
        public Validator(string StringToValidate) : this()
        {
            this._TargetValue = StringToValidate;
        }
        public Validator(Control ControlToValidate) : this()
        {
            SetControlProps(ControlToValidate);
        }

        #endregion

        #region Props
        private bool _IsValidated = false; 
        private bool _IsValid; 
        public bool IsValid
        {
            get {
                if (!_IsValidated)
                    Validate();
                return _IsValid; }
            set { _IsValid = value; }
        }
        private string _ErrorMessage=""; 
        public string ErrorMessage
        {
            get {
                if (string.IsNullOrEmpty(_ErrorMessage)) 
                    foreach (ValidationResult item in ValidationResults) 
                        _ErrorMessage += item.ErrorMessage + " ";
                if (!string.IsNullOrEmpty(_ErrorMessage))
                {
                    if (_ErrorMessage.Contains("{1}"))
                        return string.Format(_ErrorMessage, TargetCaption, "");
                    if (_ErrorMessage.Contains("{0}"))
                        return string.Format(_ErrorMessage, TargetCaption);
                } 
                return _ErrorMessage; 
            }
            set { _ErrorMessage = value; }
        }
        private string _TargetCaption;
        public string TargetCaption
        {
            get { return _TargetCaption; }
            set { _TargetCaption = value; }
        }
        private string _TargetValue;
        public string TargetValue
        {
            get { return _TargetValue; }
            set { _TargetValue = value; }
        }
        #endregion

        #region Prop Setters
        public Validator SetTargetCaption(string Caption)
        {
            _TargetCaption = Caption;
            return this;
        }
        public Validator SetErrorMessage(string ErrorMessage)
        {
            _ErrorMessage = ErrorMessage;
            return this;
        }
        #endregion

        #region Public Setters

        public Validator Validate()
        {
            IsValid = (from v in ValidationResults
                       where v.IsValid == false
                       select v
                        ).ToList().Count() == 0;
            return this;
        }
        public Validator ValidateAny()
        {
            IsValid = (from v in ValidationResults
                       where v.IsValid == true
                       select v
                        ).ToList().Count() > 0;
            return this;
        }
        public Validator ApplyValidation(Control ControlToValidate, string ValidationCode)
        {
            SetControlProps(ControlToValidate);
            Validators.Add(new ExpressionValidator(ValidationCode));
            return this;
        }
        public Validator ApplyValidation(string ValidationCode)
        {
            Validators.Add(new ExpressionValidator(ValidationCode));
            return this;
        }
        public Validator ApplyValidation(Func<string, bool> ValidationDelegate, string ErrorMessage)
        {
            AddDelegate(ValidationDelegate, ErrorMessage);
            return this;
        }

        #endregion

        #region Private Methods
        private void SetControlProps(Control ControlToValidate)
        {
            this._TargetCaption = ControlToValidate.ExtractCaptionFromControl().Trim();
            this._TargetValue = ControlToValidate.ExtractValueFromControl().Trim();
        } 
        private void AddDelegate(Func<string, bool> ValidationDelegate, string ErrorMessage)
        {
            Validators.Add(new DelegateValidator(ValidationDelegate, ErrorMessage));
        }
        public IEnumerable<ValidationResult> ValidationResults { 
            get  {
                _IsValidated = true;
                foreach (IValidator validator in Validators) 
                    yield return validator.Validate(this._TargetValue); 
            }  
        } 
        private string FormatErrorMessage(string ErrorMesssage) {
            if (ErrorMessage.Contains("{0}")) 
                 return string.Format(ErrorMesssage, TargetCaption);
            return ErrorMesssage;
        }
        #endregion
          
        #region ExpressionCodes
        public Validator IpAddress()
        {
            Validators.Add(new ExpressionValidator("IPADDRESS"));
            return this;
        }
        public Validator CIDR()
        {
            Validators.Add(new ExpressionValidator("CIDR"));
            return this;
        }
        public Validator Phone()
        {
            Validators.Add(new ExpressionValidator("PHONE"));
            return this;
        }
        public Validator Email()
        {
            Validators.Add(new ExpressionValidator("EMAIL"));
            return this;
        }
        public Validator CVE()
        {
            Validators.Add(new ExpressionValidator("CVE"));
            return this;
        }
        public Validator Numeric()
        {
            Validators.Add(new ExpressionValidator("NUMERIC"));
            return this;
        }
        #endregion

        #region Delegates

        public Validator Require()
        {
            AddDelegate((s) => (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s)), "{0} " + $"is a required field.");
            return this;
        }
        public Validator MaxLength(int Length)
        {
            if (Length < 0)
                throw new Exception("MaxLength: Length must be greater than 0.");
            AddDelegate((s) => (s.Length <= Length), "{0} " + $"must be fewer than {Length.ToString()} characters.");
            return this;
        }
        public Validator MinLength(int Length)
        {
            if (Length < 0)
                throw new Exception("MinLength: Length must be greater than 0.");
            AddDelegate((s) => (s.Length > Length), "{0} " + $"must be at least {Length.ToString()} characters.");
            return this;
        }
        public Validator Equals(string Value)
        { 
            AddDelegate((s) => (s == Value), "{0} " + $"must equal {Value.ToString()}.");
            return this;
        }
        public Validator DoesNotEqual(string Value)
        { 
            AddDelegate((s) => (s != Value), "{0} " + $"cannot equal {Value.ToString()}.");
            return this;
        }
        public Validator DoesNotEqualAny(string[] Values)
        {         
            if (Values==null)
                throw new ArgumentNullException("Argument Values cannot be null.");
            AddDelegate((s) => (!Values.Contains(s)), "{0} " + $"cannot equal any {Values.ToString()}.");
            return this;
        }
        public Validator EqualsAny(string[] Values)
        {
            if (Values == null)
                throw new ArgumentNullException("Argument Values cannot be null.");
            AddDelegate((s) => (Values.Contains(s)), "{0} " + $"must equal any {Values.ToString()}.");
            return this;
        }
         
        #endregion

    }
}
