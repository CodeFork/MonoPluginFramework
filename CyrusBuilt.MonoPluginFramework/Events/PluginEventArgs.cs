//
//  PluginStartedEventArgs.cs
//
//  Author:
//       cyrusbuilt <cyrusbuilt at gmail dot com>
//
//  Copyright (c) 2012 CyrusBuilt 2012
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

namespace CyrusBuilt.MonoPluginFramework
{
	/// <summary>
	/// Plugin started event arguments.
	/// </summary>
	public class PluginEventArgs : EventArgs
	{
		private IPlugin _plugin = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="CyrusBuilt.MonoPluginFramework.PluginEventArgs"/>
		/// class with a reference to the plugin that started.
		/// </summary>
		/// <param name="p">
		/// The plugin instance that just started.
		/// </param>
		public PluginEventArgs(IPlugin p)
			: base() {
			this._plugin = p;
		}

		/// <summary>
		/// Gets the plugin that started.
		/// </summary>
		public IPlugin PluginStarted {
			get { return this._plugin; }
		}
	}
}

