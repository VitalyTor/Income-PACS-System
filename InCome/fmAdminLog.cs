using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace InCome
{
	/// <summary>
	/// Форма для подтверждения прав администратора
	/// </summary>
	/// <remarks>Также позволяет сменить пароль администратора</remarks>
	public class fmAdminLog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbPassword;
		private System.Windows.Forms.TextBox tbNewPassword;
		private System.Windows.Forms.TextBox tbNewPasswordConf;
		private System.Windows.Forms.Button btnLog;
		private System.Windows.Forms.Button btnChangePassword;
		private System.Windows.Forms.Button bntSetPassword;
		private System.Windows.Forms.Button btnCancelNewPassword;	
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnCancel;

		/// <summary>
		/// Содержит информацию о строке соединения с базой и версии SQL-сервера 
		/// </summary>
		private CDbCreator fdcr;
		/// <summary>
		/// Результат проверки прав, имеет значение true, если подтвержданы права администратора
		/// </summary>
		private bool isLog=false;

		public fmAdminLog()
		{			
			InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmAdminLog));
            this.btnLog = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bntSetPassword = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNewPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbNewPasswordConf = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCancelNewPassword = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLog
            // 
            this.btnLog.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLog.BackgroundImage")));
            this.btnLog.Location = new System.Drawing.Point(238, 6);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(48, 23);
            this.btnLog.TabIndex = 2;
            this.btnLog.Text = "LOGIN";
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(98, 8);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(133, 20);
            this.tbPassword.TabIndex = 1;
            this.tbPassword.Text = "tanagra18";
            this.tbPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPassword_KeyDown);
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChangePassword.BackgroundImage")));
            this.btnChangePassword.Location = new System.Drawing.Point(287, 6);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(104, 23);
            this.btnChangePassword.TabIndex = 3;
            this.btnChangePassword.Text = "Change PASS";
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "PASS";
            // 
            // bntSetPassword
            // 
            this.bntSetPassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bntSetPassword.BackgroundImage")));
            this.bntSetPassword.Location = new System.Drawing.Point(239, 39);
            this.bntSetPassword.Name = "bntSetPassword";
            this.bntSetPassword.Size = new System.Drawing.Size(145, 23);
            this.bntSetPassword.TabIndex = 7;
            this.bntSetPassword.Text = "SAVE NEW PASS";
            this.bntSetPassword.Click += new System.EventHandler(this.bntSetPassword_Click);
            // 
            // label2
            // 
            this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
            this.label2.Location = new System.Drawing.Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "NEW PASS";
            // 
            // tbNewPassword
            // 
            this.tbNewPassword.Location = new System.Drawing.Point(97, 37);
            this.tbNewPassword.Name = "tbNewPassword";
            this.tbNewPassword.PasswordChar = '*';
            this.tbNewPassword.Size = new System.Drawing.Size(133, 20);
            this.tbNewPassword.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.Location = new System.Drawing.Point(8, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 14);
            this.label3.TabIndex = 8;
            this.label3.Text = "RETYPE";
            // 
            // tbNewPasswordConf
            // 
            this.tbNewPasswordConf.Location = new System.Drawing.Point(97, 64);
            this.tbNewPasswordConf.Name = "tbNewPasswordConf";
            this.tbNewPasswordConf.PasswordChar = '*';
            this.tbNewPasswordConf.Size = new System.Drawing.Size(133, 20);
            this.tbNewPasswordConf.TabIndex = 6;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.Location = new System.Drawing.Point(392, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancelNewPassword
            // 
            this.btnCancelNewPassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancelNewPassword.BackgroundImage")));
            this.btnCancelNewPassword.Location = new System.Drawing.Point(392, 39);
            this.btnCancelNewPassword.Name = "btnCancelNewPassword";
            this.btnCancelNewPassword.Size = new System.Drawing.Size(64, 23);
            this.btnCancelNewPassword.TabIndex = 8;
            this.btnCancelNewPassword.Text = "CANCEL";
            this.btnCancelNewPassword.Click += new System.EventHandler(this.btnCancelNewPassword_Click);
            // 
            // fmAdminLog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(458, 93);
            this.Controls.Add(this.btnCancelNewPassword);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbNewPasswordConf);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbNewPassword);
            this.Controls.Add(this.bntSetPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.btnLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmAdminLog";
            this.Text = "Admin Login";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.fmAdminLog_Activated);
            this.Load += new System.EventHandler(this.fmAdminLog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		/// <summary>
		/// Иницирует процесс проверки административных прав
		/// </summary>
		/// <param name="dcr">Описывает строку соединения и тип версию SQL-сервера</param>
		/// <returns></returns>
		public bool logAdmin(ref CDbCreator dcr)
		{
			fdcr=dcr;            
			ShowDialog();
			dcr=fdcr;			
			return isLog;
		}

		private void btnChangePassword_Click(object sender, System.EventArgs e)
		{
			btnLog.Visible=false;
			btnChangePassword.Visible=false;
			btnCancel.Visible=false;		
			Height=120;
		}

		private void fmAdminLog_Load(object sender, System.EventArgs e)
		{
			Height=60; 
			isLog=false;		
		}

		private void btnLog_Click(object sender, System.EventArgs e)
		{
			//сверяем введённый пароль с тем, который в строке соединения
			if((tbPassword.Text!="") && 
               (fdcr.conStr.IndexOf("Password="+tbPassword.Text+";")>=0 ||
                checkAllUsers(tbPassword.Text))) 
			{
				isLog=true;
                fmMain.currentUser = tbPassword.Text;
				Close();
			}
			else 
			{
				MessageBox.Show("Пароль неверный!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);			
				isLog=false;
			}
		}

        private bool checkAllUsers(string pwd)
        {
            bool res;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = fmMain.dcr.conStr;

            try
            {
                con.Open();
                SqlCommand sqlCom = new SqlCommand(
                    "SELECT Count(*) " +
                    "FROM Users WHERE usrPswd = '" + pwd + "'", con);
                sqlCom.CommandTimeout = 60;
                sqlCom.CommandType = CommandType.Text;
                res = ((int)sqlCom.ExecuteScalar()>0);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                res = false;
            }
            finally
            {
                con.Close();
            }

            return res;
        }
		
		private void button1_Click(object sender, System.EventArgs e)
		{
			Close();
		}
		/// <summary>
		/// Устанавливает новый пароль администратора
		/// </summary>
		///<remarks>Изменённая строка соединения с базой записывается в реестр</remarks>
		private void bntSetPassword_Click(object sender, System.EventArgs e)
		{
			//проверяем пароль 
			if((tbPassword.Text!="") && (fdcr.conStr.IndexOf("Password="+tbPassword.Text)>=0) && (tbNewPassword.Text!="") && (tbNewPassword.Text==tbNewPasswordConf.Text)) 
			{
				//меняем пароль в базе
				if(fdcr.SetPassword(tbPassword.Text,tbNewPassword.Text,"InComeUser")==false)
				{
					MessageBox.Show("Не удалось поменять пароль администратора!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
					return;
				}
				else //сохраняем соединение в реестре
				 if(fdcr.setRegValue("SOFTWARE\\InCome","connection",fdcr.conStr)==false)
				 {
						MessageBox.Show("Не удалось записать строку соединения с новым паролем в реестр!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
						return;
				 }

				MessageBox.Show("Пароль был изменён!","Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);
				tbPassword.Text=tbNewPassword.Text=tbNewPasswordConf.Text="";
				btnLog.Visible=true;
				btnChangePassword.Visible=true;
				btnCancel.Visible=true;	
				Height=60;
			}
			else
				MessageBox.Show("Введён неправильный старый пароль либо новый пароль подтверждён неверно!","Предупреждение",MessageBoxButtons.OK,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);			
		}

		private void btnCancelNewPassword_Click(object sender, System.EventArgs e)
		{
			btnLog.Visible=true;
			btnChangePassword.Visible=true;
			btnCancel.Visible=true;		
			Height=60;
		}

		private void fmAdminLog_Activated(object sender, System.EventArgs e)
		{
			tbPassword.Focus();
		}

		private void tbPassword_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if((btnLog.Visible) & (e.KeyCode==Keys.Enter))
				btnLog_Click(btnLog,null);
		}
	}
}
