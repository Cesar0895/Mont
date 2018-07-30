using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Mont.Sintactico
{
    public partial class SintacticoMain
    {
        public bool detectoIntentoGirarEje()
        {
            try
            {
                //Detectar si lo que se lee es una llamada a funcion nativa
                //comprobando que haya llamada a funcion nativa
                tipoTokenActual = arregloTokensEncontrados[indiceGeneral].tipo;
                if (Regex.Replace(tipoTokenActual, " ", "").Equals("Clase"))
                {
                    tokenActual = arregloTokensEncontrados[indiceGeneral + 1].palabra;
                    if (Regex.Replace(tokenActual, " ", "").Equals("."))
                    {
                        tokenActual = arregloTokensEncontrados[indiceGeneral + 2].palabra;
                        if (Regex.Replace(tokenActual, " ", "").Equals("girar"))
                        {
                            //notifico que si existe un intento llamada a funcion nativa
                            return true;
                        }
                    }
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

        public bool comprobarEjeGirar()
        {
            try
            {
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("Eje"))
                {
                    expresionesDentrodeCiclo += "Eje";
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("."))
                {
                    expresionesDentrodeCiclo += ".";
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (tokenActual.Equals("girar"))
                {
                    expresionesDentrodeCiclo += "girar";
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("("))
                {
                    expresionesDentrodeCiclo += "(";
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tipoTokenActual = arregloTokensEncontrados[indiceGeneral].tipo;
                if (Regex.Replace(tipoTokenActual, " ", "").Equals("Identificador"))
                {
                    expresionesDentrodeCiclo += arregloTokensEncontrados[indiceGeneral].palabra;
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals(")"))
                {
                    expresionesDentrodeCiclo += ")";
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals(";"))
                {
                    expresionesDentrodeCiclo += ";";
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
            expresionesDentrodeCiclo += "\n";
            return true;
        }
    }
}
