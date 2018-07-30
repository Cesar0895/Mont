using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Mont.Lexico;
using Mont.Sintactico;

namespace Mont
{
	public partial class MainPage : ContentPage
	{
        int contadorNoMatch = 0;
        String otrosErrores = "";
        String cadenaTexto = "";
        String[,] arreglolineasTexto;
        String[,] arregloPalabrasReservadas = new String[,] {

            { "Mont", "Palabra reservada", "Palabra que indica el inicio de una programa",@"Mont(\s+)","0"},
           
            { "noRetorna", "Palabra reservada", "Palabra que indica que una función no retorna ningún valor",@"noRetorna(\s+)","0"},
           
            { "funcion", "Palabra reservada", "Palabra que indica la declaración de una función",@"funcion(\s+)", "0" },
           
            {"configuracion","Palabra reservada","Palabra que indica la declaración de la configuración del montacargas",@"configuracion","1"},

            {"si","Palabra reservada","Palabra que indica una condicion bifurcada",@"si","1"},

            {"mientras","Palabra reservada","Palabra que indica un ciclo condicionado",@"mientras","1"},

            {"ciclo","Palabra reservada","Palabra que indica un ciclo iterado",@"ciclo","1"},

            {"consola","Palabra reservada","Palabra que indica la escritura de un mensaje en consola",@"consola","1"},

            {"iniciar","Funcion nativa","Función de la clase Maniobra encargada de iniciar las maniobras",@"iniciar","2"},

            {"encender","Funcion nativa","Función de la clase Motor la cual se encarga de encender el motor",@"encender","2"},

            {"apagar","Palabra reservada","Función de la clase Motor la cual se encarga de apagar el motor",@"apagar","2"},

            {"avanzar","Funcion nativa","Función de la clase Motor la cual se encarga de accionar el motor, hasta que el montacargas recorra cierta distancia en metros. toma un parámetro del tipo flotante de 0 a n ",@"avanzar","2"},

            {"reversa","Funcion nativa","Función de la clase Motor la cual se encarga de accionar el motor, hasta que el montacargas recorra cierta distancia hacia atrás en metros. toma un parámetro del tipo flotante de 0 a n",@"reversa","2"},

            {"frenar","Funcion nativa","Función de la clase Motor la cual se encarga de detener el motor. Haciendo que el montacargas también se detenga",@"frenar","2"},

            {"mover","Funcion nativa","Función de la clase Cuchilla la cual mueve las cuchillas en vertical porcentualmente.",@"mover","2"},

            {"abrir","Funcion nativa","Función de la clase Cuchilla la cual controla la apertura de las cuchillas porcentualmente. Toma un parámetro flotante de 0 a 100",@"abrir","2"},

            {"inclinar","Funcion nativa","Función de la clase Cuchilla la cual controla la ",@"inclinar","2"},

            {"Maniobra","Clase","Clase para administrar la ejecución de las maniobras del montacargas",@"Maniobra","3"},

            {"Cuchilla","Clase","Clase que contiene funciones y propiedades de la cuchilla",@"Cuchilla","3"},

            {"Motor","Clase","Clase que contiene funciones y propiedades del motor",@"Motor","3"},

             {"Eje","Clase","Clase que contiene funciones y propiedades del motor",@"Eje","3"},

            {"entero","ClaseTipoDato","Clase del tipo de datos entero, contiene todas sus propiedades y funciones",@"entero","3"},

            {"flotante","ClaseTipoDato","Clase del tipo de datos flotante, contiene todas sus propiedades y funciones",@"flotante","3"},

            {"cadena","ClaseTipoDato","Clase del tipo de datos cadena, contiene todas sus propiedades y funciones ",@"cadena","3"},

            {"booleano","ClaseTipoDato","Clase del tipo de datos booleano, contiene todas sus propiedades y funciones",@"booleano","3"},

            {"ESTADO_MOTOR","Propiedad nativa","Propiedad cadena de la clase Motor, que contiene el estado actual del motor",@"ESTADO_MOTOR","4"},

            {"DISTANCIA_ESTABLECIDA","Propiedad nativa","Propiedad flotante de la clase Motor, que contiene la distancia que el desarrollador le establece al motor para recorrer",@"DISTANCIA_ESTABLECIDA","4"},

            {"DISTANCIA_ESTABLECIDA_REVERSA","Propiedad nativa","Propiedad flotante de la clase Motor, que contiene la distancia en reversa que el desarrollador le establece al motor para recorrer",@"DISTANCIA_ESTABLECIDA_REVERSA","4"},

            {"DISTANCIA_RECORRIDA","Propiedad nativa","Propiedad flotante de la clase Motor, que contiene la distancia recorrida desde el inicio de la instrucción",@"DISTANCIA_RECORRIDA","4"},

            {"ESTADO_INCLINACION","Propiedad nativa","Propiedad booleana de la clase Cuchilla la cual contiene el estado de inclinación de las cuchillas",@"ESTADO_INCLINACION","4"},

            {"APERTURA_ESTABLECIDA","Propiedad nativa","Propiedad flotante de la clase Cuchilla la cual contiene la apertura establecida de las cuchillas",@"APERTURA_ESTABLECIDA","4"},

            {"APERTURA_AVANCE","Propiedad nativa","Propiedad flotante de la clase Cuchilla la cual contiene la apertura actual de las cuchillas desde que se inició la instrucción",@"APERTURA_AVANCE","4"},

            {"MOVIMIENTO_ESTABLECIDO","Propiedad nativa","Propiedad flotante de la clase Cuchilla la cual contiene el movimiento establecido en vertical de las cuchillas",@"MOVIMIENTO_ESTABLECIDO","4"},

            {"MOVIMIENTO_AVANCE","Propiedad nativa","Propiedad flotante de la clase Cuchilla la cual contiene el movimiento actual en vertical de las cuchillas desde que se inició la instrucción",@"MOVIMIENTO_AVANCE","4"},
            
            {"(","simbolo","Parentesis abierto",@"\(","5"},
            
            {")","simbolo","Parentesis cerrado",@"\)","5"},
            
            {"-","simbolo","Operador resta",@"\-","5"},
            
            {"+","simbolo","Operador suma",@"\+","5"},
            
            {"*","simbolo","Operador multiplicacion",@"\*","5"},
            
            {"/","simbolo","Operador division",@"\/","5"},
            
            {"&","simbolo","Amperson",@"\&","5"},
            
            {"=","simbolo","Igual",@"\=","5"},
            
            {".","simbolo","Punto",@"\.","5"},

            {";","simbolo","Punto y coma",@"\;","5"},

            {"{","simbolo","Llave abierta",@"\{","5"},
            
            {"}","simbolo","Llave cerrada",@"\}","5"},
            
            {"[","simbolo","Corchete abierto",@"\[","5"},
            
            {"]","simbolo","Corchete cerrado",@"\]","5"},

             {"<","simbolo","Corchete abierto",@"\<","5"},

            {">","simbolo","Corchete cerrado",@"\>","5"},

            { "Cadena de texto", "Cadena", "Cadena de texto",@"(['])(?:(?=(\\?))\2.)*?\1","7"},

            { "Identificador", "Identificador", "Nombre de una variable, funcion o propiedad",@"\w+","6"}
        };
        String mensajeLexico = "";

