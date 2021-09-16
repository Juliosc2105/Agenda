using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agenda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btbAdicionar_Click(object sender, EventArgs e)
        {
            if ((txtNome.Text != "" ) && (txtFone.Text != "" ) && (cbxSexo.Text != "" ))
            {
                if (MessageBox.Show(
                    "Deseja incluir esses dados na lista?",
                    "Informação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    lstAgenda.Items.Add(txtNome.Text + ";" + txtFone.Text + ";" + cbxSexo.Text);
                    LimparCampos();
                }
            }
            else
            {
                MessageBox.Show(
                    "Preencha todos os campos",
                    "Informação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Deseja limpar todos os campos?",
                "Informação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LimparCampos();
            }
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtFone.Clear();
            cbxSexo.Text = "";
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (lstAgenda.SelectedIndex >= 0 )
            {
                if(MessageBox.Show(
                    "Deseja realmente excluir o item selecionado?",
                    "Informação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    lstAgenda.Items.RemoveAt(lstAgenda.SelectedIndex);
                }
            }
            else
            {
                MessageBox.Show(
                    "Selecione um registro na lista",
                    "Informação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
