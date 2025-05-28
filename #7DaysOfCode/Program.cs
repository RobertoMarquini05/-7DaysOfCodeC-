using System;
using System.Threading.Tasks;
using _7DaysOfCode.BLL;

class Program
{
    static async Task Main(string[] args)
    {
        var pokemonsManager = new PokemonsManager();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Bem vindo ao universo Pokemon !");
            Console.WriteLine("======================================================");
            Console.WriteLine("1- Escolher o seu pokemon");
            Console.WriteLine("2- Visualizar os pokemons da sua pokedex");
            Console.WriteLine("3- Sair");
            Console.WriteLine("======================================================");
            Console.Write("Digite a opção escolhida: ");

            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    pokemonsManager.Listar();
                    await pokemonsManager.GetCharacteristicsAsync();
                    break;
                case "2":
                    pokemonsManager.Listar();
                    Console.WriteLine("Pressione Enter para voltar ao menu.");
                    Console.ReadLine();
                    break;
                case "3":
                    Console.WriteLine("Saindo...");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Pressione Enter para tentar novamente.");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
