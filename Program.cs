
namespace Algorithm.Logic
{
    using Algorithm.Logic.Controller;
    using System;

    public class Program
    {
        /// <summary>
        /// PROBLEMA:
        /// 
        /// Implementar um algoritmo para o controle de posição de um drone emum plano cartesiano (X, Y).
        /// 
        /// O ponto inicial do drone é "(0, 0)" para cada execução do método Evaluate ao ser executado cada teste unitário.
        /// 
        /// A string de entrada pode conter os seguintes caracteres N, S, L, e O representando Norte, Sul, Leste e Oeste respectivamente.
        /// Estes catacteres podem estar presentes aleatóriamente na string de entrada.
        /// Uma string de entrada "NNNLLL" irá resultar em uma posição final "(3, 3)", assim como uma string "NLNLNL" irá resultar em "(3, 3)".
        /// 
        /// Caso o caracter X esteja presente, o mesmo irá cancelar a operação anterior. 
        /// Caso houver mais de um caracter X consecutivo, o mesmo cancelará mais de uma ação na quantidade em que o X estiver presente.
        /// Uma string de entrada "NNNXLLLXX" irá resultar em uma posição final "(1, 2)" pois a string poderia ser simplificada para "NNL".
        /// 
        /// Além disso, um número pode estar presente após o caracter da operação, representando o "passo" que a operação deve acumular.
		/// Este número deve estar compreendido entre 1 e 2147483647.
		/// Deve-se observar que a operação 'X' não suporta opção de "passo" e deve ser considerado inválido. Uma string de entrada "NNX2" deve ser considerada inválida.
        /// Uma string de entrada "N123LSX" irá resultar em uma posição final "(1, 123)" pois a string pode ser simplificada para "N123L"
        /// Uma string de entrada "NLS3X" irá resultar em uma posição final "(1, 1)" pois a string pode ser siplificada para "NL".
        /// 
        /// Caso a string de entrada seja inválida ou tenha algum outro problema, o resultado deve ser "(999, 999)".
        /// 
        /// OBSERVAÇÕES:
        /// Realizar uma implementação com padrões de código para ambiente de "produção". 
        /// Comentar o código explicando o que for relevânte para a solução do problema.
        /// Adicionar testes unitários para alcançar uma cobertura de testes relevânte.
        /// </summary>
        /// <param name="input">String no padrão "N1N2S3S4L5L6O7O8X"</param>
        /// <returns>String representando o ponto cartesiano após a execução dos comandos (X, Y)</returns>
        public static string Evaluate(string input)
        {
            #region somewhat relevant comment block

            /**
             * @dev Jonas Gabriel Souza 
             * @email souzajdev@gmail.com
             * @github https://github.com/souzajdev/
             * */

            /**
             * at first i had no idea about how to solve this;
             * turns out the regex idea came out of the blue 
             * and i was like "well, why not?" xD
             * */

            /** 
             * coding session #1 
             * - begin @ 2019/11/22 2100
             * - end @ 2019/11/22 2359
             * 
             * coding session #2
             * - begin @ 2019/11/25 2100
             * - end @ 2019/11/25 2359
             * 
             * total coding time: 0600
             * */

            #endregion

            // output at specific format
            string output = "({0}, {1})";

            /**
             * regular expressions for matching patterns
             * there were more, one for each cardinal direction and each situation
             * but what the heck i just got 'em merged together
             * */
            string[] patternArray = new string[] {
                // matches characters not included within the pattern
                @"[^NSLOX\d]+",

                // matches numbers only input
                @"^[\d]+$", 
                
                // matches numbers followed by "NSLO"
                @"^[\d]+[NSLO]+",

                // matches "NSLOX123"
                @"[NSLO][X][\d]+", 

                // matches "NSLO123X"
                @"[NSLO][\d]+[X]", 
            
                // matches "NX", "SX", "LX", "OX"
                @"[NSLO][X]",
            
                // matches "N123", "S123", "L123", "O123"
                @"([NSLO])([\d]+)",
            
                // matches 'N', 'S', 'L', 'O'
                @"[NSLO]"
            };

            try
            {
                /**
                 * creates the drone controller, drone and pattern used to recognize input
                 * dc is short for drone controller
                 * */
                DroneController dc = new DroneController(patternArray, 0, 0);

                // handle input
                dc.ProcessInput(input);

                //return statement
                return String.Format(output, dc.Drone.CurrentPosition.X, dc.Drone.CurrentPosition.Y);
            }
            // should be one catch for each custom exceptions
            catch (Exception e)
            {
                // return specified default error return
                return String.Format(output, 999, 999);
            }

        }
    }
}
