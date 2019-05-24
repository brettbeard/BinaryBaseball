//-----------------------------------------------------------------------
// <copyright file="Enums.cs" company="Brett Beard">
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
    /// <summary>
    /// Bats enumerated type.
    /// </summary>
    public enum Bats
    {
        /// <summary>
        /// Unknown batting side.
        /// </summary>
        Unknown,

        /// <summary>
        /// Bats from the right side.
        /// </summary>
        Right,

        /// <summary>
        /// Bats from the left side.
        /// </summary>
        Left,

        /// <summary>
        /// Bats from both sides.
        /// </summary>
        Switch
    }

    /// <summary>
    /// Throws enumerated type.
    /// </summary>
    public enum Throws
    {
        /// <summary>
        /// Unknown throwing arm.
        /// </summary>
        Unknown,

        /// <summary>
        /// Throws with the right arm.
        /// </summary>
        Right,

        /// <summary>
        /// Throws with the left arm.
        /// </summary>
        Left
    }

    /// <summary>
    /// Stat to sort by enumerated type.
    /// </summary>
    public enum StatToSort
    {
        /// <summary>
        /// Sort by saves.
        /// </summary>
        Saves,

        /// <summary>
        /// Sort by games started.
        /// </summary>
        GamesStarted,

        /// <summary>
        /// Sort by batter hits.
        /// </summary>
        Batter_Hits,

        /// <summary>
        /// Sort by innings.
        /// </summary>
        Innings
    }
}
