using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _7DaysOfCode.Models;
using _7DaysOfCode.View;

namespace _7DaysOfCode.Controller
{
	public class TamagotchiController
	{
		private readonly PokemonsManager _pokemonsManager;
		private readonly List<Pokemon> _mascotesAdotados;
		private readonly PokemonView _pokemonView;
		private readonly string _nomeJogador;

		public TamagotchiController(string nomeJogador)
		{
			_pokemonsManager = new PokemonsManager();
			_mascotesAdotados = new List<Pokemon>();
			_pokemonView = new PokemonView();
			_nomeJogador = nomeJogador;
		}

		public async Task Jogar()
		{
			while (true)
			{
				_pokemonView.ExibirMenuPrincipal(_nomeJogador);
				string opcaoSelecionada = Console.ReadLine();

				switch (opcaoSelecionada)
				{
					case "1":
						await ProcessarAdo��o();
						break;
					case "2":
						_pokemonView.ExibirMascotesAdotados(_mascotesAdotados);
						break;
					case "3":
						_pokemonView.EncerrarJogo();
						return;
					default:
						_pokemonView.ExibirMensagemErro("Op��o inv�lida. Pressione Enter para tentar novamente.");
						Console.ReadLine();
						break;
				}
			}
		}

		private async Task ProcessarAdo��o()
		{
			while (true)
			{
				_pokemonsManager.Listar();
				_pokemonView.ExibirMensagem("Digite o n�mero do Pok�mon para ver suas caracter�sticas ou adotar (ou '0' para voltar):");
				string entrada = Console.ReadLine();

				if (entrada == "0")
				{
					break;
				}

				if (!int.TryParse(entrada, out int indice) || indice < 1 || indice > 5)
				{
					_pokemonView.ExibirMensagemErro("N�mero inv�lido! Escolha um n�mero entre 1 e 5 ou 0 para voltar.");
					Console.ReadLine();
					continue;
				}

				PokemonEntry pokemonSelecionado = _pokemonsManager.GetPokemonEntry(indice - 1);
				if (pokemonSelecionado == null)
				{
					_pokemonView.ExibirMensagemErro("Pok�mon n�o encontrado. Tente novamente.");
					Console.ReadLine();
					continue;
				}

				var servicoPokemon = new PokemonService();
				Pokemon detalhesPokemon = await servicoPokemon.GetPokemonAsync(pokemonSelecionado.Url);

				_pokemonView.ExibirCaracteristicasPokemon(detalhesPokemon);

				bool desejaAdotar = ClientEntry.GetCharacteristicsOrNot();
				if (desejaAdotar)
				{
					_mascotesAdotados.Add(detalhesPokemon);
					_pokemonView.ExibirMensagem($"{detalhesPokemon.Name} foi adotado com sucesso!");
				}
				else
				{
					_pokemonView.ExibirMensagem("Ado��o cancelada.");
				}

				_pokemonView.ExibirMensagem("Pressione Enter para continuar...");
				Console.ReadLine();
			}
		}
	}
}