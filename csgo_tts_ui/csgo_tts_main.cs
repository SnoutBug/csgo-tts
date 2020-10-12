using System;
using NTextCat;
using System.IO;
using System.Net.Http;
using System.Speech.Synthesis;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net;

//please excuse this mess, this is my first ever program in c#

namespace csgo_tts_ui
{
    public partial class csgo_tts_main : Form
    {
        //CURRENT VERSION
        double currentVersion = 1.31;


        //default settings
        string path = @"C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Global Offensive";
        string region = "en-US";
        string genderStr = "Male";              // gender readable
        VoiceGender gender = VoiceGender.Male; // and non-readable
        bool readNames = true;
        bool readSpots = false;
        bool combine = true;
        bool filler = true;
        bool useAlias = false;
        bool translate = true;
        int timeout = 10;

        //some variables i need in multiple methods
        string newPath;
        string configPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\CSGO_TTS\";
        long logSize;

        int len1; //previous log length
        int len2; //current log length
        string newLine = ""; //latest updated line

        //used for logging players
        int playerIndex;
        string lastChatter = "";
        bool newPlayer = false;
        bool getPlayers = false;
        bool newPlayerList = false;
        DateTime currentTime;

        //processed, clean variables
        string spot;
        string name = "";
        string message;

        //full processed message containing fillers, spots, names, whatever the user checked
        string fullMessage = "";

        //is started and initialized
        bool started = false;
        bool init = true;

        //used so the check-boxes wont untick on change
        bool switching = false;

        //lists
        List<string> lastMessage = new List<string>();
        List<string> players = new List<string>();
        List<string> alias = new List<string>();
        List<DateTime> timeoutPlayer = new List<DateTime>();
        List<string> config = new List<string>();
        List<bool> muted = new List<bool>();

        public csgo_tts_main()
        {
            InitializeComponent();
            bw.DoWork += backgroundWorker1_DoWork;
            bw.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            bw.WorkerReportsProgress = false;
            bw.WorkerSupportsCancellation = true;

            //i will come back for this but it works for now, finally, ive spent way to long on this
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve2);

            string configFile = configPath + @"config.txt";

            int g = 0;//gender in numeral
            int r = 0;//region index in culture drop down

            //check for 
            //string latestVersion = await GetLatestVersion();


            //create config file
            if (!File.Exists(configFile))
            {
                if (!Directory.Exists(configPath))
                {
                    Directory.CreateDirectory(configPath);
                }
                CreateConfig(configFile);
            }

            if (!File.Exists(configPath + "Core14.profile.xml"))
            {
                File.WriteAllText(configPath + "Core14.profile.xml", Properties.Resources.Core14_profile);
            }

            //read config file
            if (File.ReadAllLines(configFile).Length > 9)
            {
                path = File.ReadLines(configFile).Skip(0).Take(1).First().Split('=')[1];
                timeout = Int16.Parse(File.ReadLines(configFile).Skip(1).Take(1).First().Split('=')[1]);
                readNames = bool.Parse(File.ReadLines(configFile).Skip(2).Take(1).First().Split('=')[1]);
                filler = bool.Parse(File.ReadLines(configFile).Skip(3).Take(1).First().Split('=')[1]);
                readSpots = bool.Parse(File.ReadLines(configFile).Skip(4).Take(1).First().Split('=')[1]);
                combine = bool.Parse(File.ReadLines(configFile).Skip(5).Take(1).First().Split('=')[1]);
                region = File.ReadLines(configFile).Skip(6).Take(1).First().Split('=')[1];
                genderStr = File.ReadLines(configFile).Skip(7).Take(1).First().Split('=')[1];
                useAlias = bool.Parse(File.ReadLines(configFile).Skip(8).Take(1).First().Split('=')[1]);
                translate = bool.Parse(File.ReadLines(configFile).Skip(9).Take(1).First().Split('=')[1]);
            }
            else
            {
                File.Delete(configFile);
                CreateConfig(configFile);
            }
            //convert variables from config file to usable
            if (genderStr == "Female")
            {
                gender = VoiceGender.Female;
                g = 1;
            }
            else
            {
                gender = VoiceGender.Male;
                g = 0;
            }

