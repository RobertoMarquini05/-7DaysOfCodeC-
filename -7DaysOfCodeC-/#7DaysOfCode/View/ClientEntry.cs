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
            try
            {
                ConsoleKeyInfo key;
                char input;
                do
                {
                    Console.WriteLine("Deseja adotar este Pokémon? (S/N)");
                    key = Console.ReadKey();
                    input = char.ToUpper(key.KeyChar);
                    Console.WriteLine();

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
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar entrada: {ex.Message}. Considerando 'Não' como resposta.");
                return false;
            }
        }
    }
}