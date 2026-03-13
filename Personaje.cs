using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoPOO
{
    public class Personaje
    {
        public string Nombre { get; set; }
        public int Vida { get; set; }
        public int Ataque { get; set; }
        public Personaje(string nombre, int vida, int ataque)
        {
            Nombre = nombre;
            Vida = vida;
            Ataque = ataque;
        }
        public virtual int Atacar()
        {
            return Ataque;
        }

    }
}
