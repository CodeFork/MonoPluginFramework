//
//  FormPluginSettingsDialog.cs
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
	/// Form plugin settings dialog.
	/// </summary>
	public partial class FormPluginSettingsDialog : Gtk.Dialog
	{
		#region TreeView Constants
		private const Int32 COLUMN_KEY = 0;
		private const Int32 COLUMN_TYPE = 1;
		private const Int32 COLUMN_VALUE = 2;
		#endregion

		#region Fields
		private PluginConfiguration _config = null;
		private String _name = String.Empty;
		private String _asmName = String.Empty;
		private ResponseType _response = ResponseType.Cancel;
		private TreeViewColumn _tvcSetting = null;
		private TreeViewColumn _tvcType = null;
		private TreeViewColumn _tvcValue = null;
		private ListStore _settingAttribStore = null;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CyrusBuilt.MonoPluginFramework.UI.FormPluginSettingsDialog"/>
		/// class with the plugin to load and the parent window.
		/// </summary>
		/// <param name="plugin">
		/// The plugin to load info from.
		/// </param>
		/// <param name="parent">
		/// The window that is the parent of this dialog.
		/// </param>
		public FormPluginSettingsDialog(AvailablePlugin plugin, Window parent)
			: base() {
			if (parent != null) {
				this.Parent = parent;
			}

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

		#region Methods
		/// <summary>
		/// Loads the plugin configuration.
		/// </summary>
		private void LoadConfig() {
			if (!String.IsNullOrEmpty(this._name)) {
				this.Title = this._name + " Settings";
			}
			
			if (this._config == null) {
				this.treeviewPlugins.Hide();
				this.buttonSave.Hide();
				this.VBox.Add(new Label("Plugin has not settings to load."));
				this.VBox.ShowAll();
				return;
			}
			
			object value = null;
			String valueType = String.Empty;
			this._settingAttribStore = new ListStore(typeof(String), typeof(String), typeof(String));
			foreach (String key in this._config.AllKeys) {
				value = this._config.GetValue(key);
				valueType = value.GetType().FullName;
				this._settingAttribStore.AppendValues(key, valueType, value.ToString());
			}
			this.treeviewPlugins.Model = this._settingAttribStore;
		}

		/// <summary>
		/// Shows the dialog and gets the response.
		/// </summary>
		/// <returns>
		/// The dialog response.
		/// </returns>
		public ResponseType ShowDialog() {
			this.ShowAll();
			this._response = (ResponseType)this.Run();
			return this._response;
		}
		#endregion

		#region Event Handlers
		/// <summary>
		/// Handles the realized event.
		/// </summary>
		/// <param name="sender">
		/// The object sending the event call.
		/// </param>
		/// <param name="e">
		/// The event arguments.
		/// </param>
		protected void OnRealized(object sender, EventArgs e) {
			// Build setting name column.
			this._tvcSetting = new TreeViewColumn();
			this._tvcSetting.Title = "Setting";
			CellRendererText settingCell = new CellRendererText();
			this._tvcSetting.PackStart(settingCell, true);
			
			// Build data type column.
			this._tvcType = new TreeViewColumn();
			this._tvcType.Title = "Type";
			CellRendererText typeCell = new CellRendererText();
			this._tvcType.PackStart(typeCell, true);

			// Build data value column.
			this._tvcValue = new TreeViewColumn();
			this._tvcValue.Title = "Value";
			CellRendererText valueCell = new CellRendererText();
			this._tvcValue.PackStart(typeCell, true);

			// Add all columns to treeview.
			this.treeviewPlugins.AppendColumn(this._tvcSetting);
			this.treeviewPlugins.AppendColumn(this._tvcType);
			this.treeviewPlugins.AppendColumn(this._tvcValue);
			
			this._tvcSetting.AddAttribute(settingCell, "text", COLUMN_KEY);
			this._tvcType.AddAttribute(typeCell, "text", COLUMN_TYPE);
			this._tvcValue.AddAttribute(valueCell, "text", COLUMN_VALUE);
			
			this.LoadConfig();
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
		/// Handles the button cancel clicked event.
		/// </summary>
		/// <param name="sender">
		/// The object sending the event call.
		/// </param>
		/// <param name="e">
		/// The event arguments.
		/// </param>
		protected void OnButtonCancelClicked(object sender, EventArgs e) {
			lock (this) {
				this._response = ResponseType.Cancel;
			}

			if (this._settingAttribStore != null) {
				this._settingAttribStore.Dispose();
				this._settingAttribStore = null;
			}
			this.Dispose();
		}

		/// <summary>
		/// Handles the button save clicked event.
		/// </summary>
		/// <param name="sender">
		/// The object sending the event call.
		/// </param>
		/// <param name="e">
		/// The event arguments.
		/// </param>
		protected void OnButtonSaveClicked(object sender, EventArgs e) {
			String key = String.Empty;
			String val = String.Empty;
			object obj = null;
			foreach (object[] row in this._settingAttribStore) {
				key = row[COLUMN_KEY].ToString();
				val = row[COLUMN_VALUE].ToString();
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

			if (this._settingAttribStore != null) {
				this._settingAttribStore.Dispose();
				this._settingAttribStore = null;
			}
			this.Dispose();
		}

		/// <summary>
		/// Handles the treeview plugins row activated event.
		/// </summary>
		/// <param name="sender">
		/// The object sending the event call.
		/// </param>
		/// <param name="e">
		/// The event arguments.
		/// </param>
		protected void OnTreeviewPluginsRowActivated(object sender, RowActivatedArgs e) {
			TreeIter iter = TreeIter.Zero;
			TreeModel model = this.treeviewPlugins.Model;
			if (model.GetIter(out iter, e.Path)) {
				String key = model.GetValue(iter, COLUMN_KEY).ToString();
				String type = model.GetValue(iter, COLUMN_TYPE).ToString();
				String value = model.GetValue(iter, COLUMN_VALUE).ToString();
				using (FormChangeValue editor = new FormChangeValue(key, value, type)) {
					if (editor.ShowDialog() == ResponseType.Ok) {
						this._settingAttribStore.SetValue(iter, 2, editor.ValueActual);
						this._settingAttribStore.EmitRowChanged(e.Path, iter);

						// TODO I'm guessing we don't need the following line at all.
						//model.SetValue(iter, 2, editor.ValueActual);
					}
				}
			}
		}
		#endregion
	}
}

