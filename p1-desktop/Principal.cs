using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace p1_desktop
{
    public partial class Principal : Form
    {
        private Jogo Jogo { get; set; }
        private FormJogador FormJogador1 {  get; set; }
        private FormJogador FormJogador2 {  get; set; }

        public Principal()
        {
            InitializeComponent();
        }
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void novoJogoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Você deseja iniciar um novo jogo?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                NovoJogo(textBox1.Text, textBox2.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var jogador1 = textBox1.Text;
            var jogador2 = textBox2.Text;

            if ((jogador1 == null || jogador1 == "") ||  (jogador2 == null || jogador2 == ""))
            {
                MessageBox.Show("Nome de jogadores inválido!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NovoJogo(jogador1, jogador2);
        }

        private void NovoJogo(string nomeJogador1, string nomeJogador2) {
            LimparJogoAntigo();
            var jogador1 = new Jogador(nomeJogador1);
            var jogador2 = new Jogador(nomeJogador2);
            // Novo Jogo
            Jogo = new Jogo(jogador1, jogador2);

            Image char1 = Image.FromFile("C:\\Users\\Desktop\\source\\repos\\p1-desktop\\p1-desktop\\Resources\\char1.png");
            Image char2 = Image.FromFile("C:\\Users\\Desktop\\source\\repos\\p1-desktop\\p1-desktop\\Resources\\char2.png");

            FormJogador1 = new FormJogador(this, Jogo, jogador1, char2);
            FormJogador2 = new FormJogador(this, Jogo, jogador2, char1);
            FormJogador1.Location = new Point(FormJogador1.Location.X + FormJogador1.Width + 10, FormJogador1.Location.Y);

            FormJogador1.AddCarta(jogador1.Mao);
            FormJogador2.AddCarta(jogador2.Mao);

            FormJogador1.Show();
            FormJogador2.Show();

            AtualizarTela();
        }

        public void NovoTurno()
        {
            FormJogador1.NovoTurno();
            FormJogador2.NovoTurno();
        }

        private async Task AtualizarTela()
        {
            while (true)
            {
                FormJogador1.AtualizarStatus();
                FormJogador2.AtualizarStatus();

                if (Jogo.TemVencedor())
                {
                    var vencedor = Jogo.GetVencedor();
                    MessageBox.Show($"O jogador {vencedor.Nome}!", "Vencedor!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                await Task.Delay(1000);
            }
        }

        private void LimparJogoAntigo()
        {
            FormJogador1?.Close();
            FormJogador2?.Close();
        }

        private void Principal_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }
    }
}
