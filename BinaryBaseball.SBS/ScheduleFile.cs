//-----------------------------------------------------------------------
// <copyright file="ScheduleFile.cs" company="Brett Beard">
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

namespace BinaryBaseball.SBS
{
    using System;
    using System.IO;

    using BinaryBaseball.Common;

    /// <summary>
    /// SBS schedule file class.
    /// </summary>
    public class ScheduleFile
    {
        /// <summary>
        /// Loads the schedule file (via stream) and parses it.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The schedule.</returns>
        public static Schedule Load(Stream stream)
        {
            Schedule schedule = new Schedule();

            using (var reader = new StreamReader(stream))
            {
                Boolean first = true;
                while (reader.EndOfStream == false)
                {
                    var rawBuffer = new Char[431];
                    reader.ReadBlock(rawBuffer, 0, 430);

                    // Skip first record
                    if (first)
                    {
                        first = false;
                        continue;
                    }

                    if (rawBuffer[0].Equals('D') == false)
                    {
                        String buffer = new string(rawBuffer);
                        String date = buffer.Substring(2, 8);                        
                        
                        for (Int32 gameNumber = 0; gameNumber < 15; gameNumber++)
                        {
                            Int32 offset = 10 + (gameNumber * 28);
                            String visitor = buffer.Substring(offset, 8).TrimEnd(' ');
                            String home = buffer.Substring(offset + 8, 8).TrimEnd(' ');

                            if (visitor != String.Empty)
                            {
                                var game = new ScheduledGame();
                                game.When = DateTime.Parse(date);
                                game.VisitorIdentifier = visitor;
                                game.HomeIdentifier = home;
                                schedule.Games.Add(game);
                            }
                        }
                    }
                }                
            }

            return schedule;
        }
    }
}
