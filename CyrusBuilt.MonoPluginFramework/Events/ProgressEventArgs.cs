//
//  ProgressEventArgs.cs
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

namespace CyrusBuilt.MonoPluginFramework.Events
{
	/// <summary>
	/// Plugin progress event arguments.
	/// </summary>
	public class ProgressEventArgs : EventArgs
	{
		#region Fields
		private Int32 _total = 0;
		private Int32 _step = 0;
		private String _itemName = String.Empty;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CyrusBuilt.MonoPluginFramework.Events.ProgressEventArgs"/>
		/// class with the current step, total steps, and the name of the
		/// current item being processed.
		/// </summary>
		/// <param name="step">
		/// The current step.
		/// </param>
		/// <param name="total">
		/// The total number of steps.
		/// </param>
		/// <param name="item">
		/// The name of the current item being processed.
		/// </param>
		public ProgressEventArgs(Int32 step, Int32 total, String item)
			: base() {
			this._step = step;
			this._total = total;
			this._itemName = item;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the name of the item being processed.
		/// </summary>
		public String Item {
			get { return this._itemName; }
		}

		/// <summary>
		/// Gets the step.
		/// </summary>
		public Int32 Step {
			get { return this._step; }
		}

		/// <summary>
		/// Gets the total.
		/// </summary>
		public Int32 Total {
			get { return this._total; }
		}

		/// <summary>
		/// Gets the progress.
		/// </summary>
		public Int32 Progress {
			get {
				if (this._step < 0) {
					this._step = 0;
				}

				if (this._total < 0) {
					this._total = 0;
				}

				if (this._step > this._total) {
					this._step = this._total;
				}

				return ((this._step * 100) / this._total);
			}
		}
		#endregion
	}
}

