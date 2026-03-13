using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JuegoPOO
{
    public partial class Form1 : Form
    {

        Personaje jugador;
        List<Personaje> enemigos = new List<Personaje>();
        List<PictureBox> enemyBoxes = new List<PictureBox>();
        Random rnd = new Random();
        int ronda = 1;
        bool bossPresent = false;
        int vidaMaxJugador = 0;


        public Form1()
        {
            InitializeComponent();

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // rellenar solo la primera vez si fuera necesario
            if (cmbPersonaje.Items.Count == 0)
            {
                cmbPersonaje.Items.Add("Mago");
                cmbPersonaje.Items.Add("Arquero");
                cmbPersonaje.Items.Add("Guerrero");
            }

            txtLog.Multiline = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbPersonaje.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un personaje");
                return;
            }

            string seleccionado = cmbPersonaje.SelectedItem.ToString();

            switch (seleccionado)
            {
                case "Mago":
                    jugador = new Mago();
                    pbJugador.Image = Image.FromFile("mago.png");
                    break;
                case "Arquero":
                    jugador = new Arquero();
                    pbJugador.Image = Image.FromFile("arquero.png");
                    break;
                case "Guerrero":
                    jugador = new Guerrero();
                    pbJugador.Image = Image.FromFile("guerrero.png");
                    break;
                default:
                    MessageBox.Show("Selecciona un personaje");
                    return;
            }

            pbJugador.Visible = true;

            // fijar vida máxima y barras del jugador correctamente al crearlo
            vidaMaxJugador = jugador.Vida;
            pbVidaJugador.Maximum = Math.Max(1, vidaMaxJugador);
            pbVidaJugador.Value = Math.Max(0, Math.Min(pbVidaJugador.Maximum, jugador.Vida));
            lblVidaJugador.Text = jugador.Vida.ToString();

            // iniciar la primera ronda
            ronda = 1;
            bossPresent = false;
            IniciarRonda();
        }

        private void IniciarRonda()
        {
            // limpiar enemigos previos
            enemigos.Clear();
            foreach (var pb in enemyBoxes)
            {
                pb.Dispose();
            }
            enemyBoxes.Clear();
            panelEnemigos.Controls.Clear();

            txtLog.AppendText($"--- Ronda {ronda} ---{Environment.NewLine}");

            if (ronda > 3)
            {
                // aparece el boss
                bossPresent = true;
                var boss = new Boss();
                enemigos.Add(boss);

                var pb = CrearPictureBoxEnemigo("boss.png");
                panelEnemigos.Controls.Add(pb);
                enemyBoxes.Add(pb);

                // actualizar barra de vida con el boss
                pbVidaEnemigo.Maximum = boss.Vida;
                pbVidaEnemigo.Value = boss.Vida;
                lblVidaEnemigo.Text = boss.Vida.ToString();

                txtLog.AppendText("¡Ha aparecido el Boss!" + Environment.NewLine);
                return;
            }

            // cantidad de enemigos = número de la ronda
            int cantidad = ronda;
            for (int i = 0; i < cantidad; i++)
            {
                int tipo = rnd.Next(1, 4);
                Personaje e;
                string imagen;
                if (tipo == 1)
                {
                    e = new Mago();
                    imagen = "mago.png";
                }
                else if (tipo == 2)
                {
                    e = new Arquero();
                    imagen = "arquero.png";
                }
                else
                {
                    e = new Guerrero();
                    imagen = "guerrero.png";
                }
                enemigos.Add(e);
                var pb = CrearPictureBoxEnemigo(imagen);
                panelEnemigos.Controls.Add(pb);
                enemyBoxes.Add(pb);
            }

            // mostrar vida del primer enemigo vivo
            var objetivo = enemigos.FirstOrDefault(x => x.Vida > 0);
            if (objetivo != null)
            {
                pbVidaEnemigo.Maximum = objetivo.Vida;
                pbVidaEnemigo.Value = objetivo.Vida;
                lblVidaEnemigo.Text = objetivo.Vida.ToString();
            }
        }

        private PictureBox CrearPictureBoxEnemigo(string rutaImagen)
        {
            var pb = new PictureBox();
            pb.Size = new Size(120, 120);
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Margin = new Padding(10);
            pb.BorderStyle = BorderStyle.FixedSingle;
            try
            {
                pb.Image = Image.FromFile(rutaImagen);
            }
            catch
            {
                // si falta la imagen, dejar vacío pero evitar excepción
                pb.BackColor = Color.DarkRed;
            }
            return pb;
        }

        private void Atacar_Click(object sender, EventArgs e)
        {
            if (jugador == null)
            {
                MessageBox.Show("Crea un personaje primero.");
                return;
            }

            // objetivo: primer enemigo vivo
            var objetivoIndex = enemigos.FindIndex(x => x.Vida > 0);
            if (objetivoIndex == -1)
            {
                txtLog.AppendText("No hay enemigos. Inicia la siguiente ronda." + Environment.NewLine);
                return;
            }

            var objetivo = enemigos[objetivoIndex];

            int daño = jugador.Atacar();
            objetivo.Vida -= daño;
            txtLog.AppendText($"Jugador hizo {daño} de daño al enemigo {objetivoIndex + 1}\n");

            // actualizar imagen/estado del enemigo objetivo
            if (objetivo.Vida <= 0)
            {
                objetivo.Vida = 0;
                // marcar PictureBox como derrotado (oscurecer)
                enemyBoxes[objetivoIndex].Enabled = false;
                enemyBoxes[objetivoIndex].Image = null;
                enemyBoxes[objetivoIndex].BackColor = Color.Gray;
                txtLog.AppendText($"Enemigo {objetivoIndex + 1} derrotado.\n");
            }

            // enemigo contraataca: elegir un enemigo vivo al azar (si existe)
            var vivos = enemigos
                .Select((e, idx) => new { e, idx })
                .Where(x => x.e.Vida > 0)
                .ToList();

            if (vivos.Count > 0)
            {
                var atacante = vivos[rnd.Next(vivos.Count)].e;
                int contra = rnd.Next(5, 15);
                // si es boss, darle más daño
                if (atacante is Boss)
                    contra = rnd.Next(15, 30);

                jugador.Vida -= contra;
                txtLog.AppendText($"Enemigo hizo {contra} de daño\n");
            }

            // actualizar barras
            pbVidaJugador.Maximum = Math.Max(1, vidaMaxJugador);
            pbVidaJugador.Value = Math.Max(0, Math.Min(pbVidaJugador.Maximum, jugador.Vida));
            lblVidaJugador.Text = jugador.Vida.ToString();

            var primerVivo = enemigos.FirstOrDefault(x => x.Vida > 0);
            if (primerVivo != null)
            {
                pbVidaEnemigo.Maximum = Math.Max(1, primerVivo.Vida); // evitar 0 máximo
                pbVidaEnemigo.Value = Math.Max(0, Math.Min(pbVidaEnemigo.Maximum, primerVivo.Vida));
                lblVidaEnemigo.Text = primerVivo.Vida.ToString();
            }
            else
            {
                // todos muertos: ganar la ronda
                txtLog.AppendText("¡Ronda completada!" + Environment.NewLine);
                ronda++;
                if (jugador.Vida > 0)
                {
                    MessageBox.Show($"Has completado la ronda. Avanzando a la ronda {ronda}");
                    IniciarRonda();
                }
                else
                {
                    MessageBox.Show("Te han derrotado. Fin del juego.");
                }
            }

            // comprobar vida jugador
            if (jugador.Vida <= 0)
            {
                jugador.Vida = 0;
                pbVidaJugador.Value = 0;
                lblVidaJugador.Text = "0";
                MessageBox.Show("Perdiste!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // ataque especial: doble ataque al objetivo actual
            if (jugador == null)
            {
                MessageBox.Show("Crea un personaje primero.");
                return;
            }

            var objetivoIndex = enemigos.FindIndex(x => x.Vida > 0);
            if (objetivoIndex == -1)
            {
                txtLog.AppendText("No hay enemigos para atacar." + Environment.NewLine);
                return;
            }

            int daño = jugador.Ataque * 2;
            var objetivo = enemigos[objetivoIndex];
            objetivo.Vida -= daño;
            txtLog.AppendText($"Ataque especial hizo {daño} de daño al enemigo {objetivoIndex + 1}\n");

            if (objetivo.Vida <= 0)
            {
                objetivo.Vida = 0;
                enemyBoxes[objetivoIndex].Enabled = false;
                enemyBoxes[objetivoIndex].Image = null;
                enemyBoxes[objetivoIndex].BackColor = Color.Gray;
                txtLog.AppendText($"Enemigo {objetivoIndex + 1} derrotado.\n");
            }

            // contraataque (como antes)
            var vivos = enemigos
                .Select((e, idx) => new { e, idx })
                .Where(x => x.e.Vida > 0)
                .ToList();

            if (vivos.Count > 0)
            {
                var atacante = vivos[rnd.Next(vivos.Count)].e;
                int contra = rnd.Next(5, 15);
                if (atacante is Boss)
                    contra = rnd.Next(15, 30);

                jugador.Vida -= contra;
                txtLog.AppendText($"Enemigo hizo {contra} de daño\n");
            }

            pbVidaJugador.Maximum = Math.Max(1, vidaMaxJugador);
            pbVidaJugador.Value = Math.Max(0, Math.Min(pbVidaJugador.Maximum, jugador.Vida));
            lblVidaJugador.Text = jugador.Vida.ToString();

            var primerVivo = enemigos.FirstOrDefault(x => x.Vida > 0);
            if (primerVivo != null)
            {
                pbVidaEnemigo.Maximum = Math.Max(1, primerVivo.Vida);
                pbVidaEnemigo.Value = Math.Max(0, Math.Min(pbVidaEnemigo.Maximum, primerVivo.Vida));
                lblVidaEnemigo.Text = primerVivo.Vida.ToString();
            }
            else
            {
                txtLog.AppendText("¡Ronda completada!" + Environment.NewLine);
                ronda++;
                if (jugador.Vida > 0)
                {
                    MessageBox.Show($"Has completado la ronda. Avanzando a la ronda {ronda}");
                    IniciarRonda();
                }
                else
                {
                    MessageBox.Show("Te han derrotado. Fin del juego.");
                }
            }

            if (jugador.Vida <= 0)
            {
                jugador.Vida = 0;
                pbVidaJugador.Value = 0;
                lblVidaJugador.Text = "0";
                MessageBox.Show("Perdiste!");
            }
        }

        private void btnCurar_Click(object sender, EventArgs e)
        {
            if (jugador == null)
            {
                MessageBox.Show("Crea un personaje primero.");
                return;
            }

            // curar una cantidad (aquí: 25% de la vida máxima, al menos 1)
            int cura = Math.Max(1, vidaMaxJugador / 4);
            jugador.Vida += cura;
            if (jugador.Vida > vidaMaxJugador) jugador.Vida = vidaMaxJugador;

            txtLog.AppendText($"Jugador se curó {cura} puntos. Vida actual: {jugador.Vida}{Environment.NewLine}");

            pbVidaJugador.Maximum = Math.Max(1, vidaMaxJugador);
            pbVidaJugador.Value = Math.Max(0, Math.Min(pbVidaJugador.Maximum, jugador.Vida));
            lblVidaJugador.Text = jugador.Vida.ToString();

            int contra = rnd.Next(5, 15);
            if (enemigos.Any(x => x.Vida > 0))
            {
                var atacante = enemigos.Where(x => x.Vida > 0).OrderBy(x => rnd.Next()).First();
                if (atacante is Boss)
                    contra = rnd.Next(15, 30);
                jugador.Vida -= contra;
                txtLog.AppendText($"Enemigo hizo {contra} de daño mientras te curabas\n");
                pbVidaJugador.Maximum = Math.Max(1, vidaMaxJugador);
                pbVidaJugador.Value = Math.Max(0, Math.Min(pbVidaJugador.Maximum, jugador.Vida));
                lblVidaJugador.Text = jugador.Vida.ToString();
                if (jugador.Vida <= 0)
                {
                    jugador.Vida = 0;
                    pbVidaJugador.Value = 0;
                    lblVidaJugador.Text = "0";
                    MessageBox.Show("Perdiste!");
                }
            }
        }

        private void progressBar2_Click(object sender, EventArgs e)
        {
            // mantener valores no negativos
            foreach (var v in enemigos)
                if (v.Vida < 0) v.Vida = 0;

            if (jugador != null && jugador.Vida < 0)
                jugador.Vida = 0;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            foreach (var v in enemigos)
                if (v.Vida < 0) v.Vida = 0;

            if (jugador != null && jugador.Vida < 0)
                jugador.Vida = 0;
        }

    }
}
