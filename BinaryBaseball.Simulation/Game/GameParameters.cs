//-----------------------------------------------------------------------
// <copyright file="GameParameters.cs" company="Brett Beard">
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
    /// Game parameters class.
    /// </summary>
    public class GameParameters
    {
        /// <summary>
        /// The teams.
        /// </summary>
        private Team[] teams = new Team[2];

        /// <summary>
        /// The managers.
        /// </summary>
        private Manager[] managers = new Manager[2];

        /// <summary>
        /// The league averages.
        /// </summary>
        private LeagueAverages leagueAverages = new LeagueAverages();

        /// <summary>
        /// The use starting rotation.
        /// </summary>
        private Boolean useStartingRotation = true;

        /// <summary>
        /// The rotation number.
        /// </summary>
        private Int32[] rotationNumber = new Int32[2];

        /// <summary>
        /// Gets the teams.
        /// </summary>
        /// <value>
        /// The teams.
        /// </value>
        public Team[] Teams
        {
            get
            {
                return this.teams;
            }
        }

        /// <summary>
        /// Gets the managers.
        /// </summary>
        /// <value>
        /// The managers.
        /// </value>
        public Manager[] Managers
        {
            get
            {
                return this.managers;
            }
        }

        /// <summary>
        /// Gets the league averages.
        /// </summary>
        /// <value>
        /// The league averages.
        /// </value>
        public LeagueAverages LeagueAverages
        {
            get
            {
                return this.leagueAverages;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use starting rotation].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use starting rotation]; otherwise, <c>false</c>.
        /// </value>
        public bool UseStartingRotation
        {
            get
            {
                return this.useStartingRotation;
            }

            set
            {
                this.useStartingRotation = value;
            }
        }

        /// <summary>
        /// Gets or sets the rotation number.
        /// </summary>
        /// <value>
        /// The rotation number.
        /// </value>
        public int[] RotationNumber
        {
            get
            {
                return this.rotationNumber;
            }

            set
            {
                this.rotationNumber = value;
            }
        }
    }
}
