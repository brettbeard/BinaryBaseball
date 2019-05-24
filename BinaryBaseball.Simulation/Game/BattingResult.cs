//-----------------------------------------------------------------------
// <copyright file="BattingResult.cs" company="Brett Beard">
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
    /// <summary>
    /// Batting result enumerated type.
    /// </summary>
    public enum BattingResult
    {
        /// <summary>
        /// The walk.
        /// </summary>
        Walk,

        /// <summary>
        /// The single.
        /// </summary>
        Single,

        /// <summary>
        /// The double.
        /// </summary>
        Double,

        /// <summary>
        /// The triple.
        /// </summary>
        Triple,

        /// <summary>
        /// The homerun.
        /// </summary>
        Homerun,

        /// <summary>
        /// The hit batter.
        /// </summary>
        HitBatter,

        /// <summary>
        /// The error.
        /// </summary>
        Error,

        /// <summary>
        /// The out.
        /// </summary>
        Out,

        /// <summary>
        /// The strikeout.
        /// </summary>
        Strikeout,

        /// <summary>
        /// The ground out.
        /// </summary>
        GroundOut,

        /// <summary>
        /// The fly out.
        /// </summary>
        FlyOut,

        /// <summary>
        /// The pop out.
        /// </summary>
        PopOut,

        /// <summary>
        /// The line out.
        /// </summary>
        LineOut
    }
}
