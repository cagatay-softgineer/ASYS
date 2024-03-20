
#region Packages
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
#endregion

namespace NTP_P1
{
    public class Database
    {

        #region Database Implementation
        public static OleDbConnection Connect = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "/ProjectDatabase.accdb" + ";Persist Security Info=False;");
        public static OleDbCommand Command = new OleDbCommand();
        public static OleDbDataReader Reader;
        private static OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
        //public static List<Product> products = new List<Product>();
        public static Product product = new Product(1, 1, 1, 1, DateTime.Now.ToShortDateString(),1);
        public static BProduct productD = new BProduct(1, "1", 1, "1", DateTime.Now.ToShortDateString(), 1);
        public static Personel user = new Personel(1, "1", "1", "1", DateTime.Now.ToShortDateString(), false, false, false,"1","1");
        private static List<(int Id, double SatisFiyati, double SatisMiktari, double AlisFiyati, string Tarih, double StokMiktari)> products = new List<(int Id, double SatisFiyati, double SatisMiktari, double AlisFiyati, string Tarih, double StokMiktari)>();
        private static List<(int Id, string UrunAdi, double SatisFiyati, string UrunGrubu, string Tarih, int StokMiktari)> productsD = new List<(int Id, string UrunAdi, double SatisFiyati, string UrunGrubu, string Tarih, int StokMiktari)>();
        private static List<(int id, string kullaniciAdi, string sifre, string mail, string girisZamani, bool root, bool defaultUser, bool employee, string Ad, string Soyadi)> users = new List<(int id, string kullaniciAdi, string sifre, string mail, string girisZamani, bool root, bool defaultUser, bool employee, string Ad, string Soyadi)>();
        DateTime time = DateTime.Today;
        static string controlChecked = null;
        #endregion

