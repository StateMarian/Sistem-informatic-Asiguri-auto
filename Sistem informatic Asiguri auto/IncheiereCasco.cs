namespace Sistem_informatic_Asiguri_auto
{
    public class IncheiereCasco
    {
        public int Cod_casco_primary_key { get; set; }
        public int Id_subcategorie { get; set; }
        public int Cod_client { get; set; }
        public string Cod_angajat { get; set; }
        public int Cod_auto { get; set; }
        public int Id_durata { get; set; }
        public int Id_casco { get; set; }
        public int Id_fransiza { get; set; }
        public int Id_utilizare{ get; set; }
        public string Data_inceput { get; set; }
        public string Data_emiterii { get; set; }
        public int Nr_kilometri { get; set; }
        public float Valoare_prima_casco { get; set; }
        public float Valoare_Casco { get; set; }
        public bool status_Casco { get; set; }
    }
}
