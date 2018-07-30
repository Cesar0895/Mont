using Mont.Semantico;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Mont.Sintactico
{
    public partial class SintacticoMain
    {
        public bool detectoIntentoMotorReversa()
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
                        if (Regex.Replace(tokenActual, " ", "").Equals("reversa"))
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

        public bool comprobarMotorReversa()
        {
            MotorOrdenEjecucion motorOrdenEjecucion = new MotorOrdenEjecucion();
            ParametrosFuncionNativa parametrosFuncionNativa = new ParametrosFuncionNativa();
            try
            {
                motorOrdenEjecucion.linea = arregloTokensEncontrados[indiceGeneral].nLinea;
                parametrosFuncionNativa.tipoParametro = "entero";
                parametrosFuncionNativa.linea = arregloTokensEncontrados[indiceGeneral].nLinea;

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
                if (tokenActual.Equals("reversa"))
                {
                    motorOrdenEjecucion.ejecucion = "reversa";
                    expresionesDentrodeCiclo += "reversa";
                    parametrosFuncionNativa.funcion = "Motor.reversa";
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
                    parametrosFuncionNativa.parametro = arregloTokensEncontrados[indiceGeneral].palabra;
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
            arregloParametrosFuncion.Add(parametrosFuncionNativa);
            arregloOrdenEjecucion.Add(motorOrdenEjecucion);
            expresionesDentrodeCiclo += "\n";
            return true;
        }
    }
}
