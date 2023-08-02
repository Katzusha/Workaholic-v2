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
using Workaholic;

namespace StartStopWork
{
    internal class Database
    {
        private static MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection("server=152.89.234.190; uid=kerieri_WorkAholic ;pwd=WorkAholic123 ; database=kerieri_WorkAholic");
        private static string[] Months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Okt", "Nov", "Dec" };

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

        public static bool PostStamp(int StampType, string User, bool FirstStamp, string ModifiedBy)
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
                        cmd = new MySqlCommand($"INSERT INTO PrimaryStamps (Time, User_Id, ModifiedBy) VALUES('{DateTime.Now.ToString("HH:mm:ss")}', '{PublicEntitys.Encryption(User)}', '{PublicEntitys.Encryption(ModifiedBy)}');", conn);
                        cmd.ExecuteNonQuery();
                        break;
                    case 2:
                        cmd = new MySqlCommand($"INSERT INTO SecondaryStamps (StampType_Id, EndWork, User_Id, FirstStamp_Id, ModifiedBy) " +
                        $"VALUES('{StampType}', '{DateTime.Now.ToString("HH:mm:ss")}', '{PublicEntitys.Encryption(User)}', (SELECT Max(Id) FROM PrimaryStamps WHERE User_id = '{PublicEntitys.Encryption(User)}'), '{PublicEntitys.Encryption(ModifiedBy)}');", conn);
                        cmd.ExecuteNonQuery();
                        break;
                    case 3:
                        cmd = new MySqlCommand($"INSERT INTO SecondaryStamps (StampType_Id, StartBreak, User_Id, FirstStamp_Id, ModifiedBy) " +
                        $"VALUES('{StampType}', '{DateTime.Now.ToString("HH:mm:ss")}', '{PublicEntitys.Encryption(User)}', (SELECT Max(Id) FROM PrimaryStamps WHERE User_id = '{PublicEntitys.Encryption(User)}'), '{PublicEntitys.Encryption(ModifiedBy)}');", conn);
                        cmd.ExecuteNonQuery();
                        break;
                    case 4:
                        cmd = new MySqlCommand($"INSERT INTO SecondaryStamps (StampType_Id, EndBreak, User_Id, FirstStamp_Id, ModifiedBy) " +
                        $"VALUES('{StampType}', '{DateTime.Now.ToString("HH:mm:ss")}', '{PublicEntitys.Encryption(User)}', (SELECT Max(Id) FROM PrimaryStamps WHERE User_id = '{PublicEntitys.Encryption(User)}'), '{PublicEntitys.Encryption(ModifiedBy)}');", conn);
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

