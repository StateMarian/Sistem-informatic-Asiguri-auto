namespace Sistem_informatic_Asiguri_auto
{
    public class IndicatoriGrupaCapacitate
    {
        public int Cod_Indicatori_PrimaryKey { get; set; }
        public int Id_subcategorie { get; set; }
        public int Id_grupa { get; set; }
        public int Id_capacitate { get; set; }
        public int Id_indicatori { get; set; }
        public string denumire_indicator { get; set; }
        public float Procent_indicator { get; set; }
     
        public bool status { get; set; }
    }
}