            //refresh ui
            if (readNames)
            {
                checkName.Checked = true;
                checkAlias.Enabled = true;
            }
            else
            {
                checkAlias.Enabled = false;
            }
            if (readSpots)
            {
                checkSpot.Checked = true;
            }
            if (combine)
            {
                checkCombine.Checked = true;
            }
            if (filler)
            {
                checkFiller.Checked = true;
            }
            if (useAlias)
            {
                checkAlias.Checked = true;
            }
            if (translate)
            {
                checkTranslate.Checked = true;
            }
            numTimeout.Value = timeout;
            textPath.Text = path;
            dropGender.SelectedIndex = g;

            btnRefresh.Enabled = false;
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {
                foreach (InstalledVoice voice in synth.GetInstalledVoices())
                {
                    VoiceInfo info = voice.VoiceInfo;
                    if (dropRegion.FindStringExact(info.Culture.ToString()) != -1)
                    {
                        dropRegion.Items.RemoveAt(dropRegion.FindStringExact(info.Culture.ToString()));
                    }
                    dropRegion.Items.Add(info.Culture);
                    if (info.Culture.ToString() == region)
                    {
                        r = dropRegion.FindStringExact(region);
                    }
                }
            }
            dropRegion.SelectedIndex = r;

            if (!Directory.Exists(path + @"\csgo"))
            {
                MessageBox.Show("Please set up your CS:GO folder path by pressing the browse button!", "Folder not found.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textPath.ForeColor = Color.Red;
            }

            if (btnDelete.Enabled)
            {
                if (IsFileUsed(path + @"\csgo\console.log"))
                {
                    btnDelete.Enabled = false;
                    btnDelete.Text = "In Use";
                }
            }
            else
            {
                if (!IsFileUsed(path + @"\csgo\console.log"))
                {
                    btnDelete.Enabled = true;
                    btnDelete.Text = "Delete";
                }
            }


            init = false;
            UpdateFileSize();
            CheckForUpdates();
        }

        //main background loop
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var synthesizer = new SpeechSynthesizer();
            var builder = new PromptBuilder();
            len1 = RefreshLog(path).Length;

