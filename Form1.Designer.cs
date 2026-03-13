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
            btnEspecial = new Button();
            pbVidaJugador = new ProgressBar();
            pbVidaEnemigo = new ProgressBar();
            txtLog = new TextBox();
            SuspendLayout();
            // 
            // cmbPersonaje
            // 
            cmbPersonaje.FormattingEnabled = true;
            cmbPersonaje.Items.AddRange(new object[] { "Mago", "Guerrero", "Arquero" });
            cmbPersonaje.Location = new Point(133, 69);
            cmbPersonaje.Name = "cmbPersonaje";
            cmbPersonaje.Size = new Size(182, 33);
            cmbPersonaje.TabIndex = 0;
            cmbPersonaje.Text = "Elegir Personaje";
            cmbPersonaje.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(163, 218);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(112, 34);
            btnCrear.TabIndex = 1;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += button1_Click;
            // 
            // lblJugador
            // 
            lblJugador.AutoSize = true;
            lblJugador.Location = new Point(185, 416);
            lblJugador.Name = "lblJugador";
            lblJugador.Size = new Size(96, 25);
            lblJugador.TabIndex = 2;
            lblJugador.Text = "Personaje: ";
            lblJugador.Click += label1_Click;
            // 
            // lblVidaJugador
            // 
            lblVidaJugador.AutoSize = true;
            lblVidaJugador.Location = new Point(175, 441);
            lblVidaJugador.Name = "lblVidaJugador";
            lblVidaJugador.Size = new Size(116, 25);
            lblVidaJugador.TabIndex = 3;
            lblVidaJugador.Text = "Vida Jugador";
            lblVidaJugador.Click += label2_Click;
            // 
            // lblVidaEnemigo
            // 
            lblVidaEnemigo.AutoSize = true;
            lblVidaEnemigo.Location = new Point(175, 555);
            lblVidaEnemigo.Name = "lblVidaEnemigo";
            lblVidaEnemigo.Size = new Size(122, 25);
            lblVidaEnemigo.TabIndex = 4;
            lblVidaEnemigo.Text = "Vida Enemigo";
            lblVidaEnemigo.Click += lblVidaEnemigo_Click;
            // 
            // btnAtacar
            // 
            btnAtacar.Location = new Point(55, 336);
            btnAtacar.Name = "btnAtacar";
            btnAtacar.Size = new Size(112, 34);
            btnAtacar.TabIndex = 5;
            btnAtacar.Text = "Atacar";
            btnAtacar.UseVisualStyleBackColor = true;
            btnAtacar.Click += Atacar_Click;
            // 
            // btnEspecial
            // 
            btnEspecial.Location = new Point(280, 336);
            btnEspecial.Name = "btnEspecial";
            btnEspecial.Size = new Size(112, 34);
            btnEspecial.TabIndex = 6;
            btnEspecial.Text = "Especial";
            btnEspecial.UseVisualStyleBackColor = true;
            btnEspecial.Click += button3_Click;
            // 
            // pbVidaJugador
            // 
            pbVidaJugador.BackColor = Color.SeaGreen;
            pbVidaJugador.Location = new Point(97, 469);
            pbVidaJugador.Name = "pbVidaJugador";
            pbVidaJugador.Size = new Size(271, 34);
            pbVidaJugador.TabIndex = 7;
            pbVidaJugador.Click += progressBar1_Click;
            // 
            // pbVidaEnemigo
            // 
            pbVidaEnemigo.BackColor = Color.SeaGreen;
            pbVidaEnemigo.Location = new Point(97, 583);
            pbVidaEnemigo.Name = "pbVidaEnemigo";
            pbVidaEnemigo.Size = new Size(271, 34);
            pbVidaEnemigo.TabIndex = 8;
            pbVidaEnemigo.Click += progressBar2_Click;
            // 
            // txtLog
            // 
            txtLog.Location = new Point(97, 686);
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(271, 31);
            txtLog.TabIndex = 9;
            txtLog.TextChanged += txtLog_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1558, 787);
            Controls.Add(txtLog);
            Controls.Add(pbVidaEnemigo);
            Controls.Add(pbVidaJugador);
            Controls.Add(btnEspecial);
            Controls.Add(btnAtacar);
            Controls.Add(lblVidaEnemigo);
            Controls.Add(lblVidaJugador);
            Controls.Add(lblJugador);
            Controls.Add(btnCrear);
            Controls.Add(cmbPersonaje);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private ComboBox cmbPersonaje;
        private Button btnCrear;
        private Label lblJugador;
        private Label lblVidaJugador;
        private Label lblVidaEnemigo;
        private Button btnAtacar;
        private Button btnEspecial;
        private ProgressBar pbVidaJugador;
        private ProgressBar pbVidaEnemigo;
        private TextBox txtLog;
    }
}
