using System.Collections.Generic;

namespace Sistem_informatic_Asiguri_auto
{
    public class Marca
    {
        public int Id_marca { get; set; }
        public string Denumire_marca { get; set; }
        public bool status_marca { get; set; }
        public List<Model> listaModele = new List<Model>();
    }
}
