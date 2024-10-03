using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1_desktop
{
    internal class Carta
    {
        public string Nome { get; set; }
        public int Dano { get; set; }
        public int Energia { get; set; }
        public string Path { get; set; }
        public Action<Jogo, Jogador, Jogador> Efeito { get; set; }

        public Carta(string nome, int dano, int energia, string path)
        {
            Nome = nome;
            Dano = dano;
            Energia = energia;
            Path = path;
        }

        public void Usar(Jogo jogo, Jogador jogador)
        {
            if (jogador.Energia >= Energia)
            {
                jogador.Energia -= Energia;
                jogo.DanoJogador(jogador, Dano);
                jogador.RemoverCarta(this);
            }
        }
    }
}
