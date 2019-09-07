using KaObjects;
using System.Windows.Forms;

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
