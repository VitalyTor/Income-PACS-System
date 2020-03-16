using System;
using Microsoft.Win32;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;


namespace InCome
{
	/// <summary>
	/// Версия SQL-сервера
	/// </summary>
	public enum SqlVer{UNKNOWN=0,MSSQL2000=2000,MSSQL2005=2005};

	/// <summary>
	/// Класс предназначен для работы с базой данных
	/// </summary>
	public class CDbCreator
	{
		/// <summary>
		/// Строка соединения с базой
		/// </summary>
		public string conStr="";
		/// <summary>
		/// Версия SQL-сервера
		/// </summary>
		public SqlVer sqlVer=SqlVer.UNKNOWN;
       
		/// <summary>
		/// Возвращает название базы данных из строки соединенения
		/// </summary>
		/// <param name="conStr">Строка соединения с базой</param>
		/// <returns>Название</returns>
		public static string getDBCon(string conStr)
		{
			int p1=conStr.IndexOf("Initial Catalog=")+16,p2=conStr.IndexOf("Data Source=",p1);
			return conStr.Substring(p1,p2-p1-1);			
		}
		/// <summary>
		/// Возвращает название SQL-сервера из строки соединенения
		/// </summary>
		/// <param name="conStr">Строка соединения с базой</param>
		/// <returns>Название</returns>		
		public static string getServerCon(string conStr)
		{
			int p1=conStr.IndexOf("Data Source=")+12;
			return conStr.Substring(p1);			
		}
		/// <summary>
		/// Возвращает имя пользователя из строки соединенения
		/// </summary>
		/// <param name="conStr">Строка соединения с базой</param>
		/// <returns>Имя</returns>
		public static string getUserCon(string conStr)
		{
			int p1=conStr.IndexOf("User ID=")+8,p2=conStr.IndexOf("Initial Catalog=",p1);
			return conStr.Substring(p1,p2-p1-1);			
		}
		/// <summary>
		/// Возвращает пароль из строки соединенения
		/// </summary>
		/// <param name="conStr">Строка соединения с базой</param>
		/// <returns>Пароль</returns>		
		public static string getPasswCon(string conStr)
		{
			int p1=conStr.IndexOf("Password=")+9,p2=conStr.IndexOf("Persist Security Info=",p1);
			return conStr.Substring(p1,p2-p1-1);
		}
		/// <summary>
		/// Конструктор класса
		/// </summary>
		/// <param name="key">Путь к ключу в реестре</param>
		/// <remarks>Считывает из реестра строку соединения с базой и версию SQL-сервера</remarks>
		public CDbCreator(string key)
		{
			//пытаемся прочитать строку соединения с базой и версию MS SQL Server из реестра
			try
			{
				RegistryKey rk=Registry.LocalMachine.OpenSubKey(key);
				if(rk!=null) 
				{
					conStr=rk.GetValue("connection").ToString();
					switch(Convert.ToInt32(rk.GetValue("sqlver").ToString())) 
					{
						case (int)SqlVer.MSSQL2000: sqlVer=SqlVer.MSSQL2000;
							break;
						case (int)SqlVer.MSSQL2005: sqlVer=SqlVer.MSSQL2005;
							break;
						default: sqlVer=SqlVer.UNKNOWN;
							break;
					}			
				}
			}
			catch(Exception e)
			{
				MessageBox.Show("Ошибка при чтение из реестра настроек соединения с базой данных! " + e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				conStr="";
				sqlVer=SqlVer.UNKNOWN;
			}
		}
		/// <summary>
		/// Задаёт строку соединения с базой и определяет версию SQL-сервера
		/// </summary>
		/// <returns>true - выполнена успешно; false - произошла ошибка</returns>
		/// <remarks>На машине пользователя должны быть установлены "Microsoft ActiveX Data... 2.7" и "Microsoft OLEDB 1.0 Service..." </remarks>
		public bool setConStr()
		{
			sqlVer=SqlVer.UNKNOWN;
			conStr="";
			try
			{
				//нужно подключить Microsoft ActiveX Data... 2.7 и Microsoft OLEDB 1.0 Service..."
				MSDASC.DataLinks conDlg = new MSDASC.DataLinks();
				ADODB._Connection adoCon = (ADODB._Connection) conDlg.PromptNew();
				if(adoCon==null) return false;
				conStr=adoCon.ConnectionString;
				//выясняем какая версия MS SQL Server используется
				if(conStr.IndexOf("SQLOLEDB")>=0) sqlVer=SqlVer.MSSQL2000;
				if(conStr.IndexOf("SQLNCLI")>=0) sqlVer=SqlVer.MSSQL2005;
				if(sqlVer==SqlVer.UNKNOWN) return false;
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				return false;
			}
			
			return true;
		}
		/// <summary>
		/// Устанавливает значение ключа в реестре
		/// </summary>
		/// <param name="key">Путь к ключу</param>
		/// <param name="name">Имя параметра</param>
		/// <param name="val">Значение параметра</param>
		/// <returns>true - выполнена успешно; false - возникла ошибка</returns>
		public bool setRegValue(string key,string name,string val)
		{
			try
			{
				RegistryKey rk=Registry.LocalMachine.OpenSubKey(key,true);
				if(rk==null)
					rk=Registry.LocalMachine.CreateSubKey(key);	
				rk.SetValue(name,val);	
				rk.Close();				
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);				
				return false;
			}
			
			return true;
		}
		/// <summary>
		/// Считывает из реестра значения
		/// </summary>
		/// <param name="key">Путь к ключу</param>
		/// <param name="name">Имя параметра</param>
		/// <param name="val">Значение параметра</param>
		/// <returns>true - выполнена успешно; false - возникла ошибка</returns>
		public bool getRegValue(string key,string name,ref string val)
		{
			object v;

			try
			{
				RegistryKey rk=Registry.LocalMachine.OpenSubKey(key);
				if(rk!=null) 
				{
					v=rk.GetValue(name);
					if(v==null) return false;
					else
						val=v.ToString();
					rk.Close();
				}
				else 
					return false;
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);				
				return false;
			}

