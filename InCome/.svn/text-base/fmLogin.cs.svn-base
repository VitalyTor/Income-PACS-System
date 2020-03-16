using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace InCome
{
	/// <summary>
	/// Форма для авторизации сотрудника при регистрации входа/выхода
	/// </summary>
	/// <remarks>Авторизация осуществляется путём сверки заданого сотрудником логина и пароля с теми, которые хранятся в базе</remarks>
	public class fmLogin : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnConf;
		private System.Windows.Forms.Button btnCancel;
		private string login;
		private string password;
		/// <summary>
		/// Результат авторизации: true - успешно авторизирован; false - нет
		/// </summary>
		public bool result=false;		
		private System.ComponentModel.Container components = null;	
		private System.Windows.Forms.TextBox tbLogin;
		private System.Windows.Forms.TextBox tbPassword;
		
		
		/// <summary>
		/// Конструктор формы
		/// </summary>
		/// <param name="flogin">Логин сотрудника</param>
		/// <param name="fpassword">Пароль сотрудника</param>
		public fmLogin(string flogin,string fpassword)
		{		
			InitializeComponent();
			login=flogin;
			password=fpassword;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(fmLogin));
			this.btnConf = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbLogin = new System.Windows.Forms.TextBox();
			this.tbPassword = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnConf
			// 
			this.btnConf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.btnConf.Image = ((System.Drawing.Image)(resources.GetObject("btnConf.Image")));
			this.btnConf.Location = new System.Drawing.Point(18, 112);
			this.btnConf.Name = "btnConf";
			this.btnConf.Size = new System.Drawing.Size(117, 32);
			this.btnConf.TabIndex = 0;
			this.btnConf.Text = "Подтвердить";
			this.btnConf.Click += new System.EventHandler(this.btnConf_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
			this.btnCancel.Location = new System.Drawing.Point(146, 112);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(128, 32);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
			this.label1.Location = new System.Drawing.Point(8, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "Логин";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
			this.label2.Location = new System.Drawing.Point(8, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 24);
			this.label2.TabIndex = 3;
			this.label2.Text = "Пароль";
			// 
			// tbLogin
			// 
			this.tbLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.tbLogin.Location = new System.Drawing.Point(96, 16);
			this.tbLogin.Name = "tbLogin";
			this.tbLogin.Size = new System.Drawing.Size(184, 31);
			this.tbLogin.TabIndex = 4;
			this.tbLogin.Text = "";
			// 
			// tbPassword
			// 
			this.tbPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.tbPassword.Location = new System.Drawing.Point(96, 63);
			this.tbPassword.Name = "tbPassword";
			this.tbPassword.PasswordChar = '*';
			this.tbPassword.Size = new System.Drawing.Size(184, 31);
			this.tbPassword.TabIndex = 5;
			this.tbPassword.Text = "";
			// 
			// fmLogin
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(288, 151);
			this.Controls.Add(this.tbPassword);
			this.Controls.Add(this.tbLogin);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnConf);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "fmLogin";
			this.Text = "Подтверждение паролем";
			this.Activated += new System.EventHandler(this.fmLogin_Activated);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnConf_Click(object sender, System.EventArgs e)
		{
			if((tbLogin.Text!="") && (tbPassword.Text!="") && (tbLogin.Text==login) && (tbPassword.Text==password))
			{
				result=true;
				Close();
			}
			else result=false;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			result=false;
			Close();
		}

		private void fmLogin_Activated(object sender, System.EventArgs e)
		{
			tbLogin.Focus();
		}
	}
}
