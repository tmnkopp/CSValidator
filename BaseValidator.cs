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

namespace CSValidator
{
     
    internal abstract class BaseValidator
    {
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
   
        #endregion

        #region CTOR
        protected StringBuilder _MessageBuilder;
        protected ValidationResult _ValidationResult;
        public BaseValidator()
        {
            _ValidationResult = new ValidationResult();
            _MessageBuilder = new StringBuilder(); 
        } 
        #endregion

        #region METHODS
        public virtual ValidationResult Validate(string Input) {
            _ValidationResult.IsValid = string.IsNullOrEmpty(Input);
            _ValidationResult.IsValid = string.IsNullOrWhiteSpace(Input);
            return _ValidationResult; 
        } 
        #endregion 
    }
}
