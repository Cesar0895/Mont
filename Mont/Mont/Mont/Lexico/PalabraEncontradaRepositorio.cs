using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mont.Lexico
{
   public class PalabraEncontradaRepositorio
    {
        private ObservableCollection<PalabraEncontrada> orderInfo;
        public ObservableCollection<PalabraEncontrada> OrderInfoCollection
        {
            get { return orderInfo; }
            set { this.orderInfo = value; }
        }

        public PalabraEncontradaRepositorio()
        {
            orderInfo = new ObservableCollection<PalabraEncontrada>();
        }

        public void GenerateOrders(List<PalabraEncontrada> arregloTokens)
        {
            foreach (var item in arregloTokens)
            {
                orderInfo.Add(item);
            }
        }
    }
}
