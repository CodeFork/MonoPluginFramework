//
//  PluginConfiguration.cs
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
using System.Collections.Generic;

namespace CyrusBuilt.MonoPluginFramework
{
	/// <summary>
	/// Plugin configuration storage using key/value pairs.
	/// </summary>
	public class PluginConfiguration : IDisposable
	{
		#region Fields
		private Dictionary<String, Object> _backingStore = null;
		private Boolean _isDisposed = false;
		private Boolean _isDirty = false;
		#endregion

		#region Configuration
		/// <summary>
		/// Initializes a new instance of the <see cref="CyrusBuilt.MonoPluginFramework.PluginConfiguration"/>
		/// class. This is the default constructor.
		/// </summary>
		public PluginConfiguration() {
			this._backingStore = new Dictionary<String, Object>();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets a value indicating whether this instance is disposed.
		/// </summary>
		public Boolean IsDisposed {
			get { return this._isDisposed; }
		}

		/// <summary>
		/// Gets a value indicating whether this instance is dirty. If true,
		/// then values in the configuration have changed since they were loaded
		/// and have not yet been saved.
		/// </summary>
		public Boolean IsDirty {
			get { return this._isDirty; }
		}

		/// <summary>
		/// Gets a value indicating whether this instance is empty.
		/// </summary>
		public Boolean IsEmpty {
			get { return ((this._backingStore == null) || (this._backingStore.Count == 0)); }
		}

		/// <summary>
		/// Gets a collection of all key names in the configuration.
		/// </summary>
		public Dictionary<String, Object>.KeyCollection AllKeys {
			get { return this._backingStore.Keys; }
		}
		#endregion

		#region Methods
		/// <summary>
		/// Clears the dirty flag. This should be called after the configuration
		/// has been saved or otherwise persisted to disk.
		/// </summary>
		public void ClearDirty() {
			this._isDirty = false;
		}

		/// <summary>
		/// Adds the setting. If the setting already exists, this will just
		/// assign it's value.
		/// </summary>
		/// <param name="key">
		/// The setting name.
		/// </param>
		/// <param name="value">
		/// The setting value.
		/// </param>
		/// <exception cref="ObjectDisposedException">
		/// This instance has been disposed.
		/// </exception>
		public void AddSetting(String key, Object value) {
			if (this._isDisposed) {
				throw new ObjectDisposedException("PluginConfiguration");
			}

			if (this._backingStore.ContainsKey(key)) {
				this._backingStore[key] = value;
			}
			else {
				this._backingStore.Add(key, value);
			}
		}

		/// <summary>
		/// Sets the value of a setting.
		/// </summary>
		/// <param name="key">
		/// The name of the setting to assign a value to.
		/// </param>
		/// <param name="value">
		/// The value to assign.
		/// </param>
		/// <exception cref="ObjectDisposedException">
		/// This instance has been disposed.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// The specified setting name does not exist in the configuration.
		/// </exception>
		public void SetValue(String key, Object value) {
			if (this._isDisposed) {
				throw new ObjectDisposedException("PluginConfiguration");
			}

			if (!this._backingStore.ContainsKey(key)) {
				throw new ArgumentException("The configuration does not contain a setting named " + key, "key");
			}

			if (this._backingStore[key] != value) {
				this._backingStore[key] = value;
				this._isDirty = true;
			}
		}

		/// <summary>
		/// Gets the value of the specified setting.
		/// </summary>
		/// <param name="key">
		/// The name of the setting to get the value of.
		/// </param>
		/// <returns>
		/// If successful, the value of the setting; Otherwise, null if the
		/// specified setting could not be found.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// This instance has been disposed.
		/// </exception>
		public Object GetValue(String key) {
			if (this._isDisposed) {
				throw new ObjectDisposedException("PluginConfiguration");
			}

			if (this._backingStore.ContainsKey(key)) {
				return this._backingStore[key];
			}
			return null;
		}

		/// <summary>
		/// Copies from configuration values from another configuration with
		/// the same settings (key names).
		/// </summary>
		/// <param name="config">
		/// The configuration to copy from.
		/// </param>
		/// <exception cref="ObjectDisposedException">
		/// This instance has been disposed.
		/// </exception>
		public void CopyFromConfig(PluginConfiguration config) {
			if (this._isDisposed) {
				throw new ObjectDisposedException("PluginConfiguration");
			}

			if (config != null) {
				if ((!config.IsDisposed) && (!config.IsEmpty)) {
					foreach (String key in config.AllKeys) {
						if (this._backingStore.ContainsKey(key)) {
							this.SetValue(key, config.GetValue(key));
						}
					}
				}
			}
		}

		/// <summary>
		/// Clears all the configuration values.
		/// </summary>
		public void Clear() {
			if (this._backingStore != null) {
				if ((!this._isDisposed) && (!this.IsEmpty)) {
					foreach (String key in this._backingStore.Keys) {
						this._backingStore[key] = null;
					}
					this._isDirty = true;
				}
			}
		}

		/// <summary>
		/// Releases all resource used by the <see cref="CyrusBuilt.MonoPluginFramework.PluginConfiguration"/>
		/// object.
		/// </summary>
		/// <remarks>
		/// Call <see cref="Dispose"/> when you are finished using the
		/// <see cref="CyrusBuilt.MonoPluginFramework.PluginConfiguration"/>. The <see cref="Dispose"/> method leaves the
		/// <see cref="CyrusBuilt.MonoPluginFramework.PluginConfiguration"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the
		/// <see cref="CyrusBuilt.MonoPluginFramework.PluginConfiguration"/> so the garbage collector can reclaim the memory
		/// that the <see cref="CyrusBuilt.MonoPluginFramework.PluginConfiguration"/> was occupying.
		/// </remarks>
		public void Dispose() {
			if (this._isDisposed) {
				return;
			}

			if (this._backingStore != null) {
				this._backingStore.Clear();
				this._backingStore = null;
			}
			this._isDirty = false;
			this._isDisposed = true;
		}
		#endregion
	}
}

