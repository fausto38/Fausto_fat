using MySql.Data.MySqlClient;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fausto_fat
{
    public partial class Serviços : Form
    {
        string servidor;
        MySqlConnection conexao;
        MySqlCommand comando;
        string idREGISTRO;
        string op = "";

        public Serviços()
        {
            InitializeComponent();

            servidor = "Server=localhost;Database=fausto_fato_tii;Uid=root;Pwd=";
            conexao = new MySqlConnection(servidor);
            comando = conexao.CreateCommand();

            ATUALIZAR_CADASTRO();
        }


        private void Limpar_Cadastro()
        {
            textBoxNOME.Clear();
            textBoxSOBRENOME.Clear();
            textBoxTELEFONE.Clear();
            textBoxCPF.Clear();
            textBoxCELULAR.Clear();
            textBoxEMAIL.Clear();
            textBoxDATAENTRADA.Clear();
            textBoxCARACTERISTICASEQUIPAMENTOS.Clear();
            textBoxOBSERVACAO.Clear();
            textBoxQUEIXACLIENTE.Clear();
            textBoxVALORTOTAL.Clear();
            radioButtonATIVO.Checked = false;
            radioButtonANDAMENTO.Checked = false;
            radioButtonCONCLUIDO.Checked = false;
            radioButtonCANCELADO.Checked = false;
        }

        private void ATUALIZAR_CADASTRO()
        {
            try
            {
                conexao.Open();
                comando.CommandText = "SELECT * FROM tbl_cliente;";
                MySqlDataAdapter adaptadorCADASTRO = new MySqlDataAdapter(comando);
                DataTable tabelaCADASTRO = new DataTable();
                adaptadorCADASTRO.Fill(tabelaCADASTRO);
            }
            catch (Exception erro_mysql)
            {
                MessageBox.Show(erro_mysql.Message);
            }
            finally
            {
                conexao.Close();
            }



        }

        private void textBoxCARACTERISTICASEQUIPAMENTOS_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void buttonCADASTRAR_Click(object sender, EventArgs e)
        {
            labelNOME.ForeColor = Color.Black;

            if (radioButtonATIVO.Checked)
            {
                op = "Opção ANDAMENTO";
            }
            if (radioButtonANDAMENTO.Checked)
            {
                op = "Opção CONCLUIDO";
            }
            

            if (radioButtonCONCLUIDO.Checked)
            {
                op = "Opção CANCELADO";
            }
            if (radioButtonCANCELADO.Checked)
            {
                op = "Opção ATIVO";
            }
           

            try
            {

                if (textBoxNOME.Text != "" && textBoxEMAIL.Text != "")
                {
                    conexao.Open();
                    comando.CommandText = "INSERT INTO tbl_cliente(nome, sobrenome, telefone, cpf, celular, email, data_entrada, queixa_cliente, valor_total, observacoes, caracteristicas_equipamentos) VALUES ('" + textBoxNOME.Text + "', '" + textBoxSOBRENOME.Text + "', '" + textBoxTELEFONE.Text + "', '" + textBoxCPF.Text + "', '" + textBoxCELULAR.Text + "', '" + textBoxEMAIL.Text + "', '" + textBoxDATAENTRADA.Text + "' , '" + textBoxQUEIXACLIENTE + "' , '" + textBoxVALORTOTAL + "' ,'" + textBoxOBSERVACAO + "' , '" + textBoxCARACTERISTICASEQUIPAMENTOS + "' );";
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Cadastro realizado com SUCESSO!");
                    Limpar_Cadastro();
                }

                else
                    {
                    MessageBox.Show("Nome e/ou SOBRENOME estão em BRANCO! Por favor preencha!");
                   
                    if (textBoxNOME.Text == "")
                    {
                        textBoxNOME.Focus();
                        labelNOME.ForeColor = Color.Red;
                    }
                    else
                    {
                        textBoxEMAIL.Focus();
                        labelEMAIL.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception erro_mysql)
            {
                MessageBox.Show(erro_mysql.Message);
            }
        }

        private void buttonEXCLUIR_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente EXCLUIR este registro?", "ATENÇÃO!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    conexao.Open();
                    comando.CommandText = "DELETE FROM tbl_cadastro WHERE id = " + idREGISTRO + ";";
                    int resultado = comando.ExecuteNonQuery();
                    if (resultado > 0)
                    {
                        MessageBox.Show("Informação(s) removido(s) com sucesso! - " + resultado + "registros removidos...");
                    }
                    else
                    {
                        MessageBox.Show("Cadastro não encontrado!");
                    }
                }
                catch (Exception erro_mysql)
                {
                    MessageBox.Show("Erro de Sistema. Solicite ajuda!");
                }
                finally
                {
                    conexao.Close();
                }
                ATUALIZAR_CADASTRO();
            }
            
        }

        private void buttonALTERAR_Click(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();
                comando.CommandText = "UPDATE tbl_cadastro SET nome_dono = '" + textBoxNOME.Text + "', nome = '" + textBoxSOBRENOME.Text + "', sobrenome =  '" + textBoxTELEFONE.Text + "', telefone = '" + textBoxCPF.Text + "', cpf = '" + textBoxCELULAR.Text + "', celular = '" + textBoxEMAIL.Text + "', email  = '" + textBoxDATAENTRADA.Text + "','" + textBoxQUEIXACLIENTE+"','"+textBoxVALORTOTAL+"','"+textBoxOBSERVACAO+"','"+textBoxCARACTERISTICASEQUIPAMENTOS+"'  WHERE id = " + idREGISTRO + ";";
                int resultado = comando.ExecuteNonQuery();
                if (resultado > 0)
                {
                    MessageBox.Show("Cadastro(s) atualizado(s) com sucesso! - " + resultado + " registros atualizados...");
                }
                else
                {
                    MessageBox.Show("Cadastro não encontrado!");
                }
            }
            catch (Exception erro_mysql)
            {
                MessageBox.Show("Erro de Sistema. Solicite ajuda!");
            }
            finally
            {
                conexao.Close();
            }
            ATUALIZAR_CADASTRO();
            
        }
    }
}