        #region Control Methods
        public bool CheckEmailExists(string email)
        {
            Command.Connection = Connect;
            Connect.Open();

            try
            {
                string query = $"SELECT COUNT(*) FROM Personel WHERE Mail = @Email";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                Connect.Close();
            }
        }
        public bool CheckProductExists(string product)
        {
            Command.Connection = Connect;
            Connect.Open();

            try
            {
                string query = $"SELECT COUNT(*) FROM Urun WHERE UrunAdi = @product";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@product", product);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                Connect.Close();
            }
        }
        public bool CheckUsernameExists(string username)
        {
            Command.Connection = Connect;
            Connect.Open();

            try
            {
                string query = $"SELECT COUNT(*) FROM Personel WHERE KullaniciAdi = @username";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@username", username);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                Connect.Close();
            }

        }
        public bool CheckResponsiblePersonel(string group)
        {
            Command.Connection = Connect;

            try
            {
                string query = $"SELECT COUNT(*) FROM Personel WHERE SorumluUrunGrubu = @group";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@group", group);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }

        }
        public bool CheckResponsiblePersonelAutoOpenConnect(string group)
        {
            Command.Connection = Connect;
            Connect.Open();
            try
            {

                string query = $"SELECT COUNT(*) FROM Personel WHERE SorumluUrunGrubu = @group";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@group", group);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                Connect.Close();
            }

        }
        public bool CheckResponsiblePersonelAutoOpenConnectWithIDS(string group, string[] userIds)
        {
            Command.Connection = Connect;
            Connect.Open();
            try
            {
                string query = "SELECT COUNT(*) FROM Personel WHERE SorumluUrunGrubu = @group AND ID IN (" +
                                string.Join(", ", userIds) + ")";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@group", group);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                Connect.Close();
            }

        }
        public bool UsernameExistsForIDs(string username, string[] userIds)
        {
            Command.Connection = Connect;
            Connect.Open();
            try
            {
                // Use the IN clause to check for the username with the specified IDs
                string commandText = "SELECT COUNT(*) FROM Personel WHERE KullaniciAdi = @Username AND ID IN (" +
                                 string.Join(", ", userIds) + ")";

                using (OleDbCommand command = new OleDbCommand(commandText, Connect))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    // ExecuteScalar returns the result of the query (in this case, the count of matching usernames for the specified IDs)
                    int count = (int)command.ExecuteScalar();

                    // If count is greater than 0, the username exists for at least one ID
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                Connect.Close();
            }
        }
        public bool MailExistsForIDs(string mail, string[] userIds)
        {
            Command.Connection = Connect;
            Connect.Open();
            try
            {
                // Use the IN clause to check for the username with the specified IDs
                string commandText = "SELECT COUNT(*) FROM Personel WHERE Mail = @Mail AND ID IN (" +
                                 string.Join(", ", userIds) + ")";

                using (OleDbCommand command = new OleDbCommand(commandText, Connect))
                {
                    command.Parameters.AddWithValue("@Mail", mail);

                    // ExecuteScalar returns the result of the query (in this case, the count of matching usernames for the specified IDs)
                    int count = (int)command.ExecuteScalar();

                    // If count is greater than 0, the username exists for at least one ID
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                Connect.Close();
            }
        }
        public bool DateExistsForIDs(string date, string[] userIds, string tableName)
        {
            Command.Connection = Connect;
            Connect.Open();
            try
            {
                // Use the IN clause to check for the username with the specified IDsd
                string commandText = $"SELECT COUNT(*) FROM [{tableName}] WHERE Tarih = @Date AND ID IN (" +
                                 string.Join(", ", userIds) + ")";

                using (OleDbCommand command = new OleDbCommand(commandText, Connect))
                {
                    command.Parameters.AddWithValue("@Date", date);

                    // ExecuteScalar returns the result of the query (in this case, the count of matching usernames for the specified IDs)
                    int count = (int)command.ExecuteScalar();

                    // If count is greater than 0, the username exists for at least one ID
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                Connect.Close();
            }
        }
        public static bool DateExistsInDatabase(string From, string selectedDate)// SIkıntılı BU
        {
            Command.Connection = Connect;
            Connect.Open();
            try
            {
                string query = $"SELECT COUNT(*) FROM [{From}] WHERE Tarih = @SelectedDate";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    // Add parameter
                    command.Parameters.AddWithValue("@SelectedDate", selectedDate);

                    // Execute scalar query to get the count of records with the selected date
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        int count = Convert.ToInt32(result);
                        return count > 0;
                    }
                    else
                    {
                        // Handle the case where the result is null or DBNull.Value
                        MessageBox.Show("Unexpected result from the database query", Program.Output[19]);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or throw if needed
                MessageBox.Show($"{ex.Message}\nTarih Seçtiğinizden Emin Olun!", Program.Output[19]);
                return false;
            }
            finally
            {

                Connect.Close();
            }
        }
        public bool isAssignedAnyGroup(string username)
        {
            Command.Connection = Connect;
            try
            {
                string query = $"SELECT SorumluUrunGrubu FROM Personel WHERE KullaniciAdi = @username";
                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@username", username);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string value = reader["SorumluUrunGrubu"] as string;

                            // Check if the value is not null or empty
                            if (!string.IsNullOrEmpty(value))
                            {
                                return true; // Return true if there is a non-empty value
                            }
                        }

                        // Return false if no non-empty values were found
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        public bool CheckInfoExist(string tableName)
        {
            Command.Connection = Connect;
            if (Connect.State != ConnectionState.Open)
                Connect.Open();
            try
            {
                string query = $"SELECT COUNT(*) FROM [{tableName}] WHERE ID IS NOT NULL";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    //command.Parameters.AddWithValue("@id", id);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                    Connect.Close();
            }

        }
        public bool CheckRoot(string username)
        {
            Command.Connection = Connect;
            if (Connect.State != ConnectionState.Open)
                Connect.Open();
            try
            {
                string query = $"SELECT Root FROM Personel WHERE KullaniciAdi = '{username}'";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetBoolean(reader.GetOrdinal("Root"));
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                    Connect.Close();
            }
        }
        public bool CheckEmployee(string username)
        {
            Command.Connection = Connect;
            if (Connect.State != ConnectionState.Open)
                Connect.Open();
            try
            {
                string query = $"SELECT Employee FROM Personel WHERE KullaniciAdi = '{username}'";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetBoolean(reader.GetOrdinal("Employee"));
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                    Connect.Close();
            }
        }
        public bool CheckDefaultUser(string username)
        {
            Command.Connection = Connect;
            if (Connect.State != ConnectionState.Open)
                Connect.Open();
            try
            {
                string query = $"SELECT defaultUser FROM Personel WHERE KullaniciAdi = '{username}'";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetBoolean(reader.GetOrdinal("defaultUser"));
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                    Connect.Close();
            }
        }
        #endregion

        #region Column Methods
        public double GetAverageOfColumn(string productName, string columnName)
        {
            try
            {
                Connect.Open();

                string query = $"SELECT {columnName} FROM [{productName}]";
                Command.CommandText = query;

                using (OleDbDataReader reader = Command.ExecuteReader())
                {
                    List<double> values = new List<double>();

                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) // Check if the value is not null
                        {
                            double value = Convert.ToDouble(reader[columnName]);
                            values.Add(value);
                        }
                    }

                    if (values.Count > 0)
                    {
                        double average = values.Average();
                        return Convert.ToDouble(average.ToString("F2"));
                    }
                    else
                    {
                        MessageBox.Show(Program.Output[114]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                }
            }

            return 0; // Default value if there is an issue or no values found
        }
        public double GetAverageOfColumn(string productName, string columnName, string WantedCount)
        {
            try
            {
                Connect.Open();

                string query = $"SELECT TOP {WantedCount} {columnName} FROM [{productName}] ORDER BY Tarih DESC";
                Command.CommandText = query;

                using (OleDbDataReader reader = Command.ExecuteReader())
                {
                    List<double> values = new List<double>();

                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) // Check if the value is not null
                        {
                            double value = Convert.ToDouble(reader[columnName]);
                            values.Add(value);
                        }
                    }

                    if (values.Count > 0)
                    {
                        double average = values.Average();
                        return Convert.ToDouble(average.ToString("F2"));
                    }
                    else
                    {
                        MessageBox.Show(Program.Output[114]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                }
            }

            return 0; // Default value if there is an issue or no values found
        }

        public string[] GetMailFromAdmins()
        {
            try
            {
                Connect.Open();
                string query = "SELECT Mail FROM Personel WHERE Root = @isAdmin";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    List<string> values = new List<string>();
                    // Assuming admin is a boolean parameter
                    command.Parameters.Add("@isAdmin", OleDbType.Boolean).Value = true;

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string value = reader["Mail"].ToString();
                            values.Add(value);
                        }
                    }
                    if (values.Count > 0)
                    {
                        return values.ToArray();
                    }
                    else
                    {
                        MessageBox.Show(Program.Output[114]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                }
            }

            return new string[0]; // Default value if there is an issue or no values found
        }

        public string[] GetValuesOfColumn(string productName, string columnName)
        {
            try
            {
                Connect.Open();

                string query = $"SELECT {columnName} FROM [{productName}]";
                Command.CommandText = query;

                using (OleDbDataReader reader = Command.ExecuteReader())
                {
                    List<string> values = new List<string>();

                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) // Check if the value is not null
                        {
                            string value = reader[columnName].ToString();
                            values.Add(value);
                        }
                    }

                    if (values.Count > 0)
                    {
                        return values.ToArray();
                    }
                    else
                    {
                        MessageBox.Show(Program.Output[114]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                }
            }

            return new string[0]; // Default value if there is an issue or no values found
        }
        public string[] GetValuesOfColumnDiff(string productName, string columnName)
        {
            try
            {
                Connect.Open();

                string query = $"SELECT {columnName} FROM [{productName}]";
                Command.CommandText = query;

                using (OleDbDataReader reader = Command.ExecuteReader())
                {
                    List<string> values = new List<string>();

                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            string value = reader[columnName].ToString();
                            if (!values.Contains(value))
                            {
                                values.Add(value);
                            }
                        }
                    }

                    if (values.Count > 0)
                    {
                        values.Sort(StringComparer.CurrentCultureIgnoreCase);
                        return values.ToArray();
                    }
                    else
                    {
                        MessageBox.Show(Program.Output[114]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                }
            }

            return new string[0]; // Default value if there is an issue or no values found
        }
        public string[] GetValuesOfColumnWithGroup(string TableName, string columnName, string Wanted)
        {

            try
            {
                Connect.Open();

                string query = $"SELECT [{columnName}] FROM [{TableName}] WHERE UrunGrubu = @Wanted";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {

                    command.Parameters.AddWithValue("@Wanted", Wanted);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        List<string> values = new List<string>();

                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                string value = reader[columnName].ToString();
                                if (!values.Contains(value))
                                {
                                    values.Add(value);
                                }
                            }
                        }

                        if (values.Count > 0)
                        {
                            values.Sort(StringComparer.CurrentCultureIgnoreCase);
                            return values.ToArray();
                        }

                        else
                        {
                            MessageBox.Show(Program.Output[114]);
                            return new string[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),ex.GetType().ToString());
                return new string[0];
            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                }
            }

            return new string[0]; // Default value if there is an issue or no values found
        }

        public string[] GetValuesOfColumnJustDiffrentOnes(string productName, string columnName)
        {
            try
            {
                Connect.Open();

                string query = $"SELECT {columnName} FROM [{productName}]";
                Command.CommandText = query;

                using (OleDbDataReader reader = Command.ExecuteReader())
                {
                    List<string> values = new List<string>();

                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) // Check if the value is not null
                        {
                            string value = reader[columnName].ToString();
                            if (!values.Contains(value))
                            {
                                values.Add(value);
                            }
                        }
                    }

                    if (values.Count > 0)
                    {
                        return values.ToArray();
                    }
                    else
                    {
                        MessageBox.Show(Program.Output[114]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                }
            }

            return new string[0]; // Default value if there is an issue or no values found
        }


        public string[] GetValuesOfColumnSortedByDate(string productName, string columnName)
        {
            try
            {
                Connect.Open();

                string query = $"SELECT {columnName} FROM [{productName}] ORDER BY Tarih";
                Command.CommandText = query;

                using (OleDbDataReader reader = Command.ExecuteReader())
                {
                    List<string> values = new List<string>();

                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) // Check if the value is not null
                        {
                            string value = reader[columnName].ToString();
                            values.Add(value);
                        }
                    }

                    if (values.Count > 0)
                    {
                        return values.ToArray();
                    }
                    else
                    {
                        MessageBox.Show(Program.Output[114]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                }
            }

            return new string[0]; // Default value if there is an issue or no values found
        }
        public string[] GetValuesOfColumnSortedByDate(string productName, string columnName,string wantedAmount)
        {
            try
            {
                Connect.Open();

                string query = $"SELECT TOP {wantedAmount} {columnName} FROM [{productName}] ORDER BY Tarih";
                Command.CommandText = query;

                using (OleDbDataReader reader = Command.ExecuteReader())
                {
                    List<string> values = new List<string>();

                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) // Check if the value is not null
                        {
                            string value = reader[columnName].ToString();
                            values.Add(value);
                        }
                    }

                    if (values.Count > 0)
                    {
                        return values.ToArray();
                    }
                    else
                    {
                        MessageBox.Show(Program.Output[114]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                }
            }

            return new string[0]; // Default value if there is an issue or no values found
        }

        public string[] GetLanguagePacks(string productName, string columnName)
        {
            try
            {
                if (Connect.State == ConnectionState.Closed)
                {
                    Connect.Open();
                }

                string query = $"SELECT [{columnName}] FROM [{productName}] ORDER BY ID";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        List<string> values = new List<string>();

                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0)) // Check if the value is not null
                            {
                                string value = reader[columnName].ToString();
                                values.Add(value);
                            }
                        }

