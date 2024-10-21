using System.Collections.Generic;

namespace Sistem_informatic_Asiguri_auto
{
    public class Categorii
    {
        public int Id_categorie { get; set; }
        public string Denumire_categorie { get; set; }
        public string Cod_categorie { get; set; }

        public bool status_categorie { get; set; }
        public string Legenda
        {
            get { return $"{Id_categorie} --> {Denumire_categorie}"; }  
        }
        public List<Subcategorii> listaSubcat = new List<Subcategorii>();

    }
}
