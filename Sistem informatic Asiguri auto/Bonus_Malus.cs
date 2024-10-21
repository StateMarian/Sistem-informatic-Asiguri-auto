namespace Sistem_informatic_Asiguri_auto
{
    public class Bonus_Malus_Class
    {
        public int Id_bonus_malus { get; set; }
        public string Bonus_Malus { get; set; }
        public int Procent { get; set; }

        public bool status_bonus { get; set; }

        public string FullClass
        {
            get { return $"{Bonus_Malus} {Procent}%"; }
        }

    }
}
