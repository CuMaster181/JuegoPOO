namespace JuegoPOO
{
    public partial class Form1 : Form
    {

        Personaje jugador;
        Personaje enemigo;
        Random rnd = new Random();


        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbPersonaje.Items.Add("Mago");
            cmbPersonaje.Items.Add("Arquero");
            cmbPersonaje.Items.Add("Guerrero");

            txtLog.Multiline = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string seleccion = cmbPersonaje.Text;

            if (seleccion == "Mago")
                jugador = new Mago();

            else if (seleccion == "Arquero")
                jugador = new Arquero();

            else if (seleccion == "Guerrero")
                jugador = new Guerrero();

            // enemigo aleatorio
            int tipo = rnd.Next(1, 4);

            if (tipo == 1)
                enemigo = new Mago();
            else if (tipo == 2)
                enemigo = new Arquero();
            else
                enemigo = new Guerrero();

            // actualizar interfaz
            lblJugador.Text = jugador.Nombre;

            pbVidaJugador.Maximum = jugador.Vida;
            pbVidaJugador.Value = jugador.Vida;

            pbVidaEnemigo.Maximum = enemigo.Vida;
            pbVidaEnemigo.Value = enemigo.Vida;

            lblVidaJugador.Text = jugador.Vida.ToString();
            lblVidaEnemigo.Text = enemigo.Vida.ToString();

            txtLog.AppendText("¡Comienza la batalla!" + Environment.NewLine);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Atacar_Click(object sender, EventArgs e)
        {
            int daño = jugador.Atacar();
            enemigo.Vida -= daño;

            txtLog.AppendText($"Jugador hizo {daño} de daño\n");

            int contra = rnd.Next(5, 15);
            jugador.Vida -= contra;

            txtLog.AppendText($"Enemigo hizo {contra} de daño\n");

            pbVidaJugador.Value = jugador.Vida;
            pbVidaEnemigo.Value = enemigo.Vida;

            if (enemigo.Vida <= 0)
            {
                MessageBox.Show("Ganaste!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int daño = jugador.Ataque * 2;

            enemigo.Vida -= daño;

            txtLog.AppendText($"Ataque especial hizo {daño} de daño\n");

            pbVidaEnemigo.Value = enemigo.Vida;

            if (enemigo.Vida <= 0)
            {
                MessageBox.Show("Ganaste!");
            }
        }

        private void progressBar2_Click(object sender, EventArgs e)
        {
            if (enemigo.Vida < 0)
                enemigo.Vida = 0;

            if (jugador.Vida < 0)
                jugador.Vida = 0;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            if (enemigo.Vida < 0)
                enemigo.Vida = 0;

            if (jugador.Vida < 0)
                jugador.Vida = 0;
        }

        private void lblVidaEnemigo_Click(object sender, EventArgs e)
        {

        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
