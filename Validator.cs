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

    public class Validator
    {
        private string _target = "";
        public string Target
        {
            get { return _target; }
            set { _target = value; }
        }
        private string _label = "";
        public string Label
        {
            get { return _label; }
            set { _label = value; }
        } 
        private bool _required = false; 
        public bool Required
        {
            get { return _required; }
            set { _required = value; }
        }
        private StringBuilder _message; 
        public string ErrorMessage
        {
            get { return _message.ToString(); } 
        }
        private string _ValidationCodes = "";
        private List<ValidationExpression> _ValidationResults; 
        public List<ValidationExpression> ValidationResults
        {
            get {
                if (_ValidationResults == null)
                {
                    GetValidationResults();
                }
                return _ValidationResults;  
            }
            set { _ValidationResults = value; }
        } 
        public Validator()
        { 
            _message = new StringBuilder();
        }
        public Validator(Control ControlToValidate) : this()
        {
            this._label = ControlToValidate.ExtractLabelFromControl().Trim();
            this._target = ControlToValidate.ExtractValueFromControl().Trim();
            this.Required = true;
        } 
        public Validator(string Label, string Target) : this()
        {
            this._target = Target;
            this._label = Label; 
        }
        public bool Validate(string ValidationCodes)
        {
            this._ValidationCodes = ValidationCodes; 
            var validations = (from v in ValidationResults.AsEnumerable()
                               where v.Validated == false
                               select v).ToList();

            return (validations.Count == 0);
        }
        public bool ValidateAny(string ValidationCodes)
        {
            this._ValidationCodes = ValidationCodes;
            var validations = (from v in ValidationResults.AsEnumerable()
                               where v.Validated == true
                               select v).ToList(); 
             return (validations.Count > 0);
        }
     
        private void GetValidationResults() {
            ValidationProvider provider = new ValidationProvider();
            _ValidationResults = new List<ValidationExpression>();
            foreach (var item in provider.GetValidationExpressions())
                if (("," + this._ValidationCodes).Contains("," + item.CODE))
                    _ValidationResults.Add(item);

            foreach (ValidationExpression _ValidationExpression in _ValidationResults)
            {
                _ValidationExpression.ErrorMessage = string.Format(_ValidationExpression.ErrorMessage, _label) + " " ; 
                if (_ValidationExpression.ValidationType == "REQUIRE")
                { 
                    _ValidationExpression.Validated = !string.IsNullOrEmpty(_target);
                    _message.Append(_ValidationExpression.ErrorMessage);  
                }
                if (_ValidationExpression.ValidationType == "REGEX")
                { 
                    Match match = Regex.Match(_target, _ValidationExpression.Expression);
                    _ValidationExpression.Validated = match.Success;
                    if (!match.Success)  {
                        _message.Append(_ValidationExpression.ErrorMessage);
                    } 
                } 
            } 
        }  
    }
}
