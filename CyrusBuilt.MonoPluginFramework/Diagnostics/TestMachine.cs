//
//  TestMachine.cs
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

namespace CyrusBuilt.MonoPluginFramework.Diagnostics
{
	/// <summary>
	/// Utility for testing plugin files for problems.
	/// </summary>
	public sealed class TestMachine
	{
		private FileInfo _assembly = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="CyrusBuilt.MonoPluginFramework.Diagnostics.TestMachine"/>
		/// class with the assembly file to test.
		/// </summary>
		/// <param name="assembly">
		/// The assembly file to test.
		/// </param>
		public TestMachine(FileInfo assembly) {
			this._assembly = assembly;
		}

		/// <summary>
		/// Runs diagnostics on the specified file.
		/// </summary>
		/// <returns>
		/// The result of the diagnostics.
		/// </returns>
		/// <param name="plugin">
		/// The plugin to test.
		/// </param>
		public static DiagResult RunDiagnostics(FileInfo plugin) {
			DiagResult result = null;
			try {
				TestMachine tm = new TestMachine(plugin);
				result = tm.Run();
			}
			catch (Exception ex) {
				String reason = "Could not perform diagnostic tests on the specified file: " + ex.Message;
				result = new DiagResult(false, false, false, reason, null, ProcessorArchitecture.None);
			}
			return result;
		}

		/// <summary>
		/// Run this instance. This performs all the tests. A plugin is
		/// considered valid if all of the following conditions are true:
		/// - File exists.
		/// - File is a Mono/.NET assembly.
		/// - File is not an assembly that has already been loaded.
		/// - Assembly contains a public type that implements IPlugin.
		/// </summary>
		internal DiagResult Run() {
			DiagResult result = null;
			Type typeInterface = null;
			String reason = String.Empty;
			Version v = null;
			Boolean valid = false;
			Boolean isAssm = false;
			ProcessorArchitecture pa = ProcessorArchitecture.None;

			// Does the assembly even exist?
			Boolean exists = ((this._assembly != null) && (this._assembly.Exists));
			try {
				AppDomain temp = AppDomain.CurrentDomain;
				temp.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(this.MyReflectionOnlyResolveEventHandler);
				Assembly asm = Assembly.ReflectionOnlyLoadFrom(this._assembly.FullName);

				// If we got this far, the file exists and is a valid Mono/.NET assembly.
				isAssm = true;

				// Get version and proc arch. Then check to see if the assembly
				// contains a type that implements our plugin interface.
				v = asm.GetName().Version;
				pa = asm.GetName().ProcessorArchitecture;
				foreach (Type asmType in asm.GetTypes()) {
					if ((asmType.IsPublic) && (!asmType.IsAbstract)) {
						typeInterface = asmType.GetInterface("CyrusBuilt.MonoPluginFramework.IPlugin", true);
						if (typeInterface != null) {
							valid = true;
							break;
						}
					}
				}

				if (!valid) {
					reason = "The assembly does not have any public types that implement CyrusBuilt.MonoPluginFramework.IPlugin.";
				}
			}
			catch (FileNotFoundException) {
				exists = false;
				reason = "The file could not be found.";
			}
			catch (BadImageFormatException) {
				exists = true;
				reason = "The file is not an assembly.";
			}
			catch (FileLoadException) {
				exists = true;
				reason = "The assembly has already been loaded.";
			}

			result = new DiagResult(exists, valid, isAssm, reason, v, pa);
			return result;
		}

		/// <summary>
		/// Handles the reflection only resolve event.
		/// </summary>
		/// <returns>
		/// The assembly resolved from the strong path.
		/// </returns>
		/// <param name="sender">
		/// The object sending the event call.
		/// </param>
		/// <param name="args">
		/// The event arguments.
		/// </param>
		private Assembly MyReflectionOnlyResolveEventHandler(Object sender, ResolveEventArgs args) {
			AssemblyName name = new AssemblyName(args.Name);
			String asmToCheck = Path.GetDirectoryName(this._assembly.FullName) + "\\" + name.Name + ".dll";
			if (File.Exists(asmToCheck)) {
				return Assembly.ReflectionOnlyLoadFrom(asmToCheck);
			}
			return Assembly.ReflectionOnlyLoad(args.Name);
		}
	}
}

