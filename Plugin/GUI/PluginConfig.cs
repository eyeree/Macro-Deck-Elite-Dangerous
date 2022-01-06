using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EliteDangerousMacroDeckPlugin.GUI
{
    public partial class PluginConfig : DialogForm
    {
        public PluginConfig()
        {
            InitializeComponent();
        }

        private void CreateProfile_Click(object sender, EventArgs e)
        {



        }

        private void PluginLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadUrl("https://github.com/eyeree/Macro-Deck-Elite-Dangerous");
        }

        private void LoadUrl(string url)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void EliteJournalReaderLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadUrl("https://github.com/MagicMau/EliteJournalReader");
        }

        private void InputSimulatorStandard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadUrl("https://github.com/GregsStack/InputSimulatorStandard");
        }
    }
}
