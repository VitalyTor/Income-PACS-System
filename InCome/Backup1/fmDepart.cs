using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace InCome
{
	/// <summary>
	/// Форма для редактирования отделов
	/// </summary>
	public class fmDepart : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
	    /// <summary>
	    /// Набор данных, содержащий информацию об отделах
	    /// </summary>	    
		private DataSet dsDepart=null;
		/// <summary>
		/// Строка соединения с базой
		/// </summary>
		private string conStr;
		private System.Windows.Forms.Button bntAdd;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnChange;
		private System.Windows.Forms.Button btnDel;
		private System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.ComboBox cbTopDepart;
		private System.Windows.Forms.TextBox tbPhone;
		private System.Windows.Forms.ComboBox cbManager;
		private System.Windows.Forms.DataGrid dgDepart;		
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Конструктор формы
		/// </summary>
		/// <remarks>Задаёт строку соединения с базой</remarks>
		/// <param name="fconStr">Строка соединения с базой</param>
		public fmDepart(string fconStr)
		{
			InitializeComponent();
			conStr=fconStr;
		}
		/// <summary>
		/// Возвращает названия и идентификаторы всех отделов
		/// </summary>
		/// <param name="conStr">Строка соединения с базой</param>
		/// <returns>Массив с данными</returns>
		public  static string [][] getDeparts(string conStr)
		{
			string [][] res;
			try
			{
				OleDbDataAdapter sqlDA=new OleDbDataAdapter("select DPNAME,DPID from EmplDepart order by DPNAME",conStr);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(fmDepart));
			this.dgDepart = new System.Windows.Forms.DataGrid();
			this.label1 = new System.Windows.Forms.Label();
			this.tbName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cbTopDepart = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.tbPhone = new System.Windows.Forms.TextBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.cbManager = new System.Windows.Forms.ComboBox();
			this.bntAdd = new System.Windows.Forms.Button();
			this.btnChange = new System.Windows.Forms.Button();
			this.btnDel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgDepart)).BeginInit();
			this.SuspendLayout();
			// 
			// dgDepart
			// 
			this.dgDepart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dgDepart.CaptionVisible = false;
			this.dgDepart.DataMember = "";
			this.dgDepart.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgDepart.Location = new System.Drawing.Point(4, 7);
			this.dgDepart.Name = "dgDepart";
			this.dgDepart.ReadOnly = true;
			this.dgDepart.Size = new System.Drawing.Size(584, 249);
			this.dgDepart.TabIndex = 0;
			this.dgDepart.CurrentCellChanged += new System.EventHandler(this.dgDepart_CurrentCellChanged);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.Location = new System.Drawing.Point(16, 270);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Название";
			// 
			// tbName
			// 
			this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tbName.Location = new System.Drawing.Point(80, 270);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(417, 20);
			this.tbName.TabIndex = 1;
			this.tbName.Text = "";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.Location = new System.Drawing.Point(16, 305);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Подчинён";
			// 
			// cbTopDepart
			// 
			this.cbTopDepart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cbTopDepart.Location = new System.Drawing.Point(80, 305);
			this.cbTopDepart.Name = "cbTopDepart";
			this.cbTopDepart.Size = new System.Drawing.Size(417, 21);
			this.cbTopDepart.TabIndex = 2;
			this.cbTopDepart.DropDown += new System.EventHandler(this.cbTopDepart_DropDown);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.Location = new System.Drawing.Point(16, 345);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(62, 23);
			this.label3.TabIndex = 5;
			this.label3.Text = "Начальник";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.Location = new System.Drawing.Point(318, 344);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 18);
			this.label4.TabIndex = 7;
			this.label4.Text = "Телефон";
			// 
			// tbPhone
			// 
			this.tbPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tbPhone.Location = new System.Drawing.Point(373, 340);
			this.tbPhone.Name = "tbPhone";
			this.tbPhone.Size = new System.Drawing.Size(123, 20);
			this.tbPhone.TabIndex = 4;
			this.tbPhone.Text = "";
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClose.BackColor = System.Drawing.Color.LightGray;
			this.btnClose.Location = new System.Drawing.Point(512, 347);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 8;
			this.btnClose.Text = "Закрыть";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// cbManager
			// 
			this.cbManager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cbManager.Location = new System.Drawing.Point(80, 340);
			this.cbManager.Name = "cbManager";
			this.cbManager.Size = new System.Drawing.Size(224, 21);
			this.cbManager.TabIndex = 3;
			this.cbManager.DropDown += new System.EventHandler(this.cbManager_DropDown);
			// 
			// bntAdd
			// 
			this.bntAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.bntAdd.BackColor = System.Drawing.Color.LightGray;
			this.bntAdd.Location = new System.Drawing.Point(512, 262);
			this.bntAdd.Name = "bntAdd";
			this.bntAdd.TabIndex = 5;
			this.bntAdd.Text = "Добавить";
			this.bntAdd.Click += new System.EventHandler(this.bntAdd_Click);
			// 
			// btnChange
			// 
			this.btnChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnChange.BackColor = System.Drawing.Color.LightGray;
			this.btnChange.Location = new System.Drawing.Point(512, 290);
			this.btnChange.Name = "btnChange";
			this.btnChange.TabIndex = 6;
			this.btnChange.Text = "Изменить";
			this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
			// 
			// btnDel
			// 
			this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDel.BackColor = System.Drawing.Color.LightGray;
			this.btnDel.Location = new System.Drawing.Point(512, 318);
			this.btnDel.Name = "btnDel";
			this.btnDel.TabIndex = 7;
			this.btnDel.Text = "Удалить";
			this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
			// 
			// fmDepart
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Gainsboro;
			this.ClientSize = new System.Drawing.Size(592, 373);
			this.Controls.Add(this.btnDel);
			this.Controls.Add(this.btnChange);
			this.Controls.Add(this.bntAdd);
			this.Controls.Add(this.cbManager);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.tbPhone);
			this.Controls.Add(this.tbName);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cbTopDepart);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dgDepart);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(600, 400);
			this.Name = "fmDepart";
			this.Text = "Отделы";
			this.Load += new System.EventHandler(this.fmDepart_Load);
			this.Activated += new System.EventHandler(this.fmDepart_Activated);
			((System.ComponentModel.ISupportInitialize)(this.dgDepart)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// Заполняет набор данных информацией об отделах
		/// </summary>
		/// <returns>Набор данных</returns>		
		private DataSet fillDeparts()
		{
			DataSet sqlDS=null;			
			try
			{
				OleDbDataAdapter sqlDA=new OleDbDataAdapter("SELECT DPID,DPNAME,DPTOPDEPART,DPMANAGER,DPPHONE FROM EmplDepart ORDER BY DPNAME",conStr);
				sqlDA.SelectCommand.CommandTimeout=60;

				sqlDS = new DataSet();
				sqlDA.Fill(sqlDS);				
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);				
			}

			return sqlDS;
		}
		/// <summary>
		/// Связывает элемент управления "таблица" на форме с набором данными об отделах
		/// </summary>
		private void setGrid()
		{
			dsDepart=fillDeparts();
			if(dsDepart==null) 			
			{
				MessageBox.Show("Не удалось загрузить данные в справочник 'Отделы'","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				Close();
			}
			else
			{
				dgDepart.DataSource=dsDepart.Tables[0];
				dgDepart_CurrentCellChanged(dgDepart,null);
				if(dgDepart.TableStyles[dsDepart.Tables[0].TableName]==null)
				{					
					DataGridTableStyle ts=new DataGridTableStyle();
					ts.MappingName=Convert.ToString(dsDepart.Tables[0].TableName);
					ts.AllowSorting=true;
					ts.AlternatingBackColor=Color.Honeydew;									
					DataGridTextBoxColumn tb1=new DataGridTextBoxColumn();
					//идентификатор
					tb1.MappingName=Convert.ToString(dsDepart.Tables[0].Columns[0].ColumnName);
					tb1.Width=0;
					//название отдела
					DataGridTextBoxColumn tb2=new DataGridTextBoxColumn();
					tb2.Width=150;
					tb2.MappingName=Convert.ToString(dsDepart.Tables[0].Columns[1].ColumnName);
					tb2.HeaderText="Название";				
					//вышестоящий отдел
					DataGridTextBoxColumn tb3=new DataGridTextBoxColumn();
					tb3.Width=150;
					tb3.MappingName=Convert.ToString(dsDepart.Tables[0].Columns[2].ColumnName);
					tb3.HeaderText="Подчинён";
					//начальник отдела
					DataGridTextBoxColumn tb4=new DataGridTextBoxColumn();
					tb4.Width=100;
					tb4.MappingName=Convert.ToString(dsDepart.Tables[0].Columns[3].ColumnName);
					tb4.HeaderText="Начальник";
					//телефон отдела
					DataGridTextBoxColumn tb5=new DataGridTextBoxColumn();
					tb5.Width=100;
					tb5.MappingName=Convert.ToString(dsDepart.Tables[0].Columns[4].ColumnName);
					tb5.HeaderText="Телефон";
				
				
					ts.GridColumnStyles.Add(tb1);
					ts.GridColumnStyles.Add(tb2);
					ts.GridColumnStyles.Add(tb3);
					ts.GridColumnStyles.Add(tb4);
					ts.GridColumnStyles.Add(tb5);					
					
					dgDepart.TableStyles.Add(ts);
					dgDepart.ReadOnly=true;
				}
			}			
		}

		/// <summary>
		/// Добавляет новый отдел
		/// </summary>
		/// <param name="name">Название отдела</param>
		/// <param name="topdepart">Название вышестоящего отдела</param>
		/// <param name="manager">Начальник отдела</param>
		/// <param name="phone">Телефон отдела</param>
		/// <returns>true - выполнена успешно; false - возникла ошибка</returns>
		private bool addDepart(string name,string topdepart,string manager,string phone)
		{
			try
			{
				name=name.Replace("'","''");
				topdepart=topdepart.Replace("'","''");
				manager=manager.Replace("'","''");
				phone=phone.Replace("'","''");
				
				OleDbConnection sqlCon=new OleDbConnection(conStr);
				sqlCon.Open();
				OleDbCommand sqlCom=new OleDbCommand("insert into EmplDepart (DPNAME,DPTOPDEPART,DPMANAGER,DPPHONE) values ('"+name+"','"+topdepart+"','"+manager+"','"+phone+"')",sqlCon);
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
		/// Изменяет данные об отделе
		/// </summary>
		/// <param name="dpid">Идентификатор отдела</param>
		/// <param name="name">Название отдела</param>
		/// <param name="topdepart">Название вышестоящего отдела</param>
		/// <param name="manager">Начальник отдела</param>
		/// <param name="phone">Телефон отдела</param>
		/// <returns>true - выполнена успешно; false - возникла ошибка</returns>
		private bool changeDepart(int dpid,string name,string topdepart,string manager,string phone)
		{
			try
			{
				name=name.Replace("'","''");
				topdepart=topdepart.Replace("'","''");
				manager=manager.Replace("'","''");
				phone=phone.Replace("'","''");
				
				OleDbConnection sqlCon=new OleDbConnection(conStr);
				sqlCon.Open();
				OleDbCommand sqlCom=new OleDbCommand("update EmplDepart set DPNAME='"+name+"',DPTOPDEPART='"+topdepart+"',DPMANAGER='"+manager+"',DPPHONE='"+phone+"' where DPID="+dpid.ToString(),sqlCon);
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
		/// Удаляет отдел
		/// </summary>
		/// <param name="dpid">Идентификатор отдела</param>
		/// <returns>true - выполнена успешно; false - возникла ошибка</returns>
		private bool delDepart(int dpid)
		{
			try
			{
				OleDbConnection sqlCon=new OleDbConnection(conStr);
				sqlCon.Open();
				OleDbCommand sqlCom=new OleDbCommand("delete from EmplDepart where DPID="+dpid.ToString(),sqlCon);
				sqlCom.CommandTimeout=120;

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

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void fmDepart_Load(object sender, System.EventArgs e)
		{
			setGrid();	
		}

		private void bntAdd_Click(object sender, System.EventArgs e)
		{
			if(tbName.Text!="") 
			{
				if(addDepart(tbName.Text,cbTopDepart.Text,cbManager.Text,tbPhone.Text)==false) MessageBox.Show("Не удалось добавить новый отдел!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1); 
				else
					setGrid();
			}
			else
				MessageBox.Show("Задайте название отдела!","Предупреждение",MessageBoxButtons.OK,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1); 		
		}

		private void btnChange_Click(object sender, System.EventArgs e)
		{
			int r=dgDepart.CurrentRowIndex;
			if(tbName.Text!="")
			{
				if(r>=0)
				{
					if(changeDepart(Convert.ToInt32(dgDepart[r,0]),tbName.Text,cbTopDepart.Text,cbManager.Text,tbPhone.Text)==false) MessageBox.Show("Не удалось изменить отдел!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1); 
					else
						setGrid();					
				}
			}
			else
				MessageBox.Show("Задайте название отдела!","Предупреждение",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1); 
		}

		private void btnDel_Click(object sender, System.EventArgs e)
		{
			int r=dgDepart.CurrentRowIndex;
			if(r>=0)
			{
				if(delDepart(Convert.ToInt32(dgDepart[r,0]))==false) MessageBox.Show("Не удалось удалить отдел!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1); 			
				else
				 setGrid();				
			}
		}
		/// <summary>
		/// Заполняет элементы управления на форме данными о сотруднике, выбранном в таблице
		/// </summary>		
		private void dgDepart_CurrentCellChanged(object sender, System.EventArgs e)
		{
			int r=((DataGrid)sender).CurrentRowIndex;
			if(r>=0)
			{
				tbName.Text=((DataGrid)sender)[r,1].ToString();
				cbTopDepart.Text=((DataGrid)sender)[r,2].ToString();
				cbManager.Text=((DataGrid)sender)[r,3].ToString();				
				tbPhone.Text=((DataGrid)sender)[r,4].ToString();
			}
			else
			{
				tbName.Text=""; 
				cbTopDepart.Text="";
				cbManager.Text="";				
				tbPhone.Text="";
			}
		}

		private void cbTopDepart_DropDown(object sender, System.EventArgs e)
		{
			string [][] res=null;
			res=getDeparts(conStr);

			if(res.Length!=0)
			{
				cbTopDepart.Items.Clear();
				foreach(string [] s in res)				
					cbTopDepart.Items.Add(s[0]);
			}
		}

		private void cbManager_DropDown(object sender, System.EventArgs e)
		{
			string [][] res=null;
			res=fmEmpl.getEmpls(conStr,"");

			if(res.Length!=0)
			{
				cbManager.Items.Clear();
				foreach(string [] s in res)
					cbManager.Items.Add(s[0]);
			}
		}

		private void fmDepart_Activated(object sender, System.EventArgs e)
		{
			tbName.Focus();
		}
	}
}
