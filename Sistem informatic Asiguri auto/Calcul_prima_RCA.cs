namespace Sistem_informatic_Asiguri_auto
{
    public class Calcul_prima_RCA
    {
        public int Calcul_RCA_PrimaryKey { get; set; }
        public int Id_subcategorie { get; set; }
        public int Id_grupa { get; set; }
        public int Id_capacitate { get; set; }
        public int Id_durata { get; set; }
        public int Id_zona { get; set; }
        public int Id_beneficiu { get; set; }
        public int Id_discount { get; set; }
        public int Id_utilizare { get; set; } 
        public int Id_bonus_malus { get; set; }
        public int Prima_de_risc { get; set; }
        public bool status_asigurare { get; set; }
        public string Data_adaugare { get; set; }
    }
}
