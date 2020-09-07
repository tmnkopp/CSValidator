using System;
using System.Collections.Generic;
using System.Linq;
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
            _IsValidated = false;
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

        #region Initializers
        public Validator Init()
        {
            _IsValidated = false;
            Validators.Clear(); 
            return this;
        }
        public Validator Init(Control ControlToValidate)
        {
            Init();
            SetControlProps(ControlToValidate); 
            return this;
        } 
        #endregion
         
        #region Props
        private bool _IsValidated = false; 
        private bool _IsValid; 
        public bool IsValid
        {
            get {
                if (!_IsValidated)
                    ValidateAll();
                return _IsValid; }
            set { _IsValid = value; }
        }
        private string _ErrorMessage=""; 
        public string ErrorMessage
        {
            get {
                if (string.IsNullOrEmpty(_ErrorMessage)) 
                    foreach (IValidator item in ValidationResults) 
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

        #region Public Setters
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
        public Validator Validate(Control ControlToValidate, string ValidationCode)
        {
            Init();
            SetControlProps(ControlToValidate);
            Validators.Add(new ExpressionValidator(ValidationCode));
            return this;
        }
        public Validator Validate(Control ControlToValidate, Func<string, bool> ValidationDelegate, string ErrorMessage)
        {
            Init();
            SetControlProps(ControlToValidate);
            AddDelegate(ValidationDelegate, ErrorMessage);
            return this;
        }
        public Validator Validate(Func<string, bool> ValidationDelegate, string ErrorMessage)
        {
            Init();
            AddDelegate(ValidationDelegate, ErrorMessage);
            return this;
        }

        public Validator Validate(string StringToValidate, Func<string, bool> ValidationDelegate, string ErrorMessage)
        {
            Init();
            this._TargetValue = StringToValidate;
            AddDelegate(ValidationDelegate, ErrorMessage);
            return this;
        }
        public Validator Validate(string StringToValidate, string ValidationCode)
        {
            Init();
            this._TargetValue = StringToValidate;
            Validators.Add(new ExpressionValidator(ValidationCode));
            return this;
        }

        public Validator ApplyCode(string ValidationCode)
        {
            Validators.Add(new ExpressionValidator(ValidationCode));
            return this;
        }
        public Validator ApplyDelegate(Func<string, bool> ValidationDelegate, string ErrorMessage)
        {
            AddDelegate(ValidationDelegate, ErrorMessage);
            return this;
        }
        private Validator ValidateAll()
        {
            IsValid = (from v in ValidationResults
                       where v.IsValid == false
                       select v).ToList().Count() == 0;
            return this;
        }
        public Validator ValidateAny()
        {
            IsValid = (from v in ValidationResults
                       where v.IsValid == true
                       select v).ToList().Count() > 0;
            return this;
        }  

        #endregion

        #region Private Parts
        private void SetControlProps(Control ControlToValidate)
        {
            this._TargetCaption = ControlToValidate.ExtractCaptionFromControl().Trim();
            this._TargetValue = ControlToValidate.ExtractValueFromControl().Trim();
        } 

        private void AddDelegate(Func<string, bool> ValidationDelegate, string ErrorMessage)
        {
            Validators.Add(new DelegateValidator(ValidationDelegate, ErrorMessage));
        }
        private void AddDelegate(Func<string, bool> ValidationDelegate, string ErrorCode, string ValidationParam)
        {
            Validators.Add(new DelegateValidator(ValidationDelegate, ErrorCode, ValidationParam));
        }
        internal IEnumerable<IValidator> ValidationResults
        {
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
          
        #region Fluent Accessors
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

        #region Fluent Delegates

        public Validator Require()
        {
            AddDelegate((s) => !(string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s)), "{0} " + $"is a required field.");
            return this;
        }
        public Validator MaxLength(int Length)
        {
            if (Length < 0)
                throw new Exception("MaxLength: Length must be greater than 0.");
            AddDelegate((s) => (s.Length <= Length), "MaxLength", Length.ToString());
            return this;
        }
        public Validator MinLength(int Length)
        {
            if (Length < 0)
                throw new Exception("MinLength: Length must be greater than 0.");
            AddDelegate((s) => (s.Length > Length), "MinLength", Length.ToString());
            return this;
        }
        public Validator Equals(string Value)
        { 
            AddDelegate((s) => (s == Value), "Equals", Value);
            return this;
        }
        public Validator DoesNotEqual(string Value)
        { 
            AddDelegate((s) => (s != Value), "DoesNotEqual", Value);
            return this;
        }
        public Validator DoesNotEqualAny(string[] Values)
        {         
            if (Values==null)
                throw new ArgumentNullException("Argument Values cannot be null.");
            AddDelegate((s) => (!Values.Contains(s)), "DoesNotEqualAny", string.Join(", ", Values));
            return this;
        }
        public Validator EqualsAny(string[] Values)
        {
            if (Values == null)
                throw new ArgumentNullException("Argument Values cannot be null.");
            AddDelegate((s) => (Values.Contains(s)), "EqualsAny", string.Join(", ", Values));
            return this;
        }
         
        #endregion

    }
}
