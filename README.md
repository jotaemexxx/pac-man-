# 🕹️ Pac-Man

Recriação do clássico jogo Pac-Man desenvolvido com C# (ASP.NET) e HTML/CSS/JavaScript.
O projeto utiliza **grafos** como estrutura de dados central para representar o labirinto
e calcular a movimentação dos personagens.

## 🗺️ Estrutura de Grafos no Projeto

O grafo é a estrutura principal do jogo. Cada posição do labirinto é um nó,
e as conexões entre posições adjacentes são as arestas.

### Localização do Grafo no código:

- **`WebApplication1/Models/Graph.cs`** → Classe principal do Grafo
- **`WebApplication1/Models/Node.cs`** → Representa cada posição do labirinto
- **`WebApplication1/Services/BfsService.cs`** → Busca em Largura (BFS) para pathfinding
- **`WebApplication1/Services/DfsService.cs`** → Busca em Profundidade (DFS)
- **`WebApplication1/Services/MazeService.cs`** → Geração e gerenciamento do labirinto usando o grafo
- **`WebApplication1/Services/GraphTestService.cs`** → Testes e validações do grafo
- **`WebApplication1/Models/Player.cs`** → Utilizam o grafo para movimentação do Pac-Man
- **`WebApplication1/Models/Ghost.cs`** → Utilizam o grafo para movimentação dos fantasmas

## 🎮 Como jogar

- Use as **setas do teclado** para mover o Pac-Man
- Coma todos os pontos do labirinto para vencer a fase
- Fuja dos fantasmas — se um deles te pegar, você perde uma vida

## 🚀 Como rodar o projeto

### ▶️ Jogar online
Acesse diretamente pelo navegador, sem precisar instalar nada:
**https://pac-man-production-0f1e.up.railway.app/**

### 💻 Rodar localmente
### Pré-requisitos
- [Visual Studio](https://visualstudio.microsoft.com/) instalado
- .NET instalado (já vem com o Visual Studio)

### Passos
1. Clone o repositório:
```bash
   git clone https://github.com/SEU_USUARIO/pac-man-
```
2. Abra o arquivo `PacMan.slnx` no Visual Studio
3. Pressione **F5** para rodar
4. O jogo abrirá no navegador automaticamente

---

## 🛠️ Tecnologias utilizadas

- C# / ASP.NET
- HTML5, CSS3, JavaScript
- Algoritmos de grafos: BFS e DFS

---

## 📌 Status do projeto

🚧 Em desenvolvimento
