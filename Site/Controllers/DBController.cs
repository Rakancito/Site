using System;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Site.Controllers
{
    public class DBController : Controller
    {
        public static MySqlConnection ObtenerConexion()
        {
            MySqlConnection conectar = new MySqlConnection("server=IP; database=account; Uid=account; pwd=password;");
            try
            {
                conectar.Open();
                return conectar;
            }
            catch
            {
                return null;
            }
        }

        public static MySqlConnection PlayerObtenerConexion()
        {
            MySqlConnection conectar = new MySqlConnection("server=IP; database=player; Uid=account; pwd=password;");
            try
            {
                conectar.Open();
                return conectar;
            }
            catch
            {
                return null;
            }
        }

        public static int RegisterAccount(string sAccount, string sPassword, string sEmail, string sRemoveCode)
        {
            MySqlConnection conexion = ObtenerConexion();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conexion;
            int retorno = 0;
            DateTime dateTimeVariable = DateTime.Now;
            string sGetDateTime = dateTimeVariable.ToString("yyyy-MM-dd H:mm:ss");
            string sPwdMD5 = LibraryController.GenerateMySQL5PasswordHash(sPassword);
            cmd.CommandText = "SELECT * FROM account.account WHERE login = '" + sAccount + "'";
            retorno = Convert.ToInt32(cmd.ExecuteScalar());
            if (retorno >= 1)
            {
                return (retorno = 0);
            }
            MySqlCommand comando = new MySqlCommand(string.Format("INSERT INTO account (login, password, social_id, email) values ('{0}','{1}','{2}', '{3}')",
                sAccount, sPwdMD5, sRemoveCode, sEmail), conexion);

            retorno = comando.ExecuteNonQuery();
            conexion.Close();
            return retorno;

        }

        public static bool LoginAccount(string sAccount, string sPassword)
        {
            MySqlConnection conexion = ObtenerConexion();
            MySqlCommand cmd = new MySqlCommand();
            string sPwdMD5 = LibraryController.GenerateMySQL5PasswordHash(sPassword);
            cmd.Connection = conexion;
            try
            {
                cmd.CommandText = "SELECT id FROM account.account WHERE login = '" + sAccount + "' and password = '" + sPwdMD5 + "'";
                int valor = Convert.ToInt32(cmd.ExecuteScalar());
                if (valor >= 1)
                {
                    conexion.Close();
                    return true;
                }
                else
                {
                    conexion.Close();
                    return false;
                };
            }
            catch (Exception)
            {
                conexion.Close();
                return false;
            }
        }

        public static bool ChangePasswordByPasswordAndAccount(string sAccount, string sPassword, string sNewPassword)
        {
            int valor;
            string sPwdMD5 = LibraryController.GenerateMySQL5PasswordHash(sPassword);
            string sNewPwd = LibraryController.GenerateMySQL5PasswordHash(sNewPassword);
            MySqlConnection conexion = ObtenerConexion();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conexion;

            try
            {
                cmd.CommandText = "UPDATE account.account SET password= '"+ sNewPwd + "' WHERE login = '" + sAccount + "' and password = '" + sPwdMD5 + "' ";
                valor = Convert.ToInt32(cmd.ExecuteNonQuery());
                if (valor >= 1)
                {
                    conexion.Close();
                    return true;
                }
                else
                {
                    conexion.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                conexion.Close();
                return false;
            }
        }

        public static string GetEmailByAccount(string sAccount)
        {
            string NotFound = "No tiene Email";
            MySqlConnection conexion = ObtenerConexion();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conexion;
            cmd.CommandText = "SELECT email FROM account.account WHERE login = '" + sAccount + "'";
            MySqlDataReader valor = cmd.ExecuteReader();
            if (valor != null)
            {
                while (valor.Read())
                {
                    NotFound = valor.GetString(0);
                }
                conexion.Close();
                return NotFound.ToString();
            }
            else
            {
                conexion.Close();
                return NotFound = sAccount;
            }
        }
        public static string GetRemoveCodeByAccount(string sAccount)
        {
            MySqlConnection conexion = ObtenerConexion();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conexion;
            string valor = "No tiene Codigo";
            try
            {
                cmd.CommandText = "SELECT social_id FROM account.account WHERE login = '" + sAccount + "'";
                MySqlDataReader found = cmd.ExecuteReader();
                if (found != null)
                {
                    while (found.Read())
                    {
                        valor = found.GetString(0);
                    }
                    conexion.Close();
                    return valor;
                }
                else
                {
                    conexion.Close();
                    return valor;
                }
            }
            catch (Exception)
            {
                conexion.Close();
                return valor;
            }
        }

        public static string GetCoinsByAccount(string sAccount)
        {
            MySqlConnection conexion = ObtenerConexion();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conexion;
            string valor = "0";
            try
            {
                cmd.CommandText = "SELECT coins FROM account.account WHERE login = '" + sAccount + "'";
                MySqlDataReader found = cmd.ExecuteReader();
                if (found != null)
                {
                    while (found.Read())
                    {
                        valor = found.GetString(0);
                    }
                    conexion.Close();
                    return valor;
                }
                else
                {
                    conexion.Close();
                    return valor;
                }
            }
            catch (Exception)
            {
                conexion.Close();
                return valor;
            }
        }

        public static int GetOnlinePlayes()
        {
            MySqlConnection conexion = PlayerObtenerConexion();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conexion;
            int valor = 0;
            DateTime localTime = DateTime.Now.AddMinutes(-5);
            try
            {
                cmd.CommandText = "SELECT COUNT(*) as count FROM player.player WHERE '"+localTime+"' < last_play";
                valor = Convert.ToInt32(cmd.ExecuteScalar());
                return valor;
            }
            catch (Exception)
            {
                conexion.Close();
                return valor;
            }
        }

        public static MySqlDataReader GetTopPlayers()
        {
            MySqlConnection conexion = PlayerObtenerConexion();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conexion;
            try
            {
                MySqlDataReader dr;
                cmd.CommandText = "SELECT * FROM player.player WHERE name NOT LIKE '[%]%' order by level desc limit 100";
                try
                {
                    dr = cmd.ExecuteReader();
                    try
                    {
                        return dr;

                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
