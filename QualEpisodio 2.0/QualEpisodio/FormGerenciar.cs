using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace QualEpisodio
{
    public partial class FormGerenciar : Form
    {
        FormBase Base;
        public FormGerenciar(FormBase _base)
        {
            InitializeComponent();
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream myStream = myAssembly.GetManifestResourceStream("QualEpisodio.icon.ico");
            this.Icon = new Icon(myStream, 40, 40);
            Base = _base;
            ListarSeries();
        }

        public void ListarSeries()
        {
            lbSeries.Items.Clear();
            List<String> series = DBHelper.Instance.SelectAllSeries();
            foreach (String serie in series)
            {
                lbSeries.Items.Add(serie);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if(lbSeries.SelectedItems.Count == 1)
            {
               
                QualEpisodioModel serie = DBHelper.Instance.Select(false, (string)lbSeries.SelectedItem);
                new FormNova(this, Base, false, serie).Show();
     
            }
            else
            {
                MessageBox.Show("Você deve selecionar uma série!", "Qual episódio? - ERRO");
            }
        }

        private void btnNova_Click(object sender, EventArgs e)
        {
            new FormNova(this, Base, true, null).Show();
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (lbSeries.SelectedItems.Count == 1)
            {
                DialogResult dialogResult = MessageBox.Show("Tem certeza que deseja deletar?", "Qual episódio?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    DBHelper.Instance.Delete((string)lbSeries.SelectedItem);
                }
                else
                    MessageBox.Show("Você deve selecionar uma série!", "Qual episódio? - ERRO");

                ListarSeries();
                Base.SetContextMenu();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
