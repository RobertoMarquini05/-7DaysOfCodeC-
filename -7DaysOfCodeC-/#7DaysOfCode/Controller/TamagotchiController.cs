using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _7DaysOfCode.Models;
using _7DaysOfCode.View;
using AutoMapper;

namespace _7DaysOfCode.Controller
{
    public class TamagotchiController
    {
        private readonly PokemonsManager _pokemonsManager;
        private readonly List<Mascote> _mascotesAdotados;
        private readonly PokemonView _pokemonView;
        private readonly string _nomeJogador;
        private readonly IMapper _mapper;

        public TamagotchiController(string nomeJogador)
        {
            _pokemonsManager = new PokemonsManager();
            _mascotesAdotados = new List<Mascote>();
            _pokemonView = new PokemonView();
            _nomeJogador = nomeJogador;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Pokemon, Mascote>();
            });
            _mapper = config.CreateMapper();
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
                        await ProcessarAdoção();
                        break;
                    case "2":
                        _pokemonView.ExibirMascotesAdotados(_mascotesAdotados);
                        break;
                    case "3":
                        await InteragirComMascote();
                        break;
                    case "4":
                        _pokemonView.EncerrarJogo();
                        return;
                    default:
                        _pokemonView.ExibirMensagemErro("Opção inválida. Pressione Enter para tentar novamente.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private async Task ProcessarAdoção()
        {
            while (true)
            {
                _pokemonsManager.Listar();
                _pokemonView.ExibirMensagem("Digite o número do Pokémon para ver suas características ou adotar (ou '0' para voltar):");
                string entrada = Console.ReadLine();

                if (entrada == "0")
                {
                    break;
                }

                if (!int.TryParse(entrada, out int indice) || indice < 1 || indice > 5)
                {
                    _pokemonView.ExibirMensagemErro("Número inválido! Escolha um número entre 1 e 5 ou 0 para voltar.");
                    Console.ReadLine();
                    continue;
                }

                PokemonEntry pokemonSelecionado = _pokemonsManager.GetPokemonEntry(indice - 1);
                if (pokemonSelecionado == null)
                {
                    _pokemonView.ExibirMensagemErro("Pokémon não encontrado. Tente novamente.");
                    Console.ReadLine();
                    continue;
                }

                var servicoPokemon = new PokemonService();
                Pokemon detalhesPokemon = await servicoPokemon.GetPokemonAsync(pokemonSelecionado.Url);

                _pokemonView.ExibirCaracteristicasPokemon(detalhesPokemon);

                bool desejaAdotar = ClientEntry.GetCharacteristicsOrNot();
                if (desejaAdotar)
                {
                    Mascote mascote = _mapper.Map<Mascote>(detalhesPokemon);
                    _mascotesAdotados.Add(mascote);
                    _pokemonView.ExibirMensagem($"{mascote.Name} foi adotado com sucesso!");
                }
                else
                {
                    _pokemonView.ExibirMensagem("Adoção cancelada.");
                }

                _pokemonView.ExibirMensagem("Pressione Enter para continuar...");
                Console.ReadLine();
            }
        }

        private async Task InteragirComMascote()
        {
            while (true)
            {
                if (_mascotesAdotados.Count == 0)
                {
                    _pokemonView.ExibirMensagemErro("Você ainda não adotou nenhum mascote!");
                    Console.ReadLine();
                    break;
                }

                _pokemonView.ExibirMascotesAdotados(_mascotesAdotados);
                _pokemonView.ExibirMensagem("Digite o número do mascote para interagir (ou '0' para voltar):");
                string entrada = Console.ReadLine();

                if (entrada == "0")
                {
                    break;
                }

                if (!int.TryParse(entrada, out int indice) || indice < 1 || indice > _mascotesAdotados.Count)
                {
                    _pokemonView.ExibirMensagemErro("Número inválido! Escolha um número válido ou 0 para voltar.");
                    Console.ReadLine();
                    continue;
                }

                Mascote mascoteSelecionado = _mascotesAdotados[indice - 1];
                bool continuar = true;
                while (continuar)
                {
                    _pokemonView.ExibirMenuInteracao(mascoteSelecionado);
                    string opcaoInteracao = Console.ReadLine();

                    switch (opcaoInteracao)
                    {
                        case "1":
                            mascoteSelecionado.Alimentar();
                            _pokemonView.ExibirMensagem($"{mascoteSelecionado.Name} foi alimentado!");
                            break;
                        case "2":
                            mascoteSelecionado.Brincar();
                            _pokemonView.ExibirMensagem($"{mascoteSelecionado.Name} brincou e está mais feliz!");
                            break;
                        case "3":
                            mascoteSelecionado.Dormir();
                            _pokemonView.ExibirMensagem($"{mascoteSelecionado.Name} dormiu e está mais descansado!");
                            break;
                        case "4":
                            continuar = false;
                            break;
                        default:
                            _pokemonView.ExibirMensagemErro("Opção inválida. Pressione Enter para tentar novamente.");
                            Console.ReadLine();
                            break;
                    }

                    if (continuar)
                    {
                        _pokemonView.ExibirCaracteristicasMascote(mascoteSelecionado);
                        _pokemonView.ExibirMensagem("Pressione Enter para continuar...");
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}