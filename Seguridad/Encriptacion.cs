using System;
using System.Security.Cryptography;
using System.Text;

namespace Seguridad
{
    /// <summary>
    /// Manejo seguro de contraseñas.
    /// Las contraseñas nunca se guardan: se guarda un hash PBKDF2-HMAC-SHA256 con sal aleatoria.
    /// Formato almacenado:  PBKDF2$SHA256$iteraciones$salBase64$hashBase64
    /// </summary>
    public static class Encriptacion
    {
        #region "Constantes"

        private const string Prefijo = "PBKDF2$SHA256$";
        private const int Iteraciones = 600000;   // Recomendación OWASP para PBKDF2-HMAC-SHA256
        private const int TamanoSal = 16;         // 128 bits
        private const int TamanoHash = 32;        // 256 bits

        #endregion

        #region "Métodos"

        /// <summary>Genera el hash (con sal aleatoria) de una contraseña para guardarlo.</summary>
        public static string HashearPassword(string pPassword)
        {
            if (string.IsNullOrEmpty(pPassword)) throw new ArgumentException("La contraseña no puede ser vacía.");

            byte[] sal = new byte[TamanoSal];
            using (var rng = RandomNumberGenerator.Create()) { rng.GetBytes(sal); }

            byte[] hash = Derivar(pPassword, sal, Iteraciones, TamanoHash);
            return Prefijo + Iteraciones + "$" + Convert.ToBase64String(sal) + "$" + Convert.ToBase64String(hash);
        }

        /// <summary>Comprueba si la contraseña ingresada coincide con lo guardado.</summary>
        public static bool VerificarPassword(string pPassword, string pAlmacenado)
        {
            if (string.IsNullOrEmpty(pPassword) || string.IsNullOrEmpty(pAlmacenado)) return false;

            try
            {
                if (EsHash(pAlmacenado))
                {
                    string[] partes = pAlmacenado.Split('$');   // PBKDF2 | SHA256 | iter | sal | hash
                    if (partes.Length != 5) return false;

                    int iteraciones = int.Parse(partes[2]);
                    byte[] sal = Convert.FromBase64String(partes[3]);
                    byte[] esperado = Convert.FromBase64String(partes[4]);
                    byte[] calculado = Derivar(pPassword, sal, iteraciones, esperado.Length);
                    return ComparacionConstante(calculado, esperado);
                }

                // Compatibilidad: bases de datos viejas que guardaban la contraseña en Base64.
                byte[] viejo = Encoding.UTF8.GetBytes(CodificarFormatoAnterior(pPassword));
                byte[] guardado = Encoding.UTF8.GetBytes(pAlmacenado.Trim());
                return ComparacionConstante(viejo, guardado);
            }
            catch (FormatException) { return false; }
            catch (OverflowException) { return false; }
        }

        /// <summary>Indica si lo guardado está en el formato viejo (o con menos iteraciones) y conviene volver a hashear.</summary>
        public static bool NecesitaRehash(string pAlmacenado)
        {
            if (!EsHash(pAlmacenado)) return true;

            string[] partes = pAlmacenado.Split('$');
            int iteraciones;
            return partes.Length != 5 || !int.TryParse(partes[2], out iteraciones) || iteraciones < Iteraciones;
        }

        public static bool EsHash(string pAlmacenado)
        {
            return !string.IsNullOrEmpty(pAlmacenado) && pAlmacenado.StartsWith(Prefijo, StringComparison.Ordinal);
        }

        #endregion

        #region "Privados"

        private static byte[] Derivar(string pPassword, byte[] pSal, int pIteraciones, int pLargo)
        {
            using (var kdf = new Rfc2898DeriveBytes(pPassword, pSal, pIteraciones, HashAlgorithmName.SHA256))
            {
                return kdf.GetBytes(pLargo);
            }
        }

        // Compara sin cortar en el primer byte distinto, para no filtrar información por tiempos.
        private static bool ComparacionConstante(byte[] a, byte[] b)
        {
            int diferencia = a.Length ^ b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++) diferencia |= a[i] ^ b[i];
            return diferencia == 0;
        }

        // Formato que usaba la versión anterior del sistema (solo para poder migrar usuarios existentes).
        private static string CodificarFormatoAnterior(string pPassword)
        {
            return Convert.ToBase64String(Encoding.Unicode.GetBytes(pPassword));
        }

        #endregion
    }
}
