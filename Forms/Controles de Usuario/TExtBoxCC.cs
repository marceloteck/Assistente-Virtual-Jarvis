using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InteligenciaArtificial.Forms.Controles_de_Usuario
{
    public partial class TExtBoxCC : TextBox
    {
        public TExtBoxCC()
        {
            InitializeComponent();


            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            TextChanged += new EventHandler(textBox_TextChanged);
            BackColor = Color.Transparent;
            ForeColor = Color.White;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            BackColor = Color.Transparent;
        }
    }
}
