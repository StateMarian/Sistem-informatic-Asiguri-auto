using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Image = System.Drawing.Image;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace Sistem_informatic_Asiguri_auto
{
    public partial class FormCalculatorIncheiereCasco : Form
    {
        public FormCalculatorIncheiereCasco()
        {
            InitializeComponent();
            AddDurataToCombobox();
            AddJudetToCombo();
            AddCategorieToCombo();
            AddMarcaCombobox();
            AddTariToCombo();
            AddCombToCombo();
            AddDomeniuToCombo();
            AddTipCascoToListbox();
        }
        #region ListeDate
        List<DurataAsigurare> durata = DatabaseAcces.ExtrageDurataAsigurare().Where(d => d.status_durata == true).ToList();
        List<ZonaGeografica> listaZone = DatabaseAcces.ExtrageZoneGeografice().Where(d => d.status_zona == true).ToList();
        List<Categorii> listaCategorii = DatabaseAcces.ExtrageCategorii().Where(d => d.status_categorie == true).ToList();
        List<Subcategorii> listaSubcategorii = DatabaseAcces.ExtrageSubcategorii().Where(d => d.status_subcategorie == true).ToList();
        List<DomeniiUtilizare> listaDomenii = DatabaseAcces.ExtrageDomeniiUtilizare().Where(d => d.status_domeniu == true).ToList();
        List<GrupeVarsta> listaGrupeVarsta = DatabaseAcces.ExtrageGrupe().Where(d => d.status_grupa == true).ToList();
        List<CapacitateCilindrica> listaCapacitate = DatabaseAcces.ExtrageCapacitate().Where(d => d.status_capacitate == true).ToList();
        List<Marca> listaMarca = DatabaseAcces.ExtrageMarca().Where(d => d.status_marca == true).ToList();
        List<Model> listaModel = DatabaseAcces.ExtrageModel().Where(d => d.status_model == true).ToList();
        List<Client> listaClienti = DatabaseAcces.ExtrageClient();
        List<Adresa> listaAdresa = DatabaseAcces.ExtrageAdresa();
        List<Autovehicul> listaAuto = DatabaseAcces.ExtrageAutovehicul();
        List<Tip_casco> listaCasco = DatabaseAcces.ExtrageTipCasco().Where(d => d.status_tipCasco == true).ToList();
        List<RiscCasco> listaRiscuri = DatabaseAcces.ExtrageRiscuriCasco().Where(d => d.status_risc == true).ToList();
        List<AsociereRiscCasco> listaAsociereCascoRisc = DatabaseAcces.ExtrageRiscCasco();
        List<AsociereCascoClauza> listaAsociereCascoClauze = DatabaseAcces.ExtrageClauzeCasco();
        List<Clauze_suplimentare> listaClauze = DatabaseAcces.ExtrageClauzeSuplimentare().Where(d => d.status_clauza == true).ToList();
        List<Calcul_prima_Casco> listaPretCasco = DatabaseAcces.ExtrageAsigurareCasco().Where(d => d.status_casco == true).ToList();
        List<IncheiereCasco> listaPoliteCasco = DatabaseAcces.ExtragePoliteCasco().Where(d => d.status_Casco == true).ToList();
        #endregion
        #region Adauga date controale
        void AddDurataToCombobox()
        {
            comboBoxDurata.DataSource = null;
            var listaFiltrata = durata
                .Where(d => d.Tip_asigurare == "CASCO")
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
            labelDataSfarsit.Text = date.ToShortDateString();
        }

        private void comboBoxDurata_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDate();
            if (labelPretPolitaCasco.Text != "")
            {
                valoarePolitaCasco();
            }
        }

        private void dateTimePickerDataInceput_ValueChanged(object sender, EventArgs e)
        {
            setDate();
        }
        void AddJudetToCombo()
        {
            comboBoxJudet.DataSource = null;
            comboBoxJudet.Sorted = true;
            var listaFiltrata = listaZone
                .Where(d => d.Tip_Asigurare.ToUpper() == "CASCO")
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

        private void comboBoxCategorie_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddSubcategoriiToComboBox();
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
        void AddCombToCombo()
        {
            List<string> listaComb = new List<string> { "Benzina", "Motorina", "Benzina&GPL", "Hybrid", "Alt tip combustibil" };
            comboBoxCombustibil.DataSource = null;
            comboBoxCombustibil.DataSource = listaComb;
        }
        void AddDomeniuToCombo()
        {
            comboBoxDomeniuUtilizare.DataSource = null;
            comboBoxDomeniuUtilizare.DataSource = listaDomenii;
            comboBoxDomeniuUtilizare.DisplayMember = "Denumire_utilizare";
        }
        void AddTipCascoToListbox()
        {
            comboBoxTipCasco.DataSource = null;
            var listaFiltrata = listaCasco
                .Select(d => d.Denumire_casco)
                .Distinct()
                .ToList();
            listaFiltrata.Sort();
            comboBoxTipCasco.DataSource = listaFiltrata;
            comboBoxTipCasco.DisplayMember = "Denumire_casco";
        }
        void AddDenFransiza()
        {
            List<Fransiza> listaFran = DatabaseAcces.ExtrageFransizaDupaCasco(comboBoxTipCasco.Text);
            comboBoxFransiza.DataSource = null;
            var listaFransiza = listaFran
                .Select(d => d.Tip_fransiza)
                .Distinct()
                .ToList();
            listaFransiza.Sort();
            comboBoxFransiza.DataSource = listaFransiza;
            comboBoxFransiza.DisplayMember = "Tip_Fransiza";
        }

        private void comboBoxMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddModelToCombo();
        }

        private void comboBoxModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddVariantaToCombo();
        }

        private void comboBoxTipCasco_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddDenFransiza();
            AddRiscuriToListBox();
            AddClauzeToListBox();
            CheckedRadio();
        }
        public static RadioButton CreateRadiobutton(string nume, string text, int leftPos, int topPos)
        {
            RadioButton radioButton = new RadioButton();
            radioButton.AutoSize = true;
            radioButton.Location = new System.Drawing.Point(leftPos, topPos);
            radioButton.Name = nume;
            radioButton.Text = text;
            return radioButton;
        }
        void CreateRadioButtonProcent()
        {
            var selectedFransiza = comboBoxFransiza.Text;
            List<Fransiza> listaFran = DatabaseAcces.ExtrageFransizaDupaCasco(comboBoxTipCasco.Text);
            int leftPosition = 5;
            var listaFrans = listaFran.Where(d => d.Tip_fransiza == selectedFransiza).ToList();
            groupBoxProcenteFransiza.Controls.Clear();
            foreach (Fransiza fran in listaFrans)
            {
                RadioButton radioButton = CreateRadiobutton(fran.Tip_fransiza + fran.Procent, Convert.ToString(fran.Procent), leftPosition, 25);
                groupBoxProcenteFransiza.Controls.Add(radioButton);
                radioButton.CheckedChanged += RadioButton_CheckedChanged;
                leftPosition += 80;
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            AddRiscuriToListBox();
            AddClauzeToListBox();
            if (labelPretPolitaCasco.Text != "")
            {
               valoarePolitaCasco();
            }
        }

        int Procent_groupBox()
        {
            foreach (Control control in groupBoxProcenteFransiza.Controls)
            {
                if (control is RadioButton radioButton)
                {
                    if (radioButton.Checked)
                    {
                        return Convert.ToInt32(radioButton.Text);
                    }
                }
            }
            return 1;
        }

        void CheckedRadio()
        {
            List<Fransiza> listaFran = DatabaseAcces.ExtrageFransizaDupaCasco(comboBoxTipCasco.Text);
            bool fransizaFaraFransizaExista = false;

            foreach (Fransiza fran in listaFran)
            {
                if (fran.Procent == 0 && fran.Tip_fransiza == "Fara fransiza")
                {
                    fransizaFaraFransizaExista = true;
                    break;
                }
            }

            if (fransizaFaraFransizaExista)
            {
                foreach (Control control in groupBoxProcenteFransiza.Controls)
                {
                    if (control is RadioButton radioButton)
                    {
                        if (radioButton.Name.StartsWith("Fara fransiza"))
                        {
                            radioButton.Checked = true;
                        }
                        else
                        {
                            radioButton.Checked = false;
                        }
                    }
                }
            }
        }
        void AddRiscuriToListBox()
        {
            listBoxRiscuri.Items.Clear();
            foreach (AsociereRiscCasco asoc in listaAsociereCascoRisc)
            {
                if (Cod_casco() == asoc.Id_casco)
                {
                    foreach (RiscCasco risc in listaRiscuri)
                    {
                        if (risc.id_risc == asoc.Id_risc)
                        {
                            listBoxRiscuri.Items.Add(risc.Denumire_risc);
                        }
                    }
                }
            }
        }

        void AddClauzeToListBox()
        {
            listBoxClauzeSuplimentare.Items.Clear();
            foreach (AsociereCascoClauza clauza in listaAsociereCascoClauze)
            {
                if (clauza.Id_casco == Cod_casco())
                {
                    foreach (Clauze_suplimentare cl in listaClauze)
                    {
                        if (cl.Id_clauza == clauza.Id_clauza)
                        {
                            listBoxClauzeSuplimentare.Items.Add(cl.Denumire_clauza);
                        }
                    }
                }
            }
        }

        private void comboBoxFransiza_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateRadioButtonProcent();
            AddRiscuriToListBox();
            AddClauzeToListBox();
            CheckedRadio();
        }
        #endregion
        //Extrage cod_casco pe baza selectiei din controale
        int Cod_casco()
        {
            int cod_casco = 0;
            string denFran = comboBoxFransiza.Text as string;
            string denCasc = comboBoxTipCasco.Text as string;
            int procent = Convert.ToInt32(Procent_groupBox());
            List<Fransiza> listaFran = DatabaseAcces.ExtrageFransizaDupaCasco(comboBoxTipCasco.Text);
            int cod_fran = 0;
            foreach (Fransiza fran in listaFran)
            {
                if (fran.Tip_fransiza == denFran && fran.Procent == procent)
                {
                    cod_fran = fran.Id_fransiza;
                }
            }
            foreach (Tip_casco casc in listaCasco)
            {
                if (casc.Denumire_casco == denCasc && casc.Id_fransiza == cod_fran)
                {
                    cod_casco = casc.Id_casco;
                }
            }
            return cod_casco;
        }
        int Cod_fransiza()
        {
            int procent = Convert.ToInt32(Procent_groupBox());
            string denFran = comboBoxFransiza.Text as string;
            List<Fransiza> listaFran = DatabaseAcces.ExtrageFransizaDupaCasco(comboBoxTipCasco.Text);
            int cod_fran = 0;
            foreach (Fransiza fran in listaFran)
            {
                if (fran.Tip_fransiza == denFran && fran.Procent == procent)
                {
                    cod_fran = fran.Id_fransiza;
                }
            }
            return cod_fran;
        }
        int vechimePermis()
        {
            int vechime = DateTime.Now.Year - dateTimePickerAnObtinerePermis.Value.Year;
            return vechime;
        }
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
                if (sub.Denumire_Subcategorie == comboBoxSubcategorie.Text && Enumerable.Range(Convert.ToInt32(sub.Masa_min), Convert.ToInt32(sub.Masa_max)).Contains(Convert.ToInt32(textBoxMasa.Text)) && sub.Id_categorie == cod_categ)
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
                        if (cap.Min_capacitate <= Convert.ToInt32(textBoxCapacitate.Text.Trim()) && cap.Max_capacitate >= Convert.ToInt32(textBoxCapacitate.Text.Trim()) && cap.Putere == 0)
                        {
                            cod_cap = cap.Id_capacitate;
                            break;
                        }
                    }
                    else
                    {
                        if (comboBoxCategorie.Text == DateAngajat.Mopede && cap.Putere == 1)
                        {
                            if (cap.Min_capacitate <= Convert.ToInt32(textBoxCapacitate.Text.Trim()) && cap.Max_capacitate >= Convert.ToInt32(textBoxCapacitate.Text.Trim()))
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

        float ValoarePrimaCasco()
        {
            foreach (Calcul_prima_Casco casc in listaPretCasco)
            {
                if (casc.Id_subcategorie == cod_Subcategorie() && casc.Id_grupa == codGrupaVarsta() && casc.Id_capacitate == cod_cap() && casc.Id_model == cod_model())
                {
                    return casc.Valoare_prima_casco;
                }
            }
            return 0;
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
        int procentFransiza()
        {
            List<Fransiza> listaFran = DatabaseAcces.ExtrageFransizaDupaCasco(comboBoxTipCasco.Text);
            foreach (Fransiza fran in listaFran)
            {
                if (fran.Id_fransiza == Cod_fransiza())
                {
                    return fran.Procent_reducere;
                }
            }
            return 0;
        }
        //Extrage procent zonei geografice pe baza selectiei din combobox
        float procentJudet()
        {
            float procent = 0;
            foreach (ZonaGeografica zon in listaZone)
            {
                if (zon.Judet == comboBoxJudet.Text && zon.Tip_Asigurare == "CASCO")
                {
                    procent = zon.Procent;
                }
            }
            return procent;
        }
        float procentUtilizare()
        {
            float procent = 0;
            foreach (DomeniiUtilizare dom in listaDomenii)
            {
                if (dom.Denumire_utilizare == comboBoxDomeniuUtilizare.Text)
                {
                    procent = dom.Procent_utilizare;
                }
            }
            return procent;
        }
        float valoareClauzeSuplimentare()
        {
            float valoare = 0;
            foreach (AsociereCascoClauza clauza in listaAsociereCascoClauze)
            {
                if (clauza.Id_casco == Cod_casco())
                {
                    foreach (Clauze_suplimentare cl in listaClauze)
                    {
                        if (cl.Id_clauza == clauza.Id_clauza)
                        {
                            valoare = valoare + clauza.Valoare_clauza;
                        }
                    }
                }
            }
            return valoare;
        }
        //Calculare pret polita si afisarea pe interfata
        void valoarePolitaCasco()
        {
            float pretPolita = ValoarePrimaCasco();
            pretPolita = pretPolita + (pretPolita * procentJudet()) / 100 + (pretPolita * procentUtilizare()) / 100 + valoareClauzeSuplimentare() - (((pretPolita * procentDurata()) / 100) + (pretPolita * procentFransiza()) / 100);
            string numarFormatat = pretPolita.ToString("0.00");
            labelPretPolitaCasco.Text = Convert.ToString(numarFormatat);
        }
        void enableButtonInainte()
        {
            if (labelPretPolitaCasco.Text != "")
            {
                buttonInainte.Enabled = true;
            }
        }

        bool VerificaSelectieFransiza()
        {
            foreach (Control control in groupBoxProcenteFransiza.Controls)
            {
                if (control is RadioButton radioButton)
                {
                    if (radioButton.Checked)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void buttonCalculPrimaCasco_Click(object sender, EventArgs e)
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
                            if (textBoxSerieSasiu.Text == "")
                            {
                                MessageBox.Show("Campul pentru seria VIN nu poate ramane necompletat!");
                            }
                            else
                            {
                                if (textBoxNumarInmatriculare.Text == "")
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
                                            if (!Verificari.checkCnp(textBoxCapacitate.Text) || string.IsNullOrEmpty(textBoxCapacitate.Text))
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
                                                    if (!Verificari.checkCnp(textBoxNumarKM.Text) || string.IsNullOrEmpty(textBoxNumarKM.Text))
                                                    {
                                                        MessageBox.Show("Va rog completati numarul de km, acesta trebuie sa fie un numar!");
                                                    }
                                                    else
                                                    {
                                                        if (!VerificaSelectieFransiza())
                                                        {
                                                            MessageBox.Show("Va rog sa selectati un procent pentru fransiza!");
                                                        }
                                                        else
                                                        {
                                                            if (codGrupaVarsta() == 0)
                                                            {
                                                                MessageBox.Show("Pentru varsta clientului nu se mai poate incheia o polita de asigurare Casco!");
                                                                labelPretPolitaCasco.Text = "";
                                                            }
                                                            else
                                                            {
                                                                if (cod_Subcategorie() == 0)
                                                                {
                                                                    MessageBox.Show("Pentru subcategoria selectata nu a fost adaugata o polita Casco.");
                                                                    labelPretPolitaCasco.Text = "";
                                                                }
                                                                else
                                                                {
                                                                    if (cod_cap() == 0)
                                                                    {
                                                                        MessageBox.Show("Pentru capacitatea introdusa nu a fost adaugata o polita Casco.");
                                                                        labelPretPolitaCasco.Text = "";
                                                                    }
                                                                    else
                                                                    {
                                                                        if (Cod_casco() == 0)
                                                                        {
                                                                            MessageBox.Show("Pentru pachetul casco selectat nu a fost adaugata o polita Casco.");
                                                                            labelPretPolitaCasco.Text = "";
                                                                        }
                                                                        else
                                                                        {
                                                                            if (cod_model() == 0)
                                                                            {
                                                                                MessageBox.Show("Pentru modelul selectat nu a fost adaugata o polita Casco.");
                                                                                labelPretPolitaCasco.Text = "";
                                                                            }
                                                                            else
                                                                            {
                                                                                if (ValoarePrimaCasco() == 0)
                                                                                {
                                                                                    MessageBox.Show("Oferta nu mai este disponibila sau nu a fost inca introdusa in sistem!");
                                                                                    labelPretPolitaCasco.Text = "";
                                                                                }
                                                                                else
                                                                                {
                                                                                    valoarePolitaCasco();
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
                    }
                }
            }
        }
        int cod_autovehicul()
        {
            int cod_auto = 0;
            foreach (Autovehicul auto in listaAuto)
            {
                if (auto.Serie_sasiu.ToUpper() == textBoxSerieSasiu.Text.Trim().ToUpper() && auto.Nr_inmatriculare.ToUpper() == textBoxNumarInmatriculare.Text.Trim().ToUpper())
                {
                    cod_auto = auto.Cod_auto;
                    break;
                }
            }
            return cod_auto;
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
                    adr.Strada.ToUpper() == textBoxStrada.Text.Trim().ToUpper() && adr.Nr_strada.ToUpper() == textBoxNumarStrada.Text.Trim().ToUpper() && adr.Bloc.ToUpper() == textBoxBloc.Text.Trim().ToUpper() &&
                    adr.Scara.ToUpper() == textBoxScara.Text.Trim().ToUpper() && adr.Etaj.ToUpper() == textBoxEtaj.Text.Trim().ToUpper() && adr.Apartament.ToUpper() == textBoxApartament.Text.Trim().ToUpper())
                {
                    cod_adresa = adr.Cod_adresa;
                }
            }
            return cod_adresa;
        }

        //Implementare butonul de cauta
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
                        dateTimePickerAnObtinerePermis.Value = Convert.ToDateTime(cl.An_obtinere_permis);
                        textBoxEmail.Text = cl.Email;
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
                        textBoxNumarStrada.Text = adr.Nr_strada;
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
                        textBoxSerieSasiu.Text = auto.Serie_sasiu;
                        textBoxNumarInmatriculare.Text = auto.Nr_inmatriculare;
                        dateTimePickerDataFabrAuto.Value = Convert.ToDateTime(auto.An_fabricatie);
                        comboBoxCombustibil.Text = auto.Combustibil;
                        textBoxCapacitate.Text = Convert.ToString(auto.Capacitate_cilindrica);
                        textBoxPutere.Text = Convert.ToString(auto.Putere);
                        textBoxNumarLocuri.Text = Convert.ToString(auto.Nr_locuri);
                        textBoxMasa.Text = Convert.ToString(auto.Masa_maxima_autorizata);
                    }
                }
            }
        }
        //Implementare butonul de cauta
        bool verificaClientExistent()
        {
            if (listaClienti.Any(d => d.Cnp == textBoxCnp.Text))
            {
                return true;
            }
            else
                return false;
        }
        //Metoda verificare controale completate si verificarea datelor introduse de utilizator Implementare Incheiere Polita
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
                        if (string.IsNullOrEmpty(textBoxEmail.Text) || !Verificari.checkEmail(textBoxEmail.Text))
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
                                    if (!Verificari.checkCnp(textBoxNumarStrada.Text) || string.IsNullOrEmpty(textBoxNumarStrada.Text))
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
            foreach (IncheiereCasco casco in listaPoliteCasco)
            {
                if (casco.Cod_client == cod_client() && casco.Cod_auto == cod_autovehicul())
                {
                    foreach (DurataAsigurare dur in durata)
                    {
                        if (casco.Id_durata == dur.Id_durata)
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
            if (listaPoliteCasco.Count != 0)
            {
                foreach (IncheiereCasco rca in listaPoliteCasco)
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
            if (listaPoliteCasco.Count != 0)
            {
                foreach (IncheiereCasco casco in listaPoliteCasco)
                {
                    if (casco.Cod_client == cod_client() && casco.Cod_auto == cod_autovehicul())
                    {
                        DateTime data_inceput = Convert.ToDateTime(casco.Data_inceput);
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
        //Verificari existente a clientului si a autovehiculului
        bool verificaAsigurariExistente()
        {
            foreach (IncheiereCasco casco in listaPoliteCasco)
            {
                if (casco.Cod_client == cod_client() && casco.Cod_auto == cod_autovehicul())
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
        private void buttonCautaClient_Click(object sender, EventArgs e)
        {
            if (!(Verificari.checkCnp(textBoxCnp.Text)) || textBoxCnp.Text.Length != 13)
            {
                MessageBox.Show("Cnp introdus nu respecta formatul unui Cnp!");
            }
            else
            {
                if (string.IsNullOrEmpty(textBoxSerieSasiu.Text) || string.IsNullOrEmpty(textBoxNumarInmatriculare.Text))
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
                            textBoxSerieSasiu.Clear();
                            textBoxNumarInmatriculare.Clear();
                            textBoxCapacitate.Clear();
                            textBoxPutere.Clear();
                            textBoxNumarKM.Clear();
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
                                    labelPretPolitaCasco.Text = "";
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
                                    textBoxCapacitate.Clear();
                                    textBoxPutere.Clear();
                                    textBoxMasa.Clear();
                                    textBoxNumarKM.Clear();
                                    textBoxNumarLocuri.Clear();
                                    labelPretPolitaCasco.Text = "";
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
        void dezactivareControale()
        {
            dateTimePickerDataInceput.Enabled = true;
            comboBoxDurata.Enabled = true;
            textBoxCnp.Enabled = true;
            comboBoxJudet.Enabled = true;
            textBoxSerieSasiu.Enabled = true;
            textBoxNumarInmatriculare.Enabled = true;
            comboBoxCategorie.Enabled = true;
            comboBoxSubcategorie.Enabled = true;
            textBoxNumarLocuri.Enabled = true;
            textBoxMasa.Enabled = true;
            textBoxCapacitate.Enabled = true;
            textBoxPutere.Enabled = true;
            textBoxNumarKM.Enabled = true;
            dateTimePickerDataFabrAuto.Enabled = true;
            comboBoxMarca.Enabled = true;
            comboBoxModel.Enabled = true;
            comboBoxVarianta.Enabled = true;
            groupBoxProcenteFransiza.Enabled = true;
            comboBoxDomeniuUtilizare.Enabled = true;
            comboBoxCombustibil.Enabled = true;
            buttonInainte.Enabled = true;
            buttonCalculPrimaCasco.Enabled = true;
            labelPretPolitaCasco.Enabled = true;
            buttonCautaClient.Enabled = true;
            textBoxNume.Enabled = false;
            textBoxPrenume.Enabled = false;
            dateTimePickerAnObtinerePermis.Enabled = false;
            textBoxNrTelefon.Enabled = false;
            textBoxEmail.Enabled = false;
            comboBoxTara.Enabled = false;
            textBoxLocalitate.Enabled = false;
            textBoxStrada.Enabled = false;
            textBoxNumarStrada.Enabled = false;
            textBoxBloc.Enabled = false;
            textBoxScara.Enabled = false;
            textBoxEtaj.Enabled = false;
            textBoxApartament.Enabled = false;
            buttonPrevizualizare.Enabled = false;
            buttonIncheiereCasco.Enabled = false;
            buttonInapoi.Enabled = false;
        }
        void clearControls()
        {
            foreach (Control ctrl in Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).Clear();
                }
            }
            buttonIncheiereCasco.Enabled = false;
            labelPretPolitaCasco.Text = "";
            buttonPrevizualizare.Enabled = false;
            dateTimePickerDataFabrAuto.Value = DateTime.Now;
            dateTimePickerDataInceput.Value = DateTime.Now;
            dateTimePickerAnObtinerePermis.Value = DateTime.Now;
            AddJudetToCombo();
            AddCombToCombo();
            AddDomeniuToCombo();
            AddTipCascoToListbox();
            AddDenFransiza();
            dezactivareControale();
        }

        private void buttonInapoi_Click(object sender, EventArgs e)
        {
            dezactivareControale();
        }

        private void buttonInainte_Click(object sender, EventArgs e)
        {
            dateTimePickerDataInceput.Enabled = false;
            comboBoxDurata.Enabled = false;
            textBoxCnp.Enabled = false;
            comboBoxJudet.Enabled = false;
            textBoxSerieSasiu.Enabled = false;
            textBoxNumarInmatriculare.Enabled = false;
            comboBoxCategorie.Enabled = false;
            comboBoxSubcategorie.Enabled = false;
            textBoxNumarLocuri.Enabled = false;
            textBoxMasa.Enabled = false;
            textBoxCapacitate.Enabled = false;
            textBoxPutere.Enabled = false;
            textBoxNumarKM.Enabled = false;
            dateTimePickerDataFabrAuto.Enabled = false;
            comboBoxMarca.Enabled = false;
            comboBoxModel.Enabled = false;
            groupBoxProcenteFransiza.Enabled = false;
            comboBoxVarianta.Enabled = false;
            comboBoxDomeniuUtilizare.Enabled = false;
            comboBoxCombustibil.Enabled = false;
            buttonCalculPrimaCasco.Enabled = false;
            labelPretPolitaCasco.Enabled = false;
            buttonCautaClient.Enabled = false;
            buttonInainte.Enabled = false;
            textBoxNume.Enabled = true;
            textBoxPrenume.Enabled = true;
            dateTimePickerAnObtinerePermis.Enabled = true;
            textBoxNrTelefon.Enabled = true;
            textBoxEmail.Enabled = true;
            comboBoxTara.Enabled = true;
            textBoxLocalitate.Enabled = true;
            textBoxStrada.Enabled = true;
            textBoxNumarStrada.Enabled = true;
            textBoxBloc.Enabled = true;
            textBoxScara.Enabled = true;
            textBoxEtaj.Enabled = true;
            textBoxApartament.Enabled = true;
            buttonInapoi.Enabled = true;
            buttonPrevizualizare.Enabled = true;
        }
        int cod_polita()
        {
            int id_casco = 1;
            List<IncheiereCasco> listaCasco = DatabaseAcces.ExtragePoliteCasco();
            if (listaCasco.Count > 0)
            {
                id_casco = listaCasco.Max(d => d.Cod_casco_primary_key) + 1;
            }
            return id_casco;
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
        #region CreareObiecte
        IncheiereCasco creazaPolitaCasco(int id_casco, int cod_client, int cod_auto)
        {
            IncheiereCasco casco = new IncheiereCasco()
            {
                Cod_casco_primary_key = id_casco,
                Id_subcategorie = cod_Subcategorie(),
                Cod_client = cod_client,
                Cod_angajat = DateAngajat.IdAngajat,
                Cod_auto = cod_auto,
                Id_durata = ((DurataAsigurare)comboBoxDurata.SelectedItem).Id_durata,
                Id_casco = Cod_casco(),
                Id_fransiza = Cod_fransiza(),
                Id_utilizare = ((DomeniiUtilizare)comboBoxDomeniuUtilizare.SelectedItem).Id_utilizare,
                Data_inceput = Convert.ToString(dateTimePickerDataInceput.Value),
                Data_emiterii = Convert.ToString((DateTime.Now)),
                Nr_kilometri = Convert.ToInt32(textBoxNumarKM.Text),
                Valoare_prima_casco = ValoarePrimaCasco(),
                Valoare_Casco = Convert.ToSingle(labelPretPolitaCasco.Text),
                status_Casco = true
            };
            return casco;
        }

        Autovehicul creeazaAuto(int cod_auto, int cod_client)
        {
            Autovehicul auto = new Autovehicul()
            {
                Cod_auto = cod_auto,
                Serie_sasiu = textBoxSerieSasiu.Text.Trim().ToUpper(),
                Nr_inmatriculare = textBoxNumarInmatriculare.Text.Trim().ToUpper(),
                An_fabricatie = Convert.ToString(dateTimePickerDataFabrAuto.Value),
                Combustibil = comboBoxCombustibil.Text,
                Capacitate_cilindrica = Convert.ToInt32(textBoxCapacitate.Text.Trim()),
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
                Nr_strada = textBoxNumarStrada.Text.Trim(),
                Bloc = textBoxBloc.Text.Trim(),
                Scara = textBoxScara.Text.Trim(),
                Etaj = textBoxEtaj.Text.Trim(),
                Apartament = textBoxApartament.Text.Trim(),
                status_adresa = true
            };
            return adr;
        }
        #endregion

        private void buttonIncheiereCasco_Click(object sender, EventArgs e)
        {
            if (checkCompletedControls())
            {
                if (!verificaAsigurariExistente())
                {
                    MessageBox.Show($"Pentru datele completate deja exista o polita casco valabila, aceasta mai are {zileValabilitate()} zile valabilitate!");
                }
                else
                {
                    if (listaClienti.Any(d => d.Cnp == textBoxCnp.Text))
                    {
                        //Scenariul cand adresa si autovehiculul sunt aceleasi
                        if (cod_adresa() == cod_adresa_controale() && cod_autovehicul() == cod_auto_client())
                        {

                            DialogResult dialog = MessageBox.Show("Sigur doriti sa incheiati polita Casco", "Confirmare", MessageBoxButtons.YesNo);
                            if (dialog == DialogResult.Yes)
                            {
                                //Creare polita casco pentru un client cu acelasi autovehicul si cu acceasi adresa (reinoirea politei)
                                int cheie_polita = cod_polita();
                                IncheiereCasco casco = creazaPolitaCasco(cheie_polita, cod_client(), cod_autovehicul());
                                listaPoliteCasco.Add(casco);
                                DatabaseAcces.AdaugaPolitaCasco(casco);
                                DatabaseAcces.UpdateClient(cod_client(), textBoxEmail.Text.Trim(), textBoxNrTelefon.Text.Trim());
                                SalvarePdf(PrevPdf(cheie_polita), cheie_polita);
                                SendEmail(textBoxEmail.Text, SalvarePdf(PrevPdf(cheie_polita), cheie_polita));
                                MessageBox.Show("Polita Casco incheiata cu succes!");
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
                                    //Incheiere casco pentru acelasi client care are aceeasi adresa dar alt autovehicul
                                    int cheie_polita = cod_polita();
                                    IncheiereCasco casco = creazaPolitaCasco(cheie_polita, cod_client(), cod_autovehicul());
                                    listaPoliteCasco.Add(casco);
                                    DatabaseAcces.AdaugaPolitaCasco(casco);
                                    DatabaseAcces.UpdateClient(cod_client(), textBoxEmail.Text.Trim(), textBoxNrTelefon.Text.Trim());
                                    SalvarePdf(PrevPdf(cheie_polita), cheie_polita);
                                    SendEmail(textBoxEmail.Text, SalvarePdf(PrevPdf(cheie_polita), cheie_polita));
                                    MessageBox.Show("Polita Casco incheiata cu succes!");
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
                                    DialogResult dialog = MessageBox.Show("Sigur doriti sa incheiati polita Casco", "Confirmare", MessageBoxButtons.YesNo);
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
                                        //Incheiere polita casco pentru acelasi client si acelasi autovehicul dar cu alta adresa a clientului
                                        int cheie_polita = cod_polita();
                                        IncheiereCasco casco = creazaPolitaCasco(cheie_polita, cod_client(), cod_autovehicul());
                                        listaPoliteCasco.Add(casco);
                                        DatabaseAcces.AdaugaPolitaCasco(casco);
                                        DatabaseAcces.UpdateClient(cod_client(), textBoxEmail.Text.Trim(), textBoxNrTelefon.Text.Trim());
                                        SalvarePdf(PrevPdf(cheie_polita), cheie_polita);
                                        SendEmail(textBoxEmail.Text, SalvarePdf(PrevPdf(cheie_polita), cheie_polita));
                                        MessageBox.Show("Polita Casco incheiata cu succes!");
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
                                        DialogResult dialog = MessageBox.Show("Sigur doriti sa incheiati polita Casco", "Confirmare", MessageBoxButtons.YesNo);
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
                                            //Incheierea politei casco
                                            int cheie_polita = cod_polita();
                                            IncheiereCasco casco = creazaPolitaCasco(cheie_polita, cod_client(), cod_autovehicul());
                                            listaPoliteCasco.Add(casco);
                                            DatabaseAcces.AdaugaPolitaCasco(casco);
                                            DatabaseAcces.UpdateClient(cod_client(), textBoxEmail.Text.Trim(), textBoxNrTelefon.Text.Trim());
                                            SalvarePdf(PrevPdf(cheie_polita), cheie_polita);
                                            SendEmail(textBoxEmail.Text, SalvarePdf(PrevPdf(cheie_polita), cheie_polita));
                                            MessageBox.Show("Polita Casco incheiata cu succes!");
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
                        DialogResult dialog = MessageBox.Show("Sigur doriti sa incheiati polita Casco", "Confirmare", MessageBoxButtons.YesNo);
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
                                An_obtinere_permis = Convert.ToString(dateTimePickerAnObtinerePermis.Value),
                                Cnp = textBoxCnp.Text.Trim(),
                                Email = textBoxEmail.Text.Trim(),
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
                            IncheiereCasco casco = creazaPolitaCasco(cheie_polita, id_client, cod_autovehicul());
                            listaPoliteCasco.Add(casco);
                            DatabaseAcces.AdaugaPolitaCasco(casco);
                            SalvarePdf(PrevPdf(cheie_polita), cheie_polita);
                            SendEmail(textBoxEmail.Text, SalvarePdf(PrevPdf(cheie_polita), cheie_polita));
                            MessageBox.Show("Polita Casco incheiata cu succes!");
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

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Convert.ToString(cod_model()));
        }

        private void comboBoxJudet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (labelPretPolitaCasco.Text != "")
            {
                valoarePolitaCasco();
            }
        }

        private void comboBoxDomeniuUtilizare_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (labelPretPolitaCasco.Text != "")
            {
                valoarePolitaCasco();
            }
        }

        private void buttonPrevizualizare_Click(object sender, EventArgs e)
        {
            if (checkCompletedControls())
            {
                int cheie_polita = cod_polita();
                FormPrevizualizareCasco form = new FormPrevizualizareCasco(PrevPdf(cheie_polita), buttonIncheiereCasco, buttonPrevizualizare);
                form.ShowDialog();
            }
        }
        string riscuriAsigurare()
        {
            string riscuri = string.Empty;
            int contor = 0;
            foreach (AsociereRiscCasco asoc in listaAsociereCascoRisc)
            {
                if (Cod_casco() == asoc.Id_casco)
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
            return riscuri;
        }

        string clauzeSuplimentare()
        {
            string clauze = string.Empty;
            foreach (AsociereCascoClauza clauza in listaAsociereCascoClauze)
            {
                if (clauza.Id_casco == Cod_casco())
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
            return clauze;
        }
        #region CrearePdf
        Image PrevPdf(int cod_polita)
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
            grafic.DrawString(dateTimePickerDataInceput.Value.ToShortDateString(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(135, 648));
            grafic.DrawString(Convert.ToDateTime(labelDataSfarsit.Text).ToShortDateString(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(390, 648));
            grafic.DrawString(DateTime.Now.ToShortDateString(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(713, 647));
            //Adauga date Client
            grafic.DrawString(textBoxNume.Text.ToUpper() + " " + textBoxPrenume.Text.ToUpper(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(290, 562));
            grafic.DrawString(textBoxCnp.Text, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(93, 586));
            grafic.DrawString(textBoxStrada.Text.ToUpper(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(442, 586));
            grafic.DrawString(textBoxNumarStrada.Text, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(787, 586));
            grafic.DrawString(textBoxBloc.Text, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(48, 608));
            grafic.DrawString(textBoxScara.Text, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(125, 608));
            grafic.DrawString(textBoxApartament.Text, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(204, 608));
            grafic.DrawString(textBoxLocalitate.Text.ToUpper(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(315, 608));
            grafic.DrawString(comboBoxJudet.Text.ToUpper(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(630, 609));
            //Adauga date autovehicul
            grafic.DrawString(comboBoxMarca.Text.ToUpper(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(70, 687));
            grafic.DrawString(comboBoxModel.Text.ToUpper() + " " + comboBoxVarianta.Text.ToUpper(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(339, 687));
            grafic.DrawString(dateTimePickerDataFabrAuto.Value.ToShortDateString(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(110, 712));
            grafic.DrawString(textBoxCapacitate.Text + "/" + textBoxPutere.Text, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(470, 712));
            grafic.DrawString(textBoxSerieSasiu.Text, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(148, 734));
            grafic.DrawString(textBoxNumarInmatriculare.Text, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(492, 735));
            grafic.DrawString(comboBoxSubcategorie.Text.ToUpper(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(652, 735));
            grafic.DrawString("Proprietar", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(154, 758));
            grafic.DrawString(comboBoxDomeniuUtilizare.Text, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
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
            grafic.DrawString(labelPretPolitaCasco.Text, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(725, 804));
            grafic.DrawString("Numerar", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(168, 824));
            grafic.DrawString("--------", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(122, 862));
            grafic.DrawString("--------", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(600, 862));
            grafic.DrawString(labelPretPolitaCasco.Text, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(128, 883));
            grafic.DrawString(DateTime.Now.ToShortDateString(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(154, 906));
            grafic.DrawString("Persoana fizica", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(140, 960));
            grafic.DrawString(comboBoxJudet.Text.ToUpper() + " - " + textBoxLocalitate.Text.ToUpper(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(463, 960));
            grafic.DrawString(textBoxNumarKM.Text, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(445, 983));
            grafic.DrawString("Fransiza: ", new System.Drawing.Font("Times New Roman", 18, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(19, 1005));
            grafic.DrawString(comboBoxFransiza.Text, new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(90, 1005));
            grafic.DrawString("Procent Fransiza: ", new System.Drawing.Font("Times New Roman", 18, FontStyle.Regular),
                new SolidBrush(Color.Black), new Point(280, 1005));
            grafic.DrawString(Convert.ToString(Procent_groupBox()), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(413, 1005));
            grafic.DrawString(Convert.ToString(varstaClient() + " ani"), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(71, 1047));
            grafic.DrawString(Convert.ToString(vechimePermis()) + " ani", new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(466, 1047));
            grafic.DrawString("Casco " + comboBoxTipCasco.Text + " " + riscuriAsigurare(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(19, 1082));
            grafic.DrawString(clauzeSuplimentare(), new System.Drawing.Font("Times New Roman", 18, FontStyle.Bold),
                new SolidBrush(Color.Black), new Point(19, 1137));
            return bitmap;
        }
        string SalvarePdf(Image bitmap, int cod_polita)
        {
            Document doc = new Document(iTextSharp.text.PageSize.A4);
            string path = $@"D:\Marian facultate\Licenta\PolitaCasco{textBoxNume.Text}{textBoxPrenume.Text}{cod_polita}.pdf";
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
        #endregion
    }
}
