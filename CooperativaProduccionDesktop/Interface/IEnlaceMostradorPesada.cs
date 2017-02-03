using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion
{
   public interface  IEnlaceMostradorPesada
   {
       void Enviar(string productor, string cuit);
    }
}
