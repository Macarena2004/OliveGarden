using System;
using System.Collections.Generic;
using Backup;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP
{
    public class MPPGestorBD
    {
            GestorBD gestorBD = new GestorBD();

            #region "Métodos"

            public bool CrearBackup()
            {
                try { gestorBD.CrearBackUp(); return true; }
                catch (Exception ex) { throw ex; }
            }

            public bool CrearRestore(string nombreBackup)
            {
                try { gestorBD.CrearRestore(nombreBackup); return true; }
                catch (Exception ex) { throw ex; }
            }

            public List<string> ListarBackups()
            {
                try
                {
                    List<string> lista = gestorBD.ListarBackups();
                    return lista ?? null;
                }
                catch (Exception ex) { throw ex; }
            }

            #endregion
    }
}


