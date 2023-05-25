using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace UC12_DIAGRAMA
{
    public partial class FormPESSOA : Form
    {
        string servidor;
        MySqlConnection conexao;
        MySqlCommand comando;
        string id_pessoa = "";
        string id_endereco = "";

        public FormPESSOA()
        {
            InitializeComponent();
            servidor = "Server=localhost;Database=pessoa_endereco;Uid=root;Pwd=";
            conexao = new MySqlConnection(servidor);
            comando = conexao.CreateCommand();

            atualizar_dataGRID();
        }

        private void atualizar_dataGRID()
        {
            try
            {
                conexao.Open();
                comando.CommandText = "SELECT tbl_pessoa.id, nome, sobrenome, nome_social, rg, cpf, data_nasc, etnia, genero, tbl_endereco.id logradouro, bairro, cidade, estado, uf FROM tbl_endereco INNER JOIN tbl_pessoa ON (tbl_pessoa.fk_endereco = tbl_endereco.id);";

                MySqlDataAdapter adaptadorTABELA = new MySqlDataAdapter(comando);

                DataTable tabelaTABELA = new DataTable();
                adaptadorTABELA.Fill(tabelaTABELA);

                dataGridViewTABELA.DataSource = tabelaTABELA;
                //dataGridViewTABELA.Columns["id"].HeaderText = "ID";
                //dataGridViewTABELA.Columns["nome"].HeaderText = "Nome";
                //dataGridViewTABELA.Columns["sobrenome"].HeaderText = "Sobrenome";
                //dataGridViewTABELA.Columns["nome_social"].HeaderText = "Nome social";
                //dataGridViewTABELA.Columns["rg"].HeaderText = "RG";
                //dataGridViewTABELA.Columns["cpf"].HeaderText = "CPF";
                //dataGridViewTABELA.Columns["data_nasc"].HeaderText = "Data de nascimento";
                //dataGridViewTABELA.Columns["etnia"].HeaderText = "Etnia";
                //dataGridViewTABELA.Columns["genero"].HeaderText = "Genero";
                //dataGridViewTABELA.Columns["id"].HeaderText = "ID1";
                //dataGridViewTABELA.Columns["logradouro"].HeaderText = "Logradouro";
                //dataGridViewTABELA.Columns["bairro"].HeaderText = "Bairro";
                //dataGridViewTABELA.Columns["cidade"].HeaderText = "Cidade";
                //dataGridViewTABELA.Columns["estado"].HeaderText = "Estado";
                //dataGridViewTABELA.Columns["uf"].HeaderText = "UF";

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
                MessageBox.Show("Erro ao abrir a lista, Fale com o Adiministrador do sistema");
            }
            finally
            {
                conexao.Close();
            }
        }

        private void buttonCADASTRAR_Click(object sender, EventArgs e)
        {
            string ultimo = "";

            try
            {
                conexao.Open();
                comando.CommandText = "INSERT INTO tbl_endereco(logradouro, bairro, estado, cidade, uf) VALUES ('" + textBoxLOGRADOURO.Text + "', '" + textBoxBAIRRO.Text + "', '" + comboBoxESTADO.Text + "', '" + textBoxCIDADE.Text + "', '" + comboBoxUF.Text + "');";
                comando.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                //MessageBox.Show(erro.Message);
                MessageBox.Show("Erro ao cadastrar, Fale com o Adiministrador do sistema");
            }
            finally
            {
                conexao.Close();
            }

            try
            {
                conexao.Open();
                comando.CommandText = "SELECT MAX(id) FROM tbl_endereco;";
                MySqlDataReader readerID = comando.ExecuteReader();

                if (readerID.Read())
                {
                    ultimo = readerID.GetString(0);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
                MessageBox.Show("Erro ao cadastrar, Fale com o Adiministrador do sistema");
            }
            finally
            {
                conexao.Close();
            }

            try
            {
                conexao.Open();

                if (radioButtonFEMININO.Checked)
                {
                    comando.CommandText = "INSERT INTO tbl_pessoa(nome, sobrenome, nome_social, rg, cpf, data_nasc, etnia, genero, fk_endereco) VALUES ('" + textBoxNOME.Text + "', '" + textBoxSOBRENOME.Text + "', '" + textBoxUSUARIO.Text + "', '" + textBoxRG.Text + "', '" + textBoxCPF.Text + "', '" + dateTimePickerDATANASC.Value.ToString("yyyy-mm-dd") + "', '" + comboBoxETNIA.Text + "', '" + radioButtonFEMININO.Text + "', '" + ultimo + "');";
                    comando.ExecuteNonQuery();
                    MessageBox.Show("cadastrado");
                }
                if (radioButtonMASCULINO.Checked)
                {
                    comando.CommandText = "INSERT INTO tbl_pessoa(nome, sobrenome, nome_social, rg, cpf, data_nasc, etnia, genero, fk_endereco) VALUES ('" + textBoxNOME.Text + "', '" + textBoxSOBRENOME.Text + "', '" + textBoxUSUARIO.Text + "', '" + textBoxRG.Text + "', '" + textBoxCPF.Text + "', '" + dateTimePickerDATANASC.Value.ToString("yyyy-mm-dd") + "', '" + comboBoxETNIA.Text + "', '" + radioButtonMASCULINO.Text + "', '" + ultimo + "');";
                    comando.ExecuteNonQuery();
                    MessageBox.Show("cadastrado");
                }
                if (radioButtonOUTROS.Checked)
                {
                    comando.CommandText = "INSERT INTO tbl_pessoa(nome, sobrenome, nome_social, rg, cpf, data_nasc, etnia, genero, fk_endereco) VALUES ('" + textBoxNOME.Text + "', '" + textBoxSOBRENOME.Text + "', '" + textBoxUSUARIO.Text + "', '" + textBoxRG.Text + "', '" + textBoxCPF.Text + "', '" + dateTimePickerDATANASC.Value.ToString("yyyy-mm-dd") + "', '" + comboBoxETNIA.Text + "', '" + radioButtonOUTROS.Text + "', '" + ultimo + "');";
                    comando.ExecuteNonQuery();
                    MessageBox.Show("cadastrado");
                }


            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
                MessageBox.Show("Erro ao cadastrar, Fale com o Adiministrador do sistema");
            }
            finally
            {
                conexao.Close();
            }
            atualizar_dataGRID();
        }

        private void dataGridViewTABELA_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id_pessoa = dataGridViewTABELA.CurrentRow.Cells[0].Value.ToString();
            id_endereco = dataGridViewTABELA.CurrentRow.Cells[9].Value.ToString();

            textBoxNOME.Text = dataGridViewTABELA.CurrentRow.Cells[1].Value.ToString();
            textBoxSOBRENOME.Text = dataGridViewTABELA.CurrentRow.Cells[2].Value.ToString();
            textBoxUSUARIO.Text = dataGridViewTABELA.CurrentRow.Cells[3].Value.ToString();
            textBoxRG.Text = dataGridViewTABELA.CurrentRow.Cells[4].Value.ToString();
            textBoxCPF.Text = dataGridViewTABELA.CurrentRow.Cells[5].Value.ToString();
            dateTimePickerDATANASC.Text = dataGridViewTABELA.CurrentRow.Cells[6].Value.ToString();
            comboBoxETNIA.Text = dataGridViewTABELA.CurrentRow.Cells[7].Value.ToString();
            textBoxLOGRADOURO.Text = dataGridViewTABELA.CurrentRow.Cells[10].Value.ToString();
            textBoxBAIRRO.Text = dataGridViewTABELA.CurrentRow.Cells[11].Value.ToString();
            textBoxCIDADE.Text = dataGridViewTABELA.CurrentRow.Cells[12].Value.ToString();
            comboBoxESTADO.Text = dataGridViewTABELA.CurrentRow.Cells[13].Value.ToString();
            comboBoxUF.Text = dataGridViewTABELA.CurrentRow.Cells[14].Value.ToString();

            if (dataGridViewTABELA.CurrentRow.Cells[8].Value.ToString() == "Masculino")
            {
                radioButtonMASCULINO.Checked = true;
            }
            if (dataGridViewTABELA.CurrentRow.Cells[8].Value.ToString() == "Feminino")
            {
                radioButtonFEMININO.Checked = true;
            }
            if (dataGridViewTABELA.CurrentRow.Cells[8].Value.ToString() == "Outros")
            {
                radioButtonOUTROS.Checked = true;
            }

        }

        private void buttonEXCLUIR_Click(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();
                comando.CommandText = "DELETE FROM tbl_pessoa WHERE id = " + id_pessoa + " AND DELETE FROM tbl_endereco WHERE id = " + id_endereco + ";";
                comando.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
                MessageBox.Show("Erro ao cadastrar, Fale com o Adiministrador do sistema");
            }
            finally
            {
                conexao.Close();
            }
            atualizar_dataGRID();
        }
    }
}
