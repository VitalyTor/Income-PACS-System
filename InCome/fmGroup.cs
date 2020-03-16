using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace InCome
{
	/// <summary>
	/// Форма редактирования групп
	/// </summary>
	public class fmGroup : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnDel;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.Button btnChange;
		private System.Windows.Forms.DataGrid dgGroup;	
		private System.ComponentModel.Container components = null;

		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		/// <summary>
		/// Набор данных с информацией о группах
		/// </summary>
		private DataSet dsGroup=null;
		/// <summary>
		/// Строка соединения с базой 
		/// </summary>
		private string conStr;

		/// <summary>
		///Связывает элемент управления "таблица" на форме с набором данными о группах
		/// </summary>
		private void setGrid()
		{
			dsGroup=fillGroups();
			if(dsGroup==null) 			
			{
				MessageBox.Show("Не удалось загрузить данные в справочник 'Группы'","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				Close();
			}
			else
		    {
				dgGroup.DataSource=dsGroup.Tables[0];
				dgGroup_CurrentCellChanged(dgGroup,null);
				if(dgGroup.TableStyles[dsGroup.Tables[0].TableName]==null)
				{					
					DataGridTableStyle ts=new DataGridTableStyle();
					ts.MappingName=Convert.ToString(dsGroup.Tables[0].TableName);
					ts.AllowSorting=true;
					ts.AlternatingBackColor=Color.Honeydew;									
					DataGridTextBoxColumn tb1=new DataGridTextBoxColumn();
					tb1.MappingName=Convert.ToString(dsGroup.Tables[0].Columns[0].ColumnName);
					tb1.Width=0;					
					DataGridTextBoxColumn tb2=new DataGridTextBoxColumn();
					tb2.Width=300;
					tb2.MappingName=Convert.ToString(dsGroup.Tables[0].Columns[1].ColumnName);
					tb2.HeaderText="Название";				
					ts.GridColumnStyles.Add(tb1);
					ts.GridColumnStyles.Add(tb2);
					dgGroup.TableStyles.Add(ts);
					dgGroup.ReadOnly=true;
				}
			}			
		}
		/// <summary>
		/// Заполняет набор данных информацией о группах
		/// </summary>
		/// <returns>Набор данных</returns>
		public DataSet fillGroups()
		{
			DataSet sqlDS=null;
			try
			{
				SqlDataAdapter sqlDA=new SqlDataAdapter("select GRID as 'Идентификатор',GRNAME as 'Название' from EmplGroup order by GRNAME",conStr);
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
		/// Добавляет новую группу
		/// </summary>
		/// <param name="name">Название группы</param>
		/// <returns>true - выполнена успешно; false - возникла ошибка</returns>
		private bool addGroup(string name)
		{
			try
			{
				name=name.Replace("'","''");
				SqlConnection sqlCon=new SqlConnection(conStr);
				sqlCon.Open();
				SqlCommand sqlCom=new SqlCommand("insert into EmplGroup (GRNAME) values ('"+name+"')",sqlCon);
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
		/// Изменяет данные о группе
		/// </summary>
		/// <param name="grid">Идентификатор группы</param>
		/// <param name="name">Название группы</param>
		/// <returns>true - выполнена успешно; false - возникла ошибка</returns>
		private bool changeGroup(int grid,string name)
		{
			try
			{
				name=name.Replace("'","''");
				
				SqlConnection sqlCon=new SqlConnection(conStr);
				sqlCon.Open();
				SqlCommand sqlCom=new SqlCommand("update EmplGroup set GRNAME='"+name+"' where GRID="+grid.ToString(),sqlCon);
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
		/// Удаляет группу
		/// </summary>
		/// <param name="grid">Идентификатор группы</param>
		/// <returns>true - выполнена успешно; false - возникла ошибка</returns>
		private bool delGroup(int grid)
		{
			try
			{
				SqlConnection sqlCon=new SqlConnection(conStr);
				sqlCon.Open();
				SqlCommand sqlCom=new SqlCommand("delete from EmplGroup where GRID="+grid.ToString(),sqlCon);
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
		/// Возвращает названия и идентификаторы всех групп
		/// </summary>
		/// <param name="conStr">Строка соединения с базой</param>
		/// <returns>Массив с данными</returns>
		public  static string [][] getGroups(string conStr)
		{
			string [][] res;
			try
			{
				SqlDataAdapter sqlDA=new SqlDataAdapter("select GRNAME,GRID from EmplGroup order by GRNAME",conStr);
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
		/// Конструктор формы
		/// </summary>
		/// <remarks>Задаёт строку соединения с базой</remarks>
		/// <param name="fconStr">Строка соединения с базой</param>
		public fmGroup(string fconStr)
		{
			InitializeComponent();
			conStr=fconStr;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmGroup));
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgGroup = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.tbName = new System.Windows.Forms.TextBox();
            this.btnChange = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDel.BackColor = System.Drawing.Color.LightGray;
            this.btnDel.Location = new System.Drawing.Point(324, 342);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(64, 23);
            this.btnDel.TabIndex = 4;
            this.btnDel.Text = "DELETE";
            this.btnDel.UseVisualStyleBackColor = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.BackColor = System.Drawing.Color.LightGray;
            this.btnAdd.Location = new System.Drawing.Point(186, 342);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(64, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "ADD";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.BackColor = System.Drawing.Color.LightGray;
            this.btnClose.Location = new System.Drawing.Point(512, 342);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "CLOSE";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgGroup
            // 
            this.dgGroup.AllowDrop = true;
            this.dgGroup.AlternatingBackColor = System.Drawing.Color.Lavender;
            this.dgGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgGroup.CaptionVisible = false;
            this.dgGroup.DataMember = "";
            this.dgGroup.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgGroup.Location = new System.Drawing.Point(4, 8);
            this.dgGroup.Name = "dgGroup";
            this.dgGroup.Size = new System.Drawing.Size(585, 328);
            this.dgGroup.TabIndex = 3;
            this.dgGroup.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.dgGroup.CurrentCellChanged += new System.EventHandler(this.dgGroup_CurrentCellChanged);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.dgGroup;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "sss";
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.MappingName = "ssss";
            this.dataGridTextBoxColumn1.ReadOnly = true;
            this.dataGridTextBoxColumn1.Width = 20;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.MappingName = "dddd";
            this.dataGridTextBoxColumn2.Width = 75;
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbName.Location = new System.Drawing.Point(4, 344);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(176, 20);
            this.tbName.TabIndex = 1;
            // 
            // btnChange
            // 
            this.btnChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnChange.BackColor = System.Drawing.Color.LightGray;
            this.btnChange.Location = new System.Drawing.Point(255, 342);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(64, 23);
            this.btnChange.TabIndex = 3;
            this.btnChange.Text = "EDIT";
            this.btnChange.UseVisualStyleBackColor = false;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // fmGroup
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(592, 373);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.dgGroup);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "fmGroup";
            this.Text = "Groups";
            this.Activated += new System.EventHandler(this.fmGroup_Activated);
            this.Load += new System.EventHandler(this.fmGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgGroup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void fmGroup_Load(object sender, System.EventArgs e)
		{
			setGrid();			
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			if(tbName.Text!="") 
			{
				if(addGroup(tbName.Text)==false) MessageBox.Show("Не удалось добавить новую группу!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
 
				else
				  setGrid();
			}
			else
				MessageBox.Show("Задайте название группы!","Предупреждение",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
		   Close();
		}

		private void btnChange_Click(object sender, System.EventArgs e)
		{		
			int r=dgGroup.CurrentRowIndex;
			if(tbName.Text!="")
			{
				if(r>=0)
				{
					if(changeGroup(Convert.ToInt32(dgGroup[r,0]),tbName.Text)==false) MessageBox.Show("Не удалось изменить группу!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
					else
					  setGrid();											
				}
			}
			else
				MessageBox.Show("Задайте название группы!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
		}

		private void btnDel_Click(object sender, System.EventArgs e)
		{
			int r=dgGroup.CurrentRowIndex;
			if(r>=0)
			{
				if(delGroup(Convert.ToInt32(dgGroup[r,0]))==false) MessageBox.Show("Не удалось удалить группу!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				else
					setGrid();									
			}
		}

		private void dgGroup_CurrentCellChanged(object sender, System.EventArgs e)
		{
            int r=((DataGrid)sender).CurrentRowIndex;
            if(r>=0)
				tbName.Text=((DataGrid)sender)[r,1].ToString();
			else
				tbName.Text=""; 
		}

		private void fmGroup_Activated(object sender, System.EventArgs e)
		{
			tbName.Focus();
		}		
	}
}
