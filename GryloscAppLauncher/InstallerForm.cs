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

        public InstallerForm()
        {
            InitializeComponent();
        }

        public class Repo
        {
            public string? name { get; set; }
        }

        private async void InstallerForm_Shown(object sender, EventArgs e)
        {
            // Loading表示
            RepoListBox.Items.Clear();
            RepoListBox.Items.Add("Loading...");

            // GithubのGryloscのリポジトリ一覧を取得
            var username = "grylosc";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "GryloscLauncheer");

            var url = $"https://api.github.com/users/{username}/repos";

            var response = await client.GetStringAsync(url);


            repos.Clear();
            List<Repo> rawrepos = JsonSerializer.Deserialize<List<Repo>>(response)
                ?? new List<Repo>();
            foreach (var item in rawrepos)
            {
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
                string username = "grylosc";
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
                if (Program.softs.Contains(SoftTitle.Text))
                {
                    AddSoftwareButton.Text = "インストール済み";
                    AddSoftwareButton.Enabled = false;
                }
                else
                {
                    AddSoftwareButton.Text = "インストール";
                    AddSoftwareButton.Enabled = true;
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
                if (MessageBox.Show($"{repos[RepoListBox.SelectedIndex]} をインストールしますか？","Info",MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Program.isInstalling = true;
                    StatusText.Text = "ダウンロード中...";
                    string username = "Grylosc";
                    string url = $"https://github.com/{username}/{repos[RepoListBox.SelectedIndex]}/releases/latest/download/{repos[RepoListBox.SelectedIndex]}-UnuseRuntime.zip";
                    string downloadPath = $"{Program.appFolder}/rawd/download.zip";
                    Debug.WriteLine($"{repos[RepoListBox.SelectedIndex]}をダウンロード中...\n   username: {username}\n  url: {url}\n    downloadPath: {downloadPath}");
                    using var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("User-Agent", "GryloscLauncheer");
                    await File.WriteAllBytesAsync(
                        downloadPath,
                        await client.GetByteArrayAsync(url)
                        );

                    StatusText.Text = "ファイル解凍中...";
                    string softpath = $"{Program.appFolder}/softs/{repos[RepoListBox.SelectedIndex]}";
                    ZipFile.ExtractToDirectory(downloadPath, softpath);
                    Program.softs.Add(repos[RepoListBox.SelectedIndex]);
                    Program.data["Installed"] = JsonSerializer.Serialize(Program.softs);
                    Program.SaveJson();
                    StatusText.Text = "キャッシュ削除中...";
                    File.Delete(downloadPath);
                    MessageBox.Show($"{repos[RepoListBox.SelectedIndex]} のインストールが正常に完了しました。", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
