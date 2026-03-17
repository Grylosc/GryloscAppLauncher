using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using System.Text.Json;

namespace GryloscAppLauncher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private class LatestV
        {
            public string? tag_name {  get; set; }
        }
        private void InitList()
        {
            SoftListBox.Items.Clear();
            if (Program.softs.Count() > 0)
            {
                foreach (string s in Program.softs.Keys)
                {
                    SoftListBox.Items.Add(s);
                }
            }
            else
            {
                SoftListBox.Items.Add("Not installed");
            }
            Program.afterSofts = Program.softs;
            LaunchButton.Enabled = false;
            UninstallButton.Enabled = false;
            SearchBox.Enabled = true;
            if (Program.afterSofts.Count() > 0) SoftListBox.Enabled = true;
            else SoftListBox.Enabled = false;
            SoftTitle.Text = "Not Selected";
            SoftTitle.Enabled = false;
            IconBox.ImageLocation = null;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

            Program.localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            Program.appFolder = Path.Combine(Program.localAppDataPath, "Grylosc");
            if (!Directory.Exists(Program.appFolder))
            {
                Directory.CreateDirectory(Program.appFolder);
                Directory.CreateDirectory($"{Program.appFolder}/rawd");
                Directory.CreateDirectory($"{Program.appFolder}/softs");
                File.WriteAllText(Path.Combine(Program.appFolder, "data.json"), "{\"Installed\":\"{}\"}");
            }
            else
            {
                Program.data = JsonSerializer.Deserialize<Dictionary<string, string>>(
                    File.ReadAllText(Path.Combine(Program.appFolder, "data.json"))
                    ) ?? new Dictionary<string, string>();
                Program.softs = JsonSerializer.Deserialize<Dictionary<string, string>>(Program.data["Installed"])
                    ?? new Dictionary<string, string>();
            }
            InitList();
        }

        private void AddSoftwareButton_Click(object sender, EventArgs e)
        {
            InstallerForm infor = new InstallerForm();
            infor.ShowDialog();
        }

        private async void SoftListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SoftListBox.SelectedIndex != -1)
            {
                string username = "grylosc";
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"https://raw.githubusercontent.com/{username}/{Program.afterSofts.Keys.ToList()[SoftListBox.SelectedIndex]}/refs/heads/master/icon.png");
                    if (response.IsSuccessStatusCode)
                    {
                        IconBox.ImageLocation = $"https://raw.githubusercontent.com/{username}/{Program.afterSofts.Keys.ToList()[SoftListBox.SelectedIndex]}/refs/heads/master/icon.png";
                    }
                    else
                    {
                        IconBox.ImageLocation = $"https://raw.githubusercontent.com/{username}/{Program.afterSofts.Keys.ToList()[SoftListBox.SelectedIndex]}/refs/heads/main/icon.png";
                    }
                }

                SoftTitle.Text = Program.afterSofts.Keys.ToList()[SoftListBox.SelectedIndex];
                SoftTitle.Enabled = true;
                LaunchButton.Enabled = true;
                UninstallButton.Enabled = true;

                // 最新バージョンか否か確認
                var handler = new HttpClientHandler();
                handler.AllowAutoRedirect = false;
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "GryloscLauncher");
                    var request = new HttpRequestMessage
                    (
                        HttpMethod.Get,
                        $"https://github.com/{username}/{Program.afterSofts.Keys.ToList()[SoftListBox.SelectedIndex]}/releases/latest"
                    );
                    var responce = await client.SendAsync(
                        request,
                        HttpCompletionOption.ResponseHeadersRead
                        );
                    string location = responce.Headers.Location.ToString();
                    string version = location.Split('/').Last();
                    if (version != Program.afterSofts[Program.afterSofts.Keys.ToList()[SoftListBox.SelectedIndex]])    // バージョンに相違があれば更新ボタンを表示する
                    {
                        UpdateButton.Enabled = true;
                        UpdateButton.Visible = true;
                    }
                    else
                    {
                        UpdateButton.Enabled = false;
                        UpdateButton.Visible = false;
                    }
                    VersionText.Text = $"Local: {Program.afterSofts[Program.afterSofts.Keys.ToList()[SoftListBox.SelectedIndex]]}\nLatest: {version}";
                }
            }
        }

        private void SoftTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string username = "grylosc";
            Program.OpenUrl($"https://github.com/{username}/{SoftTitle.Text}");
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            SoftListBox.Items.Clear();
            SoftTitle.Enabled = false;
            LaunchButton.Enabled = false;
            UninstallButton.Enabled = false;
            foreach (string item in Program.softs.Keys)
            {
                if (item.Contains(SearchBox.Text))
                {
                    Program.afterSofts.Add(item, Program.softs[item]);
                    SoftListBox.Items.Add(item);
                }
            }
            if (Program.afterSofts.Count > 0)
            {
                SoftListBox.Items.Add("Software Not Found");
            }
            else
                if (string.IsNullOrEmpty(SearchBox.Text))
                {
                    SoftListBox.Items.Clear();
                    Program.afterSofts = Program.softs;
                    InitList();
                }
        }

        private void LaunchButton_Click(object sender, EventArgs e)
        {
            if (SoftListBox.SelectedIndex == -1) return;
            string path = $"{Program.appFolder}/softs/{Program.softs.Keys.ToList()[SoftListBox.SelectedIndex]}/{(Program.softs.Keys.ToList()[SoftListBox.SelectedIndex] == "GCubeGameBased" ? "GCubeGame" : Program.softs.Keys.ToList()[SoftListBox.SelectedIndex])}.exe";
            var psi = new ProcessStartInfo
            {
                FileName = path,
                WorkingDirectory = Path.GetDirectoryName(path)
            };
            Process.Start(psi);
        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"{Program.softs.Keys.ToList()[SoftListBox.SelectedIndex]} を本当にアンインストールしますか？", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Directory.Delete($"{Program.appFolder}/softs/{Program.softs.Keys.ToList()[SoftListBox.SelectedIndex]}", true);
                string appname = Program.softs.Keys.ToList()[SoftListBox.SelectedIndex];
                Program.softs.Remove(Program.softs.Keys.ToList()[SoftListBox.SelectedIndex]);
                Program.data["Installed"] = JsonSerializer.Serialize(Program.softs);
                Program.SaveJson();
                MessageBox.Show($"{appname} のアンインストールが正常に完了しました。", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InitList();
            }
        }

        private void OpenDireButton_Click(object sender, EventArgs e)
        {
            if (SoftListBox.SelectedIndex == -1)
            {
                Process.Start("explorer.exe", Program.appFolder);
            }
            else
            {
                Process.Start("explorer.exe", $"{Program.appFolder.Replace("/", "\\")}\\softs\\{Program.softs.Keys.ToList()[SoftListBox.SelectedIndex]}");
            }
        }

        private void ResetTSMI_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"このソフトがおかしくなったり、完全アンインストールしたい際にこのオプションを選択してください。\nこのオプションはこのソフト用のAppDataを削除するものです。\n本当に削除しますか？", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes && MessageBox.Show("本当に？", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Directory.Delete(Program.appFolder, true);
                MessageBox.Show("AppDataを削除し、内部データをリセットしました。\nこれよりアプリケーションを終了します。", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }

        private async void UpdateButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"{Program.afterSofts[Program.afterSofts.Keys.ToList()[SoftListBox.SelectedIndex]]} を最新バージョンにアップデートしますか？", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                string softname = Program.afterSofts[Program.afterSofts.Keys.ToList()[SoftListBox.SelectedIndex]];
                Program.isInstalling = true;
                var handler = new HttpClientHandler();
                handler.AllowAutoRedirect = false;
                using var client = new HttpClient(handler);
                client.DefaultRequestHeaders.Add("User-Agent", "GryloscLauncher");
                string username = "Grylosc";
                StatusText.Text = "バージョン確認中...";
                var request = new HttpRequestMessage
                    (
                        HttpMethod.Get,
                        $"https://github.com/{username}/{softname}/releases/latest"
                    );
                var responce = await client.SendAsync(
                    request,
                    HttpCompletionOption.ResponseHeadersRead
                    );
                string location = responce.Headers.Location.ToString();
                string version = location.Split('/').Last();
                StatusText.Text = "ダウンロード中...";
                string url = $"https://github.com/{username}/{softname}/releases/latest/download/{softname}-UnuseRuntime.zip";
                string downloadPath = $"{Program.appFolder}/rawd/download.zip";
                Debug.WriteLine($"{softname}をダウンロード中...\n   username: {username}\n  url: {url}\n    downloadPath: {downloadPath}");
                await File.WriteAllBytesAsync(
                    downloadPath,
                    await client.GetByteArrayAsync(url)
                    );
                StatusText.Text = "ファイル解凍中...";
                string softpath = $"{Program.appFolder}/softs/{softname}";
                ZipFile.ExtractToDirectory(downloadPath, softpath, true);
                StatusText.Text = "キャッシュ削除中...";
                File.Delete(downloadPath);
                StatusText.Text = "不要データ削除中...";
                // Languageファイルを削除
                if (File.Exists($"{Program.appFolder}/softs/{softname}/LanguageC{Program.softs[Program.afterSofts[Program.afterSofts.Keys.ToList()[SoftListBox.SelectedIndex]]].Replace("v", "").Replace(".", "")}.json"))
                {
                    File.Delete($"{Program.appFolder}/softs/{softname}/LanguageC{Program.softs[Program.afterSofts[Program.afterSofts.Keys.ToList()[SoftListBox.SelectedIndex]]].Replace("v", "").Replace(".", "")}.json");
                }
                StatusText.Text = "データ整備中...";
                Program.softs[softname] = version ?? "v1.0.0";
                Program.data["Installed"] = JsonSerializer.Serialize(Program.softs);
                Program.SaveJson();
                MessageBox.Show($"{softname} のアップデートが正常に完了しました。", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StatusText.Text = "";
                AddSoftwareButton.Text = "インストール済み";
                AddSoftwareButton.Enabled = false;
                Program.isInstalling = false;
                Application.Restart();
            }
        }
    }
}
