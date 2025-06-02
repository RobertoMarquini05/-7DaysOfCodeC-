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
            Console.WriteLine("3 - Interagir com mascotes");
            Console.WriteLine("4 - Sair do Jogo");
            Console.WriteLine("=================================================");
        }

        public void ExibirMenuInteracao(Mascote mascote)
        {
            Console.Clear();
            Console.WriteLine($"Interagindo com {mascote.Name}");
            Console.WriteLine("=================================================");
            Console.WriteLine("O que você deseja fazer?");
            Console.WriteLine("1 - Alimentar");
            Console.WriteLine("2 - Brincar");
            Console.WriteLine("3 - Dormir");
            Console.WriteLine("4 - Voltar");
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

        public void ExibirCaracteristicasMascote(Mascote mascote)
        {
            Console.WriteLine($"\nMascote: {mascote.Name}");
            var tipos = string.Join(", ", mascote.Types?.ConvertAll(t => t.Type.Name) ?? new List<string>());
            Console.WriteLine($"Tipos: {tipos}");
            var habilidades = string.Join(", ", mascote.Abilities?.ConvertAll(a =>
            {
                string tipo = a.Is_Hidden ? "(oculta)" : "(normal)";
                return $"{a.Ability.Name} {tipo}";
            }) ?? new List<string>());
            Console.WriteLine($"Habilidades: {habilidades}");
            Console.WriteLine($"Altura: {mascote.Height / 10.0:F1} metros");
            Console.WriteLine($"Peso: {mascote.Weight / 10.0:F1} kg");
            Console.WriteLine($"Alimentação: {mascote.Alimentacao}/10");
            Console.WriteLine($"Humor: {mascote.Humor}/10");
            Console.WriteLine($"Sono: {mascote.Sono}/10");
            Console.WriteLine("=================================================");
        }

        public void ExibirMascotesAdotados(List<Mascote> mascotesAdotados)
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
                    Console.WriteLine($"{i + 1} - Mascote: {mascote.Name}");
                    var tipos = string.Join(", ", mascote.Types?.ConvertAll(t => t.Type.Name) ?? new List<string>());
                    Console.WriteLine($"  Tipos: {tipos}");
                    var habilidades = string.Join(", ", mascote.Abilities?.ConvertAll(a =>
                    {
                        string tipo = a.Is_Hidden ? "(oculta)" : "(normal)";
                        return $"{a.Ability.Name} {tipo}";
                    }) ?? new List<string>());
                    Console.WriteLine($"  Habilidades: {habilidades}");
                    Console.WriteLine($"  Alimentação: {mascote.Alimentacao}/10");
                    Console.WriteLine($"  Humor: {mascote.Humor}/10");
                    Console.WriteLine($"  Sono: {mascote.Sono}/10");
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