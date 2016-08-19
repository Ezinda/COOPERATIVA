using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CooperativaProduccion
{
   public interface IEnlace
    {
       void Enviar(Guid Id, string fet, string nombre);

    }
}
