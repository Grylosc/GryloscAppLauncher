using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO.Compression;
using System.Diagnostics;

namespace GryloscAppLauncher
{
    public partial class InstallerForm : Form
    {

        private List<string> repos = new List<string>();
        private bool isUpdate = false;

        public InstallerForm()
        {
            InitializeComponent();
        }

        public class Repo
        {
            public string? name { get; set; }
        }
        private class LatestV
        {
            public string? tag_name { get; set; }
        }

        private async void InstallerForm_Shown(object sender, EventArgs e)
        {
            // Loading表示
            RepoListBox.Items.Clear();
            RepoListBox.Items.Add("Loading...");

            // GithubのGryloscのリポジトリ一覧を取得
            var username = "grylosc";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "GryloscLauncher");

            var url = $"https://api.github.com/users/{username}/repos";

            var response = await client.GetStringAsync(url);


            repos.Clear();
            List<Repo> rawrepos = JsonSerializer.Deserialize<List<Repo>>(response)
                ?? new List<Repo>();
            foreach (var item in rawrepos)
            {
                if (item.name == null) continue;
                repos.Add(item.name);
            }
            if (repos.Count() == 30)
            {
                int i = 2;
                do
                {
                    url = $"https://api.github.com/users/{username}/repos?page={i}";
                    response = await client.GetStringAsync(url);

                    List<string> rawrepo = JsonSerializer.Deserialize<List<string>>(response)
                        ?? new List<string>();
                    if (rawrepo.Count() == 0) break;
                    foreach (var repo in rawrepo)
                    {
                        repos.Add(repo);
                    }
                    if (rawrepo.Count() < 30) break;
                    i++;
                } while (true);
            }

            // ランチャーがあったら削除
            string thissoft = "GryloscAppLauncher";
            if (repos.Contains(thissoft)) repos.Remove(thissoft);

            // 取得情報からListBoxに追加
            RepoListBox.Items.Clear();  // Loading表示を消す

            foreach (var repo in repos)
            {
                RepoListBox.Items.Add(repo ?? "noname");
            }
            RepoListBox.Enabled = true;
        }

        private async void RepoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RepoListBox.SelectedIndex != -1)
            {
                string username = "Grylosc";
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"https://raw.githubusercontent.com/{username}/{repos[RepoListBox.SelectedIndex]}/refs/heads/master/icon.png");
                    if (response.IsSuccessStatusCode)
                    {
                        IconBox.ImageLocation = $"https://raw.githubusercontent.com/{username}/{repos[RepoListBox.SelectedIndex]}/refs/heads/master/icon.png";
                    }
                    else
                    {
                        IconBox.ImageLocation = $"https://raw.githubusercontent.com/{username}/{repos[RepoListBox.SelectedIndex]}/refs/heads/main/icon.png";
                    }
                }

                SoftTitle.Text = repos[RepoListBox.SelectedIndex];
                SoftTitle.Enabled = true;

                // アップデート確認
                var handler = new HttpClientHandler();
                handler.AllowAutoRedirect = false;
                using (var client = new HttpClient(handler))
                {
                    string softname = repos[RepoListBox.SelectedIndex];
                    client.DefaultRequestHeaders.Add("User-Agent", "GryloscLauncher");
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
                    if (Program.softs.ContainsKey(SoftTitle.Text))
                    {
                        if (version != Program.softs[repos[RepoListBox.SelectedIndex]])    // バージョンに相違があれば更新ボタンを表示する
                        {
                            AddSoftwareButton.Text = "アップデート";
                            isUpdate = true;
                        }
                        else
                        {
                            AddSoftwareButton.Text = "インストール済み";
                            AddSoftwareButton.Enabled = false;
                        }
                        VersionText.Text = $"Local: {Program.afterSofts[repos[RepoListBox.SelectedIndex]]}\nLatest: {version}";
                    }
                    else
                    {
                        AddSoftwareButton.Text = "インストール";
                        AddSoftwareButton.Enabled = true;
                        VersionText.Text = $"Latest: {version}";
                    }
                }
            }
        }

        private void SoftTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string username = "grylosc";
            Program.OpenUrl($"https://github.com/{username}/{SoftTitle.Text}");
        }

        private async void AddSoftwareButton_Click(object sender, EventArgs e)
        {
            if (RepoListBox.SelectedIndex != -1 && !Program.isInstalling)
            {
                if (isUpdate) 
                {
                    if (MessageBox.Show($"{repos[RepoListBox.SelectedIndex]} を最新バージョンにアップデートしますか？", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        string softname = repos[RepoListBox.SelectedIndex];
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
                        if (File.Exists($"{Program.appFolder}/softs/{softname}/LanguageC{Program.softs[repos[RepoListBox.SelectedIndex]].Replace("v", "").Replace(".", "")}.json"))
                        {
                            File.Delete($"{Program.appFolder}/softs/{softname}/LanguageC{Program.softs[repos[RepoListBox.SelectedIndex]].Replace("v", "").Replace(".", "")}.json");
                        }
                        StatusText.Text = "データ整備中...";
                        Program.softs[softname] = version;
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
                else
                {
                    if (MessageBox.Show($"{repos[RepoListBox.SelectedIndex]} をインストールしますか？", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        string softname = repos[RepoListBox.SelectedIndex];
                        Program.isInstalling = true;
                        var handler = new HttpClientHandler();
                        handler.AllowAutoRedirect = false;
                        using var client = new HttpClient(handler);
                        client.DefaultRequestHeaders.Add("User-Agent", "GryloscLauncheer");
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
                        ZipFile.ExtractToDirectory(downloadPath, softpath);
                        StatusText.Text = "キャッシュ削除中...";
                        File.Delete(downloadPath);
                        StatusText.Text = "データ整備中";
                        Program.softs.Add(softname, version);
                        Program.data["Installed"] = JsonSerializer.Serialize(Program.softs);
                        Program.SaveJson();
                        MessageBox.Show($"{softname} のインストールが正常に完了しました。", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        StatusText.Text = "";
                        AddSoftwareButton.Text = "インストール済み";
                        AddSoftwareButton.Enabled = false;
                        Program.isInstalling = false;
                        Application.Restart();
                    }
                }
            }
        }
    }
}
