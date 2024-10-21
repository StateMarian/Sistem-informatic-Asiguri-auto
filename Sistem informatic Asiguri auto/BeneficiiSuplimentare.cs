namespace Sistem_informatic_Asiguri_auto
{
    public class BeneficiiSuplimentare
    {
        public int Id_beneficiu { get; set; }
        public string Denumire_pachet  { get; set; }
        public string continut_pachet { get; set; }
        public float Procent_pachet { get; set; }
        public bool status_beneficiu { get; set; }

        public  string Beneficiu
        {
            get { return $"{Denumire_pachet} - {Procent_pachet}%"; }
           
        }

    }
}
