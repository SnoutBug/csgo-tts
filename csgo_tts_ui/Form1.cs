using System;
using System.IO;
using System.Speech.Synthesis;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;

//please excuse this mess, this is my first ever program in c#

namespace csgo_tts_ui
{
    public partial class Form1 : Form
    {
        string path = @"C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Global Offensive";
        string configPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\CSGO_TTS\";
        string newPath;
        string region = "en-US";
        string genderStr = "Male";
        long logSize;
        string name = "";
        string spot;
        string message;
        string fullMessage = "";
        string newLine = "";
        string lastChatter = "";
        bool readNames = true;
        bool readSpots = false;
        bool combine = true;
        bool filler = true;
        bool started = false;
        bool newPlayer = false;
        bool switching = false;
        bool useAlias = false;
        bool init = true;
        int timeout = 10;
        int playerIndex;
        int len1;
        int len2;
        DateTime currentTime;
        VoiceGender gender = VoiceGender.Male;
        List<string> lastMessage = new List<string>();
        List<string> players = new List<string>();
        List<string> alias = new List<string>();
        List<DateTime> timeoutPlayer = new List<DateTime>();
        List<string> config = new List<string>();
        List<bool> muted = new List<bool>();
        public Form1()
        {
            InitializeComponent();
            bw.DoWork += backgroundWorker1_DoWork;
            bw.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            bw.WorkerReportsProgress = false;
            bw.WorkerSupportsCancellation = true;
            string configFile = configPath + @"config.txt";
            int g = 0;
            int r = 0;

            //create config file
            if (!File.Exists(configFile))
            {
                if (!Directory.Exists(configPath))
                {
                    Directory.CreateDirectory(configPath);
                }
                config.Add("CS:GO folder path  =" + path);
                config.Add("Spam-Timeout (sec) =" + timeout);
                config.Add("Read Names         =" + readNames);
                config.Add("Filler             =" + filler);
                config.Add("Read Spots         =" + readSpots);
                config.Add("Combine Messages   =" + combine);
                config.Add("Region             =" + region);
                config.Add("Gender             =" + gender);
                config.Add("Use Alias          =" + useAlias);
                File.WriteAllLines(configFile, config);
            }

            //read config file
            path = File.ReadLines(configFile).Skip(0).Take(1).First().Split('=')[1];
            timeout = Int16.Parse(File.ReadLines(configFile).Skip(1).Take(1).First().Split('=')[1]);
            readNames = bool.Parse(File.ReadLines(configFile).Skip(2).Take(1).First().Split('=')[1]);
            filler = bool.Parse(File.ReadLines(configFile).Skip(3).Take(1).First().Split('=')[1]);
            readSpots = bool.Parse(File.ReadLines(configFile).Skip(4).Take(1).First().Split('=')[1]);
            combine = bool.Parse(File.ReadLines(configFile).Skip(5).Take(1).First().Split('=')[1]);
            region = File.ReadLines(configFile).Skip(6).Take(1).First().Split('=')[1];
            genderStr = File.ReadLines(configFile).Skip(7).Take(1).First().Split('=')[1];
            useAlias = bool.Parse(File.ReadLines(configFile).Skip(8).Take(1).First().Split('=')[1]);

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
            numTimeout.Value = timeout;
            textPath.Text = path;
            dropGender.SelectedIndex = g;

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

            init = false;
            UpdateFileSize();

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
        }
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

                    if (newLine.Contains('@') && newLine.Contains("Terrorist)"))
                    {
                        //chat message found
                        dynamic match = Regex.Match(newLine, @"Terrorist\) (.+?)@ ").Groups[1].Value;
                        match = Regex.Match(match, @"(?i)^[A-Za-z0-9 @!#_:-]+");
                        name = Convert.ToString(match);
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
                        match = Regex.Match(newLine, @" : (.*)*").Groups[1].Value;
                        match = Regex.Match(newLine, @" @ (.+?) : ").Groups[1].Value;
                        spot = Convert.ToString(match);
                        message = newLine.Split(new[] { @"@ " + spot + " : " }, StringSplitOptions.None).Last();
                        message = message.Replace("\n", "");
                        //compile message from config file settings
                        fullMessage = "";


                        if (readNames)
                        {
                            fullMessage = name;
                            if (useAlias)
                            {
                                if (alias[playerIndex] != "None")
                                {
                                    fullMessage = alias[playerIndex];
                                }
                            }
                        }

                        if (filler)
                        {
                            fullMessage = fullMessage + "wrote:";
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
                                //else
                                //{
                                timeoutPlayer[playerIndex] = currentTime.AddSeconds(timeout);
                                //}
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
                if (newPlayer)
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
                bw.RunWorkerAsync();
            }
            else
            {
                btnBrowse.Enabled = true;
                btnStartStop.Text = "Start";
                started = false;
                bw.CancelAsync();

            }
            UpdateFileSize();
        }
        static string[] RefreshLog(string path)
        {
            List<string> file = new List<string>();
            if (File.Exists(path + @"\csgo\console.log"))
            {
                using (var csv = new FileStream(path + @"\csgo\console.log", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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
            }
            else
            {
                readNames = false;
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
            changeLine(1, "CS:GO folder path  =" + Convert.ToString(path), tempPath);
            changeLine(2, "Spam-Timeout (sec) =" + Convert.ToString(timeout), tempPath);
            changeLine(3, "Read Names         =" + Convert.ToString(readNames), tempPath);
            changeLine(4, "Filler             =" + Convert.ToString(filler), tempPath);
            changeLine(5, "Read Spots         =" + Convert.ToString(readSpots), tempPath);
            changeLine(6, "Combine Messages   =" + Convert.ToString(combine), tempPath);
            changeLine(7, "Region             =" + Convert.ToString(region), tempPath);
            changeLine(8, "Gender             =" + Convert.ToString(genderStr), tempPath);
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

        private void Button1_Click(object sender, EventArgs e)
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
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
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

        private void Button1_Click_1(object sender, EventArgs e)
        {
            string message = "Set up:\n1. Open your Steam Library.\n2. Find Counter-Strike: Global Offensive in your Library.\n3. Right-click CS:GO then chose Properties.\n4. Click the \"SET LAUNCH OPTIONS\" button.\n5. Enter -condebug and confirm.";
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
        }
    }
}
