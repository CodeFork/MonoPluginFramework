//
//  DiagResult.cs
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

namespace CyrusBuilt.MonoPluginFramework.Diagnostics
{
	/// <summary>
	/// The result of plugin diagnostics.
	/// </summary>
	public class DiagResult
	{
		#region Fields
		private Boolean _exists = false;
		private Boolean _isValid = false;
		private Boolean _isAssembly = false;
		private String _reasonNotValid = String.Empty;
		private Version _assemblyVersion = null;
		private ProcessorArchitecture _arch = ProcessorArchitecture.None;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CyrusBuilt.MonoPluginFramework.Diagnostics.DiagResult"/>
		/// class with the result values.
		/// </summary>
		/// <param name="exists">
		/// Set true if the plugin assembly exists.
		/// </param>
		/// <param name="valid">
		/// Set true if the plugin is valid (compatible with this framework).
		/// </param>
		/// <param name="isAssm">
		/// The inspected file is a Mono/.NET assembly.
		/// </param>
		/// <param name="reasonInvalid">
		/// The reason the plugin is not valid.
		/// </param>
		/// <param name="ver">
		/// The plugin version.
		/// </param>
		/// <param name="pa">
		/// The plugin processor architecture.
		/// </param>
		public DiagResult(Boolean exists, Boolean valid, Boolean isAssm, String reasonInvalid,
		                  Version ver, ProcessorArchitecture pa) {
			this._exists = exists;
			this._isValid = valid;
			this._isAssembly = isAssm;
			this._reasonNotValid = reasonInvalid;
			this._assemblyVersion = ver;
			this._arch = pa;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets a value indicating whether the plugin assembly exists.
		/// </summary>
		public Boolean Exists {
			get { return this._exists; }
		}

		/// <summary>
		/// Gets a value indicating whether the plugin is valid.
		/// </summary>
		public Boolean IsValid {
			get { return this._isValid; }
		}

		/// <summary>
		/// Gets a value indicating whether the plugin is an assembly.
		/// </summary>
		public Boolean IsAssembly {
			get { return this._isAssembly; }
		}

		/// <summary>
		/// Gets the reason the plugin is not valid.
		/// </summary>
		public String ReasonNotValid {
			get { return this._reasonNotValid; }
		}

		/// <summary>
		/// Gets the assembly version of the plugin.
		/// </summary>
		public Version AssemblyVersion {
			get { return this._assemblyVersion; }
		}

		/// <summary>
		/// Gets the processor architecture the plugin assembly was targeted
		/// for when the assembly was built.
		/// </summary>
		public ProcessorArchitecture Arch {
			get { return this._arch; }
		}
		#endregion
	}
}

