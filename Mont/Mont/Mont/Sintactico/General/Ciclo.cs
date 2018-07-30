using Mont.Intermedio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Mont.Sintactico
{

    public partial class SintacticoMain
    {
        string restante = "";

string codigoFuente =   "void setup() {\n" +
                        "pinMode(3,OUTPUT);\n" +
                        "pinMode(4,OUTPUT);\n" +
                        "pinMode(5,OUTPUT);\n" +
                        "pinMode(6,OUTPUT);\n" +
                        "pinMode(9,OUTPUT);\n" +
                        "pinMode(10,OUTPUT);\n" +
                        "pinMode(11,OUTPUT);\n" +
                        "pinMode(12,OUTPUT);\n" +
                        "}\n" +

                        "void loop(){\n" +
                        "{reemplazar}" +
                        "}\n" +

                        "void GirarIzq(){\n" +
                        "  digitalWrite(3, LOW);\n" +
                        "  digitalWrite(4, HIGH);\n" +
                        "}\n" +

                        "void GirarDer(){\n" +
                        "  digitalWrite(3, HIGH);\n" +
                        "  digitalWrite(4, LOW);\n" +
                        "}\n" +

                        "void MotorReversa(){\n" +
                        "  digitalWrite(11, HIGH);\n" +
                        "  digitalWrite(12, LOW);\n" +
                        "}\n" +

                        "void MotorAvanzar(){\n" +
                        "  digitalWrite(11, LOW);\n" +
                        "  digitalWrite(12, HIGH);\n" +
                        "}\n" +

                        "void MotorFrenar(){\n" +
                        "  digitalWrite(11, LOW);\n" +
                        "  digitalWrite(12, LOW);\n" +
                        "}\n" +

                        "void CuchillaBajar(){\n" +
                        "  digitalWrite(9, HIGH);\n" +
                        "  digitalWrite(10, LOW);\n" +
                        "}\n" +

                        "void CuchillaParar(){\n" +
                        "  digitalWrite(9, LOW);\n" +
                        "  digitalWrite(10, LOW);\n" +
                        "}\n"+

                        "void CuchillaSubir(){\n"+
                        "  digitalWrite(9, LOW);\n"+
                        "  digitalWrite(10, HIGH);\n"+
                        "}\n"+

                        "void CuchillaReInclinar(){\n"+
                        " digitalWrite(5, HIGH);\n"+
                        " digitalWrite(6, LOW);\n"+
                        "}\n"+

                        "void CuchillaInclinar(){\n"+
                        "digitalWrite(5, LOW);\n"+
                        "digitalWrite(6, HIGH);\n"+
                        "}\n"+

                        "void CuchillaPararInclinacion(){\n"+
                        "digitalWrite(5, LOW);\n"+
                        "digitalWrite(6, LOW);\n"+
                        "}\n";

        public bool detectoIntentoCiclo()
        {
            try
            {
                //Detectar si lo que se lee es un ciclo
                //comprobando que este la palabra "ciclo"
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("ciclo"))
                {
                    //notifico que si existe un intento de maniobra
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

        public bool comprobarCiclo()
        {
            CicloIntermedio cicloIntermedio = new CicloIntermedio();
            try
            {
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("ciclo"))
                {
                   
                    for (int i = 0; i < indiceGeneral; i++)
                    {
                        restante += arregloTokensEncontrados[i].palabra+" ";
                    }

                    cicloReemplazar += "ciclo";
                       indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("("))
                {
                    cicloReemplazar += "(";
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("entero"))
                {
                    cicloReemplazar += "entero";
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tipoTokenActual = arregloTokensEncontrados[indiceGeneral].tipo;
                if (Regex.Replace(tipoTokenActual, " ", "").Equals("Identificador"))
                {
                    cicloReemplazar += arregloTokensEncontrados[indiceGeneral].palabra;
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("="))
                {
                    cicloReemplazar += "=";
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tipoTokenActual = arregloTokensEncontrados[indiceGeneral].tipo;
                if (Regex.Replace(tipoTokenActual, " ", "").Equals("Identificador"))
                {
                    cicloReemplazar += arregloTokensEncontrados[indiceGeneral].palabra;
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals(";"))
                {
                    cicloReemplazar += ";";
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tipoTokenActual = arregloTokensEncontrados[indiceGeneral].tipo;
                if (Regex.Replace(tipoTokenActual, " ", "").Equals("Identificador"))
                {
                    cicloReemplazar += arregloTokensEncontrados[indiceGeneral].palabra;
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("<"))
                {
                    cicloReemplazar += "<";
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tipoTokenActual = arregloTokensEncontrados[indiceGeneral].tipo;
                cicloIntermedio.numeroRepeticiones = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tipoTokenActual, " ", "").Equals("Identificador"))
                {
                    cicloReemplazar += arregloTokensEncontrados[indiceGeneral].palabra;
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals(";"))
                {
                    cicloReemplazar += ";";
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tipoTokenActual = arregloTokensEncontrados[indiceGeneral].tipo;
                if (Regex.Replace(tipoTokenActual, " ", "").Equals("Identificador"))
                {
                    cicloReemplazar += arregloTokensEncontrados[indiceGeneral].palabra;
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("+"))
                {
                    cicloReemplazar += "+";
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("+"))
                {
                    cicloReemplazar += "+";
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals(")"))
                {
                    cicloReemplazar += ")";
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("{"))
                {
                    cicloReemplazar += "\n{";
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

                //Si todo resulto correcto retornara verdadero
                //CICLO REEMPLAZAR ES LA VARIABLE QUE SE VA A SUSTUIR

                cicloReemplazar += expresionesDentrodeCiclo + "}";
                DisplayAlert("Aviso", cicloReemplazar, "si", "no");
                cicloIntermedio.expresiones = expresionesDentrodeCiclo;
                expresionRepetida = "";
                for (int i = 0; i < int.Parse(cicloIntermedio.numeroRepeticiones); i++)
                {
                    expresionRepetida += expresionesDentrodeCiclo + "\n";
                }
                //txtResultado.Text = restante + expresionRepetida + "}";
                return true;
            }

            catch (Exception e)
            {

            }

            return false;
        }
    }
}