//
//  PluginFailedEventArgs.cs
//
//  Author:
//       cyrusbuilt <cyrusbuilt at gmail dot com>
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

namespace CyrusBuilt.MonoPluginFramework
{
	/// <summary>
	/// Plugin failed event arguments.
	/// </summary>
	public class PluginFailedEventArgs : EventArgs
	{
		#region Fields
		private Exception _ex = null;
		private String _msg = String.Empty;
		private IPlugin _plugin = null;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CyrusBuilt.MonoPluginFramework.PluginFailedEventArgs"/>
		/// class with a message describing the failure.
		/// </summary>
		/// <param name="msg">
		/// The message describing the failure.
		/// </param>
		public PluginFailedEventArgs(String msg)
		 : base() {
			this._msg = msg;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CyrusBuilt.MonoPluginFramework.PluginFailedEventArgs"/>
		/// class with a message describing the failure and the exception that
		/// is the cause of the failure.
		/// </summary>
		/// <param name="msg">
		/// The message describing the failure.
		/// </param>
		/// <param name="ex">
		/// The exception that is the cause of the failure.
		/// </param>
		public PluginFailedEventArgs(String msg, Exception ex)
			: base() {
			this._msg = msg;
			this._ex = ex;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CyrusBuilt.MonoPluginFramework.PluginFailedEventArgs"/>
		/// class with the message describing the failure and the plugin that
		/// that failed processing.
		/// </summary>
		/// <param name="msg">
		/// The message describing the failure.
		/// </param>
		/// <param name="plugin">
		/// A reference to the plugin that failed processing.
		/// </param>
		public PluginFailedEventArgs(String msg, IPlugin plugin)
			: base() {
			this._msg = msg;
			this._plugin = plugin;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CyrusBuilt.MonoPluginFramework.PluginFailedEventArgs"/>
		/// class with a reference to the plugin that failed and the exception
		/// that is the cause of the failure.
		/// </summary>
		/// <param name="plugin">
		/// A reference to the plugin that failed processing.
		/// </param>
		/// <param name="ex">
		/// The exception that is the cause of the failure.
		/// </param>
		public PluginFailedEventArgs(IPlugin plugin, Exception ex)
			: base() {
			this._plugin = plugin;
			this._ex = ex;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CyrusBuilt.MonoPluginFramework.PluginFailedEventArgs"/>
		/// class with the message describing the failure, a reference to the
		/// plugin that failed processing, and the exception that is the cause
		/// of the failure.
		/// </summary>
		/// <param name="msg">
		/// The message describing the failure.
		/// </param>
		/// <param name="plugin">
		/// A reference to the plugin that failed processing.
		/// </param>
		/// <param name="ex">
		/// The exception that is the cause of the failure.
		/// </param>
		public PluginFailedEventArgs(String msg, IPlugin plugin, Exception ex)
			: base() {
			this._msg = msg;
			this._plugin = plugin;
			this._ex = ex;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the message describing the failure.
		/// </summary>
		public String Message {
			get { return this._msg; }
		}

		/// <summary>
		/// Gets the exception that is the failure cause.
		/// </summary>
		public Exception FailureCause {
			get { return this._ex; }
		}

		/// <summary>
		/// Gets the plugin that failed processing.
		/// </summary>
		public IPlugin Plugin {
			get { return this._plugin; }
		}
		#endregion
	}
}