			return true;
		}
		/// <summary>
		/// Проверяет, есть ли указанная база данных
		/// </summary>
		/// <param name="conStr">Строка соединения с базой master (либо какой-либо другой базой) соответствующего SQL-сервера</param>
		/// <param name="sqlVer">Версия SQL-сервера</param>
		/// <param name="dbname">Имя базы данных</param>
		/// <returns>0 - базы нет; 1 - база есть; -1 - произошла ошибка при проверке </returns>
		public int isDBase(string conStr,SqlVer sqlVer,string dbname)
		{
			try
			{
				if(conStr=="") return -1;
				OleDbConnection sqlCon=new OleDbConnection(conStr);
				sqlCon.Open();
						
				string sql="";
				if(sqlVer==SqlVer.MSSQL2000) sql="SELECT count(*) FROM master.dbo.sysdatabases WHERE name = 'InCome'";
				if(sqlVer==SqlVer.MSSQL2005) sql="SELECT count(*) FROM master.sys.databases WHERE name = 'InCome'";
				if(sql=="") return -1;

				OleDbCommand sqlCom=new OleDbCommand(sql,sqlCon);
				sqlCom.CommandTimeout=60;
			
				int cnt=Convert.ToInt32(sqlCom.ExecuteScalar());
				sqlCon.Close();
				if(cnt>0) return 1;
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				return -1;
			}			

			return 0;
		}
		/// <summary>
		/// Удаляет базу данных
		/// </summary>
		/// <returns>true - выполнена успешно; false - возникла ошибка</returns>
		public bool dropDB()
		{
			if(conStr=="") return false;

			try
			{
				OleDbConnection sqlCon=new OleDbConnection(conStr);
				sqlCon.Open();
				OleDbCommand sqlCom=new OleDbCommand();
				sqlCom.CommandTimeout=60;
				sqlCom.Connection=sqlCon;
				sqlCom.CommandText="use Master; IF ((SELECT count(*) FROM sysdatabases WHERE name = N'InCome')>0) BEGIN ";
				sqlCom.CommandText=sqlCom.CommandText+"	declare @path varchar(200) set @path='c:\\InCome_'+CONVERT(varchar,getdate(),5)+'.bak' EXEC sp_addumpdevice 'disk', 'bacInCome', @path BACKUP DATABASE InCome TO bacInCome WITH DESCRIPTION= 'backup of InCome', NAME='InCome_backup' EXEC sp_dropdevice 'bacInCome' ";
				sqlCom.CommandText=sqlCom.CommandText+" DROP DATABASE InCome  END; ";
				sqlCom.CommandText=sqlCom.CommandText+" if(select count(*) from master.dbo.syslogins where name='InComeUser')>0 begin exec sp_droplogin 'InComeUser' end;";
				sqlCom.ExecuteNonQuery();
				sqlCon.Close();
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				return false;
			}

			return true;
		}
		/// <summary>
		/// Созадёт базу данных
		/// </summary>
		/// <returns>true - выполнена успешно; false - возникла ошибка</returns>
		public bool createDB()
		{			
			try
			{
				if((conStr=="") | ((sqlVer!=SqlVer.MSSQL2000) & (sqlVer!=SqlVer.MSSQL2005))) return false;
			
				string sql="";
				TextReader sqlReader;			
				switch ((int)sqlVer)
				{
					case (int)SqlVer.MSSQL2000: 
						sqlReader=new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory+"CreateDB_2000.sql");
						sql=sqlReader.ReadToEnd();
						sqlReader.Close();
						break;
					case (int)SqlVer.MSSQL2005: 
						sqlReader=new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory+"CreateDB_2005.sql");
						sql=sqlReader.ReadToEnd();
						sqlReader.Close();						
						break;
					default: sql="";
						break;
				}
				if(sql=="") return false;	
              
