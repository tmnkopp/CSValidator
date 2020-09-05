using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSValidator
{
    public class ValidationProvider
    {
        public ValidationProvider()
        {
        }
        public IEnumerable<ValidationItem> GetValidationExpressions() {
            List<ValidationItem> ValidationExpressions = new List<ValidationItem>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CAClientConnectionString"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ValidatorExpressions", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MODE", "VALIDATION_EXPRESSIONS");
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            yield return new ValidationItem()
                            {
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
    }
}
