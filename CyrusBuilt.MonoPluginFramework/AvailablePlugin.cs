//
//  AvailablePlugin.cs
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

namespace CyrusBuilt.MonoPluginFramework
{
	/// <summary>
	/// Available plugin.
	/// </summary>
	public class AvailablePlugin
	{
		#region Type Constants
		private const Int32 HASH_MULTIPLIER = 31;
		#endregion
		
		#region Fields
		private IPlugin _pluginInstance = null;
		private String _assemblyPath = String.Empty;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CyrusBuilt.MonoPluginFramework.AvailablePlugin"/>
		/// class with the plugin instance and assembly path.
		/// </summary>
		/// <param name="instance">
		/// The plugin instance.
		/// </param>
		/// <param name="path">
		/// The full path to the plugin assembly.
		/// </param>
		public AvailablePlugin(IPlugin instance, String path) {
			this._pluginInstance = instance;
			this._assemblyPath = path;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the plugin instance.
		/// </summary>
		public IPlugin Instance {
			get { return this._pluginInstance; }
		}

		/// <summary>
		/// Gets the full path to the plugin assembly.
		/// </summary>
		public String AssemblyPath {
			get { return this._assemblyPath; }
		}
		#endregion

		#region Methods
		/// <summary>
		/// Serves as a hash function for a <see cref="CyrusBuilt.MonoPluginFramework.AvailablePlugin"/>
		/// object.
		/// </summary>
		/// <returns>
		/// A hash code for this instance that is suitable for use in hashing
		/// algorithms and data structures such as a hash table.
		/// </returns>
		public override Int32 GetHashCode() {
			unchecked {
				Int32 verHash = this._pluginInstance.Version.GetHashCode();
				Int32 nameHash = this._pluginInstance.Name.GetHashCode();
				Int32 hashCode = base.GetType().GetHashCode();
				return (hashCode * HASH_MULTIPLIER) ^ verHash ^ nameHash;
			}
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current
		/// <see cref="CyrusBuilt.MonoPluginFramework.AvailablePlugin"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents the current
		/// <see cref="CyrusBuilt.MonoPluginFramework.AvailablePlugin"/>.
		/// </returns>
		public override String ToString() {
			return this._pluginInstance.Name;
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is
		/// equal to the current <see cref="CyrusBuilt.MonoPluginFramework.AvailablePlugin"/>.
		/// </summary>
		/// <param name='obj'>
		/// The <see cref="System.Object"/> to compare with the current
		/// <see cref="CyrusBuilt.MonoPluginFramework.AvailablePlugin"/>.
		/// </param>
		/// <returns>
		/// <c>true</c> if the specified <see cref="System.Object"/> is equal
		/// to the current <see cref="CyrusBuilt.MonoPluginFramework.AvailablePlugin"/>;
		/// otherwise, <c>false</c>.
		/// </returns>
		public override Boolean Equals(Object obj) {
			if (obj == null) {
				return false;
			}
			
			AvailablePlugin ap = obj as AvailablePlugin;
			if ((Object)ap == null) {
				return false;
			}
			
			return ((this._assemblyPath == ap.AssemblyPath) && 
			        (this._pluginInstance == ap.Instance));
		}

		/// <summary>
		/// Determines whether the specified <see cref="CyrusBuilt.MonoPluginFramework.AvailablePlugin"/>
		/// is equal to the current <see cref="CyrusBuilt.MonoPluginFramework.AvailablePlugin"/>.
		/// </summary>
		/// <param name='plugin'>
		/// The <see cref="CyrusBuilt.MonoPluginFramework.AvailablePlugin"/> to
		/// compare with the current <see cref="CyrusBuilt.MonoPluginFramework.AvailablePlugin"/>.
		/// </param>
		/// <returns>
		/// <c>true</c> if the specified <see cref="CyrusBuilt.MonoPluginFramework.AvailablePlugin"/>
		/// is equal to the current <see cref="CyrusBuilt.MonoPluginFramework.AvailablePlugin"/>;
		/// otherwise, <c>false</c>.
		/// </returns>
		public Boolean Equals(AvailablePlugin plugin) {
			if (plugin == null) {
				return false;
			}
			
			if ((Object)plugin == null) {
				return false;
			}
			
			return ((this._assemblyPath == plugin.AssemblyPath) &&
			        (this._pluginInstance == plugin.Instance));
		}
		#endregion
	}
}

