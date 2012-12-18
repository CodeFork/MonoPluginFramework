//
//  AvailablePluginCollection.cs
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
using System.Collections;

namespace CyrusBuilt.MonoPluginFramework
{
	/// <summary>
	/// Represents a collection of <see cref="AvailablePlugin"/> objects. This
	/// collection class will not allow duplicates of any one instance.
	/// </summary>
	public class AvailablePluginCollection : CollectionBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CyrusBuilt.MonoPluginFramework.AvailablePluginCollection"/>
		/// class. This is the default constructor.
		/// </summary>
		public AvailablePluginCollection()
			: base() {
		}
		#endregion

		#region Indexers
		/// <summary>
		/// Gets or sets the <see cref="CyrusBuilt.MonoPluginFramework.AvailablePluginCollection"/>
		/// at the specified index.
		/// </summary>
		/// <param name="index">
		/// The zero-based index of the <see cref="AvailablePlugin"/> to get or
		/// set.
		/// </param>
		/// <returns>
		/// The <see cref="AvailablePlugin"/> at the specified index.
		/// </returns>			
		public AvailablePlugin this[Int32 index] {
			get { return base.List[index] as AvailablePlugin; }
			set { base.List[index] = value; }
		}
		#endregion

		#region Methods
		/// <summary>
		/// Adds the specified <see cref="AvailablePlugin"/> to the collection
		/// if it is not already in the collection.
		/// </summary>
		/// <param name="plugin">
		/// The plugin to add to the collection.
		/// </param>
		/// <returns>
		/// If successful, the zero-based index position into which the plugin
		/// was added; Otherwise, -1.
		/// </returns>			
		public Int32 Add(AvailablePlugin plugin) {
			if (!base.List.Contains(plugin)) {
				return base.List.Add(plugin);
			}
			return -1;
		}

		/// <summary>
		/// Inserts a <see cref="AvailablePlugin"/> into the collection at the
		/// specified index.
		/// </summary>
		/// <param name="index">
		/// The zero-based index at which the <see cref="AvailablePlugin"/>
		/// should be inserted.
		/// </param>
		/// <param name="plugin">
		/// The <see cref="AvailablePlugin"/> to insert into the collection.
		/// </param>
		/// <exception cref="DuplicatePluginInstanceException">
		/// An instance of the specified plugin already exists in the collection.
		/// </exception>
		public void Insert(Int32 index, AvailablePlugin plugin) {
			if (base.List.Contains(plugin)) {
				throw new DuplicatePluginInstanceException(plugin);
			}
			base.List.Insert(index, plugin);
		}

		/// <summary>
		/// Removes the first occurrence of a specified <see cref="AvailablePlugin"/>
		/// from the collection.
		/// </summary>
		/// <param name="plugin">
		/// The <see cref="AvailablePlugin"/> to remove from the collection.
		/// </param>
		public void Remove(AvailablePlugin plugin) {
			base.List.Remove(plugin);
		}

		/// <summary>
		/// Determines whether or not the collection contains a specific
		/// <see cref="AvailablePlugin"/>.
		/// </summary>
		/// <param name="plugin">
		/// The <see cref="AvailablePlugin"/> to locate in the collection.
		/// </param>
		/// <returns>
		/// true if the <see cref="AvailablePlugin"/> is found in the collection;
		/// Otherwise, false.
		/// </returns>
		public Boolean Contains(AvailablePlugin plugin) {
			return base.List.Contains(plugin);
		}

		/// <summary>
		/// Determines the index of a specific <see cref="AvailablePlugin"/> in
		/// the collection.
		/// </summary>
		/// <param name="plugin">
		/// The <see cref="AvailablePlugin"/> to locate in the collection.
		/// </param>
		/// <returns>
		/// The index of the <see cref="AvailablePlugin"/> if found in the
		/// collection; Otherwise, -1.
		/// </returns>
		public Int32 IndexOf(AvailablePlugin plugin) {
			return base.List.IndexOf(plugin);
		}

		/// <summary>
		/// Copies the elements of the collection to a <see cref="AvailablePlugin"/>
		/// array, starting at a particular array index.
		/// </summary>
		/// <param name="array">
		/// The one-dimensional log entry array that is the destination of the
		/// elements copied from the collection.  The location array must have
		/// zero-based indexing.
		/// </param>
		/// <param name="index">
		/// The zero-based index in the array at which copying begins.
		/// </param>
		public void CopyTo(AvailablePlugin[] array, Int32 index) {
			base.List.CopyTo(array, index);
		}

		/// <summary>
		/// Finds a plugin in the available Plugins.
		/// </summary>
		/// <param name="nameOrPath">
		/// The name or File path of the plugin to find.
		/// </param>
		/// <returns>
		/// Available Plugin, or null if the plugin is not found.
		/// </returns>
		public AvailablePlugin Find(String nameOrPath) {
			AvailablePlugin found = null;
			foreach (AvailablePlugin ap in this) {
				if ((ap.Instance.Name.Equals(nameOrPath)) || (ap.AssemblyPath.Equals(nameOrPath))) {
					found = ap;
					break;
				}
			}
			return found;
		}

		/// <summary>
		/// Determines if the specified <b>AvailablePlugins</b> collection is
		/// null or empty.
		/// </summary>
		/// <param name="collection">
		/// The <b>AvailablePlugins</b> collection to check.
		/// </param>
		/// <returns>
		/// true if the specified collection is null or empty; Otherwise, false.
		/// </returns>
		public static Boolean IsNullOrEmpty(AvailablePluginCollection collection) {
			return ((collection == null) || (collection.Count == 0));
		}
		#endregion
	}
}

