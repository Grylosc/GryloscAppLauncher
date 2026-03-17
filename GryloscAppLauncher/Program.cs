using System.Text.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace GryloscAppLauncher
{
    internal static class Program
    {
        public static Dictionary<string, string> data = new Dictionary<string, string>();   // json生データ
        public static Dictionary<string, string> softs = new Dictionary<string, string>();  // インストール済みソフト
        public static Dictionary<string, string> afterSofts = new Dictionary<string, string>(); // 検索後のソフト一覧
        public static string localAppDataPath = ""; // appdataフォルダの位置
        public static string appFolder = "";    // appdataフォルダのgryloscフォルダーの位置
        public static bool isInstalling = false;    // ソフトをインストール中か

        public static void SaveJson()
        {
            File.WriteAllText($"{appFolder}/data.json", JsonSerializer.Serialize(data));
        }
        public static void OpenUrl(string url)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}