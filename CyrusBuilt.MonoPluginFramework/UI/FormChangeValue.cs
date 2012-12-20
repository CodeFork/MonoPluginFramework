//
//  FormChangeValue.cs
//
//  Author:
//       Chris Brunner <cyrusbuilt at gmail dot com>
//
//  Copyright (c) 2012 Copyright (c) 2012 CyrusBuilt
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
	public partial class FormChangeValue : Dialog
	{
		#region Fields
		private String _key = String.Empty;
		private String _val = String.Empty;
		private String _type = String.Empty;
		#endregion

		public FormChangeValue(String key, String value, String type)
			: base() {
			this._key = key;
			this._val = value;
			this._type = type;
		}

		protected void OnButtonOkClicked(object sender, EventArgs e) {

		}

		protected void OnButtonCancelClicked(object sender, EventArgs e) {

		}

		protected void OnRealized(object sender, EventArgs e) {

		}
	}
}

