//
//  FormSettingsDialog.cs
//
//  Author:
//       Chris Brunner <cyrusbuilt at gmail dot com>
//
//  Copyright (c) 2012 CyrusBuilt
//
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 2 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
//
using System;
using System.Reflection;
using Gtk;

namespace CyrusBuilt.MonoPluginFramework.UI
{
	/// <summary>
	/// Form settings dialog.
	/// </summary>
	public partial class FormSettingsDialog : Window
	{
		#region Fields
		private PluginConfiguration _config = null;
		private String _name = String.Empty;
		private String _asmName = String.Empty;
		private ResponseType _response = ResponseType.Cancel;
		private TreeViewColumn tvcSetting = null;
		private TreeViewColumn tvcType = null;
		private TreeViewColumn tvcValue = null;
		private ListStore settingAttribStore = null;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CyrusBuilt.MonoPluginFramework.UI.FormSettingsDialog"/>
		/// class with the plugin to load the configuration from.
		/// </summary>
		/// <param name="plugin">
		/// The plugin to load the configuration from.
		/// </param>
		/// <param name="parent">
		/// 
		/// </param>
		public FormSettingsDialog(AvailablePlugin plugin, Window parent)
			: base(WindowType.Toplevel) {
			this.Build();
			this.Parent = parent;
			if (plugin != null) {
				this._name = plugin.Instance.Name;
				this._config = plugin.Instance.GetConfiguration();
				Assembly asm = Assembly.ReflectionOnlyLoadFrom(plugin.AssemblyPath);
				this._asmName = asm.GetName().Name;
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the loaded (or modified) configuration.
		/// </summary>
		public PluginConfiguration Configuration {
			get { return this._config; }
		}

		/// <summary>
		/// Gets the dialog response.
		/// </summary>
		public ResponseType DialogResponse {
			get { return this._response; }
		}
		#endregion

		/// <summary>
		/// Loads the config.
		/// </summary>
		private void LoadConfig() {
			if (!String.IsNullOrEmpty(this._name)) {
				this.Title = this._name + " Settings";
			}

			if (this._config == null) {
				this.treeviewPlugins.Hide();
				this.buttonSave.Hide();
				// TODO show a lable indicating no config.
				return;
			}



			object value = null;
			String valueType = String.Empty;
			this.settingAttribStore = new ListStore(typeof(String), typeof(String), typeof(String));
			foreach (String key in this._config.AllKeys) {
				value = this._config.GetValue(key);
				valueType = value.GetType().FullName;
				settingAttribStore.AppendValues(key, valueType, value.ToString());
			}
			this.treeviewPlugins.Model = settingAttribStore;
		}

		/// <summary>
		/// Raises the realized event.
		/// </summary>
		/// <param name="sender">
		/// 
		/// </param>
		/// <param name="e">
		/// 
		/// </param>
		protected void OnRealized(object sender, EventArgs e) {
			// Build setting name column.
			this.tvcSetting = new TreeViewColumn();
			this.tvcSetting.Title = "Setting";
			CellRendererText settingCell = new CellRendererText();
			this.tvcSetting.PackStart(settingCell, true);

			/// Build data type column.
			this.tvcType = new TreeViewColumn();
			this.tvcType.Title = "Type";
			CellRendererText typeCell = new CellRendererText();
			this.tvcType.PackStart(typeCell, true);

			// Build data value column.
			this.tvcValue = new TreeViewColumn();
			this.tvcValue.Title = "Value";
			CellRendererText valueCell = new CellRendererText();
			this.tvcValue.PackStart(typeCell, true);

			// Add all columns to treeview.
			this.treeviewPlugins.AppendColumn(this.tvcSetting);
			this.treeviewPlugins.AppendColumn(this.tvcType);
			this.treeviewPlugins.AppendColumn(this.tvcValue);

			this.tvcSetting.AddAttribute(settingCell, "text", 0);
			this.tvcType.AddAttribute(typeCell, "text", 1);
			this.tvcValue.AddAttribute(valueCell, "text", 2);



			this.LoadConfig();
		}

		/// <summary>
		/// Shows the dialog.
		/// </summary>
		/// <returns>
		/// The dialog response.
		/// </returns>
		public ResponseType ShowDialog() {
			this.ShowAll();
			return this._response;
		}

		/// <summary>
		/// Raises the delete event event.
		/// </summary>
		/// <param name="sender">
		/// The object sending the event call.
		/// </param>
		/// <param name="e">
		/// The event arguments.
		/// </param>
		protected void OnDeleteEvent(object sender, DeleteEventArgs e) {
			Application.Quit();
			e.RetVal = true;
		}

		/// <summary>
		/// Raises the button close clicked event. Closes the dialog.
		/// </summary>
		/// <param name="sender">
		/// The object sending the event call.
		/// </param>
		/// <param name="e">
		/// The event arguments.
		/// </param>
		protected void OnButtonCloseClicked (object sender, EventArgs e) {
			lock (this) {
				this._response = ResponseType.Cancel;
			}

			if (this.settingAttribStore != null) {
				this.settingAttribStore.Dispose();
				this.settingAttribStore = null;
			}
			this.Dispose();
		}

		/// <summary>
		/// Raises the button save clicked event.
		/// </summary>
		/// <param name="sender">
		/// The object sending the event call.
		/// </param>
		/// <param name="e">
		/// The event arguments.
		/// </param>
		protected void OnButtonSaveClicked (object sender, EventArgs e) {
			String key = String.Empty;
			String val = String.Empty;
			object obj = null;
			foreach (object[] row in this.settingAttribStore) {
				key = row[0];
				val = row[2].ToString();
				obj = this._config.GetValue(key);
				if (obj is String) {
					this._config.SetValue(key, val);
				}
				else if (obj is Boolean) {
					this._config.SetValue(key, Boolean.Parse(val));
				}
				else if (obj is Char) {
					this._config.SetValue(key, Char.Parse(val));
				}
				else if (obj is Decimal) {
					this._config.SetValue(key, Decimal.Parse(val));
				}
				else if (obj is Double) {
					this._config.SetValue(key, Double.Parse(val));
				}
				else if (obj is float) {
					this._config.SetValue(key, float.Parse(val));
				}
				else if (obj is Int32) {
					this._config.SetValue(key, Int32.Parse(val));
				}
				else if (obj is long) {
					this._config.SetValue(key, long.Parse(val));
				}
				else if (obj is short) {
					this._config.SetValue(key, short.Parse(val));
				}
				else if (obj is DateTime) {
					this._config.SetValue(key, DateTime.Parse(val));
				}
			}

			lock (this) {
				this._response = ResponseType.Ok;
			}
			this.Dispose();
		}

		/// <summary>
		/// Raises the treeview plugins select cursor row event.
		/// </summary>
		/// <param name='o'>
		/// O.
		/// </param>
		/// <param name='args'>
		/// Arguments.
		/// </param>
		protected void OnTreeviewPluginsSelectCursorRow(object o, SelectCursorRowArgs args) {
			TreeSelection selected = (o as TreeView).Selection;
			TreeModel model = null;
			TreeIter iter = null;
			if (selected.GetSelected(out model, out iter)) {
				String key = model.GetValue(iter, 0).ToString();
				String type = model.GetValue(iter, 1).ToString();
				String val = model.GetValue(iter, 2).ToString();
				// TODO send params to edit form.
			}
		}
	}
}

