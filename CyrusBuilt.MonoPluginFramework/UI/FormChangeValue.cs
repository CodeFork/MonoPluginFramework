//
//  FormChangeValue.cs
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
using Gtk;

namespace CyrusBuilt.MonoPluginFramework.UI
{
	/// <summary>
	/// Setting value change dialog. This provides a dialog for changing the
	/// value of a particular configuration setting.
	/// </summary>
	public partial class FormChangeValue : Dialog
	{
		#region Fields
		private String _key = String.Empty;
		private String _val = String.Empty;
		private String _type = String.Empty;
		private object _valueActual = null;
		private ResponseType _response = ResponseType.Cancel;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CyrusBuilt.MonoPluginFramework.UI.FormChangeValue"/>
		/// class with the key, value, and type.
		/// </summary>
		/// <param name="key">
		/// The key (setting name).
		/// </param>
		/// <param name="value">
		/// The value associated with the setting.
		/// </param>
		/// <param name="type">
		/// The name of the value type (ie. System.Object, System.String, etc).
		/// </param>
		public FormChangeValue(String key, String value, String type)
			: base() {
			this._key = key;
			this._val = value;
			this._type = type;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the key (setting name).
		/// </summary>
		public String Key {
			get { return this._key; }
		}

		/// <summary>
		/// Gets the value type.
		/// </summary>
		public String ValueType {
			get { return this._type; }
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		public String Value {
			get { return this._val; }
		}

		/// <summary>
		/// Gets the *actual* value. The value returned will be of type <see cref="System.Object"/>
		/// but can then be cast to the actual typed value using the <see cref="ValueType"/>
		/// property by the caller.
		/// </summary>
		public object ValueActual {
			get { return this._valueActual; }
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
		/// Handles the button ok clicked event.
		/// </summary>
		/// <param name="sender">
		/// The object sending the event call.
		/// </param>
		/// <param name="e">
		/// The event arguments.
		/// </param>
		protected void OnButtonOkClicked(object sender, EventArgs e) {
			Boolean isBad = false;
			String typeName = this.entryType.Text;
			String val = this.entryValue.Text.Trim();
			switch (typeName) {
				case "System.Object":
					this._valueActual = (object)val;
					break;
				case "System.String":
					this._valueActual = val;
					break;
				case "float":
					Single s = 0;
					isBad = (!Single.TryParse(val, out s));
					if (!isBad) {
						this._valueActual = s;
					}
					break;
				case "long":
					Int64 l = 0;
					isBad = (!Int64.TryParse(val, out l));
					if (!isBad) {
						this._valueActual = l;
					}
					break;
				case "System.Boolean":
					Boolean b = false;
					isBad = (!Boolean.TryParse(val, out b));
					if (!isBad) {
						this._valueActual = b;
					}
					break;
				case "short":
					Int16 ss = 0;
					isBad = (!Int16.TryParse(val, out ss));
					if (!isBad) {
						this._valueActual = ss;
					}
					break;
				case "System.Char":
					Char c;
					isBad = (!Char.TryParse(val, out c));
					if (!isBad) {
						this._valueActual = c;
					}
					break;
				case "System.Decimal":
					Decimal d = 0;
					isBad = (!Decimal.TryParse(val, out d));
					if (!isBad) {
						this._valueActual = d;
					}
					break;
				case "System.Double":
					Double dd = 0;
					isBad = (!Double.TryParse(val, out dd));
					if (!isBad) {
						this._valueActual = dd;
					}
					break;
				case "System.Int32":
					Int32 i = 0;
					isBad = (!Int32.TryParse(val, out i));
					if (!isBad) {
						this._valueActual = i;
					}
					break;
				case "System.DateTime":
					DateTime dt = DateTime.MinValue;
					isBad = (!DateTime.TryParse(val, out dt));
					if (!isBad) {
						this._valueActual = dt;
					}
					break;
			}

			if (isBad) {
				MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok,
				                                     "Specified value cannot be parsed into type: " + typeName);
				md.Run();
				md.Destroy();
				this.entryValue.Activate();
				return;
			}

			this._response = ResponseType.Ok;
			this.Dispose();
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
			this._response = ResponseType.Cancel;
			this.Dispose();
		}

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
			this.entrySetting.Text = this._key;
			this.entryType.Text = this._type;
			this.entryValue.Text = this._val;
		}
		#endregion
	}
}

