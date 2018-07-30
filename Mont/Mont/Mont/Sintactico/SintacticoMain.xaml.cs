using Mont.Semantico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mont.Sintactico
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SintacticoMain : ContentPage
	{
        string expresionRepetida = "";
        string resultadoGeneral = "";
        string tokenActual = "";
        string tipoTokenActual = "";
        string bitacoraAutomata;
        int indiceGeneral = 0;
        string cicloReemplazar = "";
        string expresionesDentrodeCiclo ="";
        string cadenaTexto = "";

        List<PalabraEncontrada> arregloTokensEncontrados;
        List<VariableDeclarada> arregloVariablesEncontradas=new List<VariableDeclarada>();
        List<NombresFunciones> arregloNombresFunciones = new List<NombresFunciones>();
        List<ParametrosFuncionNativa> arregloParametrosFuncion = new List<ParametrosFuncionNativa>();
        List<MotorOrdenEjecucion> arregloOrdenEjecucion = new List<MotorOrdenEjecucion>();


        public SintacticoMain(List<PalabraEncontrada> arregloTokensEncontrados, string cadenaTexto)
        {
            InitializeComponent();
            this.arregloTokensEncontrados = arregloTokensEncontrados;
            comprobarEstructuraGeneral();
            txtResultado.Text = resultadoGeneral;
            btnCompilar.Clicked += BtnCompilar_Clicked;
            btnOptimizar.Clicked += BtnOptimizar_Clicked;
            btnGenerar.Clicked += BtnGenerar_Clicked;
            this.cadenaTexto = cadenaTexto;
        }

        private void BtnGenerar_Clicked(object sender, EventArgs e)
        {
            string ejecucionFinal = "";

            ejecucionFinal+= expresionRepetida.Replace("Eje.girar(1);", "GirarDer();\n delay(500);");
            ejecucionFinal += expresionRepetida.Replace( "Eje.girar(2);", "GirarIzq(); \n delay(500);");
            ejecucionFinal += expresionRepetida.Replace("Motor.reversa(1);", "MotorReversa(); \n delay(3000);");
            ejecucionFinal += expresionRepetida.Replace( "Motor.avanzar(1);", "MotorAvanzar();\n delay(3000);");
            ejecucionFinal += expresionRepetida.Replace( "Motor.frenar();", "MotorFrenar(); \n delay(2000);");
            ejecucionFinal += expresionRepetida.Replace( "Cuchilla.mover(0);", "CuchillaBajar();\n delay(1000);");
            ejecucionFinal += expresionRepetida.Replace( "Cuchilla.mover(1);", "CuchillaSubir();\n delay(1000);");
            ejecucionFinal += expresionRepetida.Replace( "Cuchilla.inclinar(false);", "CuchillaReInclinar(); \n delay(1000);");
            ejecucionFinal += expresionRepetida.Replace("Cuchilla.inclinar(true);", "CuchillaInclinar();\n delay(1000);");

            string codigoReemplazado=codigoFuente.Replace("{reemplazar}", ejecucionFinal);
            txtResultado.Text = codigoReemplazado;
        }

        private void BtnOptimizar_Clicked(object sender, EventArgs e)
        {
            txtResultado.Text = restante + expresionRepetida + "}";
        }

        private void BtnCompilar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SemanticoMain(this.arregloVariablesEncontradas, 
                                                    this.arregloTokensEncontrados,
                                                    this.arregloNombresFunciones,
                                                    this.arregloParametrosFuncion,
                                                    this.arregloOrdenEjecucion));
        }

        public void comprobarEstructuraGeneral()
        {
            bitacoraAutomata += "Comprobando el automata GENERAL\n";

            //validar nombre del programa
            bitacoraAutomata += "Comenzando por comprobar que el automata de NOMBRE DEL PROGRAMA se complete correctamente\n";
            if (comprobarNombrePrograma())
            {
                int lineaResultado = arregloTokensEncontrados[indiceGeneral-1].nLinea;
                resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: El nombre del programa se declaro correctamente\n";
                bitacoraAutomata += "El automata de NOMBRE DEL PROGRAMA se completo correctamente\n";
            }
            else
            {
                int lineaResultado = arregloTokensEncontrados[indiceGeneral-1].nLinea;
                resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: El nombre del programa se declaro incorrectamente\n";
                bitacoraAutomata += "El automata de NOMBRE DEL PROGRAMA no se completo\n";
                return;
            }

            //Es necesario que al menos una de las 3 declaraciones sea escrita
            //configuracion, declaracion de variables globales o el inicio de la maniobra
            bitacoraAutomata += "El automata GENERAL puede tomar 3 caminos (CONFIGURACION, DECLARACION DE VARIABLES o INICIO DE MANIOBRA)\n";

            //comienza por comprobar si hubo una configuracion
            bitacoraAutomata += "Se comenzara comprobando que se intenta acceder al automata de CONFIGURACION\n";
            bool intentoConfiguracion = detectoIntentoConfiguracion();
            //si es que hubo un intento de configuracion 
            if (intentoConfiguracion)
            {
                bitacoraAutomata += "Se intenta acceder al automata de CONFIGURACION\n";
                bitacoraAutomata += "Se comprobara que el automata de CONFIGURACION se complete correctamente\n";

                //se comienza a someter el automata de configuracion
                if (comprobarConfiguracion())
                {
                    //la linea puede ser 
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral-1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La configuracion se declaro correctamente\n";
                    bitacoraAutomata += "El automata de CONFIGURACION se completo correctamente\n";
                }
                else
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral-1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La configuracion se declaro incorrectamente\n";
                    bitacoraAutomata += "El automata de CONFIGURACION no se completo\n";
                    return;
                }
            }

            //despues comprueba si se declararon variables
            bitacoraAutomata += "Se comprobara que se intenta acceder al automata de DECLARACION DE VARIABLES\n";
            bool intentoVariableGlobal = detectoIntentoVariablesGlobales();
            //si es que hubo un intento de declaracion de variables
         
            if (intentoVariableGlobal)
            {
                do
                {
                    bitacoraAutomata += "Se intenta acceder al automata de DECLARACION DE VARIABLES\n";
                    bitacoraAutomata += "Se comprobara que el automata de DECLARACION DE VARIABLES se complete correctamente\n";

                    //se comienza a someter el automata de declaracion de variables
                    if (comprobarVariablesGlobales())
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: Las variables se declararon correctamente\n";
                        bitacoraAutomata += "El automata de DECLARACION DE VARIABLES se completo correctamente\n";
                    }
                    else
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: Las variables se declararon incorrectamente\n";
                        bitacoraAutomata += "El automata de DECLARACION DE VARIABLES no se completo\n";
                        return;
                    }
                }
                while (detectoIntentoVariablesGlobales());
            }

            //despues comprueba si se declaro el inicio de la maniobra
            bitacoraAutomata += "Se comprobara que se intenta acceder al automata de INICIO DE MANIOBRA\n";
            bool intentoManiobra = detectoIntentoManiobra();
            //si es que hubo un intento de inicio de maniobra
            if (intentoManiobra)
            {
                bitacoraAutomata += "Se intenta acceder al automata de INICIO DE MANIOBRA\n";
                bitacoraAutomata += "Se comprobara que el automata de INICIO DE MANIOBRA se complete correctamente\n";

                //se comienza a someter el automata de inicio de maniobra
                if (comprobarManiobra())
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral-1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: El inicio de maniobra se declaro correctamente\n";
                    bitacoraAutomata += "El automata de INICIO DE MANIOBRA se completo correctamente\n";
                }
                else
                {
                    int lineaResultado = arregloTokensEncontrados[indiceGeneral-1].nLinea;
                    resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: El inicio de maniobra se declaro incorrectamente\n";
                    bitacoraAutomata += "El automata de INICIO DE MANIOBRA no se completo\n";
                    return;
                }
            }

            //despues comprueba si se declararon funciones vacias
            bitacoraAutomata += "Se comprobara que se intenta acceder al automata de DECLARACION DE FUNCIONES VACIAS Y QUE NO RETORNEN\n";
            bool intentofuncionVaciaNoRetorna = detectoIntentofuncionVaciaNoRetorna();
            //si es que hubo un intento de declaracion de funciones vacias

            if (intentofuncionVaciaNoRetorna)
            {
                do
                {
                    bitacoraAutomata += "Se intenta acceder al automata de DECLARACION DE FUNCIONES VACIAS Y QUE NO RETORNEN\n";
                    bitacoraAutomata += "Se comprobara que el automata de DECLARACION DE FUNCIONES VACIAS Y QUE NO RETORNEN se complete correctamente\n";

                    //se comienza a someter el automata de declaracion de variables
                    if (comprobarFuncionVaciaNoRetorna())
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje: La funcion se declaro correctamente\n";
                        bitacoraAutomata += "El automata de DECLARACION DE FUNCIONES VACIAS Y QUE NO RETORNEN se completo correctamente\n";
                    }
                    else
                    {
                        int lineaResultado = arregloTokensEncontrados[indiceGeneral - 1].nLinea;
                        resultadoGeneral += "Linea:" + lineaResultado + "\nMensaje:  La funcion se declaro incorrectamente\n";
                        bitacoraAutomata += "El automata de DECLARACION DE FUNCIONES VACIAS Y QUE NO RETORNEN no se completo\n";
                        return;
                    }
                }
                while (detectoIntentofuncionVaciaNoRetorna());
            }
        }

        public bool comprobarNombrePrograma()
        {
            try
            {
                //Primero debe estar Mont
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if(Regex.Replace(tokenActual, " ", "").Equals("Mont"))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Despues un identificador
                tipoTokenActual = arregloTokensEncontrados[indiceGeneral].tipo;
                if (tipoTokenActual.Equals("Identificador"))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

               

                //Si todo resulto correcto retornara verdadero
                return true;
            }

            catch (Exception e)
            {

            }

            return false;
        }

        public bool detectoIntentoConfiguracion()
        {
            try
            {
                //Detectar si lo que se lee es una configuracion
                //comprobando que este la palabra "configuracion"
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("configuracion"))
                {
                    //notifico que si existe un intento de configuracion
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

        public bool comprobarConfiguracion()
        {
            try
            {
                //Primero debe estar configuracion
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("configuracion"))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Despues un parentesis abierto
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("("))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Despues un parentesis cerrado
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals(")"))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Despues una llave abierto
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("{"))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Despues un corchete que abre
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("["))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Despues un corchete que cierra
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("]"))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Por ultimo un parentesis que cierra
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
                return true;
            }

            catch (Exception e)
            {

            }

            return false;
        }

        public bool detectoIntentoVariablesGlobales()
        {
            try
            {
                //Detectar si lo que se lee es una declaracion de variables
                //comprobando que este algun "tipo de dato"
                tipoTokenActual = arregloTokensEncontrados[indiceGeneral].tipo;
                if (tipoTokenActual.Equals("ClaseTipoDato"))
                {
                    //notifico que si existe un intento de declaracion de variables
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

        public bool comprobarVariablesGlobales()
        {
            try
            {
                //crear objeto de variable encontrada
                VariableDeclarada variableEncontrada= new VariableDeclarada();

                //numero linea y columna del objeto variable encontrada
                variableEncontrada.linea = arregloTokensEncontrados[indiceGeneral].nLinea;
                variableEncontrada.columna = arregloTokensEncontrados[indiceGeneral].indice;

                //Primero debe haber un tipo de dato
                tipoTokenActual = arregloTokensEncontrados[indiceGeneral].tipo;
                if (tipoTokenActual.Equals("ClaseTipoDato"))
                {
                    variableEncontrada.tipoDato = arregloTokensEncontrados[indiceGeneral].palabra;
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Despues un identificador
                tipoTokenActual = arregloTokensEncontrados[indiceGeneral].tipo;
                if (tipoTokenActual.Equals("Identificador"))
                {
                    variableEncontrada.nombreVariable = arregloTokensEncontrados[indiceGeneral].palabra;
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Despues puede haber un punto y coma y finalizar
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals(";"))
                {
                    indiceGeneral++;
                    return true;
                }
                //O puede haber un signo = y tambien ser valido aqui la validacion continua
                else if (Regex.Replace(tokenActual, " ", "").Equals("="))
                {
                    indiceGeneral++;
                }
                //si no es ni ; ni = marca error
                else
                {
                    return false;
                }

                //A partir de aqui continua solo si hubo antes un signo =

                //Despues un valor ya sea cadena booleano entero o flotante
                tipoTokenActual = arregloTokensEncontrados[indiceGeneral].tipo;
                if (tipoTokenActual.Equals("Identificador")
                    || tipoTokenActual.Equals("Cadena"))
                {
                    variableEncontrada.valor = arregloTokensEncontrados[indiceGeneral].palabra;
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Por ultimo un ;
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals(";"))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }


                //Si todo resulto correcto retornara verdadero
                arregloVariablesEncontradas.Add(variableEncontrada);
                return true;
            }

            catch (Exception e)
            {

            }

            return false;
        }

        public bool detectoIntentoManiobra()
        {
            try
            {
                //Detectar si lo que se lee es una maniobra
                //comprobando que este la palabra "Maniobra"
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("Maniobra"))
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

        public bool comprobarManiobra()
        {
            try
            {
                //Primero debe estar Maniobra
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("Maniobra"))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Despues un .
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("."))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Despues iniciar
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("iniciar"))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Despues un parentesis abierto
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("("))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Despues un parentesis cerrado
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals(")"))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Despues una llave abierta
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

                //Despues una llave cerrada
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
                return true;
            }

            catch (Exception e)
            {

            }

            return false;
        }


        public bool detectoIntentofuncionVaciaNoRetorna()
        {
            try
            {
                //Detectar si lo que se lee es una funcion vacia que no retorne
                //comprobando que este la palabra "funcion"
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("funcion"))
                {
                   //comprobar que este la palabra noRetorna
                    tokenActual = arregloTokensEncontrados[indiceGeneral + 1].palabra;
                    if (Regex.Replace(tokenActual, " ", "").Equals("noRetorna"))
                    {
                        //notifico que si existe un intento de maniobra
                        return true;
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

        public bool comprobarFuncionVaciaNoRetorna()
        {
            NombresFunciones nombresFunciones = new NombresFunciones();
            nombresFunciones.linea = arregloTokensEncontrados[indiceGeneral].nLinea;
            try
            {
                //Primero debe estar funcion
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("funcion"))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Despues noRetorna
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("noRetorna"))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Despues un identificador
                tipoTokenActual = arregloTokensEncontrados[indiceGeneral].tipo;
                if (Regex.Replace(tipoTokenActual, " ", "").Equals("Identificador"))
                {
                    nombresFunciones.nombreFuncion= arregloTokensEncontrados[indiceGeneral].palabra;
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Despues un parentesis abierto
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("("))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Despues un parentesis cerrado
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals(")"))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //Despues una llave abierta
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
                while(expresionesDeclaradas())
                {
                    expresionesDeclaradas();
                }
              
                //Despues una llave cerrada
                tokenActual = arregloTokensEncontrados[indiceGeneral].palabra;
                if (Regex.Replace(tokenActual, " ", "").Equals("}"))
                {
                    indiceGeneral++;
                }
                else
                {
                    return false;
                }

                //agregar elemento a nombre funciones
                arregloNombresFunciones.Add(nombresFunciones);
                //Si todo resulto correcto retornara verdadero
                return true;
            }

            catch (Exception e)
            {

            }

            return false;
        }
    }
}