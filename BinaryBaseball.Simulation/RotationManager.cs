//-----------------------------------------------------------------------
// <copyright file="RotationManager.cs" company="Brett Beard">
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

    /// <summary>
    /// Rotation manager class.
    /// </summary>
    public class RotationManager
    {
        /// <summary>
        /// The rotation dictionary.
        /// </summary>
        private Dictionary<String, Int32> rotationDictionary = new Dictionary<String, Int32>();

        /// <summary>
        /// Gets the rotation number.
        /// </summary>
        /// <param name="teamId">The team identifier.</param>
        /// <returns>The rotation number.</returns>
        public Int32 GetRotationNumber(String teamId)
        {
            Int32 rotationNumber = 0;

            if (this.rotationDictionary.TryGetValue(teamId, out rotationNumber) == false)
            {
                this.rotationDictionary.Add(teamId, rotationNumber);
            }

            return rotationNumber;
        }

        /// <summary>
        /// Updates the specified team identifier.
        /// </summary>
        /// <param name="teamId">The team identifier.</param>
        public void Update(String teamId)
        {
            if (this.rotationDictionary.ContainsKey(teamId))
            {
                if (++this.rotationDictionary[teamId] > 4)
                {
                    this.rotationDictionary[teamId] = 0;
                }
            }
            else
            {
                // TBD
            }
        }
    }
}
