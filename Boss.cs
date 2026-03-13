using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoPOO
{
    public class Boss : Personaje
    {
        public Boss() : base("Boss", 250, 30)
        {
        }

        public override int Atacar()
        {
            // ataque variable para el boss
            var rnd = new Random();
            return Ataque + rnd.Next(5, 15);
        }
    }
}
