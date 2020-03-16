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
	/// Summary description for Form1.
	/// </summary>
	public class fmEmplEdit : System.Windows.Forms.Form
	{
        static string emplMcode, sql;

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
        private OleDbDataAdapter sqlDA = new OleDbDataAdapter();
        static OleDbConnection con = new OleDbConnection();
        DataSet sqlDS = new DataSet();
        /// <param name="fconStr">Строка соединения с базой</param>
		/// <param name="femplMid">Mid сотрудника</param>
		public fmEmplEdit(string fconStr, string femplMcode)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            con.ConnectionString = fconStr;
			emplMcode = femplMcode;
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
            sql = "SELECT ISNULL(MSURNAME, '')+' '+ISNULL(MNAME, '')+' '+ISNULL(MSECNAME, '') AS FIO, " +
                  "MDEPARTMENT AS MDEPARTMENT, MGROUP FROM EmplMain AS MGROUP WHERE MCODE = '" + emplMcode + "'";
            OleDbDataAdapter sqlDA = new OleDbDataAdapter(sql, con);
            sqlDA.SelectCommand.CommandTimeout = 120;
            sqlDA.Fill(sqlDS, "Employee");

            tbFIO.Text = (string)sqlDS.Tables["Employee"].Rows[0]["FIO"];
            tbDivision.Text = sqlDS.Tables["Employee"].Rows[0]["MDEPARTMENT"] as string;
            tbGroup.Text = Convert.ToString(sqlDS.Tables["Employee"].Rows[0]["MGROUP"]);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmEmplEdit));
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
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // dgCurrent
            // 
            this.dgCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
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
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveChanges.BackColor = System.Drawing.Color.LightGray;
            this.btnSaveChanges.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveChanges.Image")));
            this.btnSaveChanges.Location = new System.Drawing.Point(512, 16);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(36, 37);
            this.btnSaveChanges.TabIndex = 9;
            this.btnSaveChanges.Tag = "Сохранить";
            this.btnSaveChanges.UseVisualStyleBackColor = false;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // lbPhoto
            // 
            this.lbPhoto.Location = new System.Drawing.Point(380, 30);
            this.lbPhoto.Name = "lbPhoto";
            this.lbPhoto.Size = new System.Drawing.Size(35, 16);
            this.lbPhoto.TabIndex = 11;
            this.lbPhoto.Text = "Фото";
            this.lbPhoto.Visible = false;
            // 
            // pbPhoto
            // 
            this.pbPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPhoto.Location = new System.Drawing.Point(367, 4);
            this.pbPhoto.Name = "pbPhoto";
            this.pbPhoto.Size = new System.Drawing.Size(60, 66);
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
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void fmEmplEdit_Load(object sender, System.EventArgs e)
		{
            try
            {
           
                sql = "SELECT TOP 1 MPHOTO FROM EmplMain where MCODE = '" + emplMcode + "'";
                OleDbDataAdapter sqlDA = new OleDbDataAdapter(sql, con);
                sqlDA.SelectCommand.CommandTimeout = 120;
                sqlDA.Fill(sqlDS, "EmplPhoto");
                byte[] photo = (Convert.IsDBNull(sqlDS.Tables["EmplPhoto"].Rows[0][0])) ? 
                                new byte[0] : (byte[])sqlDS.Tables["EmplPhoto"].Rows[0][0];
                if (photo.Length != 0)
                {
                    MemoryStream ms = new MemoryStream(photo);
                    pbPhoto.Image = new Bitmap(ms);
                    lbPhoto.Hide();
                }
                else
                {
                    pbPhoto.Image = null;
                    lbPhoto.Show();
                }

                Refill_Grid();

            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.Message,"Ошибка",
                                MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);								
            }

            if (sqlDS == null)
            {
                MessageBox.Show("Не удалось загрузить данные о выбранном сотруднике!", "Ошибка", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                Close();
            }
            else
            {
                try
                {
                if (dgCurrent.TableStyles["EmplEdit"] == null)
                {
                    DataGridTableStyle ts = new DataGridTableStyle();
                    ts.MappingName = "EmplEdit";
                    ts.AllowSorting = true;
                    ts.AlternatingBackColor = Color.Honeydew;
                    //ID
                    DataGridTextBoxColumn tb0 = new DataGridTextBoxColumn();
                    tb0.Width = 40;
                    tb0.MappingName = Convert.ToString(sqlDS.Tables["EmplEdit"].Columns["ID"].ColumnName);
                    tb0.HeaderText = "ID";
                    tb0.ReadOnly = true;
                    //время прихода
                    DataGridTextBoxColumn tb1 = new DataGridTextBoxColumn();
                    tb1.Width = 120;
                    tb1.MappingName = Convert.ToString(sqlDS.Tables["EmplEdit"].Columns["INDATETIME"].ColumnName);
                    tb1.HeaderText = "Приход";
                    //время ухода
                    DataGridTextBoxColumn tb2 = new DataGridTextBoxColumn();
                    tb2.Width = 120;
                    tb2.MappingName = Convert.ToString(sqlDS.Tables["EmplEdit"].Columns["OUTDATETIME"].ColumnName);
                    tb2.HeaderText = "Уход";
                    //времени на рабочем месте
                    DataGridTextBoxColumn tb3 = new DataGridTextBoxColumn();
                    tb3.Width = 100;
                    tb3.MappingName = Convert.ToString(sqlDS.Tables["EmplEdit"].Columns["DUR"].ColumnName);
                    tb3.HeaderText = "Отработано";
                    tb3.ReadOnly = true;

                    ts.GridColumnStyles.Add(tb0);
                    ts.GridColumnStyles.Add(tb1);
                    ts.GridColumnStyles.Add(tb2);
                    ts.GridColumnStyles.Add(tb3);									

                    dgCurrent.TableStyles.Add(ts);
                    dgCurrent.ReadOnly = false;
                }
                dgCurrent.SetDataBinding(sqlDS, "EmplEdit");

                }
                catch(Exception e2)
                {
                    MessageBox.Show("Ошибка при инициализации таблицы. "+e2.Message,"Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
                }
            }	
		}

		private void tbFIO_TextChanged(object sender, System.EventArgs e)
		{
		
		}

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (dgCurrent[dgCurrent.CurrentRowIndex, 1] == DBNull.Value || 
               (dgCurrent[dgCurrent.CurrentRowIndex, 1].ToString().Trim() == "01.01.0001 0:00:00"))
            {
                MessageBox.Show("Неверное время прихода", "Ошибка!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            if (dgCurrent[dgCurrent.CurrentRowIndex, 2].ToString().Trim() == "01.01.0001 0:00:00")
                dgCurrent[dgCurrent.CurrentRowIndex, 2] = DBNull.Value;

            if (dgCurrent[dgCurrent.CurrentRowIndex, 2] != DBNull.Value && 
                (DateTime)dgCurrent[dgCurrent.CurrentRowIndex, 1] > 
                (DateTime)dgCurrent[dgCurrent.CurrentRowIndex, 2])
            {
                MessageBox.Show("Время прихода больше времени ухода.", "Ошибка!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }

             //сохраним изменения в базе данных
            try
            {
                con.Open();
                OleDbCommand sqlCom = new OleDbCommand("[updEmplCurrent]", con);
                sqlCom.CommandTimeout = 60;
                sqlCom.CommandType = CommandType.StoredProcedure;
                sqlCom.Parameters.Add("@ID", OleDbType.Integer, 4);
                sqlCom.Parameters["@ID"].Value = dgCurrent[dgCurrent.CurrentRowIndex, 0];
                sqlCom.Parameters.Add("@INDT", OleDbType.DBTimeStamp);
                sqlCom.Parameters["@INDT"].Value = dgCurrent[dgCurrent.CurrentRowIndex, 1];
                sqlCom.Parameters.Add("@OUTDT", OleDbType.DBTimeStamp);
                sqlCom.Parameters["@OUTDT"].Value = dgCurrent[dgCurrent.CurrentRowIndex, 2];
                sqlCom.Parameters.Add("@Author", OleDbType.VarChar);
                sqlCom.Parameters["@Author"].Value = fmMain.currentUser;
                int cr = dgCurrent.CurrentRowIndex;

                sqlCom.ExecuteNonQuery();

                // уберем строки из DataSet    
                sqlDS.Clear();
                // и заполним его заново
                Refill_Grid();
  
                dgCurrent.Select(cr);

            }
            catch (Exception e3)
            {
                MessageBox.Show("Возникла ошибка в процессе обновления данных!" + e3.Message, "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                con.Close();
            }

        }

        private void Refill_Grid()
        {
            sql = "SELECT ec.ID as ID, ec.INDT as INDATETIME, ec.OUTDT as OUTDATETIME, " + 
                  "CAST(DATEDIFF(mi, ec.INDT, ec.OUTDT) / 60 AS varchar) + ' ч. ' + " + 
                  "CAST(DATEDIFF(mi, ec.INDT, ec.OUTDT) % 60 AS varchar)+' мин.' AS DUR " +
                  "FROM EmplCurrent AS ec INNER JOIN EmplMain AS em ON ec.MID = em.MID " + 
                  "WHERE  em.MCODE = '" + emplMcode + "' " +
                  "ORDER BY em.MSURNAME";
            OleDbDataAdapter sqlDA = new OleDbDataAdapter(sql, con);
            sqlDA.Fill(sqlDS, "EmplEdit");
        }

	}
}
