//
//  IPlugin.cs
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
using System.IO;
using CyrusBuilt.MonoPluginFramework.Events;

namespace CyrusBuilt.MonoPluginFramework
{
	/// <summary>
	/// A plugin interface. All assemblies representing plugins to the application
	/// must contain a type that implements this interface.
	/// </summary>
	public interface IPlugin : IDisposable
	{
		#region Events
		/// <summary>
		/// Occurs when the plugin is stopping.
		/// </summary>
		event EventHandler Stopping;

		/// <summary>
		/// Occurs when the plugin has stopped.
		/// </summary>
		event EventHandler Stopped;

		/// <summary>
		/// Occurs when the plugin is starting.
		/// </summary>
		event EventHandler Starting;

		/// <summary>
		/// Occurs when the plugin has started.
		/// </summary>
		event PluginEventHandler Started;

		/// <summary>
		/// Occurs when the plugin makes progress.
		/// </summary>
		event ProgressEventHandler Progress;

		/// <summary>
		/// Occurs when a plugin operation fails.
		/// </summary>
		event PluginFailedEventHandler Failed;
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets plugin host. This would be the application (class)
		/// that implements <see cref="IPluginHost"/> for using this plugin.
		/// </summary>
		IPluginHost Host { get; set; }

		/// <summary>
		/// Gets the name of the plugin.
		/// </summary>
		String Name { get; }
		
		/// <summary>
		/// Gets a description of the plugin.
		/// </summary>
		String Description { get; }
		
		/// <summary>
		/// Gets the plugin's author.
		/// </summary>
		String Author { get; }
		
		/// <summary>
		/// Gets the plugin version.
		/// </summary>
		Version Version { get; }

		/// <summary>
		/// Gets whether or not the plugin has been disposed.
		/// </summary>
		Boolean IsDisposed { get; }
		
		/// <summary>
		/// Gets a flag to indicate whether or not the plugin has been
		/// initialized.
		/// </summary>
		Boolean Initialized { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is busy.
		/// </summary>
		Boolean IsBusy { get; }
		
		/// <summary>
		/// Gets or sets the instance index. This is useful for save file dialogs.
		/// </summary>
		Int32 Index { get; set; }
		#endregion

		#region Methods
		/// <summary>
		/// Initializes the plugin.
		/// </summary>
		void Initialize();

		/// <summary>
		/// Start this instance.
		/// </summary>
		/// <exception cref="ObjectDisposedException">
		/// This plugin instance has been disposed.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This plugin has not yet been initialized.
		/// </exception>
		void Start();

		/// <summary>
		/// Stop this instance.
		/// </summary>
		void Stop();

		/// <summary>
		/// Gets the plugin's configuration.
		/// </summary>
		/// <returns>
		/// The plugin's configuration.
		/// </returns>
		PluginConfiguration GetConfiguration();

		/// <summary>
		/// Sets the plugin's configuration.
		/// </summary>
		/// <param name="config">
		/// The configuration to set.
		/// </param>
		void SetConfiguration(PluginConfiguration config);
		#endregion
	}
}

