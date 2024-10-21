namespace Sistem_informatic_Asiguri_auto
{
    public class CapacitateCilindrica
    {
        public int Id_capacitate { get; set; }
        public int Min_capacitate { get; set; }
        public int Max_capacitate { get; set; }
        public bool status_capacitate { get; set; }
        public int Putere { get; set; }
    
        public  string FullCap
        {
            get { return $"{Min_capacitate} -- {Max_capacitate} Putere {Putere}"; }
           
        }

    }
}