            while (!bw.CancellationPending)
            {
                if (!File.Exists(path + @"\csgo\console.log"))
                {
                    MessageBox.Show("Please enter a valid path!", "Invalid folder path", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bw.CancelAsync();
                }
                if (btnDelete.Enabled)
                {
                    if (IsFileUsed(path + @"\csgo\console.log"))
                    {
                        InvokeDisableDeleteButton();
                    }
                }
                else
                {
                    if (!IsFileUsed(path + @"\csgo\console.log"))
                    {
                        InvokeEnableDeleteButton();
                    }
                }
                len2 = RefreshLog(path).Length;
                if (len2 > len1)
                {
                    len1 = len2;
                    var temp = RefreshLog(path);
                    newLine = temp[temp.Length - 1];
                    currentTime = DateTime.Now;
                    UpdateFileSize();
                    //                   \/ special char right here
                    if (newLine.Contains('‎') && newLine.Contains("Terrorist)"))
                    {
                        //chat message found
                        dynamic match = Regex.Match(newLine, @"Terrorist\) (.+?)‎").Groups[1].Value;
                        name = RemoveSpecialChars(match);

                        if (!players.Contains(name))
                        {
                            //new chatter found
                            players.Add(name);
                            timeoutPlayer.Add(DateTime.Now);
                            lastMessage.Add("0");
                            muted.Add(false);
                            alias.Add("None");
                            newPlayer = true;
                        }

                        playerIndex = players.IndexOf(name);
                        if (newLine.Contains('@'))
                        {
                            match = Regex.Match(newLine, @" @ (.+?) : ").Groups[1].Value;
                            spot = Convert.ToString(match);
                            message = newLine.Split(new[] { @"@ " + spot + " : " }, StringSplitOptions.None).Last();
                        }
                        else
                        {
                            spot = "";
                            message = newLine.Split(new[] { @"‎ : "}, StringSplitOptions.None).Last();
                        }

                        message = message.Replace("\n", "");
                        //compile message from config file settings
                        string from = GetLanguage(message);
                        string to = region.Substring(0, 2);
                        string fillerStr = "wrote";
                        fullMessage = "";
                        if (translate)
                        {
                            message = Translate(message, from, to);
                            fillerStr = Translate(fillerStr, "en", to);
                        }

                        if (readNames)
                        {
                            fullMessage = name;
                            if (useAlias)
                            {
                                if (alias[playerIndex] != "None")
                                {
                                    fullMessage = Translate(alias[playerIndex], "en", to);
                                }
                            }
                        }

                        if (filler)
                        {
                            fullMessage = fullMessage + " "+fillerStr+": ";
                        }
                        else
                        {
                            fullMessage = fullMessage + "!";
                        }

                        if (readSpots)
                        {
                            fullMessage = fullMessage + " @" + spot;
                        }

                        if (combine)
                        {
                            if (lastChatter == name)
                            {
                                if (timeoutPlayer[playerIndex] >= currentTime)
                                {
                                    fullMessage = "";
                                }
                                else
                                {
                                    lastChatter = "";
                                }
                            }
                        }
                        fullMessage = fullMessage + message;
                        builder.StartVoice(new CultureInfo(region));
                        synthesizer.SelectVoiceByHints(gender, VoiceAge.Adult);

                        if (!muted[playerIndex])
                        {
                            if (lastMessage[playerIndex] == message)
                            {
                                if (timeoutPlayer[playerIndex] <= currentTime)
                                {
                                    synthesizer.SetOutputToDefaultAudioDevice();
                                    builder.AppendText(fullMessage);
                                    builder.EndVoice();
                                    synthesizer.Speak(builder);
                                    lastMessage[playerIndex] = message;
                                    lastChatter = name;

                                }
                                timeoutPlayer[playerIndex] = currentTime.AddSeconds(timeout);
                            }
                            else
                            {
                                synthesizer.SetOutputToDefaultAudioDevice();
                                builder.AppendText(fullMessage);
                                builder.EndVoice();
                                synthesizer.Speak(builder);
                                lastMessage[playerIndex] = message;
                                lastChatter = name;
                                timeoutPlayer[playerIndex] = currentTime.AddSeconds(timeout);
                            }
                        }
                        builder.ClearContent();
                    }
                }
                //get players with status command
                if (getPlayers)
                {
                    List<int> statusList = new List<int>();
                    int lc = 0;
                    getPlayers = false;
                    string[] lines = RefreshLog(path);

                    foreach (string i in lines)
                    {
                        lc += 1;
                        if (i.Contains("# userid name uniqueid connected ping loss state rate"))
                        {
                            statusList.Add(lc);
                        }
                    }
                    int m = statusList.Max();
                    players.Clear();
                    timeoutPlayer.Clear();
                    lastMessage.Clear();
                    muted.Clear();
                    alias.Clear();

                    while (lines[m].Contains("\" STEAM_") || lines[m].Contains("\" BOT"))
                    {
                        string d = lines[m];
                        string name;
                        if (d.Contains("\" STEAM_"))
                        {
                            int pFrom = d.IndexOf(" \"") + " \"".Length;
                            int pTo = d.LastIndexOf("\" STEAM_");
                            name = RemoveSpecialChars(d.Substring(pFrom, pTo - pFrom));
                            players.Add(name);
                            timeoutPlayer.Add(DateTime.Now);
                            lastMessage.Add("0");
                            muted.Add(false);
                            alias.Add("None");
                            newPlayerList = true;
                            newPlayer = false;
                        }
                        m += 1;
                    }
                }

                if (newPlayer || newPlayerList)
                {
                    bw.CancelAsync();
                }
                Thread.Sleep(2);
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                label1.Text = "stopped";
            }
            else if (e.Error != null)
            {
                label1.Text = "There was an error running the process. The thread aborted";
            }
            else
            {
                if (newPlayer)
                {
                    newPlayer = false;
                    PopulatePlayerList();
                    bw.RunWorkerAsync();
                    labelSize.Text = Convert.ToString(logSize);
                }
                else if (newPlayerList)
                {
                    newPlayerList = false;
                    dropPlayerName.Items.Clear();
                    foreach (string i in players)
                    {
                        dropPlayerName.Items.Add(i);
                    }
                    bw.RunWorkerAsync();
                }
                else
                {
                    label1.Text = "Stopped looking for messages.";
                }

            }
        }

