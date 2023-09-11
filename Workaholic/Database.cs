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
                    PublicEntitys.ShowError(400);
                    return false;
                }

                var cmd = new MySqlCommand($"SELECT * FROM Users WHERE Username = '{PublicEntitys.Encryption(username)}' AND Password = '{PublicEntitys.Encryption(password)}'", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PublicEntitys.configuration.AppSettings.Settings["Firstname"].Value = reader.GetString(1);
                        PublicEntitys.configuration.AppSettings.Settings["Surname"].Value = reader.GetString(2);
                        PublicEntitys.configuration.AppSettings.Settings["Username"].Value = username;
                        PublicEntitys.configuration.AppSettings.Settings["Password"].Value = password;
                        PublicEntitys.configuration.AppSettings.Settings["AuthLevel"].Value = reader.GetString(5);
                        PublicEntitys.configuration.Save(ConfigurationSaveMode.Full, true);
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

        public static bool PostStamp(int StampType, string? Start, string? End, int? PostWorkStampId, string User)
        {
            try
            {
                conn.Open();
                try
                {
                    var cmd = new MySqlCommand();

                    if (Start == null || End == null)
                    {
                        switch (StampType)
                        {
                            case 1:
                                cmd = new MySqlCommand($"INSERT INTO Stamps (StampType, Start, Username, ModifiedBy) VALUES(1, '{DateTime.Now.ToString("HH:mm:ss")}', '{PublicEntitys.Encryption(User)}', '{PublicEntitys.Encryption(PublicEntitys.configuration.AppSettings.Settings["Username"].Value)}');", conn);
                                cmd.ExecuteNonQuery();
                                cmd = new MySqlCommand($"SELECT Max(Id) FROM Stamps WHERE Username = '{PublicEntitys.Encryption(User)}';", conn);
                                WorkStampId = (Int32)cmd.ExecuteScalar();
                                break;
                            case 2:
                                cmd = new MySqlCommand($"INSERT INTO Stamps (StampType, Start, WorkStampId, Username, ModifiedBy) " +
                                    $"VALUES(2, '{DateTime.Now.ToString("HH:mm:ss")}', {WorkStampId.ToString()}, '{PublicEntitys.Encryption(User)}', '{PublicEntitys.Encryption(PublicEntitys.configuration.AppSettings.Settings["Username"].Value)}');", conn);
                                cmd.ExecuteNonQuery();
                                cmd = new MySqlCommand($"SELECT Max(Id) FROM Stamps WHERE Username = '{PublicEntitys.Encryption(User)}';", conn);
                                BreakStampId = (Int32)cmd.ExecuteScalar();
                                cmd.ExecuteNonQuery();
                                break;
                            case 3:
                                cmd = new MySqlCommand($"UPDATE Stamps SET End = '{DateTime.Now.ToString("HH:mm:ss")}' WHERE Id = {BreakStampId}", conn);
                                cmd.ExecuteNonQuery();
                                break;
                            case 4:
                                cmd = new MySqlCommand($"UPDATE Stamps SET End = '{DateTime.Now.ToString("HH:mm:ss")}' WHERE Id = {WorkStampId}", conn);
                                cmd.ExecuteNonQuery();
                                break;
                        }
                    }
                    else
                    {
                        switch (StampType)
                        {
                            case 2:
                                cmd = new MySqlCommand($"SELECT CreatedDate FROM Stamps WHERE Id = '{PostWorkStampId.ToString()}';", conn);
                                DateTime createddate = (DateTime)cmd.ExecuteScalar();
                                cmd = new MySqlCommand($"INSERT INTO Stamps (StampType, Start, WorkStampId, Username, ModifiedBy, CreatedDate) " +
                                    $"VALUES(2, '{Start.ToString()}', {PostWorkStampId.ToString()}, '{PublicEntitys.Encryption(User)}', '{PublicEntitys.Encryption(PublicEntitys.configuration.AppSettings.Settings["Username"].Value)}', '{createddate.ToString("yyyy-MM-dd HH:mm:ss")}');", conn);
                                cmd.ExecuteNonQuery();
                                cmd = new MySqlCommand($"SELECT Id FROM Stamps WHERE Username = '{PublicEntitys.Encryption(User)}' AND WorkStampId = '{PostWorkStampId.ToString()}' AND Start = '{Start.ToString()}';", conn);
                                BreakStampId = (Int32)cmd.ExecuteScalar();
                                break;
                            case 3:
                                cmd = new MySqlCommand($"UPDATE Stamps SET End = '{End.ToString()}' WHERE Id = {BreakStampId}", conn);
                                cmd.ExecuteNonQuery();
                                break;
                        }
                    }

                    conn.Close();
                    return true;
                }
                catch
                {
                    conn.Close();
                    PublicEntitys.ShowError(409);
                    return false;
                }
            }
            catch
            {
                conn.Close();
                PublicEntitys.ShowError(400);
                return false;
            }
        }

        #region DAILY HOURS
        public static List<DailyHours> GetDailyHours(string Username, int Year, int Month)
        {
            try
            {
                conn.Open();
                try
                {
                    List<DailyHours> _dailyHours = conn.Query<DailyHours>($"SELECT * FROM DailyHours WHERE Username = '{PublicEntitys.Encryption(Username)}' and YEAR(Date) = {Year.ToString()} and MONTH(Date) = {Month.ToString()}").ToList(); 
                    conn.Close();
                    return _dailyHours;
                }
                catch
                {
                    PublicEntitys.ShowError(500);
                    conn.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
                PublicEntitys.ShowError(400);
                return null;
            }

        }

        public static List<DailyHours> GetDailyHoursDetail(string Username, int Id)
        {
            try
            {
                conn.Open();
                try
                {
                    List<DailyHours> _monthlyHours = conn.Query<DailyHours>($"SELECT * FROM DailyHours WHERE Username = '{PublicEntitys.Encryption(Username)}' AND (Id = {Id} OR WorkStampId = {Id}) ORDER BY Date").ToList();
                    conn.Close();
                    return _monthlyHours;
                }
                catch
                {
                    PublicEntitys.ShowError(500);
                    conn.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                PublicEntitys.ShowError(400);
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static bool UpdateDailyHoursDetail(string username, List<DailyHours> dailyHours)
        {
            try
            {
                try
                {
                    foreach (DailyHours _dailyHours in dailyHours)
                    {
                        if (_dailyHours.Id == 0)
                        {
                            if (_dailyHours.Start != _dailyHours.End)
                            {
                                PostStamp(2, TimeSpan.FromHours(_dailyHours.Start).ToString(@"hh\:mm"), TimeSpan.FromHours(_dailyHours.End).ToString(@"hh\:mm"), Int32.Parse(_dailyHours.Username), username);

                                PostStamp(3, TimeSpan.FromHours(_dailyHours.Start).ToString(@"hh\:mm"), TimeSpan.FromHours(_dailyHours.End).ToString(@"hh\:mm"), Int32.Parse(_dailyHours.Username), username);
                            }
                        }
                        else
                        {
                            if (_dailyHours.Start != _dailyHours.End)
                            {
                                conn.Open();
                                conn.Execute($"UPDATE Stamps SET Start = '{TimeSpan.FromHours(_dailyHours.Start).ToString()}', End = '{TimeSpan.FromHours(_dailyHours.End).ToString()}', ModifiedBy = '{PublicEntitys.Encryption(PublicEntitys.configuration.AppSettings.Settings["Username"].Value)}' WHERE Id = {_dailyHours.Id.ToString()}");
                                conn.Close();
                            }
                            else
                            {
                                conn.Open();
                                conn.Execute($"DELETE FROM Stamps WHERE Id = {_dailyHours.Id.ToString()}");
                                conn.Close();
                            }
                        }
                    }
                    return true;
                }
                catch
                {
                    PublicEntitys.ShowError(409);
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                PublicEntitys.ShowError(400);
                return false;
            }
        }
        #endregion

        #region
        public static List<MonthlyHours> GetMonthlyHours(string Username)
        {
            try
            {
                conn.Open();
                try
                {
                    List<MonthlyHours> _monthlyHours = conn.Query<MonthlyHours>($"SELECT * FROM MonthlyHours WHERE Username = '{PublicEntitys.Encryption(Username)}'").ToList();
                    conn.Close();
                    return _monthlyHours;
                }
                catch
                {
                    PublicEntitys.ShowError(500);
                    conn.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                PublicEntitys.ShowError(1);
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        #endregion

        #region
        public static List<DaysOff> GetDaysOff(string Username)
        {
            try
            {
                conn.Open();
                try
                {
                    List<DaysOff> _monthlyHours = conn.Query<DaysOff>($"SELECT * FROM DaysOff WHERE Username = '{PublicEntitys.Encryption(Username)}' AND Start < '{DateOnly.Parse(DateTime.Now.AddDays(7).ToString("yyyy-MM-dd")).ToString("yyyy-MM-dd")}'").ToList();
                    conn.Close();
                    return _monthlyHours;
                }
                catch
                {
                    PublicEntitys.ShowError(500);
                    conn.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                PublicEntitys.ShowError(1);
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        #endregion
    }
}
