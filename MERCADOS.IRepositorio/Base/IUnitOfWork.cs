﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MERCADOS.IRepositorio
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Guarda los cambios del contexto subyacente.
        /// </summary>
        int Guardar(bool validate = true);
    }
}
