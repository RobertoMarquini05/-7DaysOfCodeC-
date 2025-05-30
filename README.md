7DaysOfCode - Desafio Pokémon Tamagotchi

Sobre o Projeto

Este projeto é parte do primeiro desafio do [7DaysOfCode], onde a missão é criar uma aplicação back-end em C# que consome a API pública do Pokémon.

Inspirado no conceito do Tamagotchi, o “bichinho virtual”, o programa permite que o usuário escolha um Pokémon como mascote virtual, podendo consultar suas características para facilitar a escolha. Recentemente, o projeto foi expandido para incluir a funcionalidade de adoção de mascotes, permitindo que o jogador adote e gerencie uma lista de Pokémon como mascotes virtuais.



Funcionalidades





Consulta lista de Pokémons via API REST pública (https://pokeapi.co/)



Exibe informações detalhadas do Pokémon escolhido: tipos, habilidades e estatísticas base



Interface de linha de comando simples e interativa



Parseamento do JSON retornado pela API



Exibição das habilidades, altura e peso do Pokémon escolhido



Adoção de Pokémon como mascotes virtuais, com opção de visualizar os mascotes adotados



Gerenciamento de uma lista de mascotes adotados pelo usuário



Tecnologias utilizadas





C# (.NET 6 ou superior)



HttpClient para requisições HTTP



System.Text.Json para desserialização de JSON



API Pokémon (https://pokeapi.co/)



Git para controle de versão com arquivo .gitignore configurado para ignorar arquivos gerados (ex.: bin/, obj/, .vs/)



Como executar





Clone o repositório

git clone https://github.com/RobertoMarquini05/7DaysOfCode-PokemonTamagotchi.git





Navegue até o diretório do projeto

cd 7DaysOfCode-PokemonTamagotchi





Restaure as dependências e execute o projeto

dotnet restore
dotnet run





Siga as instruções no console para interagir com o programa.



Autor

Roberto Marquini - Desenvolvedor Pleno C# (atualizado em 30/05/2025)

Histórico de Atualizações



30/05/2025: Adicionados suporte à adoção de mascotes, exibição de mascotes adotados, e configuração do arquivo .gitignore para ignorar arquivos gerados pelo build e IDEs.