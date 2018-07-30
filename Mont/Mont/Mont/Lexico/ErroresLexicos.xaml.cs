using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mont.Lexico
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ErroresLexicos : ContentPage
	{
        string mensajeLexico;
		public ErroresLexicos (string mensajeLexico)
		{
			InitializeComponent ();
            this.mensajeLexico = mensajeLexico;
            mostrarErroresLexicos();
		}

        public void mostrarErroresLexicos()
        {
            txtErroresLexico.Text = this.mensajeLexico;
            txtErroresLexico.Text += "Aqui encontraras todos los caracteres validos de nuestro lenguaje\n";
            txtErroresLexico.Text += "Alfabeto: a b c d e f g h i j k l m n o p q r s t u v w x y z\n";
            txtErroresLexico.Text += "A B C D E F G H I J K L M N O P Q R S T U V  X Y Z\n";
            txtErroresLexico.Text += "Digitos: 0 1 2 3 4 5 6 7 8 9\n";
            txtErroresLexico.Text += "Operadores aritmeticos: + - * /\n";
            txtErroresLexico.Text += "Operadores logicos: && || == != < > <= >=\n";
            txtErroresLexico.Text += "Agrupadores: ( ) [ ] { }\n";
            txtErroresLexico.Text += "Caracteres: . , _\n";
        }
	}
}