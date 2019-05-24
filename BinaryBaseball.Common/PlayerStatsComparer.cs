//-----------------------------------------------------------------------
// <copyright file="PlayerStatsComparer.cs" company="Brett Beard">
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
    /// Player stats comparer class.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IComparer{BinaryBaseball.Common.Player}" />
    public class PlayerStatsComparer : IComparer<Player>
    {
        /// <summary>
        /// The stat to sort by.
        /// </summary>
        private StatToSort sortStat;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerStatsComparer"/> class.
        /// </summary>
        /// <param name="sortStat">The sort stat.</param>
        public PlayerStatsComparer(StatToSort sortStat)
        {
            this.sortStat = sortStat;
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
            switch (this.sortStat)
            {
                case StatToSort.GamesStarted:
                    result = y.PitchingStats.GamesStarted.CompareTo(x.PitchingStats.GamesStarted);
                    break;
                case StatToSort.Saves:
                    result = y.PitchingStats.Saves.CompareTo(x.PitchingStats.Saves);
                    break;
                case StatToSort.Batter_Hits:
                    result = y.BattingStats.Hits.CompareTo(x.BattingStats.Hits);
                    break;
                case StatToSort.Innings:
                    result = y.PitchingStats.Innings.CompareTo(x.PitchingStats.Innings);
                    break;
            }

            return result;
        }
    }
}
