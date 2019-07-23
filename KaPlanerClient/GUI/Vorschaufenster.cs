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
        public Vorschaufenster(KaEvent kaEvent)
        {
            InitializeComponent();
            TB_Title.Text = kaEvent.Titel;
            TB_Place.Text = kaEvent.Ort;
            tB_Beginn.Text = kaEvent.Beginn.ToString();
            tb_Ende.Text = kaEvent.Ende.ToString();
            rTB_Beschreibung.Text = kaEvent.Beschreibung;
            tb_Verantwortlicher.Text = kaEvent.owner.name;
        }

    }
}
