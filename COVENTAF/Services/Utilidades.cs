using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVENTAF.Services
{
    public static class Utilidades
    {
        public static void UnPunto(KeyPressEventArgs e, String cadena, ref bool bandera)
        {
            int contador = 0;
            String caracter = "";
            for (int n = 0; n < cadena.Length; n++)
            {
                caracter = cadena.Substring(n, 1);
                if (caracter == ".")
                {
                    contador++;
                }
            }

            if (contador == 0)
            {
                bandera = true;
                if (e.KeyChar == '.' && bandera)
                {
                    bandera = false;
                    e.Handled = false;
                }
                else if (Char.IsDigit(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else
            {
                bandera = false;
                e.Handled = true;
                if (Char.IsDigit(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }
    }
}
