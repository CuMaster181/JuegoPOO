namespace JuegoPOO
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmbPersonaje = new ComboBox();
            btnCrear = new Button();
            lblJugador = new Label();
            lblVidaJugador = new Label();
            lblVidaEnemigo = new Label();
            btnAtacar = new Button();
            btnCurar = new Button();
            btnEspecial = new Button();
            pbVidaJugador = new ProgressBar();
            pbVidaEnemigo = new ProgressBar();
            txtLog = new TextBox();
            pbJugador = new PictureBox();
            panelEnemigos = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)pbJugador).BeginInit();
            SuspendLayout();
            // 
            // cmbPersonaje
            // 
            cmbPersonaje.FormattingEnabled = true;
            cmbPersonaje.Items.AddRange(new object[] { "Mago", "Guerrero", "Arquero" });
            cmbPersonaje.Location = new Point(106, 55);
            cmbPersonaje.Margin = new Padding(2);
            cmbPersonaje.Name = "cmbPersonaje";
            cmbPersonaje.Size = new Size(146, 28);
            cmbPersonaje.TabIndex = 0;
            cmbPersonaje.Text = "Elegir Personaje";
            cmbPersonaje.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(130, 174);
            btnCrear.Margin = new Padding(2);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(90, 27);
            btnCrear.TabIndex = 1;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += button1_Click;
            // 
            // lblJugador
            // 
            lblJugador.AutoSize = true;
            lblJugador.Location = new Point(377, 33);
            lblJugador.Margin = new Padding(2, 0, 2, 0);
            lblJugador.Name = "lblJugador";
            lblJugador.Size = new Size(79, 20);
            lblJugador.TabIndex = 2;
            lblJugador.Text = "Personaje: ";
            // 
            // lblVidaJugador
            // 
            lblVidaJugador.AutoSize = true;
            lblVidaJugador.Location = new Point(377, 58);
            lblVidaJugador.Margin = new Padding(2, 0, 2, 0);
            lblVidaJugador.Name = "lblVidaJugador";
            lblVidaJugador.Size = new Size(96, 20);
            lblVidaJugador.TabIndex = 3;
            lblVidaJugador.Text = "Vida Jugador";
            // 
            // lblVidaEnemigo
            // 
            lblVidaEnemigo.AutoSize = true;
            lblVidaEnemigo.Location = new Point(948, 58);
            lblVidaEnemigo.Margin = new Padding(2, 0, 2, 0);
            lblVidaEnemigo.Name = "lblVidaEnemigo";
            lblVidaEnemigo.Size = new Size(102, 20);
            lblVidaEnemigo.TabIndex = 4;
            lblVidaEnemigo.Text = "Vida Enemigo";
            // 
            // btnAtacar
            // 
            btnAtacar.Location = new Point(44, 269);
            btnAtacar.Margin = new Padding(2);
            btnAtacar.Name = "btnAtacar";
            btnAtacar.Size = new Size(90, 27);
            btnAtacar.TabIndex = 5;
            btnAtacar.Text = "Atacar";
            btnAtacar.UseVisualStyleBackColor = true;
            btnAtacar.Click += Atacar_Click;
            // 
            // btnCurar
            // 
            btnCurar.Location = new Point(137, 269);
            btnCurar.Margin = new Padding(2);
            btnCurar.Name = "btnCurar";
            btnCurar.Size = new Size(90, 27);
            btnCurar.TabIndex = 6;
            btnCurar.Text = "Curar";
            btnCurar.UseVisualStyleBackColor = true;
            btnCurar.Click += btnCurar_Click;
            // 
            // btnEspecial
            // 
            btnEspecial.Location = new Point(231, 269);
            btnEspecial.Margin = new Padding(2);
            btnEspecial.Name = "btnEspecial";
            btnEspecial.Size = new Size(90, 27);
            btnEspecial.TabIndex = 7;
            btnEspecial.Text = "Especial";
            btnEspecial.UseVisualStyleBackColor = true;
            btnEspecial.Click += button3_Click;
            // 
            // pbVidaJugador
            // 
            pbVidaJugador.BackColor = Color.SeaGreen;
            pbVidaJugador.Location = new Point(377, 55);
            pbVidaJugador.Margin = new Padding(2);
            pbVidaJugador.Name = "pbVidaJugador";
            pbVidaJugador.Size = new Size(217, 27);
            pbVidaJugador.TabIndex = 8;
            pbVidaJugador.Click += pbVidaJugador;
            // 
            // pbVidaEnemigo
            // 
            pbVidaEnemigo.BackColor = Color.SeaGreen;
            pbVidaEnemigo.Location = new Point(833, 55);
            pbVidaEnemigo.Margin = new Padding(2);
            pbVidaEnemigo.Name = "pbVidaEnemigo";
            pbVidaEnemigo.Size = new Size(217, 27);
            pbVidaEnemigo.TabIndex = 9;
            pbVidaEnemigo.Click += progressBar2_Click;
            // 
            // txtLog
            // 
            txtLog.Location = new Point(62, 333);
            txtLog.Margin = new Padding(2);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(218, 223);
            txtLog.TabIndex = 10;
            // 
            // pbJugador
            // 
            pbJugador.BorderStyle = BorderStyle.FixedSingle;
            pbJugador.Location = new Point(377, 93);
            pbJugador.Margin = new Padding(2);
            pbJugador.Name = "pbJugador";
            pbJugador.Size = new Size(325, 400);
            pbJugador.SizeMode = PictureBoxSizeMode.Zoom;
            pbJugador.TabIndex = 11;
            pbJugador.TabStop = false;
            pbJugador.Visible = false;
            // 
            // panelEnemigos
            // 
            panelEnemigos.AutoScroll = true;
            panelEnemigos.FlowDirection = FlowDirection.TopDown;
            panelEnemigos.Location = new Point(770, 93);
            panelEnemigos.Margin = new Padding(2);
            panelEnemigos.Name = "panelEnemigos";
            panelEnemigos.Size = new Size(280, 400);
            panelEnemigos.TabIndex = 12;
            panelEnemigos.WrapContents = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1246, 630);
            Controls.Add(panelEnemigos);
            Controls.Add(pbJugador);
            Controls.Add(txtLog);
            Controls.Add(btnEspecial);
            Controls.Add(btnCurar);
            Controls.Add(btnAtacar);
            Controls.Add(lblVidaEnemigo);
            Controls.Add(lblVidaJugador);
            Controls.Add(lblJugador);
            Controls.Add(btnCrear);
            Controls.Add(cmbPersonaje);
            Controls.Add(pbVidaJugador);
            Controls.Add(pbVidaEnemigo);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pbJugador).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbPersonaje;
        private Button btnCrear;
        private Label lblJugador;
        private Label lblVidaJugador;
        private Label lblVidaEnemigo;
        private Button btnAtacar;
        private Button btnCurar;
        private Button btnEspecial;
        private ProgressBar pbVidaJugador;
        private ProgressBar pbVidaEnemigo;
        private TextBox txtLog;
        private PictureBox pbJugador;
        private FlowLayoutPanel panelEnemigos;
    }
}
