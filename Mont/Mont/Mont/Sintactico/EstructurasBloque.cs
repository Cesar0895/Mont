using System;
using System.Collections.Generic;
using System.Text;

namespace Mont.Sintactico
{
    public partial class SintacticoMain
    {
        bool bloquesExpresiones = true;

        bool intentoCiclo = false;
        bool intentoCondicion = false;
        bool intentoContrario = false;
        bool intentoMientras = false;
        bool intentoLlamadaFuncionNativa = false;

        public bool expresionesDeclaradas()
        {
            bloquesExpresiones = true;
            while (bloquesExpresiones)
            {
                intentoCiclo = detectoIntentoCiclo();
                if (intentoCiclo)
                {
                    bool cicloCorrecto = comprobarCiclo();
                    if (cicloCorrecto)
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: El ciclo se declaro correctamente\n";
                        return true;
                    }
                    else
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: El ciclo se declaro incorrectamente\n";
                        return false;
                    }
                }

                intentoCondicion = detectoIntentoCondicion();
                if (intentoCondicion)
                {
                    bool condicionCorrecta = comprobarCondicion();
                    if (condicionCorrecta)
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La condicion se declaro correctamente\n";
                        return true;
                    }
                    else
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La condicion se declaro incorrectamente\n";
                        return false;
                    }
                }

                intentoContrario = detectoIntentoContrario();
                if (intentoContrario)
                {
                    bool contrarioCorrecto = comprobarContrario();
                    if (contrarioCorrecto)
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La condicion contraria se declaro correctamente\n";
                        return true;
                    }
                    else
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La condicion contraria se declaro incorrectamente\n";
                        return false;
                    }
                }

                intentoMientras = detectoIntentoMientras();
                if (intentoMientras)
                {
                    bool mientrasCorrecto = comprobarMientras();
                    if (mientrasCorrecto)
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion mientras se declaro correctamente\n";
                        return true;
                    }
                    else
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion mientras se declaro incorrectamente\n";
                        return false;
                    }
                }

                intentoLlamadaFuncionNativa = detectoIntentoEncenderMotor();
                if (intentoLlamadaFuncionNativa)
                {
                    bool motorEncenderCorrecto = comprobarMotorEncender();
                    if (motorEncenderCorrecto)
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion motor encender se declaro correctamente\n";
                        return true;
                    }
                    else
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion motor encender se declaro incorrectamente\n";
                        return false;
                    }
                }

                intentoLlamadaFuncionNativa = detectoIntentoApagarMotor();
                if (intentoLlamadaFuncionNativa)
                {
                    bool motorApagarCorrecto = comprobarMotorApagar();
                    if (motorApagarCorrecto)
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion motor apagar se declaro correctamente\n";
                        return true;
                    }
                    else
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion motor apagar se declaro incorrectamente\n";
                        return false;
                    }
                }

                intentoLlamadaFuncionNativa = detectoIntentoMotorAvanzar();
                if (intentoLlamadaFuncionNativa)
                {
                    bool motorAvanzarCorrecto = comprobarMotorAvanzar();
                    if (motorAvanzarCorrecto)
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion motor avanzar se declaro correctamente\n";
                        return true;
                    }
                    else
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion motor avanzar se declaro incorrectamente\n";
                        return false;
                    }
                }

                intentoLlamadaFuncionNativa = detectoIntentoMotorReversa();
                if (intentoLlamadaFuncionNativa)
                {
                    bool motorReversaCorrecto = comprobarMotorReversa();
                    if (motorReversaCorrecto)
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion motor reversa se declaro correctamente\n";
                        return true;
                    }
                    else
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion motor reversa se declaro incorrectamente\n";
                        return false;
                    }
                }

                intentoLlamadaFuncionNativa = detectoIntentoFrenarMotor();
                if (intentoLlamadaFuncionNativa)
                {
                    bool motorFrenarCorrecto = comprobarMotorFrenar();
                    if (motorFrenarCorrecto)
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion frenar motor se declaro correctamente\n";
                        return true;
                    }
                    else
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion frenar motor reversa se declaro incorrectamente\n";
                        return false;
                    }
                }

                intentoLlamadaFuncionNativa = detectoIntentoGirarEje();
                if (intentoLlamadaFuncionNativa)
                {
                    bool motorFrenarCorrecto = comprobarEjeGirar();
                    if (motorFrenarCorrecto)
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion frenar motor se declaro correctamente\n";
                        return true;
                    }
                    else
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion frenar motor reversa se declaro incorrectamente\n";
                        return false;
                    }
                }

                intentoLlamadaFuncionNativa = detectoIntentoGirarEje();
                if (intentoLlamadaFuncionNativa)
                {
                    bool motorFrenarCorrecto = comprobarEjeGirar();
                    if (motorFrenarCorrecto)
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion girar eje se declaro correctamente\n";
                        return true;
                    }
                    else
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion girar eje  se declaro incorrectamente\n";
                        return false;
                    }
                }

                intentoLlamadaFuncionNativa = detectoIntentoCuchillaMover();
                if (intentoLlamadaFuncionNativa)
                {
                    bool motorFrenarCorrecto = comprobarMoverCuchilla();
                    if (motorFrenarCorrecto)
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion mover cuchilla se declaro correctamente\n";
                        return true;
                    }
                    else
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion mover cuchilla reversa se declaro incorrectamente\n";
                        return false;
                    }
                }

                intentoLlamadaFuncionNativa = detectoIntentoCuchillaInclinar();
                if (intentoLlamadaFuncionNativa)
                {
                    bool motorFrenarCorrecto = comprobarInclinarCuchilla();
                    if (motorFrenarCorrecto)
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion inclinar cuchilla se declaro correctamente\n";
                        return true;
                    }
                    else
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion inclinar cuchilla reversa se declaro incorrectamente\n";
                        return false;
                    }
                }

                bloquesExpresiones = false;
            }
            return false;
        }
        public bool comprobarEstructurasBloques()
        { 
            intentoCiclo = detectoIntentoCiclo();
            if (intentoCiclo)
            {
                bool cicloCorrecto = comprobarCiclo();
                if (cicloCorrecto)
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: El ciclo se declaro correctamente\n";
                }
                else
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: El ciclo se declaro incorrectamente\n";
                    return false;
                }
            }

            intentoCondicion = detectoIntentoCondicion();
            if (intentoCondicion)
            {
                bool condicionCorrecta = comprobarCondicion();
                if (condicionCorrecta)
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La condicion se declaro correctamente\n";
                }
                else
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La condicion se declaro incorrectamente\n";
                    return false;
                }
            }

            intentoContrario = detectoIntentoContrario();
            if (intentoContrario)
            {
                bool contrarioCorrecto = comprobarContrario();
                if (contrarioCorrecto)
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La condicion contraria se declaro correctamente\n";
                }
                else
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La condicion contraria se declaro incorrectamente\n";
                    return false;
                }
            }

            intentoMientras = detectoIntentoMientras();
            if (intentoMientras)
            {
                bool mientrasCorrecto = comprobarMientras();
                if (mientrasCorrecto)
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion mientras se declaro correctamente\n";
                }
                else
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion mientras se declaro incorrectamente\n";
                    return false;
                }
            }

            intentoLlamadaFuncionNativa = detectoIntentoEncenderMotor();
            if (intentoLlamadaFuncionNativa)
            {
                bool motorEncenderCorrecto = comprobarMotorEncender();
                if (motorEncenderCorrecto)
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion motor encender se declaro correctamente\n";
                }
                else
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion motor encender se declaro incorrectamente\n";
                    return false;
                }
            }

            intentoLlamadaFuncionNativa = detectoIntentoApagarMotor();
            if (intentoLlamadaFuncionNativa)
            {
                bool motorApagarCorrecto = comprobarMotorApagar();
                if (motorApagarCorrecto)
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion motor apagar se declaro correctamente\n";
                }
                else
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion motor apagar se declaro incorrectamente\n";
                    return false;
                }
            }

            intentoLlamadaFuncionNativa = detectoIntentoMotorAvanzar();
            if (intentoLlamadaFuncionNativa)
            {
                bool motorAvanzarCorrecto = comprobarMotorAvanzar();
                if (motorAvanzarCorrecto)
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion motor avanzar se declaro correctamente\n";
                }
                else
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion motor avanzar se declaro incorrectamente\n";
                    return false;
                }
            }

            intentoLlamadaFuncionNativa = detectoIntentoMotorReversa();
            if (intentoLlamadaFuncionNativa)
            {
                bool motorReversaCorrecto = comprobarMotorReversa();
                if (motorReversaCorrecto)
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion motor reversa se declaro correctamente\n";
                }
                else
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion motor reversa se declaro incorrectamente\n";
                    return false;
                }
            }

            intentoLlamadaFuncionNativa = detectoIntentoFrenarMotor();
            if (intentoLlamadaFuncionNativa)
            {
                bool motorFrenarCorrecto = comprobarMotorFrenar();
                if (motorFrenarCorrecto)
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion frenar motor se declaro correctamente\n";
                }
                else
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion frenar motor reversa se declaro incorrectamente\n";
                    return false;
                }
            }

            intentoLlamadaFuncionNativa = detectoIntentoGirarEje();
            if (intentoLlamadaFuncionNativa)
            {
                bool motorFrenarCorrecto = comprobarEjeGirar();
                if (motorFrenarCorrecto)
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion frenar motor se declaro correctamente\n";
                }
                else
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion frenar motor reversa se declaro incorrectamente\n";
                    return false;
                }
            }

            intentoLlamadaFuncionNativa = detectoIntentoGirarEje();
            if (intentoLlamadaFuncionNativa)
            {
                bool motorFrenarCorrecto = comprobarEjeGirar();
                if (motorFrenarCorrecto)
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion girar eje se declaro correctamente\n";
                }
                else
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion girar eje  se declaro incorrectamente\n";
                    return false;
                }
            }

            intentoLlamadaFuncionNativa = detectoIntentoCuchillaMover();
            if (intentoLlamadaFuncionNativa)
            {
                bool motorFrenarCorrecto = comprobarMoverCuchilla();
                if (motorFrenarCorrecto)
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion mover cuchilla se declaro correctamente\n";
                }
                else
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion mover cuchilla reversa se declaro incorrectamente\n";
                    return false;
                }
            }

            intentoLlamadaFuncionNativa = detectoIntentoCuchillaInclinar();
            if (intentoLlamadaFuncionNativa)
            {
                bool motorFrenarCorrecto = comprobarInclinarCuchilla();
                if (motorFrenarCorrecto)
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion inclinar cuchilla se declaro correctamente\n";
                }
                else
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La expresion inclinar cuchilla reversa se declaro incorrectamente\n";
                    return false;
                }
            }

            return true;
        }
    }
}