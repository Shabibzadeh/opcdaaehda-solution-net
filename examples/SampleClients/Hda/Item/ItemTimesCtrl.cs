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

using System;
using System.Collections;
using System.Windows.Forms;

using SampleClients.Common;
using SampleClients.Hda.Edit;
using SampleClients.Hda.Trend;
using SampleClients.Hda.Test;

using Technosoftware.DaAeHdaClient.Hda;

#endregion

namespace SampleClients.Hda.Item
{
    /// <summary>
    /// Summary description for ReadParametersCtrl.
    /// </summary>
    public class ItemTimesCtrl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ListView timesLv_;
		private System.Windows.Forms.Panel mainPn_;
		private System.Windows.Forms.ContextMenuStrip popupMenu_;
		private System.Windows.Forms.ToolStripMenuItem addMi_;
		private System.Windows.Forms.ToolStripMenuItem editMi_;
		private System.Windows.Forms.ToolStripMenuItem removeMi_;
		private System.Windows.Forms.ToolStripMenuItem copyMi_;
		private System.Windows.Forms.ToolStripMenuItem separator02_;
		private System.Windows.Forms.ToolStripMenuItem importTimesMi_;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components_ = null;

		public ItemTimesCtrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			
			timesLv_.SmallImageList = Resources.Instance.ImageList;
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
			this.timesLv_ = new System.Windows.Forms.ListView();
			this.popupMenu_ = new System.Windows.Forms.ContextMenuStrip();
			this.addMi_ = new System.Windows.Forms.ToolStripMenuItem();
			this.copyMi_ = new System.Windows.Forms.ToolStripMenuItem();
			this.editMi_ = new System.Windows.Forms.ToolStripMenuItem();
			this.removeMi_ = new System.Windows.Forms.ToolStripMenuItem();
			this.separator02_ = new System.Windows.Forms.ToolStripMenuItem();
			this.importTimesMi_ = new System.Windows.Forms.ToolStripMenuItem();
			this.mainPn_ = new System.Windows.Forms.Panel();
			this.mainPn_.SuspendLayout();
			this.SuspendLayout();
			// 
			// TimesLV
			// 
			this.timesLv_.ContextMenuStrip = this.popupMenu_;
			this.timesLv_.Dock = System.Windows.Forms.DockStyle.Fill;
			this.timesLv_.FullRowSelect = true;
			this.timesLv_.Location = new System.Drawing.Point(0, 0);
			this.timesLv_.Name = "timesLv_";
			this.timesLv_.Size = new System.Drawing.Size(544, 360);
			this.timesLv_.TabIndex = 1;
			this.timesLv_.View = System.Windows.Forms.View.Details;
			this.timesLv_.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TimesLV_MouseDown);
			this.timesLv_.DoubleClick += new System.EventHandler(this.EditMI_Click);
			// 
			// PopupMenu
			// 
			this.popupMenu_.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
																					  this.addMi_,
																					  this.copyMi_,
																					  this.editMi_,
																					  this.removeMi_,
																					  this.separator02_,
																					  this.importTimesMi_});
			// 
			// AddMI
			// 
			this.addMi_.ImageIndex = 0;
			this.addMi_.Text = "Add...";
			this.addMi_.Click += new System.EventHandler(this.AddMI_Click);
			// 
			// CopyMI
			// 
			this.copyMi_.ImageIndex = 1;
			this.copyMi_.Text = "Copy...";
			this.copyMi_.Click += new System.EventHandler(this.CopyMI_Click);
			// 
			// EditMI
			// 
			this.editMi_.ImageIndex = 2;
			this.editMi_.Text = "Edit...";
			this.editMi_.Click += new System.EventHandler(this.EditMI_Click);
			// 
			// RemoveMI
			// 
			this.removeMi_.ImageIndex = 3;
			this.removeMi_.Text = "Remove";
			this.removeMi_.Click += new System.EventHandler(this.RemoveMI_Click);
			// 
			// Separator02
			// 
			this.separator02_.ImageIndex = 4;
			this.separator02_.Text = "-";
			// 
			// ImportTimesMI
			// 
			this.importTimesMi_.ImageIndex = 5;
			this.importTimesMi_.Text = "Import Times...";
			this.importTimesMi_.Click += new System.EventHandler(this.ImportTimesMI_Click);
			// 
			// MainPN
			// 
			this.mainPn_.Controls.Add(this.timesLv_);
			this.mainPn_.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPn_.Location = new System.Drawing.Point(0, 0);
			this.mainPn_.Name = "mainPn_";
			this.mainPn_.Size = new System.Drawing.Size(544, 360);
			this.mainPn_.TabIndex = 2;
			// 
			// ItemTimesCtrl
			// 
			this.Controls.Add(this.mainPn_);
			this.Name = "ItemTimesCtrl";
			this.Size = new System.Drawing.Size(544, 360);
			this.mainPn_.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Public Interface
		/// <summary>
		/// Initializes the control with a set of items.
		/// </summary>
		public void Initialize(TsCHdaServer server, TsCHdaItemTimeCollection times)
		{
			mServer_ = server;

			timesLv_.Items.Clear();

			if (times != null)
			{
				// add item times to list.
				foreach (DateTime time in times)
				{
					AddListItem(time, -1);
				}

				// adjust the list view columns to fit the data.
				AdjustColumns();
			}
		}	

		/// <summary>
		/// Returns the set of item times stored in the list control.
		/// </summary>
		public TsCHdaItemTimeCollection GetTimes()
		{
			TsCHdaItemTimeCollection times = new TsCHdaItemTimeCollection();

			foreach (ListViewItem listItem in timesLv_.Items)
			{
				if (typeof(DateTime).IsInstanceOfType(listItem.Tag))
				{
					times.Add(listItem.Tag);
				}
			}

			return times;
		}
		#endregion

		#region Private Members
		/// <summary>
		/// Constants used to identify the list view columns.
		/// </summary>
		private const int Timestamp         = 0;

		/// <summary>
		/// The list view column names.
		/// </summary>
		private readonly string[] columnNames_ = new string[]
		{
			"Timestamp"
		};
		
		/// <summary>
		/// The historian database server.
		/// </summary>
		private TsCHdaServer mServer_ = null;	

		/// <summary>
		/// Sets the columns shown in the list view.
		/// </summary>
		private void SetColumns(string[] columns)
		{		
			timesLv_.Clear();

			foreach (string column in columns)
			{
				ColumnHeader header = new ColumnHeader();
				header.Text  = column;
				timesLv_.Columns.Add(header);
			}

			AdjustColumns();
		}

		/// <summary>
		/// Adjusts the columns shown in the list view.
		/// </summary>
		private void AdjustColumns()
		{
			// adjust column widths.
			for (int ii = 0; ii < timesLv_.Columns.Count; ii++)
			{
				// always show the timestamp column.
				if (ii == Timestamp)
				{
					timesLv_.Columns[ii].Width = -2;
					continue;
				}

				// adjust to width of contents if column not empty.
				bool empty = true;

				foreach (ListViewItem current in timesLv_.Items)
				{
					if (current.SubItems[ii].Text != "") 
					{ 
						empty = false;
						timesLv_.Columns[ii].Width = -2;
						break;
					}
				}

				// set column width to zero if no data it in.
				if (empty) timesLv_.Columns[ii].Width = 0;
			}
		}
		
		/// <summary>
		/// Returns the time of the specified field.
		/// </summary>
		private object GetFieldValue(DateTime time, int fieldId)
		{
			switch (fieldId)
			{
				case Timestamp: { return time.ToString("yyyy-MM-dd hh:mm:ss"); }
			}

			return null;
		}

		/// <summary>
		/// Adds an item to the list view.
		/// </summary>
		private void AddListItem(DateTime time, int index)
		{
			// create list view item.
			ListViewItem listItem = new ListViewItem("", Resources.IMAGE_YELLOW_SCROLL);

			// add empty columns.
			while (listItem.SubItems.Count < columnNames_.Length) listItem.SubItems.Add("");
			
			// update columns.
			UpdateListItem(listItem, time);
	
			// add to list view.
			if (index < 0)
			{
				timesLv_.Items.Add(listItem);
			}
			else
			{
				timesLv_.Items.Insert(index, listItem);
			}
		}

		/// <summary>
		/// Updates an item in the list view.
		/// </summary>
		private void UpdateListItem(ListViewItem listItem, DateTime time)
		{
			// set column times.
			for (int ii = 0; ii < listItem.SubItems.Count; ii++)
			{
				listItem.SubItems[ii].Text = Technosoftware.DaAeHdaClient.OpcConvert.ToString(GetFieldValue(time, ii));
			}

			// save object as list view item tag.
			listItem.Tag = time;
		}
		#endregion

		/// <summary>
		/// Adds a new item time.
		/// </summary>
		private void AddMI_Click(object sender, System.EventArgs e)
		{
			try
			{
				// prompt user to edit item time.
				DateTime time = new ItemTimeEditDlg().ShowDialog(RunTestDlg.Basetime);

				if (time == DateTime.MinValue)
				{
					return;
				}

				// get the current selection.
				int index = -1;

				if (timesLv_.SelectedIndices.Count > 0)
				{
					index = timesLv_.SelectedIndices[timesLv_.SelectedIndices.Count-1];
				}

				// update display.
				AddListItem(time, index);

				// adjust columns
				AdjustColumns();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		/// <summary>
		/// Adds a new item time by copying an existing one.
		/// </summary>
		private void CopyMI_Click(object sender, System.EventArgs e)
		{
			try
			{
				// check for valid selection.
				if (timesLv_.SelectedItems.Count != 1)
				{
					return;
				}

				DateTime time = (DateTime)timesLv_.SelectedItems[0].Tag;

				// create new item time.
				DateTime copy = new ItemTimeEditDlg().ShowDialog((DateTime)time);

				// prompt user to edit item time.
				if (copy == DateTime.MinValue)
				{
					return;
				}
				
				// update display.
				AddListItem(copy, timesLv_.SelectedIndices[0]);

				// adjust columns
				AdjustColumns();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		/// <summary>
		/// Edits the parameters of s item time.
		/// </summary>
		private void EditMI_Click(object sender, System.EventArgs e)
		{
			try
			{
				// check for valid selection.
				if (timesLv_.SelectedItems.Count != 1)
				{
					return;
				}

				// create new item time.
				DateTime time = new ItemTimeEditDlg().ShowDialog((DateTime)timesLv_.SelectedItems[0].Tag);

				// prompt user to edit item time.
				if (time == DateTime.MinValue)
				{
					return;
				}
				
				// update display.
				UpdateListItem(timesLv_.SelectedItems[0], time);
				
				// adjust columns
				AdjustColumns();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		/// <summary>
		/// Deletes am existing item time.
		/// </summary>
		private void RemoveMI_Click(object sender, System.EventArgs e)
		{
			try
			{
				// check for valid selection.
				if (timesLv_.SelectedItems.Count == 0)
				{
					return;
				}
				
				// update display.
				ArrayList items = new ArrayList();

				foreach (ListViewItem listItem in timesLv_.SelectedItems)
				{
					items.Add(listItem);
				}

				foreach (ListViewItem listItem in items)
				{
					listItem.Remove();
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		/// <summary>
		/// Imports item times from another item.
		/// </summary>
		private void ImportTimesMI_Click(object sender, System.EventArgs e)
		{
			try
			{
				// prompt user to select values from another item.
				TsCHdaItemValueCollection values = new ReadValuesDlg().ShowDialog(mServer_, RequestType.ReadRaw, true);

				if (values == null)
				{
					return;
				}

				// get the current selection.
				int index = 0;

				if (timesLv_.SelectedIndices.Count > 0)
				{
					index = timesLv_.SelectedIndices[timesLv_.SelectedIndices.Count-1];
				}

				// add new times to list.
				foreach (TsCHdaItemValue value in values)
				{
					AddListItem(value.Timestamp, index++);
				}				  

				// adjust columns
				AdjustColumns();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		/// <summary>
		/// Updates the state of the menu buttons when the mouse is pressed.
		/// </summary>
		private void TimesLV_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// ignore left button actions.
			if (e.Button != MouseButtons.Right)	return;

			// set default times.
			addMi_.Enabled         = true;
			copyMi_.Enabled        = false;
			editMi_.Enabled        = false;
			removeMi_.Enabled      = false;
			importTimesMi_.Enabled = true;

			// selects the item that was right clicked on.
			ListViewItem clickedItem = timesLv_.GetItemAt(e.X, e.Y);

			// no item clicked on - do nothing.
			if (clickedItem == null) return;

			// force selection to clicked item.
			clickedItem.Selected = true;

			if (timesLv_.SelectedItems.Count == 1)
			{			
				copyMi_.Enabled = true;
				editMi_.Enabled = true;
			}

			if (timesLv_.SelectedItems.Count > 0)
			{			
				removeMi_.Enabled = true;
			}		
		}
	}
}
