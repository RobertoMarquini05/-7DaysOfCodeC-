using System;
using System.Threading.Tasks;
using _7DaysOfCode.BLL;
using _7DaysOfCode.Utils;
using _7DaysOfCode.Models;
using System.Collections.Generic;
using _7DaysOfCode.Services;

class Program
{
    static async Task Main(string[] args)
    {
        var pokemonsManager = new PokemonsManager();
        var mascotesAdotados = new List<Pokemon>();
        string nomeJogador = ObterNomeJogador();

        // Loop principal do jogo
        while (true)
        {
            ExibirMenuPrincipal(nomeJogador);
            string opcaoSelecionada = Console.ReadLine();

            switch (opcaoSelecionada)
            {
                case "1":
                    await ProcessarAdoção(pokemonsManager, mascotesAdotados);
                    break;
                case "2":
                    ExibirMascotesAdotados(mascotesAdotados);
                    break;
                case "3":
                    EncerrarJogo();
                    return;
                default:
                    ExibirMensagemErro("Opção inválida. Pressione Enter para tentar novamente.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    /// <summary>
    /// Solicita e retorna o nome do jogador.
    /// </summary>
    private static string ObterNomeJogador()
    {
        Console.Clear();
        Console.WriteLine("Bem-vindo ao universo Pokémon!");
        Console.WriteLine("Por favor, digite seu nome para começarmos:");
        return Console.ReadLine();
    }

    /// <summary>
    /// Exibe o menu principal com as opções disponíveis.
    /// </summary>
    private static void ExibirMenuPrincipal(string nomeJogador)
    {
        Console.Clear();
        Console.WriteLine($"Olá, {nomeJogador}! Bem-vindo ao universo Pokémon!");
        Console.WriteLine("=================================================");
        Console.WriteLine("O que você deseja?");
        Console.WriteLine("1 - Adoção de mascotes");
        Console.WriteLine("2 - Ver mascotes adotados");
        Console.WriteLine("3 - Sair do Jogo");
        Console.WriteLine("=================================================");
    }

    /// <summary>
    /// Processa a adoção de um novo mascote, permitindo ao jogador escolher e adotar um Pokémon.
    /// </summary>
    private static async Task ProcessarAdoção(PokemonsManager pokemonManager, List<Pokemon> mascotesAdotados)
    {
        while (true)
        {
            pokemonManager.Listar();
            Console.WriteLine("Digite o número do Pokémon para ver suas características ou adotar (ou '0' para voltar):");
            string entrada = Console.ReadLine();

            if (entrada == "0")
            {
                break; // Retorna ao menu principal
            }

            if (!int.TryParse(entrada, out int indice) || indice < 1 || indice > 5)
            {
                ExibirMensagemErro("Número inválido! Escolha um número entre 1 e 5 ou 0 para voltar.");
                Console.ReadLine();
                continue;
            }

            // Obtém os detalhes do Pokémon selecionado
            PokemonEntry pokemonSelecionado = pokemonManager.GetPokemonEntry(indice - 1);
            var servicoPokemon = new PokemonService();
            Pokemon detalhesPokemon = await servicoPokemon.GetPokemonAsync(pokemonSelecionado.Url);

            // Exibe as características do Pokémon
            ExibirCaracteristicasPokemon(detalhesPokemon);

            // Pergunta se o jogador deseja adotar
            Console.WriteLine("Deseja adotar este Pokémon? (s/n)");
            string opcaoAdocao = Console.ReadLine().ToLower();
            if (opcaoAdocao == "s")
            {
                mascotesAdotados.Add(detalhesPokemon);
                Console.WriteLine($"{detalhesPokemon.Name} foi adotado com sucesso!");
            }
            else
            {
                Console.WriteLine("Adoção cancelada.");
            }

            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Exibe as características de um Pokémon na tela.
    /// </summary>
    private static void ExibirCaracteristicasPokemon(Pokemon pokemon)
    {
        Console.WriteLine($"\nPokémon: {pokemon.Name}");
        var tipos = string.Join(", ", pokemon.Types?.ConvertAll(t => t.Type.Name) ?? new List<string>());
        Console.WriteLine($"Tipos: {tipos}");
        Console.WriteLine("Estatísticas Base:");
        foreach (var estatistica in pokemon.Stats)
        {
            Console.WriteLine($"  {estatistica.Stat.Name}: {estatistica.Base_Stat}");
        }
        var habilidades = string.Join(", ", pokemon.Abilities?.ConvertAll(a =>
        {
            string tipo = a.Is_Hidden ? "(oculta)" : "(normal)";
            return $"{a.Ability.Name} {tipo}";
        }) ?? new List<string>());
        Console.WriteLine($"Habilidades: {habilidades}");
        Console.WriteLine($"Altura: {pokemon.Height / 10.0:F1} metros");
        Console.WriteLine($"Peso: {pokemon.Weight / 10.0:F1} kg");
        Console.WriteLine("=================================================");
    }

    /// <summary>
    /// Exibe a lista de mascotes adotados pelo jogador.
    /// </summary>
    private static void ExibirMascotesAdotados(List<Pokemon> mascotesAdotados)
    {
        Console.Clear();
        if (mascotesAdotados.Count == 0)
        {
            Console.WriteLine("Você ainda não adotou nenhum mascote!");
        }
        else
        {
            Console.WriteLine("Seus mascotes adotados:");
            Console.WriteLine("=================================================");
            for (int i = 0; i < mascotesAdotados.Count; i++)
            {
                var mascote = mascotesAdotados[i];
                Console.WriteLine($"{i + 1} - Pokémon: {mascote.Name}");
                var tipos = string.Join(", ", mascote.Types?.ConvertAll(t => t.Type.Name) ?? new List<string>());
                Console.WriteLine($"  Tipos: {tipos}");
                var habilidades = string.Join(", ", mascote.Abilities?.ConvertAll(a =>
                {
                    string tipo = a.Is_Hidden ? "(oculta)" : "(normal)";
                    return $"{a.Ability.Name} {tipo}";
                }) ?? new List<string>());
                Console.WriteLine($"  Habilidades: {habilidades}");
                Console.WriteLine("---------------------------------------------");
            }
        }
        Console.WriteLine("Pressione Enter para voltar ao menu.");
        Console.ReadLine();
    }

    /// <summary>
    /// Exibe uma mensagem de erro na tela.
    /// </summary>
    private static void ExibirMensagemErro(string mensagem)
    {
        Console.WriteLine(mensagem);
    }

    /// <summary>
    /// Encerra o jogo com uma mensagem de despedida.
    /// </summary>
    private static void EncerrarJogo()
    {
        Console.WriteLine("Saindo do Jogo...");
    }

}

