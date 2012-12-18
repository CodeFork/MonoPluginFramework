//
//  PluginManager.cs
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
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using CyrusBuilt.MonoPluginFramework.Events;
using CyrusBuilt.MonoPluginFramework.Diagnostics;

namespace CyrusBuilt.MonoPluginFramework
{
	/// <summary>
	/// Manages the locating, loading, and unloading of plugins. This is a
	/// serializable singleton class. This means that this class can only be
	/// instantiated *once* using the instance getter and then all 
	/// </summary>
	[Serializable]
	public sealed class PluginManager : IPluginHost, ISerializable
	{
		#region Fields
		private String _startupPath = String.Empty;
		private AvailablePluginCollection _plugins = null;
		private SerializationInfo _serializationInfo = null;
		private StreamingContext _streamingContext;
		private static Boolean _isDisposed = false;
		private static Boolean _initialized = false;
		private static Int32 _refCount = 0;
		private static volatile PluginManager _instance = null;
		private static readonly Object _padlock = new Object();
		#endregion

		#region Constructors and Destructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CyrusBuilt.MonoPluginFramework.PluginManager"/>
		/// class. This is the default private constructor and is only used internally.
		/// </summary>
		private PluginManager() {
			this._startupPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CyrusBuilt.MonoPluginFramework.PluginManager"/>
		/// class that is serializable and uses the specified serialization data.
		/// </summary>
		/// <param name="info">
		/// A <see cref="SerializationInfo"/> object that contains the information
		/// required to serialize the new <see cref="PluginManager"/> instance.
		/// </param>
		/// <param name="context">
		/// A <see cref="StreamingContext"/> object that contains the source
		/// and destination of the serialized stream associated with the new
		/// <see cref="PluginManager"/> instance.
		/// </param>
		public PluginManager(SerializationInfo info, StreamingContext context) {
			this._serializationInfo = info;
			this._streamingContext = context;
		}

		/// <summary>
		/// Guarantees a singleton instance of this class (thread safe).
		/// </summary>
		/// <value>
		/// The first call to this instantiator will return a new instance of this
		/// class object.  Any additional calls that follow will return a reference
		/// to the already created object instance (double-check locking method).
		/// </value>
		public static PluginManager Instance {
			get {
				lock (_padlock) {
					if (_instance == null) {
						_instance = new PluginManager();
						_isDisposed = false;
					}
					_refCount++;
					return _instance;
				}
			}
		}

		/// <summary>
		/// Method for disposing object references, only if all other references
		/// to this class have already been disposed.  This also disposes
		/// managed resources.
		/// </summary>
		/// <param name="disposing">
		/// Flag for indicating that this was called from the public <see cref="Dispose()"/>
		/// method, and thus we really do want to dispose this class reference.
		/// </param>
		private void Dispose(Boolean disposing) {
			if (_isDisposed) {
				return;
			}

			if (this._plugins != null) {
				lock (this._plugins) {
					foreach (AvailablePlugin p in this._plugins) {
						if ((p.Instance.Initialized) || (!p.Instance.IsDisposed)) {
							p.Instance.Dispose();
						}
					}
					this._plugins.Clear();
				}
				this._plugins = null;
			}

			if (disposing) {
				lock (_padlock) {
					if (_refCount == 0) {
						_instance = null;
					}
				}
			}

			this._serializationInfo = null;
			_isDisposed = true;
			_initialized = false;
		}

		/// <summary>
		/// Releases all resource used by the <see cref="CyrusBuilt.MonoPluginFramework.PluginManager"/>
		/// object.
		/// </summary>
		/// <remarks>
		/// Call <see cref="Dispose"/> when you are finished using the
		/// <see cref="CyrusBuilt.MonoPluginFramework.PluginManager"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="CyrusBuilt.MonoPluginFramework.PluginManager"/>
		/// in an unusable state. After calling <see cref="Dispose"/>, you must
		/// release all references to the <see cref="CyrusBuilt.MonoPluginFramework.PluginManager"/>
		/// so the garbage collector can reclaim the memory that the
		/// <see cref="CyrusBuilt.MonoPluginFramework.PluginManager"/> was occupying.
		/// </remarks>
		public void Dispose() {
			_refCount--;
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the collection of all plugins found and loaded by the
		/// <see cref="FindPlugins()"/> method.
		/// </summary>
		public AvailablePluginCollection AvailablePlugins {
			get { return this._plugins; }
		}

		/// <summary>
		/// Gets the startup path.
		/// </summary>
		public String StartupPath {
			get { return this._startupPath; }
		}

		/// <summary>
		/// Gets a value indicating whether this instance is disposed.
		/// </summary>
		public Boolean IsDisposed {
			get { return _isDisposed; }
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="CyrusBuilt.MonoPluginFramework.PluginManager"/>
		/// is initialized.
		/// </summary>
		public Boolean Initialized {
			get { return _initialized; }
		}
		#endregion

		#region Methods
		/// <summary>
		/// This method is called when serializing this class (singleton).
		/// </summary>
		/// <param name="info">
		/// The contextual information about the source or destination.
		/// </param>
		/// <param name="context">
		/// The object that holds the serialized object data.
		/// </param>
		[SecurityPermissionAttribute(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) {
			info.SetType(typeof(SingletonSerializationHelper));
		}

		/// <summary>
		/// Initializes the plugin manager.
		/// </summary>
		public void Initialize() {
			if (_initialized) {
				return;
			}
			this._plugins = new AvailablePluginCollection();
			_initialized = true;
		}

		/// <summary>
		/// Finds and loads plugins located in the specified directory.
		/// </summary>
		/// <param name="directory">
		/// The directory where the plugins are located.
		/// </param>
		/// <exception cref="InvalidOperationException">
		/// The plugin manager has not been initialized.
		/// </exception>
		public void FindPlugins(DirectoryInfo directory) {
			if (!_initialized) {
				throw new InvalidOperationException("PluginManager has not been initialized.");
			}
			
			if (directory == null) {
				return;
			}
			
			this._plugins.Clear();
			if (directory.Exists) {
				foreach (FileInfo fi in directory.GetFiles("*.dll")) {
					this.AddPlugin(fi);
				}
			}
		}

		/// <summary>
		/// Finds and loads plugins located in the same directory as this
		/// assembly.
		/// </summary>
		public void FindPlugins() {
			this.FindPlugins(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory));
		}
		
		/// <summary>
		/// Closes all loaded plugins.
		/// </summary>
		public void ClosePlugins() {
			if (!_initialized) {
				return;
			}
			
			foreach (AvailablePlugin plugin in this._plugins) {
				plugin.Instance.Dispose();
			}
			this._plugins.Clear();
		}

		/// <summary>
		/// Loads the specified plugin and adds it to the managed plugin
		/// collection.
		/// </summary>
		/// <param name="file">
		/// The assembly (*.dll file) that is the plugin.
		/// </param>
		private void AddPlugin(FileInfo file) {
			if ((file == null) || (!file.Exists)) {
				return;
			}

			Type typeInterface = null;
			IPlugin instance = null;
			AvailablePlugin newPlugin = null;
			Assembly pluginAssembly = Assembly.LoadFrom(file.FullName);
			foreach (Type pluginType in pluginAssembly.GetTypes()) {
				if ((pluginType.IsPublic) && (!pluginType.IsAbstract)) {
					typeInterface = pluginType.GetInterface("CyrusBuilt.MonoPluginFramework.IPlugin", true);
					if (typeInterface != null) {
						// Load the assembly instance if it is a valid plugin.
						instance = (IPlugin)Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString()));
						
						// Initialize the plugin and add it to the managed collection.
						newPlugin = new AvailablePlugin(instance, file.FullName);
						newPlugin.Instance.Host = this;
						newPlugin.Instance.Initialize();
						this._plugins.Add(newPlugin);
					}
				}
			}
		}

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
		/// <exception cref="ArgumentNullException">
		/// <paramref name="plugin"/> cannot be null.
		/// </exception>
		public PluginConfiguration LoadPluginConfiguration(IPlugin plugin) {
			if (plugin == null) {
				throw new ArgumentNullException("plugin");
			}
			return plugin.GetConfiguration();
		}

		/// <summary>
		/// Saves the specified plugin's configuration.
		/// </summary>
		/// <param name="plugin">
		/// The plugin containing the configuration to save.
		/// </param>
		/// <param name="config">
		/// The configuration to save.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="plugin"/> cannot be null.
		/// </exception>
		public void SavePluginConfiguration(IPlugin plugin, PluginConfiguration config) {
			if (plugin == null) {
				throw new ArgumentNullException("plugin");
			}
			
			if (config != null) {
				if ((!config.IsEmpty) && (config.IsDirty)) {
					plugin.SaveConfiguration(config);
					config.ClearDirty();
				}
			}
		}
		#endregion
	}

	/// <summary>
	/// A helper class for serialization of the <see cref="PluginManager"/> class object.
	/// </summary>
	[Serializable]
	internal sealed class SingletonSerializationHelper : IObjectReference
	{
		/// <summary>
		/// This method is called after this object is deserialized.
		/// </summary>
		/// <param name="context">
		/// The contextual information about the source or destination.
		/// </param>
		/// <returns>
		/// A reference to the <see cref="PluginManager"/> singleton instance.
		/// </returns>
		public Object GetRealObject(StreamingContext context) {
			return PluginManager.Instance;
		}
	}
}

