using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace InCome
{
	/// <summary>
	/// Форма для отображения/подсчёта статистики по учёту рабочего времени сотрудников
	/// </summary>
	public class fmStat : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button2;
		/// <summary>
		private string conStr;		
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button bntRefresh;
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.RadioButton rbTotal;
		private System.Windows.Forms.RadioButton rbDayly;
		private System.Windows.Forms.DateTimePicker dtpTime2;
		private System.Windows.Forms.DateTimePicker dtpDate2;
		private System.Windows.Forms.DateTimePicker dtpTime1;
		private System.Windows.Forms.DateTimePicker dtpDate1;
		private System.Windows.Forms.CheckedListBox clbEmpl;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.CheckBox cbAll;
		private System.Windows.Forms.CheckBox cbSusp;
		private System.Windows.Forms.Label lbWait;
		private System.Windows.Forms.DataGrid dgCurrent;	
		private System.Windows.Forms.Button btnCorrect;
		private System.Windows.Forms.Button btnEdit;		
		private System.ComponentModel.IContainer components=null;

		/// <summary>
		/// Конструктор формы
		/// </summary>
		/// <param name="fconStr">Строка соединения с базой</param>
		public fmStat(string fconStr)
		{
			InitializeComponent();
			conStr=fconStr;
		}

		/// <summary>
		/// Заполняет набор данных для формирования "ежедневного" и "итогового" отчётов 
		/// </summary>
		/// <returns>Набор данных</returns>
		private dsReportSource fillReportSource()
		{
			dsReportSource sqlDS=null;			

			try
			{   
				OleDbDataAdapter sqlDA=new OleDbDataAdapter("SELECT * FROM vwReportSource",conStr);
				sqlDA.SelectCommand.CommandTimeout=120;

				sqlDS = new dsReportSource();
				sqlDA.Fill(sqlDS,"vwReportSource");				
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);				
			}

			return sqlDS;
		}
		/// <summary>
		/// Корректирует некорректные записи о регистрации сотрудников в базе
		/// </summary>
		/// <param name="mode">0 - только определить количество некорректных записей; 1 - удалить некорректные записи </param>
		/// <param name="cnt">Количество некорректных записей в базе</param>
		/// <returns></returns>
		private bool correctTime(int mode,ref int cnt)
		{
			try
			{				
				OleDbConnection sqlCon=new OleDbConnection(conStr);
				sqlCon.Open();
				OleDbCommand sqlCom=new OleDbCommand("[correctTime]",sqlCon);
				sqlCom.CommandTimeout=120;
				sqlCom.CommandType = System.Data.CommandType.StoredProcedure;								
				sqlCom.Parameters.Add("@mode",OleDbType.Integer);
				sqlCom.Parameters.Add("@cnt",OleDbType.Integer);
				sqlCom.Parameters["@mode"].Value=mode;
				sqlCom.Parameters["@cnt"].Direction=ParameterDirection.Output;	    			
				sqlCom.ExecuteNonQuery();
				cnt=Convert.ToInt32(sqlCom.Parameters["@cnt"].Value);

				sqlCon.Close();
			}
			catch(Exception e)
			{
				MessageBox.Show("Не удалось определить/исправить ошибочные временные диапазоны! "+ e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);								
				return false;
			}

			return true;
		}		
		/// <summary>
		/// В базе создаёт и заполняет таблицу с информацией, необходимой для формирования "ежедневного" и "итогового" отчётов по сотрудникам
		/// </summary>
		/// <param name="midlist">Список идентификаторов сотрудников</param>
		/// <param name="d1">Начальный временной диапазон отчётности</param>
		/// <param name="d2">Конечный временной диапазон отчётности</param>
		/// <returns></returns>
		private bool formReportTable(string midlist,DateTime d1,DateTime d2)
		{
			//подготовка таблицы, с которой делаются отчёты
			try
			{
				
				OleDbConnection sqlCon=new OleDbConnection(conStr);
				sqlCon.Open();
				OleDbCommand sqlCom=new OleDbCommand("[formReportTable]",sqlCon);
				sqlCom.CommandTimeout=120;
				sqlCom.CommandType = System.Data.CommandType.StoredProcedure;
								
				sqlCom.Parameters.Add("@df",OleDbType.Date);
				sqlCom.Parameters.Add("@dt",OleDbType.Date);
				sqlCom.Parameters.Add("@midlist",OleDbType.VarChar);
				sqlCom.Parameters["@df"].Value=d1;
				sqlCom.Parameters["@dt"].Value=d2;
				sqlCom.Parameters["@midlist"].Value=midlist;
								
				sqlCom.ExecuteNonQuery();

				sqlCon.Close();
			}
			catch(Exception e)
			{
				MessageBox.Show("Не удалось подготовить таблицу для отчёта! "+ e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);										
				return false;
			}
		
			return true;
		}
		/// <summary>
		/// Заполняет набор данных информацией о текущих зарегистрированных сотрудниках
		/// </summary>
		/// <param name="susp">true - выбираются только "подозрительные" сотрудники (у которых временной диапазон приход/уход слишком большой); false - выбираются все текуще зарегистированные сотрудники  </param>
		private dsCurrent fillCurrent(bool susp)
		{
			dsCurrent sqlDS=null;			
			string sql="";
			try
			{   
				sql=sql+"SELECT ec.MID,em.MCODE,ISNULL(em.MSURNAME, '')+' '+ISNULL(em.MNAME, '')+' '+ISNULL(em.MSECNAME, '') AS FIO, em.MDEPARTMENT as MDEPARTMENT,em.MGROUP as MGROUP, ec.INDT as INOUTDATETIME, "; 
				sql=sql+"CAST(DATEDIFF(mi, ec.INDT, GETDATE()) / 60 AS varchar) + ' ч. ' + CAST(DATEDIFF(mi, ec.INDT, GETDATE()) % 60 AS varchar)+' мин.' AS DUR ";
				sql=sql+"FROM EmplCurrent AS ec INNER JOIN EmplMain AS em ON ec.MID = em.MID WHERE  (ec.ID IN (SELECT MAX(ID) AS MAXID FROM EmplCurrent AS ec1 GROUP BY MID)) AND (ec.OUTDT IS NULL) ";
				if(susp) sql=sql+" AND (DATEDIFF(mi, ec.INDT,GETDATE()) > 720) ";//в минутах подозрительное время на работе, сейчас 12 часов
                sql=sql+" ORDER BY em.MSURNAME";

				OleDbDataAdapter sqlDA=new OleDbDataAdapter(sql,conStr);
				sqlDA.SelectCommand.CommandTimeout=120;

				sqlDS = new dsCurrent();
				sqlDA.Fill(sqlDS,"EmplCurrent");				
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);								
			}

			return sqlDS;
		}
		/// <summary>
		/// Связывает элемент управления "таблица" на форме с набором данных о текуще зарегистрированных сотрудниках
		/// </summary>
		/// <param name="susp"></param>
		private void setGridCurrent(bool susp)
		{
			dsCurrent dsCur=fillCurrent(susp);
			if(dsCur==null) 			
			{
				MessageBox.Show("Не удалось загрузить данные о присутствующих на данный момент сотрудниках!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);				
				Close();
			}
			else
			{
				try
				{
				   if(dgCurrent.TableStyles["EmplCurrent"]==null)
				   {					
						DataGridTableStyle ts=new DataGridTableStyle();
						ts.MappingName="EmplCurrent";
						ts.AllowSorting=true;
						ts.AlternatingBackColor=Color.Honeydew;									
						DataGridTextBoxColumn tb1=new DataGridTextBoxColumn();
						//идентификатор
						tb1.MappingName=Convert.ToString(dsCur.Tables["EmplCurrent"].Columns[0].ColumnName);
						tb1.Width=0;
						//штрих-код
						DataGridTextBoxColumn tb2=new DataGridTextBoxColumn();
						tb2.Width=120;
						tb2.MappingName=Convert.ToString(dsCur.Tables["EmplCurrent"].Columns[1].ColumnName);
						tb2.HeaderText="Штрих-код";				
						//фио
						DataGridTextBoxColumn tb3=new DataGridTextBoxColumn();
						tb3.Width=200;
						tb3.MappingName=Convert.ToString(dsCur.Tables["EmplCurrent"].Columns[2].ColumnName);
						tb3.HeaderText="ФИО";
						//отдел
						DataGridTextBoxColumn tb4=new DataGridTextBoxColumn();
						tb4.Width=200;
						tb4.MappingName=Convert.ToString(dsCur.Tables["EmplCurrent"].Columns[3].ColumnName);
						tb4.HeaderText="Отдел";
						//группа
						DataGridTextBoxColumn tb5=new DataGridTextBoxColumn();
						tb5.Width=150;
						tb5.MappingName=Convert.ToString(dsCur.Tables["EmplCurrent"].Columns[4].ColumnName);
						tb5.HeaderText="Группа";
						//время прихода
						DataGridTextBoxColumn tb6=new DataGridTextBoxColumn();
						tb6.Width=120;
						tb6.MappingName=Convert.ToString(dsCur.Tables["EmplCurrent"].Columns[5].ColumnName);
						tb6.HeaderText="Приход";
						//времени на рабочем месте
						DataGridTextBoxColumn tb7=new DataGridTextBoxColumn();
						tb7.Width=100;
						tb7.MappingName=Convert.ToString(dsCur.Tables["EmplCurrent"].Columns[6].ColumnName);
						tb7.HeaderText="Отработано";
				
						ts.GridColumnStyles.Add(tb1);
						ts.GridColumnStyles.Add(tb2);
						ts.GridColumnStyles.Add(tb3);
						ts.GridColumnStyles.Add(tb4);
						ts.GridColumnStyles.Add(tb5);					
						ts.GridColumnStyles.Add(tb6);
						ts.GridColumnStyles.Add(tb7);										
					
						dgCurrent.TableStyles.Add(ts);
						dgCurrent.ReadOnly=true;				
				   }
				   dgCurrent.SetDataBinding(dsCur,"EmplCurrent");				
				}
				catch(Exception e)
				{
					MessageBox.Show("Ошбика при инициализации таблицы. "+e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				}
			}			
		}



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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(fmStat));
			this.button2 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lbWait = new System.Windows.Forms.Label();
			this.cbAll = new System.Windows.Forms.CheckBox();
			this.btnGo = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.clbEmpl = new System.Windows.Forms.CheckedListBox();
			this.rbTotal = new System.Windows.Forms.RadioButton();
			this.rbDayly = new System.Windows.Forms.RadioButton();
			this.dtpTime2 = new System.Windows.Forms.DateTimePicker();
			this.dtpDate2 = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.dtpTime1 = new System.Windows.Forms.DateTimePicker();
			this.dtpDate1 = new System.Windows.Forms.DateTimePicker();
			this.btnPrint = new System.Windows.Forms.Button();
			this.bntRefresh = new System.Windows.Forms.Button();
			this.cbSusp = new System.Windows.Forms.CheckBox();
			this.dgCurrent = new System.Windows.Forms.DataGrid();
			this.btnCorrect = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgCurrent)).BeginInit();
			this.SuspendLayout();
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button2.BackColor = System.Drawing.Color.LightGray;
			this.button2.Location = new System.Drawing.Point(288, 584);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(80, 23);
			this.button2.TabIndex = 7;
			this.button2.Text = "Закрыть";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.lbWait);
			this.groupBox1.Controls.Add(this.cbAll);
			this.groupBox1.Controls.Add(this.btnGo);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.clbEmpl);
			this.groupBox1.Controls.Add(this.rbTotal);
			this.groupBox1.Controls.Add(this.rbDayly);
			this.groupBox1.Controls.Add(this.dtpTime2);
			this.groupBox1.Controls.Add(this.dtpDate2);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.dtpTime1);
			this.groupBox1.Controls.Add(this.dtpDate1);
			this.groupBox1.Location = new System.Drawing.Point(0, 400);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(684, 176);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Выборка за период времени";
			// 
			// lbWait
			// 
			this.lbWait.BackColor = System.Drawing.Color.Transparent;
			this.lbWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.lbWait.Location = new System.Drawing.Point(13, 145);
			this.lbWait.Name = "lbWait";
			this.lbWait.Size = new System.Drawing.Size(192, 12);
			this.lbWait.TabIndex = 13;
			this.lbWait.Text = "Подождите, идёт подготовка...";
			this.lbWait.Visible = false;
			// 
			// cbAll
			// 
			this.cbAll.Checked = true;
			this.cbAll.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbAll.Location = new System.Drawing.Point(336, 22);
			this.cbAll.Name = "cbAll";
			this.cbAll.Size = new System.Drawing.Size(48, 16);
			this.cbAll.TabIndex = 5;
			this.cbAll.Text = "Все";
			this.cbAll.Click += new System.EventHandler(this.cbAll_Click);
			// 
			// btnGo
			// 
			this.btnGo.BackColor = System.Drawing.Color.LightGray;
			this.btnGo.Image = ((System.Drawing.Image)(resources.GetObject("btnGo.Image")));
			this.btnGo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnGo.Location = new System.Drawing.Point(112, 96);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(96, 34);
			this.btnGo.TabIndex = 8;
			this.btnGo.Text = "Выбрать ";
			this.btnGo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(260, 22);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Сотрудники:";
			// 
			// clbEmpl
			// 
			this.clbEmpl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.clbEmpl.ColumnWidth = 250;
			this.clbEmpl.Location = new System.Drawing.Point(258, 40);
			this.clbEmpl.MultiColumn = true;
			this.clbEmpl.Name = "clbEmpl";
			this.clbEmpl.ScrollAlwaysVisible = true;
			this.clbEmpl.Size = new System.Drawing.Size(420, 124);
			this.clbEmpl.TabIndex = 6;
			this.clbEmpl.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbEmpl_ItemCheck);
			// 
			// rbTotal
			// 
			this.rbTotal.Checked = true;
			this.rbTotal.Location = new System.Drawing.Point(16, 88);
			this.rbTotal.Name = "rbTotal";
			this.rbTotal.Size = new System.Drawing.Size(80, 24);
			this.rbTotal.TabIndex = 6;
			this.rbTotal.TabStop = true;
			this.rbTotal.Text = "Итоговая";
			// 
			// rbDayly
			// 
			this.rbDayly.Location = new System.Drawing.Point(16, 112);
			this.rbDayly.Name = "rbDayly";
			this.rbDayly.Size = new System.Drawing.Size(72, 24);
			this.rbDayly.TabIndex = 7;
			this.rbDayly.Text = "По дням";
			// 
			// dtpTime2
			// 
			this.dtpTime2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dtpTime2.Location = new System.Drawing.Point(33, 51);
			this.dtpTime2.Name = "dtpTime2";
			this.dtpTime2.ShowUpDown = true;
			this.dtpTime2.Size = new System.Drawing.Size(72, 20);
			this.dtpTime2.TabIndex = 3;
			// 
			// dtpDate2
			// 
			this.dtpDate2.Location = new System.Drawing.Point(113, 51);
			this.dtpDate2.Name = "dtpDate2";
			this.dtpDate2.Size = new System.Drawing.Size(135, 20);
			this.dtpDate2.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(9, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(17, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "по";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(9, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(16, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "с";
			// 
			// dtpTime1
			// 
			this.dtpTime1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dtpTime1.Location = new System.Drawing.Point(33, 20);
			this.dtpTime1.Name = "dtpTime1";
			this.dtpTime1.ShowUpDown = true;
			this.dtpTime1.Size = new System.Drawing.Size(72, 20);
			this.dtpTime1.TabIndex = 1;
			// 
			// dtpDate1
			// 
			this.dtpDate1.Location = new System.Drawing.Point(113, 20);
			this.dtpDate1.Name = "dtpDate1";
			this.dtpDate1.Size = new System.Drawing.Size(136, 20);
			this.dtpDate1.TabIndex = 2;
			// 
			// btnPrint
			// 
			this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnPrint.BackColor = System.Drawing.Color.LightGray;
			this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
			this.btnPrint.Location = new System.Drawing.Point(52, 355);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(36, 37);
			this.btnPrint.TabIndex = 3;
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// bntRefresh
			// 
			this.bntRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.bntRefresh.BackColor = System.Drawing.Color.LightGray;
			this.bntRefresh.Image = ((System.Drawing.Image)(resources.GetObject("bntRefresh.Image")));
			this.bntRefresh.Location = new System.Drawing.Point(8, 355);
			this.bntRefresh.Name = "bntRefresh";
			this.bntRefresh.Size = new System.Drawing.Size(36, 37);
			this.bntRefresh.TabIndex = 2;
			this.bntRefresh.Click += new System.EventHandler(this.bntRefresh_Click);
			// 
			// cbSusp
			// 
			this.cbSusp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cbSusp.Location = new System.Drawing.Point(135, 363);
			this.cbSusp.Name = "cbSusp";
			this.cbSusp.Size = new System.Drawing.Size(185, 24);
			this.cbSusp.TabIndex = 4;
			this.cbSusp.Text = "Подозрительные сотрудники";
			this.cbSusp.CheckedChanged += new System.EventHandler(this.cbSusp_CheckedChanged);
			// 
			// dgCurrent
			// 
			this.dgCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dgCurrent.CaptionBackColor = System.Drawing.Color.Silver;
			this.dgCurrent.CaptionForeColor = System.Drawing.Color.Navy;
			this.dgCurrent.CaptionText = "Зарегистрированные сотрудники";
			this.dgCurrent.DataMember = "";
			this.dgCurrent.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgCurrent.Location = new System.Drawing.Point(4, 7);
			this.dgCurrent.Name = "dgCurrent";
			this.dgCurrent.Size = new System.Drawing.Size(683, 344);
			this.dgCurrent.TabIndex = 1;
			// 
			// btnCorrect
			// 
			this.btnCorrect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCorrect.BackColor = System.Drawing.Color.LightGray;
			this.btnCorrect.Image = ((System.Drawing.Image)(resources.GetObject("btnCorrect.Image")));
			this.btnCorrect.Location = new System.Drawing.Point(608, 355);
			this.btnCorrect.Name = "btnCorrect";
			this.btnCorrect.Size = new System.Drawing.Size(36, 37);
			this.btnCorrect.TabIndex = 5;
			this.btnCorrect.Click += new System.EventHandler(this.btnCorrect_Click);
			// 
			// btnEdit
			// 
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnEdit.BackColor = System.Drawing.Color.LightGray;
			this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
			this.btnEdit.Location = new System.Drawing.Point(648, 355);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(36, 37);
			this.btnEdit.TabIndex = 8;
			this.btnEdit.Tag = "Редактировать";
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// fmStat
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Gainsboro;
			this.ClientSize = new System.Drawing.Size(692, 613);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnCorrect);
			this.Controls.Add(this.dgCurrent);
			this.Controls.Add(this.cbSusp);
			this.Controls.Add(this.bntRefresh);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.button2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(700, 640);
			this.Name = "fmStat";
			this.Text = "Статистика";
			this.Load += new System.EventHandler(this.fmStat_Load);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgCurrent)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// Обработчик события загрузки формы
		/// </summary>
		/// <remarks>Иницирует элементы управления на форме и заполняет их данными</remarks>	
		private void fmStat_Load(object sender, System.EventArgs e)
		{
			setGridCurrent(cbSusp.Checked);
			dtpTime1.Value=DateTime.Now;
			dtpDate1.Value=DateTime.Now;
			dtpTime2.Value=DateTime.Now;
			dtpDate2.Value=DateTime.Now;
			rbTotal.Checked=true;
		    clbEmpl.Items.Clear();
			cbAll.Checked=true;
			string [][] empls=fmEmpl.getEmpls(conStr,"");
			foreach(string [] s in empls)
				clbEmpl.Items.Add(s[0]+" ["+s[1]+"]");
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void dgCurrent_DoubleClick(object sender, System.EventArgs e)
		{
			setGridCurrent(cbSusp.Checked);	
		}

		private void bntRefresh_Click(object sender, System.EventArgs e)
		{
			setGridCurrent(cbSusp.Checked);
		}
		/// <summary>
		/// Иницирует к открытию отчёт о текуще зарегистрированных сотрудниках
		/// </summary>
		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			dsCurrent ds=null;
			
			try
			{
				crCurrent cr=new crCurrent();			
				ds=fillCurrent(cbSusp.Checked);	                
				cr.SetDataSource(ds);			
				
				fmRep codeRep=new fmRep(cr);
				codeRep.ShowDialog();				
			}
			catch(Exception ex)
			{
				MessageBox.Show("Не удалось создать отчёт! "+ex.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);				
			}
		}

		private void cbAll_Click(object sender, System.EventArgs e)
		{
			if(cbAll.Checked==true)
			{
				for(int i=0;i<clbEmpl.Items.Count;i++)
					clbEmpl.SetItemChecked(i,false);				
				cbAll.Checked=true;
			}
		}

		private void clbEmpl_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
			cbAll.Checked=false;
		}

		private void cbSusp_CheckedChanged(object sender, System.EventArgs e)
		{
			setGridCurrent(cbSusp.Checked);		
		}
		/// <summary>
		/// Иницирует к открытию "ежедневный" либо "итоговый" отчёты
		/// </summary>		
		private void btnGo_Click(object sender, System.EventArgs e)
		{
			string midlist="",s="";
			DateTime d1,d2;

			if((cbAll.Checked==false) && (clbEmpl.CheckedItems.Count==0))
			{
				MessageBox.Show("Не выбраны сотрудники для формирования отчёта! ","Предупреждение",MessageBoxButtons.OK,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);				
				return;
			}
			
		    d1=Convert.ToDateTime(dtpDate1.Text+" "+dtpTime1.Text);
			d2=Convert.ToDateTime(dtpDate2.Text+" "+dtpTime2.Text);
			if(d2<=d1)
			{
				MessageBox.Show("Некорректно задан временной диапазон! ", "Сообщение",MessageBoxButtons.OK);
				return;
			}

			lbWait.Show();
			Cursor=Cursors.WaitCursor;
			//список идентификаторов сотрудников
			if((cbAll.Checked==false) && (clbEmpl.CheckedItems.Count!=0)) //список идентификаторов сотрудников, которые отображаются в отчёте
				for(int i=0;i<clbEmpl.CheckedItems.Count;i++)
				{
					s=clbEmpl.CheckedItems[i].ToString();
					s=s.Substring(s.IndexOf('['));
					midlist=midlist+s;
				}
			//формирование таблицы для отчёта
			if(formReportTable(midlist,d1,d2))
			{
				//выбираем отчёт для формирования
				dsReportSource ds=null;
				ReportClass cr=null;

				try
				{
					if(rbDayly.Checked)
					    cr=new crDayReport();			
                    else
						cr=new crTotalReport();
					ds=fillReportSource();	                
					cr.SetDataSource(ds);			
					string report_dur="за период с "+Convert.ToString(d1)+" по "+Convert.ToString(d2);
					cr.SetParameterValue("report_dur",report_dur);
				
					fmRep codeRep=new fmRep(cr);
					Cursor=Cursors.Default;
					lbWait.Hide();

					codeRep.ShowDialog();				
				}
				catch(Exception ex)
				{
					MessageBox.Show("Не удалось создать отчёт! "+ex.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);				
				}
			}
			else MessageBox.Show("Не удалось создать таблицу для отчёта! ","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);				
			Cursor=Cursors.Default;
			lbWait.Hide();			
		}
		
		private void btnCorrect_Click(object sender, System.EventArgs e)
		{
			//есть ли некорректные записи
			Cursor=Cursors.WaitCursor;
			int cnt=0;
			if(correctTime(0,ref cnt))
			{
				if(cnt!=0) 
				{
					//удаляем некорректные
					if(MessageBox.Show("Найдены записи с неправильными временными диапазонами ("+cnt.ToString()+" шт.). Рекомендуется удалить эти записи во избежании сбоев в отчётах. Удалить записи?", "Предупреждение",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1)==DialogResult.Yes)
					{
						if(correctTime(1,ref cnt)) MessageBox.Show("Некорректные записи были удалены!", "Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);					
						else MessageBox.Show("Некорректные записи не были удалены из-за ошибки!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
					}
				}
				else
					MessageBox.Show("Не найдено записей с неправильными временными диапазонами.", "Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1); 			
			}
			else
				MessageBox.Show("Не удалось почучить информацию о некорректных записяз из-за ошибки!", "Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);					 						
			Cursor=Cursors.Default;
		}

		#region New Code 
		#endregion

		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			fmEmplEdit edit=new fmEmplEdit(conStr, dgCurrent[dgCurrent.CurrentCell.RowNumber,1] as string);
            edit.ShowDialog();
		}

		/// <summary>
		/// Связывает элемент управления "таблица" на форме с набором данных о текуще зарегистрированных сотрудниках
		/// </summary>
		/// <param name="susp"></param>
		private void setGridToEdit(bool susp)
		{
			dsCurrent dsCur=fillToEdit(susp);
			if(dsCur==null) 			
			{
				MessageBox.Show("Не удалось загрузить данные о присутствующих на данный момент сотрудниках!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);				
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
						DataGridTextBoxColumn tb1=new DataGridTextBoxColumn();
						//идентификатор
						tb1.MappingName=Convert.ToString(dsCur.Tables["EmplEdit"].Columns[0].ColumnName);
						tb1.Width=0;
						//штрих-код
						DataGridTextBoxColumn tb2=new DataGridTextBoxColumn();
						tb2.Width=120;
						tb2.MappingName=Convert.ToString(dsCur.Tables["EmplEdit"].Columns[1].ColumnName);
						tb2.HeaderText="Штрих-код";				
						//фио
						DataGridTextBoxColumn tb3=new DataGridTextBoxColumn();
						tb3.Width=200;
						tb3.MappingName=Convert.ToString(dsCur.Tables["EmplEdit"].Columns[2].ColumnName);
						tb3.HeaderText="ФИО";
						//отдел
						DataGridTextBoxColumn tb4=new DataGridTextBoxColumn();
						tb4.Width=200;
						tb4.MappingName=Convert.ToString(dsCur.Tables["EmplEdit"].Columns[3].ColumnName);
						tb4.HeaderText="Отдел";
						//группа
						DataGridTextBoxColumn tb5=new DataGridTextBoxColumn();
						tb5.Width=150;
						tb5.MappingName=Convert.ToString(dsCur.Tables["EmplEdit"].Columns[4].ColumnName);
						tb5.HeaderText="Группа";
						//время прихода
						DataGridTextBoxColumn tb6=new DataGridTextBoxColumn();
						tb6.Width=120;
						tb6.MappingName=Convert.ToString(dsCur.Tables["EmplEdit"].Columns[5].ColumnName);
						tb6.HeaderText="Приход";
						//времени на рабочем месте
						DataGridTextBoxColumn tb7=new DataGridTextBoxColumn();
						tb7.Width=100;
						tb7.MappingName=Convert.ToString(dsCur.Tables["EmplEdit"].Columns[6].ColumnName);
						tb7.HeaderText="Отработано";
				
						ts.GridColumnStyles.Add(tb1);
						ts.GridColumnStyles.Add(tb2);
						ts.GridColumnStyles.Add(tb3);
						ts.GridColumnStyles.Add(tb4);
						ts.GridColumnStyles.Add(tb5);					
						ts.GridColumnStyles.Add(tb6);
						ts.GridColumnStyles.Add(tb7);										
					
						dgCurrent.TableStyles.Add(ts);
						dgCurrent.ReadOnly=true;				
					}
					dgCurrent.SetDataBinding(dsCur,"EmplEdit");				
				}
				catch(Exception e)
				{
					MessageBox.Show("Ошбика при инициализации таблицы. "+e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				}
			}			
		}
		/// <summary>
		/// Заполняет набор данных информацией о текущих зарегистрированных сотрудниках
		/// </summary>
		/// <param name="susp">true - выбираются только "подозрительные" сотрудники (у которых временной диапазон приход/уход слишком большой); false - выбираются все текуще зарегистированные сотрудники  </param>
		private dsCurrent fillToEdit(bool susp)
		{
			dsCurrent sqlDS=null;			
			string sql="";
			try
			{   
				sql=sql+"SELECT ec.MID,em.MCODE,ISNULL(em.MSURNAME, '')+' '+ISNULL(em.MNAME, '')+' '+ISNULL(em.MSECNAME, '') AS FIO, em.MDEPARTMENT as MDEPARTMENT,em.MGROUP as MGROUP, ec.INDT as INOUTDATETIME, "; 
				sql=sql+"CAST(DATEDIFF(mi, ec.INDT, GETDATE()) / 60 AS varchar) + ' ч. ' + CAST(DATEDIFF(mi, ec.INDT, GETDATE()) % 60 AS varchar)+' мин.' AS DUR ";
				sql=sql+"FROM EmplCurrent AS ec INNER JOIN EmplMain AS em ON ec.MID = em.MID WHERE  (ec.ID IN (SELECT MAX(ID) AS MAXID FROM EmplCurrent AS ec1 GROUP BY MID)) AND (ec.OUTDT IS NULL) ";
				if(susp) sql=sql+" AND (DATEDIFF(mi, ec.INDT,GETDATE()) > 720) ";//в минутах подозрительное время на работе, сейчас 12 часов
				sql=sql+" ORDER BY em.MSURNAME";

				OleDbDataAdapter sqlDA=new OleDbDataAdapter(sql,conStr);
				sqlDA.SelectCommand.CommandTimeout=120;

				sqlDS = new dsCurrent();
				sqlDA.Fill(sqlDS,"EmplEdit");				
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);								
			}

			return sqlDS;
		}
		}
}
