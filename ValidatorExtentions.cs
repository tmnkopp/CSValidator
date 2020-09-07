using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("CSTests")]
namespace CSValidator
{
    internal static class ValidatorExtentions
    {
        public static string ExtractCaptionFromControl(this object Control)
        {
            if (Control.GetType().GetProperty("ID") != null)
                return Control.GetPropertyValue("ID").ToProperCase();
            if (Control.GetType().GetProperty("Label") != null)
                return Control.GetPropertyValue("Label").ToProperCase();
            return null;
        }
        public static string ExtractValueFromControl(this object Control)
        {
            if (Control.GetType().GetProperty("SelectedDate") != null)
                return Control.GetPropertyValue("SelectedDate");
            if (Control.GetType().GetProperty("SelectedValue") != null)
                return Control.GetPropertyValue("SelectedValue");
            if (Control.GetType().GetProperty("Text") != null)
                return Control.GetPropertyValue("Text");
            return "";
        }
        public static string GetPropertyValue(this object Object, string PropertyName)
        {
            PropertyInfo prop = Object.GetType().GetProperty(PropertyName);
            if (prop == null)
                return "";
            if (prop.PropertyType == typeof(System.String))
                return (string)prop.GetValue(Object);
            if (PropertyName == "SelectedDate")
                return Convert.ToString(prop.GetValue(Object));
            return "";
        }
        public static string ToProperCase(this string the_string)
        { 
            if (the_string == null) return the_string;
            if (the_string.Length < 2) return the_string.ToUpper(); 
            int start = 0;
            while (start <= the_string.Length && char.IsLower(the_string[start]))
            {
                start++;
            } 
            string chr = "";
            int pos = (start < 5) ? start : 0; 
            for (int i = start; i < the_string.Length - 1; i++)
            {
                if (char.IsUpper(the_string[i]) && char.IsLower(the_string[i + 1])) {
                    chr += " " + the_string[i] ;
                } else {
                    chr += the_string[i];
                }    
            }
            chr += the_string[the_string.Length-1];
            return chr;
        }


    }
}
