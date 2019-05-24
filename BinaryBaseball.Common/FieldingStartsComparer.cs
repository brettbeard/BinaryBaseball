//-----------------------------------------------------------------------
// <copyright file="FieldingStartsComparer.cs" company="Brett Beard">
// Copyright 2019 Brett Beard
//
// Binary Baseball is free software: you can redistribute it and/or modify 
// it under the terms of the GNU General Public License as published by the 
// Free Software Foundation, either version 3 of the License, or (at your 
// option) any later version.
//
// Binary Baseball is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the GNU General 
// Public License for more details.
//
// You should have received a copy of the GNU General Public License 
// along with Binary Baseball. If not, see http://www.gnu.org/licenses/.
// </copyright>
//-----------------------------------------------------------------------

namespace BinaryBaseball.Common
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Fielding starts comparer class.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IComparer{BinaryBaseball.Common.Player}" />
    public class FieldingStartsComparer : IComparer<Player>
    {
        /// <summary>
        /// The position.
        /// </summary>
        private Int32 position;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldingStartsComparer"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public FieldingStartsComparer(Int32 position)
        {
            this.position = position;
        }

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// Value Condition Less than zero<paramref name="x"/> is less than <paramref name="y"/>.Zero<paramref name="x"/> equals <paramref name="y"/>.Greater than zero<paramref name="x"/> is greater than <paramref name="y"/>.
        /// </returns>
        public Int32 Compare(Player x, Player y)
        {
            Int32 result = 0;

            Int32 xStarts = 0;
            Int32 yStarts = 0;

            var starts = x.FieldingStarts.Find(this.position);
            if (starts != null)
            {
                xStarts = starts.Starts;
            }

            starts = y.FieldingStarts.Find(this.position);
            if (starts != null)
            {
                yStarts = starts.Starts;
            }

            result = yStarts.CompareTo(xStarts);        

            return result;
        }
    }
}