        String caracteresValidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+-*/&|!<>=()[]{}.,_;' \n";

        List<PalabraEncontrada> listaPalabrasEncontradas = new List<PalabraEncontrada>();
        List<PalabraError> listaErroresEncontrados = new List<PalabraError>();

        public MainPage()
		{
			InitializeComponent();
            btnCompilar.Clicked += btnCompilar_Clicked;
            btnSintactico.Clicked += BtnSintactico_Clicked;
        }

        private void BtnSintactico_Clicked(object sender, EventArgs e)
        {
            cadenaTexto = txtEditor.Text;
            Navigation.PushAsync(new SintacticoMain(listaPalabrasEncontradas, cadenaTexto));
        }

        private void btnCompilar_Clicked(object sender, EventArgs e)
        {
            analizarLexico();
        }

        public void analizarLexico()
        {
            //Limpiar las listas de errores y palabras encontradas
            listaPalabrasEncontradas.Clear();
            listaErroresEncontrados.Clear();
            //Lista temporal donde se guardaran los resultados por linea
            List<PalabraEncontrada> listaPalabrasEncontradaTemporal = new List<PalabraEncontrada>();
            //Lista temporal donde se guardaran los resultados por linea ya ordenadas
            List<PalabraEncontrada> listaPalabrasEncontradaPorLineaOrdenada;
            //Lista temporal donde se guardaran los resultados por linea ya ordenadas
            List<PalabraError> listaErroresTemporal;

            //Tomar cadena de texto
            cadenaTexto = txtEditor.Text;
            //Descomponer cadena en saltos de linea 
            String[] arregloLineas = cadenaTexto.Split(new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None);
            //Inicializar el arreglo de lineas de texto
            arreglolineasTexto = new String[arregloLineas.Length, 2];
            //Agregar cada linea a un arreglo multidimensional
            for (int i = 0; i < arregloLineas.Length; i++)
            {
                //cambio mas reciente
                string lineaActual = arregloLineas[i].TrimStart();
                //DisplayAlert("Aviso",lineaActual, "Si", "No");
                int numeroLinea = i + 1;
                arreglolineasTexto[i, 0] = numeroLinea.ToString();
                arreglolineasTexto[i, 1] = " "+lineaActual+" ";
            }

            //Por cada linea someterla a todas las palabras reservadas
            for (int i = 0; i < arreglolineasTexto.GetLength(0); i++)
            {
                //Limpiar la lista de palabras temporales
                listaPalabrasEncontradaTemporal.Clear();

                String cadenaTexto = arreglolineasTexto[i, 1];
                //DisplayAlert("Aviso","Evaluando"+cadenaTexto,"si","no");
                //DisplayAlert("Aviso", cadenaTexto,"si","no");
                for (int j = 0; j < arregloPalabrasReservadas.GetLength(0); j++)
                {
                    //tomo su expresion regular
                    Regex rx = new Regex(arregloPalabrasReservadas[j, 3]);
                    //DisplayAlert("Aviso", "Evaluando "+cadenaTexto+" con " + rx, "si", "no");
                    //ciclo foreach para recorrer la cadena en busca de 1 o mas coincidencias

                    //SI DESPUES DE HABER RECORRIDO TODAS LAS PALABRAS Y NO HIZO MATCH
                    //LA LINEA SE TOMA COMO ERROR

                    if (rx.IsMatch(cadenaTexto)==false)
                    {
                        contadorNoMatch++;
                    }
                    if (contadorNoMatch==arregloPalabrasReservadas.GetLength(0))
                    {
                        agregarError(cadenaTexto,i+1);
                    }
                    foreach (Match validacionregex in rx.Matches(cadenaTexto))
                    {
                        //DisplayAlert("Aviso", validacionregex.Value+" TIPO:"+ arregloPalabrasReservadas[j, 0], "si", "no");
                        //Cada que encuentre una pasarla a comprobar
                        if (comprobarToken(j, validacionregex, i))
                        {
                            //Por cada palabra encontrada crear un objeto temporal
                            PalabraEncontrada palabraReservada = new PalabraEncontrada();
                            palabraReservada.palabra = validacionregex.Value;
                            //palabraReservada.palabra = arregloPalabrasReservadas[j, 0];
                            palabraReservada.tipo = arregloPalabrasReservadas[j, 1];
                            palabraReservada.descripcion = arregloPalabrasReservadas[j, 2];
                            palabraReservada.indice = validacionregex.Index;
                            palabraReservada.nLinea = int.Parse(arreglolineasTexto[i, 0]);

                            //Y agregarlo a listaPalabrasEncontradaTemporal
                            listaPalabrasEncontradaTemporal.Add(palabraReservada);
                        }

                    }
                }
                //Cada que termina de someter una linea de texto a las palabras reservadas
                //Si encontro algunas hay que ordenarlas
                listaPalabrasEncontradaPorLineaOrdenada = ordenarPalabrasEncontradasPorLinea(listaPalabrasEncontradaTemporal);
                //Una vez ordenada la lista encontrar las palabras que no correspondieron a ninguna palabra reservada
                listaErroresTemporal = encontrarErroresPorLinea(listaPalabrasEncontradaPorLineaOrdenada, arreglolineasTexto[i, 1], i+1);

                //Agregar palabras encontradas y errores a sus respectivas listas 
                listaPalabrasEncontradas.AddRange(listaPalabrasEncontradaPorLineaOrdenada);
                listaErroresEncontrados.AddRange(listaErroresTemporal);
            }

            //Por ultimo mostrar los resultados
            mostrarPalabrasEncontradas(listaPalabrasEncontradas);
            mostrarErroresEncontradas(listaErroresEncontrados);
        }

