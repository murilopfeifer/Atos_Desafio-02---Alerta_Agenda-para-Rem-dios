using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioRemedio
{
    class Banco
    {

        private string stringConexao = "Data Source=localhost; Initial Catalog=remedios;User ID=seujoao2; password=senha; language=Portuguese";

        private SqlConnection cn;

        private void conexao()// vincula a string com o c, tb inicia o cn (objeto sql conection)
        {
            cn = new SqlConnection(stringConexao);
        }

        public SqlConnection abrirConexao()
        {
            try //tentar fazer algo 
            {
                conexao();
                cn.Open();

                return cn;
            }
            catch (Exception ex) //faz algo se deu erro
            {
                return null;
            }
        }

        public void fecharConexao()
        {
            try
            {
                cn.Close();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public DataTable executarConsultaGenerica(string sql)
        {
            try
            {

                abrirConexao();

                SqlCommand command = new SqlCommand(sql, cn);
                command.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                adapter.Fill(dt); //adapter preenche o datable com os dados 

                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                fecharConexao();
            }

        }
























    }
}
