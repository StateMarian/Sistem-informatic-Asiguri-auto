using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Net.Mail;
using System.Net;
using Image = System.Drawing.Image;

namespace Sistem_informatic_Asiguri_auto
{
    public partial class FormCalculatorIncheiereRca : Form
    {
        public FormCalculatorIncheiereRca()
        {
            InitializeComponent();
            AddDurataToCombobox();
            AddJudetToCombo();
            AddCategorieToCombo();
            AddDomeniuToCombo();
            AddDiscountToCombo();
            AddBeneficiiToCombo();
            AddBonusMalusToCombo();
            AddCombToCombo();
            AddMarcaCombobox();
            setareData();
            AddTariToCombo();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
       
        //Liste cu date extrase din baza de date
        #region ListeDate
        List<DurataAsigurare> durata = DatabaseAcces.ExtrageDurataAsigurare().Where(d => d.status_durata == true).ToList();
        List<ZonaGeografica> listaZone = DatabaseAcces.ExtrageZoneGeografice().Where(d => d.status_zona == true).ToList();
        List<Categorii> listaCategorii = DatabaseAcces.ExtrageCategorii().Where(d => d.status_categorie == true).ToList();
        List<Subcategorii> listaSubcategorii = DatabaseAcces.ExtrageSubcategorii().Where(d => d.status_subcategorie == true).ToList();
        List<DomeniiUtilizare> listaDomenii = DatabaseAcces.ExtrageDomeniiUtilizare().Where(d => d.status_domeniu == true).ToList();
        List<Discount_RCA> listaDisc = DatabaseAcces.ExtrageDiscount().Where(d => d.status_discount == true).ToList();
        List<BeneficiiSuplimentare> listaBeneficii = DatabaseAcces.ExtrageBeneficii().Where(d => d.status_beneficiu == true).ToList();
        List<Bonus_Malus_Class> listaBM = DatabaseAcces.ExtrageClaseBonusMalus().Where(d => d.status_bonus == true).ToList();
        List<GrupeVarsta> listaGrupeVarsta = DatabaseAcces.ExtrageGrupe().Where(d => d.status_grupa == true).ToList();
        List<CapacitateCilindrica> listaCapacitate = DatabaseAcces.ExtrageCapacitate().Where(d => d.status_capacitate == true).ToList();
        List<IndicatoriGrupaCapacitate> indicatoriGrupaCapacitate = DatabaseAcces.ExtrageIndicatoriDupaGrupaCapacitate().Where(d => d.status == true).ToList();
        List<Calcul_prima_RCA> listaAsigurariRca = DatabaseAcces.ExtrageAsigurareRCA().Where(d => d.status_asigurare == true).ToList();
        List<Marca> listaMarca = DatabaseAcces.ExtrageMarca().Where(d => d.status_marca == true).ToList();
        List<Model> listaModel = DatabaseAcces.ExtrageModel().Where(d => d.status_model == true).ToList();
        List<Client> listaClienti = DatabaseAcces.ExtrageClient();
        List<Adresa> listaAdresa = DatabaseAcces.ExtrageAdresa();
        List<Autovehicul> listaAuto = DatabaseAcces.ExtrageAutovehicul();
        List<IncheiereRCA> listaRCAFiltrata = DatabaseAcces.ExtrageRCA().Where(d => d.status_rca == true).ToList();
        #endregion
        //Setare data pana unde poate realiza angajatul o asigurare pentru un client
        void setareData()
        {
            dateTimePickerDataInceput.MaxDate = DateTime.Now.AddDays(30);

        }
        #region AdaugaDateControale
        void AddDurataToCombobox()
        {
            comboBoxDurata.DataSource = null;
            var listaFiltrata = durata
                .Where(d => d.Tip_asigurare == "RCA")
                .ToList();
            comboBoxDurata.DataSource = listaFiltrata;
            comboBoxDurata.DisplayMember = "Durata";
        }
        void setDate()
        {
            int luni = 0;
            string durata = comboBoxDurata.Text;
            if (durata == "12 luni")
            {
                luni = 12;
            }
            else
            {
                luni = 6;
            }
            DateTime date = new DateTime();
            date = dateTimePickerDataInceput.Value.AddMonths(luni);
            labelsfarsit.Text = date.ToShortDateString();
        }

        void AddJudetToCombo()
        {
            comboBoxJudet.DataSource = null;
            comboBoxJudet.Sorted = true;
            var listaFiltrata = listaZone
                .Where(d => d.Tip_Asigurare == "RCA")
                .ToList();
            listaFiltrata.RemoveAt(0);
            comboBoxJudet.DataSource = listaFiltrata;
            comboBoxJudet.DisplayMember = "Judet";
        }
        void AddCategorieToCombo()
        {
            comboBoxCategorie.DataSource = null;
            comboBoxCategorie.DataSource = listaCategorii;
            comboBoxCategorie.DisplayMember = "Denumire_categorie";
        }
        void AddDiscountToCombo()
        {
            comboBoxDiscount.DataSource = null;
            comboBoxDiscount.DataSource = listaDisc;
            comboBoxDiscount.DisplayMember = "Denumire_discount";
        }
        void AddSubcategoriiToComboBox()
        {
            comboBoxSubcategorie.DataSource = null;
            var listaSubcategoriiFiltrata = listaSubcategorii
                .Where(s => s.Id_categorie == ((Categorii)comboBoxCategorie.SelectedItem).Id_categorie)
                .Select(d => d.Denumire_Subcategorie).ToList()
                .Distinct().ToList();
            comboBoxSubcategorie.DataSource = listaSubcategoriiFiltrata;
            comboBoxSubcategorie.DisplayMember = "Denumire_Subcategorie";
        }
        void AddDomeniuToCombo()
        {
            comboBoxutilizare.DataSource = null;
            comboBoxutilizare.DataSource = listaDomenii;
            comboBoxutilizare.DisplayMember = "Denumire_utilizare";
        }
        void AddBeneficiiToCombo()
        {
            comboBoxBeneficii.DataSource = null;
            comboBoxBeneficii.DataSource = listaBeneficii;
            comboBoxBeneficii.DisplayMember = "Denumire_pachet";
        }
        void AddBonusMalusToCombo()
        {
            comboBoxBM.DataSource = null;
            comboBoxBM.DataSource = listaBM;
            comboBoxBM.DisplayMember = "Bonus_Malus";
        }
        void AddCombToCombo()
        {
            List<string> listaComb = new List<string> { "Benzina", "Motorina", "Benzina&GPL", "Hybrid", "Alt tip combustibil" };
            comboBoxCombustibil.DataSource = null;
            comboBoxCombustibil.DataSource = listaComb;
        }
        void AddMarcaCombobox()
        {
            comboBoxMarca.DataSource = null;
            comboBoxMarca.DataSource = listaMarca;
            comboBoxMarca.DisplayMember = "denumire_marca";
        }
        void AddModelToCombo()
        {
            comboBoxModel.DataSource = null;
            var listaFiltrata = listaModel
                .Where(d => d.Id_marca == ((Marca)comboBoxMarca.SelectedItem).Id_marca)
                .Select(d => d.Denumire_model)
                .Distinct()
                .ToList();
            comboBoxModel.DataSource = listaFiltrata;
            comboBoxModel.DisplayMember = "Denumire_model";
        }
        void AddVariantaToCombo()
        {
            var modelSelectat = comboBoxModel.SelectedItem as string;
            List<string> listaMod = new List<string>();
            if (comboBoxModel.SelectedItem != null)
            {
                comboBoxVarianta.DataSource = null;
                foreach (Model mod in listaModel)
                {
                    if (mod.Denumire_model == modelSelectat)
                    {
                        var varianta = mod.Varianta;
                        listaMod.Add(varianta);
                    }
                }
                listaMod.Sort();
                comboBoxVarianta.DataSource = listaMod;
            }
        }

        void AddTariToCombo()
        {
            List<string> listaTari = new List<string>() { "Romania" };
            comboBoxTara.DataSource = null;
            comboBoxTara.DataSource = listaTari;
        }
        private void dateTimePickerDataInceput_ValueChanged(object sender, EventArgs e)
        {
            setDate();
        }

        private void comboBoxDurata_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDate();
            if (labelPretPolita.Text != "")
            {
                valoarePolita();
            }
        }

