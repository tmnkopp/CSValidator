using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSValidator
{
    public enum ValidationTypes {  
        REGEX,
        FUNC
    }
    public class Validator
    {
        #region PROPS

        private string _StringToValidate = "";
        public string StringToValidate
        {
            get { return _StringToValidate; }
            set { _StringToValidate = value; }
        }
        private string _ControlCaption = "";
        public string ControlCaption
        {
            get { return _ControlCaption; }
            set { _ControlCaption = value; }
        }
        private bool _required = false;
        public bool Required
        {
            get { return _required; }
            set { _required = value; }
        }
        private bool _IsValid;
        public bool IsValid
        {
            get  {
                if (!_Validated)
                    SetIsValidAll();
                return _IsValid;
            }
            set { _IsValid = value; }
        }
        private bool _Validated = false;

        private StringBuilder _MessageBuilder;
        private string _ErrorMessage;
        public string ErrorMessage
        {
            get  {
                if (_ErrorMessage == null)
                    _ErrorMessage = _MessageBuilder.ToString();
                return _ErrorMessage;
            } 
            set { _ErrorMessage = value; }
        }  
        private List<ValidationItem> _ValidationItems;
        public List<ValidationItem> ValidationItems
        {
            get { return _ValidationItems; }
            set { _ValidationItems = value; }
        }
        private ValidationProvider _ValidationProvider;


        #endregion

        #region CTOR

        public Validator()
        {
            _ValidationProvider = new ValidationProvider();
            _MessageBuilder = new StringBuilder();
            _ValidationItems = new List<ValidationItem>(); 
        }
        public Validator(Control ControlToValidate) : this()
        {
            this._ControlCaption = ControlToValidate.ExtractLabelFromControl().Trim();
            this._StringToValidate = ControlToValidate.ExtractValueFromControl().Trim();
        }
        public Validator(Control ControlToValidate, string ValidationCode) : this(ControlToValidate)
        {
            this.ValidationItems.Add(new ValidationItem(ValidationCode));
        }
        public Validator(string StringToValidate) : this()
        {
            this._StringToValidate = StringToValidate;
        }
        public Validator(string StringToValidate, string ValidationCode) : this(StringToValidate)
        {
            this.ValidationItems.Add(new ValidationItem(ValidationCode));
        }
   

        #endregion

        #region METHODS

        public void SetIsValidAll()
        {
            ValidateItems();
            var validations = (from v in ValidationItems.AsEnumerable()
                               where v.Validated == false
                               select v).ToList();

            IsValid = (validations.Count == 0);
        }
        public void SetIsValidAny()
        {
            ValidateItems();
            var validations = (from v in ValidationItems.AsEnumerable()
                               where v.Validated == true
                               select v).ToList();
            IsValid = (validations.Count > 0);
        }
        private void MapToProvider(ValidationItem validationItem)
        {
            if (validationItem.ValidationCode == null)
                return;

            var item = _ValidationProvider
                .GetValidationExpressions()
                .Where(v => v.ValidationCode == validationItem.ValidationCode).Single();
            if (item != null)
            { 
                if (validationItem.Expression == null)
                    validationItem.Expression = item.Expression;
                if (validationItem.ErrorMessage == null)
                    validationItem.ErrorMessage = item.ErrorMessage;
                if (validationItem.ValidationType == null)
                    validationItem.ValidationType = item.ValidationType; 
            };  
        }

        private void ValidateItems() {  
        
            foreach (ValidationItem _ValidationItem in _ValidationItems)
            {
                MapToProvider(_ValidationItem);

                if (_ValidationItem.ErrorMessage != null)  { 
                    _ValidationItem.ErrorMessage = string.Format(_ValidationItem.ErrorMessage, _ControlCaption, _ValidationItem.DelegateArgument);
                }
                 
                if (_ValidationItem.ValidationType == "REGEX")
                {
                    Match match = Regex.Match(_StringToValidate, _ValidationItem.Expression);
                    _ValidationItem.Validated = match.Success;
                    if (!match.Success)
                    {
                        _MessageBuilder.Append(_ValidationItem.ErrorMessage);
                    }
                }
                if (_ValidationItem.ValidationType == "FUNC")
                {
                    _ValidationItem.Validated = _ValidationItem.ValidationMethod(_StringToValidate);
                    if (!_ValidationItem.Validated)
                        _MessageBuilder.Append(_ValidationItem.ErrorMessage);
                }
            }
            this._Validated = true;
        }

        #endregion

    }
}
