using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace InCome
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class fmEmplEdit : System.Windows.Forms.Form
	{
		private string conStr, emplMcode;

		private System.Windows.Forms.TextBox tbFIO;
		private System.Windows.Forms.Label labFIO;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnSaveChanges;
		private System.Windows.Forms.Label lbPhoto;
		private System.Windows.Forms.PictureBox pbPhoto;
		private System.Windows.Forms.DataGrid dgCurrent;
		private System.Windows.Forms.TextBox tbDivision;
		private System.Windows.Forms.TextBox tbGroup;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        /// <param name="fconStr">Строка соединения с базой</param>
		/// <param name="femplMid">Mid сотрудника</param>
		public fmEmplEdit(string fconStr, string femplMcode)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            conStr = fconStr;
			emplMcode = femplMcode;
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(fmEmplEdit));
			this.dgCurrent = new System.Windows.Forms.DataGrid();
			this.tbFIO = new System.Windows.Forms.TextBox();
			this.labFIO = new System.Windows.Forms.Label();
			this.tbDivision = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbGroup = new System.Windows.Forms.TextBox();
			this.btnSaveChanges = new System.Windows.Forms.Button();
			this.lbPhoto = new System.Windows.Forms.Label();
			this.pbPhoto = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.dgCurrent)).BeginInit();
			this.SuspendLayout();
			// 
			// dgCurrent
			// 
			this.dgCurrent.DataMember = "";
			this.dgCurrent.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgCurrent.Location = new System.Drawing.Point(1, 72);
			this.dgCurrent.Name = "dgCurrent";
			this.dgCurrent.Size = new System.Drawing.Size(559, 328);
			this.dgCurrent.TabIndex = 0;
			// 
			// tbFIO
			// 
			this.tbFIO.Location = new System.Drawing.Point(47, 6);
			this.tbFIO.Name = "tbFIO";
			this.tbFIO.Size = new System.Drawing.Size(289, 20);
			this.tbFIO.TabIndex = 1;
			this.tbFIO.Text = "";
			this.tbFIO.TextChanged += new System.EventHandler(this.tbFIO_TextChanged);
			// 
			// labFIO
			// 
			this.labFIO.Location = new System.Drawing.Point(8, 9);
			this.labFIO.Name = "labFIO";
			this.labFIO.Size = new System.Drawing.Size(40, 16);
			this.labFIO.TabIndex = 2;
			this.labFIO.Text = "ФИО:";
			// 
			// tbDivision
			// 
			this.tbDivision.Location = new System.Drawing.Point(59, 27);
			this.tbDivision.Name = "tbDivision";
			this.tbDivision.Size = new System.Drawing.Size(277, 20);
			this.tbDivision.TabIndex = 1;
			this.tbDivision.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Отдел:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Группа:";
			// 
			// tbGroup
			// 
			this.tbGroup.Location = new System.Drawing.Point(59, 48);
			this.tbGroup.Name = "tbGroup";
			this.tbGroup.Size = new System.Drawing.Size(277, 20);
			this.tbGroup.TabIndex = 1;
			this.tbGroup.Text = "";
			// 
			// btnSaveChanges
			// 
			this.btnSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSaveChanges.BackColor = System.Drawing.Color.LightGray;
			this.btnSaveChanges.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveChanges.Image")));
			this.btnSaveChanges.Location = new System.Drawing.Point(512, 16);
			this.btnSaveChanges.Name = "btnSaveChanges";
			this.btnSaveChanges.Size = new System.Drawing.Size(36, 37);
			this.btnSaveChanges.TabIndex = 9;
			this.btnSaveChanges.Tag = "Сохранить";
			// 
			// lbPhoto
			// 
			this.lbPhoto.Location = new System.Drawing.Point(364, 30);
			this.lbPhoto.Name = "lbPhoto";
			this.lbPhoto.Size = new System.Drawing.Size(32, 16);
			this.lbPhoto.TabIndex = 11;
			this.lbPhoto.Text = "Фото";
			this.lbPhoto.Visible = false;
			// 
			// pbPhoto
			// 
			this.pbPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbPhoto.Location = new System.Drawing.Point(348, 8);
			this.pbPhoto.Name = "pbPhoto";
			this.pbPhoto.Size = new System.Drawing.Size(64, 56);
			this.pbPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbPhoto.TabIndex = 10;
			this.pbPhoto.TabStop = false;
			// 
			// fmEmplEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(560, 400);
			this.Controls.Add(this.lbPhoto);
			this.Controls.Add(this.pbPhoto);
			this.Controls.Add(this.btnSaveChanges);
			this.Controls.Add(this.labFIO);
			this.Controls.Add(this.tbFIO);
			this.Controls.Add(this.dgCurrent);
			this.Controls.Add(this.tbDivision);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbGroup);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "fmEmplEdit";
			this.Text = "Редактирование записей сотрудника";
			this.Load += new System.EventHandler(this.fmEmplEdit_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgCurrent)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void fmEmplEdit_Load(object sender, System.EventArgs e)
		{
            DataSet sqlDS = new DataSet();			
			string sql="";
			try
			{   
                OleDbDataAdapter sqlDA=new OleDbDataAdapter(sql,conStr);
				sqlDA.SelectCommand.CommandTimeout=120;
                sqlDS = new dsCurrent();

                sql="SELECT ISNULL(MSURNAME, '')+' '+ISNULL(MNAME, '')+' '+ISNULL(MSECNAME, ''), " + 
                            "MDEPARTMENT AS FIO, MGROUP FROM EmplMain WHERE MCODE = '" + emplMcode + "'";
                sqlDA.SelectCommand.CommandText = sql;
                sqlDA.Fill(sqlDS);

                tbFIO.Text = (string)sqlDS.Tables[2].Rows[0][0];
			    tbDivision.Text = sqlDS.Tables[2].Rows[0][1] as string;
                tbGroup.Text = Convert.ToString(sqlDS.Tables[2].Rows[0][2]);

//				sql="SELECT TOP 1 MPHOTO FROM EmplMain where MCODE = '" + emplMcode + "'";
//                sqlDA.SelectCommand.CommandText = sql;
//				sqlDA.Fill(sqlDS);
//				byte [] photo=(Convert.IsDBNull(sqlDS.Tables[2].Rows[0][0]))?new byte[0]:(byte [])sqlDS.Tables[2].Rows[0][0];
////				if(photo.Length!=0)
////				{
////					MemoryStream ms=new MemoryStream(photo);
////					pbPhoto.Image=new Bitmap(ms);
////					lbPhoto.Hide();
////				}
////				else
////				{
////					pbPhoto.Image=null;
////					lbPhoto.Show();
////				}

				sql="SELECT ISNULL(em.MSURNAME, '')+' '+ISNULL(em.MNAME, '')+' '+ISNULL(em.MSECNAME, '') AS FIO, em.MDEPARTMENT as MDEPARTMENT,em.MGROUP as MGROUP, ec.INDT as INOUTDATETIME, "; 
				sql=sql+"CAST(DATEDIFF(mi, ec.INDT, GETDATE()) / 60 AS varchar) + ' ч. ' + CAST(DATEDIFF(mi, ec.INDT, GETDATE()) % 60 AS varchar)+' мин.' AS DUR ";
				sql=sql+"FROM EmplCurrent AS ec INNER JOIN EmplMain AS em ON ec.MID = em.MID WHERE  em.MCODE = '" + emplMcode + "'";
				sql=sql+" ORDER BY em.MSURNAME";

                sqlDA.SelectCommand.CommandText = sql;
				sqlDA.Fill(sqlDS,"EmplEdit");	
			}
			catch(Exception e1)
			{
				MessageBox.Show(e1.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);								
			}

			if(sqlDS==null) 			
			{
				MessageBox.Show("Не удалось загрузить данные о выбранном сотруднике!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);				
				Close();
			}
			else
			{
				try
				{
					if(dgCurrent.TableStyles["EmplEdit"]==null)
					{					
						DataGridTableStyle ts=new DataGridTableStyle();
						ts.MappingName="EmplEdit";
						ts.AllowSorting=true;
						ts.AlternatingBackColor=Color.Honeydew;					
						//фио
						DataGridTextBoxColumn tb3=new DataGridTextBoxColumn();
						tb3.Width=150;
						tb3.MappingName=Convert.ToString(sqlDS.Tables["EmplEdit"].Columns[0].ColumnName);
						tb3.HeaderText="ФИО";
						tb3.ReadOnly = true;
						//отдел
						DataGridTextBoxColumn tb4=new DataGridTextBoxColumn();
						tb4.Width=150;
						tb4.MappingName=Convert.ToString(sqlDS.Tables["EmplEdit"].Columns[1].ColumnName);
						tb4.HeaderText="Отдел";
						tb4.ReadOnly = true;
						//группа
						DataGridTextBoxColumn tb5=new DataGridTextBoxColumn();
						tb5.Width=100;
						tb5.MappingName=Convert.ToString(sqlDS.Tables["EmplEdit"].Columns[2].ColumnName);
						tb5.HeaderText="Группа";
						tb5.ReadOnly = true;
						//время прихода
						DataGridTextBoxColumn tb6=new DataGridTextBoxColumn();
						tb6.Width=120;
						tb6.MappingName=Convert.ToString(sqlDS.Tables["EmplEdit"].Columns[3].ColumnName);
						tb6.HeaderText="Приход";
						//времени на рабочем месте
						DataGridTextBoxColumn tb7=new DataGridTextBoxColumn();
						tb7.Width=100;
						tb7.MappingName=Convert.ToString(sqlDS.Tables["EmplEdit"].Columns[4].ColumnName);
						tb7.HeaderText="Отработано";
						tb7.ReadOnly = true;

						ts.GridColumnStyles.Add(tb3);
						ts.GridColumnStyles.Add(tb4);
						ts.GridColumnStyles.Add(tb5);					
						ts.GridColumnStyles.Add(tb6);
						ts.GridColumnStyles.Add(tb7);										
					
						dgCurrent.TableStyles.Add(ts);		
						dgCurrent.ReadOnly=false;
					}
					dgCurrent.SetDataBinding(sqlDS,"EmplEdit");

				}
				catch(Exception e2)
				{
					MessageBox.Show("Ошибка при инициализации таблицы. "+e2.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				}
			}	
		}

		private void tbFIO_TextChanged(object sender, System.EventArgs e)
		{
		
		}

	}
}
