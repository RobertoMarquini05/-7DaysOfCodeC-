using System;
using System.Collections.Generic;
using _7DaysOfCode.Models;

namespace _7DaysOfCode.View
{
    public class PokemonView
    {
        public void ExibirMenuPrincipal(string nomeJogador)
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

        public void ExibirCaracteristicasPokemon(Pokemon pokemon)
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

        public void ExibirMascotesAdotados(List<Pokemon> mascotesAdotados)
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

        public void ExibirMensagemErro(string mensagem)
        {
            Console.WriteLine(mensagem);
        }

        public void ExibirMensagem(string mensagem)
        {
            Console.WriteLine(mensagem);
        }

        public void EncerrarJogo()
        {
            Console.WriteLine("Saindo do Jogo...");
        }

        public string ObterNomeJogador()
        {
            Console.Clear();
            Console.WriteLine("Bem-vindo ao universo Pokémon!");
            Console.WriteLine("Por favor, digite seu nome para começarmos:");
            return Console.ReadLine();
        }
    }
}