using System;
using NTextCat;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net;

namespace csgo_tts_ui
{
    public partial class csgo_tts_settings : Form
    {
        public static List<Button> containerOrder = new List<Button>();
        List<int> containerOffset = new List<int>() { 24, 128, 232, 336 };
        string configFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\CSGO_TTS\config.txt";
        public csgo_tts_settings()
        {
            InitializeComponent();
            containerOrder.Clear();
            foreach (char letter in csgo_tts_main.letters)
            {
                if (letter == 'N')
                {
                    ContainerName.BringToFront();
                    containerOrder.Add(ContainerName);
                    ContainerName.Left = containerOffset.ElementAt(containerOrder.IndexOf(ContainerName));
                }
                if (letter == 'F')
                {
                    ContainerFiller.BringToFront();
                    containerOrder.Add(ContainerFiller);
                    ContainerFiller.Left = containerOffset.ElementAt(containerOrder.IndexOf(ContainerFiller));
                }
                if (letter == 'S')
                {
                    ContainerSpot.BringToFront();
                    containerOrder.Add(ContainerSpot);
                    ContainerSpot.Left = containerOffset.ElementAt(containerOrder.IndexOf(ContainerSpot));
                }
                if (letter == 'M')
                {
                    ContainerMessage.BringToFront();
                    containerOrder.Add(ContainerMessage);
                    ContainerMessage.Left = containerOffset.ElementAt(containerOrder.IndexOf(ContainerMessage));
                }
            }
        }

        private Point MouseLocation;

        private void Container_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseLocation = e.Location;
            }
        }
        private void Container_MouseUp(object sender, MouseEventArgs e)
        {
            var container = (Button)sender;
            int xPos = e.X + container.Left - MouseLocation.X;
            ClipObject(xPos, container);
        }
        private void Container_MouseMove(object sender, MouseEventArgs e)
        {
            var container = (Button)sender;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                container.Left = e.X + container.Left - MouseLocation.X;
            }
        }

        private void ClipObject(int xPos, Button container)
        {
            if (xPos < 440 && xPos >= 336)
            {
                int indexOfReplacingContainer = containerOrder.IndexOf(container);
                Button containerToBeReplaced = containerOrder[3];
                containerOrder[indexOfReplacingContainer] = containerToBeReplaced;
                containerToBeReplaced.Left = containerOffset[indexOfReplacingContainer];

                container.Left = 336;
                containerOrder[3] = container;
            }
            if (xPos < 336 && xPos >= 232)
            {
                int indexOfReplacingContainer = containerOrder.IndexOf(container);
                Button containerToBeReplaced = containerOrder[2];
                containerOrder[indexOfReplacingContainer] = containerToBeReplaced;
                containerToBeReplaced.Left = containerOffset[indexOfReplacingContainer];

                container.Left = 232;
                containerOrder[2] = container;
            }
            if (xPos < 232 && xPos >= 128)
            {
                int indexOfReplacingContainer = containerOrder.IndexOf(container);
                Button containerToBeReplaced = containerOrder[1];
                containerOrder[indexOfReplacingContainer] = containerToBeReplaced;
                containerToBeReplaced.Left = containerOffset[indexOfReplacingContainer];

                container.Left = 128;
                containerOrder[1] = container;
            }
            if (xPos < 128)
            {
                int indexOfReplacingContainer = containerOrder.IndexOf(container);
                Button containerToBeReplaced = containerOrder[0];
                containerOrder[indexOfReplacingContainer] = containerToBeReplaced;
                containerToBeReplaced.Left = containerOffset[indexOfReplacingContainer];

                container.Left = 24;
                containerOrder[0] = container;
            }
            if(containerOrder.IndexOf(ContainerMessage) < containerOrder.IndexOf(ContainerFiller))
            {
                ContainerFiller.Text = "Filler: By";
            }
            else
            {
                ContainerFiller.Text = "Filler: Wrote";
            }
        }
        private Tuple<string, string> TTSDemoBuilder()
        {
            string messageDemo = string.Empty;
            string messageOrder = string.Empty;
            csgo_tts_main.letters.Clear();
            foreach (Button btn in containerOrder)
            {
                if (btn == ContainerName)
                {
                    messageDemo = messageDemo + " Player One ";
                    messageOrder = messageOrder + "N";
                    csgo_tts_main.letters.Add('N');
                }
                if (btn == ContainerFiller)
                {
                    if (ContainerFiller.Text.Contains("Wrote"))
                    {
                        messageDemo = messageDemo + " wrote ";
                    }
                    else
                    {
                        messageDemo = messageDemo + " by ";
                    }
                    messageOrder = messageOrder + "F";
                    csgo_tts_main.letters.Add('F');
                }
                if (btn == ContainerSpot)
                {
                    messageDemo = messageDemo + " @ Terrorist-Entry ";
                    messageOrder = messageOrder + "S";
                    csgo_tts_main.letters.Add('S');
                }
                if (btn == ContainerMessage)
                {
                    messageDemo = messageDemo + " Let's rush B! ";
                    messageOrder = messageOrder + "M";
                    csgo_tts_main.letters.Add('M');
                }
            }
            return Tuple.Create(messageDemo, messageOrder);
        }

        private void BtnPreviewTTS_Click(object sender, EventArgs e)
        {
            string messageDemo = TTSDemoBuilder().Item1;
            var builder = new PromptBuilder();
            builder.StartVoice(new CultureInfo(csgo_tts_main.region));
            builder.AppendText(messageDemo);
            builder.EndVoice();
            csgo_tts_main.synthesizer.SpeakAsync(builder);
            builder.ClearContent();
        }

        private void BtnSaveMessageOrder_Click(object sender, EventArgs e)
        {
            string messageOrder = TTSDemoBuilder().Item2;
            changeLine(11, "Message Order      =" + Convert.ToString(messageOrder), configFile);
            this.Hide();
            this.Close();
        }
        private void changeLine(int line_to_edit, string newText, string fileName)
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[line_to_edit - 1] = newText;
            File.WriteAllLines(fileName, arrLine);
        }

        //nah dude this is cheating, you were supposed to find this inside of the application
        private void Egg_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/paypalme/snoutie");
        }
    }
}