				OleDbConnection sqlCon=new OleDbConnection(conStr);
				sqlCon.Open();
                string [] sqlbatch=null; //последовательность запросов
				sqlbatch=sql.Split('#');

				OleDbCommand sqlCom=new OleDbCommand();
				sqlCom.CommandTimeout=300;
				sqlCom.Connection=sqlCon;
				//выполняем последовательность запросов
				for(int i=0;i<sqlbatch.Length;i++)
				{	
					sqlCom.CommandText=sqlbatch[i];
					sqlCom.ExecuteNonQuery();				
				}				
				sqlCon.Close();
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				return false;
			}

			return true;
		}
		/// <summary>
		/// Изменяет пароль администратора
		/// </summary>
		/// <param name="oldpassword">Старый пароль</param>
		/// <param name="newpassword">Новый пароль</param>
		/// <param name="login">Логин администратора ("InComeUser")</param>
		/// <returns>true - выполнена успешно; false - возникла ошибка</returns>
		public bool SetPassword(string oldpassword,string newpassword,string login)
		{
			string sql="";
			string oldConStr=conStr;
			login=login.Replace("'","''");
			oldpassword=oldpassword.Replace("'","''");
			newpassword=newpassword.Replace("'","''");
			
			try
			{
				sql="exec sp_password '" +oldpassword+"','"+newpassword+"','"+login+"'";
     			//в базе
				OleDbConnection sqlCon=new OleDbConnection(conStr);
				sqlCon.Open();
				OleDbCommand sqlCom=new OleDbCommand(sql,sqlCon);
				sqlCom.CommandTimeout=60;
				sqlCom.ExecuteNonQuery();												
				sqlCon.Close();
				//в строке соединения				
				int p1=conStr.IndexOf("Password=");
				int p2=conStr.IndexOf(";",p1);
				conStr=conStr.Replace(conStr.Substring(p1,p2-p1),"Password="+newpassword);				
			}
			catch(Exception e)
			{				
				MessageBox.Show(e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				conStr=oldConStr;
				return false;				
			}

			return true;
		}
	}
}
