namespace Sistem_informatic_Asiguri_auto
{
    public class GrupeVarsta
    {
        public int Id_grupa { get; set; }
        public int Min_varsta { get; set; }
        public int Max_varsta { get; set; }
        public bool status_grupa { get; set; }

        public string StringLista
        {
            get { return $"{Min_varsta}-{Max_varsta} ani"; }
            
        }

    }
}
