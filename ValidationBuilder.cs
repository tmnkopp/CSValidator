using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace CSValidator
{
    public class ValidationBuilder
    {
         
        #region CTORS

        private Validator Validator = new Validator();
        public ValidationBuilder(string StringToValidate)
        {
            Validator = new Validator(StringToValidate);
        }
        public ValidationBuilder(Control ControlToValidate)
        {
            Validator = new Validator(ControlToValidate);
        }
        public ValidationBuilder(string StringToValidate, string ValidationCode)
        {
            Validator = new Validator(StringToValidate, ValidationCode);
        }
        public ValidationBuilder(Control ControlToValidate, string ValidationCode)
        {
            Validator = new Validator(ControlToValidate, ValidationCode);
        }

        #endregion

        #region PROP GETTERS

        public bool IsValid
        {
            get { return Validator.IsValid; }
        }
        public string ErrorMessage
        {
            get { return Validator.ErrorMessage; }
        }

        #endregion

        #region PROP SETTERS

        public ValidationBuilder SetControlCaption(string Caption)
        {
            Validator.ControlCaption = Caption;
            return this;
        }
        public ValidationBuilder SetErrorMessage(string ErrorMessage)
        {
            Validator.ErrorMessage = ErrorMessage;
            return this;
        }

        #endregion

        #region METHOD SETTERS

        public ValidationBuilder Validate()
        {
            Validator.SetIsValidAll();
            return this;
        }
        public ValidationBuilder ValidateAny()
        {
            Validator.SetIsValidAny();
            return this;
        }
        public ValidationBuilder AddValidation(string ValidationCode)
        { 
            Validator.ValidationItems.Add(new ValidationItem(ValidationCode));
            return this;
        }
        public ValidationBuilder AddValidationMethod(Func<string, bool> ValidationDelegate)
        {
            AddDelegate(ValidationDelegate, null);
            return this;
        }
        public ValidationBuilder AddValidationMethod(Func<string, bool> ValidationDelegate, string ValidationCode)
        {
            AddDelegate(ValidationDelegate, ValidationCode);
            return this;
        }
        #endregion

        #region DELEGATES
         
        public ValidationBuilder Require()
        {
            AddDelegate((s) => (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s)), $"REQUIRE");
            return this;
        }
        public ValidationBuilder MaxLength(int Length)
        {
            if (Length < 0)
                throw new Exception("MaxLength: Length must be greater than 0.");
            AddDelegate((s) => (s.Length <= Length), "MAXLEN", Length.ToString());
            return this;
        }
        public ValidationBuilder MinLength(int Length)
        {
            if (Length < 0)
                throw new Exception("MinLength: Length must be greater than 0.");
            AddDelegate((s) => (s.Length > Length), "MINLEN", Length.ToString());
            return this;
        }
        public ValidationBuilder DoesNotEqual(string Value)
        {
            if (string.IsNullOrWhiteSpace(Value))
                throw new ArgumentNullException("Argument Value cannot be null.");
            AddDelegate((s) => (s != Value), "DoesNotEqual", Value);
            return this;
        }
        public ValidationBuilder DoesNotEqualAny(string[] Values)
        {         
            if (Values==null)
                throw new ArgumentNullException("Argument Values cannot be null.");
            AddDelegate((s) => (!Values.Contains(s)), "DoesNotEqualAny", Values.ToString());
            return this;
        }
        public ValidationBuilder EqualsAny(string[] Values)
        {
            if (Values == null)
                throw new ArgumentNullException("Argument Values cannot be null.");
            AddDelegate((s) => (Values.Contains(s)), "EqualsAny", Values.ToString());
            return this;
        }
        private void AddDelegate(Func<string, bool> ValidationDelegate)
        {
            AddDelegate(ValidationDelegate, null, null);
        }
        private void AddDelegate(Func<string, bool> ValidationDelegate, string ValidationCode)
        {
            AddDelegate(ValidationDelegate, ValidationCode, null);
        }
        private void AddDelegate(Func<string, bool> ValidationDelegate, string ValidationCode, string DelegateArgument)
        {
            var item = new ValidationItem();
            item.ValidationMethod = ValidationDelegate;
            item.ValidationType = "FUNC";
            item.ValidationCode = ValidationCode;
            item.DelegateArgument = DelegateArgument;
            Validator.ValidationItems.Add(item);
        }

        #endregion

    }
}
