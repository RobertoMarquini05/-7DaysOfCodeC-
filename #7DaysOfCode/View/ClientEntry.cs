using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7DaysOfCode.View
{
    public static class ClientEntry
    {
        public static bool GetCharacteristicsOrNot()
        {
            ConsoleKeyInfo key;
            char input;
            do
            {
                Console.WriteLine("Deseja visualizar as características do seu Pokemon ? (S/N)");
                key = Console.ReadKey(); // Obtém ConsoleKeyInfo
                input = char.ToUpper(key.KeyChar); // Converte para maiúscula
                Console.WriteLine(); // Pula uma linha

                if (input == 'S')
                {
                    Console.WriteLine("Você escolheu Sim!");
                    return true;
                }
                else if (input == 'N')
                {
                    Console.WriteLine("Você escolheu Não!");
                    return false;
                }
                else
                {
                    Console.WriteLine("Entrada inválida! Por favor, pressione S ou N.");
                }
            } while (input != 'S' && input != 'N');

            return false;
        }
    }
}
