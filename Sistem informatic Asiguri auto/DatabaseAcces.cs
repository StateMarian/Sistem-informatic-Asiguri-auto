using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Sistem_informatic_Asiguri_auto
{
    public class DatabaseAcces
    {
        const string connString = @"Server=DESKTOP-JUNT3BF;Database=Asigurari_auto;Trusted_Connection=True;";

        public static List<Angajat> ExtrageAngajati()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaAngajat = con.Query<Angajat>("Select * from Angajat").ToList();
                return listaAngajat;
            }
        }

        public static void AddAngajat(Angajat ang)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("INSERT INTO ANGAJAT VALUES('" + ang.Cod_angajat + "','" + ang.Nume + "','" + ang.Prenume + "'," +
                    "'" + ang.Cnp + "','" + ang.Email + "','" + ang.Nr_telefon + "','" + ang.Data_angajare + "','" + ang.Tip_angajat + "','" + ang.Parola + "',NULL,'" + ang.status + "')");
            }
        }


        public static void UpdateAngajat(List<Angajat> listaAng)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                foreach (Angajat ang in listaAng)
                {
                    con.Execute("UPDATE ANGAJAT SET Nume='" + ang.Nume + "',Prenume='" + ang.Prenume + "',Cnp='" + ang.Cnp + "',Email='" + ang.Email + "'," +
                        "Nr_telefon='" + ang.Nr_telefon + "',Tip_angajat='" + ang.Tip_angajat + "',Parola='" + ang.Parola + "' where Cod_angajat='" + ang.Cod_angajat + "'");
                }
            }
        }

        public static void ConcediereAngajat(List<Angajat> listaAng, string cod, string data, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                foreach (Angajat ang in listaAng)
                {
                    con.Execute("UPDATE ANGAJAT SET status='" + status + "',data_concediere='" + data + "' where Cod_angajat='" + cod + "'");
                }
            }
        }

        public static void StatusAngajat(string cod, string data_angajare, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("UPDATE ANGAJAT SET status='" + status + "',Data_angajare='" + data_angajare + "',data_concediere=NULL where Cod_angajat='" + cod + "'");
            }
        }

        public static List<Categorii> ExtrageCategorii()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaCategorii = con.Query<Categorii>("Select * from Categorii").ToList();
                return listaCategorii;
            }
        }

        public static List<Categorii> ExtrageCategoriisiSubcategorii()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaCategorii = con.Query<Categorii>("Select * from Categorii").ToList();
                foreach (Categorii cat in listaCategorii)
                {
                    cat.listaSubcat = con.Query<Subcategorii>("Select * from Subcategorie_auto where id_categorie='" + cat.Id_categorie + "' and status_subcategorie=1").ToList();
                }
                return listaCategorii;
            }
        }

        public static void AddCategorie(Categorii cat)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Execute("INSERT INTO Categorii VALUES ('" + cat.Id_categorie + "','" + cat.Denumire_categorie + "','" + cat.Cod_categorie + "' , '" + cat.status_categorie + "')");
            }
        }

        public static void ModificaStatusCategorie(int index, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("update categorii set status_categorie='" + status + "' where id_categorie='" + index + "'");
            }
        }

        public static void ModificaCategorie(int cod, string denumire, string cod_categorie)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("UPDATE Categorii set Denumire_categorie='" + denumire + "', Cod_categorie='" + cod_categorie + "' where Id_categorie='" + cod + "'");
            }
        }

        public static List<Subcategorii> ExtrageSubcategorii()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaSubcategorii = con.Query<Subcategorii>("Select * from Subcategorie_auto ").ToList();
                return listaSubcategorii;
            }
        }


        public static void AddSubCategorii(Subcategorii sub)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("INSERT INTO Subcategorie_auto Values('" + sub.Id_subcategorie + "','" + sub.Denumire_Subcategorie + "','" + sub.Numar_locuri + "'," +
                    "'" + sub.Masa_min + "','" + sub.Masa_max + "','" + sub.Id_categorie + "', '" + sub.status_subcategorie + "')");
            }
        }

        public static void ModificaStatusSubcategorie(int cod, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("UPDATE Subcategorie_auto SET status_subcategorie='" + status + "' where id_subcategorie='" + cod + "'");
            }
        }

        public static List<CapacitateCilindrica> ExtrageCapacitate()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaCapacitati = con.Query<CapacitateCilindrica>("Select * from Capacitate_cilindrica").ToList();
                return listaCapacitati;
            }
        }

        public static void AdaugaCapacitate(CapacitateCilindrica cap)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("Insert into Capacitate_cilindrica values('" + cap.Id_capacitate + "','" + cap.Min_capacitate + "','" + cap.Max_capacitate + "','" + cap.Putere + "', '" + cap.status_capacitate + "')");
            }
        }

        public static void ModificaCapacitate(List<CapacitateCilindrica> listacap)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                foreach (CapacitateCilindrica cap in listacap)
                {
                    con.Execute("update Capacitate_cilindrica set Min_capacitate='" + cap.Min_capacitate + "', Max_capacitate='" + cap.Max_capacitate + "',Putere='" + cap.Putere + "' where Id_capacitate='" + cap.Id_capacitate + "'");
                }
            }
        }

        public static void ModificaStatusCapacitate(int index, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("update Capacitate_cilindrica set status_capacitate='" + status + "' where Id_capacitate = '" + index + "'");
            }
        }

        public static List<GrupeVarsta> ExtrageGrupe()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaGrupe = con.Query<GrupeVarsta>("Select * from Grupe_varsta").ToList();
                return listaGrupe;
            }
        }

        public static void AdaugaGrupa(GrupeVarsta grup)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into Grupe_varsta values ('" + grup.Id_grupa + "','" + grup.Min_varsta + "','" + grup.Max_varsta + "', '" + grup.status_grupa + "')");
            }
        }

        public static void ModificaGrupa(int cod, int min, int max)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("Update Grupe_varsta set Min_varsta='" + min + "',Max_varsta='" + max + "' where Id_grupa='" + cod + "'");
            }
        }

        public static void ModificaStatusGrupa(int index, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("update grupe_varsta set status_grupa = '" + status + "' where id_grupa='" + index + "'");
            }
        }

        public static List<Bonus_Malus_Class> ExtrageClaseBonusMalus()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaClase = con.Query<Bonus_Malus_Class>("select * from Sistem_Bonus_Malus").ToList();
                return listaClase;
            }
        }

        public static void AdaugaClasa(Bonus_Malus_Class bonMal)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into Sistem_Bonus_Malus values('" + bonMal.Id_bonus_malus + "','" + bonMal.Bonus_Malus + "','" + bonMal.Procent + "', '" + bonMal.status_bonus + "')");
            }
        }

        public static void ModificaClasa(int index, string denumire, int procent)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("update Sistem_Bonus_Malus set Bonus_Malus='" + denumire + "',Procent='" + procent + "' where Id_bonus_malus='" + index + "'");
            }
        }

        public static void ModificaStatusBonusMalus(int index, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("UPDATE sistem_bonus_malus set status_bonus='" + status + "' where id_bonus_malus = '" + index + "' ");
            }
        }

        public static List<ZonaGeografica> ExtrageZoneGeografice()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaZone = con.Query<ZonaGeografica>("select * from Zona_geografica").ToList();
                return listaZone;
            }
        }

        public static void AdaugaZona(ZonaGeografica zona)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into zona_geografica values ('" + zona.Id_zona + "','" + zona.Judet + "','" + zona.Procent + "','" + zona.Tip_Asigurare + "','" + zona.status_zona + "')");
            }
        }

        public static void ModificaStatusZonaGeografica(int index, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("update zona_geografica set status_zona='" + status + "' where id_zona='" + index + "'");
            }
        }
        public static void AdaugaDurata(DurataAsigurare dur)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into DurataAsigurari values('" + dur.Id_durata + "','" + dur.Durata + "','" + dur.Procent_durata + "','" + dur.Tip_asigurare + "','" + dur.status_durata + "')");
            }
        }

        public static List<DurataAsigurare> ExtrageDurataAsigurare()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaDurata = con.Query<DurataAsigurare>("select * from DurataAsigurari").ToList();
                return listaDurata;
            }
        }
        public static void ModificaStatusDurata(int index, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("update DurataAsigurari set status_durata='" + status + "' where Id_durata='" + index + "'");
            }
        }

        public static List<BeneficiiSuplimentare> ExtrageBeneficii()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaBeneficii = con.Query<BeneficiiSuplimentare>("select * from Pachet_Beneficii_Suplimentare").ToList();
                return listaBeneficii;
            }
        }

        public static void AdaugaBeneficiu(BeneficiiSuplimentare ben)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into Pachet_Beneficii_suplimentare values ('" + ben.Id_beneficiu + "','" + ben.Denumire_pachet + "','" + ben.continut_pachet + "','" + ben.Procent_pachet + "','" + ben.status_beneficiu + "')");
            }
        }

        public static void ModificaStatusBeneficiu(int index, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("update Pachet_beneficii_suplimentare set status_beneficiu='" + status + "' where id_beneficiu='" + index + "'");
            }
        }

        public static List<Discount_RCA> ExtrageDiscount()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaDiscount = con.Query<Discount_RCA>("select * from Discount").ToList();
                return listaDiscount;
            }
        }

        public static void AdaugaDiscount(Discount_RCA disc)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into Discount values ('" + disc.Id_discount + "','" + disc.Denumire_discount + "','" + disc.Procent_discount + "','" + disc.status_discount + "')");
            }
        }

        public static void ModificaStatusDiscount(int index, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("update discount set status_discount='" + status + "' where id_discount='" + index + "'");
            }
        }

        public static List<DomeniiUtilizare> ExtrageDomeniiUtilizare()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaDomenii = con.Query<DomeniiUtilizare>("select * from Domeniu_de_utilizare").ToList();
                return listaDomenii;
            }
        }
        public static void AdaugaDomeniu(DomeniiUtilizare dom)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into Domeniu_de_utilizare values ('" + dom.Id_utilizare + "','" + dom.Denumire_utilizare + "','" + dom.Procent_utilizare + "','" + dom.status_domeniu + "')");
            }
        }
        public static void ModificaStautsDomeniuUtilizare(int index, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("update Domeniu_de_utilizare set status_domeniu='" + status + "' where id_utilizare='" + index + "'");
            }
        }
        public static List<Fransiza> ExtrageFransiza()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaFransiza = con.Query<Fransiza>("select * from Fransiza").ToList();
                return listaFransiza;
            }
        }

        public static void AdaugaFransiza(Fransiza fran)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into Fransiza values ('" + fran.Id_fransiza + "','" + fran.Tip_fransiza + "','" + fran.Procent + "','" + fran.Procent_reducere + "','" + fran.status_fransiza + "')");
            }
        }

        public static void ModificaStatusFransiza(int index, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("update Fransiza set status_fransiza ='" + status + "' where id_fransiza='" + index + "'");
            }
        }

        public static List<Tip_casco> ExtrageTipCasco()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaTipcasco = con.Query<Tip_casco>("select * from Tip_casco").ToList();
                return listaTipcasco;
            }
        }

        public static void AdaugaTipCasco(Tip_casco casc)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into Tip_casco values ('" + casc.Id_casco + "','" + casc.Denumire_casco + "','" + casc.Id_fransiza + "','" + casc.status_tipCasco + "')");
            }
        }

        public static void ModificastatusTipCasco(int index, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("update tip_casco set status_tipCasco ='" + status + "' where id_casco='" + index + "'");
            }
        }

        public static List<IndicatoriSuplimentari> ExtrageIndicatoriSuplimentari()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaIndicatori = con.Query<IndicatoriSuplimentari>("select * from Indicatori").ToList();
                return listaIndicatori;
            }
        }

        public static void AdaugaIndicatori(IndicatoriSuplimentari ind)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into Indicatori values ('" + ind.Id_indicatori + "','" + ind.Denumire_indicator + "','" + ind.status_indicator + "')");
            }
        }

        public static void ModificastatusIndicatori(int index, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("update indicatori set status_indicator ='" + status + "' where id_indicatori='" + index + "'");
            }
        }

        public static List<IndicatoriGrupaCapacitate> ExtrageIndicatoriDupaGrupaCapacitate()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaIndicatorigrupa = con.Query<IndicatoriGrupaCapacitate>("select * from Indicatori_capacitate_Grupa").ToList();
                return listaIndicatorigrupa;
            }
        }

        public static void AdaugaIndicatoriCapacitate(IndicatoriGrupaCapacitate ind)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into Indicatori_capacitate_grupa values ('" + ind.Cod_Indicatori_PrimaryKey + "','" + ind.Id_subcategorie + "','" + ind.Id_grupa + "'" +
                    ", '" + ind.Id_capacitate + "','" + ind.Id_indicatori + "','" + ind.Procent_indicator + "','" + ind.status + "')");
            }
        }

        public static void ModificaStatusValoareIndicatori(int cod, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("UPDATE indicatori_capacitate_grupa SET status='" + status + "' where Cod_Indicatori_PrimaryKey='" + cod + "'");
            }
        }

        public static List<RiscCasco> ExtrageRiscuriCasco()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaRiscuri = con.Query<RiscCasco>("select * from Riscuri_asigurate").ToList();
                return listaRiscuri;
            }
        }
        public static void AdaugaRisc(RiscCasco risc)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into Riscuri_asigurate values ('" + risc.id_risc + "','" + risc.Denumire_risc + "','" + risc.status_risc + "')");
            }
        }

        public static void ModificastatusRisc(int index, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("update riscuri_asigurate set status_risc ='" + status + "' where id_risc='" + index + "'");
            }
        }

        public static List<Clauze_suplimentare> ExtrageClauzeSuplimentare()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaClauze = con.Query<Clauze_suplimentare>("select * from clauze_suplimentare").ToList();
                return listaClauze;
            }
        }
        public static void AdaugaClauza(Clauze_suplimentare cl)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into clauze_suplimentare values ('" + cl.Id_clauza + "','" + cl.Denumire_clauza + "','" + cl.status_clauza + "')");
            }
        }
        public static void ModificastatusClauza(int index, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("update clauze_suplimentare set status_clauza ='" + status + "' where id_clauza='" + index + "'");
            }
        }

        public static void AdaugaRiscCascoAsociate(AsociereRiscCasco asoc)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into riscuri_tipcasco_asociere values ('" + asoc.Id_casco + "','" + asoc.Id_risc + "','" + asoc.status_asociere + "')");
            }
        }
        public static List<AsociereRiscCasco> ExtrageRiscCasco()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaAsoc = con.Query<AsociereRiscCasco>("select * from riscuri_tipcasco_asociere").ToList();
                return listaAsoc;
            }
        }
        public static List<AsociereCascoClauza> ExtrageClauzeCasco()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaAsoc = con.Query<AsociereCascoClauza>("select * from Clauze_casco_tip").ToList();
                return listaAsoc;
            }
        }
        public static void AdaugaClauzaCascoAsociate(AsociereCascoClauza asoc)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into clauze_casco_tip values ('" + asoc.Id_casco + "','" + asoc.Id_clauza + "','" + asoc.Valoare_clauza + "')");
            }
        }

        public static List<Marca> ExtrageMarca()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaMarci = con.Query<Marca>("select * from marca").ToList();
                return listaMarci;
            }
        }
        public static void AdaugaMarca(Marca marc)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into marca values ('" + marc.Id_marca + "','" + marc.Denumire_marca + "','" + marc.status_marca + "')");
            }
        }
        public static void ModificastatusMarca(int index, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("update marca set status_marca ='" + status + "' where id_marca='" + index + "'");
            }
        }
        public static List<Model> ExtrageModel()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaModele = con.Query<Model>("select * from model").ToList();
                return listaModele;
            }
        }
        public static List<Marca> ExtrageMarcasiModele()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaMarca = con.Query<Marca>("Select * from marca").ToList();
                foreach (Marca mar in listaMarca)
                {
                    mar.listaModele = con.Query<Model>("Select * from model where id_marca='" + mar.Id_marca + "' and status_model=1").ToList();
                }
                return listaMarca;
            }
        }

        public static void AdaugaModel(Model mod)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into model values ('" + mod.Id_model + "','" + mod.Denumire_model + "','" + mod.Varianta + "', '" + mod.Tip_auto + "','" + mod.Id_marca + "','" + mod.status_model + "')");
            }
        }

        public static void ModificastatusModel(int index, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("update model set status_model ='" + status + "' where id_model='" + index + "'");
            }
        }

        public static List<Calcul_prima_RCA> ExtrageAsigurareRCA()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaRCA = con.Query<Calcul_prima_RCA>("select * from Calcul_prima_RCA").ToList();
                return listaRCA;
            }
        }
        public static void AdaugaRCA(Calcul_prima_RCA asig)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into Calcul_prima_RCA values ('" + asig.Calcul_RCA_PrimaryKey + "','" + asig.Id_subcategorie + "','" + asig.Id_grupa + "'," +
                    "'" + asig.Id_capacitate + "','" + asig.Id_durata + "','" + asig.Id_zona + "','" + asig.Id_beneficiu + "','" + asig.Id_discount + "','" + asig.Id_utilizare + "'," +
                    "'" + asig.Id_bonus_malus + "','" + asig.Prima_de_risc + "','" + asig.status_asigurare + "','" + asig.Data_adaugare + "')");
            }
        }
        public static void ModificaStatusAsigurareRCA(int cod, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("UPDATE Calcul_prima_RCA SET status_asigurare='" + status + "' where Calcul_RCA_PrimaryKey='" + cod + "'");
            }
        }
        public static void ModificaStatusAsigurareRCAExpirare(int cod, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("UPDATE Calcul_prima_RCA SET status_asigurare='" + status + "' where Calcul_RCA_PrimaryKey='" + cod + "'");
            }
        }
        public static List<Calcul_prima_Casco> ExtrageAsigurareCasco()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaCasco = con.Query<Calcul_prima_Casco>("select * from Calcul_prima_CASCO").ToList();
                return listaCasco;
            }
        }
        public static void AdaugaCasco(Calcul_prima_Casco casc)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into Calcul_prima_CASCO values ('" + casc.Calcul_Casco_PrimaryKey + "','" + casc.Id_subcategorie + "','" + casc.Id_grupa + "'," +
                    "'" + casc.Id_capacitate + "','" + casc.Id_durata + "','" + casc.Id_zona + "','" + casc.Id_utilizare + "','" + casc.Id_model + "'," +
                    "'" + casc.Id_casco + "','" + casc.Valoare_prima_casco + "','" + casc.status_casco + "','" + casc.Data_adaugare + "')");
            }
        }

        public static void ModificaStatusAsigurareCasco(int cod, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("UPDATE Calcul_prima_Casco SET status_casco='" + status + "' where Calcul_Casco_PrimaryKey='" + cod + "'");
            }
        }

        public static List<Fransiza> ExtrageFransizaDupaCasco(string casco)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaFrans = con.Query<Fransiza>("select * from fransiza fran inner join tip_casco tip on fran.id_fransiza=tip.id_fransiza where denumire_casco='" + casco + "'").ToList();
                return listaFrans;
            }
        }

        public static List<IndicatoriGrupaCapacitate> ExtrageIndicatoriCalcul(int sub, int cod_cap, int grupa)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaIndicatori = con.Query<IndicatoriGrupaCapacitate>("select * from Indicatori_capacitate_Grupa ind inner join Indicatori indic on " +
                    "ind.Id_indicatori=indic.Id_indicatori where Id_subcategorie='" + sub + "' and Id_grupa='" + grupa + "' and Id_capacitate='" + cod_cap + "'" +
                    "order by indic.Id_Indicatori");
                return listaIndicatori.ToList();
            }
        }

        public static List<Client> ExtrageClient()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaClienti = con.Query<Client>("select * from Client").ToList();
                return listaClienti;
            }
        }
        public static List<Adresa> ExtrageAdresa()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaAdrese = con.Query<Adresa>("select * from Adresa_client").ToList();
                return listaAdrese;
            }
        }
        public static List<Autovehicul> ExtrageAutovehicul()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaAuto = con.Query<Autovehicul>("select * from Autovehicul").ToList();
                return listaAuto;
            }
        }
        public static List<IncheiereRCA> ExtrageRCA()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaRCA = con.Query<IncheiereRCA>("select * from Incheiere_Polita_RCA").ToList();
                return listaRCA;
            }
        }

        public static void AdaugaPolitaRca(IncheiereRCA rca)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into Incheiere_Polita_RCA values ('" + rca.Cod_polita_primary_key + "','" + rca.Id_subcategorie + "','" + rca.Id_bonus_malus + "','" + rca.Cod_client + "','" + rca.Id_discount + "','" + rca.Id_beneficiu + "'," +
                    "'" + rca.Id_utilizare + "','" + rca.Id_durata + "','" + rca.Cod_auto + "','" + rca.Cod_angajat + "','" + rca.Data_inceput + "','" + rca.Data_emiterii + "','"+rca.Prima_de_risc+"','" + rca.Valoare_politaRCA + "','"+rca.status_rca+"')");
            }
        }
        public static void UpdateClient(int cod,string email,string nr_telefon )
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "UPDATE Client SET Email = @Email, Nr_telefon = @Nr_telefon WHERE Cod_client = @Cod_client";
                con.Execute(query, new { Email = email, Nr_telefon = nr_telefon, Cod_client = cod });
            }
        }

        public static void AdaugaAuto(Autovehicul auto)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into Autovehicul values ('" + auto.Cod_auto + "','" + auto.Serie_sasiu + "','" + auto.Nr_inmatriculare + "','" + auto.An_fabricatie + "','" + auto.Combustibil + "'," +
                    "'" + auto.Capacitate_cilindrica + "','" + auto.Putere + "','" + auto.Nr_locuri + "','" + auto.Masa_maxima_autorizata + "','" + auto.Id_model + "','" + auto.Cod_client + "','" + auto.Id_subcategorie + "')");
            }
        }
        public static void ModificaStatusAdresa(int cod, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "UPDATE Adresa_client SET status_adresa = @status_adresa  WHERE cod_adresa = @cod_adresa";
                con.Execute(query, new { status_adresa = status, cod_adresa = cod });
            }
        }
        public static void UpdateClientAdresa(int cod, int adresa)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "UPDATE Client SET Cod_adresa = @Cod_adresa WHERE Cod_client = @Cod_client";
                con.Execute(query, new { Cod_adresa = adresa, Cod_client = cod });
            }
        }

        public static void AdaugaAdresa(Adresa adr)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into Adresa_client values ('"+adr.Cod_adresa+"','"+adr.Tara+"','"+adr.Judet+"','"+adr.Localitate+"','"+adr.Strada+"','"+adr.Nr_strada+"','"+adr.Bloc+"'," +
                    "'"+adr.Scara+"','"+adr.Etaj+"','"+adr.Apartament+"','"+adr.status_adresa+"')");
            }
        }
        public static void AdaugaClient(Client cl)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into Client values ('"+cl.Cod_client+"','"+cl.Nume+"','"+cl.Prenume+"','"+cl.An_obtinere_permis+"','"+cl.Cnp+"','"+cl.Email+"','"+cl.Nr_telefon+"','"+cl.Cod_adresa+"','"+null+"')");
            }
        }
        public static void ModificaStatusRCA(int cod, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "UPDATE Incheiere_Polita_RCA SET status_rca = @status_rca WHERE Cod_polita_primary_key = @cod_rca";
                con.Execute(query, new { status_rca = status, cod_rca = cod });
            }
        }

        public static List<IncheiereCasco> ExtragePoliteCasco()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaCasco = con.Query<IncheiereCasco>("select * from incheiere_polita_casco").ToList();
                return listaCasco;
            }
        }
        public static void AdaugaPolitaCasco(IncheiereCasco casco)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Execute("insert into incheiere_polita_casco values ('" + casco.Cod_casco_primary_key + "','" + casco.Id_subcategorie + "','" + casco.Cod_client + "','" + casco.Cod_angajat + "','" + casco.Cod_auto + "','" + casco.Id_durata + "'," +
                    "'" + casco.Id_casco + "','" + casco.Id_fransiza + "','"+casco.Id_utilizare+"','"+casco.Data_inceput+"','" + casco.Data_emiterii + "','"+casco.Nr_kilometri+"','"+casco.Valoare_prima_casco+"','" + casco.Valoare_Casco + "','" + casco.status_Casco + "')");
            }
        }
        public static void ModificaStatusCasco(int cod, bool status)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "UPDATE incheiere_polita_casco SET status_casco = @status_casco WHERE Cod_casco_primary_key = @cod_casco";
                con.Execute(query, new { status_casco = status, cod_casco = cod });
            }
        }

        public static List<Client> listaClientiPdf(int cod_client)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaClient = con.Query<Client>($"select * from Client where cod_client={cod_client}").ToList();
                return listaClient;
            }
        }
        public static List<Adresa> listaAdresaPdf(int cod_adresa)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaAdresa = con.Query<Adresa>($"select * from Adresa_client where Cod_adresa={cod_adresa}").ToList();
                return listaAdresa;
            }
        }
        public static List<Autovehicul> listaAutoPdf(int cod_auto)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                var listaAuto = con.Query<Autovehicul>($"select * from Autovehicul where Cod_auto={cod_auto}").ToList();
                return listaAuto;
            }
        }

    }
}

