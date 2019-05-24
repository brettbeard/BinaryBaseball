//-----------------------------------------------------------------------
// <copyright file="Lineup.cs" company="Brett Beard">
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
    using BinaryBaseball.Common;

    /// <summary>
    /// Lineup class.
    /// </summary>
    public class Lineup
    {
        /// <summary>
        /// The batting order.
        /// </summary>
        private BattingOrder battingOrder = new BattingOrder();

        /// <summary>
        /// The pitcher
        /// </summary>
        private Player pitcher;

        /// <summary>
        /// Gets the batting order.
        /// </summary>
        /// <value>
        /// The batting order.
        /// </value>
        public BattingOrder BattingOrder
        {
            get
            {
                return this.battingOrder;
            }
        }

        /// <summary>
        /// Gets or sets the pitcher.
        /// </summary>
        /// <value>
        /// The pitcher.
        /// </value>
        public Player Pitcher
        {
            get
            {
                return this.pitcher;
            }

            set
            {
                this.pitcher = value;
            }
        }
    }
}
