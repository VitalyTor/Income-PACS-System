using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
//using System.Data.OleDb;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;


namespace InCome
{
	/// <summary>
	/// Форма для редактирования данных о сотрудниках
	/// </summary>
	/// <remarks>Позволяет добавлять, изменять и удалять сотрудников.</remarks>
	public class fmEmpl : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel pnData;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel pnEmplInfo;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox cbLocked;
		private System.Windows.Forms.CheckBox cbUsePassword;
		private System.Windows.Forms.TextBox tbPassword;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox tbLogin;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Button btnNewGroup;
		private System.Windows.Forms.Button btnNewDepart;
		private System.Windows.Forms.ComboBox cbGroup;
		private System.Windows.Forms.ComboBox cbDepart;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox tbEmail;
		private System.Windows.Forms.TextBox tbMobphone;
		private System.Windows.Forms.TextBox tbPhone;
		private System.Windows.Forms.TextBox tbAddress;
		private System.Windows.Forms.TextBox tbSettlement;
		private System.Windows.Forms.TextBox tbDistrict;
		private System.Windows.Forms.TextBox tbRegion;
		private System.Windows.Forms.TextBox tbCountry;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox tbDocType;
		private System.Windows.Forms.TextBox tbDoc;
		private System.Windows.Forms.ComboBox cbMale;
		private System.Windows.Forms.TextBox tbSecname;
		private System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.TextBox tbSurname;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
        /// <summary>
        /// Строка соединения с базой
        /// </summary>
		private string conStr;
		private System.Windows.Forms.TreeView tvEmpl;
		private System.Windows.Forms.ContextMenu cmTree;
		private System.Windows.Forms.MenuItem miDepart;
		private System.Windows.Forms.MenuItem miGroup;
		private System.Windows.Forms.GroupBox gbPassword;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem miAdd;
		private System.Windows.Forms.MenuItem miSave;
		private System.Windows.Forms.MenuItem miDel;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button btnNewCode;
		private System.Windows.Forms.TextBox tbCode;
		private System.Windows.Forms.Button btnToQueue;
		private System.Windows.Forms.Button btnDelQueue;
		private System.Windows.Forms.Button btnPrintQueue;
		private System.Windows.Forms.DateTimePicker dtpBirth;
		private System.Windows.Forms.DateTimePicker dtpEmplDate;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem miSearch;
		private System.Windows.Forms.PictureBox pbPhoto;
		private System.Windows.Forms.OpenFileDialog ofdPhoto;
		private System.Windows.Forms.Label lbPhoto;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnDelEmpl;
        private Button btnEdit;
		private System.ComponentModel.Container components = null;
		
		/// <summary>
		/// Конструктор формы
		/// </summary>
		/// <remarks>Задаёт строку соединения с базой</remarks>
		/// <param name="fconStr">Строка соединения с базой</param>
		public fmEmpl(string fconStr)
		{
			
			InitializeComponent();
		    conStr=fconStr;
		}	
		/// <summary>
		/// Добавляет информацию о сотруднике в "очередь печати" карточек со штрих-кодом либо только определяет количество записей в очереди
		/// </summary>
		/// <param name="surname">Фамилия</param>
		/// <param name="name">Имя</param>
		/// <param name="secname">Отчество</param>
		/// <param name="code">Штрих-код</param>
		/// <param name="department">Отдел</param>
		/// <param name="group">Группа</param>
		/// <param name="add">Флаг (true - добавить в очередь и вернуть количество в "очереди";false - только вернуть количество в "очереди")</param>
		/// <returns>Количество записей в "очереди"</returns>
		public int addToQueue(int id,string code,bool add)
		{
			object res;

			try
			{
                SqlConnection sqlCon = new SqlConnection();
                SqlCommand sqlCom = new SqlCommand();
                //SqlConnection sqlCon=new SqlConnection(conStr);
                //SqlCommand sqlCom=new SqlCommand();
                sqlCom.CommandTimeout=60;
				sqlCom.Connection=sqlCon;
    			sqlCon.Open();
				if(add)
				{
					sqlCom.CommandText="insert into CodeToPrint (FIO,CODE,CODEPRINT,DEPARTMENT,GRUPA,PHOTO) select MSURNAME+' '+ISNULL(left(MNAME,1)+'. ','')+' '+ISNULL(left(MSECNAME,1)+'. ',''),MCODE,?,MDEPARTMENT,MGROUP,MPHOTO from EmplMain where mid=? ";
					sqlCom.Parameters.Add(new SqlParameter("@codeprint",CEncode128.Encode128(code)));
					sqlCom.Parameters.Add(new SqlParameter("@id",id));					
				}
				sqlCom.CommandText+="select count(*) from CodeToPrint";
				
				sqlCom.CommandTimeout=60;
                res=sqlCom.ExecuteScalar();				
				sqlCon.Close();
			}
			catch(Exception e)
			{
				MessageBox.Show("Не удалось добавить работника в очередь на печать! "+e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);				
				return -1;
			}


			return Convert.ToInt32(res);
		}
		/// <summary>
		/// Очищает "очередь печати" карточек со штрих-кодом
		/// </summary>
		/// <returns>true - выполнена успешно; false - возникла ошибка</returns>
		public bool delQueue()
		{
			try
			{
				SqlConnection sqlCon=new SqlConnection(conStr);
				sqlCon.Open();				
				SqlCommand sqlCom=new SqlCommand("delete from CodeToPrint",sqlCon);
				sqlCom.CommandTimeout=60;
				sqlCom.ExecuteNonQuery();
				sqlCon.Close();
			}
			catch(Exception e)
			{
				MessageBox.Show("Не удалось очистить очередь печати! "+e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);   
				return false;
			}

			return true;
		}

		/// <summary>
		/// Возвращает контрольное число
		/// </summary>
		/// <param name="s">Строка с цифровой последовательностью</param>
		/// <returns>Двухсимвольное контрольное число</returns>
		public static string getKontrSum(string s)
		{
            string k="",s1="",s2="";	
			for(int i=0;i<s.Length;i+=2)
			{
				s1=s1+s[i];
				if(i<s.Length-1)
					s2=s2+s[i+1];				
			}

			//s1
			int c1=0,c2=0;
			for(int j=0;j<s1.Length;j+=2)
			{
				c1=c1+Convert.ToInt32(s1.Substring(j,1));
                if(j<s1.Length-1)
					c2=c2+Convert.ToInt32(s1.Substring(j+1,1));
			}
			c1=c1+3*c2;
			c1=10-(c1 % 10);
			c1=(c1==10)?0:c1;
			k=c1.ToString();
			//s2
			c1=0; c2=0;
			for(int j=0;j<s2.Length;j+=2)
			{
				c1=c1+Convert.ToInt32(s2.Substring(j,1));
				if(j<s2.Length-1)
					c2=c2+Convert.ToInt32(s2.Substring(j+1,1));
			}
			c1=c1+3*c2;
			c1=10-(c1 % 10);
			c1=(c1==10)?0:c1;
			k=k+c1.ToString();

			return k;
		}
		/// <summary>
		/// Проверяет, имеет ли штрих-код сотрудника правльное контрольное число
		/// </summary>
		/// <param name="s">Штрих-код сотрудника</param>
		/// <returns>true - контрольное число правильное; false - контрольное число неправильное</returns>
		public static bool isKontrSumRight(string s)
		{
			if(getKontrSum(s.Substring(0,s.Length-2))==s.Substring(s.Length-2))
				return true;

            return false;
		}
		
