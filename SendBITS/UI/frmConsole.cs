using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendBITS.UI
{
    public partial class frmConsole : Form
    {//TODO: Create this class to handle Console write events will color styles and accept Console inputs (shell like commands)
        TextWriter writer;
        public frmConsole()
        {
            InitializeComponent();
        }



        private void frmConsole_Load(object sender, EventArgs e)
        {
            writer = new TextBoxStreamWriter(txtConsoleOut);
        }

        private void frmConsole_FormClosed(object sender, FormClosedEventArgs e)
        {
            writer.Close();
            writer.Dispose();
            writer = null;
        }
    }

    internal class TextBoxStreamWriter : TextWriter
    {//https://saezndaree.wordpress.com/2009/03/29/how-to-redirect-the-consoles-output-to-a-textbox-in-c/
        TextBox _output = null;

        public TextBoxStreamWriter(TextBox output)
        {
            _output = output;
            Console.SetOut(this);
        }

        private void tbAppendText(string s)
        {
            if (_output.InvokeRequired)
            {
                _output.Invoke(new Action<string>(tbAppendText), s);
                return;
            }
            _output.AppendText(s);
        }

        public override void Write(char value)
        {
            base.Write(value);
            //_output.AppendText(value.ToString()); // When character data is written, append it to the text box.
            if (!_output.IsDisposed)
            {
                tbAppendText(value.ToString());
            }
            else
            {
                //Console.Out.Close();
                this.Close();
                this.Dispose();
                Console.OpenStandardOutput();
            }
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }

}
