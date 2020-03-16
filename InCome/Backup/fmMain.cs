using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using Microsoft.Win32;
using System.IO;


namespace InCome
{
	/// <summary>
	/// Главная форма приложения
	/// </summary>
	/// <remarks>Содержит элементы управления для регистрации сотрудников и открытия других форм приложения</remarks>
	
	public class fmMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbCode;
		private System.Windows.Forms.Label lbMsg;		
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.PictureBox pb;
		private System.Timers.Timer timer;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnNewDB;
		private System.Windows.Forms.Button btnStat;
		private System.Windows.Forms.Button btnEmpl;
		private System.Windows.Forms.Label lbFio;
		private System.Windows.Forms.PictureBox pbPhoto;
		private System.Windows.Forms.HelpProvider helpProvider;
		/// <summary>
		/// Описывает строку соединения с базой данных и версию SQL-сервера
		/// </summary>
		private CDbCreator dcr;

		/// <summary>
		/// Конструктор формы по-умолчанию
		/// </summary>
		public fmMain()
		{
			InitializeComponent();
			try
			{				
				helpProvider.HelpNamespace=Environment.CurrentDirectory+"\\InComeHelp.chm";
				helpProvider.SetShowHelp(this,true);
			}
			catch{}
		}
		
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if( disposing )
			{
				if (components != null) 
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(fmMain));
			this.pb = new System.Windows.Forms.PictureBox();
			this.btnGo = new System.Windows.Forms.Button();
			this.tbCode = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lbMsg = new System.Windows.Forms.Label();
			this.timer = new System.Timers.Timer();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnNewDB = new System.Windows.Forms.Button();
			this.btnStat = new System.Windows.Forms.Button();
			this.btnEmpl = new System.Windows.Forms.Button();
			this.lbFio = new System.Windows.Forms.Label();
			this.pbPhoto = new System.Windows.Forms.PictureBox();
			this.helpProvider = new System.Windows.Forms.HelpProvider();
			((System.ComponentModel.ISupportInitialize)(this.timer)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pb
			// 
			this.pb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pb.Image = ((System.Drawing.Image)(resources.GetObject("pb.Image")));
			this.pb.Location = new System.Drawing.Point(0, 0);
			this.pb.Name = "pb";
			this.pb.Size = new System.Drawing.Size(832, 640);
			this.pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pb.TabIndex = 2;
			this.pb.TabStop = false;
			// 
			// btnGo
			// 
			this.btnGo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnGo.Image = ((System.Drawing.Image)(resources.GetObject("btnGo.Image")));
			this.btnGo.Location = new System.Drawing.Point(517, 358);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(36, 36);
			this.btnGo.TabIndex = 1;
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// tbCode
			// 
			this.tbCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.tbCode.AutoSize = false;
			this.tbCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.tbCode.Location = new System.Drawing.Point(160, 359);
			this.tbCode.Name = "tbCode";
			this.tbCode.Size = new System.Drawing.Size(344, 32);
			this.tbCode.TabIndex = 0;
			this.tbCode.Text = "";
			this.tbCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCode_KeyPress);
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.BackColor = System.Drawing.Color.Gainsboro;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label1.Location = new System.Drawing.Point(40, 364);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 24);
			this.label1.TabIndex = 8;
			this.label1.Text = "Штрихкод";
			// 
			// lbMsg
			// 
			this.lbMsg.AutoSize = true;
			this.lbMsg.BackColor = System.Drawing.Color.Gainsboro;
			this.lbMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.lbMsg.ForeColor = System.Drawing.Color.Red;
			this.lbMsg.Image = ((System.Drawing.Image)(resources.GetObject("lbMsg.Image")));
			this.lbMsg.Location = new System.Drawing.Point(8, 48);
			this.lbMsg.Name = "lbMsg";
			this.lbMsg.Size = new System.Drawing.Size(0, 58);
			this.lbMsg.TabIndex = 12;
			// 
			// timer
			// 
			this.timer.Interval = 1500;
			this.timer.SynchronizingObject = this;
			this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);
			// 
			// groupBox1
			// 
			this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
			this.groupBox1.Controls.Add(this.btnNewDB);
			this.groupBox1.Controls.Add(this.btnStat);
			this.groupBox1.Controls.Add(this.btnEmpl);
			this.groupBox1.Location = new System.Drawing.Point(3, -3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(109, 43);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			// 
			// btnNewDB
			// 
			this.btnNewDB.Image = ((System.Drawing.Image)(resources.GetObject("btnNewDB.Image")));
			this.btnNewDB.Location = new System.Drawing.Point(77, 12);
			this.btnNewDB.Name = "btnNewDB";
			this.btnNewDB.Size = new System.Drawing.Size(25, 25);
			this.btnNewDB.TabIndex = 7;
			this.btnNewDB.Click += new System.EventHandler(this.btnNewDB_Click);
			// 
			// btnStat
			// 
			this.btnStat.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStat.BackgroundImage")));
			this.btnStat.Location = new System.Drawing.Point(41, 12);
			this.btnStat.Name = "btnStat";
			this.btnStat.Size = new System.Drawing.Size(25, 25);
			this.btnStat.TabIndex = 6;
			this.btnStat.Click += new System.EventHandler(this.pbStat_Click);
			// 
			// btnEmpl
			// 
			this.btnEmpl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEmpl.BackgroundImage")));
			this.btnEmpl.Location = new System.Drawing.Point(5, 12);
			this.btnEmpl.Name = "btnEmpl";
			this.btnEmpl.Size = new System.Drawing.Size(25, 25);
			this.btnEmpl.TabIndex = 5;
			this.btnEmpl.Click += new System.EventHandler(this.pbSets_Click);
			// 
			// lbFio
			// 
			this.lbFio.AutoSize = true;
			this.lbFio.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.lbFio.ForeColor = System.Drawing.Color.Red;
			this.lbFio.Image = ((System.Drawing.Image)(resources.GetObject("lbFio.Image")));
			this.lbFio.Location = new System.Drawing.Point(8, 288);
			this.lbFio.Name = "lbFio";
			this.lbFio.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lbFio.Size = new System.Drawing.Size(0, 40);
			this.lbFio.TabIndex = 14;
			// 
			// pbPhoto
			// 
			this.pbPhoto.Image = ((System.Drawing.Image)(resources.GetObject("pbPhoto.Image")));
			this.pbPhoto.Location = new System.Drawing.Point(16, 112);
			this.pbPhoto.Name = "pbPhoto";
			this.pbPhoto.Size = new System.Drawing.Size(120, 160);
			this.pbPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbPhoto.TabIndex = 15;
			this.pbPhoto.TabStop = false;
			this.pbPhoto.Visible = false;
			// 
			// fmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(792, 573);
			this.Controls.Add(this.pbPhoto);
			this.Controls.Add(this.lbFio);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.lbMsg);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbCode);
			this.Controls.Add(this.btnGo);
			this.Controls.Add(this.pb);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(750, 550);
			this.Name = "fmMain";
			this.Text = "Система учёта рабочего времени";
			this.Load += new System.EventHandler(this.fmMain_Load);
			this.Activated += new System.EventHandler(this.fmMain_Activated);
			((System.ComponentModel.ISupportInitialize)(this.timer)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new fmMain());
		}
		/// <summary>
		/// Осуществляет регистрацию времени входа/выхода сотрудника 
		/// </summary>
		/// <param name="code">Штрих-код сотрудника</param>
		/// <returns>0 - вошёл; 1 - вышел; 2 - ошибка регистрации </returns>		
		private int LogInOut(string code,ref string fio,out Bitmap bm)
		{
			bm=null;
			try
			{
				OleDbConnection sqlCon=new OleDbConnection(dcr.conStr);
				sqlCon.Open();

				OleDbCommand sqlCom=new OleDbCommand("[LogInOut]",sqlCon);				
				sqlCom.CommandTimeout=60;
                sqlCom.CommandType=CommandType.StoredProcedure;
				sqlCom.Parameters.Add("@code",OleDbType.VarChar);
				sqlCom.Parameters.Add("@res",OleDbType.VarChar,3);
				sqlCom.Parameters.Add("@fio",OleDbType.VarChar,100);
				sqlCom.Parameters["@code"].Value=code;
				sqlCom.Parameters["@res"].Direction=ParameterDirection.Output;
				sqlCom.Parameters["@fio"].Direction=ParameterDirection.Output;
 
				object image=sqlCom.ExecuteScalar();
				string result=Convert.ToString(sqlCom.Parameters["@res"].Value);
				fio=Convert.ToString(sqlCom.Parameters["@fio"].Value);
                sqlCon.Close();

				byte [] photo=(image==Convert.DBNull)?new byte[0]:(byte []) image;
				if(photo.Length!=0)
					using(MemoryStream ms=new MemoryStream(photo))
						bm=new Bitmap(ms);
													
				if(result=="NON") 
				{
				    MessageBox.Show("Возникла ошибка в процессе регистрации сотрудника!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
					return 2;
				}
				if(result=="IN") return 0;
				if(result=="OUT") return 1;				
			}
			catch(Exception e)
			{
				MessageBox.Show("Возникла ошибка в процессе регистрации сотрудника! "+e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				return 2;
			}					    
			   

          return 2;  
		}

		/// <summary>
		/// Проверяет штрих-код сотрудника
		/// </summary>
		/// <param name="code">Штрих-код сотрудника</param>
		/// <param name="login">Логин сотрудника (если задан)</param>
		/// <param name="password">Пароль сотрудника (если задан)</param>
		/// <returns>0 - штрих-код правильный; 1 - для регистрации дополнительно нужно задать логин и пароль сотрудника; 3 - неправильное КЧ в штрих-коде; 4 - штрих-код не найден в базе; 5 - ошибка при проверке</returns>		*/
		private int CheckCode(string code,ref string login,ref string password)
		{
			if(fmEmpl.isKontrSumRight(code)==false) //проверка КС
			{
				MessageBox.Show("Неправильная контрольная сумма штрих-кода!","Предупреждение",MessageBoxButtons.OK,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
                return 3;
        	}

			try
			{
				OleDbDataAdapter sqlDA=new OleDbDataAdapter("SELECT TOP 1 MCODE,MLOGIN,MPASSWORD,MUSEPASSW,MLOCKED FROM EmplMain where MCODE='"+code+"'",dcr.conStr);
				sqlDA.SelectCommand.CommandTimeout=30;
				DataSet sqlDS = new DataSet();
				sqlDA.Fill(sqlDS);
				if(sqlDS.Tables[0].Rows.Count==0) 
				{
					MessageBox.Show("Введён несуществующий штрих-код!","Предупреждение",MessageBoxButtons.OK,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
					return 4;
				}
                
				if((Convert.IsDBNull(sqlDS.Tables[0].Rows[0].ItemArray[4])?"NO":Convert.ToString(sqlDS.Tables[0].Rows[0].ItemArray[4]))=="YES") 
				{				    
					return 2;
				}
				if((Convert.IsDBNull(sqlDS.Tables[0].Rows[0].ItemArray[3])?"NO":Convert.ToString(sqlDS.Tables[0].Rows[0].ItemArray[3]))=="YES") 
				{
					login=Convert.IsDBNull(sqlDS.Tables[0].Rows[0].ItemArray[1])?"":Convert.ToString(sqlDS.Tables[0].Rows[0].ItemArray[1]);
					password=Convert.IsDBNull(sqlDS.Tables[0].Rows[0].ItemArray[2])?"":Convert.ToString(sqlDS.Tables[0].Rows[0].ItemArray[2]);
					return 1;
				}
				
			}				
			catch(Exception e)
			{
                MessageBox.Show("Не удалось проверить штрих-код! Попробуйте ещё раз. "+e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				return 5;			
			}


			return 0;
		}

		/// <summary>
		/// Обработчик события загрузки главной формы приложения
		/// </summary>
		/// <remarks>Проверяет реестр на наличие строки соединения с базой. В случае, когда строки соединения нет либо она задана неверно - предлагается проверить наличие базы InCome на сервере: </remarks>
		/// <remarks> - если базы нет, то создаётся новая</remarks>
		/// <remarks> - если база есть, то строка соединения записывается в реестр</remarks>
		private void fmMain_Load(object sender, System.EventArgs e)
		{	
			dcr=new CDbCreator("SOFTWARE\\InCome");
		   
			if((dcr.conStr=="") || (dcr.sqlVer==SqlVer.UNKNOWN) || (dcr.isDBase(dcr.conStr,dcr.sqlVer,"InCome")!=1)) //запустили впервые либо не задана строка соединения с базой
			{
				//задаём строку соединения с таблицей master MS SQL Server
		        MessageBox.Show("Не была найдена строка соединения с базой InCome либо сама база. Возможно приложение запускается в первый раз на данном компьютере. Для определения наличия базы задайте строку соединения с базой master вашего MS SQL Server (при этом вы должны иметь административные права для записи в реестр)!","Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);
				if(dcr.setConStr()!=true) //не задана строка соединения
				{
					MessageBox.Show("Не была задана строка соединения. Приложение не может продолжать работу!","Предупреждение",MessageBoxButtons.OK,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
					this.Close();
					return;
				}
				else //задана строка соединения
				{
					//проверка, есть ли база
					int isBase=dcr.isDBase(dcr.conStr,dcr.sqlVer,"InCome");
					bool crBase=false;
					switch(isBase)
					{
						case -1:
							if(MessageBox.Show("Не удалось установить, существует ли уже база InCome на заданном MS SQL Server! Создать базу заново (при этом существующая база, если она есть, будет удалена)? ","Предупреждение",MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2)==DialogResult.Yes)
								crBase=true;
							break;
						case 0: if(MessageBox.Show("База InCome на заданном MS SQL Server не найдена! Создать базу заново? ","Предупреждение",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1)==DialogResult.Yes)
									crBase=true;
								else    
								{
									MessageBox.Show("Приложение не может продолжать работу без базы InCome. Создайте базу на выбранном MS SQL Server либо укажите сервер, где она уже существует!","Предупреждение",MessageBoxButtons.OK,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
									this.Close();
								}
							break;
						case 1: 
							if(MessageBox.Show("База InCome на заданном MS SQL Server уже существует! ","Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1)==DialogResult.Yes)
								crBase=false;
							break;
					}			
					//если нужно создать базу
					if((crBase==true) && ((isBase==-1) || (isBase==0)))
					{
						Cursor=Cursors.WaitCursor;
						Refresh();
						if(dcr.createDB()==false)
						{
							MessageBox.Show("Не удалось создать базу InCome на выбранном MS SQL Server. Приложение не может продолжать работать!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
							this.Close();
							return;
						}
						else//пишем в реестр строку соединения и версию
						{
							//подменяем пользователя и базу на стандартные
							int p1=dcr.conStr.IndexOf("User ID=");
							int p2=dcr.conStr.IndexOf("Data Source=");						   
							dcr.conStr=dcr.conStr.Replace(dcr.conStr.Substring(p1,p2-p1),"User ID=InComeUser;Initial Catalog=InCome;");

							if((dcr.setRegValue("SOFTWARE\\InCome","connection",dcr.conStr)==false) || (dcr.setRegValue("SOFTWARE\\InCome","sqlver",Convert.ToString((int)dcr.sqlVer))==false))					    
							{
								MessageBox.Show("Не удалось записать настройки соединения с базой InCome в реестр. Попробуйте перезапустить приложение и повторить настройку!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
								this.Close();
								return;
							}
							MessageBox.Show("Создана база InCome!","Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);
						}
						Cursor=Cursors.Default;
						Refresh();
					}
				    //если нужно только задать строку соединения и записать её в реестр
					else
					{
						MessageBox.Show("Для продолжения работы укажите строку соединения с базой InCome для пользователя InComeUser (при этом вы должны иметь административные права для записи в реестр)!","Предупреждение",MessageBoxButtons.OK,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
						if(dcr.setConStr()!=true) //не задана строка соединения
						{
							MessageBox.Show("Не была задана строка соединения. Приложение не может продолжать работу!","Предупреждение",MessageBoxButtons.OK,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
							this.Close();
							return;
						}
						if(CDbCreator.getUserCon(dcr.conStr).ToUpper()!="INCOMEUSER")
						{
							MessageBox.Show("Задан отличный от InComeUser пользователь базы InCome. Приложение не может продолжать работу!","Предупреждение",MessageBoxButtons.OK,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
							this.Close();
							return;
						}
						if((dcr.setRegValue("SOFTWARE\\InCome","connection",dcr.conStr)==false) || (dcr.setRegValue("SOFTWARE\\InCome","sqlver",Convert.ToString((int)dcr.sqlVer))==false))					    
						{
							MessageBox.Show("Не удалось записать настройки соединения с базой InCome в реестр. Попробуйте перезапустить приложение и повторить настройку!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
							this.Close();
							return;
						}
						MessageBox.Show("Была задана строка соединения с базой InCome!","Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);
						Refresh();					
					}
				}
			}			
		}
        /// <summary>
        /// Вызывает форму для редактирования данных о сотрудниках
        /// </summary>
        /// <remarks>Предварительно запускает процедуру автризации пользователя</remarks>
		private void pbSets_Click(object sender, System.EventArgs e)
		{
//			fmAdminLog adminLog=new fmAdminLog();			
//			if(adminLog.logAdmin(ref dcr)==true) 
//			{
				fmEmpl empl=new fmEmpl(dcr.conStr);
				empl.ShowDialog();
//			}
//			tbCode.Focus();
		}
		/// <summary>
		/// Вызывает форму со статистикой
		/// </summary>
		/// <remarks>Предварительно запускает процедуру автризации пользователя</remarks>
		private void pbStat_Click(object sender, System.EventArgs e)
		{
			//fmAdminLog adminLog=new fmAdminLog();			
//			if(adminLog.logAdmin(ref dcr)==true) 
//			{
				fmStat stat=new fmStat(dcr.conStr);
				stat.ShowDialog();
//			}
		    tbCode.Focus();
		}
		/// <summary>
		/// Запускает процесс регистрации сотрудника
		/// </summary>
		/// <remarks>Сперва проверяется штрих-код, потом происходит собственно регистрация</remarks>
		private void btnGo_Click(object sender, System.EventArgs e)
		{			
			
			string c=tbCode.Text,login="",password="";
			if(c.Length==17)
			{
				int result=0;
				lbMsg.Text="";
				lbFio.Text="";				
				string fio="";
				Bitmap bm;
				switch (CheckCode(c,ref login,ref password))
				{
					case 0: 
							result=LogInOut(c,ref fio,out bm); //войти/выйти
							pbPhoto.Image=bm;
							if(result==0)  
							{
								lbMsg.ForeColor=Color.Red;
								lbMsg.Text="Вход"; 
							}
							if(result==1)
							{
								lbMsg.ForeColor=Color.Blue;
								lbMsg.Text="Выход";
							}
							lbFio.Text=fio;
						break;
					case 1: fmLogin fmlogin=new fmLogin(login,password);
						fmlogin.ShowDialog();
						if(fmlogin.result==true) 
						{	
							result=LogInOut(c,ref fio,out bm);
							pbPhoto.Image=bm;
							if(result==0)  
							{
								lbMsg.ForeColor=Color.Red;
								lbMsg.Text="Вход"; 
							}
							if(result==1)
							{
								lbMsg.ForeColor=Color.Blue;
								lbMsg.Text="Выход";
							}
							lbFio.Text=fio;
						}						
						break;
					case 2: lbMsg.Text="Заблокирован";					
						break;
				}
				lbFio.Show();
				lbMsg.Show();
				pbPhoto.Show();
				timer.Enabled=true;			
			}	
			tbCode.Text="";
			tbCode.Focus();
		}

		private void fmMain_Activated(object sender, System.EventArgs e)
		{
			tbCode.Focus();
			pbStat_Click(sender, e);
		}

		private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			lbMsg.Hide();
			lbFio.Hide();
			pbPhoto.Hide();
			timer.Enabled=false;
		}

		private void tbCode_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(Convert.ToInt32(e.KeyChar)==13)
				btnGo_Click(null,null);			
		}
		/// <summary>
		/// Создаёт новую базу InCome либо только прописывает в реестре строку соединения с базой
		/// </summary>
		/// <remarks>Предварительно запускает процедуру автризации пользователя</remarks>
		private void btnNewDB_Click(object sender, System.EventArgs e)
		{
			fmAdminLog adminLog=new fmAdminLog();			
			if(adminLog.logAdmin(ref dcr)==true) 
				//если пересоздаем базу
				if(MessageBox.Show("Вы действительно хотите пересоздать базу InCome (при этом вся существующая информация будет потеряна, административный пароль изменится на стандартный)? Если да, удостоверьтесь, что база в данный момент никем не используется","Предупреждение",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1)==DialogResult.Yes)
				{
					dcr=new CDbCreator("SOFTWARE\\InCome");
					if(dcr.setConStr()!=true) //не задана строка соединения
						MessageBox.Show("Не была задана строка соединения c MS SQL Server!","Предупреждение",MessageBoxButtons.OK,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
					else
					{				
						Cursor=Cursors.WaitCursor;
						Refresh();
						if(dcr.createDB()==false)
						{
							MessageBox.Show("Не удалось создать базу InCome на выбранном MS SQL Server. Приложение не может продолжать работать!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
							this.Close();
							return;
						}
						else//пишем в реестр строку соединения и версию
						{
							//подменяем пользователя и базу на стандартные
							int p1=dcr.conStr.IndexOf("User ID=");
							int p2=dcr.conStr.IndexOf("Data Source=");						   
							dcr.conStr=dcr.conStr.Replace(dcr.conStr.Substring(p1,p2-p1),"User ID=InComeUser;Initial Catalog=InCome;");

							if((dcr.setRegValue("SOFTWARE\\InCome","connection",dcr.conStr)==false) || (dcr.setRegValue("SOFTWARE\\InCome","sqlver",Convert.ToString((int)dcr.sqlVer))==false))					    
							{
								MessageBox.Show("Не удалось записать настройки соединения с базой InCome в реестр. Попробуйте перезапустить приложение и повторить настройку!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
								this.Close();
								return;
							}
							MessageBox.Show("Создана база InCome!","Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);
						}
						Cursor=Cursors.Default;
						Refresh();
					}
				}
				else
					//если меняем только строку соединения
					if(MessageBox.Show("Вы хотите изменить строку соединения с базой?","Предупреждение",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)==DialogResult.Yes)
					{
						if(dcr.setConStr()!=true) //не задана строка соединения
							MessageBox.Show("Не была задана строка соединения c MS SQL Server!","Предупреждение",MessageBoxButtons.OK,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
						else//пишем в реестр строку соединения и версию
						{
							//подменяем пользователя и базу на стандартные
							int p1=dcr.conStr.IndexOf("User ID=");
							int p2=dcr.conStr.IndexOf("Data Source=");						   
							dcr.conStr=dcr.conStr.Replace(dcr.conStr.Substring(p1,p2-p1),"User ID=InComeUser;Initial Catalog=InCome;");

							if((dcr.setRegValue("SOFTWARE\\InCome","connection",dcr.conStr)==false) || (dcr.setRegValue("SOFTWARE\\InCome","sqlver",Convert.ToString((int)dcr.sqlVer))==false))					    
							{
								MessageBox.Show("Не удалось записать настройки соединения с базой InCome в реестр. Попробуйте перезапустить приложение и повторить настройку!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
								this.Close();
							}
							MessageBox.Show("Строка соединения изменена!","Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);
						}
					}
			tbCode.Focus();		
		}
	}
}
