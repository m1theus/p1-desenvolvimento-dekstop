using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace p1_desktop
{
    internal class Jogador
    {
        public string Nome { get; set; }
        public int Vida { get; set; } = 100;
        public int Energia { get; set; } = 1;
        public IList<Carta> Deck { get; set; } = new List<Carta>();
        public IList<Carta> Mao { get; set; } = new List<Carta>();
        public bool MeuTurno { get; set; } = false;

        public Jogador(string nome)
        { 
            this.Nome = nome;
            
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string cardsPath = Path.Combine(appDirectory, "Resources", "cards.json");
            string jsonContent = File.ReadAllText(cardsPath);
            Deck = JsonConvert.DeserializeObject<IList<Carta>>(jsonContent);
            DistribuirCartas();
        }

        public void AumentarEnergia()
        {
            if (Energia <= 10) Energia++;
        }

        public void DistribuirCartas()
        {
            Mao.Clear();
            Random random = new Random();
            for (int i = 0; i < 3; i++)
            {
                Mao.Add(Deck.ElementAt(random.Next(0, Deck.Count)));
            }
        }

        public void RemoverCarta(Carta carta)
        {
            Mao.Remove(carta);
        }

        internal void ReceberDano(int dano)
        {
            Vida -= dano;
        }

        internal bool EhMeuTurno()
        {
            return MeuTurno;
        }
    }
}
