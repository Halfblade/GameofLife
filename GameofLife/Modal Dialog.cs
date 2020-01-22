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
    public partial class Modal_Dialog : Form
    {
        public Modal_Dialog()
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


        public int NumberHeight 
        {

            get
            {
                return (int)numericUpDown2.Value;
            }



            set
            {
                numericUpDown2.Value = value;
            }
             
        
        }


        public int NumberWidth
        {

            get
            {
                return (int)numericUpDown3.Value;
            }



            set
            {
                numericUpDown3.Value = value;
            }


        }
    }
}
