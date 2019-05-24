//-----------------------------------------------------------------------
// <copyright file="BattingOrder.cs" company="Brett Beard">
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

namespace BinaryBaseball.Simulation
{
    using System;

    using BinaryBaseball.Common;

    /// <summary>
    /// Batting order class.
    /// </summary>
    public class BattingOrder
    {
        /// <summary>
        /// The slots.
        /// </summary>
        private Player[] slots = new Player[9];

        /// <summary>
        /// The order index.
        /// </summary>
        private Int32 orderIndex = 0;

        /// <summary>
        /// Gets or sets the <see cref="Player"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="Player"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns>The player.</returns>
        public Player this[Int32 index]
        {
            get
            {
                return this.slots[index];
            }

            set
            {
                this.slots[index] = value;
            }
        }

        /// <summary>
        /// Move to the next batter.
        /// </summary>
        /// <returns>The batter.</returns>
        public Player NextBatter()
        {
            Player player = this.slots[this.orderIndex++];

            if (player == null)
            {
            }

            if (this.orderIndex == 9)
            {
                this.orderIndex = 0;
            }

            return player;
        }
    }
}
