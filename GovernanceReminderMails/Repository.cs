using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace GovernanceReminderMails
{
    public class Repository
    {
        IConfiguration _configuration;
        string query = "SELECT * FROM SMTP WHERE interfacestatus <> 'decomission' AND NextleadershipcallDate = '{0}'";
        string _connectionString
        {
            get
            {
                return _configuration.GetValue<string>("ConnectionString");
            }
        }
        public Repository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<SMTP>> GetSMTPs(DateTime today)
        {
            List<SMTP> smtps = new List<SMTP>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(
                  string.Format(query, today.ToString("yyyy-MM-dd")),
                  connection);

                await connection.OpenAsync();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        var smtp = new SMTP();
                        smtp.AgreedRemediation = reader.ToDate("AgreedRemediation");
                        smtp.InterfaceID = reader["InterfaceID"].ToString();
                        smtp.NextleadershipcallDate = reader.ToDate("NextleadershipcallDate");
                        smtp.NoOfInterfaces = reader.ToInt("NoOfInterfaces");
                        smtp.Notes = reader["Notes"].ToString();
                        smtp.RequestSummary = reader["RequestSummary"].ToString();
                        smtp.RITM = reader["RITM"].ToString();
                        smtp.Status = reader["Status"].ToString();
                        smtp.Task = reader["Task"].ToString();
                        smtp.EmailId = reader["EmailId"].ToString();
                        smtp.InterfaceStatus = reader["InterfaceStatus"].ToString();
                        smtps.Add(smtp);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
            }
            return smtps;
        }
    }
    public static class DataReaderExtensions
    {
        public static int ToInt(this IDataReader dr, string colName)
        {
            if (int.TryParse(dr[colName].ToString(), out var value))
            {
                return value;
            }
            return 0;
        }
        public static DateTime ToDate(this IDataReader dr, string colName)
        {
            if (DateTime.TryParse(dr[colName].ToString(), out var value))
            {
                return value;
            }
            return DateTime.MinValue;
        }
    }
}
