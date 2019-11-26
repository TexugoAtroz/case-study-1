using Algorithm.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Algorithm.Logic.Controller
{
    class DroneController
    {
        // properties
        public Drone Drone { get; }
        public DroneInputPattern DroneInputPattern { get; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="x">starting x position</param>
        /// <param name="y">starting y position</param>
        public DroneController(string[] patternArray, Int32 x = 0, Int32 y = 0)
        {
            // init drone
            Drone = new Drone(x, y);

            // @todo create NullPatternArrayException
            if (patternArray == null) throw new Exception("The regular expression array cannot be null");

            // @todo create EmptyPatternArrayException
            if (patternArray.Length == 0) throw new Exception("The regular expression array cannot be empty");

            // init input pattern
            DroneInputPattern = new DroneInputPattern(patternArray);
        }

        /// <summary>
        /// move drone by 1 or 'n' steps
        /// </summary>
        /// <param name="n">number of steps</param>
        /// <returns> true | false </returns>
        public bool MoveDroneNorth(int n = 0)
        {
            // @todo NegativeStepSizeException
            if (n < 0) throw new Exception("Step size cannot be smaller than zero");

            try
            {
                if (n > 0)
                {
                    Drone.CurrentPosition.Y += n;
                }
                else
                {
                    Drone.CurrentPosition.Y += 1;
                }
            } 
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// move drone by 1 or 'n' steps
        /// </summary>
        /// <param name="n">number of steps</param>
        /// <returns> true | false </returns>
        public bool MoveDroneSouth(int n = 0)
        {
            // @todo NegativeStepSizeException
            if (n < 0) throw new Exception("Step size cannot be smaller than zero");

            try
            {
                if (n > 0)
                {
                    Drone.CurrentPosition.Y -= n;
                }
                else
                {
                    Drone.CurrentPosition.Y -= 1;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// move drone by 1 or 'n' steps
        /// </summary>
        /// <param name="n">number of steps</param>
        /// <returns> true | false </returns>
        public bool MoveDroneEast(int n = 0)
        {
            // @todo NegativeStepSizeException
            if (n < 0) throw new Exception("Step size cannot be smaller than zero");

            try
            {
                if (n > 0)
                {
                    Drone.CurrentPosition.X += n;
                }
                else
                {
                    Drone.CurrentPosition.X += 1;
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// move drone by 1 or 'n' steps
        /// </summary>
        /// <param name="n">number of steps</param>
        /// <returns> true | false </returns>
        public bool MoveDroneWest(int n = 0)
        {
            // @todo NegativeStepSizeException
            if (n < 0) throw new Exception("Step size cannot be smaller than zero");

            try
            {
                if (n > 0)
                {
                    Drone.CurrentPosition.X -= n;
                }
                else
                {
                    Drone.CurrentPosition.X -= 1;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// function responsible making the input less complex so that at the end
        /// the remaining input will be something with the [NSLO]+ format
        /// 
        /// this guarantees that the drone will only move the required amount of steps
        /// instead of doing and undoing its movements
        /// </summary>
        /// <param name="input">drone input</param>
        public void ProcessInput(string input)
        {
            // trims the input
            input = input.Trim();

            // @todo create NullOrEmptyInput
            if (String.IsNullOrEmpty(input)) throw new Exception("Input cannot be null or empty");

            // @todo create NullOrWhiteSpaceInput
            if (String.IsNullOrWhiteSpace(input)) throw new Exception("Input cannot be null or whitespace");

            // matches characters not included within the pattern
            // @todo create InvalidCharactersInput
            if (Regex.Match(input, DroneInputPattern.PatternArray[0]).Success) throw new Exception("Input contains invalid characters");

            // matches numbers only input
            // @todo create NumbersOnlyInput
            if (Regex.Match(input, DroneInputPattern.PatternArray[1]).Success) throw new Exception("Input contains numbers only");

            // matches numbers followed by [NSLO]+
            // @todo create StepBeforeDirectionInput
            if (Regex.Match(input, DroneInputPattern.PatternArray[2]).Success) throw new Exception("Input contains a start with steps before direction");

            // matches 'NNX2'
            // @todo create InputOverflowException
            if (Regex.Match(input, DroneInputPattern.PatternArray[3]).Success) throw new Exception("Input does not support 'x' followed by step");

            // matches 'N123X', 'S123X', 'E123X', 'W123X'
            foreach (Match match in Regex.Matches(input, DroneInputPattern.PatternArray[4]))
            {
                // removes the match (which is invalid) from the input
                input = input.Replace(match.Value, "");
            }

            // while finds matches of "NX", "SX", "EX", "WX"
            while (Regex.Match(input, DroneInputPattern.PatternArray[5]).Success)
            {
                foreach (Match match in Regex.Matches(input, DroneInputPattern.PatternArray[5]))
                {
                    // remove the match from the input string
                    input = input.Replace(match.Value, "");
                }
            }

            // matches 'N123', 'S123', 'E123', 'W123
            foreach (Match match in Regex.Matches(input, DroneInputPattern.PatternArray[6]))
            {
                // try parse the numeric part
                if (Int32.TryParse(match.Groups[2].Value, out Int32 count))
                {
                    // check whether the numeric part is bigger than max int32 value
                    if (count >= Int32.MaxValue) throw new Exception("Input overflow exception");

                    /**
                     * insert inside the string the proper amount of step rules;
                     * the Move[Direction]() methods also allow to specify 
                     * the number of steps towards a specific directions
                     * */
                    for (int i = 0; i < count; i += 1)
                    {
                        // inser the step at the position where it is first found
                        input = input.Insert(input.IndexOf(match.Value), match.Groups[1].Value);
                    }
                }
                
                // remove the match from the input string
                input = input.Replace(match.Value, "");
            }

            // matches 'N', 'S', 'E', 'W'
            foreach (Match match in Regex.Matches(input, DroneInputPattern.PatternArray[7], RegexOptions.IgnoreCase))
            {
                // matching value
                string matchString = match.Value;

                switch (matchString)
                {
                    case "N":
                        MoveDroneNorth();
                        break;

                    case "S":
                        MoveDroneSouth();
                        break;

                    case "L":
                        MoveDroneEast();
                        break;

                    case "O":
                        MoveDroneWest();
                        break;

                    default:
                        // do nothing
                        break;
                }
            }
        }

    }
}
