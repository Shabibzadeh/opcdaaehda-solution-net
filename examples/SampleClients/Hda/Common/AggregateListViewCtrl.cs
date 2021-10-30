#region Copyright (c) 2011-2021 Technosoftware GmbH. All rights reserved
//-----------------------------------------------------------------------------
// Copyright (c) 2011-2021 Technosoftware GmbH. All rights reserved
// Web: https://technosoftware.com  
// 
// Purpose: 
// 
//
// The Software is subject to the Technosoftware GmbH Source Code License Agreement, 
// which can be found here:
// https://technosoftware.com/documents/Source_License_Agreement.pdf
// 
// The Software is based on the OPC .NET API Sample Code.
//-----------------------------------------------------------------------------
#endregion Copyright (c) 2011-2021 Technosoftware GmbH. All rights reserved

#region Using Directives

using System.Windows.Forms;

using SampleClients.Common;

using Technosoftware.DaAeHdaClient.Hda;

#endregion

namespace SampleClients.Hda.Common
{
	/// <summary>
	/// A control used to display a list of item properties.
	/// </summary>
	public class AggregateListViewCtrl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ToolStripMenuItem removeMi_;
		private System.Windows.Forms.ToolStripMenuItem editMi_;
		private System.Windows.Forms.ToolStripMenuItem copyMi_;
		private System.Windows.Forms.ListView aggregatesLv_;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components_ = null;

		public AggregateListViewCtrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			aggregatesLv_.SmallImageList = Resources.Instance.ImageList;
			
			SetColumns(columnNames_);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components_ != null)
				{
					components_.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.aggregatesLv_ = new System.Windows.Forms.ListView();
			this.copyMi_ = new System.Windows.Forms.ToolStripMenuItem();
			this.editMi_ = new System.Windows.Forms.ToolStripMenuItem();
			this.removeMi_ = new System.Windows.Forms.ToolStripMenuItem();
			this.SuspendLayout();
			// 
			// AggregatesLV
			// 
			this.aggregatesLv_.Dock = System.Windows.Forms.DockStyle.Fill;
			this.aggregatesLv_.FullRowSelect = true;
			this.aggregatesLv_.Location = new System.Drawing.Point(0, 0);
			this.aggregatesLv_.MultiSelect = false;
			this.aggregatesLv_.Name = "aggregatesLv_";
			this.aggregatesLv_.Size = new System.Drawing.Size(432, 272);
			this.aggregatesLv_.TabIndex = 0;
			this.aggregatesLv_.View = System.Windows.Forms.View.Details;
			// 
			// CopyMI
			// 
			this.copyMi_.Text = "";
			// 
			// EditMI
			// 
			this.editMi_.Text = "";
			// 
			// RemoveMI
			// 
			this.removeMi_.Text = "";
			// 
			// AggregateListViewCtrl
			// 
			this.AllowDrop = true;
			this.Controls.Add(this.aggregatesLv_);
			((Control)this).Name = "AggregateListViewCtrl";
			this.Size = new System.Drawing.Size(432, 272);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Constants used to identify the list view columns.
		/// </summary>
		private const int Id          = 0;
		private const int Name        = 1;
		private const int Description = 2;

		/// <summary>
		/// The list view column names.
		/// </summary>
		private readonly string[] columnNames_ = new string[]
		{
			"ID",
			"Name",
			"Description"
		};
		
		/// <summary>
		/// The server containing the aggregates to be displayed.
		/// </summary>
		private TsCHdaServer mServer_ = null;

		/// <summary>
		/// Initializes the control with a set of identified results.
		/// </summary>
		public void Initialize(TsCHdaServer server)
		{
			aggregatesLv_.Items.Clear();

			// check if there is nothing to do.
			if (server == null) return;

			mServer_ = server;

			foreach (TsCHdaAggregate aggregate in server.Aggregates)
			{
				AddAggregate(aggregate);
			}
			
			// adjust the list view columns to fit the data.
			AdjustColumns();
		}	

		/// <summary>
		/// Sets the columns shown in the list view.
		/// </summary>
		private void SetColumns(string[] columns)
		{		
			aggregatesLv_.Clear();

			foreach (string column in columns)
			{
				ColumnHeader header = new ColumnHeader();
				header.Text  = column;
				aggregatesLv_.Columns.Add(header);
			}

			AdjustColumns();
		}

		/// <summary>
		/// Adjusts the columns shown in the list view.
		/// </summary>
		private void AdjustColumns()
		{
			// adjust column widths.
			for (int ii = 0; ii < aggregatesLv_.Columns.Count; ii++)
			{
				// always show the aggregate id and value column.
				if (ii == Id)
				{
					aggregatesLv_.Columns[ii].Width = -2;
					continue;
				}

				// adjust to width of contents if column not empty.
				bool empty = true;

				foreach (ListViewItem current in aggregatesLv_.Items)
				{
					if (current.SubItems[ii].Text != "") 
					{ 
						empty = false;
						aggregatesLv_.Columns[ii].Width = -2;
						break;
					}
				}

				// set column width to zero if no data it in.
				if (empty) aggregatesLv_.Columns[ii].Width = 0;
			}
		}
	
		/// <summary>
		/// Returns the value of the specified field.
		/// </summary>
		private object GetFieldValue(TsCHdaAggregate aggregate, int fieldId)
		{
			switch (fieldId)
			{
				case Id:          { return aggregate.Id; }
				case Name:        { return aggregate.Name; }
				case Description: { return aggregate.Description; }
			}

			return null;
		}

		/// <summary>
		/// Adds an item to the list view.
		/// </summary>
		private void AddAggregate(TsCHdaAggregate aggregate)
		{
			// create list view item.
			ListViewItem listItem = new ListViewItem("", Resources.IMAGE_EXPLODING_BOX);

			// add empty columns.
			while (listItem.SubItems.Count < columnNames_.Length) listItem.SubItems.Add("");
			
			// set column values.
			for (int ii = 0; ii < listItem.SubItems.Count; ii++)
			{
				listItem.SubItems[ii].Text = Technosoftware.DaAeHdaClient.OpcConvert.ToString(GetFieldValue(aggregate, ii));
			}

			// save object as list view item tag.
			listItem.Tag = aggregate;
		
			// add to list view.
			aggregatesLv_.Items.Add(listItem);
		}
	}
}
