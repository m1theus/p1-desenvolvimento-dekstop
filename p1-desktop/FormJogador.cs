using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace p1_desktop
{
    internal partial class FormJogador : Form
    {
        private Jogo Jogo {  get; set; }
        private Principal Principal {  get; set; }
        private Jogador Jogador {  get; set; }
        private Image ImagemPersonagem {  get; set; }
        private Panel PainelVida {  get; set; }
        private Panel PainelEnergia {  get; set; }
        private IList<Button> CartasAtual {  get; set; }
        private Button EncerrarButton {  get; set; }

        public FormJogador(Principal formPrincipal, Jogo jogo, Jogador jogador, Image imagemPersonagem)
        {
            this.Principal = formPrincipal;
            this.Jogo = jogo;
            this.Jogador = jogador;
            this.ImagemPersonagem = imagemPersonagem;
            this.CartasAtual = new List<Button>();
            InicializarForm();
        }

        private void InicializarForm()
        {
            this.Text = Jogador.Nome;
            this.Size = new Size(750, 750);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(200, 200);

            var imgPersonagem = new PictureBox
            {
                Size = new Size(200, 200),
                Image = ResizeImage(ImagemPersonagem, 200, 150),
                Location = new Point(0, 100),
            };

            var labelNomeJogador = new Label
            {
                Text = Jogador.Nome,
                Location = new Point(20, 10),
                Font = new Font(Font.Name, 14),
            };

            PainelVida = new Panel
            {
                Location = new Point(20, 40)
            };
            PainelVida.Paint += (object sender, PaintEventArgs e) => progressPanelPaint1(PainelVida, Jogador.Vida, $"Vida {Jogador.Vida}/100", sender, e);


            PainelEnergia = new Panel
            {
                Location = new Point(PainelVida.Location.X + PainelVida.Width + 10, PainelVida.Location.Y),
            };
            PainelEnergia.Paint += (object sender, PaintEventArgs e) => progressPanelPaint1(PainelEnergia, Jogador.Energia, $"Energia {Jogador.Energia}/10", sender, e);

            EncerrarButton = new Button
            {
                Text = "Encerrar Turno",
                Size = new Size(100, 50),
                Location = new Point(PainelEnergia.Location.X + PainelEnergia.Width + 190, 200),
                Enabled = Jogador.EhMeuTurno(),
            };
            EncerrarButton.Click += EncerrarTurno;

            this.Controls.Add(imgPersonagem);
            this.Controls.Add(labelNomeJogador);
            this.Controls.Add(PainelVida);
            this.Controls.Add(PainelEnergia);
            this.Controls.Add(EncerrarButton);
        }

        private Image ResizeImage(Image originalImage, int newWidth, int newHeight)
        {
            Bitmap resizedImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
            }
            originalImage.Dispose();
            return resizedImage;
        }

        private void progressPanelPaint1(Panel self, int progressValue, string progressText, object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Dimensões do painel
            int panelWidth = 200;
            int panelHeight = 30;
            Brush color = progressText.Contains("Vida") ? Brushes.Green : Brushes.Blue;
            int progressTotal = progressText.Contains("Vida") ? 100 : 10;

            // Calcula a largura da barra de progresso com base no progresso atual
            int progressWidth = (int)((progressValue / (float)progressTotal) * panelWidth);

            // Desenha o fundo da barra de progresso
            g.FillRectangle(Brushes.Transparent, 0, 0, panelWidth, panelHeight);

            // Desenha a barra de progresso
            g.FillRectangle(color, 0, 0, progressWidth, panelHeight);

            // Desenha a borda ao redor da barra
            g.DrawRectangle(Pens.Black, 0, 0, panelWidth - 1, panelHeight - 1);

            // Desenha o texto do progresso
            SizeF textSize = g.MeasureString(progressText, this.Font);
            g.DrawString(progressText, this.Font, Brushes.Black,
                (panelWidth - textSize.Width) / 2, (panelHeight - textSize.Height) / 2);
        }

        internal void AddCarta(IList<Carta> cartas)
        {
            int imagemX = 220;
            int imagemY = 400;

            int posX = 20;
            foreach (Carta carta in cartas)
            {
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string cardPath = Path.Combine(appDirectory, "Resources", carta.Path);
                var buttonCarta = new Button
                {
                    BackColor = Color.Transparent,
                    Size = new Size(imagemX, imagemY),
                    Location = new Point(posX, 300),
                    BackgroundImage = ResizeImage(Image.FromFile(cardPath), imagemX, imagemY),
                    Enabled = (Jogador.Energia >= carta.Energia) && Jogador.EhMeuTurno(),
                };

                buttonCarta.Click += (s, e) => EfetuarAtaque(carta, buttonCarta, s, e);

                CartasAtual.Add(buttonCarta);
                Controls.Add(buttonCarta);

                posX += 240;
            }

        }

        public void AtualizarStatus()
        {
            PainelVida.Invalidate();
            PainelEnergia.Invalidate();
            PainelVida.Paint += (object sender, PaintEventArgs e) => progressPanelPaint1(PainelVida, Jogador.Vida, $"Vida {Jogador.Vida}/100", sender, e);
            PainelEnergia.Paint += (object sender, PaintEventArgs e) => progressPanelPaint1(PainelEnergia, Jogador.Energia, $"Energia {Jogador.Energia}/10", sender, e);
        }

        private void EfetuarAtaque(Carta carta, Button button, object sender, EventArgs e)
        {
            if (Jogador.Energia >= carta.Energia)
            {
                carta.Usar(Jogo, Jogador);
                button.Enabled = false;
            }
        }

        private void EncerrarTurno(object sender, EventArgs eventArgs)
        {
            Jogo.EncerrarTurno(Jogador);
            AtualizarCartas();
            Principal.NovoTurno();
            EncerrarButton.Enabled = false;
        }

        public void NovoTurno()
        {
            if (Jogador.EhMeuTurno())
            {
                EncerrarButton.Enabled = true;

                foreach (var btn in CartasAtual)
                {
                    btn.Enabled = true;
                }
            }
        }

        private void AtualizarCartas()
        {
            foreach (var carta in CartasAtual)
            {
               Controls.Remove(carta);
            }

            AddCarta(Jogador.Mao);
        }
    }
}
