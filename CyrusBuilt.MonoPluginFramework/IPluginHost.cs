//
//  IPluginHost.cs
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
using CyrusBuilt.MonoPluginFramework.UI;

namespace CyrusBuilt.MonoPluginFramework
{
	/// <summary>
	/// A plugin host interface. Any type that will act as a host for plugin
	/// assemblies must implement this interface.
	/// </summary>
	public interface IPluginHost : IDisposable
	{
		/// <summary>
		/// Gets the startup path.
		/// </summary>
		String StartupPath { get; }

		/// <summary>
		/// Gets the available plugins.
		/// </summary>
		AvailablePluginCollection AvailablePlugins { get; }

		/// <summary>
		/// Gets a value indicating whether this <see cref="CyrusBuilt.MonoPluginFramework.IPluginHost"/>
		/// is initialized.
		/// </summary>
		Boolean Initialized { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is disposed.
		/// </summary>
		Boolean IsDisposed { get; }

		/// <summary>
		/// Initialize the plugin host.
		/// </summary>
		void Initialize();

		/// <summary>
		/// Loads the specified plugin's configuration.
		/// </summary>
		/// <param name="plugin">
		/// The plugin to load the configuration from.
		/// </param>
		/// <returns>
		/// If successful, the plugin's configuration; Otherwise, null. A null
		/// return can occur if the specified plugin does not have any settings
		/// to configure.
		/// </returns>
		PluginConfiguration LoadPluginConfiguration(IPlugin plugin);
		
		/// <summary>
		/// Saves the specified plugin's configuration.
		/// </summary>
		/// <param name="plugin">
		/// The plugin containing the configuration to save.
		/// </param>
		/// <param name="config">
		/// The configuration to save.
		/// </param>
		void SavePluginConfiguration(IPlugin plugin, PluginConfiguration config);

		/// <summary>
		/// Gets the plugin's configuration dialog.
		/// </summary>
		/// <param name="plugin">
		/// The plugin to get the configuration from.
		/// </param>
		/// <returns>
		/// A dialog form containing the settings read from the specified
		/// plugin's configuration.
		/// </returns>
		FormSettingsDialog GetConfigurationDialog(AvailablePlugin plugin);
	}
}