        public bool comprobarToken(int indicePalabraArreglo, Match validacionRegex, int indiceLineaArreglo)
        {
            //Tomar el indice de la coincidencia encontrada
            var indicePalabraReservada = validacionRegex.Index;
            //obtener el tipo de palabra reservada (aislada,parametro)
            var tipoPalabraReservada = arregloPalabrasReservadas[indicePalabraArreglo, 4];

            switch (tipoPalabraReservada)
            {
                //palabra aislada
                case "0":
                    //DisplayAlert("Aviso","Se trata de una palabra aislada","si","no");
                    if (comprobarPalabraAislada(validacionRegex, indicePalabraReservada, indiceLineaArreglo))
                    {
                        return true;
                    }
                    break;
                //palabra parametro
                case "1":
                    if (comprobarPalabraParametro(validacionRegex, indicePalabraReservada, indicePalabraArreglo, indiceLineaArreglo))
                    {
                        return true;
                    }
                    break;

                //palabra funcion nativa con punto antes opcional y parentesis despues opcional
                case "2":
                    if (comprobarPalabraFuncionNativa(validacionRegex, indicePalabraReservada, indicePalabraArreglo, indiceLineaArreglo))
                    {
                        return true;
                    }
                    break;

                //palabra clase con espacio en blanco antes opcional y con punto final opcional
                case "3":
                    if (comprobarPalabraClase(validacionRegex, indicePalabraReservada, indicePalabraArreglo, indiceLineaArreglo))
                    {
                        return true;
                    }
                    break;

                //palabra propiedad con punto detras opcional o espacio en blanco 
                //y operadores logicos y de asignacion o espacio en blanco final opcional
                case "4":
                    if (comprobarPalabraPropiedad(validacionRegex, indicePalabraReservada, indicePalabraArreglo, indiceLineaArreglo))
                    {
                        return true;
                    }
                    break;

                case "5":
                    if (comprobarPalabraSimbolo(validacionRegex, indicePalabraReservada, indicePalabraArreglo, indiceLineaArreglo))
                    {
                        return true;
                    }
                    break;

                case "6":
                    if (comprobarPalabraIdentificador(validacionRegex, indicePalabraReservada, indicePalabraArreglo, indiceLineaArreglo))
                    {
                        return true;
                    }
                    break;

                case "7":
                    if (comprobarCadena(validacionRegex, indicePalabraReservada, indicePalabraArreglo, indiceLineaArreglo))
                    {
                        return true;
                    }
                    break;

                default:
                   
                    break;
            }
            return false;
        }

