using System;
using System.Threading.Tasks;
using _7DaysOfCode.Controller;
using _7DaysOfCode.View;
class Program
{
    static async Task Main(string[] args)
    {
        var pokemonView = new PokemonView();
        string nomeJogador = pokemonView.ObterNomeJogador();
        var controller = new TamagotchiController(nomeJogador);
        await controller.Jogar();
    }
}