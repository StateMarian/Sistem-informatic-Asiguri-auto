namespace Sistem_informatic_Asiguri_auto
{
    public class ZonaGeografica
    {
        public int Id_zona { get; set; }
        public string Judet { get; set; }
        public float Procent { get; set; }
        public string Tip_Asigurare  { get; set; }
        public bool status_zona { get; set; }

        public string FillListboxZone
        {
            get { return $"{Judet} - {Tip_Asigurare} - {Procent}% "; }
        }

    }
}