                        if (values.Count > 0)
                        {
                            return values.ToArray();
                        }
                        else
                        {
                            throw new InvalidOperationException(Program.Output[114]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Ensure the connection is closed in case of an exception
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                }
            }

            return new string[0]; // Default value if there is an issue or no values found
        }

        public string[] GetLanguagePacksTest(string productName, string columnName)
        {
            if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(productName))
            {
                try
                {
                    if (Connect.State == ConnectionState.Closed)
                    {
                        Connect.Open();
                    }

                    string query = $"SELECT [{columnName}] FROM  [{productName}] ORDER BY ID";

                    using (OleDbCommand command = new OleDbCommand(query, Connect))
                    {
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            List<string> values = new List<string>();

                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0)) // Check if the value is not null
                                {
                                    string value = reader[columnName].ToString();
                                    values.Add(value);
                                }
                            }

                            if (values.Count > 0)
                            {
                                Program.LanguagePackTaken = true;
                                return values.ToArray();
                            }
                            else
                            {
                                Program.LanguagePackTaken = false;
                                throw new InvalidOperationException(Program.Output[114]);

                            }
                        }
                    }
                }
                catch (OleDbException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}");
                    Program.LanguagePackTaken = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.ToString()}");
                }
                finally
                {
                    // Ensure the connection is closed in case of an exception
                    if (Connect.State == ConnectionState.Open)
                    {
                        Connect.Close();
                    }
                }
            }
            else
            {
                Program.LanguagePackTaken = false;
                return new string[0];
                // Default value if there is an issue or no values found
            }
            Program.LanguagePackTaken = false;
            return new string[0];

        }

        public int GetCountOfColumn(string productName, string columnName)
        {
            try
            {
                Connect.Open();

                string query = $"SELECT {columnName} FROM [{productName}]";
                Command.CommandText = query;

                using (OleDbDataReader reader = Command.ExecuteReader())
                {
                    List<string> values = new List<string>();

                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) // Check if the value is not null
                        {
                            string value = reader[columnName].ToString();
                            values.Add(value);
                        }
                    }


                    return values.Count;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                }
            }

            return 0; // Default value if there is an issue or no values found
        }
        public int GetCountOfColumn(string productName, string columnName, string Group)
        {
            try
            {
                Connect.Open();

                string query = $"SELECT {columnName} FROM [{productName}] WHERE UrunGrubu = @Group";
                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@Group", Group);
                    using (OleDbDataReader reader = Command.ExecuteReader())
                    {
                        List<string> values = new List<string>();

                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0)) // Check if the value is not null
                            {
                                string value = reader[columnName].ToString();
                                values.Add(value);
                            }
                        }


                        return values.Count;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                }
            }

            return 0; // Default value if there is an issue or no values found
        }

        public int GetCountOfColumn(string productName, string columnName, DateTime lowerDate, DateTime upperDate)
        {
            try
            {
                Connect.Open();

                string query = $"SELECT {columnName} FROM [{productName}] WHERE Tarih BETWEEN @StartDate AND @EndDate";
                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@StartDate", lowerDate);
                    command.Parameters.AddWithValue("@EndDate", upperDate);
                    using (OleDbDataReader reader = Command.ExecuteReader())
                    {
                        List<string> values = new List<string>();

                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0)) // Check if the value is not null
                            {
                                string value = reader[columnName].ToString();
                                values.Add(value);
                            }
                        }
                        return values.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                }
            }

            return 0; // Default value if there is an issue or no values found
        }
        #endregion

        #region Table Methods
        //Methods that use DataTable variable
        public static void TableCleaner(DataTable table)
        {

            table.Columns.Clear();
            table.Rows.Clear();
        } // its for cleaning the DataTable variable
        public static DataTable DGWShow(DataTable datatable, string tableName, string filter = "*") // its for showing data in DataGridView
        {
            try
            {
                TableCleaner(datatable);

                Command.Connection = Connect;
                Command.CommandText = String.Format("select {0} from {1}", filter, tableName);

                //Adapter = new OleDbDataAdapter(Command);

                Adapter.Fill(datatable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                }
            }

            return datatable;
        }

        public static void TableCreate(string newTableName)
        {
            Command.Connection = Connect;
            Connect.Open();
            try
            {

                Command.CommandText = String.Format($"CREATE TABLE {newTableName}(ID AUTOINCREMENT, SatışMiktarı int, SatışFiyatı int, AlışFiyatı int, StokMiktari int, Tarih DATE)");
                Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connect.Close();
            }
        }

        public static void TableCreateSilent(string newTableName)
        {
            Command.Connection = Connect;
            Connect.Open();
            try
            {

                Command.CommandText = String.Format($"CREATE TABLE {newTableName}(ID AUTOINCREMENT, SatışMiktarı int, SatışFiyatı int, AlışFiyatı int, StokMiktari int, Tarih DATE)");
                Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                Connect.Close();
            }
        }

        #endregion

        #region Select,Insert Methods
        public void Select(string want, string tableName, string control)
        {


            if (control == null)
            {
                controlChecked = null;
            }  //checks if there are a control statement with WHERE
            else
            {
                controlChecked = "where " + control;
            }

            try
            {

                Command.Connection = Connect;
                Command.CommandText = String.Format("Select {0} from {1} {2}", want, tableName, controlChecked);
                Reader = Database.Command.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string SelectKullaniciAdi(string condition)
        {
            string kullaniciAdiValue = null;

            try
            {
                Command.Connection = Connect;
                Command.CommandText = $"SELECT KullaniciAdi FROM Personel WHERE {condition}";

                Reader = Command.ExecuteReader();

                // Check if there are rows
                if (Reader.HasRows)
                {
                    // Read the first row
                    Reader.Read();

                    // Get the value of the column named "KullaniciAdi"
                    kullaniciAdiValue = Reader["KullaniciAdi"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Don't forget to close the reader when you're done
                if (Reader != null && !Reader.IsClosed)
                {
                    Reader.Close();
                }
            }

            return kullaniciAdiValue;
        }

        public string GetKullaniciAdi(string username)
        {
            Command.Connection = Connect;
            try
            {
                string query = $"SELECT KullaniciAdi FROM Personel WHERE KullaniciAdi = @username";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@username", username);

                    object result = command.ExecuteScalar();

                    // If the result is not null, return the KullaniciAdi value
                    return result != null ? result.ToString() : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public string GetKullaniciAdiWithID(string id)
        {
            Command.Connection = Connect;
            Connect.Open();
            try
            {
                string query = $"SELECT KullaniciAdi FROM Personel WHERE ID = @ID";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    object result = command.ExecuteScalar();

                    // If the result is not null, return the KullaniciAdi value
                    return result != null ? result.ToString() : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                Connect.Close();
            }
        }

        public string GetSettingsWithUsername(string username)
        {
            Command.Connection = Connect;
            try
            {
                string query = $"SELECT Settings FROM Personel WHERE KullaniciAdi = @username";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@username", username);

                    object result = command.ExecuteScalar();

                    // If the result is not null, return the KullaniciAdi value
                    return result != null ? result.ToString() : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public string GetGroupWithUsername(string username)
        {
            Command.Connection = Connect;
            if (Connect.State != ConnectionState.Open)
                Connect.Open();
            try
            {
                string query = $"SELECT SorumluUrunGrubu FROM Personel WHERE KullaniciAdi = @username";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@username", username);

                    object result = command.ExecuteScalar();

                    // If the result is not null, return the KullaniciAdi value
                    return result != null ? result.ToString() : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                    Connect.Close();
            }
        }
        public string GetGroupWithUsernameWithoutAutoOpenConnect(string username)
        {
            try
            {
                string query = $"SELECT SorumluUrunGrubu FROM Personel WHERE KullaniciAdi = @username";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@username", username);

                    object result = command.ExecuteScalar();

                    // If the result is not null, return the KullaniciAdi value
                    return result != null ? result.ToString() : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public string[] GetNameAndSurnameWithGroup(string group)
        {
            string[] names = new string[2];
            string[] nullNames = new string[2];
            nullNames[0] = "";
            nullNames[1] = "";
            Command.Connection = Connect;
            if (Connect.State != ConnectionState.Open)
                Connect.Open();
            if (CheckResponsiblePersonel(group))
            {


                try
                {
                    string query = $"SELECT Ad,Soyad FROM Personel WHERE SorumluUrunGrubu = @group";

                    using (OleDbCommand command = new OleDbCommand(query, Connect))
                    {
                        command.Parameters.AddWithValue("@group", group);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            // Check if there is at least one row
                            if (reader.Read())
                            {
                                // Concatenate Ad and Soyad and return the result
                                names[0] = $"{reader["Ad"]}";
                                names[1] = $"{reader["Soyad"]}";
                                return names;
                            }
                            else
                            {
                                // No matching record found
                                return nullNames;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return nullNames;
                }
                finally
                {
                    if (Connect.State == ConnectionState.Open)
                        Connect.Close();
                }
            }
            else
            {
                return nullNames;
            }
        }
        public string[] GetNameAndSurnameWithGroupWithoutAutoConnect(string group)
        {
            string[] names = new string[2];
            string[] nullNames = new string[2];
            nullNames[0] = "";
            nullNames[1] = "";
            Command.Connection = Connect;
            if (CheckResponsiblePersonel(group))
            {


                try
                {
                    string query = $"SELECT Ad,Soyad FROM Personel WHERE SorumluUrunGrubu = @group";

                    using (OleDbCommand command = new OleDbCommand(query, Connect))
                    {
                        command.Parameters.AddWithValue("@group", group);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            // Check if there is at least one row
                            if (reader.Read())
                            {
                                // Concatenate Ad and Soyad and return the result
                                names[0] = $"{reader["Ad"]}";
                                names[1] = $"{reader["Soyad"]}";
                                return names;
                            }
                            else
                            {
                                // No matching record found
                                return nullNames;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return nullNames;
                }
            }
            else
            {
                return nullNames;
            }
        }
        public string[] GetNameAndSurnameWithUsername(string username)
        {
            string[] names = new string[2];
            string[] nullNames = new string[2];
            nullNames[0] = "";
            nullNames[1] = "";

            Command.Connection = Connect;

            if (Connect.State != ConnectionState.Open)
                Connect.Open();

            try
            {
                string query = $"SELECT Ad,Soyad FROM Personel WHERE KullaniciAdi = @username";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@username", username);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        // Check if there is at least one row
                        if (reader.Read())
                        {
                            // Concatenate Ad and Soyad and return the result
                            names[0] = $"{reader["Ad"]}";
                            names[1] = $"{reader["Soyad"]}";
                            return names;
                        }
                        else
                        {
                            // No matching record found
                            return nullNames;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return nullNames;
            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                    Connect.Close();
            }

        }

        public string GetImageFromUsername(string username)
        {
            Command.Connection = Connect;
            if (Connect.State != ConnectionState.Open)
                Connect.Open();
            try
            {
                string query = $"SELECT Resim FROM Personel WHERE KullaniciAdi = @username";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@username", username);

                    object result = command.ExecuteScalar();

                    // If the result is not null, return the KullaniciAdi value
                    return result != null ? result.ToString() : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;

            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                    Connect.Close();
            }
        }

        public string GetSettingsWithUsernameBase(string username)
        {
            Command.Connection = Connect;
            if (Connect.State != ConnectionState.Open)
                Connect.Open();
            try
            {
                string query = $"SELECT Settings FROM Personel WHERE KullaniciAdi = @username";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@username", username);

                    object result = command.ExecuteScalar();

                    // If the result is not null, return the KullaniciAdi value
                    return result != null ? result.ToString() : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;

            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                    Connect.Close();
            }
        }

        public string GetIDWithUsername(string username)
        {
            Command.Connection = Connect;
            try
            {
                string query = $"SELECT ID FROM Personel WHERE KullaniciAdi = @username";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@username", username);

                    object result = command.ExecuteScalar();

                    // If the result is not null, return the KullaniciAdi value
                    return result != null ? result.ToString() : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public string GetImage(string id)
        {
            Command.Connection = Connect;
            try
            {
                string query = $"SELECT KullaniciAdi FROM Personel WHERE ID = @id";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@id", id);

                    object result = command.ExecuteScalar();

                    // If the result is not null, return the KullaniciAdi value
                    return result != null ? result.ToString() : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public string GetKullaniciAdiFromMail(string email)
        {
            Command.Connection = Connect;
            Connect.Open();
            try
            {
                string query = $"SELECT KullaniciAdi FROM Personel WHERE Mail = @Email";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    object result = command.ExecuteScalar();

                    // If the result is not null, return the KullaniciAdi value
                    return result != null ? result.ToString() : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                Connect.Close();
            }
        }

        public string GetMailWithID(string id)
        {
            Command.Connection = Connect;
            Connect.Open();
            try
            {
                string query = $"SELECT Mail FROM Personel WHERE ID = @ID";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    object result = command.ExecuteScalar();

                    // If the result is not null, return the KullaniciAdi value
                    return result != null ? result.ToString() : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                Connect.Close();
            }
        }

        public string GetDateWithID(string id)
        {
            Command.Connection = Connect;
            Connect.Open();
            try
            {
                string query = $"SELECT GirisZamani FROM Personel WHERE ID = @ID";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    object result = command.ExecuteScalar();

                    // If the result is not null, return the KullaniciAdi value
                    return result != null ? result.ToString() : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                Connect.Close();
            }
        }


        public string GetSifre(string sifre)
        {
            Command.Connection = Connect;


            try
            {
                string query = $"SELECT Sifre FROM Personel WHERE Sifre = @sifre";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@sifre", sifre);

                    object result = command.ExecuteScalar();

                    // If the result is not null, return the Sifre value
                    return result != null ? result.ToString() : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public string GetSifreFromUsername(string username)
        {
            Command.Connection = Connect;
            try
            {
                string query = $"SELECT Sifre FROM Personel WHERE KullaniciAdi = @username";

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@username", username);

                    object result = command.ExecuteScalar();

                    // If the result is not null, return the Sifre value
                    return result != null ? result.ToString() : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public async static void Insert(string tableName, string[] columns, string[] values)
        {
            try
            {
                Command.Connection = Connect;
                Command.CommandText = String.Format("insert into [{0}] ({1}) values ('{2}')", tableName, string.Join(", ", columns), string.Join("', '", values));
                Command.ExecuteNonQuery();
                await Task.Run(() =>
                {
                    MessageBox.Show(Program.Output[115]);
                });
            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    MessageBox.Show(ex.Message);
                });
            }
        }
        public static void InsertSilent(string tableName, string[] columns, string[] values)
        {
            try
            {
                Command.Connection = Connect;
                Command.CommandText = String.Format("insert into [{0}] ({1}) values ('{2}')", tableName, string.Join(", ", columns), string.Join("', '", values));
                Command.ExecuteNonQuery();
                //MessageBox.Show(Program.Output[115]);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }


        public static void InsertEncoded1(string tableName, string[] columns, string[] values)
        {
            try
            {
                if (columns.Length != values.Length)
                {
                    MessageBox.Show(Program.Output[116]);
                    return;
                }

                Command.Connection = Connect;

                // Use parameterized query to avoid SQL injection
                Command.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                    tableName,
                    string.Join(", ", columns),
                    string.Join(", ", Enumerable.Range(0, values.Length).Select(i =>
                    {
                        if (columns[i] == "KullaniciAdi" || columns[i] == "Sifre" || columns[i] == "Mail")
                        {
                            return $"@param{i}";
                        }
                        else
                        {
                            return $"'{values[i]}'";
                        }
                    })));

                // Add parameters for encoded values
                for (int i = 0; i < values.Length; i++)
                {
                    if (columns[i] == "KullaniciAdi" || columns[i] == "Sifre" || columns[i] == "Mail")
                    {
                        Command.Parameters.AddWithValue($"@param{i}", Program.EncryptIt(values[i])); // Assuming Encode is your encoding method
                    }
                }

                Command.ExecuteNonQuery();
                MessageBox.Show(Program.Output[115]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void InsertEncoded(string tableName, string[] columns, string[] values, List<string> columnsToEncode)
        {
            try
            {
                if (columns.Length != values.Length)
                {
                    MessageBox.Show(Program.Output[116]);
                    return;
                }

                Command.Connection = Connect;

                // Use parameterized query to avoid SQL injection
                Command.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                    tableName,
                    string.Join(", ", columns),
                    string.Join(", ", Enumerable.Range(0, values.Length).Select(i =>
                    {
                        if (columnsToEncode.Contains(columns[i]))
                        {
                            return $"@param{i}";
                        }
                        else
                        {
                            return $"'{values[i]}'";
                        }
                    })));

                // Add parameters for encoded values
                for (int i = 0; i < values.Length; i++)
                {
                    if (columnsToEncode.Contains(columns[i]))
                    {
                        Command.Parameters.AddWithValue($"@param{i}", Program.EncryptIt(values[i])); // Assuming Encode is your encoding method
                    }
                }

                Command.ExecuteNonQuery();
                MessageBox.Show(Program.Output[115]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        static void InsertProductsIntoDatabase(List<Product> products)
        {
            string connectionString = "YourConnectionString"; // Replace with your actual connection string
            string insertQuery = "INSERT INTO YourTableName (Id, SatisFiyati, SatisMiktari, Tarih, AlisFiyati) VALUES (@Id, @SatisFiyati, @SatisMiktari, @Tarih, @AlisFiyati)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var product in products)
                {
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", product.Id);
                        command.Parameters.AddWithValue("@SatisFiyati", product.SatisFiyati);
                        command.Parameters.AddWithValue("@SatisMiktari", product.SatisMiktari);
                        command.Parameters.AddWithValue("@Tarih", product.Tarih);
                        command.Parameters.AddWithValue("@AlisFiyati", product.AlisFiyati);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public static List<(int Id, double SatisFiyati, double SatisMiktari, double AlisFiyati, string Tarih, double StokMiktari)> RetrieveProductsFromDatabase(string dtTableName)
        {
            products.Clear();
            string query = $"SELECT * FROM [{dtTableName}] ORDER BY Tarih";
            try
            {
                Connect.Open();

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            product.Id = Convert.ToInt32(reader["ID"]);
                            product.SatisFiyati = Convert.ToDouble(reader["SatışFiyatı"]);
                            product.SatisMiktari = Convert.ToDouble(reader["SatışMiktarı"]);
                            product.Tarih = Convert.ToDateTime(reader["Tarih"]).ToShortDateString();
                            product.AlisFiyati = Convert.ToDouble(reader["AlışFiyatı"]);
                            product.StokMiktari = Convert.ToDouble(reader["StokMiktari"]);

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                object columnValue = reader[i];
                                // You can process columnName and columnValue as needed
                                // For example, you might want to log or display them
                                //Console.WriteLine($"{columnName}: {columnValue}");
                            }
                            if (product != null)
                            {
                                products.Add((product.Id, product.SatisFiyati, product.SatisMiktari, product.AlisFiyati, product.Tarih, product.StokMiktari));
                            }
                            //MessageBox.Show($"Product: {Program.product.Id}");



                        }
                        //MessageBox.Show($"Products: {products}");
                        return products;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;

            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                    System.GC.Collect();
                }

            }
        }

        public static List<(int ID, string UrunAdi, double SatisFiyati, string UrunGrubu, string Tarih ,int StokMiktari)> GetAllProduct(string dtTableName)
        {
            productsD.Clear();
            string query = $"SELECT * FROM [{dtTableName}]";
            try
            {
                Connect.Open();

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productD.Id = Convert.ToInt32(reader["ID"]);
                            productD.SatisFiyati = Convert.ToDouble(reader["UrunSatisFiyati"]);
                            productD.UrunAdı = (reader["UrunAdi"]).ToString();
                            productD.Tarih = Convert.ToDateTime(reader["EklenmeVeyaGuncellenmeTarihi"]).ToShortDateString();
                            productD.UrunGrubu = (reader["UrunGrubu"]).ToString();
                            productD.StokMiktari = Convert.ToInt32(reader["StokMiktari"]);

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                object columnValue = reader[i];
                            }
                            if (productD != null)
                            {
                                productsD.Add((productD.Id, productD.UrunAdı, productD.SatisFiyati, productD.UrunGrubu, productD.Tarih, productD.StokMiktari));
                            }
                            //MessageBox.Show($"Product: {Program.product.Id}");



                        }
                        //MessageBox.Show($"Products: {products}");
                        return productsD;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;

            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                    System.GC.Collect();
                }

            }
        }

        public static List<(int id, string kullaniciAdi, string sifre, string mail, string girisZamani, bool root, bool defaultUser, bool employee, string Ad, string Soyadi)> GetAllUsers(string dtTableName)
        {
            users.Clear();
            string query = $"SELECT * FROM [{dtTableName}]";
            try
            {
                Connect.Open();

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.Id = Convert.ToInt32(reader["ID"]);
                            user.KullaniciAdi = (reader["KullaniciAdi"]).ToString();
                            user.Sifre = (reader["Sifre"]).ToString();
                            user.GirisZamani = Convert.ToDateTime(reader["GirisZamani"]).ToShortDateString();
                            user.Mail = (reader["Mail"]).ToString();
                            user.isRoot = Convert.ToBoolean(reader["Root"]);
                            user.isDefaultUser = Convert.ToBoolean(reader["defaultUser"]);
                            user.isEmployee = Convert.ToBoolean(reader["Employee"]);
                            user.Ad = (reader["Ad"]).ToString();
                            user.Soyadi = (reader["Soyad"]).ToString();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                object columnValue = reader[i];
                            }
                            if (users != null)
                            {
                                users.Add((user.Id, user.KullaniciAdi, user.Sifre, user.Mail, user.GirisZamani, user.isRoot, user.isDefaultUser, user.isEmployee, user.Ad, user.Soyadi));
                            }
                            //MessageBox.Show($"Product: {Program.product.Id}");



                        }
                        //MessageBox.Show($"Products: {products}");
                        return users;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;

            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                    System.GC.Collect();
                }

            }
        }


        public static List<(int ID, double UrunAdi, double SatisFiyati, double UrunGrubu, string Tarih, double StokMiktari)> Search(string dtTableName, int ID)
        {
            products.Clear();
            string query = ($"SELECT * FROM [{dtTableName}] WHERE ID = {ID}");
            try
            {
                Connect.Open();

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            product.Id = Convert.ToInt32(reader["ID"]);
                            product.SatisFiyati = Convert.ToInt32(reader["SatışFiyatı"]);
                            product.SatisMiktari = Convert.ToInt32(reader["SatışMiktarı"]);
                            product.Tarih = Convert.ToDateTime(reader["Tarih"]).ToShortDateString();
                            product.AlisFiyati = Convert.ToInt32(reader["AlışFiyatı"]);

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                object columnValue = reader[i];
                                // You can process columnName and columnValue as needed
                                // For example, you might want to log or display them
                                //Console.WriteLine($"{columnName}: {columnValue}");
                            }
                            if (product != null)
                            {
                                products.Add((product.Id, product.SatisFiyati, product.SatisMiktari, product.AlisFiyati, product.Tarih, product.StokMiktari));
                            }
                            //MessageBox.Show($"Product: {Program.product.Id}");



                        }
                        //MessageBox.Show($"Products: {products}");
                        return products;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;

            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                    System.GC.Collect();
                }

            }
        }
        public static List<(int Id, string UrunAdi, double SatisFiyati, string UrunGrubu, string Tarih, int StokMiktari)> GetProductWithGroup(string dtTableName, string Group)
        {
            productsD.Clear();
            string query = ($"SELECT * FROM [{dtTableName}] WHERE UrunGrubu = @Group");
            try
            {
                Connect.Open();

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@Group", Group);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productD.Id = Convert.ToInt32(reader["ID"]);
                            productD.UrunAdı = Convert.ToString(reader["UrunAdi"]);
                            productD.SatisFiyati = Convert.ToDouble(reader["UrunSatisFiyati"]);
                            productD.Tarih = Convert.ToDateTime(reader["EklenmeVeyaGuncellenmeTarihi"]).ToShortDateString();
                            productD.UrunGrubu = Convert.ToString(reader["UrunGrubu"]);
                            productD.StokMiktari = Convert.ToInt32(reader["StokMiktari"]);

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                object columnValue = reader[i];
                                // You can process columnName and columnValue as needed
                                // For example, you might want to log or display them
                                //Console.WriteLine($"{columnName}: {columnValue}");
                            }
                            if (productD != null)
                            {
                                productsD.Add((productD.Id, productD.UrunAdı, productD.SatisFiyati, productD.UrunGrubu, productD.Tarih, productD.StokMiktari));
                            }
                            //MessageBox.Show($"Product: {Program.product.Id}");



                        }
                        //MessageBox.Show($"Products: {products}");
                        return productsD;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;

            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                    System.GC.Collect();
                }

            }
        }
        public static List<(int ID, double UrunAdi, double SatisFiyati, double UrunGrubu, string Tarih, double StokMiktari)> Search(string dtTableName, DateTime Tarih1, DateTime Tarih2)
        {
            products.Clear();
            string query = ($"SELECT * FROM [{dtTableName}] WHERE Tarih BETWEEN @StartDate AND @EndDate");
            try
            {
                Connect.Open();

                using (OleDbCommand command = new OleDbCommand(query, Connect))
                {
                    command.Parameters.AddWithValue("@StartDate", Tarih1);
                    command.Parameters.AddWithValue("@EndDate", Tarih2);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            product.Id = Convert.ToInt32(reader["ID"]);
                            product.SatisFiyati = Convert.ToInt32(reader["SatışFiyatı"]);
                            product.SatisMiktari = Convert.ToInt32(reader["SatışMiktarı"]);
                            product.Tarih = (reader["Tarih"]).ToString();
                            product.AlisFiyati = Convert.ToInt32(reader["AlışFiyatı"]);

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                object columnValue = reader[i];
                                // You can process columnName and columnValue as needed
                                // For example, you might want to log or display them
                                //Console.WriteLine($"{columnName}: {columnValue}");
                            }
                            if (product != null)
                            {
                                products.Add((product.Id, product.SatisFiyati, product.SatisMiktari, product.AlisFiyati, product.Tarih, product.StokMiktari));
                            }
                            //MessageBox.Show($"Product: {Program.product.Id}");



                        }
                        //MessageBox.Show($"Products: {products}");
                        return products;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;

            }
            finally
            {
                if (Connect.State == ConnectionState.Open)
                {
                    Connect.Close();
                    System.GC.Collect();
                }

            }
        }
        #endregion

        #region Update,Delete Methods
        public static void Update(string tableName, string[] changes, string ID)
        {
            try
            {
                Command.Connection = Connect;
                Command.CommandText = String.Format("update {0} set {1} where ID={2}", tableName, string.Join(", ", changes), ID);
                Command.ExecuteNonQuery();
                MessageBox.Show(Program.Output[113]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async static void CommonUpdate(string tableName, string[] changes, string ID)
        {
            try
            {
                Command.Connection = Connect;
                Command.CommandText = $"UPDATE [{tableName}] SET {string.Join(", ", changes)} WHERE ID={ID}";
                Command.ExecuteNonQuery();
                await Task.Run(() =>
                {
                    MessageBox.Show(Program.Output[113]);
                });
            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    MessageBox.Show(ex.Message);
                });
            }
        }
        public async static void CommonUpdateSilent(string tableName, string[] changes, string ID)
        {
            try
            {
                Command.Connection = Connect;
                Command.CommandText = $"UPDATE [{tableName}] SET {string.Join(", ", changes)} WHERE ID={ID}";
                Command.ExecuteNonQuery();
                await Task.Run(() =>
                {
                    //MessageBox.Show(Program.Output[113]);
                });
            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    //MessageBox.Show(ex.Message);
                });
            }
        }
        public static void BasicUpdate(string tableName, string changes, string where)
        {
            try
            {
                Command.Connection = Connect;
                Command.CommandText = String.Format("update {0} set {1} where {2}", tableName, changes, where);
                Command.ExecuteNonQuery();
                MessageBox.Show(Program.Output[113]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ImageUpdate(string tableName, string changes, string ID)
        {
            try
            {
                Command.Connection = Connect;
                Command.CommandText = $"UPDATE {tableName} SET Image=['{changes}'] WHERE ID={ID}";
                Command.ExecuteNonQuery();
                MessageBox.Show(Program.Output[113]);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void UpdateStringValue(string tableName, string columnName, string newValue, string conditionColumn, string conditionValue)
        {
            try
            {
                string updateQuery = $"UPDATE {tableName} SET {columnName} = @NewValue WHERE {conditionColumn} = @ConditionValue";

                using (OleDbCommand command = new OleDbCommand(updateQuery, Connect))
                {
                    command.Parameters.AddWithValue("@NewValue", newValue);
                    command.Parameters.AddWithValue("@ConditionValue", conditionValue);

                    command.ExecuteNonQuery();
                }

                MessageBox.Show(Program.Output[117]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Program.Output[19]);
            }
        }




        public static void Delete(string tableName, string ID)
        {
            try
            {
                Command.Connection = Connect;
                Command.CommandText = String.Format("delete from [{0}] where ID={1}", tableName, ID);
                Command.ExecuteNonQuery();
                MessageBox.Show(Program.Output[118]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

    }
}