        public bool comprobarPalabraAislada(Match validacionRegex, int indicePalabraReservada, int indiceLineaArreglo)
        {
            string cadenaTexto = arreglolineasTexto[indiceLineaArreglo, 1];
            //DisplayAlert("Aviso",cadenaTexto,"si","no");
            //validar que la palabra este aislada  por la parte de atras 
            if ((cadenaTexto[indicePalabraReservada - 1].ToString()).Equals(" "))
            {
                //DisplayAlert("Aviso", "la palabra "+validacionRegex.Value+" se encuentra aislada y lista para agregarse a la tabla", "si","no");
                return true;
            }
            return false;
        }

        public bool  comprobarPalabraParametro(Match validacionRegex, int indicePalabraReservada,int indicePalabraArreglo, int indiceLineaArreglo)
        {
            int longitudPalabraArreglo = arregloPalabrasReservadas[indicePalabraArreglo, 0].Length;
            string cadenaTexto = arreglolineasTexto[indiceLineaArreglo, 1];
            //Validar que detras de la palabra haya un espacio en blanco
            if ((cadenaTexto[indicePalabraReservada - 1].ToString()).Equals(" "))
            {
                //Despues validar que despues de la palabra haya un espacio en blanco o un parentesis (
                if ((cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals(" ") || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("("))
                {
                    //DisplayAlert("Aviso", "la palabra " + validacionRegex.Value + " se encuentra aislada y lista para agregarse a la tabla", "si", "no");
                    return true;
                }
            }
            return false;
        }

        public bool  comprobarPalabraFuncionNativa(Match validacionRegex, int indicePalabraReservada, int indicePalabraArreglo, int indiceLineaArreglo)
        {
            int longitudPalabraArreglo = arregloPalabrasReservadas[indicePalabraArreglo, 0].Length;
            string cadenaTexto = arreglolineasTexto[indiceLineaArreglo, 1];

            //Validar que antes de la palabra haya un espacio en blanco o un punto
            if ((cadenaTexto[indicePalabraReservada - 1].ToString()).Equals(" ") || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("."))
            {
                //Validar que despues de la palabra haya un espacio en blanco o un parentesis
                if ((cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals(" ") || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("("))
                {
                   // DisplayAlert("Aviso", "la palabra " + validacionRegex.Value + " se encuentra aislada y lista para agregarse a la tabla", "si", "no");
                    return true;
                }
            }
            return false;
        }

        public bool comprobarPalabraClase(Match validacionRegex, int indicePalabraReservada, int indicePalabraArreglo, int indiceLineaArreglo)
        {
            int longitudPalabraArreglo = arregloPalabrasReservadas[indicePalabraArreglo, 0].Length;
            string cadenaTexto = arreglolineasTexto[indiceLineaArreglo, 1];
   
            //validar que antes de la palabra haya espacios en blanco o simbolos
            if (
                (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals(" ")
                ||(cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("=")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("<")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals(">")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("-")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("+")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("/")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("*")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("&")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("|")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("(")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals(";")
                )
            {
                //Validar que despues de la palabra haya un espacio en blanco o un punto
                if ((cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals(" ") || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("."))
                {
                   // DisplayAlert("Aviso", "la palabra " + validacionRegex.Value + " se encuentra aislada y lista para agregarse a la tabla", "si", "no");
                    return true;
                }
            }
            return false;
        }

        public bool comprobarPalabraPropiedad(Match validacionRegex, int indicePalabraReservada, int indicePalabraArreglo, int indiceLineaArreglo)
        {
            int longitudPalabraArreglo = arregloPalabrasReservadas[indicePalabraArreglo, 0].Length;
            string cadenaTexto = arreglolineasTexto[indiceLineaArreglo, 1];

            //validar que la palabra este aislada con espacio en blanco o un punto .
            if ((cadenaTexto[indicePalabraReservada - 1].ToString()).Equals(" ") || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("."))
            {
                //Validar que despues de la palabra haya espacio en blanco o simbolos
                if ((cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals(" ")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("=")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("<")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals(">")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("-")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("+")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("/")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("*")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("&")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("|"))
                {
                    //DisplayAlert("Aviso", "la palabra " + validacionRegex.Value + " se encuentra aislada y lista para agregarse a la tabla", "si", "no");
                    return true;
                }
            }
            return false;
        }

        public bool  comprobarPalabraSimbolo(Match validacionRegex, int indicePalabraReservada, int indicePalabraArreglo, int indiceLineaArreglo)
        {
            //DisplayAlert("Aviso", "simbolo " + validacionRegex.Value,"si","no");
            return true;
        }

        public bool comprobarCadena(Match validacionRegex, int indicePalabraReservada, int indicePalabraArreglo, int indiceLineaArreglo)
        {
            //DisplayAlert("Aviso", "simbolo " + validacionRegex.Value,"si","no");
            return true;
        }

        public bool comprobarPalabraIdentificador(Match validacionRegex, int indicePalabraReservada, int indicePalabraArreglo, int indiceLineaArreglo)
        {
            int longitudPalabraArreglo = validacionRegex.Value.Length;
            string cadenaTexto = arreglolineasTexto[indiceLineaArreglo, 1];

            //validar que la palabra este aislada con espacio en blanco o un punto .
            if ((cadenaTexto[indicePalabraReservada - 1].ToString()).Equals(" ") 
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals(".")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("=")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("<")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals(">")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("-")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("+")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("/")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("*")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("&")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("|")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals("(")
                || (cadenaTexto[indicePalabraReservada - 1].ToString()).Equals(")"))
            {
                //Validar que despues de la palabra haya espacio en blanco o simbolos
                if ((cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals(" ")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("=")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("<")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals(">")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("-")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("+")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("/")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("*")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("&")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("|")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals("(")
                || (cadenaTexto[indicePalabraReservada + longitudPalabraArreglo].ToString()).Equals(")")
                )
                {
                    //DisplayAlert("Aviso", "la palabra " + validacionRegex.Value + " se encuentra aislada y lista para agregarse a la tabla", "si", "no");
                    if (palabraNoEsReservada(validacionRegex.Value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool palabraNoEsReservada(string identificador)
        {
            for (int i=0; i<arregloPalabrasReservadas.GetLength(0); i++)
            {
                if ((Regex.Replace(arregloPalabrasReservadas[i, 0], " ", "")).Equals(Regex.Replace(identificador, " ", "")))
                {
                    return false;
                }
            }
            return true;
        }

        public List<PalabraEncontrada> ordenarPalabrasEncontradasPorLinea(List<PalabraEncontrada> listaPalabrasEncontradaTemporal)
        {
            List<PalabraEncontrada> ListaOrdenada = listaPalabrasEncontradaTemporal.OrderBy(o => o.indice).ToList();
            string palabrasEncontradas = "";
            for (int i=0;i< ListaOrdenada.Count; i++)
            {
                palabrasEncontradas += ListaOrdenada[i].palabra + "\n";
            }
            //DisplayAlert("Aviso","Por linea: "+palabrasEncontradas, "si", "no");

            return ListaOrdenada;
        }

        public List<PalabraError> encontrarErroresPorLinea(List<PalabraEncontrada> listaPalabrasEncontradaPorLinea,string cadenaTextoLinea, int nLinea)
        {
            //Lista temporal de errores por linea
            List<PalabraError> listaErroresTemporales =  new List<PalabraError>();
            //Separamos la cadena de texto por espacios en blanco
            string[] palabrasCadenaTextoOriginal = cadenaTextoLinea.Split(' ');

            //recorremos cada palabra de la cadena original
            for(int i=0; i< palabrasCadenaTextoOriginal.Length; i++)
            {
                //variable booleana para guardar si la palabra actual es reservada o no
                bool palabraEsReservada = false;
                //tomamos la palabra en turno
                string palabra = palabrasCadenaTextoOriginal[i];
                //comparamos con cada palabra del arreglo de palabras reservadas encontradas
                for (int j=0; j< listaPalabrasEncontradaPorLinea.Count; j++)
                {
                    string palabraReservada = listaPalabrasEncontradaPorLinea[j].palabra;
                    //si la palabra de la cadena original no coincide con la cadena en turno
                    if(!(Regex.Replace(palabra, " ", "")).Equals(Regex.Replace(palabraReservada, " ", "")))
                    {
                        //se pone como falso
                        palabraEsReservada = false;
                    }
                    //por el contrario si si es una palabra reservada
                    else
                    {
                        //se pone como verdadero
                        palabraEsReservada = true;
                    }
                }
                //cuando se termine de comparar la palabra de la cadena original
                //con la palabra reservada 
                //si  la variable palabraEsReservada fue false
                //quiere decir que la palabra es un error
                //y se agrega
                if (palabraEsReservada==false)
                {
                    PalabraError palabraError = new PalabraError();
                    palabraError.palabra = palabra;
                    palabraError.nLinea = nLinea;
                    listaErroresTemporales.Add(palabraError);
                }
            }
            return listaErroresTemporales;
        }

        public void agregarError(string cadenaTexto,int linea)
        {
            PalabraError palabraError = new PalabraError();
            palabraError.palabra = cadenaTexto;
            palabraError.nLinea = linea;
            listaErroresEncontrados.Add(palabraError);
        }

        public void mostrarPalabrasEncontradas(List<PalabraEncontrada> palabrasEncontradas)
        {
            String resultado = "";
            for (int i = 0; i < palabrasEncontradas.Count; i++)
            {
                resultado += "Palabra: " + palabrasEncontradas[i].palabra + "\nLinea: " + palabrasEncontradas[i].nLinea + "\n\n";
            }

            //DisplayAlert("Palabras",resultado,"Aceptar","Cerrar");
            buscarCaracteresValidos(palabrasEncontradas);
        }

        public void mostrarErroresEncontradas(List<PalabraError> palabrasEncontradas)
        {
            String resultado = "";
            for (int i = 0; i < palabrasEncontradas.Count; i++)
            {
                if (palabrasEncontradas[i].palabra.Contains(".")
                   || palabrasEncontradas[i].palabra.Contains(",")
                   || palabrasEncontradas[i].palabra.Contains("(")
                   || palabrasEncontradas[i].palabra.Contains(")")
                   || palabrasEncontradas[i].palabra.Contains("+")
                   || palabrasEncontradas[i].palabra.Contains("-")
                   || palabrasEncontradas[i].palabra.Contains("/")
                   || palabrasEncontradas[i].palabra.Contains("*")
                   || palabrasEncontradas[i].palabra.Contains("&")
                   || palabrasEncontradas[i].palabra.Contains("|")
                   || palabrasEncontradas[i].palabra.Contains("<")
                   || palabrasEncontradas[i].palabra.Contains(">")
                   || palabrasEncontradas[i].palabra.Contains("=")
                   || palabrasEncontradas[i].palabra.Contains(" ")
                   || palabrasEncontradas[i].palabra.Contains("\n"))
                {

                }
                else
                {
                    resultado += "Error: " + palabrasEncontradas[i].palabra + "\nLinea: " + palabrasEncontradas[i].nLinea + "\n\n";
                }
            }

            // DisplayAlert("Errores", resultado, "Aceptar", "Cerrar");
        }

        public void buscarCaracteresValidos(List<PalabraEncontrada> palabrasEncontradas)
        {
            for (int i = 0; i < arreglolineasTexto.GetLength(0); i++)
            {
                string lineaTexto = arreglolineasTexto[i,1];
                comprobarCaracteresValidos(lineaTexto);
            }
            Navigation.PushAsync(new tablaPalabras(palabrasEncontradas, mensajeLexico));
        }

        public void comprobarCaracteresValidos(string lineaTexto)
        {
            for (int i=0; i<lineaTexto.Length; i++)
            {
                char caracterActual = lineaTexto[i];
                if (!caracteresValidos.Contains(caracterActual))
                {
                    mensajeLexico += "El caracter "+caracterActual+" No es un caracter reconocido\n";
                }
            }
          
        }
    }
}
