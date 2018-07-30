using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mont.Semantico
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SemanticoMain : ContentPage
	{
        List<VariableDeclarada> arregloVariablesDeclaras;
        List<PalabraEncontrada> arregloPalabrasEncontradas;
        List<NombresFunciones> arregloNombreFunciones;
        List<ParametrosFuncionNativa> arregloNombreParametrosFunciones;
        List<MotorOrdenEjecucion> arregloOrdenEjecucion;
        List<int> funcionesYaRevisadas =new List<int>();
        List<int> variablesYaRevisadas = new List<int>();

        public SemanticoMain (List<VariableDeclarada> arregloVariablesDeclaras,
                            List<PalabraEncontrada> arregloPalabrasEncontradas,
                            List<NombresFunciones> arregloNombreFunciones,
                            List<ParametrosFuncionNativa> arregloNombreParametrosFunciones,
                            List<MotorOrdenEjecucion> arregloOrdenEjecucion
                            )
		{
			InitializeComponent ();
            this.arregloVariablesDeclaras = arregloVariablesDeclaras;
            this.arregloPalabrasEncontradas = arregloPalabrasEncontradas;
            this.arregloNombreFunciones = arregloNombreFunciones;
            this.arregloNombreParametrosFunciones = arregloNombreParametrosFunciones;
            this.arregloOrdenEjecucion = arregloOrdenEjecucion;
            comprobarFuncionesRepetidas();
            comprobarVariablesRepetidas();
            comprobarParametrosFunciones();
            comprobarOrdenEjecucionMotor();
        }

        public void comprobarFuncionesRepetidas()
        {
    
            for (int i=0; i<arregloNombreFunciones.Count; i++)
            {
                string nombreFuncion = arregloNombreFunciones[i].nombreFuncion;
                int linea = arregloNombreFunciones[i].linea;
                for (int j=0; j<arregloNombreFunciones.Count; j++)
                {
                    if ((i!=j) && funcionYaRevisada(i) && funcionYaRevisada(i)) 
                    {
                        string nombreFuncion2 = arregloNombreFunciones[j].nombreFuncion;

                        if (nombreFuncion.Equals(nombreFuncion2))
                        {
                            funcionesYaRevisadas.Add(i);
                            funcionesYaRevisadas.Add(j);
                            lblSemantico.Text += "ya existe una funcion llamada " + nombreFuncion + " en la linea " + linea + "\n";
                        }
                    }
                }
            }
        }

        public void comprobarVariablesRepetidas()
        {

            for (int i = 0; i < arregloVariablesDeclaras.Count; i++)
            {
                string nombreVar = arregloVariablesDeclaras[i].nombreVariable;
                int linea = arregloVariablesDeclaras[i].linea;
                for (int j = 0; j < arregloVariablesDeclaras.Count; j++)
                {
                    if ((i != j) && variableYaRevisada(i) && variableYaRevisada(i))
                    {
                        string nombreVar2 = arregloVariablesDeclaras[j].nombreVariable;

                        if (nombreVar.Equals(nombreVar2))
                        {
                            variablesYaRevisadas.Add(i);
                            variablesYaRevisadas.Add(j);
                            lblSemantico.Text += "ya existe una variable llamada " + nombreVar + " en la linea " + linea + "\n";
                        }
                    }
                }
            }
        }

        public void comprobarParametrosFunciones()
        {
            for (int i=0; i< arregloNombreParametrosFunciones.Count;i++)
            {
                string tipoParametro = arregloNombreParametrosFunciones[i].tipoParametro;
                string parametro= arregloNombreParametrosFunciones[i].parametro;
                string funcion = arregloNombreParametrosFunciones[i].funcion;
                int linea = arregloNombreParametrosFunciones[i].linea;

                switch (tipoParametro)
                {
                    case "entero":
                        int value;
                        if (!(int.TryParse(parametro, out value)))
                        {
                            lblSemantico.Text += "La funcion "+funcion+" en la linea "+linea+" solo acepta un parametro del tipo entero\n";
                        }
                        break;
                }
            }
        }

        public bool funcionYaRevisada(int linea)
        {
            for (int i=0; i<funcionesYaRevisadas.Count; i++)
            {
                if (linea== funcionesYaRevisadas[i])
                {
                    return false;
                }
            }
            return true;
        }

        public bool variableYaRevisada(int linea)
        {
            for (int i = 0; i < variablesYaRevisadas.Count; i++)
            {
                if (linea == variablesYaRevisadas[i])
                {
                    return false;
                }
            }
            return true;
        }

        public void ComprobarVariablesDeclaradas()
        {

            bool identificadorEncontrado = false;
            for (int i = 0; i < arregloPalabrasEncontradas.Count; i++)
            {
                string identificadorEnTurno = arregloPalabrasEncontradas[i].palabra;
                int value;
                int indiceAnterior = i - 1;
                if (indiceAnterior<0)
                {
                    indiceAnterior = 0;
                }
                if (arregloPalabrasEncontradas[i].tipo.Equals("Identificador")
                    && !(int.TryParse(arregloPalabrasEncontradas[i].palabra, out value))
                    &&(!arregloPalabrasEncontradas[indiceAnterior].palabra.Contains("noRetorna"))
                    && (!arregloPalabrasEncontradas[indiceAnterior].palabra.Contains("Mont"))
                    )
                {
                    for (int j = 0; j < arregloVariablesDeclaras.Count; j++)
                    {

                        string variableEnTurno = arregloVariablesDeclaras[j].nombreVariable;
                        if (Regex.Replace(identificadorEnTurno, " ", "").Equals(Regex.Replace(variableEnTurno, " ", "")))
                        {
                            identificadorEncontrado = true;
                            break;
                        }
                        else
                        {
                            identificadorEncontrado = false;
                        }

                    }
                    if (identificadorEncontrado == false)
                    {
                        lblSemantico.Text += "No se ha declarado la variable " + identificadorEnTurno + "\n";
                    }
                }
            }
        }

        public void comprobarOrdenEjecucionMotor()
        {
            string ejecucion1 = arregloOrdenEjecucion[0].ejecucion;
            int linea = arregloOrdenEjecucion[1].linea;

            if (!ejecucion1.Equals("encender"))
            {
                lblSemantico.Text += "Linea \n"+linea+"Se intenta ejecutar la instruccion Motor."+ejecucion1+" sin enmbargo el motor aun no ha sido encendido\n";
            }
        }

    }
}