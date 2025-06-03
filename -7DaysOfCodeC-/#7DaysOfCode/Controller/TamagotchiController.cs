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
            try
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
            catch (Exception ex)
            {
                _pokemonView.ExibirMensagemErro($"Erro ao inicializar o jogo: {ex.Message}");
                throw;
            }
        }

        public async Task Jogar()
        {
            while (true)
            {
                try
                {
                    _pokemonView.ExibirMenuPrincipal(_nomeJogador);
                    string opcaoSelecionada;
                    bool entradaValida;
                    do
                    {
                        opcaoSelecionada = Console.ReadLine()?.Trim();
                        entradaValida = opcaoSelecionada == "1" || opcaoSelecionada == "2" ||
                                        opcaoSelecionada == "3" || opcaoSelecionada == "4";
                        if (!entradaValida)
                        {
                            _pokemonView.ExibirMensagemErro("Op��o inv�lida! Escolha 1, 2, 3 ou 4.");
                        }
                    } while (!entradaValida);

                    switch (opcaoSelecionada)
                    {
                        case "1":
                            await ProcessarAdo��o();
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
                    }
                }
                catch (Exception ex)
                {
                    _pokemonView.ExibirMensagemErro($"Ocorreu um erro inesperado: {ex.Message}. Pressione Enter para continuar.");
                    Console.ReadLine();
                }
            }
        }

        private async Task ProcessarAdo��o()
        {
            while (true)
            {
                try
                {
                    var pokemonsDisponiveis = _pokemonsManager.Listar();
                    if (pokemonsDisponiveis == null || pokemonsDisponiveis.Count == 0)
                    {
                        _pokemonView.ExibirMensagemErro("API indispon�vel no momento. Tente novamente mais tarde.");
                        Console.ReadLine();
                        break;
                    }

                    _pokemonView.ExibirMensagem("Digite o n�mero do Pok�mon para ver suas caracter�sticas ou adotar (ou '0' para voltar):");
                    string entrada;
                    int indice;
                    bool entradaValida;
                    do
                    {
                        entrada = Console.ReadLine()?.Trim();
                        entradaValida = int.TryParse(entrada, out indice) && indice >= 0 && indice <= 5;
                        if (!entradaValida)
                        {
                            _pokemonView.ExibirMensagemErro("N�mero inv�lido! Escolha um n�mero entre 0 e 5.");
                        }
                    } while (!entradaValida);

                    if (indice == 0)
                    {
                        break;
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

                    if (detalhesPokemon == null)
                    {
                        _pokemonView.ExibirMensagemErro("N�o foi poss�vel obter os detalhes do Pok�mon. Tente novamente.");
                        Console.ReadLine();
                        continue;
                    }

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
                        _pokemonView.ExibirMensagem("Ado��o cancelada.");
                    }

                    _pokemonView.ExibirMensagem("Pressione Enter para continuar...");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    _pokemonView.ExibirMensagemErro($"Erro ao processar ado��o: {ex.Message}. Pressione Enter para continuar.");
                    Console.ReadLine();
                }
            }
        }

        private async Task InteragirComMascote()
        {
            while (true)
            {
                try
                {
                    if (_mascotesAdotados.Count == 0)
                    {
                        _pokemonView.ExibirMensagemErro("Voc� ainda n�o adotou nenhum mascote!");
                        Console.ReadLine();
                        break;
                    }

                    _pokemonView.ExibirMascotesAdotados(_mascotesAdotados);
                    _pokemonView.ExibirMensagem("Digite o n�mero do mascote para interagir (ou '0' para voltar):");
                    string entrada;
                    int indice;
                    bool entradaValida;
                    do
                    {
                        entrada = Console.ReadLine()?.Trim();
                        entradaValida = int.TryParse(entrada, out indice) && indice >= 0 && indice <= _mascotesAdotados.Count;
                        if (!entradaValida)
                        {
                            _pokemonView.ExibirMensagemErro($"N�mero inv�lido! Escolha um n�mero entre 0 e {_mascotesAdotados.Count}.");
                        }
                    } while (!entradaValida);

                    if (indice == 0)
                    {
                        break;
                    }

                    Mascote mascoteSelecionado = _mascotesAdotados[indice - 1];
                    bool continuar = true;
                    while (continuar)
                    {
                        _pokemonView.ExibirMenuInteracao(mascoteSelecionado);
                        string opcaoInteracao;
                        do
                        {
                            opcaoInteracao = Console.ReadLine()?.Trim();
                            entradaValida = opcaoInteracao == "1" || opcaoInteracao == "2" ||
                                            opcaoInteracao == "3" || opcaoInteracao == "4";
                            if (!entradaValida)
                            {
                                _pokemonView.ExibirMensagemErro("Op��o inv�lida! Escolha 1, 2, 3 ou 4.");
                            }
                        } while (!entradaValida);

                        switch (opcaoInteracao)
                        {
                            case "1":
                                mascoteSelecionado.Alimentar();
                                _pokemonView.ExibirMensagem($"{mascoteSelecionado.Name} foi alimentado!");
                                break;
                            case "2":
                                mascoteSelecionado.Brincar();
                                _pokemonView.ExibirMensagem($"{mascoteSelecionado.Name} brincou e est� mais feliz!");
                                break;
                            case "3":
                                mascoteSelecionado.Dormir();
                                _pokemonView.ExibirMensagem($"{mascoteSelecionado.Name} dormiu e est� mais descansado!");
                                break;
                            case "4":
                                continuar = false;
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
                catch (Exception ex)
                {
                    _pokemonView.ExibirMensagemErro($"Erro ao interagir com mascote: {ex.Message}. Pressione Enter para continuar.");
                    Console.ReadLine();
                }
            }
        }
    }
}