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

        public static SettingsWindow GetDailyHours(string User, SettingsWindow settingsWindow)
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
                    return settingsWindow;
                }

                settingsWindow.DailyHistory.Children.Clear();
                settingsWindow.DailyHistory.ColumnDefinitions.Clear();

                var cmd = new MySqlCommand($"SELECT * FROM DailyHours", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    int column = 0;
                    DateOnly coldate = DateOnly.Parse(DateTime.Now.AddDays(-30).ToString("dd.MM.yyyy"));

                    Label time = new Label();
                    time.IsHitTestVisible = false;
                    time.VerticalAlignment = VerticalAlignment.Center;
                    time.FontSize = 20;
                    double worktime = 0;
                    double breaktime = 0;

                    while (reader.Read())
                    {
                        if (DateOnly.Parse(reader.GetDateTime(5).ToShortDateString()) == coldate)
                        {
                            column--;
                        }
                        else
                        {
                            if (column != 0)
                            {
                                if ((worktime * 0.0625) < breaktime)
                                {
                                    time.Content = TimeSpan.FromHours(worktime - (breaktime - (worktime * 0.0625))).ToString(@"hh\:mm");
                                }
                                else
                                {
                                    time.Content = TimeSpan.FromHours(worktime).ToString(@"hh\:mm");
                                }
                                
                                Grid.SetColumn(time, column - 1);
                                settingsWindow.DailyHistory.Children.Add(time);

                                time = new Label();
                                time.IsHitTestVisible = false;
                                time.FontSize = 20;
                                time.VerticalAlignment = VerticalAlignment.Center;
                                time.Content = "";
                                worktime = 0;
                                breaktime = 0;
                            }
                        }

                        if (reader.GetInt16(1) == 1)
                        {
                            ReadWriteBar bar = new ReadWriteBar();
                            ColumnDefinition col = new ColumnDefinition();
                            Label date = new Label();
                            col.Width = new GridLength(80);
                            settingsWindow.DailyHistory.ColumnDefinitions.Add(col);
                            date.Content = reader.GetDateTime(5).ToString("dd. ") + Months[Int32.Parse(reader.GetDateTime(5).ToString("MM")) - 1];
                            date.IsHitTestVisible = true;

                            worktime = worktime + double.Parse(reader.GetDouble(4).ToString());
                            bar.MaxValue = 24;
                            bar.WorkMargin = reader.GetDouble(2);
                            bar.WorkHeight = reader.GetDouble(4);
                            bar.StampType = 1;
                            //bar.BreakMargin = reader.GetDouble(3);
                            //bar.BreakHeight = reader.GetDouble(5);

                            bar.Id = reader.GetInt16(0);

                            bar.ToolTip = $"WORK" +
                                $"\nStart: {TimeSpan.FromHours(reader.GetDouble(2)).ToString(@"hh\:mm")}" +
                                $"\nEnd: {TimeSpan.FromHours(reader.GetDouble(3)).ToString(@"hh\:mm")}";

                            Grid.SetColumn(bar, column);
                            Grid.SetColumn(date, column);
                            Grid.SetRow(date, 1);

                            settingsWindow.DailyHistory.Children.Add(bar);
                            settingsWindow.DailyHistory.Children.Add(date);

                            coldate = DateOnly.Parse(reader.GetDateTime(5).ToShortDateString());

                            column++;
                        }
                        else if (reader.GetInt16(1) == 2)
                        {
                            ReadWriteBar bar = new ReadWriteBar();
                            ColumnDefinition col = new ColumnDefinition();
                            Label date = new Label();
                            col.Width = new GridLength(80);
                            settingsWindow.DailyHistory.ColumnDefinitions.Add(col);
                            date.Content = reader.GetDateTime(5).ToString("dd. ") + Months[Int32.Parse(reader.GetDateTime(5).ToString("MM")) - 1];
                            date.IsHitTestVisible = true;

                            breaktime = breaktime + double.Parse(reader.GetDouble(4).ToString());
                            bar.MaxValue = 24;
                            bar.WorkMargin = reader.GetDouble(2);
                            bar.WorkHeight = reader.GetDouble(4);
                            bar.StampType = 2;

                            bar.Id = reader.GetInt16(0);

                            bar.ToolTip = $"BREAK" +
                                $"\nStart: {TimeSpan.FromHours(reader.GetDouble(2)).ToString(@"hh\:mm")}" +
                                $"\nEnd: {TimeSpan.FromHours(reader.GetDouble(3)).ToString(@"hh\:mm")}";

                            Grid.SetColumn(bar, column);
                            Grid.SetColumn(date, column);
                            Grid.SetRow(date, 1);

                            settingsWindow.DailyHistory.Children.Add(bar);
                            settingsWindow.DailyHistory.Children.Add(date);

                            coldate = DateOnly.Parse(reader.GetDateTime(5).ToShortDateString());

                            column++;
                        }
                    }
                    if ((worktime * 0.0625) < breaktime)
                    {
                        time.Content = TimeSpan.FromHours(worktime - (breaktime - (worktime * 0.0625))).ToString(@"hh\:mm");
                    }
                    else
                    {
                        time.Content = TimeSpan.FromHours(worktime).ToString(@"hh\:mm");
                    }

                    Grid.SetColumn(time, column - 1);
                    settingsWindow.DailyHistory.Children.Add(time);
                }

                conn.Close();

                return settingsWindow;
            }
            catch (Exception ex)
            {
                conn.Close();
                PublicEntitys.ShowError(1);
                MessageBox.Show(ex.Message);
                return settingsWindow;
            }
        }

        public static SettingsWindow GetMonthlyHours(string username, SettingsWindow settingsWindow)
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
                    return settingsWindow;
                }

                settingsWindow.MonthlyHistory.Children.Clear();
                settingsWindow.MonthlyHistory.ColumnDefinitions.Clear();

                var cmd = new MySqlCommand($"SELECT * FROM MonthlyHours order by Year, Month LIMIT 12", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    int column = 0;
                    int coldate = 0;

                    Label time = new Label();
                    time.VerticalAlignment = VerticalAlignment.Center;
                    time.FontSize = 20;
                    double worktime = 0;
                    double breaktime = 0;

                    while (reader.Read())
                    {
                        if (Int16.Parse(reader.GetInt16(3).ToString()) == coldate)
                        {
                            column--;
                        }
                        else
                        {
                            if (column != 0)
                            {
                                if ((worktime * 0.0625) < breaktime)
                                {
                                    time.Content = TimeSpan.FromHours(worktime - (breaktime - (worktime * 0.0625))).ToString(@"hh\:mm");
                                }
                                else
                                {
                                    time.Content = TimeSpan.FromHours(worktime).ToString(@"hh\:mm");
                                }

                                Grid.SetColumn(time, column - 1);
                                settingsWindow.MonthlyHistory.Children.Add(time);

                                time = new Label();
                                time.FontSize = 20;
                                time.VerticalAlignment = VerticalAlignment.Center;
                                time.Content = "";
                                worktime = 0;
                                breaktime = 0;
                            }
                        }

                        if (reader.GetInt16(1) == 1)
                        {
                            ReadOnlyBar bar = new ReadOnlyBar();
                            ColumnDefinition col = new ColumnDefinition();
                            Label date = new Label();
                            col.Width = new GridLength(80);
                            settingsWindow.MonthlyHistory.ColumnDefinitions.Add(col);
                            date.Content = Months[Int32.Parse(reader.GetInt16(3).ToString()) - 1];
                            date.IsHitTestVisible = true;

                            worktime = worktime + double.Parse(reader.GetDouble(2).ToString());
                            bar.MaxValue = 160;
                            bar.WorkMargin = 0;
                            bar.WorkHeight = reader.GetDouble(2);
                            bar.StampType = 1;

                            bar.Id = reader.GetInt16(0);

                            bar.ToolTip = $"Of that working hours:" +
                                $"\n{TimeSpan.FromHours(worktime).ToString(@"dd\.hh\:mm")}";

                            Grid.SetColumn(bar, column);
                            Grid.SetColumn(date, column);
                            Grid.SetRow(date, 1);

                            settingsWindow.MonthlyHistory.Children.Add(bar);
                            settingsWindow.MonthlyHistory.Children.Add(date);

                            coldate = Int16.Parse(reader.GetInt16(3).ToString());

                            column++;
                        }
                        else if (reader.GetInt16(1) == 2)
                        {
                            ReadOnlyBar bar = new ReadOnlyBar();
                            ColumnDefinition col = new ColumnDefinition();
                            Label date = new Label();
                            col.Width = new GridLength(80);
                            settingsWindow.MonthlyHistory.ColumnDefinitions.Add(col);
                            date.Content = Months[Int32.Parse(reader.GetInt16(3).ToString()) - 1];
                            date.IsHitTestVisible = true;

                            breaktime = breaktime + double.Parse(reader.GetDouble(2).ToString());
                            bar.MaxValue = 160;
                            bar.WorkMargin = 0;
                            bar.WorkHeight = reader.GetDouble(2);
                            bar.StampType = 2;

                            bar.Id = reader.GetInt16(0);

                            if ((worktime * 0.0625) < breaktime)
                            {
                                bar.ToolTip = $"Of that breaks:" +
                                $"\n{TimeSpan.FromHours(breaktime).ToString(@"hh\:mm")}" +
                                $"\n\nOf that overdo breaks:" +
                                $"\n{TimeSpan.FromHours((breaktime - (worktime * 0.0625))).ToString(@"hh\:mm")}";
                            }
                            else
                            {
                                bar.ToolTip = $"Of that breaks:" +
                                $"\n{TimeSpan.FromHours(breaktime).ToString(@"hh\:mm")}";
                            }

                            Grid.SetColumn(bar, column);
                            Grid.SetColumn(date, column);
                            Grid.SetRow(date, 1);

                            settingsWindow.MonthlyHistory.Children.Add(bar);
                            settingsWindow.MonthlyHistory.Children.Add(date);

                            coldate = Int16.Parse(reader.GetInt16(3).ToString());

                            column++;
                        }
                    }
                    if ((worktime * 0.0625) < breaktime)
                    {
                        time.Content = TimeSpan.FromHours(worktime - (breaktime - (worktime * 0.0625))).ToString(@"hh\:mm");
                    }
                    else
                    {
                        time.Content = TimeSpan.FromHours(worktime).ToString(@"hh\:mm");
                    }

                    Grid.SetColumn(time, column - 1);
                    settingsWindow.MonthlyHistory.Children.Add(time);
                }

                conn.Close();

                return settingsWindow;
            }
            catch
            {
                conn.Close();
                PublicEntitys.ShowError(1);
                return settingsWindow;
            }
        }
    }
}