                var cmd = new MySqlCommand($"SELECT * FROM DailyHours LIMIT 30", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    int column = 0;
                    DateOnly coldate = DateOnly.Parse(DateTime.Now.AddDays(-30).ToString("dd.MM.yyyy"));

                    Label time = new Label();
                    time.VerticalAlignment = VerticalAlignment.Center;
                    time.FontSize = 20;
                    double worktime = 0;
                    bool isFirst = true;

                    while (reader.Read())
                    {
                        if (DateOnly.Parse(reader.GetDateTime(7).ToShortDateString()) == coldate)
                        {
                            column--;
                            worktime = worktime + (double.Parse(reader.GetDouble(2).ToString()) - double.Parse(reader.GetDouble(5).ToString()));
                        }
                        else
                        {
                            if (column != 0)
                            {
                                Grid.SetColumn(time, column - 1);
                                settingsWindow.DailyHistory.Children.Add(time);

                                time = new Label();
                                time.FontSize = 20;
                                time.VerticalAlignment = VerticalAlignment.Center;
                                time.Content = "";
                                worktime = 0;
                                isFirst = true;
                            }
                        }

                        ReadWriteBar bar = new ReadWriteBar();
                        ColumnDefinition col = new ColumnDefinition();
                        Label date = new Label();
                        col.Width = new GridLength(80);
                        settingsWindow.DailyHistory.ColumnDefinitions.Add(col);
                        date.Content = reader.GetDateTime(7).ToString("dd. ") + Months[Int32.Parse(reader.GetDateTime(7).ToString("MM")) - 1];
                        date.IsHitTestVisible = true;

                        if (isFirst)
                        {
                            isFirst = false;
                            worktime = worktime + (double.Parse(reader.GetDouble(2).ToString()) - double.Parse(reader.GetDouble(5).ToString()));
                        }

                        time.Content = TimeSpan.FromHours(worktime).ToString(@"hh\:mm");
                        bar.MaxValue = 24;
                        bar.WorkMargin = reader.GetDouble(0);
                        bar.WorkHeight = reader.GetDouble(2);
                        bar.BreakMargin = reader.GetDouble(3);
                        bar.BreakHeight = reader.GetDouble(5);

                        if (reader.GetDouble(5) != 0)
                        {
                            bar.ToolTip = $"Start: {TimeSpan.FromHours(reader.GetDouble(0)).ToString(@"hh\:mm")}" +
                            $"\nBreak: {TimeSpan.FromHours(reader.GetDouble(3)).ToString(@"hh\:mm")} - {TimeSpan.FromHours(reader.GetDouble(4)).ToString(@"hh\:mm")}" +
                            $"\nEnd: {TimeSpan.FromHours(reader.GetDouble(1)).ToString(@"hh\:mm")}";
                        }
                        else
                        {
                            bar.ToolTip = $"Start: {TimeSpan.FromHours(reader.GetDouble(0)).ToString(@"hh\:mm")}" +
                            $"\nEnd: {TimeSpan.FromHours(reader.GetDouble(1)).ToString(@"hh\:mm")}";
                        }

                        Grid.SetColumn(bar, column);
                        Grid.SetColumn(date, column);
                        Grid.SetRow(date, 1);

                        settingsWindow.DailyHistory.Children.Add(bar);
                        settingsWindow.DailyHistory.Children.Add(date);

                        coldate = DateOnly.Parse(reader.GetDateTime(7).ToShortDateString());

                        column++;
                    }


                    Grid.SetColumn(time, column - 1);
                    settingsWindow.DailyHistory.Children.Add(time);
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
                    while (reader.Read())
                    {
                        try
                        {
                            ColumnDefinition col = new ColumnDefinition();
                            col.Width = new GridLength(80);
                            settingsWindow.DailyHistory.ColumnDefinitions.Add(col);

                            ReadOnlyBar bar = new ReadOnlyBar();
                            bar.ThisValue = TimeSpan.FromHours(reader.GetDouble(0)).ToString(@"hh\:mm");
                            bar.MaxValue = 160;
                            bar.WorkMargin = 0;
                            bar.WorkHeight = reader.GetDouble(0);
                            bar.BreakMargin = 0;
                            bar.BreakHeight = reader.GetDouble(1);

                            bar.ToolTip = $"Monthly hours made: {TimeSpan.FromHours(reader.GetDouble(0)).ToString(@"hh\:mm")}" +
                                $"\nOf that breaks: {TimeSpan.FromHours(reader.GetDouble(1)).ToString(@"hh\:mm")}";

                            double breakoverdue = reader.GetDouble(0) * 0.0625;

                            if (reader.GetDouble(1) > breakoverdue)
                            {
                                bar.ToolTip = $"Monthly hours made: {TimeSpan.FromHours(reader.GetDouble(0)).ToString(@"hh\:mm")}" +
                                $"\nOf that breaks: {TimeSpan.FromHours(reader.GetDouble(1)).ToString(@"hh\:mm")} " +
                                $"\nOf that overdue breaks {TimeSpan.FromHours(reader.GetDouble(1) - breakoverdue).ToString(@"hh\:mm")}";

                                bar.ThisValue = TimeSpan.FromHours(reader.GetDouble(0) - (reader.GetDouble(1) - breakoverdue)).ToString(@"hh\:mm");
                            }
                            else
                            {
                                bar.ToolTip = $"Monthly hours made: {TimeSpan.FromHours(reader.GetDouble(0)).ToString(@"hh\:mm")}" +
                                $"\nOf that breaks: {TimeSpan.FromHours(reader.GetDouble(1)).ToString(@"hh\:mm")}";
                            }

                            Grid.SetColumn(bar, column);

                            Label date = new Label();
                            date.Content = Months[Int32.Parse(reader.GetInt32(3).ToString()) - 1];
                            Grid.SetColumn(date, column);
                            Grid.SetRow(date, 1);

                            settingsWindow.MonthlyHistory.Children.Add(bar);
                            settingsWindow.MonthlyHistory.Children.Add(date);

                            column++;
                        }
                        catch { }
                    }
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
