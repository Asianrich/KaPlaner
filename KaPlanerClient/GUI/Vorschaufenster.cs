using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using KaPlaner.Logic;
using KaObjects;

namespace KaPlaner.GUI
{
    public partial class Vorschaufenster : Form
    {
        public Vorschaufenster()
        {
            InitializeComponent();
            rTB_Terminvorschau.Text = "test";
        }
    }
}
