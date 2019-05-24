//-----------------------------------------------------------------------
// <copyright file="Player.cs" company="Brett Beard">
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
    using System.Collections.ObjectModel;    

    /// <summary>
    /// Player class.
    /// </summary>
    public class Player : ICloneable
    {
        /// <summary>
        /// The batting stats.
        /// </summary>
        private BattingStats battingStats = new BattingStats();

        /// <summary>
        /// The pitching stats.
        /// </summary>
        private PitchingStats pitchingStats = new PitchingStats();

        /// <summary>
        /// The fielding starts
        /// </summary>
        private FieldingStarts fieldingStarts = new FieldingStarts();

        /// <summary>
        /// The fatigue instance.
        /// </summary>
        private Fatigue fatigue = new Fatigue();

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public String Identifier { get; set; }

        /// <summary>
        /// Gets or sets the bats.
        /// </summary>
        /// <value>
        /// The bats.
        /// </value>
        public Bats Bats { get; set; }

        /// <summary>
        /// Gets or sets the throws.
        /// </summary>
        /// <value>
        /// The throws.
        /// </value>
        public Throws Throws { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public String FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public String LastName { get; set; }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>
        /// The speed.
        /// </value>
        public Int32 Speed { get; set; }

        /// <summary>
        /// Gets or sets the batting stats.
        /// </summary>
        /// <value>
        /// The batting stats.
        /// </value>
        public BattingStats BattingStats
        {
            get
            {
                return this.battingStats;
            }

            set
            {
                this.battingStats = value;
            }
        }

        /// <summary>
        /// Gets the fielding starts.
        /// </summary>
        /// <value>
        /// The fielding starts.
        /// </value>
        public FieldingStarts FieldingStarts
        {
            get
            {
                return this.fieldingStarts;
            }
        }

        /// <summary>
        /// Gets the pitching stats.
        /// </summary>
        /// <value>
        /// The pitching stats.
        /// </value>
        public PitchingStats PitchingStats
        {
            get
            {
                return this.pitchingStats;
            }
        }

        /// <summary>
        /// Gets the fatigue.
        /// </summary>
        /// <value>
        /// The fatigue.
        /// </value>
        public Fatigue Fatigue
        {
            get
            {
                return this.fatigue;
            }
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    /// <summary>
    /// Player collection class.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.Collection{BinaryBaseball.Common.Player}" />
    public class Players : Collection<Player>
    {
        /// <summary>
        /// Sort the players by the specified stat.
        /// </summary>
        /// <param name="sortStat">The sort stat.</param>
        public void SortByStat(StatToSort sortStat)
        {
            List<Player> list = (List<Player>)this.Items;
            var comparer = new PlayerStatsComparer(sortStat);
            list.Sort(comparer);
        }

        /// <summary>
        /// Sort the players by position starts.
        /// </summary>
        /// <param name="position">The position.</param>
        public void SortByPositionStarts(Int32 position)
        {
            List<Player> list = (List<Player>)this.Items;
            var comparer = new FieldingStartsComparer(position);
            list.Sort(comparer);
        }

        /// <summary>
        /// Gets the starters for position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>A player for the specified position.</returns>
        public Players GetStartersForPosition(Int32 position)
        {
            var starters = new Players();

            // Sort the players by starts at the position
            this.SortByPositionStarts(position);
            
            // Iterate through each player
            foreach (var player in this.Items)
            {
                // Get the start for this player
                var start = player.FieldingStarts.Find(position);
                if (start != null)
                {
                    // Add the player to the list
                    starters.Add(player);
                }
                else
                {
                    break;
                }
            }

            return starters;
        }
    }
}
