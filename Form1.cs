using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuegoPOO
{
    public partial class Form1 : Form
    {

        Personaje jugador;
        List<Personaje> enemigos = new List<Personaje>();
        List<PictureBox> enemyBoxes = new List<PictureBox>();
        List<ProgressBar> enemyHealthBars = new List<ProgressBar>();
        List<int> enemyMaxVida = new List<int>();
        Random rnd = new Random();
        int ronda = 1;
        bool bossPresent = false;
        int vidaMaxJugador = 0;

        int selectedEnemyIndex = -1;

        enum Turn { Player, Enemy }
        Turn turno = Turn.Player;

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
                    pbJugador.Image = TryLoadImage("mago.png");
                    break;
                case "Arquero":
                    jugador = new Arquero();
                    pbJugador.Image = TryLoadImage("arquero.png");
                    break;
                case "Guerrero":
                    jugador = new Guerrero();
                    pbJugador.Image = TryLoadImage("guerrero.png");
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
            turno = Turn.Player;
            IniciarRonda();
        }

        private Image TryLoadImage(string fileName)
        {
            try
            {
                var path1 = Path.Combine(Application.StartupPath, fileName);
                if (File.Exists(path1))
                    return Image.FromFile(path1);

                var path2 = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                if (File.Exists(path2))
                    return Image.FromFile(path2);

                return null;
            }
            catch
            {
                return null;
            }
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

            foreach (var hb in enemyHealthBars)
            {
                hb.Dispose();
            }
            enemyHealthBars.Clear();
            enemyMaxVida.Clear();

            panelEnemigos.Controls.Clear();

            txtLog.AppendText($"--- Ronda {ronda} ---{Environment.NewLine}");

            if (ronda > 3)
            {
                // aparece el boss
                bossPresent = true;
                var boss = new Boss();
                enemigos.Add(boss);

                CrearControlesEnemigo(boss, "boss.png");

                txtLog.AppendText("¡Ha aparecido el Boss!" + Environment.NewLine);

                // seleccionar primer enemigo por defecto
                SelectEnemy(0);
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
                    imagen = "enemigo1.png";
                }
                else if (tipo == 2)
                {
                    e = new Arquero();
                    imagen = "enemigo1.png";
                    imagen = "enemigo2.png";
                }
                else
                {
                    e = new Guerrero();
                    imagen = "enemigo2.png";
                }
                enemigos.Add(e);
                CrearControlesEnemigo(e, imagen);
            }

            // seleccionar primer enemigo vivo
            var primero = enemigos.Select((x, idx) => new { x, idx }).FirstOrDefault(x => x.x.Vida > 0);
            if (primero != null)
                SelectEnemy(primero.idx);
        }

        private void CrearControlesEnemigo(Personaje enemigo, string rutaImagen)
        {
            // panel para contener imagen + barra
            var container = new Panel();
            container.Size = new Size(140, 170);
            container.Margin = new Padding(6);
            container.BackColor = SystemColors.Control;

            var pb = new PictureBox();
            pb.Size = new Size(120, 120);
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Location = new Point(10, 6);
            pb.BorderStyle = BorderStyle.FixedSingle;
            try
            {
                var img = TryLoadImage(rutaImagen);
                if (img != null)
                    pb.Image = img;
                else
                    pb.BackColor = Color.DarkRed;
            }
            catch
            {
                pb.BackColor = Color.DarkRed;
            }

            int index = enemyBoxes.Count; // índice actual antes de añadir
            pb.Cursor = Cursors.Hand;
            pb.Click += (s, e) =>
            {
                SelectEnemy(index);
            };

            var hb = new ProgressBar();
            hb.Size = new Size(120, 16);
            hb.Location = new Point(10, 130);
            hb.Minimum = 0;
            hb.Maximum = Math.Max(1, enemigo.Vida);
            hb.Value = Math.Max(0, Math.Min(hb.Maximum, enemigo.Vida));
            var lbl = new Label();
            lbl.AutoSize = false;
            lbl.Size = new Size(120, 14);
            lbl.Location = new Point(10, 148);
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Text = enemigo.Vida.ToString();

            container.Controls.Add(pb);
            container.Controls.Add(hb);
            container.Controls.Add(lbl);

            panelEnemigos.Controls.Add(container);

            enemyBoxes.Add(pb);
            enemyHealthBars.Add(hb);
            enemyMaxVida.Add(hb.Maximum);
        }

        private void SelectEnemy(int index)
        {
            if (index < 0 || index >= enemigos.Count) return;
            selectedEnemyIndex = index;

            // resaltar seleccionado visualmente
            for (int i = 0; i < enemyBoxes.Count; i++)
            {
                enemyBoxes[i].BackColor = (i == index) ? Color.LightGreen : Color.Transparent;
            }

            // actualizar el panel de vida del enemigo principal si existe
            var objetivo = enemigos[index];
            pbVidaEnemigo.Maximum = Math.Max(1, enemyMaxVida[index]);
            pbVidaEnemigo.Value = Math.Max(0, Math.Min(pbVidaEnemigo.Maximum, objetivo.Vida));
            lblVidaEnemigo.Text = objetivo.Vida.ToString();
        }

        private async void Atacar_Click(object sender, EventArgs e)
        {
            if (jugador == null)
            {
                MessageBox.Show("Crea un personaje primero.");
                return;
            }

            if (turno != Turn.Player)
            {
                MessageBox.Show("No es tu turno.");
                return;
            }

            if (selectedEnemyIndex == -1 || selectedEnemyIndex >= enemigos.Count)
            {
                MessageBox.Show("Selecciona un enemigo a atacar.");
                return;
            }

            var objetivo = enemigos[selectedEnemyIndex];
            if (objetivo.Vida <= 0)
            {
                MessageBox.Show("Ese enemigo ya está derrotado. Selecciona otro.");
                return;
            }

            int daño = jugador.Atacar();
            objetivo.Vida -= daño;
            txtLog.AppendText($"Jugador hizo {daño} de daño al enemigo {selectedEnemyIndex + 1}{Environment.NewLine}");

            if (objetivo.Vida <= 0)
            {
                objetivo.Vida = 0;
                // actualizar visual del enemigo derrotado
                enemyBoxes[selectedEnemyIndex].Enabled = false;
                enemyBoxes[selectedEnemyIndex].Image = null;
                enemyBoxes[selectedEnemyIndex].BackColor = Color.Gray;
                txtLog.AppendText($"Enemigo {selectedEnemyIndex + 1} derrotado.{Environment.NewLine}");
            }

            UpdateEnemyBar(selectedEnemyIndex);

            // finalizar turno del jugador y procesar enemigos
            await EndPlayerTurnAsync();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            // ataque especial: doble ataque al objetivo actual
            if (jugador == null)
            {
                MessageBox.Show("Crea un personaje primero.");
                return;
            }

            if (turno != Turn.Player)
            {
                MessageBox.Show("No es tu turno.");
                return;
            }

            if (selectedEnemyIndex == -1 || selectedEnemyIndex >= enemigos.Count)
            {
                MessageBox.Show("Selecciona un enemigo para el ataque especial.");
                return;
            }

            var objetivo = enemigos[selectedEnemyIndex];
            if (objetivo.Vida <= 0)
            {
                MessageBox.Show("Ese enemigo ya está derrotado. Selecciona otro.");
                return;
            }

            int daño = jugador.Ataque * 2;
            objetivo.Vida -= daño;
            txtLog.AppendText($"Ataque especial hizo {daño} de daño al enemigo {selectedEnemyIndex + 1}{Environment.NewLine}");

            if (objetivo.Vida <= 0)
            {
                objetivo.Vida = 0;
                enemyBoxes[selectedEnemyIndex].Enabled = false;
                enemyBoxes[selectedEnemyIndex].Image = null;
                enemyBoxes[selectedEnemyIndex].BackColor = Color.Gray;
                txtLog.AppendText($"Enemigo {selectedEnemyIndex + 1} derrotado.{Environment.NewLine}");
            }

            UpdateEnemyBar(selectedEnemyIndex);

            await EndPlayerTurnAsync();
        }

        private async void btnCurar_Click(object sender, EventArgs e)
        {
            if (jugador == null)
            {
                MessageBox.Show("Crea un personaje primero.");
                return;
            }

            if (turno != Turn.Player)
            {
                MessageBox.Show("No es tu turno.");
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

            await EndPlayerTurnAsync();
        }

        private void UpdateEnemyBar(int idx)
        {
            if (idx < 0 || idx >= enemyHealthBars.Count) return;
            var objetivo = enemigos[idx];
            var hb = enemyHealthBars[idx];
            hb.Maximum = Math.Max(1, enemyMaxVida[idx]);
            hb.Value = Math.Max(0, Math.Min(hb.Maximum, objetivo.Vida));

            // si está seleccionado, actualizar el pbVidaEnemigo principal también
            if (selectedEnemyIndex == idx)
            {
                pbVidaEnemigo.Maximum = hb.Maximum;
                pbVidaEnemigo.Value = hb.Value;
                lblVidaEnemigo.Text = objetivo.Vida.ToString();
            }
        }

        private async Task EndPlayerTurnAsync()
        {
            // comprobar si todos los enemigos están muertos
            if (!enemigos.Any(x => x.Vida > 0))
            {
                // si era boss, reiniciar el juego al vencerlo
                if (bossPresent)
                {
                    txtLog.AppendText("¡Has derrotado al Boss! El juego se reiniciará." + Environment.NewLine);
                    SafeRestart("¡Has derrotado al Boss! El juego se reiniciará. Selecciona otro personaje.");
                    return;
                }

                txtLog.AppendText("¡Ronda completada!" + Environment.NewLine);
                ronda++;
                if (jugador != null && jugador.Vida > 0)
                {
                    MessageBox.Show($"Has completado la ronda. Avanzando a la ronda {ronda}");
                    turno = Turn.Player;
                    IniciarRonda();
                }
                else
                {
                    SafeRestart("Perdiste! El juego se reiniciará. Selecciona otro personaje.");
                }
                return;
            }

            // pasar turno a enemigos
            turno = Turn.Enemy;

            await ProcesarTurnoEnemigosAsync();

            // después del turno enemigo, comprobar vida jugador y continuar
            if (jugador == null || jugador.Vida <= 0)
            {
                SafeRestart("Perdiste! El juego se reiniciará. Selecciona otro personaje.");
                return;
            }

            // si todavía hay enemigos, volver al turno del jugador
            turno = Turn.Player;

            // si el objetivo seleccionado murió, seleccionar el siguiente vivo
            if (selectedEnemyIndex == -1 || selectedEnemyIndex >= enemigos.Count || enemigos[selectedEnemyIndex].Vida <= 0)
            {
                var primerVivoIdx = enemigos.Select((e, idx) => new { e, idx }).FirstOrDefault(x => x.e.Vida > 0)?.idx ?? -1;
                if (primerVivoIdx != -1)
                    SelectEnemy(primerVivoIdx);
                else
                {
                    // todos muertos -> iniciar siguiente ronda
                    txtLog.AppendText("¡Ronda completada!" + Environment.NewLine);
                    ronda++;
                    if (jugador != null && jugador.Vida > 0)
                    {
                        MessageBox.Show($"Has completado la ronda. Avanzando a la ronda {ronda}");
                        IniciarRonda();
                    }
                    else
                    {
                        SafeRestart("Perdiste! El juego se reiniciará. Selecciona otro personaje.");
                    }
                }
            }
        }

        private async Task ProcesarTurnoEnemigosAsync()
        {
            // cada enemigo vivo ataca una vez con pequeña pausa entre ataques
            var vivos = enemigos
                .Select((e, idx) => new { e, idx })
                .Where(x => x.e.Vida > 0)
                .ToList();

            foreach (var atacanteInfo in vivos)
            {
                var atacante = atacanteInfo.e;
                int contra = rnd.Next(5, 10);
                if (atacante is Boss)
                    contra = rnd.Next(5, 15);

                jugador.Vida -= contra;
                if (jugador.Vida < 0) jugador.Vida = 0;
                txtLog.AppendText($"Enemigo {atacanteInfo.idx + 1} hizo {contra} de daño{Environment.NewLine}");

                pbVidaJugador.Maximum = Math.Max(1, vidaMaxJugador);
                pbVidaJugador.Value = Math.Max(0, Math.Min(pbVidaJugador.Maximum, jugador.Vida));
                lblVidaJugador.Text = jugador.Vida.ToString();

                if (jugador.Vida <= 0)
                {
                    jugador.Vida = 0;
                    pbVidaJugador.Value = 0;
                    lblVidaJugador.Text = "0";
                    // reiniciar juego tras derrota del jugador de forma segura
                    SafeRestart("Perdiste! El juego se reiniciará. Selecciona otro personaje.");
                    break;
                }

                await Task.Delay(350); // pequeña pausa para sensación de turno
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

        // ------------------ Nuevos métodos para reiniciar ------------------

        private void SafeRestart(string message)
        {
            if (this.IsHandleCreated && !this.IsDisposed)
            {
                Action restartAction = () =>
                {
                    try
                    {
                        MessageBox.Show(message);
                    }
                    catch { /* ignorar */ }
                    ReiniciarJuego();
                };

                if (this.InvokeRequired)
                    this.BeginInvoke(restartAction);
                else
                    restartAction();
            }
        }

        private void ManejarDerrotaJugador()
        {
            SafeRestart("Perdiste! El juego se reiniciará. Selecciona otro personaje.");
        }

        private void ReiniciarJuego()
        {
            // limpiar estado de enemigos
            enemigos.Clear();
            foreach (var pb in enemyBoxes)
            {
                try { pb.Dispose(); } catch { }
            }
            enemyBoxes.Clear();

            foreach (var hb in enemyHealthBars)
            {
                try { hb.Dispose(); } catch { }
            }
            enemyHealthBars.Clear();
            enemyMaxVida.Clear();

            try { panelEnemigos.Controls.Clear(); } catch { }

            // reset visual jugador
            jugador = null;
            vidaMaxJugador = 0;
            selectedEnemyIndex = -1;
            turno = Turn.Player;
            bossPresent = false;
            ronda = 1;

            try { pbJugador.Image = null; pbJugador.Visible = false; } catch { }

            try
            {
                pbVidaJugador.Maximum = 1;
                pbVidaJugador.Value = 0;
                lblVidaJugador.Text = "0";
            }
            catch { }

            try
            {
                pbVidaEnemigo.Maximum = 1;
                pbVidaEnemigo.Value = 0;
                lblVidaEnemigo.Text = "0";
            }
            catch { }

            // permitir escoger personaje otra vez
            try
            {
                cmbPersonaje.SelectedItem = null;
                cmbPersonaje.Enabled = true;
            }
            catch { }

            try
            {
                txtLog.AppendText("Juego reiniciado. Selecciona un personaje para comenzar de nuevo." + Environment.NewLine);
            }
            catch { }
        }

    }
}
