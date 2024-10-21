using System.Collections.Generic;
using System.Linq;

namespace Sistem_informatic_Asiguri_auto
{
    public static class DateAngajat
    {
        public static string IdAngajat { get; set; }
        public static int PutereTractor = 45;
        public static string Tractoare = "Tractoare rutiere"; 
        public static string Autototurisme = "Autoturisme, autoturisme de teren si autovehicule mixte cu masa maxima autorizata mai mica de 3,5t si maxim 9 locuri";
        public static string Mopede = "Motociclete, mopede,  ATVuri";
        static List<Angajat> listaAngajati = DatabaseAcces.ExtrageAngajati().Where(d => d.status == true).ToList();
        public static string numeAngajat()
        {
            foreach(Angajat ang in listaAngajati)
            {
                if(ang.Cod_angajat==DateAngajat.IdAngajat)
                {
                    return ang.Nume + " " + ang.Prenume;
                }
            }
            return string.Empty;
        }
    }
}