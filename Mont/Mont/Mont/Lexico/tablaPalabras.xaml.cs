using Syncfusion.SfDataGrid.XForms;
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
	public partial class tablaPalabras : ContentPage
	{
        //SfDataGrid dataGrid;
        PalabraEncontradaRepositorio viewModel;
        string mensajeLexico;
        public tablaPalabras(List<PalabraEncontrada> arregloTokens,string mensajeLexico)
        {
            InitializeComponent();
            //dataGrid = new SfDataGrid();
            this.mensajeLexico = mensajeLexico;
            viewModel = new PalabraEncontradaRepositorio();
            dataGrid.AutoGenerateColumns = false;

            viewModel.GenerateOrders(arregloTokens);
            dataGrid.ItemsSource = viewModel.OrderInfoCollection;
            

            GridTextColumn palabra = new GridTextColumn();
            palabra.MappingName = "palabra";
            palabra.ColumnSizer = ColumnSizer.Star;

            GridTextColumn tipo = new GridTextColumn();
            tipo.MappingName = "tipo";
            tipo.ColumnSizer = ColumnSizer.LastColumnFill;

            GridTextColumn descripcion = new GridTextColumn();
            descripcion.MappingName = "descripcion";
            descripcion.ColumnSizer = ColumnSizer.Star;

            GridTextColumn indice = new GridTextColumn();
            indice.MappingName = "indice";

            GridTextColumn linea = new GridTextColumn();
            linea.MappingName = "nLinea";

            dataGrid.Columns.Add(palabra);
            dataGrid.Columns.Add(tipo);
            dataGrid.Columns.Add(descripcion);
            dataGrid.Columns.Add(indice);
            dataGrid.Columns.Add(linea);

            btnErrorLexico.Clicked += BtnErrorLexico_Clicked;
        }

        private void BtnErrorLexico_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ErroresLexicos(mensajeLexico));
        }
        //public tablaPalabras (List<PalabraEncontrada> arregloTokens)
        //{
        //	InitializeComponent ();
        //          PalabraEncontradaRepositorio viewModel = new PalabraEncontradaRepositorio();
        //          viewModel.GenerateOrders(arregloTokens);
        //          dataGrid.ItemsSource = viewModel.OrderInfoCollection;
        //      }
    }
}