        private void comboBoxCategorie_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddSubcategoriiToComboBox();
        }
        private void comboBoxMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddModelToCombo();
        }
        private void comboBoxModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddVariantaToCombo();
        }
        #endregion
        //Extrage varsta client din CNP
        int varstaClient()
        {
            int an = Convert.ToInt32(textBoxCnp.Text.Substring(1, 2));
            int luna = Convert.ToInt32(textBoxCnp.Text.Substring(3, 2));
            int zi = Convert.ToInt32(textBoxCnp.Text.Substring(5, 2));
            if (textBoxCnp.Text[0] == '1' || textBoxCnp.Text[0] == '2')
            {
                an += 1900;
            }
            else
            {
                if (textBoxCnp.Text[0] == '5' || textBoxCnp.Text[0] == '6')
                {
                    an += 2000;
                }
            }
            DateTime dataNastere = new DateTime(an, luna, zi);
            DateTime dataCurenta = DateTime.Now;

            int varsta = dataCurenta.Year - dataNastere.Year;

            if (dataNastere.Date > dataCurenta.Date.AddYears(-varsta))
            {
                varsta--;
            }
            return varsta;
        }
        //Stabilire grupa varsta pe baza varstei clientului
        int codGrupaVarsta()
        {
            int cod_grupa = 0;
            foreach (GrupeVarsta gr in listaGrupeVarsta)
            {
                if (gr.Min_varsta <= varstaClient() && gr.Max_varsta >= varstaClient())
                {
                    cod_grupa = gr.Id_grupa;
                }
            }
            return cod_grupa;
        }
        //Stabilire subcategorie pe baza informatiilo introduse de utilizator de la tastatura
        int cod_Subcategorie()
        {
            int cod_categ = ((Categorii)comboBoxCategorie.SelectedItem).Id_categorie;
            int cod_sub = 0;
            foreach (Subcategorii sub in listaSubcategorii)
            {
                if(sub.Denumire_Subcategorie == comboBoxSubcategorie.Text && Enumerable.Range(Convert.ToInt32(sub.Masa_min), Convert.ToInt32(sub.Masa_max)).Contains(Convert.ToInt32(textBoxMasa.Text)) && sub.Id_categorie == cod_categ)
                {
                    cod_sub = sub.Id_subcategorie;
                }
            }
            return cod_sub;
        }
        //Stabilire grupe de capacitate pe baza categoriei si a capacitati introduse de utilizator de la tastatura
        int cod_cap()
        {
            int cod_cap = 0;
            if (comboBoxCategorie.Text == DateAngajat.Tractoare)
            {
                foreach (CapacitateCilindrica cap in listaCapacitate)
                {
                    if (cap.Putere == DateAngajat.PutereTractor)
                    {
                        if (Convert.ToInt32(textBoxPutere.Text) <= cap.Putere)
                        {
                            cod_cap = cap.Id_capacitate;
                            break;
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(textBoxPutere.Text) >= cap.Putere && cap.Putere > 1)
                        {
                            cod_cap = cap.Id_capacitate;
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (CapacitateCilindrica cap in listaCapacitate)
                {
                    if (comboBoxCategorie.Text == DateAngajat.Autototurisme)
                    {
                        if (cap.Min_capacitate <= Convert.ToInt32(textBoxCapacitatecmc.Text.Trim()) && cap.Max_capacitate >= Convert.ToInt32(textBoxCapacitatecmc.Text.Trim()) && cap.Putere == 0)
                        {
                            cod_cap = cap.Id_capacitate;
                            break;
                        }
                    }
                    else
                    {
                        if (comboBoxCategorie.Text == DateAngajat.Mopede && cap.Putere == 1)
                        {
                            if (cap.Min_capacitate <= Convert.ToInt32(textBoxCapacitatecmc.Text.Trim()) && cap.Max_capacitate >= Convert.ToInt32(textBoxCapacitatecmc.Text.Trim()))
                            {
                                cod_cap = cap.Id_capacitate;
                                break;
                            }
                        }
                        else
                        {
                            cod_cap = cap.Id_capacitate;
                        }
                    }
                }
            }
            return cod_cap;
        }
        //Extragere prima de risc 
        int ValoarePrimaDeRisc()
        {
            int PrimaDeRisc = 0;
            foreach (Calcul_prima_RCA rca in listaAsigurariRca)
            {
                if (rca.Id_subcategorie == cod_Subcategorie() && rca.Id_grupa == codGrupaVarsta() && rca.Id_capacitate == cod_cap())
                {
                    PrimaDeRisc = rca.Prima_de_risc;
                }
            }
            return PrimaDeRisc;
        }
        //Calculare prima bruta pe baza indicatorilor extrasi din baza de date 
        float ValoarePrimaBruta()
        {
            float primaBruta = 0;
            List<IndicatoriGrupaCapacitate> indicatori = DatabaseAcces.ExtrageIndicatoriCalcul(cod_Subcategorie(), cod_cap(), codGrupaVarsta());
            if (indicatori.Count != 0)
            {
                float factorDaune = indicatori[0].Procent_indicator;
                float ibnr = indicatori[1].Procent_indicator;
                float trendulDaunelor = indicatori[2].Procent_indicator;
                float MarjaSiguranta = indicatori[3].Procent_indicator;
                float factorBM = indicatori[4].Procent_indicator;
                float cheltuieli = indicatori[5].Procent_indicator;
                float MarjaProfit = indicatori[6].Procent_indicator;
                primaBruta = (ValoarePrimaDeRisc() * (1 + factorDaune / 100) * (1 + ibnr / 100) * (1 + trendulDaunelor / 100) * (1 + MarjaSiguranta / 100)) / ((1 - cheltuieli / 100 - MarjaProfit / 100) * (1 - factorBM / 100));
            }
            return primaBruta;
        }
        //Extrage procent durata pe baza selectiei din combobox
        int procentDurata()
        {
            int procent = 0;
            foreach (DurataAsigurare dur in durata)
            {
                if (dur.Durata == comboBoxDurata.Text)
                {
                    procent = dur.Procent_durata;
                }
            }
            return procent;
        }
        //Extrage procent zonei geografice pe baza selectiei din combobox
        float procentJudet()
        {
            float procent = 0;
            foreach (ZonaGeografica zon in listaZone)
            {
                if (zon.Judet == comboBoxJudet.Text && zon.Tip_Asigurare=="RCA")
                {
                    procent = zon.Procent;
                }
            }
            return procent;
        }
        //Extrage procent domeniu de utilizare pe baza selectiei din combobox
        float procentUtilizare()
        {
            float procent = 0;
            foreach (DomeniiUtilizare dom in listaDomenii)
            {
                if (dom.Denumire_utilizare == comboBoxutilizare.Text)
                {
                    procent = dom.Procent_utilizare;
                }
            }
            return procent;
        }
        //Extrage procent discount pe baza selectiei din combobox
        float procentDiscount()
        {
            float procent = 0;
            foreach (Discount_RCA disc in listaDisc)
            {
                if (disc.Denumire_discount == comboBoxDiscount.Text)
                {
                    procent = disc.Procent_discount;
                }
            }
            return procent;
        }
        //Extrage procent discount pe baza selectiei din combobox
        float procentBeneficii()
        {
            float procent = 0;
            foreach (BeneficiiSuplimentare ben in listaBeneficii)
            {
                if (ben.Denumire_pachet == comboBoxBeneficii.Text)
                {
                    procent = ben.Procent_pachet;
                }
            }
            return procent;
        }
        //Extrage procent bonus-malus pe baza selectiei din combobox
        float procentBonus_Malus()
        {
            float procent = 0;
            foreach (Bonus_Malus_Class bm in listaBM)
            {
                if (bm.Bonus_Malus == comboBoxBM.Text)
                {
                    procent = bm.Procent;
                }
            }
            return procent;
        }
        //Calculare pret polita si afisarea pe interfata
        void valoarePolita()
        {
            float pretPolita = ValoarePrimaBruta();
            pretPolita = pretPolita + (pretPolita * procentJudet()) / 100 + (pretPolita * procentUtilizare()) / 100
                 + (pretPolita * procentBeneficii()) / 100 - ((pretPolita * procentDurata()) / 100 + (pretPolita * procentDiscount()) / 100);
            if (comboBoxBM.Text.Contains('B'))
            {
                float pretRca = pretPolita - (pretPolita * procentBonus_Malus()) / 100;
                string numarFormatat = pretRca.ToString("0.00");
                labelPretPolita.Text = Convert.ToString(numarFormatat);
            }
            else
            {
                float pretRca = pretPolita + (pretPolita * procentBonus_Malus()) / 100;
                string numarFormatat = pretRca.ToString("0.00");
                labelPretPolita.Text = Convert.ToString(numarFormatat);
            }
        }
        
        void enableButtonInainte()
        {
            if (labelPretPolita.Text != "")
            {
                buttonInainte.Enabled = true;
            }
        }

        private void buttonCalcul_Click(object sender, EventArgs e)
        {
            if (!(Verificari.checkCnp(textBoxCnp.Text)) || textBoxCnp.Text.Length != 13)
            {
                MessageBox.Show("Cnp introdus nu respecta formatul unui Cnp!");
            }
            else
            {
                if (string.IsNullOrEmpty(textBoxCnp.Text))
                {
                    MessageBox.Show("Va rog completati campul pentru CNP!");
                }
                else
                {
                    if (varstaClient() < 18)
                    {
                        MessageBox.Show("Persoanele sub 18 ani nu pot incheie o polita RCA!");
                    }
                    else
                    {
                        if (!(Verificari.checkCnp(textBoxCnp.Text)) || textBoxCnp.Text.Length != 13)
                        {
                            MessageBox.Show("Cnp introdus nu respecta formatul unui Cnp!");
                        }
                        else
                        {
                            if (textBoxseriesasiu.Text == "")
                            {
                                MessageBox.Show("Campul pentru seria VIN nu poate ramane necompletat!");
                            }
                            else
                            {
                                if (textBoxNrInamtri.Text == "")
                                {
                                    MessageBox.Show("Campul pentru numarul de inmatriculare nu poate ramane necompletat!");
                                }
                                else
                                {
                                    if (!Verificari.checkCnp(textBoxNumarLocuri.Text) || string.IsNullOrEmpty(textBoxNumarLocuri.Text))
                                    {
                                        MessageBox.Show("Va rog completati numarul de locuri, acesta trebuie sa fie un numar!");
                                    }
                                    else
                                    {
                                        if (!Verificari.checkCnp(textBoxMasa.Text) || string.IsNullOrEmpty(textBoxMasa.Text))
                                        {
                                            MessageBox.Show("Va rog completati masa maxima autorizata, aceasta trebuie sa fie un numar!");
                                        }
                                        else
                                        {
                                            if (!Verificari.checkCnp(textBoxCapacitatecmc.Text) || string.IsNullOrEmpty(textBoxCapacitatecmc.Text))
                                            {
                                                MessageBox.Show("Va rog completati capacitatea cilindrica, aceasta trebuie sa fie un numar!");
                                            }
                                            else
                                            {
                                                if (!Verificari.checkCnp(textBoxPutere.Text) || string.IsNullOrEmpty(textBoxPutere.Text))
                                                {
                                                    MessageBox.Show("Va rog completati puterea masini, aceasta trebuie sa fie un numar!");
                                                }
                                                else
                                                {
                                                    if (codGrupaVarsta() == 0)
                                                    {
                                                        MessageBox.Show("Pentru varsta clientului nu se mai poate incheia o polita de asigurare RCA!");
                                                        labelPretPolita.Text = "";
                                                    }
                                                    else
                                                    {
                                                        if (cod_Subcategorie() == 0)
                                                        {
                                                            MessageBox.Show("Pentru subcategoria selectata nu a fost adaugata o polita RCA.");
                                                            labelPretPolita.Text = "";
                                                        }
                                                        else
                                                        {
                                                            if (cod_cap() == 0)
                                                            {
                                                                MessageBox.Show("Pentru capacitatea introdusa nu a fost adaugata o polita RCA.");
                                                                labelPretPolita.Text = "";
                                                            }
                                                            else
                                                            {
                                                                if (ValoarePrimaBruta() == 0)
                                                                {
                                                                    MessageBox.Show("Oferta nu mai este disponibila sau nu a fost inca introdusa in sistem!");
                                                                    labelPretPolita.Text = "";
                                                                }
                                                                else
                                                                {
                                                                    valoarePolita();
                                                                    enableButtonInainte();
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #region Verificari
        void completeazaControaleAutomat()
        {

            if (listaClienti.Any(d => d.Cnp == textBoxCnp.Text))
            {
                int cod_client = 0;
                int cod_adresa = 0;
                foreach (Client cl in listaClienti)
                {
                    if (cl.Cnp == textBoxCnp.Text)
                    {
                        textBoxNume.Text = cl.Nume;
                        textBoxPrenume.Text = cl.Prenume;
                        dateTimePickerObtinerePermis.Value = Convert.ToDateTime(cl.An_obtinere_permis);
                        textBoxemail.Text = cl.Email;
                        textBoxNrTelefon.Text = cl.Nr_telefon;
                        cod_client = cl.Cod_client;
                        cod_adresa = cl.Cod_adresa;
                    }
                }
                foreach (Adresa adr in listaAdresa)
                {
                    if (adr.Cod_adresa == cod_adresa && adr.status_adresa == true)
                    {
                        textBoxLocalitate.Text = adr.Localitate;
                        textBoxStrada.Text = adr.Strada;
                        textBoxNumarstrada.Text = adr.Nr_strada;
                        textBoxBloc.Text = adr.Bloc;
                        textBoxScara.Text = adr.Scara;
                        textBoxEtaj.Text = adr.Etaj;
                        textBoxApartament.Text = adr.Apartament;
                    }
                }
                foreach (Autovehicul auto in listaAuto)
                {
                    if (auto.Cod_client == cod_client && auto.Cod_auto == cod_autovehicul())
                    {
                        textBoxseriesasiu.Text = auto.Serie_sasiu;
                        textBoxNrInamtri.Text = auto.Nr_inmatriculare;
                        dateTimePickerFabricatieAuto.Value = Convert.ToDateTime(auto.An_fabricatie);
                        comboBoxCombustibil.Text = auto.Combustibil;
                        textBoxCapacitatecmc.Text = Convert.ToString(auto.Capacitate_cilindrica);
                        textBoxPutere.Text = Convert.ToString(auto.Putere);
                        textBoxNumarLocuri.Text = Convert.ToString(auto.Nr_locuri);
                        textBoxMasa.Text = Convert.ToString(auto.Masa_maxima_autorizata);
                    }
                }
            }
        }

        bool verificaClientExistent()
        {
            if (listaClienti.Any(d => d.Cnp == textBoxCnp.Text))
            {
                return true;
            }
            else
                return false;
        }

        //Metoda verificare controale completate si verificarea datelor introduse de utilizator
        bool checkCompletedControls()
        {
            if (!Verificari.checkName(textBoxNume.Text) || string.IsNullOrEmpty(textBoxNume.Text))
            {
                MessageBox.Show("Numele introdus nu este corect!");
                return false;
            }
            else
            {
                if (!Verificari.checkName(textBoxPrenume.Text) || string.IsNullOrEmpty(textBoxPrenume.Text))
                {
                    MessageBox.Show("Prenumele introdus nu este corect!");
                    return false;
                }
                else
                {
                    if (!(Verificari.checkCnp(textBoxNrTelefon.Text)) || textBoxNrTelefon.Text.Length != 10)
                    {
                        MessageBox.Show("Numarul de telefon introdus este invalid!");
                        return false;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(textBoxemail.Text) || !Verificari.checkEmail(textBoxemail.Text))
                        {
                            MessageBox.Show("Emailul introdus nu respecta formatul unui email!");
                            return false;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(textBoxLocalitate.Text))
                            {
                                MessageBox.Show("Completati localitatea!");
                                return false;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(textBoxStrada.Text))
                                {
                                    MessageBox.Show("Completati strada!");
                                    return false;
                                }
                                else
                                {
                                    if (!Verificari.checkCnp(textBoxNumarstrada.Text) || string.IsNullOrEmpty(textBoxNumarstrada.Text))
                                    {
                                        MessageBox.Show("Completati numarul strazii!");
                                        return false;
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(textBoxBloc.Text))
                                        {
                                            MessageBox.Show("Completati campul pentru bloc daca exista daca nu completati cu - !");
                                            return false;
                                        }
                                        else
                                        {
                                            if (string.IsNullOrEmpty(textBoxScara.Text))
                                            {
                                                MessageBox.Show("Completati campul pentru scara daca exista daca nu completati cu - !");
                                                return false;
                                            }
                                            else
                                            {
                                                if (string.IsNullOrEmpty(textBoxEtaj.Text))
                                                {
                                                    MessageBox.Show("Completati campul pentru etaj daca exista daca nu completati cu - !");
                                                    return false;
                                                }
                                                else
                                                {
                                                    if (string.IsNullOrEmpty(textBoxApartament.Text))
                                                    {
                                                        MessageBox.Show("Completati campul pentru apartament daca exista daca nu completati cu - !");
                                                        return false;
                                                    }
                                                    else
                                                    {
                                                        return true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        //Extragere durata din baza de date
        string denumireDurata()
        {
            string den = string.Empty;
            foreach (IncheiereRCA rca in listaRCAFiltrata)
            {
                if (rca.Cod_client == cod_client() && rca.Cod_auto == cod_autovehicul())
                {
                    foreach (DurataAsigurare dur in durata)
                    {
                        if (rca.Id_durata == dur.Id_durata)
                        {
                            den = dur.Durata;
                        }
                    }
                }
            }
            return den;
        }
        //Pe baza extragerii din baza de date a denumiri duratei se initializeaza variabila zile 
        int zileDurate()
        {
            int zile = 0;
            if (denumireDurata() == "6 luni")
            {
                zile = 183;
            }
            else
            {
                zile = 365;
            }
            return zile;
        }
        //Stabilire zile valabilitate RCA
        int zileValabilitate()
        {
            int zileScurse = 0;
            if (listaRCAFiltrata.Count != 0)
            {
                foreach (IncheiereRCA rca in listaRCAFiltrata)
                {
                    if (rca.Cod_client == cod_client() && rca.Cod_auto == cod_autovehicul())
                    {
                        DateTime data_inceput = Convert.ToDateTime(rca.Data_inceput);
                        DateTime data_curenta = DateTime.Now;
                        TimeSpan diferenta = data_curenta - data_inceput;
                        zileScurse = diferenta.Days;
                        if (zileScurse <= zileDurate())
                        {
                            return zileDurate() - zileScurse;
                        }
                    }
                }
            }
            else
            {

            }
            return 0;
        }
        //Verificarea valabilitati RCA pe baza stabiliri zilelor de valabilitate cu ajutorul metodei de mai sus
        bool verificaValabilitateRca()
        {
            int zileScurse = 0;
            if (listaRCAFiltrata.Count != 0)
            {
                foreach (IncheiereRCA rca in listaRCAFiltrata)
                {
                    if (rca.Cod_client == cod_client() && rca.Cod_auto == cod_autovehicul())
                    {
                        DateTime data_inceput = Convert.ToDateTime(rca.Data_inceput);
                        DateTime data_curenta = DateTime.Now;
                        TimeSpan diferenta = data_curenta - data_inceput;
                        zileScurse = diferenta.Days;
                        if (zileScurse <= zileDurate())
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {

            }
            return false;
        }
        #endregion
        //Dezactivare controale dupa calcularea politei
        #region DezactivareControale
        private void buttonInainte_Click(object sender, EventArgs e)
        {
            dateTimePickerDataInceput.Enabled = false;
            comboBoxDurata.Enabled = false;
            textBoxCnp.Enabled = false;
            comboBoxJudet.Enabled = false;
            textBoxseriesasiu.Enabled = false;
            textBoxNrInamtri.Enabled = false;
            comboBoxCategorie.Enabled = false;
            comboBoxSubcategorie.Enabled = false;
            textBoxNumarLocuri.Enabled = false;
            textBoxMasa.Enabled = false;
            textBoxCapacitatecmc.Enabled = false;
            textBoxPutere.Enabled = false;
            dateTimePickerFabricatieAuto.Enabled = false;
            comboBoxMarca.Enabled = false;
            comboBoxModel.Enabled = false;
            comboBoxVarianta.Enabled = false;
            comboBoxutilizare.Enabled = false;
            comboBoxDiscount.Enabled = false;
            comboBoxBeneficii.Enabled = false;
            buttonCauta.Enabled = false;
            buttonInainte.Enabled = false;
            comboBoxBM.Enabled = false;
            comboBoxCombustibil.Enabled = false;
            buttonCalcul.Enabled = false;
            labelPretPolita.Enabled = false;
            textBoxNume.Enabled = true;
            textBoxPrenume.Enabled = true;
            dateTimePickerObtinerePermis.Enabled = true;
            textBoxNrTelefon.Enabled = true;
            textBoxemail.Enabled = true;
            comboBoxTara.Enabled = true;
            textBoxLocalitate.Enabled = true;
            textBoxStrada.Enabled = true;
            textBoxNumarstrada.Enabled = true;
            textBoxBloc.Enabled = true;
            textBoxScara.Enabled = true;
            textBoxEtaj.Enabled = true;
            textBoxApartament.Enabled = true;
            buttonPrevPolita.Enabled = true;
            buttonInapoi.Enabled = true;
        }
        //Dezactivare controale daca se revine la calculare
        private void buttonInapoi_Click(object sender, EventArgs e)
        {
            dezactivareControale();
        } 

        void dezactivareControale()
        {
            dateTimePickerDataInceput.Enabled = true;
            comboBoxDurata.Enabled = true;
            textBoxCnp.Enabled = true;
            comboBoxJudet.Enabled = true;
            textBoxseriesasiu.Enabled = true;
            textBoxNrInamtri.Enabled = true;
            comboBoxCategorie.Enabled = true;
            comboBoxSubcategorie.Enabled = true;
            textBoxNumarLocuri.Enabled = true;
            textBoxMasa.Enabled = true;
            textBoxCapacitatecmc.Enabled = true;
            textBoxPutere.Enabled = true;
            dateTimePickerFabricatieAuto.Enabled = true;
            comboBoxMarca.Enabled = true;
            comboBoxModel.Enabled = true;
            comboBoxVarianta.Enabled = true;
            comboBoxutilizare.Enabled = true;
            comboBoxDiscount.Enabled = true;
            comboBoxBeneficii.Enabled = true;
            comboBoxBM.Enabled = true;
            comboBoxCombustibil.Enabled = true;
            buttonCalcul.Enabled = true;
            buttonCauta.Enabled = true;
            buttonInainte.Enabled = true;
            labelPretPolita.Enabled = true;
            textBoxNume.Enabled = false;
            textBoxPrenume.Enabled = false;
            dateTimePickerObtinerePermis.Enabled = false;
            textBoxNrTelefon.Enabled = false;
            textBoxemail.Enabled = false;
            comboBoxTara.Enabled = false;
            textBoxLocalitate.Enabled = false;
            textBoxStrada.Enabled = false;
            textBoxNumarstrada.Enabled = false;
            textBoxBloc.Enabled = false;
            textBoxScara.Enabled = false;
            textBoxEtaj.Enabled = false;
            textBoxApartament.Enabled = false;
            buttonPrevPolita.Enabled = false;
            buttonInapoi.Enabled = false;
            buttonIncheierePolita.Enabled = false; 
        }
        //Golire controale dupa incheierea politei
        void clearControls()
        {
            foreach (Control ctrl in Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).Clear();
                }
            }    
            labelPretPolita.Text = "";
            dateTimePickerFabricatieAuto.Value = DateTime.Now;
            dateTimePickerDataInceput.Value = DateTime.Now;
            dateTimePickerObtinerePermis.Value = DateTime.Now;
            AddJudetToCombo();
            AddDomeniuToCombo();
            AddDiscountToCombo();
            AddBeneficiiToCombo();
            AddBonusMalusToCombo();
            AddCombToCombo();
            dezactivareControale();
        }
        #endregion
        //Verificari existente a clientului si a autovehiculului
        bool verificaAsigurariExistente()
        {
            foreach (IncheiereRCA rca in listaRCAFiltrata)
            {
                if (rca.Cod_client == cod_client() && rca.Cod_auto == cod_autovehicul())
                {
                    return false;
                }
            }
            return true;
        }
        bool VerificaAutoExistentClient()
        {
            if (listaAuto.Any(d => d.Cod_client == cod_client() && d.Cod_auto == cod_autovehicul()))
            {
                return true;
            }
            else
                return false;
        }
        //Cautarea clientului pe baza cnp-ului, seriei de sasiu si a numarului de inmatriculare
        private void buttonCauta_Click(object sender, EventArgs e)
        {
            if (!(Verificari.checkCnp(textBoxCnp.Text)) || textBoxCnp.Text.Length != 13)
            {
                MessageBox.Show("Cnp introdus nu respecta formatul unui Cnp!");
            }
            else
            {
                if (string.IsNullOrEmpty(textBoxseriesasiu.Text) || string.IsNullOrEmpty(textBoxNrInamtri.Text))
                {
                    MessageBox.Show("Pentru a gasi clientul trebuie completat si seria de sasiu si numarul de inmatriculare!");
                }
                else
                {
                    if (verificaValabilitateRca())
                    {
                        DialogResult dialog = MessageBox.Show($"Pentru datele introduse aveti o asigurare cu valabilitate de {zileValabilitate()} zile, daca doriti sa incheiati o asigurare pentru alt autovehicul continuati!", "Confirmare", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.Yes)
                        {
                            completeazaControaleAutomat();
                            textBoxseriesasiu.Clear();
                            textBoxNrInamtri.Clear();
                            textBoxCapacitatecmc.Clear();
                            textBoxPutere.Clear();
                            textBoxMasa.Clear();
                            textBoxNumarLocuri.Clear();
                        }
                    }
                    else
                    {
                        if (verificaClientExistent())
                        {
                            if (VerificaAutoExistentClient())
                            {
                                DialogResult dialog = MessageBox.Show("Nu aveti o asigurare valabila, doriti sa incheiati o asigurare?", "Confirmare", MessageBoxButtons.YesNo);
                                if (dialog == DialogResult.Yes)
                                {
                                    completeazaControaleAutomat();
                                    labelPretPolita.Text = "";
                                }
                                else
                                {
                                    clearControls();
                                }
                            }
                            else
                            {
                                DialogResult dialog = MessageBox.Show("Clientul pentru care ati introdus cnp-ul exista in sistem dar datele introduse pentru autovehicul nu exista daca doriti sa continuati va rog completati campurile pentru autovehicul", "Confirmare", MessageBoxButtons.YesNo);
                                if (dialog == DialogResult.Yes)
                                {
                                    completeazaControaleAutomat();
                                    textBoxCapacitatecmc.Clear();
                                    textBoxPutere.Clear();
                                    textBoxMasa.Clear();
                                    textBoxNumarLocuri.Clear();
                                    labelPretPolita.Text = "";
                                }
                                else
                                {
                                    clearControls();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Clientul nu exista in sistem continuati cu completarea campurilor!");
                            //clearControls();
                        }
                    }
                }
            }
        }

        #region Date necesare incheiere polita
        int cod_model()
        {
            int cod_model = 0;
            int cod_marca = ((Marca)comboBoxMarca.SelectedItem).Id_marca;
            var modelSelectat = comboBoxModel.SelectedItem as string;
            var variantaSelectata = comboBoxVarianta.SelectedItem as string;
            foreach (Model mod in listaModel)
            {
                if (mod.Denumire_model == modelSelectat && mod.Varianta == variantaSelectata && mod.Id_marca == cod_marca)
                {
                    cod_model = mod.Id_model;
                }
            }
            return cod_model;
        }
        int cod_client()
        {
            int cod_client = 0;
            foreach (Client cl in listaClienti)
            {
                if (cl.Cnp == textBoxCnp.Text.Trim())
                {
                    cod_client = cl.Cod_client;
                    break;
                }
            }
            return cod_client;
        }

        int cod_autovehicul()
        {
            int cod_auto = 0;
            foreach (Autovehicul auto in listaAuto)
            {
                if (auto.Serie_sasiu.ToUpper() == textBoxseriesasiu.Text.Trim().ToUpper() && auto.Nr_inmatriculare.ToUpper() == textBoxNrInamtri.Text.Trim().ToUpper())
                {
                    cod_auto = auto.Cod_auto;
                    break;
                }
            }
            return cod_auto;
        }
        int cod_auto_client()
        {
            int cod_auto = 1;
            foreach (Autovehicul auto in listaAuto)
            {
                if (auto.Cod_client == cod_client() && auto.Cod_auto == cod_autovehicul())
                {
                    cod_auto = auto.Cod_auto;
                }
            }
            return cod_auto;
        }

        int cod_adresa()
        {
            int cod_adresa = 0;
            foreach (Client cl in listaClienti)
            {
                if (cl.Cnp == textBoxCnp.Text.Trim())
                {
                    cod_adresa = cl.Cod_adresa;
                    break;
                }
            }
            return cod_adresa;
        }

        int cod_adresa_controale()
        {
            int cod_adresa = 0;
            foreach (Adresa adr in listaAdresa)
            {
                if (adr.Judet.ToUpper() == comboBoxJudet.Text.ToUpper() && adr.Localitate.ToUpper() == textBoxLocalitate.Text.Trim().ToUpper() &&
                    adr.Strada.ToUpper() == textBoxStrada.Text.Trim().ToUpper() && adr.Nr_strada.ToUpper() == textBoxNumarstrada.Text.Trim().ToUpper() && adr.Bloc.ToUpper() == textBoxBloc.Text.Trim().ToUpper() &&
                    adr.Scara.ToUpper() == textBoxScara.Text.Trim().ToUpper() && adr.Etaj.ToUpper() == textBoxEtaj.Text.Trim().ToUpper() && adr.Apartament.ToUpper() == textBoxApartament.Text.Trim().ToUpper())
                {
                    cod_adresa = adr.Cod_adresa;
                }
            }
            return cod_adresa;
        }

        int cod_polita()
        {
            int id_rca = 1;
            List<IncheiereRCA> listaRCA = DatabaseAcces.ExtrageRCA();
            if (listaRCA.Count > 0)
            {
                id_rca = listaRCA.Max(d => d.Cod_polita_primary_key) + 1;
            }
            return id_rca;
        }
        int cod_auto()
        {
            int id_auto = 1;
            List<Autovehicul> listaAuto = DatabaseAcces.ExtrageAutovehicul();
            if (listaAuto.Count > 0)
            {
                id_auto = listaAuto.Max(d => d.Cod_auto) + 1;
            }
            return id_auto;
        }

        int cheie_primara_adresa()
        {
            int id_adresa = 1;
            List<Adresa> listaAdresaCheie = DatabaseAcces.ExtrageAdresa();
            if (listaAdresaCheie.Count > 0)
            {
                id_adresa = listaAdresaCheie.Max(d => d.Cod_adresa) + 1;
            }
            return id_adresa;
        }
        #endregion
        #region CreareObiecte
        IncheiereRCA creazaPolita(int id_rca, int cod_client, int cod_auto)
        {
            IncheiereRCA rca = new IncheiereRCA()
            {
                Cod_polita_primary_key = id_rca,
                Id_subcategorie = cod_Subcategorie(),
                Id_bonus_malus = ((Bonus_Malus_Class)comboBoxBM.SelectedItem).Id_bonus_malus,
                Cod_client = cod_client,
                Id_discount = ((Discount_RCA)comboBoxDiscount.SelectedItem).Id_discount,
                Id_beneficiu = ((BeneficiiSuplimentare)comboBoxBeneficii.SelectedItem).Id_beneficiu,
                Id_utilizare = ((DomeniiUtilizare)comboBoxutilizare.SelectedItem).Id_utilizare,
                Id_durata = ((DurataAsigurare)comboBoxDurata.SelectedItem).Id_durata,
                Cod_auto = cod_auto,
                Cod_angajat = DateAngajat.IdAngajat,
                Data_inceput = Convert.ToString(dateTimePickerDataInceput.Value),
                Data_emiterii = Convert.ToString((DateTime.Now)),
                Prima_de_risc = ValoarePrimaDeRisc(),
                Valoare_politaRCA = Convert.ToDecimal(labelPretPolita.Text),
                status_rca = true
            };
            return rca;
        }

        Autovehicul creeazaAuto(int cod_auto, int cod_client)
        {
            Autovehicul auto = new Autovehicul()
            {
                Cod_auto = cod_auto,
                Serie_sasiu = textBoxseriesasiu.Text.Trim().ToUpper(),
                Nr_inmatriculare = textBoxNrInamtri.Text.Trim().ToUpper(),
                An_fabricatie = Convert.ToString(dateTimePickerFabricatieAuto.Value),
                Combustibil = comboBoxCombustibil.Text,
                Capacitate_cilindrica = Convert.ToInt32(textBoxCapacitatecmc.Text.Trim()),
                Putere = Convert.ToInt32(textBoxPutere.Text.Trim()),
                Nr_locuri = Convert.ToInt32(textBoxNumarLocuri.Text.Trim()),
                Masa_maxima_autorizata = Convert.ToInt32(textBoxMasa.Text.Trim()),
                Id_model = cod_model(),
                Cod_client = cod_client,
                Id_subcategorie = cod_Subcategorie()
            };
            return auto;
        }

        Adresa creeazaAdresa(int cod_adresa)
        {
            Adresa adr = new Adresa()
            {
                Cod_adresa = cod_adresa,
                Tara = comboBoxTara.Text.Trim(),
                Judet = comboBoxJudet.Text.Trim(),
                Localitate = textBoxLocalitate.Text.Trim(),
                Strada = textBoxStrada.Text.Trim(),
                Nr_strada = textBoxNumarstrada.Text.Trim(),
                Bloc = textBoxBloc.Text.Trim(),
                Scara = textBoxScara.Text.Trim(),
                Etaj = textBoxEtaj.Text.Trim(),
                Apartament = textBoxApartament.Text.Trim(),
                status_adresa = true
            };
            return adr;
        }
        #endregion

        #region IncheierePolitaRca
        private void buttonIncheierePolita_Click(object sender, EventArgs e)
        {

            if (checkCompletedControls())
            {
                if (!verificaAsigurariExistente())
                {
                    MessageBox.Show($"Pentru datele completate deja exista o asigurare valabila, aceasta mai are {zileValabilitate()} zile valabilitate!");
                }
                else
                {
                    if (listaClienti.Any(d => d.Cnp == textBoxCnp.Text))
                    {
                        //Scenariul cand adresa si autovehiculul sunt aceleasi
                        if (cod_adresa() == cod_adresa_controale() && cod_autovehicul() == cod_auto_client())
                        {

                            DialogResult dialog = MessageBox.Show("Sigur doriti sa incheiati polita RCA", "Confirmare", MessageBoxButtons.YesNo);
                            if (dialog == DialogResult.Yes)
                            {
                                //Creare polita rca pentru un client cu acelasi autovehicul si cu acceasi adresa (reinoirea politei)
                                int cheie_polita = cod_polita();
                                IncheiereRCA rca = creazaPolita(cheie_polita, cod_client(), cod_autovehicul());
                                listaRCAFiltrata.Add(rca);
                                DatabaseAcces.AdaugaPolitaRca(rca);
                                DatabaseAcces.UpdateClient(cod_client(), textBoxemail.Text.Trim(), textBoxNrTelefon.Text.Trim());
                                SalvarePdf(PrevPdf(cheie_polita), cheie_polita);
                                SendEmail(textBoxemail.Text, SalvarePdf(PrevPdf(cheie_polita), cheie_polita));
                                MessageBox.Show("Polita Rca incheiata cu succes!");                              
                                clearControls();
                                buttonInainte.Enabled = false;
                            }
                            else
                            {
                                MessageBox.Show("Actiune anulata!");
                            }
                        }
                        else
                        {
                            //Scenariul cand adresa este aceeasi dar autovehiculul difera
                            if (cod_adresa() == cod_adresa_controale() && cod_autovehicul() != cod_auto_client())
                            {
                                DialogResult dialog = MessageBox.Show("Sigur doriti sa incheiati polita RCA", "Confirmare", MessageBoxButtons.YesNo);
                                if (dialog == DialogResult.Yes)
                                {
                                    //Creare autovehicul nou pentru acelasi client
                                    int cod_Auto = cod_auto();
                                    Autovehicul auto = creeazaAuto(cod_Auto, cod_client());
                                    listaAuto.Add(auto);
                                    DatabaseAcces.AdaugaAuto(auto);
                                    //Incheiere rca pentru acelasi client care are aceeasi adresa dar alt autovehicul
                                    int cheie_polita = cod_polita();
                                    IncheiereRCA rca = creazaPolita(cheie_polita, cod_client(), cod_Auto);
                                    listaRCAFiltrata.Add(rca);
                                    DatabaseAcces.AdaugaPolitaRca(rca);
                                    DatabaseAcces.UpdateClient(cod_client(), textBoxemail.Text.Trim(), textBoxNrTelefon.Text.Trim());
                                    SalvarePdf(PrevPdf(cheie_polita), cheie_polita);
                                    SendEmail(textBoxemail.Text, SalvarePdf(PrevPdf(cheie_polita), cheie_polita));
                                    MessageBox.Show("Polita Rca incheiata cu succes!");                                 
                                    clearControls();
                                    buttonInainte.Enabled = false;
                                }
                                else
                                {
                                    MessageBox.Show("Actiune anulata!");
                                }
                            }
                            else
                            {
                                //Scenariul cand adresa difera dar autovehiculul este acelasi
                                if (cod_adresa() != cod_adresa_controale() && cod_autovehicul() == cod_auto_client())
                                {
                                    DialogResult dialog = MessageBox.Show("Sigur doriti sa incheiati polita RCA", "Confirmare", MessageBoxButtons.YesNo);
                                    if (dialog == DialogResult.Yes)
                                    {
                                        //Modificare status adresa veche,creare noua adresa si modificare adresa in client
                                        bool status_adresa = false;
                                        int cod_Adresa = cod_adresa();
                                        DatabaseAcces.ModificaStatusAdresa(cod_Adresa, status_adresa);
                                        int cheie_adresa = cheie_primara_adresa();
                                        Adresa adr = creeazaAdresa(cheie_adresa);
                                        listaAdresa.Add(adr);
                                        DatabaseAcces.AdaugaAdresa(adr);
                                        DatabaseAcces.UpdateClientAdresa(cod_client(), cheie_adresa);
                                        //Incheiere polita rca pentru acelasi client si acelasi autovehicul dar cu alta adresa a clientului
                                        int cheie_polita = cod_polita();
                                        IncheiereRCA rca = creazaPolita(cheie_polita, cod_client(), cod_autovehicul());
                                        listaRCAFiltrata.Add(rca);
                                        DatabaseAcces.AdaugaPolitaRca(rca);
                                        DatabaseAcces.UpdateClient(cod_client(), textBoxemail.Text.Trim(), textBoxNrTelefon.Text.Trim());
                                        SalvarePdf(PrevPdf(cheie_polita), cheie_polita);
                                        SendEmail(textBoxemail.Text, SalvarePdf(PrevPdf(cheie_polita), cheie_polita));
                                        MessageBox.Show("Polita Rca incheiata cu succes!");
                                        clearControls();
                                        buttonInainte.Enabled = false;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Actiune anulata!");
                                    }
                                }
                                else
                                {
                                    //Scenariul cand autovehiculul si adresa sunt diferite
                                    if (cod_adresa() != cod_adresa_controale() && cod_autovehicul() != cod_auto_client())
                                    {
                                        DialogResult dialog = MessageBox.Show("Sigur doriti sa incheiati polita RCA", "Confirmare", MessageBoxButtons.YesNo);
                                        if (dialog == DialogResult.Yes)
                                        {
                                            //Modificare status adresa veche,creare noua adresa si modificare adresa in client
                                            bool status_adresa = false;
                                            int cod_Adresa = cod_adresa();
                                            DatabaseAcces.ModificaStatusAdresa(cod_Adresa, status_adresa);
                                            int cheie_adresa = cheie_primara_adresa();
                                            Adresa adr = creeazaAdresa(cheie_adresa);
                                            listaAdresa.Add(adr);
                                            DatabaseAcces.AdaugaAdresa(adr);
                                            DatabaseAcces.UpdateClientAdresa(cod_client(), cheie_adresa);
                                            //Creare obiect autovehicul nou pentru acelasi client
                                            int cod_Auto = cod_auto();
                                            Autovehicul auto = creeazaAuto(cod_Auto, cod_client());
                                            listaAuto.Add(auto);
                                            DatabaseAcces.AdaugaAuto(auto);
                                            //Incheierea politei RCA
                                            int cheie_polita = cod_polita();
                                            IncheiereRCA rca = creazaPolita(cheie_polita, cod_client(), cod_Auto);
                                            listaRCAFiltrata.Add(rca);
                                            DatabaseAcces.AdaugaPolitaRca(rca);
                                            DatabaseAcces.UpdateClient(cod_client(), textBoxemail.Text.Trim(), textBoxNrTelefon.Text.Trim());
                                            SalvarePdf(PrevPdf(cheie_polita), cheie_polita);
                                            SendEmail(textBoxemail.Text, SalvarePdf(PrevPdf(cheie_polita), cheie_polita));
                                            MessageBox.Show("Polita Rca incheiata cu succes!");                                          
                                            clearControls();
                                            buttonInainte.Enabled = false;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Actiune anulata!");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        DialogResult dialog = MessageBox.Show("Sigur doriti sa incheiati polita RCA", "Confirmare", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.Yes)
                        {
                            //Creare adresa pentru un client nou
                            int id_adresa = cheie_primara_adresa();
                            Adresa adr = creeazaAdresa(id_adresa);
                            listaAdresa.Add(adr);
                            DatabaseAcces.AdaugaAdresa(adr);
                            //Creare client nou
                            int id_client = 1;
                            List<Client> listaClienti = DatabaseAcces.ExtrageClient();
                            if (listaClienti.Count > 0)
                            {
                                id_client = listaClienti.Max(d => d.Cod_client) + 1;
                            }

                            Client cl = new Client()
                            {
                                Cod_client = id_client,
                                Nume = textBoxNume.Text.Trim(),
                                Prenume = textBoxPrenume.Text.TrimEnd(),
                                An_obtinere_permis = Convert.ToString(dateTimePickerObtinerePermis.Value),
                                Cnp = textBoxCnp.Text.Trim(),
                                Email = textBoxemail.Text.Trim(),
                                Nr_telefon = textBoxNrTelefon.Text.Trim(),
                                Cod_adresa = id_adresa,
                                Parola = null
                            };
                            listaClienti.Add(cl);
                            DatabaseAcces.AdaugaClient(cl);
                            //Creare autovehicul pentru un client nou
                            int cod_Auto = cod_auto();
                            Autovehicul auto = creeazaAuto(cod_Auto, id_client);
                            listaAuto.Add(auto);
                            DatabaseAcces.AdaugaAuto(auto);
                            //Creare polita rca pentru un client nou
                            int cheie_polita = cod_polita();
                            IncheiereRCA rca = creazaPolita(cheie_polita, id_client, cod_Auto);
                            listaRCAFiltrata.Add(rca);
                            DatabaseAcces.AdaugaPolitaRca(rca);
                            SalvarePdf(PrevPdf(cheie_polita),cheie_polita);
                            SendEmail(textBoxemail.Text, SalvarePdf(PrevPdf(cheie_polita), cheie_polita));
                            MessageBox.Show("Polita Rca incheiata cu succes!");                    
                            clearControls();
                            buttonInainte.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Actiune anulata!");
                        }
                    }
                }
            }
        }
        #endregion
        #region GenerarePdfSiTrimitereEmail
        string categoriaAuto()
        {
            foreach (Categorii cat in listaCategorii)
            {
                if (cat.Id_categorie == ((Categorii)comboBoxCategorie.SelectedItem).Id_categorie)
                {
                    return cat.Cod_categorie;
                }
            }
            return string.Empty;
        }
        string SubcategoriePDF()
        {
            int cod_categ = ((Categorii)comboBoxCategorie.SelectedItem).Id_categorie;
            foreach (Subcategorii sub in listaSubcategorii)
            {
                if (sub.Denumire_Subcategorie == comboBoxSubcategorie.Text && Enumerable.Range(Convert.ToInt32(sub.Masa_min), Convert.ToInt32(sub.Masa_max)).Contains(Convert.ToInt32(textBoxMasa.Text)) && sub.Id_categorie == cod_categ)
                {
                    return $"{sub.Denumire_Subcategorie} ({sub.Numar_locuri}, =<{sub.Masa_max})";
                }
            }
            return string.Empty;
        }
        void SendEmail(string email, string path)
        {
            try
            {     
                SmtpClient sc = new SmtpClient();
                sc.EnableSsl = true;            
                sc.Port = 587;
                sc.Host = "smtp.gmail.com";
                sc.EnableSsl = true;
                sc.Timeout = 10000;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential("state719@gmail.com", "suyu rfsm qoqg ncfq");
                string msg_subject = "PolitaRCA";
                string msg_body = "Polita RCA a fost emisa si o gasiti anexata acestui email";
                MailMessage mm = new MailMessage("noreplymessage08@gmail.com", email, msg_subject, msg_body);
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(path);
                mm.Attachments.Add(attachment);
                sc.Send(mm); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        string Beneficii()
        {
            foreach(BeneficiiSuplimentare ben in listaBeneficii)
            {
                if (ben.Denumire_pachet == comboBoxBeneficii.Text && ben.Procent_pachet == 0)
                {
                    return ben.Denumire_pachet;
                }
                else
                {
                    if (ben.Denumire_pachet == comboBoxBeneficii.Text && ben.Procent_pachet != 0)
                    {
                        return ben.Denumire_pachet + ", " + ben.continut_pachet;
                    }
                }
            }
            return string.Empty;
        }

        string SalvarePdf(Image bitmap,int cod_polita)
        {
            Document doc = new Document(iTextSharp.text.PageSize.A4);
            string path = $@"D:\Marian facultate\Licenta\Polita{textBoxNume.Text}{textBoxPrenume.Text}{cod_polita}.pdf";
            PdfWriter pdf = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            doc.Open();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                // Convertirea fluxului de date (MemoryStream) într-un array de bytes
                byte[] imageBytes = memoryStream.ToArray();
                // Crearea unei imagini iTextSharp din array-ul de bytes
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imageBytes);
                // Scalarea imaginii pentru a se încadra pe întreaga pagină
                img.ScaleToFit(doc.PageSize.Width, doc.PageSize.Height);

                // Calcularea poziției pentru a plasa imaginea în mijlocul paginii
                float x = (doc.PageSize.Width - img.ScaledWidth) / 2;
                float y = (doc.PageSize.Height - img.ScaledHeight) / 2;

                // Setarea poziției absolute a imaginii pe mijlocul paginii
                img.SetAbsolutePosition(x, y);
                // Adăugarea imaginii în document
                doc.Add(img);
            }
            doc.Close();
            return path;
        }

        Image PrevPdf(int cod_polita)
        {        
            Image bitmap = Bitmap.FromFile(@"D:\Marian facultate\Licenta\TemplateRca.jpg");
            Graphics grafic = Graphics.FromImage(bitmap);
            StringFormat polita = new StringFormat();
            polita.Alignment = StringAlignment.Far;
            //Adauga data inceput si data sfarsit
            grafic.DrawString(Convert.ToString(dateTimePickerDataInceput.Value.Day), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(40, 380));
            grafic.DrawString(Convert.ToString(dateTimePickerDataInceput.Value.Month), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(152, 380));
            grafic.DrawString(Convert.ToString(dateTimePickerDataInceput.Value.Year), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(240, 380));
            grafic.DrawString(Convert.ToString(Convert.ToDateTime(labelsfarsit.Text).Day), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(360, 380));
            grafic.DrawString(Convert.ToString(Convert.ToDateTime(labelsfarsit.Text).Month), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(470 , 380));
            grafic.DrawString(Convert.ToString(Convert.ToDateTime(labelsfarsit.Text).Year), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(560, 380));
            grafic.DrawString((cod_polita + 1000).ToString(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(880, 420));
            //Adauga date companie la pozitia 10
            grafic.DrawString("National Insurance S.A., \nStr. George Enescu nr.30,\nGalati Romania \nTelefon 0745124541 \nMail www.nationalinsurance.ro", new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(1260, 350));
            //Adauga numar inmatriculare,marca si categoria
            grafic.DrawString(textBoxNrInamtri.Text.ToUpper(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(250, 560));
            grafic.DrawString(comboBoxMarca.Text.ToUpper(), new System.Drawing.Font("Times New Roman", 8, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(940, 560));
            grafic.DrawString(categoriaAuto().ToUpper(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(770, 560));
            //Adauga nume si prenuma la pozitia 9
            grafic.DrawString(textBoxNume.Text.ToUpper() + " " + textBoxPrenume.Text.ToUpper(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(1650, 150));
            grafic.DrawString("Jud." + comboBoxJudet.Text + "; " + "Loc. " + textBoxLocalitate.Text
                 + "; Str. " + textBoxStrada.Text + "Nr. " + textBoxNumarstrada.Text, new System.Drawing.Font("Times New Roman", 9, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(1400, 200));
            //Adauga date personale
            grafic.DrawString(textBoxNume.Text.ToUpper() + " " + textBoxPrenume.Text.ToUpper(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(410, 1850));
            grafic.DrawString(textBoxCnp.Text, new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(410, 1910));
            grafic.DrawString(textBoxNume.Text.ToUpper() + " " + textBoxPrenume.Text.ToUpper(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(410, 1990));
            grafic.DrawString(textBoxCnp.Text, new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(410, 2050));
            grafic.DrawString("Jud." + comboBoxJudet.Text + "; " + "Loc. " + textBoxLocalitate.Text
                 + "; Str. " + textBoxStrada.Text, new System.Drawing.Font("Times New Roman", 9, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(410, 2110));
            grafic.DrawString("Nr. " + textBoxNumarstrada.Text + "; Bl " + textBoxBloc.Text + "; Sc " + textBoxScara.Text + "; Et " + textBoxEtaj.Text +
                "; Ap " + textBoxApartament.Text + "; Tel. " + textBoxNrTelefon.Text, new System.Drawing.Font("Times New Roman", 9, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(410, 2160));
            grafic.DrawString("Email " + textBoxemail.Text, new System.Drawing.Font("Times New Roman", 9, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(410, 2210));
            grafic.DrawString(textBoxNume.Text.ToUpper() + " " + textBoxPrenume.Text.ToUpper() + " " + textBoxCnp.Text, new System.Drawing.Font("Times New Roman", 9, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(500, 2320));
            //Adauga date autovehicul 
            grafic.DrawString(textBoxNrInamtri.Text.ToUpper(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(1920, 1910));
            grafic.DrawString(textBoxseriesasiu.Text.ToUpper(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(1820, 1985));
            grafic.DrawString(textBoxCapacitatecmc.Text + " / " + textBoxPutere.Text, new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(1910, 2050));
            grafic.DrawString(textBoxNumarLocuri.Text + " / " + textBoxMasa.Text, new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(1920, 2110));
            grafic.DrawString(SubcategoriePDF(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(1780, 1816));
            grafic.DrawString(comboBoxMarca.Text + " " + comboBoxModel.Text, new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(1820, 1860));
            //Adauga date contract
            grafic.DrawString(dateTimePickerDataInceput.Value.ToShortDateString(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(420, 2405));
            grafic.DrawString(labelsfarsit.Text, new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
              new SolidBrush(Color.Black), new Point(800, 2405));
            grafic.DrawString(DateTime.Now.ToShortDateString(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(1430, 2405));
            grafic.DrawString(labelPretPolita.Text, new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(380, 2455));
            grafic.DrawString(comboBoxBM.Text, new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(996, 2455));
            grafic.DrawString("-", new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(513, 2505));
            grafic.DrawString("1", new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(225, 2555));
            grafic.DrawString(labelPretPolita.Text + " Lei", new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(710, 2555));
            grafic.DrawString(DateTime.Now.ToShortDateString() + ".", new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(1350, 2545));
            grafic.DrawString("Numerar", new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(350, 2595));
            grafic.DrawString(DateTime.Now.ToShortDateString(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(1350, 2595));
            grafic.DrawString("6.070.000 euro", new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(1200, 2655));
            grafic.DrawString("1.220.000 euro", new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(1200, 2705));
            grafic.DrawString(Beneficii(), new System.Drawing.Font("Times New Roman", 8, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(500, 2775));

            //Adauga date angajat si date companie
            grafic.DrawString(DateAngajat.numeAngajat(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
            new SolidBrush(Color.Black), new Point(383, 1693));
            grafic.DrawString("RBK-792", new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
            new SolidBrush(Color.Black), new Point(1800, 1740));    
            return bitmap;
        }
        #endregion

        private void comboBoxJudet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (labelPretPolita.Text != "")
            {
                valoarePolita();
            }
        }

        private void comboBoxutilizare_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (labelPretPolita.Text != "")
            {
                valoarePolita();
            }
        }

        private void comboBoxDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (labelPretPolita.Text != "")
            {
                valoarePolita();
            }
        }

        private void comboBoxBeneficii_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (labelPretPolita.Text != "")
            {
                valoarePolita();
            }
        }

        private void comboBoxBM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (labelPretPolita.Text != "")
            {
                valoarePolita();
            }
        }
       
        private void buttonPrevPolita_Click(object sender, EventArgs e)
        {
            if (checkCompletedControls())
            {
                int cheie_polita = cod_polita();
                FormPrevizualizareRca form = new FormPrevizualizareRca(PrevPdf(cheie_polita), buttonIncheierePolita, buttonPrevPolita);
                form.ShowDialog();
            }
        }
    }
}