		/// <summary>
		/// Формирует новый штрих-код для сотрудника
		/// </summary>
		/// <param name="fio">Фамилия Имя Отчество сотрудника</param>
		/// <returns>Новый штрих-код</returns>
		/// <remarks>Штрих-код формируется из следующих частей: "1"[1 символ]+хэш ФИО[2 символа]+год[4 символа]+день в году[3 символа]+секунда в дне[5 символов]+контрольное число[2 символа]</remarks>
		public static string getNewCode(string fio)
		{
			DateTime dt=DateTime.Now;
			int hash=Math.Abs(fio.GetHashCode() % 100),year=dt.Year,day=dt.DayOfYear,sec=dt.Second+dt.Hour*3600+dt.Minute*60,msec=dt.Millisecond;
			string s1=hash.ToString().PadLeft(2,'0'),s2=year.ToString().PadLeft(3,'0'),s3=day.ToString().PadLeft(3,'0'),s4=sec.ToString().PadLeft(5,'0');
			
            string s="1"+s1+s2+s3+s4; 
			
			return (s+getKontrSum(s));
		}
			
		/// <summary>
		/// Возвращает ФИО и идентификатор сотрудников, удовлетворяющих некоторому критерию
		/// </summary>
		/// <param name="conStr">Строка соединения с базой</param>
		/// <param name="filter">Критерий фильтрации</param>
		/// <returns>Массив с данными о сотрудниках</returns>
		public static string [][] getEmpls(string conStr,string filter)
		{
			string [][] res;
			string sql="";
			
			try
			{
				sql="set concat_null_yields_null off; select MSURNAME+' '+MNAME+' '+MSECNAME as FIO,MID from EmplMain @ order by MNAME; set concat_null_yields_null on;";
				if(filter=="") sql=sql.Replace("@"," ");
				else
					sql=sql.Replace("@",filter);

				SqlDataAdapter sqlDA=new SqlDataAdapter(sql,conStr);
				sqlDA.SelectCommand.CommandTimeout=60;
				DataSet sqlDS = new DataSet();
				sqlDA.Fill(sqlDS);
				int cnt=sqlDS.Tables[0].Rows.Count;	
				res=new string[cnt][];				
			
				int i=0;
				foreach(DataRow o in sqlDS.Tables[0].Rows)
				{
					res[i]=new string[2];
					res[i][0]=Convert.ToString(o.ItemArray[0]);
					res[i][1]=Convert.ToString(o.ItemArray[1]);
					++i;
				}
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				res=new string[0][];				
			}

			return res;
		}
		/// <summary>
		/// Обновляет список отделов на форме
		/// </summary>
		private void refreshDepart()
		{
			string [][] res=null;
			res=fmDepart.getDeparts(conStr);

			if(res.Length!=0)
			{
				cbDepart.Items.Clear();
				foreach(string [] s in res)				
					cbDepart.Items.Add(s[0]);
			}
		}
		/// <summary>
		/// Обновляет список групп на форме
		/// </summary>
		private void refreshGroup()
		{
			string [][] res=null;
			res=fmGroup.getGroups(conStr);

			if(res.Length!=0)
			{
				cbGroup.Items.Clear();
				foreach(string [] s in res)				
					cbGroup.Items.Add(s[0]);
			}
		}
		/// <summary>
		/// Загружает информацию о сотруднике из базы в элементы управления
		/// </summary>
		/// <param name="id">Идентификатор сотрудника</param>
		/// <returns>true - выполнена успешно; false - возникла ошибка</returns>
		private bool getUserByID(int id)
		{
			string sql="SELECT TOP 1 MID,MCODE,MLOGIN,MPASSWORD,MLOCKED,MCODEDT,MPASSWORDDT,MUSEPASSW,";
                   sql=sql+"MDEPARTMENT,MGROUP,MSURNAME,MNAME,MSECNAME,MBIRTHDAY,MDOCUMENT,MDOCTYPE,MMALE,MEMPDATE,MPHONE,MMOBPHONE, ";
                   sql=sql+"MEMAIL, MCOUNTRY, MREGION,MDISTRICT, MSETTLEMENT, MADDRESS,MPHOTO FROM EmplMain";
			       sql=sql+" where mid="+id.ToString();
			try
			{					
				SqlDataAdapter sqlDA=new SqlDataAdapter(sql,conStr);
				sqlDA.SelectCommand.CommandTimeout=60;
				DataSet sqlDS = new DataSet();
				sqlDA.Fill(sqlDS);
				
				if(sqlDS.Tables[0].Rows.Count>0)
				{
					object [] o=sqlDS.Tables[0].Rows[0].ItemArray;
					tbCode.Text=(Convert.IsDBNull(o[1]))?"":o[1].ToString();
					tbLogin.Text=(Convert.IsDBNull(o[2]))?"":o[2].ToString();
					tbPassword.Text=(Convert.IsDBNull(o[3]))?"":o[3].ToString();
					if((Convert.IsDBNull(o[4])) || (o[4].ToString()=="NO"))
						cbLocked.Checked=false;
					else
						cbLocked.Checked=true;
					if((Convert.IsDBNull(o[7])) || (o[7].ToString()=="NO"))
					{
						cbUsePassword.Checked=false;
						gbPassword.Enabled=false;						
					}
					else
					{
						cbUsePassword.Checked=true;
						gbPassword.Enabled=true;
					}                    
					
					cbDepart.SelectedIndex=cbDepart.Items.IndexOf((Convert.IsDBNull(o[8]))?"":o[8].ToString());
					cbGroup.SelectedIndex=cbGroup.Items.IndexOf((Convert.IsDBNull(o[9]))?"":o[9].ToString());
					tbSurname.Text=(Convert.IsDBNull(o[10]))?"":o[10].ToString();
					tbName.Text=(Convert.IsDBNull(o[11]))?"":o[11].ToString();
					tbSecname.Text=(Convert.IsDBNull(o[12]))?"":o[12].ToString();					
					dtpBirth.Value=(Convert.IsDBNull(o[13]))? DateTime.Now:Convert.ToDateTime(o[13]);					
					tbDoc.Text=(Convert.IsDBNull(o[14]))?"":o[14].ToString();
					tbDocType.Text=(Convert.IsDBNull(o[15]))?"":o[15].ToString();
					cbMale.SelectedIndex=cbMale.Items.IndexOf((Convert.IsDBNull(o[16]))?"_":o[16].ToString());
					dtpEmplDate.Value=(Convert.IsDBNull(o[17]))? DateTime.Now:Convert.ToDateTime(o[17]);
					tbPhone.Text=(Convert.IsDBNull(o[18]))?"":o[18].ToString();
					tbMobphone.Text=(Convert.IsDBNull(o[19]))?"":o[19].ToString();
					tbEmail.Text=(Convert.IsDBNull(o[20]))?"":o[20].ToString();
					tbCountry.Text=(Convert.IsDBNull(o[21]))?"":o[21].ToString();
					tbRegion.Text=(Convert.IsDBNull(o[22]))?"":o[22].ToString();
					tbDistrict.Text=(Convert.IsDBNull(o[23]))?"":o[23].ToString();
					tbSettlement.Text=(Convert.IsDBNull(o[24]))?"":o[24].ToString();
					tbAddress.Text=(Convert.IsDBNull(o[25]))?"":o[25].ToString();
					
					byte [] photo=(Convert.IsDBNull(o[26]))?new byte[0]:(byte [])o[26];
					if(photo.Length!=0)
					{
						MemoryStream ms=new MemoryStream(photo);
						pbPhoto.Image=new Bitmap(ms);
						lbPhoto.Hide();
					}
					else
					{
						pbPhoto.Image=null;
						lbPhoto.Show();
					}
				}
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				return false;
			}
			
			return true;
		}
		/// <summary>
		/// Удаляет сотрудника из базы
		/// </summary>
		/// <param name="id">Идентификатор сотрудника</param>
		/// <returns>true - выполнена успешно; false - возникла ошибка</returns>
		private bool delUserByID(int id)
		{
			try
			{
				SqlConnection sqlCon=new SqlConnection(conStr);
				sqlCon.Open();
				SqlCommand sqlCom=new SqlCommand("delete from EmplMain where MID="+id.ToString(),sqlCon);
				sqlCom.CommandTimeout=60;
				if(sqlCom.ExecuteNonQuery()==0)
					return false;
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
		/// Добавляет нового сотрудника в отдел либо группу
		/// </summary>
		/// <param name="name">Имя отдела (группы)</param>
		/// <param name="mode">0 - добавляет в отдел; 1 - добавляет в группу</param>
		/// <returns>true - выполнена успешно; false - возникла ошибка</returns>		
		private bool AddUser(string name,int mode)
		{		
			string sql=(mode==0)?"insert into EmplMain (MSURNAME,MDEPARTMENT) Values ('<Сотрудник>','"+name+"')":"insert into EmplMain (MSURNAME,MGROUP) Values ('<Сотрудник>','"+name.Replace("'","''")+"')";

			try
			{
				SqlConnection sqlCon=new SqlConnection(conStr);
				sqlCon.Open();
				SqlCommand sqlCom=new SqlCommand(sql);
				sqlCom.CommandTimeout=60;
				sqlCom.Connection=sqlCon;
				if(sqlCom.ExecuteNonQuery()!=1) return false;			
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
		/// Изменяет информацию о сотруднике
		/// </summary>
		/// <param name="id">Идентификатор сотрудника</param>
		/// <returns>true - выполнена успешно; false - возникла ошибка</returns>
		private bool changeUserByID(int id)
		{
			string sql="update EmplMain set ";

			SqlConnection sqlCon=new SqlConnection(conStr);
			sql= "update EmplMain set MCODE=@MCODE,MLOGIN=@MLOGIN,MPASSWORD=@MPASSWORD,MLOCKED=@MLOCKED,MUSEPASSW=@MUSEPASSW,MDEPARTMENT=@MDEPARTMENT,MGROUP=@MGROUP,MSURNAME=@MSURNAME,MNAME=@MNAME,MSECNAME=@MSECNAME, ";
			sql+= "MBIRTHDAY=@MBIRTHDAY,MDOCUMENT=@MDOCUMENT,MDOCTYPE=@MDOCTYPE,MMALE=@MMALE,MEMPDATE=@MEMPDATE,MPHONE=@MPHONE,MMOBPHONE=@MMOBPHONE,MEMAIL=@MEMAIL,MCOUNTRY=@MCOUNTRY,MREGION=@MREGION,MDISTRICT=@MDISTRICT,MSETTLEMENT=@MSETTLEMENT,MADDRESS=@MADDRESS,MPHOTO=@MPHOTO where mid=@mid";
			SqlCommand sqlCom=new SqlCommand(sql,sqlCon);
			sqlCom.CommandTimeout=60;
            
			sqlCom.Parameters.Add(new SqlParameter("@MCODE",tbCode.Text));
			sqlCom.Parameters.Add(new SqlParameter("@MLOGIN",tbLogin.Text));
			sqlCom.Parameters.Add(new SqlParameter("@MPASSWORD",tbPassword.Text));
			sqlCom.Parameters.Add(new SqlParameter("@MLOCKED",(cbLocked.Checked)?"YES":"NO"));
			sqlCom.Parameters.Add(new SqlParameter("@MUSEPASSW",(cbUsePassword.Checked)?"YES":"NO"));
			sqlCom.Parameters.Add(new SqlParameter("@MDEPARTMENT",cbDepart.Text));
			sqlCom.Parameters.Add(new SqlParameter("@MGROUP",cbGroup.Text));
			sqlCom.Parameters.Add(new SqlParameter("@MSURNAME",tbSurname.Text));
			sqlCom.Parameters.Add(new SqlParameter("@MNAME",tbName.Text));
			sqlCom.Parameters.Add(new SqlParameter("@MSECNAME",tbSecname.Text));
			sqlCom.Parameters.Add(new SqlParameter("@MBIRTHDAY",dtpBirth.Value));
			sqlCom.Parameters.Add(new SqlParameter("@MDOCUMENT",tbDoc.Text));
			sqlCom.Parameters.Add(new SqlParameter("@MDOCTYPE",tbDocType.Text));
			sqlCom.Parameters.Add(new SqlParameter("@MMALE",cbMale.Text));
			sqlCom.Parameters.Add(new SqlParameter("@MEMPDATE",dtpEmplDate.Value));
			sqlCom.Parameters.Add(new SqlParameter("@MPHONE",tbPhone.Text));
			sqlCom.Parameters.Add(new SqlParameter("@MMOBPHONE",tbMobphone.Text));
			sqlCom.Parameters.Add(new SqlParameter("@MEMAIL",tbEmail.Text));
			sqlCom.Parameters.Add(new SqlParameter("@MCOUNTRY",tbCountry.Text));
			sqlCom.Parameters.Add(new SqlParameter("@MREGION",tbRegion.Text));
			sqlCom.Parameters.Add(new SqlParameter("@MDISTRICT",tbDistrict.Text));
			sqlCom.Parameters.Add(new SqlParameter("@MSETTLEMENT",tbSettlement.Text));
			sqlCom.Parameters.Add(new SqlParameter("@MADDRESS",tbAddress.Text));
			//записываем фотографию
			using(MemoryStream ms=new MemoryStream())
			{
				if(pbPhoto.Image!=null)
				{
					Bitmap bm;
					if((bm=(pbPhoto.Image as Bitmap))!=null)
					{
						
						bm.Save(ms,pbPhoto.Image.RawFormat);			
							
						byte [] photo=new byte[ms.Length];
						ms.Position=0;
						ms.Read(photo,0, (int)ms.Length);
						sqlCom.Parameters.Add("@MPHOTO", SqlDbType.Binary,photo.Length).Value=photo;
					}
				}
			}
			//...			
			sqlCom.Parameters.Add(new SqlParameter("@mid",id));
				
			try
			{
  				sqlCon.Open();								
				if(sqlCom.ExecuteNonQuery()!=1) return false;			
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
		/// Заполняет "дерево сотрудников"
		/// </summary>
		/// <param name="mode">0 - "дерево" заполняется по отделам; 1- "дерево" заполняется по группам</param>
		/// <param name="tv">Элемент управления TreeView для заполнения</param>
		/// <param name="openname">Имя отдела (группы), узел которого нужно открыть (опция) </param>
		/// <param name="ndname">Имя сотрудника (узла), который необходимо выбрать (опция)</param>
		//заполняет дерево работников
		private void fillTree(int mode,TreeView tv,string openname,string ndname)
		{
			Cursor=Cursors.WaitCursor;
			tv.Nodes.Clear();
            
			try
			{
				ImageList il=new ImageList();					
				Bitmap bm=new Bitmap(System.AppDomain.CurrentDomain.BaseDirectory+"home.bmp");
				il.Images.Add(bm);
				bm=new Bitmap(System.AppDomain.CurrentDomain.BaseDirectory+"users.bmp");
				il.Images.Add(bm);
				bm=new Bitmap(System.AppDomain.CurrentDomain.BaseDirectory+"user.bmp");
				il.Images.Add(bm);
				tv.ImageList=il;
			}
			catch
			{}            			
				string [][] rNodes=new string[0][];

				//родиельские узлы
				switch(mode)
				{
					case 0: rNodes=fmDepart.getDeparts(conStr);
						break;
					case 1: rNodes=fmGroup.getGroups(conStr);
						break;
				}
				foreach(string [] s in rNodes)
				{
					TreeNode tnd=new TreeNode(s[0]);
					tnd.ImageIndex=mode;
					tnd.SelectedImageIndex=mode;
					if(tnd.Text==openname)
						tnd.ExpandAll();
					tv.Nodes.Add(tnd);		
				}
				//проходимся по всем узлам и заполняем их			
				foreach(TreeNode tn in tv.Nodes)
				{
					switch(mode) 
					{
						case 0: rNodes=getEmpls(conStr," where (MDEPARTMENT='"+tn.Text+"')");
							break;
						case 1: rNodes=getEmpls(conStr," where (MGROUP='"+tn.Text+"')");
							break;
					}
					foreach(string [] s in rNodes)
					{	
						TreeNode tnd=new TreeNode(s[0]);
						tnd.Tag=Convert.ToInt32(s[1]);
						tnd.ImageIndex=2;
						tnd.SelectedImageIndex=2;
						tn.Nodes.Add(tnd);
					}
				}
				//работники, у которых нет группы или отдела
				switch(mode) 
				{
					case 0: rNodes=getEmpls(conStr," where (select count(*) from EmplDepart where DPNAME=MDEPARTMENT)=0");
						break;
					case 1: rNodes=getEmpls(conStr," where (select count(*) from EmplGroup where GRNAME=MGROUP)=0");
						break;
				}
				if(rNodes.Length!=0) 
				{
					TreeNode tn=new TreeNode("Прочие");				
					tn.ImageIndex=mode;
					tn.SelectedImageIndex=mode;
					foreach(string [] s in rNodes)
					{
						TreeNode tnd=new TreeNode(s[0]);
						tnd.ImageIndex=2;
						tnd.SelectedImageIndex=2;					
						tnd.Tag=Convert.ToInt32(s[1]);
						tn.Nodes.Add(tnd);
					}
					tv.Nodes.Add(tn);
				}
			
			foreach(TreeNode tn in tv.Nodes)
			{
				if((tn.Text==openname) && (openname!="")) tn.ExpandAll();								
				foreach(TreeNode tnd in tn.Nodes)
					if((tnd.Text==ndname) && (ndname!="")) tv.SelectedNode=tnd;
			}			
			
			Cursor=Cursors.Default;		
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmEmpl));
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Root");
            this.pnData = new System.Windows.Forms.Panel();
            this.pnEmplInfo = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.dtpEmplDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnNewCode = new System.Windows.Forms.Button();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.btnToQueue = new System.Windows.Forms.Button();
            this.btnPrintQueue = new System.Windows.Forms.Button();
            this.btnDelQueue = new System.Windows.Forms.Button();
            this.cbLocked = new System.Windows.Forms.CheckBox();
            this.cbUsePassword = new System.Windows.Forms.CheckBox();
            this.gbPassword = new System.Windows.Forms.GroupBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.btnNewGroup = new System.Windows.Forms.Button();
            this.btnNewDepart = new System.Windows.Forms.Button();
            this.cbGroup = new System.Windows.Forms.ComboBox();
            this.cbDepart = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.tbMobphone = new System.Windows.Forms.TextBox();
            this.tbPhone = new System.Windows.Forms.TextBox();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.tbSettlement = new System.Windows.Forms.TextBox();
            this.tbDistrict = new System.Windows.Forms.TextBox();
            this.tbRegion = new System.Windows.Forms.TextBox();
            this.tbCountry = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbPhoto = new System.Windows.Forms.Label();
            this.pbPhoto = new System.Windows.Forms.PictureBox();
            this.tbDocType = new System.Windows.Forms.TextBox();
            this.tbDoc = new System.Windows.Forms.TextBox();
            this.cbMale = new System.Windows.Forms.ComboBox();
            this.dtpBirth = new System.Windows.Forms.DateTimePicker();
            this.tbSecname = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbSurname = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tvEmpl = new System.Windows.Forms.TreeView();
            this.cmTree = new System.Windows.Forms.ContextMenu();
            this.miDepart = new System.Windows.Forms.MenuItem();
            this.miGroup = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miAdd = new System.Windows.Forms.MenuItem();
            this.miSave = new System.Windows.Forms.MenuItem();
            this.miDel = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.miSearch = new System.Windows.Forms.MenuItem();
            this.btnClose = new System.Windows.Forms.Button();
            this.ofdPhoto = new System.Windows.Forms.OpenFileDialog();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelEmpl = new System.Windows.Forms.Button();
            this.pnData.SuspendLayout();
            this.pnEmplInfo.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gbPassword.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnData
            // 
            this.pnData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnData.Controls.Add(this.pnEmplInfo);
            this.pnData.Controls.Add(this.splitter1);
            this.pnData.Controls.Add(this.tvEmpl);
            this.pnData.Location = new System.Drawing.Point(2, 2);
            this.pnData.Name = "pnData";
            this.pnData.Size = new System.Drawing.Size(688, 590);
            this.pnData.TabIndex = 0;
            // 
            // pnEmplInfo
            // 
            this.pnEmplInfo.AutoScroll = true;
            this.pnEmplInfo.AutoScrollMinSize = new System.Drawing.Size(427, 0);
            this.pnEmplInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.pnEmplInfo.Controls.Add(this.groupBox3);
            this.pnEmplInfo.Controls.Add(this.groupBox2);
            this.pnEmplInfo.Controls.Add(this.groupBox1);
            this.pnEmplInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnEmplInfo.Location = new System.Drawing.Point(259, 0);
            this.pnEmplInfo.Name = "pnEmplInfo";
            this.pnEmplInfo.Size = new System.Drawing.Size(429, 590);
            this.pnEmplInfo.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnEdit);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.dtpEmplDate);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.cbLocked);
            this.groupBox3.Controls.Add(this.cbUsePassword);
            this.groupBox3.Controls.Add(this.gbPassword);
            this.groupBox3.Controls.Add(this.btnNewGroup);
            this.groupBox3.Controls.Add(this.btnNewDepart);
            this.groupBox3.Controls.Add(this.cbGroup);
            this.groupBox3.Controls.Add(this.cbDepart);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Location = new System.Drawing.Point(5, 352);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(419, 232);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Service information";
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.BackColor = System.Drawing.Color.LightGray;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(361, 196);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(39, 30);
            this.btnEdit.TabIndex = 17;
            this.btnEdit.Tag = "Редактировать";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(8, 20);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(128, 14);
            this.label18.TabIndex = 16;
            this.label18.Text = "Employment Date";
            // 
            // dtpEmplDate
            // 
            this.dtpEmplDate.Location = new System.Drawing.Point(141, 15);
            this.dtpEmplDate.Name = "dtpEmplDate";
            this.dtpEmplDate.Size = new System.Drawing.Size(128, 20);
            this.dtpEmplDate.TabIndex = 1;
            this.dtpEmplDate.Value = new System.DateTime(2007, 3, 16, 14, 16, 23, 477);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnNewCode);
            this.groupBox4.Controls.Add(this.tbCode);
            this.groupBox4.Controls.Add(this.btnToQueue);
            this.groupBox4.Controls.Add(this.btnPrintQueue);
            this.groupBox4.Controls.Add(this.btnDelQueue);
            this.groupBox4.Location = new System.Drawing.Point(8, 93);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(400, 46);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Barcode";
            // 
            // btnNewCode
            // 
            this.btnNewCode.BackColor = System.Drawing.Color.LightGray;
            this.btnNewCode.Image = ((System.Drawing.Image)(resources.GetObject("btnNewCode.Image")));
            this.btnNewCode.Location = new System.Drawing.Point(177, 10);
            this.btnNewCode.Name = "btnNewCode";
            this.btnNewCode.Size = new System.Drawing.Size(40, 31);
            this.btnNewCode.TabIndex = 2;
            this.btnNewCode.UseVisualStyleBackColor = false;
            this.btnNewCode.Click += new System.EventHandler(this.btnNewCode_Click);
            // 
            // tbCode
            // 
            this.tbCode.Location = new System.Drawing.Point(5, 15);
            this.tbCode.Name = "tbCode";
            this.tbCode.ReadOnly = true;
            this.tbCode.Size = new System.Drawing.Size(163, 20);
            this.tbCode.TabIndex = 1;
            // 
            // btnToQueue
            // 
            this.btnToQueue.BackColor = System.Drawing.Color.LightGray;
            this.btnToQueue.Image = ((System.Drawing.Image)(resources.GetObject("btnToQueue.Image")));
            this.btnToQueue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnToQueue.Location = new System.Drawing.Point(246, 10);
            this.btnToQueue.Name = "btnToQueue";
            this.btnToQueue.Size = new System.Drawing.Size(54, 31);
            this.btnToQueue.TabIndex = 3;
            this.btnToQueue.Text = "(0)";
            this.btnToQueue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnToQueue.UseVisualStyleBackColor = false;
            this.btnToQueue.Click += new System.EventHandler(this.btnToQueue_Click);
            // 
            // btnPrintQueue
            // 
            this.btnPrintQueue.BackColor = System.Drawing.Color.LightGray;
            this.btnPrintQueue.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintQueue.Image")));
            this.btnPrintQueue.Location = new System.Drawing.Point(352, 9);
            this.btnPrintQueue.Name = "btnPrintQueue";
            this.btnPrintQueue.Size = new System.Drawing.Size(40, 31);
            this.btnPrintQueue.TabIndex = 5;
            this.btnPrintQueue.UseVisualStyleBackColor = false;
            this.btnPrintQueue.Click += new System.EventHandler(this.btnPrintQueue_Click);
            // 
            // btnDelQueue
            // 
            this.btnDelQueue.BackColor = System.Drawing.Color.LightGray;
            this.btnDelQueue.Image = ((System.Drawing.Image)(resources.GetObject("btnDelQueue.Image")));
            this.btnDelQueue.Location = new System.Drawing.Point(306, 10);
            this.btnDelQueue.Name = "btnDelQueue";
            this.btnDelQueue.Size = new System.Drawing.Size(40, 31);
            this.btnDelQueue.TabIndex = 4;
            this.btnDelQueue.UseVisualStyleBackColor = false;
            this.btnDelQueue.Click += new System.EventHandler(this.btnDelQueue_Click);
            // 
            // cbLocked
            // 
            this.cbLocked.Location = new System.Drawing.Point(10, 204);
            this.cbLocked.Name = "cbLocked";
            this.cbLocked.Size = new System.Drawing.Size(102, 17);
            this.cbLocked.TabIndex = 9;
            this.cbLocked.Text = "Blocked";
            // 
            // cbUsePassword
            // 
            this.cbUsePassword.Location = new System.Drawing.Point(19, 144);
            this.cbUsePassword.Name = "cbUsePassword";
            this.cbUsePassword.Size = new System.Drawing.Size(125, 24);
            this.cbUsePassword.TabIndex = 7;
            this.cbUsePassword.Text = "Password protect";
            this.cbUsePassword.Click += new System.EventHandler(this.cbUsePassword_Click);
            // 
            // gbPassword
            // 
            this.gbPassword.Controls.Add(this.tbPassword);
            this.gbPassword.Controls.Add(this.label20);
            this.gbPassword.Controls.Add(this.tbLogin);
            this.gbPassword.Controls.Add(this.label19);
            this.gbPassword.Location = new System.Drawing.Point(8, 152);
            this.gbPassword.Name = "gbPassword";
            this.gbPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gbPassword.Size = new System.Drawing.Size(400, 43);
            this.gbPassword.TabIndex = 8;
            this.gbPassword.TabStop = false;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(250, 17);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(140, 20);
            this.tbPassword.TabIndex = 2;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(194, 20);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(60, 14);
            this.label20.TabIndex = 2;
            this.label20.Text = "Password";
            // 
            // tbLogin
            // 
            this.tbLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLogin.Location = new System.Drawing.Point(48, 17);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(140, 20);
            this.tbLogin.TabIndex = 1;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(6, 20);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(40, 12);
            this.label19.TabIndex = 0;
            this.label19.Text = "Login";
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.BackColor = System.Drawing.Color.LightGray;
            this.btnNewGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNewGroup.Location = new System.Drawing.Point(368, 68);
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(32, 23);
            this.btnNewGroup.TabIndex = 5;
            this.btnNewGroup.Text = "...";
            this.btnNewGroup.UseVisualStyleBackColor = false;
            this.btnNewGroup.Click += new System.EventHandler(this.btnNewGroup_Click);
            // 
            // btnNewDepart
            // 
            this.btnNewDepart.BackColor = System.Drawing.Color.LightGray;
            this.btnNewDepart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNewDepart.Location = new System.Drawing.Point(368, 40);
            this.btnNewDepart.Name = "btnNewDepart";
            this.btnNewDepart.Size = new System.Drawing.Size(32, 23);
            this.btnNewDepart.TabIndex = 3;
            this.btnNewDepart.Text = "...";
            this.btnNewDepart.UseVisualStyleBackColor = false;
            this.btnNewDepart.Click += new System.EventHandler(this.btnNewDepart_Click);
            // 
            // cbGroup
            // 
            this.cbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroup.Location = new System.Drawing.Point(72, 69);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Size = new System.Drawing.Size(296, 21);
            this.cbGroup.TabIndex = 4;
            this.cbGroup.SelectedIndexChanged += new System.EventHandler(this.cbGroup_SelectedIndexChanged);
            // 
            // cbDepart
            // 
            this.cbDepart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDepart.Location = new System.Drawing.Point(72, 42);
            this.cbDepart.Name = "cbDepart";
            this.cbDepart.Size = new System.Drawing.Size(296, 21);
            this.cbDepart.TabIndex = 2;
            this.cbDepart.SelectedIndexChanged += new System.EventHandler(this.cbDepart_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(10, 72);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(48, 23);
            this.label17.TabIndex = 1;
            this.label17.Text = "Group";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(8, 45);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(40, 23);
            this.label16.TabIndex = 0;
            this.label16.Text = "Department";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbEmail);
            this.groupBox2.Controls.Add(this.tbMobphone);
            this.groupBox2.Controls.Add(this.tbPhone);
            this.groupBox2.Controls.Add(this.tbAddress);
            this.groupBox2.Controls.Add(this.tbSettlement);
            this.groupBox2.Controls.Add(this.tbDistrict);
            this.groupBox2.Controls.Add(this.tbRegion);
            this.groupBox2.Controls.Add(this.tbCountry);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(5, 184);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(418, 161);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lives";
            // 
            // tbEmail
            // 
            this.tbEmail.Location = new System.Drawing.Point(80, 134);
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.Size = new System.Drawing.Size(328, 20);
            this.tbEmail.TabIndex = 15;
            // 
            // tbMobphone
            // 
            this.tbMobphone.Location = new System.Drawing.Point(293, 111);
            this.tbMobphone.Name = "tbMobphone";
            this.tbMobphone.Size = new System.Drawing.Size(114, 20);
            this.tbMobphone.TabIndex = 14;
            // 
            // tbPhone
            // 
            this.tbPhone.Location = new System.Drawing.Point(80, 111);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Size = new System.Drawing.Size(120, 20);
            this.tbPhone.TabIndex = 13;
            // 
            // tbAddress
            // 
            this.tbAddress.Location = new System.Drawing.Point(80, 88);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(328, 20);
            this.tbAddress.TabIndex = 12;
            // 
            // tbSettlement
            // 
            this.tbSettlement.Location = new System.Drawing.Point(80, 64);
            this.tbSettlement.Name = "tbSettlement";
            this.tbSettlement.Size = new System.Drawing.Size(328, 20);
            this.tbSettlement.TabIndex = 11;
            // 
            // tbDistrict
            // 
            this.tbDistrict.Location = new System.Drawing.Point(80, 40);
            this.tbDistrict.Name = "tbDistrict";
            this.tbDistrict.Size = new System.Drawing.Size(328, 20);
            this.tbDistrict.TabIndex = 10;
            // 
            // tbRegion
            // 
            this.tbRegion.Location = new System.Drawing.Point(256, 15);
            this.tbRegion.Name = "tbRegion";
            this.tbRegion.Size = new System.Drawing.Size(152, 20);
            this.tbRegion.TabIndex = 9;
            // 
            // tbCountry
            // 
            this.tbCountry.Location = new System.Drawing.Point(80, 16);
            this.tbCountry.Name = "tbCountry";
            this.tbCountry.Size = new System.Drawing.Size(96, 20);
            this.tbCountry.TabIndex = 8;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(10, 137);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 16);
            this.label15.TabIndex = 8;
            this.label15.Text = "Email";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(208, 113);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 16);
            this.label14.TabIndex = 7;
            this.label14.Text = "tel.2";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(10, 114);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 16);
            this.label13.TabIndex = 6;
            this.label13.Text = "tel.";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(10, 91);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 16);
            this.label12.TabIndex = 5;
            this.label12.Text = "Address";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(10, 66);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 16);
            this.label11.TabIndex = 4;
            this.label11.Text = "City";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(10, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 16);
            this.label10.TabIndex = 3;
            this.label10.Text = "Area";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(10, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 16);
            this.label9.TabIndex = 2;
            this.label9.Text = "Country";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(200, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 16);
            this.label8.TabIndex = 1;
            this.label8.Text = "Region";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbPhoto);
            this.groupBox1.Controls.Add(this.pbPhoto);
            this.groupBox1.Controls.Add(this.tbDocType);
            this.groupBox1.Controls.Add(this.tbDoc);
            this.groupBox1.Controls.Add(this.cbMale);
            this.groupBox1.Controls.Add(this.dtpBirth);
            this.groupBox1.Controls.Add(this.tbSecname);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.tbSurname);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(418, 165);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Personal data";
            // 
            // lbPhoto
            // 
            this.lbPhoto.Location = new System.Drawing.Point(344, 56);
            this.lbPhoto.Name = "lbPhoto";
            this.lbPhoto.Size = new System.Drawing.Size(40, 16);
            this.lbPhoto.TabIndex = 9;
            this.lbPhoto.Text = "FOTO";
            this.lbPhoto.Visible = false;
            this.lbPhoto.DoubleClick += new System.EventHandler(this.pbPhoto_Click);
            // 
            // pbPhoto
            // 
            this.pbPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPhoto.Location = new System.Drawing.Point(324, 15);
            this.pbPhoto.Name = "pbPhoto";
            this.pbPhoto.Size = new System.Drawing.Size(75, 100);
            this.pbPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPhoto.TabIndex = 8;
            this.pbPhoto.TabStop = false;
            this.pbPhoto.Click += new System.EventHandler(this.pbPhoto_Click_1);
            this.pbPhoto.DoubleClick += new System.EventHandler(this.pbPhoto_Click);
            // 
            // tbDocType
            // 
            this.tbDocType.Location = new System.Drawing.Point(227, 138);
            this.tbDocType.Name = "tbDocType";
            this.tbDocType.Size = new System.Drawing.Size(184, 20);
            this.tbDocType.TabIndex = 7;
            // 
            // tbDoc
            // 
            this.tbDoc.Location = new System.Drawing.Point(10, 138);
            this.tbDoc.Multiline = true;
            this.tbDoc.Name = "tbDoc";
            this.tbDoc.Size = new System.Drawing.Size(198, 20);
            this.tbDoc.TabIndex = 6;
            // 
            // cbMale
            // 
            this.cbMale.BackColor = System.Drawing.SystemColors.Highlight;
            this.cbMale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMale.ForeColor = System.Drawing.SystemColors.Window;
            this.cbMale.Items.AddRange(new object[] {
            "м",
            "ж",
            "_"});
            this.cbMale.Location = new System.Drawing.Point(70, 94);
            this.cbMale.Name = "cbMale";
            this.cbMale.Size = new System.Drawing.Size(50, 21);
            this.cbMale.TabIndex = 4;
            // 
            // dtpBirth
            // 
            this.dtpBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBirth.Location = new System.Drawing.Point(210, 94);
            this.dtpBirth.Name = "dtpBirth";
            this.dtpBirth.Size = new System.Drawing.Size(88, 20);
            this.dtpBirth.TabIndex = 5;
            this.dtpBirth.Value = new System.DateTime(2019, 4, 25, 0, 0, 0, 0);
            // 
            // tbSecname
            // 
            this.tbSecname.Location = new System.Drawing.Point(101, 64);
            this.tbSecname.Name = "tbSecname";
            this.tbSecname.Size = new System.Drawing.Size(195, 20);
            this.tbSecname.TabIndex = 3;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(101, 39);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(195, 20);
            this.tbName.TabIndex = 2;
            // 
            // tbSurname
            // 
            this.tbSurname.Location = new System.Drawing.Point(101, 15);
            this.tbSurname.Name = "tbSurname";
            this.tbSurname.Size = new System.Drawing.Size(195, 20);
            this.tbSurname.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(231, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 16);
            this.label7.TabIndex = 6;
            this.label7.Text = "Title of the document";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(10, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(224, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Proof of identity";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(128, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Date of birth";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Sex";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "middle name";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Surname";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(256, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 590);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // tvEmpl
            // 
            this.tvEmpl.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvEmpl.Location = new System.Drawing.Point(0, 0);
            this.tvEmpl.Name = "tvEmpl";
            treeNode2.Name = "";
            treeNode2.Text = "Root";
            this.tvEmpl.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.tvEmpl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tvEmpl.Size = new System.Drawing.Size(256, 590);
            this.tvEmpl.Sorted = true;
            this.tvEmpl.TabIndex = 0;
            this.tvEmpl.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvEmpl_AfterSelect);
            // 
            // cmTree
            // 
            this.cmTree.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miDepart,
            this.miGroup,
            this.menuItem1,
            this.menuItem3,
            this.menuItem4,
            this.menuItem2,
            this.miAdd,
            this.miSave,
            this.miDel,
            this.menuItem5,
            this.miSearch});
            // 
            // miDepart
            // 
            this.miDepart.Checked = true;
            this.miDepart.Index = 0;
            this.miDepart.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.miDepart.Text = "По отделам";
            this.miDepart.Click += new System.EventHandler(this.miDepart_Click);
            // 
            // miGroup
            // 
            this.miGroup.Index = 1;
            this.miGroup.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.miGroup.Text = "По группам";
            this.miGroup.Click += new System.EventHandler(this.miDepart_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.Text = "-";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 3;
            this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.F9;
            this.menuItem3.Text = "Новый штрих-код";
            this.menuItem3.Click += new System.EventHandler(this.btnNewCode_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 4;
            this.menuItem4.Shortcut = System.Windows.Forms.Shortcut.F10;
            this.menuItem4.Text = "В печать штрих-код";
            this.menuItem4.Click += new System.EventHandler(this.btnToQueue_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 5;
            this.menuItem2.Text = "-";
            // 
            // miAdd
            // 
            this.miAdd.Index = 6;
            this.miAdd.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.miAdd.Text = "Добавить";
            this.miAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // miSave
            // 
            this.miSave.Index = 7;
            this.miSave.Shortcut = System.Windows.Forms.Shortcut.F6;
            this.miSave.Text = "Сохранить";
            this.miSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // miDel
            // 
            this.miDel.Index = 8;
            this.miDel.Shortcut = System.Windows.Forms.Shortcut.F8;
            this.miDel.Text = "Удалить";
            this.miDel.Click += new System.EventHandler(this.btnDelEmpl_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 9;
            this.menuItem5.Text = "-";
            // 
            // miSearch
            // 
            this.miSearch.Index = 10;
            this.miSearch.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.miSearch.Text = "Поиск сотрудника";
            this.miSearch.Click += new System.EventHandler(this.miSearch_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.BackColor = System.Drawing.Color.LightGray;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.btnClose.Location = new System.Drawing.Point(608, 608);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // ofdPhoto
            // 
            this.ofdPhoto.DefaultExt = "jpg";
            this.ofdPhoto.Filter = "JPEG-файл (*.jpg)|*.jpg|TIFF-файл (*.tif)|*.tif|Bitmap-файл (*.bmp)|*.bmp|GIF-фай" +
    "л (*.gif)|*.gif)";
            this.ofdPhoto.FilterIndex = 0;
            this.ofdPhoto.Title = "Фотография";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnAdd);
            this.groupBox5.Controls.Add(this.btnSave);
            this.groupBox5.Controls.Add(this.btnDelEmpl);
            this.groupBox5.Location = new System.Drawing.Point(4, 596);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(260, 40);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.BackColor = System.Drawing.Color.LightGray;
            this.btnAdd.Location = new System.Drawing.Point(9, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.BackColor = System.Drawing.Color.LightGray;
            this.btnSave.Location = new System.Drawing.Point(91, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelEmpl
            // 
            this.btnDelEmpl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelEmpl.BackColor = System.Drawing.Color.LightGray;
            this.btnDelEmpl.Location = new System.Drawing.Point(173, 10);
            this.btnDelEmpl.Name = "btnDelEmpl";
            this.btnDelEmpl.Size = new System.Drawing.Size(75, 23);
            this.btnDelEmpl.TabIndex = 9;
            this.btnDelEmpl.Text = "Удалить";
            this.btnDelEmpl.UseVisualStyleBackColor = false;
            this.btnDelEmpl.Click += new System.EventHandler(this.btnDelEmpl_Click);
            // 
            // fmEmpl
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(694, 639);
            this.ContextMenu = this.cmTree;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pnData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(700, 600);
            this.Name = "fmEmpl";
            this.Text = "Employee Handbook";
            this.Load += new System.EventHandler(this.fmEmpl_Load);
            this.pnData.ResumeLayout(false);
            this.pnEmplInfo.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.gbPassword.ResumeLayout(false);
            this.gbPassword.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Вызывает форму для редактирования отделов
		/// </summary>		
		private void btnNewDepart_Click(object sender, System.EventArgs e)
		{
			fmDepart fmD=new fmDepart(conStr);
			fmD.ShowDialog();

			miDepart_Click(miDepart,null);
			refreshDepart();		
		}
		/// <summary>
		/// Вызывает форму для редактирования групп
		/// </summary>
		private void btnNewGroup_Click(object sender, System.EventArgs e)
		{
			fmGroup fmGr=new fmGroup(conStr);
			fmGr.ShowDialog();

			miDepart_Click(miGroup,null);						
			refreshGroup();
		}
		/// <summary>
		/// Запускает процесс заполнения "дерева сотрудников" 
		/// </summary>
		/// <remarks>В зависимости от выбора заполнение происходит по отделам либо по группам</remarks>
		private void miDepart_Click(object sender, System.EventArgs e)
		{
			((MenuItem)sender).Checked=true;
			if(((MenuItem)sender).Text=="По отделам")
			{
				fillTree(0,tvEmpl,"","");
				miGroup.Checked=false;
			}

			if(((MenuItem)sender).Text=="По группам")
			{
				fillTree(1,tvEmpl,"","");
				miDepart.Checked=false;
			}
		}
		/// <summary>
		/// Обработчик события загрузки формы
		/// </summary>
		///<remarks>Иницирует заполнение "дерева" сотрудников, обновляет списки отделов и групп</remarks>
		private void fmEmpl_Load(object sender, System.EventArgs e)
		{
            miDepart_Click(miDepart,null);
			refreshDepart();
			refreshGroup();
			//сколько нераспечатанных осталось в CodeToPrint
			btnToQueue.Text="("+addToQueue(0,"",false).ToString()+")";
		}
		/// <summary>
		/// Запускается после выбора сотрудника в "дереве сотрудников" и иницирует заполнение элементов управления информацией о сотруднике
		/// </summary>
		private void tvEmpl_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if((tvEmpl.SelectedNode.Parent!=null) && (tvEmpl.SelectedNode.Tag!=null))
			{
				Cursor=Cursors.WaitCursor;
				getUserByID((int)tvEmpl.SelectedNode.Tag);
				Cursor=Cursors.Default;
				pnEmplInfo.Enabled=true;
			}			
			else
				pnEmplInfo.Enabled=false;			
		}
		
		private void cbUsePassword_Click(object sender, System.EventArgs e)
		{
			if(((CheckBox)sender).Checked)
			{
				cbUsePassword.Checked=true;
				gbPassword.Enabled=true;
			}
			else
			{
				cbUsePassword.Checked=false;
				gbPassword.Enabled=false;
			}
		}

		/// <summary>
		/// Иницирует удаление сотрудника из базы
		/// </summary>		
		private void btnDelEmpl_Click(object sender, System.EventArgs e)
		{
			if((tvEmpl.SelectedNode!=null) && (tvEmpl.SelectedNode.Parent!=null) && (tvEmpl.SelectedNode.Tag!=null))
			{
				if(MessageBox.Show("Вы действительно хотите удалить работника (при этом он будет исключён из статистики)?","Предупреждение",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)==DialogResult.Yes)
				{
					string name=tvEmpl.SelectedNode.Parent.Text;
					if(delUserByID((int)tvEmpl.SelectedNode.Tag)==false) 
						MessageBox.Show("Работник не был удалён! Попробуйте ещё раз.", "Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
					else
					{
						fillTree((miDepart.Checked)?0:1,tvEmpl,name,"");
						pnEmplInfo.Enabled=false;
					}
				}
			}		
		}
		/// <summary>
		/// Иницирует добавление нового сотрудника в отдел (группу)
		/// </summary>		
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			if(tvEmpl.SelectedNode!=null)
			{
				string name="";
				if(tvEmpl.SelectedNode.Parent==null) 
					name=tvEmpl.SelectedNode.Text;
				else
					name=tvEmpl.SelectedNode.Parent.Text;

				if(name=="Прочие") return;
				
				if(AddUser(name,(miDepart.Checked)?0:1)==false)
					MessageBox.Show("Не удалось добавить работника", "Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				else
					fillTree((miDepart.Checked)?0:1,tvEmpl,name,"");				
			}
		}

		/// <summary>
		/// Иницирует сохранение изменённых данных о сотруднике
		/// </summary>	
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if((tvEmpl.SelectedNode!=null) && (tvEmpl.SelectedNode.Parent!=null) && (tvEmpl.SelectedNode.Tag!=null))
			{
				if((cbDepart.SelectedIndex==-1) || (cbGroup.SelectedIndex==-1))
					MessageBox.Show("Обязательно укажите отдел и группу!","Предупреждение",MessageBoxButtons.OK,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
				else
				{
					string name="",nd="";

					if(tvEmpl.SelectedNode.Parent==null) 
						name=tvEmpl.SelectedNode.Text;
					else
					{
						name=tvEmpl.SelectedNode.Parent.Text;
						nd=tvEmpl.SelectedNode.Text;
					}
					

					if(name=="Прочие") return;
					
					Cursor=Cursors.WaitCursor;
					if(changeUserByID((int)tvEmpl.SelectedNode.Tag)==false) MessageBox.Show("Не удалось изменить информацию о работнике", "Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
					else fillTree((miDepart.Checked)?0:1,tvEmpl,name,nd);
				
					Cursor=Cursors.Default;
				}
			}
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void btnNewCode_Click(object sender, System.EventArgs e)
		{
			tbCode.Text=getNewCode(tbSurname.Text+tbName.Text+tbSecname.Text);
		}

		private void btnToQueue_Click(object sender, System.EventArgs e)
		{
			//добавляем в таблицу для печати
			if(tbCode.Text!="")
				btnToQueue.Text="("+addToQueue((int)tvEmpl.SelectedNode.Tag,tbCode.Text,true)+")";
			else
				MessageBox.Show("Не был указан штрих-код!","Предупреждение",MessageBoxButtons.OK,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
		}

		private void btnDelQueue_Click(object sender, System.EventArgs e)
		{
			if(delQueue()==true)				
				btnToQueue.Text="(0)";
		}
		/// <summary>
		/// Печатае карточки со штрих-кодом из "очереди печати" 
		/// </summary>	
		private void btnPrintQueue_Click(object sender, System.EventArgs e)
		{
			DataSet sqlDS=null;
			try
			{
					SqlDataAdapter sqlDA=new SqlDataAdapter("SELECT FIO,CODE,DEPARTMENT,GRUPA,CODEPRINT,PHOTO FROM CodeToPrint",conStr);
				    sqlDA.SelectCommand.CommandTimeout=60;
					sqlDS = new DataSet();
					sqlDA.Fill(sqlDS);					
					crCards cr=new crCards();				
					ConnectionInfo crConInfo=new ConnectionInfo(); 
					TableLogOnInfo crTableLogOnInfo=new TableLogOnInfo();
					crConInfo.ServerName=CDbCreator.getServerCon(conStr);
					crConInfo.DatabaseName=CDbCreator.getDBCon(conStr);
					crConInfo.UserID=CDbCreator.getUserCon(conStr);
					crConInfo.Password=CDbCreator.getPasswCon(conStr);
					Tables crTables=cr.Database.Tables;					
					foreach(Table t in crTables)
					{
						crTableLogOnInfo=t.LogOnInfo;
						crTableLogOnInfo.ConnectionInfo=crConInfo;
						t.ApplyLogOnInfo(crTableLogOnInfo);
					}
					cr.SetDataSource(sqlDS);
					fmRep codeRep=new fmRep(cr);
					codeRep.ShowDialog();				
			}
			catch(Exception ex)
			{
				MessageBox.Show("Не удалось распечатать штрих-кода! "+ex.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
			}
		}

		private void btnClose_Click_1(object sender, System.EventArgs e)
		{
			Close();
		}

		private void miSearch_Click(object sender, System.EventArgs e)
		{
			fmEmplSearch f=new fmEmplSearch();
			if((f.ShowDialog()==DialogResult.OK) && (f.fio!="")) 
			{
				bool found=false;
				foreach(TreeNode pn in tvEmpl.Nodes)
					foreach(TreeNode n in pn.Nodes)
						if(n.Text.ToUpper().IndexOf(f.fio.ToUpper())>=0)
						{
							tvEmpl.SelectedNode=n;
							found=true;
							break;
						}
				if(!found) MessageBox.Show("Сотрудник не найден! ","Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);
			}
		}

		private void pbPhoto_Click(object sender, System.EventArgs e)
		{
			if(ofdPhoto.ShowDialog()==DialogResult.OK)
			{
				Bitmap bm=new Bitmap(ofdPhoto.FileName);
				pbPhoto.Image=bm;
				lbPhoto.Hide();
			}
		}

		private void pbPhoto_Click_1(object sender, System.EventArgs e)
		{
		
		}

        private void btnEdit_Click(object sender, EventArgs e)
        {
            fmEmplEdit edit = new fmEmplEdit(conStr, tbCode.Text);
            edit.ShowDialog();
        }

        private void cbDepart_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

		
	}
}
