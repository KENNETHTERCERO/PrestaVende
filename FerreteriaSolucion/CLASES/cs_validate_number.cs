using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FerreteriaSolucion.CLASES
{
    class cs_validate_number
    {
        public void upperCaseCharacterPress(KeyPressEventArgs pE)
        {
            pE.KeyChar = Char.ToUpper(pE.KeyChar);
            //if (char.IsDigit(pE.KeyChar))
            //{
            //    pE.Handled = false;
            //}
            //else if (char.IsControl(pE.KeyChar))
            //{
            //    pE.Handled = false;
            //}
            //else if (pE.KeyChar == '.')
            //{
            //    pE.Handled = false;
            //}
            //else
            //{
            //    pE.Handled = true;
            //}
        }

        public void SoloNumeros(KeyPressEventArgs pE)
        {
            if (char.IsDigit(pE.KeyChar))
            {
                pE.Handled = false;
            }
            else if (char.IsControl(pE.KeyChar))
            {
                pE.Handled = false;
            }
            else if (pE.KeyChar == '.')
            {
                pE.Handled = false;
            }
            else
            {
                pE.Handled = true;
            }
        }

        public void SoloNumerosConUnPunto(KeyPressEventArgs pE, string texto)
        {

            if (ContadorCaracteres(texto, pE))
            {
                texto.Split();
                if (char.IsDigit(pE.KeyChar))
                {
                    pE.Handled = false;
                }
                else if (char.IsControl(pE.KeyChar))
                {
                    pE.Handled = false;
                }
                else if (pE.KeyChar == '.')
                {
                    pE.Handled = false;
                }
                else
                {
                    pE.Handled = true;
                }
            }
            else
            {
                pE.Handled = true;
            }
        }

        public bool ContadorCaracteres(string textToTest, KeyPressEventArgs keyPressN)
        {   //Este metodo cuenta los puntos que esten en un string
            try
            {
                if (keyPressN.KeyChar == '.')
                {
                    char[] cadenaAr;
                    int count = 0;
                    cadenaAr = textToTest.ToCharArray();

                    for (int i = 0; i < cadenaAr.Length; i++)
                    {
                        if (textToTest.Contains(".") && keyPressN.KeyChar == '.')
                        {
                            count += 1;
                        }
                    }

                    if (count >= 0 && count <= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SoloNumerosNit(KeyPressEventArgs pE)
        {
            if (char.IsDigit(pE.KeyChar))
            {
                pE.Handled = false;
            }
            else if (char.IsControl(pE.KeyChar))
            {
                pE.Handled = false;
            }
            else if (pE.KeyChar == 'C')
            {
                pE.Handled = false;
            }
            else if (pE.KeyChar == '/')
            {
                pE.Handled = false;
            }
            else if (pE.KeyChar == 'F')
            {
                pE.Handled = false;
            }
            else if (pE.KeyChar == 'K')
            {
                pE.Handled = false;
            }
            else if (pE.KeyChar == 'c')
            {
                pE.Handled = false;
            }
            else if (pE.KeyChar == 'f')
            {
                pE.Handled = false;
            }
            else if (pE.KeyChar == 'k')
            {
                pE.Handled = false;
            }
            else
            {
                pE.Handled = true;
            }
        }

        public void SoloNumerosSinSignos(KeyPressEventArgs pE)
        {
            if (char.IsDigit(pE.KeyChar))
            {
                pE.Handled = false;

            }
            else if (char.IsControl(pE.KeyChar))
            {
                pE.Handled = false;

            }
            else
            {
                pE.Handled = true;

            }
        }

        public void SoloNumerosSeparadoPorComa(KeyPressEventArgs pE)
        {
            if (char.IsDigit(pE.KeyChar))
            {
                pE.Handled = false;
            }
            else if (char.IsControl(pE.KeyChar))
            {
                pE.Handled = false;
            }
            else if (pE.KeyChar == ',')
            {
                pE.Handled = false;
            }
            else
            {
                pE.Handled = true;
            }
        }

        public void SoloLetras(KeyPressEventArgs pE)
        {
            if (Char.IsLetter(pE.KeyChar))
            {
                pE.Handled = false;
            }
            else if (Char.IsControl(pE.KeyChar))
            {
                pE.Handled = false;
            }
            else if (Char.IsSeparator(pE.KeyChar))
            {
                pE.Handled = false;
            }
            else
            {
                pE.Handled = true;
            }
        }
    }
}
