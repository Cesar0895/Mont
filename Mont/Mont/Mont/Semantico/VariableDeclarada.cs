using System;
using System.Collections.Generic;
using System.Text;

namespace Mont.Semantico
{
   public class VariableDeclarada
    {
        public string nombreVariable { get; set; }
        public string tipoDato { get; set; }
        public string valor { get; set; }
        public int linea { get; set; }
        public int columna { get; set; }
    }
}
