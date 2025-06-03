# 7DaysOfCode - Desafio Pokémon Tamagotchi

## Sobre o Projeto

Este projeto faz parte do primeiro desafio do [7DaysOfCode](https://www.alura.com.br/7days), onde a missão é criar uma aplicação back-end em C# que consome a API pública do Pokémon.

Inspirado no conceito do Tamagotchi, o “bichinho virtual”, o programa permite que o usuário escolha um Pokémon como mascote virtual, podendo consultar suas características para facilitar a escolha. Recentemente, o projeto foi expandido para incluir a funcionalidade de adoção de mascotes, permitindo que o jogador adote e gerencie uma lista de Pokémons como mascotes virtuais.

---

## Funcionalidades

- Consulta da lista de Pokémons via API pública (https://pokeapi.co/)
- Exibição de informações detalhadas do Pokémon escolhido: tipos, habilidades e estatísticas base
- Interface de linha de comando simples e interativa
- Parseamento do JSON retornado pela API
- Exibição das habilidades, altura e peso do Pokémon selecionado
- Adoção de Pokémons como mascotes virtuais
- Visualização e gerenciamento dos mascotes adotados
---

## Tecnologias Utilizadas

- C# (.NET 6 ou superior)
- `HttpClient` para requisições HTTP
- `System.Text.Json` para desserialização de JSON
- API Pokémon (https://pokeapi.co/)
- Git para controle de versão
- Arquivo `.gitignore` configurado para ignorar pastas como `bin/`, `obj/` e `.vs/`

---

## Como Executar

1. Clone o repositório:

   ```bash
   git clone https://github.com/RobertoMarquini05/7DaysOfCode-PokemonTamagotchi.git
   ```

2. Navegue até o diretório do projeto:

   ```bash
   cd 7DaysOfCode-PokemonTamagotchi
   ```

3. Restaure as dependências e execute o projeto:

   ```bash
   dotnet restore
   dotnet run
   ```

4. Siga as instruções exibidas no console para interagir com o programa.

---

## Autor

**Roberto Marquini**  
Desenvolvedor Pleno C#  
Atualizado em 30/05/2025

---

## Histórico de Atualizações

- **30/05/2025**:  
  - Adicionado suporte à adoção de mascotes  
  - Implementada visualização dos mascotes adotados  
  - Adicionado arquivo `.gitignore` para ignorar arquivos gerados pelo build e IDEs

- **02/06/2025**:
- Descrição das novas funcionalidades: interações com mascotes (alimentar, brincar, dormir) e uso do AutoMapper para mapeamento de Pokemon para Mascote.
- Instrução para instalar o pacote AutoMapper via NuGet (dotnet add package AutoMapper --version 14.0.0).
- Atualização do histórico de atualizações com as mudanças de 02/06/2025 (classe Mascote, interações, AutoMapper, menus).
- Menção à documentação do AutoMapper e à necessidade de conexão com a internet para a PokeAPI.
- Estrutura reorganizada para maior clareza, mantendo a data 02/06/2025.
