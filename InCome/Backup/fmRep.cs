using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;

namespace InCome
{
	/// <summary>
	/// Форма для отображения отчётов
	/// </summary>
	public class fmRep : System.Windows.Forms.Form
	{
		private CrystalDecisions.Windows.Forms.CrystalReportViewer crv;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Конструктор формы
		/// </summary>
		/// <param name="r">Объект отчёта, который отображается</param>
     	public fmRep(ReportClass r)
		{			
			InitializeComponent();
			crv.ReportSource=r;			
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(fmRep));
			this.crv = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
			this.SuspendLayout();
			// 
			// crv
			// 
			this.crv.ActiveViewIndex = -1;
			this.crv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.crv.DisplayGroupTree = false;
			this.crv.Location = new System.Drawing.Point(0, 0);
			this.crv.Name = "crv";
			this.crv.ReportSource = null;
			this.crv.Size = new System.Drawing.Size(512, 272);
			this.crv.TabIndex = 0;
			// 
			// fmRep
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.crv);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "fmRep";
			this.Text = "Статистика";
			this.ResumeLayout(false);

		}
		#endregion

		
	}
}
