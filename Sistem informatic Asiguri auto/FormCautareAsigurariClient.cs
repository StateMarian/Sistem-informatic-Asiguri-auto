using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace Sistem_informatic_Asiguri_auto
{
    public partial class FormCautareAsigurariClient : Form
    {
        public FormCautareAsigurariClient()
        {
            InitializeComponent();
        }
        List<IncheiereRCA> listaRCAFiltrata = DatabaseAcces.ExtrageRCA().Where(d => d.status_rca == true).ToList();
        List<Client> listaClienti = DatabaseAcces.ExtrageClient();
        List<Autovehicul> listaAuto = DatabaseAcces.ExtrageAutovehicul();
        List<IncheiereCasco> listaPoliteCasco = DatabaseAcces.ExtragePoliteCasco().Where(d => d.status_Casco == true).ToList();

        int cod_client()
        {
            foreach (Client cl in listaClienti)
            {
                if (cl.Cnp == textBoxCnp.Text.Trim())
                {
                    return cl.Cod_client;
                }
            }
            return 0;
        }
        int cod_adresa()
        {
            foreach (Client cl in listaClienti)
            {
                if (cl.Cnp == textBoxCnp.Text.Trim())
                {
                    return cl.Cod_adresa;
                }
            }
            return 0;
        }
        int cod_auto()
        {
            foreach (Autovehicul auto in listaAuto)
            {
                if (auto.Serie_sasiu.ToUpper() == textBoxSerieSasiu.Text.Trim().ToUpper() && auto.Nr_inmatriculare.ToUpper() == textBoxNumarInmatriculare.Text.Trim().ToUpper())
                {
                    return auto.Cod_auto;
                }
            }
            return 0;
        }
        string denumireDurata()
        {
            List<IncheiereRCA> listaRCA = DatabaseAcces.ExtrageRCA();
            List<DurataAsigurare> durata = DatabaseAcces.ExtrageDurataAsigurare().Where(d => d.status_durata == true && d.Tip_asigurare == "RCA").ToList();
            string den = string.Empty;
            foreach (IncheiereRCA rca in listaRCA)
            {
                if (rca.Cod_client == cod_client() && rca.Cod_auto == cod_auto())
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
        string denumireDurataCASCO()
        {
            List<IncheiereCasco> listaCasco = DatabaseAcces.ExtragePoliteCasco();
            List<DurataAsigurare> durata = DatabaseAcces.ExtrageDurataAsigurare().Where(d => d.status_durata == true && d.Tip_asigurare == "RCA").ToList();
            string den = string.Empty;
            foreach (IncheiereCasco casc in listaCasco)
            {
                if (casc.Cod_client == cod_client() && casc.Cod_auto == cod_auto())
                {
                    foreach (DurataAsigurare dur in durata)
                    {
                        if (casc.Id_durata == dur.Id_durata)
                        {
                            den = dur.Durata;
                        }
                    }
                }
            }
            return den;
        }
        DateTime setDate(DateTime data)
        {
            int luni = 0;
            string durata = denumireDurata();
            if (durata == "12 luni")
            {
                luni = 12;
            }
            else
            {
                luni = 6;
            }
            DateTime date = new DateTime();
            date = data.AddMonths(luni);
            return date;
        }
        DateTime setDateCasco(DateTime data)
        {
            int luni = 0;
            string durata = denumireDurataCASCO();
            if (durata == "12 luni")
            {
                luni = 12;
            }
            else
            {
                luni = 6;
            }
            DateTime date = new DateTime();
            date = data.AddMonths(luni);
            return date;
        }
        string Valabilitate(DateTime data)
        {

            DateTime data_curenta = DateTime.Now;
            TimeSpan diferenta = data_curenta - data;
            float luni = diferenta.Days;
            return Convert.ToString(luni);
        }
        void VerificareRca()
        {
            int cod_autove = cod_auto();
            int cod_cl = cod_client();
            int cod_adr = cod_adresa();
            List<IncheiereRCA> listaPoliteRcaFiltrata = DatabaseAcces.ExtrageRCA().Where(d => d.status_rca == true && d.Cod_client == cod_cl && d.Cod_auto == cod_autove).ToList();
            if (!(Verificari.checkCnp(textBoxCnp.Text)) || textBoxCnp.Text.Length != 13 || string.IsNullOrEmpty(textBoxCnp.Text))
            {
                MessageBox.Show("Cnp introdus nu respecta formatul unui Cnp!");
            }
            else
            {
                if (string.IsNullOrEmpty(textBoxSerieSasiu.Text))
                {
                    MessageBox.Show("Va rog completati seria de sasiu!");
                }
                else
                {
                    if (string.IsNullOrEmpty(textBoxNumarInmatriculare.Text))
                    {
                        MessageBox.Show("Va rog completati numarul de inmatriculare!");
                    }
                    else
                    {
                        if (cod_client() == 0)
                        {
                            MessageBox.Show("Nu exista nici un client pentru cnp-ul introdus!");
                        }
                        else
                        {
                            if (cod_auto() == 0)
                            {
                                MessageBox.Show("Pentru datele introduse autovehiculul nu exista in sistem!");
                            }
                            else
                            {
                                if (listaPoliteRcaFiltrata.Count != 0)
                                {
                                    foreach (IncheiereRCA rca in listaPoliteRcaFiltrata)
                                    {

                                        labelDataVerificare.Text = $"La data de {dateTimePickerDataVerificare.Value.ToShortDateString()}, cu datele introduse";
                                        labelCnpverificare.Text = $"CNP {textBoxCnp.Text}";
                                        labelSerieSasiuVerificare.Text = $"Serie sasiu {textBoxSerieSasiu.Text.ToUpper()}";
                                        labelNumarinmatrverificare.Text = $"Numar inmatriculare {textBoxNumarInmatriculare.Text.ToUpper()}";
                                        labelDataInceput.Text = "Data inceput valabilitate " + Convert.ToDateTime(rca.Data_inceput).ToShortDateString();
                                        labelDate.Text = "Autovehiculul are o polita RCA valabila cu urmatoarele date";
                                        DateTime dataSfarsit = setDate(Convert.ToDateTime(rca.Data_inceput));
                                        labelDataSfarsit.Text = "Data sfarsit valabilitate " + dataSfarsit.ToShortDateString();
                                        labelValabilitate.Text = "Valabila " + " " + Valabilitate(setDate(Convert.ToDateTime(rca.Data_inceput))) + " zile";
                                        textBoxCnp.Clear();
                                        textBoxNumarInmatriculare.Clear();
                                        textBoxSerieSasiu.Clear();
                                        groupBoxTipCasco.Enabled = false;
                                        buttonDescarca.Visible = true;
                                        buttonTrimiteEmail.Visible = true;
                                        Client(cod_cl);
                                        Adresa(cod_adr);
                                        Autovehicul(cod_autove);
                                        Rca(cod_cl, cod_autove);
                                        Beneficii();
                                        BonusMalus();
                                        SubcategoriePDF();
                                        CategoriePDF();
                                        Model();
                                        Marca();
                                        break;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Nu exista polite valabile in sistem pentru datele introduse de utilizator!");
                                    textBoxCnp.Clear();
                                    textBoxNumarInmatriculare.Clear();
                                    textBoxSerieSasiu.Clear(); 
                                }
                            }
                        }
                    }
                }
            }
        }

        void VerificareCasco()
        {
            List<IncheiereCasco> listaPoliteCascoFiltrata = DatabaseAcces.ExtragePoliteCasco().Where(d => d.status_Casco == true && d.Cod_client == cod_client() && d.Cod_auto == cod_auto()).ToList();
            if (!(Verificari.checkCnp(textBoxCnp.Text)) || textBoxCnp.Text.Length != 13 || string.IsNullOrEmpty(textBoxCnp.Text))
            {
                MessageBox.Show("Cnp introdus nu respecta formatul unui Cnp!");
            }
            else
            {
                if (string.IsNullOrEmpty(textBoxSerieSasiu.Text))
                {
                    MessageBox.Show("Va rog completati seria de sasiu!");
                }
                else
                {
                    if (string.IsNullOrEmpty(textBoxNumarInmatriculare.Text))
                    {
                        MessageBox.Show("Va rog completati numarul de inmatriculare!");
                    }
                    else
                    {
                        if (cod_client() == 0)
                        {
                            MessageBox.Show("Nu exista nici un client pentru cnp-ul introdus!");
                        }
                        else
                        {
                            if (cod_auto() == 0)
                            {
                                MessageBox.Show("Pentru datele introduse pentru autovehicul nu exista in sistem!");
                            }
                            else
                            {
                                if (listaPoliteCascoFiltrata.Count != 0)
                                {
                                    foreach (IncheiereCasco casc in listaPoliteCascoFiltrata)
                                    {
                                        int cod_autove = cod_auto();
                                        int cod_cl = cod_client();
                                        int cod_adr = cod_adresa();
                                        labelDataVerificare.Text = $"La data de {dateTimePickerDataVerificare.Value.ToShortDateString()}, cu datele introduse";
                                        labelCnpverificare.Text = $"CNP {textBoxCnp.Text}";
                                        labelSerieSasiuVerificare.Text = $"Serie sasiu {textBoxSerieSasiu.Text.ToUpper()}";
                                        labelNumarinmatrverificare.Text = $"Numar inmatriculare {textBoxNumarInmatriculare.Text.ToUpper()}";
                                        labelDataInceput.Text = "Data inceput valabilitate " + Convert.ToDateTime(casc.Data_inceput).ToShortDateString();
                                        labelDate.Text = "Autovehiculul are o polita CASCO valabila cu urmatoarele date";
                                        DateTime dataSfarsit = setDate(Convert.ToDateTime(casc.Data_inceput));
                                        labelDataSfarsit.Text = "Data sfarsit valabilitate " + dataSfarsit.ToShortDateString();
                                        labelValabilitate.Text = "Valabila " + " " + Valabilitate(setDateCasco(Convert.ToDateTime(casc.Data_inceput))) + " zile";
                                        textBoxCnp.Clear();
                                        textBoxNumarInmatriculare.Clear();
                                        textBoxSerieSasiu.Clear();
                                        groupBoxTipCasco.Enabled = false;
                                        buttonDescarca.Visible = true;
                                        buttonTrimiteEmail.Visible = true;
                                        Client(cod_cl);
                                        Adresa(cod_adr);
                                        Autovehicul(cod_autove);
                                        Casco(cod_cl, cod_autove);
                                        SubcategoriePDFCasco();
                                        Model();
                                        Marca();
                                        Casco();
                                        Fransiza();
                                        riscuriAsigurare();
                                        clauzeSuplimentare();
                                        break;

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Nu exista polite valabile in sistem pentru datele introduse de utilizator!");
                                    textBoxCnp.Clear();
                                    textBoxNumarInmatriculare.Clear();
                                    textBoxSerieSasiu.Clear();
                                }
                            }
                        }
                    }
                }
            }
        }

        string tipCascoRadio()
        {
            foreach(RadioButton radio in groupBoxTipCasco.Controls)
            {
                if(radio.Checked)
                {
                    return radio.Text;
                }
            }
            return string.Empty;
        }

        private void buttonCauta_Click(object sender, EventArgs e)
        {
            if (tipCascoRadio() == "RCA")
            {
                VerificareRca();
                buttonCauta.Enabled = false;
            }
            else
            {
                VerificareCasco();
                buttonCauta.Enabled = false;
            }
        }

        string nume = string.Empty;
        string prenume = string.Empty;
        string cnp = string.Empty;
        string email = string.Empty;
        string nrTelefon = string.Empty;
        string anObtinerePermis = string.Empty;

        void Client(int cod_client)
        {
            foreach (Client cl in listaClienti)
            {
                if (cl.Cod_client == cod_client)
                {
                    nume = cl.Nume;
                    prenume = cl.Prenume;
                    cnp = cl.Cnp;
                    email = cl.Email;
                    nrTelefon = cl.Nr_telefon;
                    anObtinerePermis = cl.An_obtinere_permis;
                }
            }
        }

        string judet = string.Empty;
        string localitate = string.Empty;
        string strada = string.Empty;
        string nr_strada = string.Empty;
        string bloc = string.Empty;
        string scara = string.Empty;
        string etaj = string.Empty;
        string apartament = string.Empty;
        void Adresa(int cod_adresa)
        {
            List<Adresa> listaAdresa = DatabaseAcces.listaAdresaPdf(cod_adresa);
            foreach (Adresa adr in listaAdresa)
            {
                judet = adr.Judet;
                localitate = adr.Localitate;
                strada = adr.Strada;
                nr_strada = adr.Nr_strada;
                bloc = adr.Bloc;
                scara = adr.Scara;
                etaj = adr.Etaj;
                apartament = adr.Apartament;
            }
        }
        string serieSasiu = string.Empty;
        string nr_inmatr = string.Empty;
        string an_fabr = string.Empty;
        int capacitate = 0;
        int putere = 0;
        int nr_locuri = 0;
        int masa_maxima = 0;
        int cod_model = 0;



        void Autovehicul(int cod_auto)
        {
            foreach (Autovehicul auto in listaAuto)
            {
                if (auto.Cod_auto == cod_auto)
                {
                    serieSasiu = auto.Serie_sasiu;
                    nr_inmatr = auto.Nr_inmatriculare;
                    an_fabr = auto.An_fabricatie;
                    capacitate = auto.Capacitate_cilindrica;
                    putere = auto.Putere;
                    nr_locuri = auto.Nr_locuri;
                    masa_maxima = auto.Masa_maxima_autorizata;
                    cod_model = auto.Id_model;
                }
            }
        }
        DateTime dataInceput;
        DateTime dataSfarsit;
        DateTime dataEmiterii;
        decimal valoareRca = 0;
        int idBeneficii = 0;
        int idBonus = 0;
        int idSubcategorie = 0;
        int cod_polita = 0;
       
        void Rca(int cod_cl, int cod_auto)
        {
            foreach (IncheiereRCA rca in listaRCAFiltrata)
            {
                if (rca.Cod_client == cod_cl && rca.Cod_auto == cod_auto)
                {
                    dataInceput = Convert.ToDateTime(rca.Data_inceput);
                    dataSfarsit = setDate(Convert.ToDateTime(rca.Data_inceput));
                    valoareRca = rca.Valoare_politaRCA;
                    dataEmiterii = Convert.ToDateTime(rca.Data_emiterii);
                    idBeneficii = rca.Id_beneficiu;
                    idBonus = rca.Id_bonus_malus;
                    idSubcategorie = rca.Id_subcategorie;
                    cod_polita = rca.Cod_polita_primary_key;
                    break;
                }
            }
        }

        DateTime dataInceputCasco;
        DateTime dataSfarsitCasco;
        DateTime dataEmiteriiCasco;
        float valoareCasco = 0;
        int subcategorieCasco = 0;
        int utilizare = 0;
        int nrKm=0;
        int idFransiza=0;
        int idCasco = 0;
        void Casco(int cod_cl, int cod_auto)
        {
            foreach (IncheiereCasco casc in listaPoliteCasco)
            {
                if (casc.Cod_client == cod_cl && casc.Cod_auto == cod_auto)
                {
                    dataInceputCasco = Convert.ToDateTime(casc.Data_inceput);
                    dataSfarsitCasco = setDateCasco(Convert.ToDateTime(casc.Data_inceput));
                    valoareCasco = casc.Valoare_Casco;
                    dataEmiteriiCasco = Convert.ToDateTime(casc.Data_emiterii);
                    idSubcategorie = casc.Id_subcategorie;
                    cod_polita = casc.Cod_casco_primary_key;
                    subcategorieCasco = casc.Id_subcategorie;
                    utilizare = casc.Id_utilizare;
                    nrKm = casc.Nr_kilometri;
                    idFransiza = casc.Id_fransiza;
                    idCasco = casc.Id_casco;
                    break;
                }
            }
        }
        string tipCasco = string.Empty;
        void Casco()
        {
            List<Tip_casco> listatipCasco = DatabaseAcces.ExtrageTipCasco().Where(d => d.status_tipCasco == true).ToList();
            foreach (Tip_casco tip in listatipCasco)
            {
                if (tip.Id_casco == idCasco)
                {
                    tipCasco = tip.Denumire_casco;
                }
            }
        }
        string denFran = string.Empty;
        int procentFran = 0;
        void Fransiza()
        {
            List<Fransiza> fransize = DatabaseAcces.ExtrageFransiza().Where(d => d.status_fransiza == true).ToList();
            foreach(Fransiza fran in fransize)
            {
                if(fran.Id_fransiza==idFransiza)
                {
                    denFran = fran.Tip_fransiza;
                    procentFran = fran.Procent;
                }
            }
        }

        string domeniuUtilizare = string.Empty;

        void DomUtilizare()
        {
            List<DomeniiUtilizare> utilizari = DatabaseAcces.ExtrageDomeniiUtilizare().Where(d => d.status_domeniu == true).ToList();
            foreach (DomeniiUtilizare dom in utilizari)
            {
                if(dom.Id_utilizare==utilizare)
                {
                    domeniuUtilizare = dom.Denumire_utilizare;
                }    
            }
        }

        string subcategorieCasc= string.Empty;
        void SubcategoriePDFCasco()
        {
            List<Subcategorii> listaSubcategorii = DatabaseAcces.ExtrageSubcategorii().Where(d => d.status_subcategorie == true).ToList();
            foreach (Subcategorii sub in listaSubcategorii)
            {
                if (sub.Id_subcategorie == subcategorieCasco)
                {
                    subcategorieCasc = sub.Denumire_Subcategorie;
                }
            }
        }

        string beneficiiSuplimentare = string.Empty;
        void Beneficii()
        {
            List<BeneficiiSuplimentare> beneficii = DatabaseAcces.ExtrageBeneficii().Where(d => d.status_beneficiu == true).ToList();
            foreach (BeneficiiSuplimentare ben in beneficii)
            {
                if (ben.Id_beneficiu == idBeneficii)
                {
                    beneficiiSuplimentare = ben.Denumire_pachet + " " + ben.continut_pachet;
                }
            }
        }
        string bonusMalus = string.Empty;
        void BonusMalus()
        {
            List<Bonus_Malus_Class> listaBonus = DatabaseAcces.ExtrageClaseBonusMalus().Where(d => d.status_bonus == true).ToList();
            foreach (Bonus_Malus_Class bon in listaBonus)
            {
                if (bon.Id_bonus_malus == idBonus)
                {
                    bonusMalus = bon.Bonus_Malus;
                }
            }
        }
        string Subcategorie = string.Empty;
        int cod_categorie = 0;
        void SubcategoriePDF()
        {
            List<Subcategorii> listaSubcategorii = DatabaseAcces.ExtrageSubcategorii().Where(d => d.status_subcategorie == true).ToList();
            foreach (Subcategorii sub in listaSubcategorii)
            {
                if (sub.Id_subcategorie == idSubcategorie)
                {
                    Subcategorie = $"{sub.Denumire_Subcategorie} ({sub.Numar_locuri}, =<{sub.Masa_max})";
                    cod_categorie = sub.Id_categorie;
                }
            }
        }
        string codCat = string.Empty;
        void CategoriePDF()
        {
            List<Categorii> listaCategorii = DatabaseAcces.ExtrageCategorii().Where(d => d.status_categorie == true).ToList();
            foreach (Categorii cat in listaCategorii)
            {
                if (cat.Id_categorie == cod_categorie)
                {
                    codCat = cat.Cod_categorie;
                }
            }
        }
        string denModel = string.Empty;
        string denVarianta = string.Empty;
        int Idmarca = 0;
        void Model()
        {
            List<Model> listaModel = DatabaseAcces.ExtrageModel().Where(d => d.status_model == true).ToList();
            foreach (Model mod in listaModel)
            {
                if (mod.Id_model == cod_model)
                {
                    denModel = mod.Denumire_model;
                    denVarianta = mod.Varianta;
                    Idmarca = mod.Id_marca;
                }
            }
        }
        string marca = string.Empty;
        void Marca()
        {
            List<Marca> listaMarca = DatabaseAcces.ExtrageMarca().Where(d => d.status_marca == true).ToList();
            foreach (Marca marc in listaMarca)
            {
                if (marc.Id_marca == Idmarca)
                {
                    marca = marc.Denumire_marca;
                }
            }
        }
        #region PDFRCA
        Image PrevPdf(int cod_polita)
        {
            Image bitmap = Bitmap.FromFile(@"D:\Marian facultate\Licenta\TemplateRca.jpg");
            Graphics grafic = Graphics.FromImage(bitmap);
            StringFormat polita = new StringFormat();
            polita.Alignment = StringAlignment.Far;
            //Adauga data inceput si data sfarsit
            grafic.DrawString(Convert.ToString(dataInceput.Day), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(40, 380));
            grafic.DrawString(Convert.ToString(dataInceput.Month), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(152, 380));
            grafic.DrawString(Convert.ToString(dataInceput.Year), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(240, 380));
            grafic.DrawString(Convert.ToString(dataSfarsit.Day), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(360, 380));
            grafic.DrawString(Convert.ToString(dataSfarsit.Month), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(470, 380));
            grafic.DrawString(Convert.ToString(dataSfarsit.Year), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(560, 380));
            grafic.DrawString((cod_polita + 1000).ToString(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(880, 420));
            //Adauga date companie la pozitia 10
            grafic.DrawString("National Insurance S.A., \nStr. George Enescu nr.30,\nGalati Romania \nTelefon 0745124541 \nMail www.nationalinsurance.ro", new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(1260, 350));
            //Adauga numar inmatriculare,marca si categoria
            grafic.DrawString(nr_inmatr, new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(250, 560));
            grafic.DrawString(marca.ToUpper(), new System.Drawing.Font("Times New Roman", 8, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(940, 560));
            grafic.DrawString(codCat.ToUpper(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(770, 560));
            //Adauga nume si prenuma la pozitia 9
            grafic.DrawString(nume.ToUpper() + " " + prenume.ToUpper(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(1650, 150));
            grafic.DrawString("Jud." + judet + "; " + "Loc. " + localitate + "; Str. " + strada + "Nr. " + nr_strada, new System.Drawing.Font("Times New Roman", 9, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(1400, 200));
            //Adauga date personale
            grafic.DrawString(nume.ToUpper() + " " + prenume.ToUpper(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(410, 1850));
            grafic.DrawString(cnp, new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(410, 1910));
            grafic.DrawString(nume.ToUpper() + " " + prenume.ToUpper(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(410, 1990));
            grafic.DrawString(cnp, new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(410, 2050));
            grafic.DrawString("Jud." + judet + "; " + "Loc. " + localitate
                 + "; Str. " + strada, new System.Drawing.Font("Times New Roman", 9, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(410, 2110));
            grafic.DrawString("Nr. " + nr_strada + "; Bl " + bloc + "; Sc " + scara + "; Et " + etaj +
                "; Ap " + apartament + "; Tel. " + nrTelefon, new System.Drawing.Font("Times New Roman", 9, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(410, 2160));
            grafic.DrawString("Email " + email, new System.Drawing.Font("Times New Roman", 9, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(410, 2210));
            grafic.DrawString(nume.ToUpper() + " " + prenume.ToUpper() + " " + textBoxCnp.Text, new System.Drawing.Font("Times New Roman", 9, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(500, 2320));
            //Adauga date autovehicul 
            grafic.DrawString(nr_inmatr, new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(1920, 1910));
            grafic.DrawString(serieSasiu, new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(1820, 1985));
            grafic.DrawString(capacitate + " / " + putere, new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(1910, 2050));
            grafic.DrawString(nr_locuri + " / " + masa_maxima, new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(1920, 2110));
            grafic.DrawString(Subcategorie, new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(1780, 1816));
            grafic.DrawString(marca + " " + denModel, new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(1820, 1860));
            //Adauga date contract
            grafic.DrawString(dataInceput.ToShortDateString(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(420, 2405));
            grafic.DrawString(dataSfarsit.ToShortDateString(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
              new SolidBrush(Color.Black), new Point(800, 2405));
            grafic.DrawString(dataEmiterii.ToShortDateString(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(1430, 2405));
            grafic.DrawString(Convert.ToString(valoareRca), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(380, 2455));
            grafic.DrawString(bonusMalus, new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(996, 2455));
            grafic.DrawString("-", new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(513, 2505));
            grafic.DrawString("1", new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(225, 2555));
            grafic.DrawString(Convert.ToString(valoareRca) + " Lei", new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(710, 2555));
            grafic.DrawString(dataEmiterii.ToShortDateString() + ".", new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(1350, 2545));
            grafic.DrawString("Numerar", new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(350, 2595));
            grafic.DrawString(dataEmiterii.ToShortDateString(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(1350, 2595));
            grafic.DrawString("6.070.000 euro", new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(1200, 2655));
            grafic.DrawString("1.220.000 euro", new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(1200, 2705));
            grafic.DrawString(beneficiiSuplimentare, new System.Drawing.Font("Times New Roman", 8, FontStyle.Regular),
               new SolidBrush(Color.Black), new Point(500, 2775));

            //Adauga date angajat si date companie
            grafic.DrawString(DateAngajat.numeAngajat(), new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
            new SolidBrush(Color.Black), new Point(383, 1693));
            grafic.DrawString("RBK-792", new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular),
            new SolidBrush(Color.Black), new Point(1800, 1740));
            return bitmap;

        }
        string SalvarePdf(Image bitmap, int cod_polita)
        {
            Document doc = new Document(iTextSharp.text.PageSize.A4);
            string path = $@"D:\Marian facultate\Licenta\Polita{nume}{prenume}{cod_polita}.pdf";
            iTextSharp.text.pdf.PdfWriter pdf = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
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

        private void buttonDescarca_Click(object sender, EventArgs e)
        {
            if (tipCascoRadio() == "RCA")
            {
                DialogResult dialog = MessageBox.Show("Sigur doriti sa descarcati polita!", "Confirmare", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    SalvarePdf(PrevPdf(cod_polita), cod_polita);
                    MessageBox.Show("Polita descarcata cu succes!");
                }
                else
                {
                    MessageBox.Show("Descarcare anulata!");
                }
            }
            else
            {
                DialogResult dialog = MessageBox.Show("Sigur doriti sa descarcati polita!", "Confirmare", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    SalvarePdfCasco(PrevPdfCasco(cod_polita), cod_polita);
                    MessageBox.Show("Polita Casco descarcata cu succes!");
                }
                else
                {
                    MessageBox.Show("Descarcare anulata!");
                }
            }
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
        private void buttonTrimiteEmail_Click(object sender, EventArgs e)
        {
            if (tipCascoRadio() == "RCA")
            {
                DialogResult dialog = MessageBox.Show("Sigur doriti sa trimiteti polita RCA pe email", "Confirmare", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    SendEmail(email, SalvarePdf(PrevPdf(cod_polita), cod_polita));
                    MessageBox.Show("Trimitere reusita!");
                }
                else
                {
                    MessageBox.Show("Trimitere anulata!");
                }
            }
            else
            {
                DialogResult dialog = MessageBox.Show("Sigur doriti sa trimiteti polita Casco pe email", "Confirmare", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    SendEmailCasco(email, SalvarePdfCasco(PrevPdfCasco(cod_polita), cod_polita));
                    MessageBox.Show("Trimitere reusita!");
                }
                else
                {
                    MessageBox.Show("Trimitere anulata!");
                }
            }
        }
        #endregion
        int varstaClient()
        {
            int an = Convert.ToInt32(cnp.Substring(1, 2));
            int luna = Convert.ToInt32(cnp.Substring(3, 2));
            int zi = Convert.ToInt32(cnp.Substring(5, 2));
            if (cnp[0] == '1' || cnp[0] == '2')
            {
                an += 1900;
            }
            else
            {
                if (cnp[0] == '5' || cnp[0] == '6')
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
        int vechimePermis()
        {
            int vechime = DateTime.Now.Year - Convert.ToDateTime(anObtinerePermis).Year;
            return vechime;
        }
        string riscuriAsociate = string.Empty;
        void riscuriAsigurare()
        {
            string riscuri = string.Empty;
            int contor = 0;
            List<AsociereRiscCasco> listaAsociereCascoRisc = DatabaseAcces.ExtrageRiscCasco().Where(d => d.status_asociere == true).ToList();
            List<RiscCasco> listaRiscuri = DatabaseAcces.ExtrageRiscuriCasco().Where(d => d.status_risc == true).ToList();
            foreach (AsociereRiscCasco asoc in listaAsociereCascoRisc)
            {
                if (idCasco == asoc.Id_casco)
                {
                    foreach (RiscCasco risc in listaRiscuri)
                    {
                        if (risc.id_risc == asoc.Id_risc)
                        {
                            riscuri += risc.Denumire_risc;
                            contor++;
                            if (contor == 4)
                            {
                                riscuri += "\n";
                            }
                            else if (contor % 11 == 0)
                            {
                                riscuri += "\n";
                            }
                            else
                            {
                                riscuri += ",";
                            }
                        }
                    }
                }
            }
            riscuriAsociate = riscuri;
        }
        string benereficiiSupl = string.Empty;
        void clauzeSuplimentare()
        {
            string clauze = string.Empty;
            List<AsociereCascoClauza> listaAsociereCascoClauze = DatabaseAcces.ExtrageClauzeCasco();
            List<Clauze_suplimentare> listaClauze = DatabaseAcces.ExtrageClauzeSuplimentare().Where(d => d.status_clauza == true).ToList();
            foreach (AsociereCascoClauza clauza in listaAsociereCascoClauze)
            {
                if (clauza.Id_casco == idCasco)
                {
                    foreach (Clauze_suplimentare cl in listaClauze)
                    {
                        if (cl.Id_clauza == clauza.Id_clauza)
                        {
                            clauze = clauze + "\n" + cl.Denumire_clauza;
                        }
                    }
                }
            }
            beneficiiSuplimentare = clauze;
        }
        Image PrevPdfCasco(int cod_polita)
        {
            Image bitmap = Bitmap.FromFile(@"D:\Marian facultate\Licenta\TemplateCasco.jpg");
            Graphics grafic = Graphics.FromImage(bitmap);
            StringFormat polita = new StringFormat();
            polita.Alignment = StringAlignment.Far;
            //Adauga date polita, nr polita si cod 
            grafic.DrawString("National", new System.Drawing.Font("Times New Roman", 13, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(25, 99));
            grafic.DrawString("National", new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(198, 122));
            grafic.DrawString("Galati", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(80, 341));
            grafic.DrawString("National", new System.Drawing.Font("Times New Roman", 13, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(25, 1405));
            grafic.DrawString("National", new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(197, 1429));
            grafic.DrawString("Galati Str. ALBATROSULUI, Nr. 1", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(80, 365));
            grafic.DrawString("15", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(705, 341));
            grafic.DrawString("0724124122", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(705, 365));
            grafic.DrawString(Convert.ToString(cod_polita + 1000), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(90, 500));
            grafic.DrawString(DateAngajat.numeAngajat().ToUpper(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(100, 522));
            grafic.DrawString("RAF358470 inregistrat la ASF", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(630, 524));
            //Adauga data inceput si data sfarsit
            grafic.DrawString(dataInceputCasco.ToShortDateString(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(135, 648));
            grafic.DrawString(dataSfarsitCasco.ToShortDateString(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(390, 648));
            grafic.DrawString(dataEmiteriiCasco.ToShortDateString(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(713, 647));
            //Adauga date Client
            grafic.DrawString(nume.ToUpper() + " " + prenume.ToUpper(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(290, 562));
            grafic.DrawString(cnp, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(93, 586));
            grafic.DrawString(strada.ToUpper(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(442, 586));
            grafic.DrawString(nr_strada, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(787, 586));
            grafic.DrawString(bloc, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(48, 608));
            grafic.DrawString(scara, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(125, 608));
            grafic.DrawString(apartament, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(204, 608));
            grafic.DrawString(localitate.ToUpper(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(315, 608));
            grafic.DrawString(judet.ToUpper(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(630, 609));
            //Adauga date autovehicul
            grafic.DrawString(marca.ToUpper(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(70, 687));
            grafic.DrawString(denModel.ToUpper() + " " + denVarianta.ToUpper(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(339, 687));
            grafic.DrawString(Convert.ToDateTime(an_fabr).ToShortDateString(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(110, 712));
            grafic.DrawString(capacitate + "/" + putere, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(470, 712));
            grafic.DrawString(serieSasiu, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(148, 734));
            grafic.DrawString(nr_inmatr, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(492, 735));
            grafic.DrawString(subcategorieCasc.ToUpper(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(652, 735));
            grafic.DrawString("Proprietar", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(154, 758));
            grafic.DrawString(domeniuUtilizare, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(573, 758));
            grafic.DrawString("RON", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(84, 780));
            grafic.DrawString("50.000", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(363, 780));
            grafic.DrawString("===", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(658, 780));
            grafic.DrawString("RON", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(212, 804));
            grafic.DrawString("1", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(440, 804));
            grafic.DrawString(Convert.ToString(valoareCasco), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(725, 804));
            grafic.DrawString("Numerar", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(168, 824));
            grafic.DrawString("--------", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(122, 862));
            grafic.DrawString("--------", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(600, 862));
            grafic.DrawString(Convert.ToString(valoareCasco), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(128, 883));
            grafic.DrawString(DateTime.Now.ToShortDateString(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(154, 906));
            grafic.DrawString("Persoana fizica", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(140, 960));
            grafic.DrawString(judet.ToUpper() + " - " + localitate.ToUpper(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(463, 960));
            grafic.DrawString(Convert.ToString(nrKm), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(445, 983));
            grafic.DrawString("Fransiza: ", new System.Drawing.Font("Times New Roman", 18, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(19, 1005));
            grafic.DrawString(denFran, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(90, 1005));
            grafic.DrawString("Procent Fransiza: ", new System.Drawing.Font("Times New Roman", 18, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(280, 1005));
            grafic.DrawString(Convert.ToString(procentFran), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(413, 1005));
            grafic.DrawString(Convert.ToString(varstaClient() + " ani"), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(71, 1047));
            grafic.DrawString(Convert.ToString(vechimePermis()) + " ani", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(466, 1047));
            grafic.DrawString("Casco " + tipCasco + " " + riscuriAsociate, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(19, 1082));
            grafic.DrawString(beneficiiSuplimentare, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(19, 1137));
            return bitmap;
        }
        string SalvarePdfCasco(Image bitmap, int cod_polita)
        {
            Document doc = new Document(iTextSharp.text.PageSize.A4);
            string path = $@"D:\Marian facultate\Licenta\PolitaCasco{nume}{prenume}{cod_polita}.pdf";
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
        void SendEmailCasco(string email, string path)
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
                string msg_subject = "PolitaCasco";
                string msg_body = "Polita Casco a fost emisa si o gasiti anexata acestui email";
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

        private void buttonCautareNoua_Click(object sender, EventArgs e)
        {
            groupBoxTipCasco.Enabled = true;
            buttonCauta.Enabled = true;
            buttonDescarca.Visible = false;
            buttonTrimiteEmail.Visible = false;
            labelDataVerificare.Text = "";
            labelCnpverificare.Text = "";
            labelSerieSasiuVerificare.Text = "";
            labelNumarinmatrverificare.Text = "";
            labelDate.Text = "";
            labelDataInceput.Text = "";
            labelDataSfarsit.Text = "";
            labelValabilitate.Text = "";
            textBoxCnp.Clear();
            textBoxNumarInmatriculare.Clear();
            textBoxSerieSasiu.Clear();
        }
    }
}
