using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("CSTests")]
[assembly: InternalsVisibleTo("VBTest")]
namespace CSValidator
{
    public abstract class BaseValidator: IValidator
    {
        #region CTORs 
        public BaseValidator()
        {
        }
        public BaseValidator(string StringToValidate, string ErrorMessage)
        {
            _Input = StringToValidate;
            _ErrorMessage = ErrorMessage;
        }
        #endregion

        #region PROPS 
        private string _Input = "";
        public string Input
        {
            get { return _Input; }
            set { _Input = value; }
        } 
        private bool _IsValid;
        public bool IsValid
        {
            get  { return _IsValid;  }
            set { _IsValid = value; }
        }
        private string _ErrorMessage = "";
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }
        #endregion
         
        #region METHODS
        public virtual IValidator Validate(string Input) {
            this.IsValid = !string.IsNullOrEmpty(Input) && !string.IsNullOrWhiteSpace(Input);
            return this; 
        } 
        #endregion 
    }
}
