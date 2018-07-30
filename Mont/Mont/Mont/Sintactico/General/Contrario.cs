using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Mont.Sintactico
{
    public partial class SintacticoMain
    {
        public bool detectoIntentoContrario()
        {
            try
            {
                //Detectar si lo que se lee es una condicion contraria
                //comprobando que este la palabra "contrario"
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("contrario"))
                {
                    //notifico que si existe un intento de condicion contraria
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {

            }
            return false;
        }

        public bool comprobarContrario()
        {
            try
            {
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("contrario"))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("{"))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //reviso si hay expresiones declaradas
                while (expresionesDeclaradas())
                {
                    expresionesDeclaradas();
                }

                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("}"))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {

            }
            return true;
        }

    }
}
