namespace Sistem_informatic_Asiguri_auto
{
    public class IncheiereRCA
    {
        public int Cod_polita_primary_key { get; set; }
        public int Id_subcategorie { get; set; }
        public int Id_bonus_malus { get; set; }
        public int Cod_client { get; set; }
        public int Id_discount { get; set; }
        public int Id_beneficiu { get; set; }
        public int Id_utilizare { get; set; }
        public int Id_durata { get; set; }
        public int  Cod_auto { get; set; }
        public string Cod_angajat { get; set; }
        public string Data_inceput { get; set; }
        public string Data_emiterii { get; set; }
        public int Prima_de_risc { get; set; }
        public decimal Valoare_politaRCA { get; set; }
        public bool status_rca { get; set; }
    }
}