        private void BtnStartStop_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(path + @"\csgo\"))
            {
                if (!File.Exists(path + @"\csgo\console.log"))
                {
                    var consoleLog = File.Create(path + @"\csgo\console.log");
                    consoleLog.Close();
                    MessageBox.Show("Make sure to enable -condebug in the CS:GO launch options.", "Launch options probably not set", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("The path you set was not valid", "Invalid folder path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                started = true;
                textPath.ForeColor = Color.Red;
            }
            if (!started)
            {
                btnBrowse.Enabled = false;
                btnStartStop.Text = "Stop";
                label1.Text = "Looking for chat messages!";
                started = true;
                btnRefresh.Enabled = true;
                bw.RunWorkerAsync();
            }
            else
            {
                btnBrowse.Enabled = true;
                btnStartStop.Text = "Start";
                started = false;
                btnRefresh.Enabled = false;
                bw.CancelAsync();

            }
            UpdateFileSize();
        }
        static string[] RefreshLog(string path)
        {
            List<string> file = new List<string>();
            if (File.Exists(path + @"\csgo\console.log"))
            {
                using (var csv = new FileStream(path + @"\csgo\console.log", System.IO.FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var sr = new StreamReader(csv))
                {
                    while (!sr.EndOfStream)
                    {
                        file.Add(sr.ReadLine());
                    }


                }
            }
            else
            {

            }
            return file.ToArray();
        }

        private void CheckName_CheckedChanged(object sender, EventArgs e)
        {
            if (checkName.Checked)
            {
                readNames = true;
                checkAlias.Enabled = true;
            }
            else
            {
                readNames = false;
                checkAlias.Enabled = false;
            }
            saveChanges();
        }

        private void CheckSpot_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSpot.Checked)
            {
                readSpots = true;
            }
            else
            {
                readSpots = false;
            }
            saveChanges();
        }

        private void CheckFiller_CheckedChanged(object sender, EventArgs e)
        {
            if (checkFiller.Checked)
            {
                filler = true;
            }
            else
            {
                filler = false;
            }
            saveChanges();
        }

        private void CheckCombine_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCombine.Checked)
            {
                combine = true;
            }
            else
            {
                combine = false;
            }
            saveChanges();
        }

        private void NumTimeout_ValueChanged(object sender, EventArgs e)
        {
            timeout = Convert.ToInt16(numTimeout.Value);
            saveChanges();
        }
        static void changeLine(int line_to_edit, string newText, string fileName)
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[line_to_edit - 1] = newText;
            File.WriteAllLines(fileName, arrLine);
        }

