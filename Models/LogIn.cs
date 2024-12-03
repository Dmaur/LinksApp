using System.Net.WebSockets;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MySqlConnector;

namespace LinksApp.Models
{
    public class LogIn
    {
        private MySqlConnection dbConnection;
        private string connectionString;
        private MySqlCommand dbCommand;
        private MySqlDataReader dbReader;
        private HttpContext context;

        private string _username;
        private string _password;
        private bool _access;

        public LogIn(string myConnectionString, HttpContext myHttpContext)
        {
            connectionString = myConnectionString;
            context = myHttpContext;

            // clear out session each time this object is constructed
            context.Session.Clear();

            _username = "";
            _password = "";
            _access = false;
        }

        public string Username
        {
            set { _username = (value == null ? "" : value); }
        }

        public string password
        {
            set { _password = (value == null ? "" : value); }
        }

        public bool Access
        {
            get { return _access; }
        }

        public bool Unlock()
        {
            // assume no access
            _access = false;

            // trim 
            _username = Truncate(_username, 10);
            _password = Truncate(_password, 10);

            try
            {
                dbConnection = new MySqlConnection(connectionString);
                dbConnection.Open();

                dbCommand = new MySqlCommand("SELECT password, salted FROM tblUsers WHERE username=?username", dbConnection);
                dbCommand.Parameters.AddWithValue("?username", _username);

                dbReader = dbCommand.ExecuteReader();

                //  username does not exist
                if (!dbReader.HasRows)
                {
                    dbConnection.Close();
                    return _access;
                }
                // move to first recorrd\
                dbReader.Read();

                // hash and salt supplied password
                string hashedPassword = GetHashed(_password, dbReader["salted"].ToString());

                // compare hashed password against db
                if (hashedPassword == dbReader["password"].ToString())
                {
                    _access = true;

                    context.Session.SetString("auth", "true");
                    context.Session.SetString("username", _username);

                }
            }
            finally
            {
                dbConnection.Close();
            }

            return _access;

        }
        // ----------------------------------------------------------private methods
        private string GetSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
        
            return Convert.ToBase64String(salt);
        }

        private string GetHashed(string MyPassword, string Mysalt)
        {
            byte[] salt = Convert.FromBase64String(Mysalt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: MyPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
            

            return hashed;
        }

        private string Truncate(string value, int maxLength)
        {
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        
    }


}
