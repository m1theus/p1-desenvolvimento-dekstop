# Jogo de RPG de Cartas por Turnos
#### Um jogo de RPG de cartas onde dois jogadores competem usando estratégias de cartas em turnos. Cada jogador começa com um deck de cartas e pontos de vida, e deve derrotar o oponente reduzindo seus pontos de vida a zero.

#### Desenvolvido para a disciplina de Desenvolvimento para Desktop I do curso de Sistemas de Informação da Universidade de Araraquara (UNIARA)
---
Faça o download [Download](https://github.com/m1theus/p1-desenvolvimento-desktop/releases/tag/v1.0.0)

![image](https://github.com/user-attachments/assets/74cdeebb-15ed-4da5-9ec3-d8da63e3a5b0)
---
## Tecnologias Utilizadas

- **Linguagem:** C#
- **Framework:** .NET Framework 4.7.2
- **Interface:** Windows Forms
- **Sistema Operacional:** Windows

## Funcionalidades

- **Modo de Jogo Multijogador:** Dois jogadores competem em turnos alternados.
- **Sistema de Deck e Cartas:** Cada jogador possui um deck de 5 cartas por rodada, que são substituídas ao final de cada turno.
- **Energia Progressiva:** Cada jogador começa com 1 ponto de energia, que aumenta a cada turno até o máximo de 9, permitindo o uso de cartas mais poderosas ao longo do jogo.
- **Pontos de Vida:** Ambos os jogadores iniciam com 100 pontos de vida; o primeiro a chegar a 0 perde o jogo.
- **Sistema de Descarte Automático:** Cartas não usadas no turno são descartadas e novas cartas são puxadas para a próxima rodada.
- **Interface Gráfica Intuitiva:** A interface permite a seleção e uso de cartas, além de acompanhar o progresso dos jogadores.

## Regras do Jogo

1. **Objetivo:** Reduzir os pontos de vida do oponente a zero.
2. **Turnos Alternados:** Os jogadores jogam alternadamente, usando cartas do deck para atacar ou defender.
3. **Cartas e Energia:** Cada carta possui um custo de energia. Os jogadores ganham 1 ponto de energia adicional por turno (até o máximo de 9).
4. **Descarte e Substituição:** Ao final de cada turno, cartas não usadas são descartadas e substituídas por novas cartas no próximo turno.

## Estrutura do Projeto

- **FormPrincipal.cs:** Interface principal onde ocorre a batalha, com botões para selecionar e usar cartas.
- **Classe Carta:** Define as propriedades de cada carta, como custo de energia, dano e efeitos especiais.
- **Classe Jogador:** Gerencia o deck, pontos de vida, energia e ações do jogador.
- **Classe Jogo:** Controla a lógica principal dos turnos e a aplicação de regras.

## Pré-requisitos

- **.NET Framework 4.7.2**
- **Visual Studio** ou qualquer IDE que suporte projetos Windows Forms.

## Como Executar

1. Clone o repositório:
 ```bash
 git clone https://github.com/m1theus/p1-desenvolvimento-desktop.git
```

Download da release v1.0.0
 - [Download](https://github.com/m1theus/p1-desenvolvimento-desktop/releases/tag/v1.0.0)

