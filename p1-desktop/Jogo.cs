using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1_desktop
{
    internal class Jogo
    {
        public Jogador Jogador1 { get; set; }
        public Jogador Jogador2 { get; set; }
        public int Turno { get; set; } = 1;

        public Jogo(Jogador jogador1, Jogador jogador2) {
            this.Jogador1 = jogador1;
            this.Jogador2 = jogador2;
            this.Turno = 1;
            SortearInicio();
        }

        public void DanoJogador(Jogador jogador, int dano)
        {
            if (Jogador1.Equals(jogador))
            {
                // oponente
                Jogador2.ReceberDano(dano);
            }else
            {
                Jogador1.ReceberDano(dano);
            }
        }

        internal void EncerrarTurno(Jogador jogador)
        {
            
            Jogador1.MeuTurno = !Jogador1.MeuTurno;
            Jogador2.MeuTurno = !Jogador2.MeuTurno;
            
            jogador.AumentarEnergia();
            jogador.DistribuirCartas();
            Turno++;
        }

        internal bool TemVencedor()
        {
            return Jogador1.Vida <= 0 || Jogador2.Vida <= 0;
        }

        public Jogador GetVencedor()
        {
            if (Jogador1.Vida > 0 && Jogador2.Vida <= 0)
            {
                return Jogador1;
            }
            else
            {
                return Jogador2;
            }
        }

        private void SortearInicio()
        {
            var random = new Random();

            if (random.NextDouble() > 0.5)
            {
                Jogador2.MeuTurno = true;
            } else
            {
                Jogador1.MeuTurno = true;
            }
        }
    }
}