        private void saveChanges()
        {
            string tempPath = configPath + @"\config.txt";
            changeLine(1,  "CS:GO folder path  =" + Convert.ToString(path), tempPath);
            changeLine(2,  "Spam-Timeout (sec) =" + Convert.ToString(timeout), tempPath);
            changeLine(3,  "Read Names         =" + Convert.ToString(readNames), tempPath);
            changeLine(4,  "Filler             =" + Convert.ToString(filler), tempPath);
            changeLine(5,  "Read Spots         =" + Convert.ToString(readSpots), tempPath);
            changeLine(6,  "Combine Messages   =" + Convert.ToString(combine), tempPath);
            changeLine(7,  "Region             =" + Convert.ToString(region), tempPath);
            changeLine(8,  "Gender             =" + Convert.ToString(genderStr), tempPath);
            changeLine(9,  "Use Alias          =" + Convert.ToString(useAlias), tempPath);
            changeLine(10, "Auto-Translate     =" + Convert.ToString(translate), tempPath);
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                textPath.Text = folderBrowser.SelectedPath;
                path = folderBrowser.SelectedPath;
                textPath.ForeColor = Color.Black;
                saveChanges();
            }
        }
        private void TextPath_TextChanged(object sender, EventArgs e)
        {
            newPath = textPath.Text;
            textPath.ForeColor = Color.MediumSlateBlue;
            UpdateFileSize();
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (Directory.Exists(newPath))
                {
                    textPath.ForeColor = Color.Black;
                    path = newPath;
                    saveChanges();
                }
                else
                {
                    textPath.ForeColor = Color.Red;
                    MessageBox.Show("Please enter a valid path!", "Invalid folder path", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                UpdateFileSize();
            }
        }

        private void DropGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            genderChange();
            saveChanges();
        }

        private void DropRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!init)
            {
                region = Convert.ToString(dropRegion.SelectedItem);
                using (SpeechSynthesizer synth = new SpeechSynthesizer())
                {
                    synth.GetInstalledVoices(new CultureInfo(region));
                    dropGender.Items.Clear();
                    foreach (InstalledVoice voice in synth.GetInstalledVoices(new CultureInfo(region)))
                    {
                        VoiceInfo info = voice.VoiceInfo;
                        dropGender.Items.Add(info.Gender.ToString());
                        if (genderStr != info.Gender.ToString())
                        {
                            genderStr = info.Gender.ToString();
                            genderChange();
                        }
                        dropGender.SelectedIndex = dropGender.FindStringExact(info.Gender.ToString());
                    }
                }
                saveChanges();
            }
        }

        private void genderChange()
        {
            if (dropGender.SelectedIndex == dropGender.FindStringExact("Male"))
            {
                gender = VoiceGender.Male;
                genderStr = "Male";
            }
            else
            {
                gender = VoiceGender.Female;
                genderStr = "Female";
            }
        }

        private void DropPlayerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            switching = true;
            if (muted[dropPlayerName.SelectedIndex])
            {
                checkMute.Checked = true;
            }
            else
            {
                checkMute.Checked = false;
            }
            if (alias[dropPlayerName.SelectedIndex] != "None")
            {
                dropAlias.SelectedIndex = dropAlias.FindStringExact(alias[dropPlayerName.SelectedIndex]);
            }
            else
            {
                dropAlias.SelectedIndex = dropAlias.FindStringExact("None");
            }
            switching = false;
        }
        private void PopulatePlayerList()
        {
            dropPlayerName.Items.Add(players.Last());
        }

        private void CheckMute_CheckedChanged(object sender, EventArgs e)
        {
            if (!switching)
            {
                if (dropPlayerName.SelectedIndex != -1)
                {
                    if (!muted[dropPlayerName.SelectedIndex])
                    {
                        muted[dropPlayerName.SelectedIndex] = true;
                    }
                    else
                    {
                        muted[dropPlayerName.SelectedIndex] = false;
                    }

                }
            }
        }

        private void DropAlias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!switching)
            {
                if (dropPlayerName.SelectedIndex != -1)
                {
                    alias[dropPlayerName.SelectedIndex] = dropAlias.SelectedItem.ToString();
                }
            }
        }

        private void CheckAlias_CheckedChanged(object sender, EventArgs e)
        {
            if (!useAlias)
            {
                useAlias = true;
            }
            else
            {
                useAlias = false;
            }
            saveChanges();
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            if (label1.Text == "Stay hydrated!")
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\Friedrich\Documents\Projects\csgo_tts_ui\csgo_tts_ui\Nut Button Sound Effect.wav");
                player.Play();
            }
        }
        private void UpdateFileSize()
        {
            if (File.Exists(path + @"\csgo\console.log"))
            {
                logSize = new System.IO.FileInfo(path + @"\csgo\console.log").Length;

                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                int order = 0;
                while (logSize >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    logSize = logSize / 1024;
                }
                string logSizeText = String.Format("{0:0.##} {1}", logSize, sizes[order]);

                if (this.labelSize.InvokeRequired)
                {
                    this.labelSize.BeginInvoke((MethodInvoker)delegate () { this.labelSize.Text = logSizeText; });
                }
                else
                {
                    this.labelSize.Text = logSizeText;
                }
            }
            else
            {
                this.labelSize.Text = "ERR_FILE_NOT_FOUND";
            }
                
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!IsFileUsed(path + @"\csgo\console.log"))
            {
                File.WriteAllText(path + @"\csgo\console.log", "");
                len1 = RefreshLog(path).Length;
                btnDelete.Enabled = true;
                btnDelete.Text = "Delete";
                UpdateFileSize();
            }
            else
            {
                btnDelete.Enabled = false;
                btnDelete.Text = "In Use";
            }

        }
        protected virtual bool IsFileUsed(string path)
        {
            FileInfo file = new FileInfo(path);
            try
            {
                using (FileStream stream = file.Open(System.IO.FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }

            //file is not locked
            return false;
        }
        private void InvokeDisableDeleteButton()
        {
            this.labelSize.BeginInvoke((MethodInvoker)delegate ()
            {
                this.btnDelete.Text = "In Use";
                this.btnDelete.Enabled = false;
            });
        }
        private void InvokeEnableDeleteButton()
        {
            this.labelSize.BeginInvoke((MethodInvoker)delegate ()
            {
                this.btnDelete.Text = "Delete";
                this.btnDelete.Enabled = true;
            });
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string message = "Set up:\n1. Open your Steam Library.\n" +
                "2. Find Counter-Strike: Global Offensive in your Library.\n" +
                "3. Right-click CS:GO then chose Properties.\n" +
                "4. Click the \"SET LAUNCH OPTIONS\" button.\n" +
                "5. Enter -condebug and confirm." +
                "\n" +
                "\n" +
                "created by snoutie";
            string title = "Counter-Strike: Global Offensive TTS - Help Window";
            DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Question);
            if (IsFileUsed(path+@"\csgo\console.log"))
            {
                btnDelete.Text = "In Use";
                btnDelete.Enabled = false;
            }
            else
            {
                btnDelete.Text = "Delete";
                btnDelete.Enabled = true;
            }
            CheckForUpdates();
        }
        private void CheckForUpdates()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "csgotts");
                var response = client.GetAsync("https://api.github.com/repos/snoutbug/csgotts/releases/latest").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    string responseString = responseContent.ReadAsStringAsync().Result;
                    double ver = Convert.ToDouble(responseString.Split(',')[7].Split('"')[3].Replace('.',','));
                    if (ver > currentVersion)
                    {
                        DialogResult result = MessageBox.Show("There is a new version available.\nDo you want to download it?", "Update Available!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start("https://github.com/snoutbug/csgotts/releases/latest");
                            this.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Could not check for updates.", "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (started)
            {
                DialogResult result = MessageBox.Show("Please enter \"status\" in your CS:GO-console now.\nPress OK when you are done.", "Refresh Player List.", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    getPlayers = true;
                }
            }
            else
            {
                btnRefresh.Enabled = false;
                MessageBox.Show("Please press the \"Start\"-Button first.", "Worker not started yet.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static string RemoveSpecialChars(dynamic m)
        {
            m = Regex.Match(m, @"(?i)^[A-Za-z0-9 @!#(_):-]+");
            string str = Convert.ToString(m);
            return str;
        }

        private void CheckAlias_MouseHover(object sender, EventArgs e)
        {
            if (readNames)
            {
                toolTip.SetToolTip(checkAlias, "Use colors, instead of player names");
            }
        }

        bool IsShown = false;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Control ctrl = this.GetChildAtPoint(e.Location);

            if (ctrl != null)
            {
                if (ctrl == this.checkAlias && !IsShown)
                {
                    string tipstring = "Enable \"Read Names\" to use this feature";
                    this.toolTip.GetToolTip(this.checkAlias);
                    this.toolTip.Show(tipstring, this.checkAlias,
                    ctrl.Width / 2, ctrl.Height / 2);
                    IsShown = true;
                }
            }
            else
            {
                this.toolTip.Hide(this.checkAlias);
                IsShown = false;
            }
        }

        private string GetLanguage(string input)
        {
            var factory = new RankedLanguageIdentifierFactory();
            string lang;

            var identifier = factory.Load(configPath+"Core14.profile.xml");
            var languages = identifier.Identify(input);
            var mostCertainLanguage = languages.FirstOrDefault();
            if (mostCertainLanguage != null)
                lang = mostCertainLanguage.Item1.Iso639_3.Substring(0, 2);
            else
                lang = "en";
            return lang;
        }

        private void CheckTranslate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTranslate.Checked)
            {
                translate = true;
            }
            else
            {
                translate = false;
            }
            saveChanges();
        }

        public string Translate(string word, string from, string to)
        {
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={from}&tl={to}&dt=t&q={word}";
            var webClient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };
            var result = webClient.DownloadString(url);
            try
            {
                result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                return result;
            }
            catch
            {
                return word;
            }
        }
        public void CreateConfig(string configFile)
        {
            config.Add("CS:GO folder path  =" + path);
            config.Add("Spam-Timeout (sec) =" + timeout);
            config.Add("Read Names         =" + readNames);
            config.Add("Filler             =" + filler);
            config.Add("Read Spots         =" + readSpots);
            config.Add("Combine Messages   =" + combine);
            config.Add("Region             =" + region);
            config.Add("Gender             =" + gender);
            config.Add("Use Alias          =" + useAlias);
            config.Add("Auto-Translate     =" + translate);
            File.WriteAllLines(configFile, config);
        }
        System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string dllName = args.Name.Contains(',') ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");

            dllName = dllName.Replace(".", "_");

            if (dllName.EndsWith("_resources")) return null;

            System.Resources.ResourceManager rm = new System.Resources.ResourceManager(GetType().Namespace + ".Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());

            byte[] bytes = (byte[])rm.GetObject(dllName);

            return System.Reflection.Assembly.Load(bytes);
        }
        System.Reflection.Assembly CurrentDomain_AssemblyResolve2(object sender, ResolveEventArgs args)
        {
            string dllName = args.Name.Contains(',') ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".xml", "");

            dllName = dllName.Replace(".", "_");

            if (dllName.EndsWith("_resources")) return null;

            System.Resources.ResourceManager rm = new System.Resources.ResourceManager(GetType().Namespace + ".Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());

            byte[] bytes = (byte[])rm.GetObject(dllName);

            return System.Reflection.Assembly.Load(bytes);
        }
    }
}
