namespace Sistem_informatic_Asiguri_auto
{
    public class Subcategorii
    {
        public int Id_subcategorie { get; set; }
        public string Denumire_Subcategorie { get; set; }
        public string Numar_locuri { get; set; }
        public string Masa_min { get; set; }
        public string Masa_max { get; set; }
        public int Id_categorie { get; set; }
        public bool status_subcategorie { get; set; }
        public string FullSubcategorie
        {
            get { return $"{Denumire_Subcategorie} Locuri {Numar_locuri} Masa {Masa_min} -- {Masa_max}"; } 

        }

    }
}
