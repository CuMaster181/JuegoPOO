using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoPOO
{
    public class Guerrero : Personaje
    {
       
        public Guerrero() : base("Guerrero", 120, 20)
        {
        }
        public override int Atacar()
        {
            return Ataque + 10;
        }

    }
}
