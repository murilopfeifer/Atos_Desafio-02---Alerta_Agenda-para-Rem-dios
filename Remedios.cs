using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioRemedio
{
    class Remedios
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string horario { get; set; }



        public bool adicionaRemedio()
        {
            Banco banco = new Banco();

            SqlConnection cn = banco.abrirConexao();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand command = new SqlCommand();

            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = System.Data.CommandType.Text;

            command.CommandText = "INSERT INTO agenda VALUES (@nome, @horario);";
            command.Parameters.Add("@nome", System.Data.SqlDbType.VarChar);
            command.Parameters.Add("@horario", System.Data.SqlDbType.VarChar);
            command.Parameters[0].Value = nome;
            command.Parameters[1].Value = horario;
            

            try
            {
                command.ExecuteNonQuery();
                tran.Commit();
                return true;
            }

            catch (Exception ex)
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                banco.fecharConexao();
            }

        }
    }
}
