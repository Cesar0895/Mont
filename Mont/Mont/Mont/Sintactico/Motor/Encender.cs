using Mont.Semantico;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Mont.Sintactico
{
    public partial class SintacticoMain
    {

        public bool detectoIntentoEncenderMotor()
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
                        if (Regex.Replace(tokenActual, " ", "").Equals("encender"))
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

        public bool comprobarMotorEncender()
        {
            MotorOrdenEjecucion motorOrdenEjecucion = new MotorOrdenEjecucion();
            try
            {
                motorOrdenEjecucion.linea = arregloTokensEncontrados[indiceGeneral].nLinea;
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("Motor"))
                {
                    expresionesDentrodeCiclo += "Motor";
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
                if (tokenActual.Equals("encender"))
                {
                    expresionesDentrodeCiclo += "encender";
                    motorOrdenEjecucion.ejecucion = "encender";
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
            arregloOrdenEjecucion.Add(motorOrdenEjecucion);
            expresionesDentrodeCiclo += "\n";
            return true;
        }
    }
}
