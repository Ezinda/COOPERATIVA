using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionRRHH
{
    public interface IEnlaceNovedades
    {
        void Enviar(Guid ProductorId,string fet, string nombre);
    }
}
