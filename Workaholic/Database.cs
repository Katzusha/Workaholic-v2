using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Workaholic;
using Workaholic.Models;

namespace StartStopWork
{
    internal class Database
    {
        private static MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection("server=152.89.234.190; uid=kerieri_WorkAholic ;pwd=WorkAholic123 ; database=kerieri_WorkAholic");
        private static string[] Months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Okt", "Nov", "Dec" };
        public static int WorkStampId = 0;
        public static int BreakStampId = 0;

        public static bool TryConnection()
        {            
            try
            {
                conn.Open();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }

        public static bool LogIn(string username, string password)
        {
            try
            {
                try
                {
                    conn.Open();
                }
                catch
                {
                    conn.Close();
                    PublicEntitys.ShowError(1);
                    return false;
                }

                var cmd = new MySqlCommand($"SELECT * FROM Users WHERE Username = '{PublicEntitys.Encryption(username)}' AND Password = '{PublicEntitys.Encryption(password)}'", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        configuration.AppSettings.Settings["Firstname"].Value = reader.GetString(1);
                        configuration.AppSettings.Settings["Surname"].Value = reader.GetString(2);
                        configuration.AppSettings.Settings["Username"].Value = username;
                        configuration.AppSettings.Settings["Password"].Value = password;
                        configuration.Save(ConfigurationSaveMode.Full, true);
                        ConfigurationManager.RefreshSection("appSettings");

                    }
                }

                conn.Close();
                return true;
            }
            catch
            {
                conn.Close();
                PublicEntitys.ShowError(10);
                return false;
            }
        }

        public static bool PostStamp(int StampType, string User, string ModifiedBy)
        {
            try
            {
                try
                {
                    conn.Open();
                }
                catch
                {
                    conn.Close();
                    PublicEntitys.ShowError(1);
                    return false;
                }

                var cmd = new MySqlCommand();

                switch (StampType)
                {
                    case 1:
                        cmd = new MySqlCommand($"INSERT INTO Stamps (StampType, Start, Username, ModifiedBy) VALUES(1, '{DateTime.Now.ToString("HH:mm:ss")}', '{PublicEntitys.Encryption(User)}', '{PublicEntitys.Encryption(ModifiedBy)}');", conn);
                        cmd.ExecuteNonQuery();
                        cmd = new MySqlCommand($"SELECT Max(Id) FROM Stamps WHERE Username = '{PublicEntitys.Encryption(User)}';", conn);
                        WorkStampId = (Int32)cmd.ExecuteScalar();
                        break;
                    case 2:
                        cmd = new MySqlCommand($"INSERT INTO Stamps (StampType, Start, WorkStampId, Username, ModifiedBy) " +
                        $"VALUES(2, '{DateTime.Now.ToString("HH:mm:ss")}', {WorkStampId.ToString()}, '{PublicEntitys.Encryption(User)}', '{PublicEntitys.Encryption(ModifiedBy)}');", conn);
                        cmd.ExecuteNonQuery();
                        cmd = new MySqlCommand($"SELECT Max(Id) FROM Stamps WHERE Username = '{PublicEntitys.Encryption(User)}';", conn);
                        BreakStampId = (Int32)cmd.ExecuteScalar();
                        cmd.ExecuteNonQuery();
                        break;
                    case 3:
                        cmd = new MySqlCommand($"UPDATE Stamps SET End = '{DateTime.Now.ToString("HH:mm:ss")}' WHERE Id = {BreakStampId}",conn);
                        cmd.ExecuteNonQuery();
                        break;
                    case 4:
                        cmd = new MySqlCommand($"UPDATE Stamps SET End = '{DateTime.Now.ToString("HH:mm:ss")}' WHERE Id = {WorkStampId}", conn);
                        cmd.ExecuteNonQuery();
                        break;
                }              

                conn.Close();
                return true;
            }
            catch
            {
                conn.Close();
                PublicEntitys.ShowError(1);
                return false;
            }
        }

        public static List<DailyHours> GetDailyHours(string Username)
        {
            try
            {
                conn.Open();
                List<DailyHours> _dailyHours = conn.Query<DailyHours>($"SELECT * FROM DailyHours WHERE Username = '{PublicEntitys.Encryption(Username)}' LIMIT 30").ToList();
                conn.Close();
                return _dailyHours;
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
                PublicEntitys.ShowError(1);
                return null;
            }
            
        }

        public static List<MonthlyHours> GetMonthlyHours(string Username)
        {
            try
            {
                conn.Open();
                List<MonthlyHours> _monthlyHours = conn.Query<MonthlyHours>($"SELECT * FROM MonthlyHours WHERE Username = '{PublicEntitys.Encryption(Username)}' ORDER BY Year, Month LIMIT 12").ToList();
                conn.Close();
                return _monthlyHours;
            }
            catch (Exception ex)
            {
                conn.Close();
                PublicEntitys.ShowError(1);
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static List<DailyHours> GetDailyHoursDetail(string Username, int Id)
        {
            try
            {
                conn.Open();
                List<DailyHours> _monthlyHours = conn.Query<DailyHours>($"SELECT * FROM DailyHours WHERE Username = '{PublicEntitys.Encryption(Username)}' AND (Id = {Id} OR WorkStampId = {Id}) ORDER BY Date").ToList();
                conn.Close();
                return _monthlyHours;
            }
            catch (Exception ex)
            {
                conn.Close();
                PublicEntitys.ShowError(1);
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
