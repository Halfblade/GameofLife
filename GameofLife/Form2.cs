using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameofLife
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        public int Number
        {
            get
            {
                return (int)numericUpDown1.Value;
            }

            set
            {
                numericUpDown1.Value = value;
            }
        }
    }
}
