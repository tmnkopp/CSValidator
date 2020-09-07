using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CSValidator
{
    public class ValidationDefinitionProvider
    {
        #region PROPS
        public IEnumerable<ValidationDefinition> ValidationDefinitions
        {
            get { return GetItems(); }
        }
        public string ConnectionString { get { return ConfigurationManager.ConnectionStrings["CAClientConnectionString"].ConnectionString; } }
         
        #endregion

        #region Methods
        public IEnumerable<ValidationDefinition> Get(Expression<Func<ValidationDefinition, bool>> predicate)
        {
            return ValidationDefinitions.AsQueryable().Where(predicate).AsEnumerable<ValidationDefinition>() ;
        }
        public IEnumerable<ValidationDefinition> GetItems() { 
            using (SqlConnection conn = new SqlConnection(ConnectionString)){
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ValidatorDefinitions", conn))  {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MODE", "");
                    using (SqlDataReader rdr = cmd.ExecuteReader())  {
                        while (rdr.Read())  {
                            yield return new ValidationDefinition ()  {
                                ValidationCode = rdr["ValidationCode"].ToString(),
                                Expression = rdr["Expression"].ToString(),
                                ValidationType = rdr["ValidationType"].ToString(),
                                ErrorMessage = rdr["ErrorMessage"].ToString()
                            };
                         
                        }
                    }
                }
            }   
        }
        #endregion
    }
}
