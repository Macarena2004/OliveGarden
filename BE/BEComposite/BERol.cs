using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.BEComposite
{
    public class BERol : BEComposite
    {
        #region "Propiedades"
        public List<BEComposite> listaPermisos;
        #endregion

        #region "Constructor"
        public BERol(long pId, string pNombre) : base(pId, pNombre)
        {
            listaPermisos = new List<BEComposite>();
        }
        #endregion

        #region "Métodos"
        public override void Agregar(BEComposite oBEComponente)
        {
            try
            {
                if (oBEComponente != null) { listaPermisos.Add(oBEComponente); }
            }
            catch (Exception ex) { throw ex; }
        }

        public override IList<BEComposite> ObtenerHijos()
        {
            try { return listaPermisos; }
            catch (Exception ex) { throw ex; }
        }
        #endregion
    }
}
