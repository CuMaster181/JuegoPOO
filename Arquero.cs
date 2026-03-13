using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoPOO
{
    public class Arquero : Personaje
    {
        public Arquero() : base("Arquero", 100, 18)
        {
        }

        public override int Atacar()
        {
            return Ataque + 3;
        }

    }
}
