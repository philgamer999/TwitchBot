using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitchBot
{
    public partial class BotController : Form
    {
        // Constructor to initialize the BotController form
        public BotController()
        {
            InitializeComponent();
        }

        // Event to notify when a command button is clicked
        public event Action<string> OnCommand;

        // Handler for the Toggle Bot button click event
        private void ButtonToggle_Click(object sender, EventArgs e)
        {
            OnCommand?.Invoke("TOGGLE");
        }

        // Handler for the Chat Fish button click event
        private void ButtonChatFish_Click(object sender, EventArgs e)
        {
            OnCommand?.Invoke("CHATFISH");
        }
    }
}
