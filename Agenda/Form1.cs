using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace Agenda
{
    public partial class Form1 : Form
    {
        int indiceSelecionado; //Usado para atualizar um registro


        public Form1()
        {
            InitializeComponent();
        }
        
        private void LimparCampos()
        {
            txtNome.Clear();
            txtFone.Clear();
            cbxSexo.Text = "";
            txtNome.Focus();
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
                    btnAtualizar.Text = "Atualizar";    
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

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (btnAtualizar.Text == "Atualizar")
            {
                if (lstAgenda.SelectedIndex >= 0)
                {
                    indiceSelecionado = lstAgenda.SelectedIndex;
                    string[] campo = lstAgenda.SelectedItem.ToString().Split(';');

                    txtNome.Text = campo[0];
                    txtFone.Text = campo[1];
                    cbxSexo.Text = campo[2];

                    btnAtualizar.Text = "Salvar";
                    btnNovo.Enabled = false;
                    btbAdicionar.Enabled = false;
                    btnExcluir.Enabled = false;
                    
                }
                else
                {
                    MessageBox.Show(
                        "Selecione um registro na lista",
                        "Informação",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }else if (btnAtualizar.Text == "Salvar")
            {
                if (MessageBox.Show(
                    "Deseja Atualizar esse registro?",
                    "Informação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    btnAtualizar.Text = "Atualizar";
                    btnNovo.Enabled = true; 
                    btbAdicionar.Enabled = true;
                    btnExcluir.Enabled = true;

                    lstAgenda.Items.RemoveAt(indiceSelecionado);
                    lstAgenda.Items.Insert(indiceSelecionado, txtNome.Text+";"+txtFone.Text+";"+cbxSexo.Text);
                    indiceSelecionado = -1;
                    LimparCampos();
                }else
                {
                    txtNome.Focus();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FileInfo salvarArquivo = new FileInfo(@".\agenda.txt");
            if (!salvarArquivo.Exists)
            {
                using (StreamWriter sa = salvarArquivo.CreateText())
                {
                    for(int i = 0; i < lstAgenda.Items.Count; i++)
                    {
                        sa.WriteLine(lstAgenda.Items[i].ToString());
                    }
                }
            }
            else
            {
                if (MessageBox.Show(
                    "O arquivo " + salvarArquivo+" foi encontrado. Substituir?",
                    "Informação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (StreamWriter sa = salvarArquivo.CreateText())
                    {
                        for (int i = 0; i < lstAgenda.Items.Count; i++)
                        {
                            sa.WriteLine(lstAgenda.Items[i].ToString());
                        }
                    }
                }
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            FileInfo abrirArquivo = new FileInfo(@".\agenda.txt");
            if (abrirArquivo.Exists)
            {
                using (StreamReader aa = abrirArquivo.OpenText())
                {
                    lstAgenda.Items.Clear();

                    string linha = "";
                    while ((linha = aa.ReadLine()) != null)
                    {
                        lstAgenda.Items.Add(linha.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "O arquivo" + abrirArquivo + " não foi encontrado",
                    "Informação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnImportar.PerformClick();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                    "Desseja limpar sua agenda?",
                    "Informação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information) == DialogResult.Yes)
            {
                lstAgenda.Items.Clear();
            } 
            else
            {
                MessageBox.Show("Processo de limpeza cancelado",
                    "Informação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
