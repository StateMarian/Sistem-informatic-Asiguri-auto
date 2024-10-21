namespace Sistem_informatic_Asiguri_auto
{
    public class Angajat
    {
        public string Cod_angajat { get; set; }
        public string Nume { get; set; }
        public string Prenume  { get; set; }
        public string Cnp { get; set; }
        public string Email { get; set; }
        public string Nr_telefon { get; set; }
        public string Data_angajare { get; set; }
        public string Tip_angajat { get; set; }
        public string Parola { get; set; }
        public string data_concediere { get; set; }
        public bool status { get; set; }

        public string FullName
        {
            get { return $"{Nume}  {Prenume}"; }
        }

    }
}
