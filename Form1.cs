using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesafioRemedio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        readonly SoundPlayer soundPlayer = new SoundPlayer();


        private void btn_resultados_Click(object sender, EventArgs e)
        {
            Banco bd = new Banco();

            string sql = "select nome, horario from agenda";

            DataTable dt = new DataTable();

            dt = bd.executarConsultaGenerica(sql);

            dataGridView1.DataSource = dt;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbl_HraAtual.Text = DateTime.Now.ToString("dd/MM/yy HH:mm");

            Banco bd = new Banco();
            int hora = DateTime.Now.Hour;
            int minuto = DateTime.Now.Minute;

            try
            {
                SqlConnection cn = bd.abrirConexao();
                SqlCommand command = new SqlCommand("select * from agenda", cn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string[] t = reader.GetString(2).Split(':');
                    int h = int.Parse(t[0]);
                    int m = int.Parse(t[1]);

                    if (h == hora && m == minuto)
                    {
                        soundPlayer.SoundLocation = "C:\\Users\\PC_01\\source\\repos\\DesafioRemedio\\DesafioRemedio\\Resources\\Alarm.wav";
                        soundPlayer.PlayLooping();
                        MessageBox.Show("Esta na hora de tomar: " + reader.GetString(1));
                        soundPlayer.Stop();
                    }

                }

            }
            catch (Exception ex)
            {

                return;
            }
            finally
            {
                bd.fecharConexao();
            }




        }





        private void adicionaRemedio_Click(object sender, EventArgs e)
        {
            Remedios r = new Remedios();
            r.nome = txtNome.Text;
            r.horario = txtHorario.Text;

            
            bool retorno = r.adicionaRemedio();
            if (retorno)
            {
                MessageBox.Show("Agendamento Concluído");
            }
            else
            {
                MessageBox.Show("Falha no registro");
            }

            txtNome.Text = "";
            txtHorario.Text = "";


            Banco bd = new Banco();

            string sql = "select nome, horario from agenda";

            DataTable dt = new DataTable();

            dt = bd.executarConsultaGenerica(sql);

            dataGridView1.DataSource = dt;

            txtNome.Text = "";
            txtHorario.Text = "";


        }


        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Banco bd = new Banco();
            string sql = $"delete from agenda where horario = '{txtHorario.Text}' and nome = '{txtNome.Text}'";
            DataTable dt = new DataTable();
            dt = bd.executarConsultaGenerica(sql);
            dataGridView1.DataSource = dt;
            MessageBox.Show("Agendamento Excluído");

           
            string sql2 = "select nome, horario from agenda";
            DataTable dt2 = new DataTable();
            dt2 = bd.executarConsultaGenerica(sql2);
            dataGridView1.DataSource = dt2;

            txtNome.Text = "";
            txtHorario.Text = "";
        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
            {
                try
                {

                    txtNome.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtHorario.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
        }







    }
}
