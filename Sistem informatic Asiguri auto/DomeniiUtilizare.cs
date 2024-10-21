namespace Sistem_informatic_Asiguri_auto
{
    public class DomeniiUtilizare
    {
        public int Id_utilizare { get; set; }
        public string Denumire_utilizare { get; set; }
        public float Procent_utilizare { get; set; }
        public bool status_domeniu { get; set; }

        public string Domeniu
        {
            get { return $"{Denumire_utilizare} -- {Procent_utilizare}%"; }
        }

    }
}
