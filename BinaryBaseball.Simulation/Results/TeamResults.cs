//-----------------------------------------------------------------------
// <copyright file="TeamResults.cs" company="Brett Beard">
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
    using System.Collections.Generic;

    using BinaryBaseball.Common;

    /// <summary>
    /// Team results class.
    /// </summary>
    public class TeamResults
    {
        /// <summary>
        /// The player results dictionary.
        /// </summary>
        private Dictionary<String, Player> playerResultsDictionary = new Dictionary<string, Player>();

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public String Identifier { get; set; }

        /// <summary>
        /// Gets or sets the wins.
        /// </summary>
        /// <value>
        /// The wins.
        /// </value>
        public Int32 Wins { get; set; }

        /// <summary>
        /// Gets or sets the losses.
        /// </summary>
        /// <value>
        /// The losses.
        /// </value>
        public Int32 Losses { get; set; }

        /// <summary>
        /// Gets the player results dictionary.
        /// </summary>
        /// <value>
        /// The player results dictionary.
        /// </value>
        public Dictionary<string, Player> PlayerResultsDictionary
        {
            get
            {
                return this.playerResultsDictionary;
            }
        }

        /////// <summary>
        /////// Gets the player results.
        /////// </summary>
        /////// <param name="player">The player.</param>
        /////// <returns>The player.</returns>
        ////public Player GetPlayerResults(Player player)
        ////{
        ////    Player playerResults = null;
        ////    if (this.playerResultsDictionary.TryGetValue(player.Identifier, out playerResults) == false)
        ////    {
        ////        playerResults = (Player)player.Clone();
        ////        playerResults.BattingStats = new BattingStats();
        ////        this.playerResultsDictionary.Add(player.Identifier, playerResults);
        ////    }

        ////    return playerResults;
        ////}        
    }
}
