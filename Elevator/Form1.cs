using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elevator
{
    public partial class Form1 : Form
    {
        private Elevator _elevator;
        public Form1()
        {
            InitializeComponent();
            _elevator = new Elevator(2);
            _elevator.Arrival += value => floorProgressBar.Invoke((MethodInvoker)(() => floorProgressBar.Value = value * 10));
            _elevator.EndMove += value => startButton.Invoke((MethodInvoker)(() => startButton.Enabled = true));
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            int nextFloor = Convert.ToInt32(nextFloorTextBox.Text);
            _elevator.NextFloor = nextFloor;
            _elevator.Start();
            startButton.Enabled = false;
        }
    }
}
