namespace Sistem_informatic_Asiguri_auto
{
    public class Discount_RCA
    {
        public int Id_discount { get; set; }
        public string Denumire_discount { get; set; }
        public float Procent_discount { get; set; }
        public bool status_discount { get; set; }
        public string Discount
        {
            get { return $"{Denumire_discount} -- {Procent_discount}%"; }
        }

    }
}
