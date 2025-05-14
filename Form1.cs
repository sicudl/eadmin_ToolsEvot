//#define DEBUG_SERVER_LDAP
//#define DEBUG_10

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Threading;
using System.Drawing;

using System.Windows.Forms;

using ConectorLDAP;
using MultiTools;
using RecursosProgramacio;
/*
 * 
 * 
 * ldapsearch -LLL -H ldap://directori.udl.cat:389 -x -w nZ5mA8hb -D 
 * "uid=ManagerEVOT,ou=People,dc=udl,dc=es" -b "ou=People,ou=alumnes,dc=udl,dc=es" "uid=p4372770" userpassword
 * */
namespace TOOLS_eVOT
    {
    public partial class Form1 : Form
        {
        public void ProcesParalel_CARREGA_csv_PDI()
            {
            string Filtre = "";
            int conter = 0;
            int limitTest = 0;
            //this.bStopImportLoader = false;
#if DEBUG_SERVER_LDAP
            ConectorLDAP.Conector LDAP = new ConectorLDAP.Conector("cialis.udl.net");
#else
            //ConectorLDAP.Conector LDAP = new ConectorLDAP.Conector("directori.udl.cat","ManagerEVOT","nZ5mA8hb");
            ConectorLDAP.Conector LDAP = new ConectorLDAP.Conector();//"directori.udl.cat", "manager", "pallus");
#endif
            System.Threading.Thread.Sleep(1000);

            for (int i = 0; i < dniLIst.Count; i++)

            // DictionaryEntry ItemVoter in voters)
                {
                /*if (bStopImportLoader)
                    {
     //                this.status.BeginInvoke((MethodInvoker) delegate() {this.status.AppendText("*** Interruptus!"); ;});    }
     
                    status.AppendText(Environment.NewLine);
                    break;
                    }*/
                string dni = (string)dniLIst[i];

                string ItemVoter = (string)voters[dni];
                string Valor = ItemVoter;
                //(string)ItemVoter.Value;
                string[] camps = Valor.Split(';');
                string uidBuscar = (string)camps[0];

                Filtre = "(&(employeeNumber =" + dni + "))(objectClass=posixAccount)";
                //Filtre = "(&(uid=k4371605))(objectClass=posixAccount)";
                if (uidBuscar.Length > 0)
                    {
                    System.Threading.Thread.Sleep(1000);
                    try
                        {
                        LDAP.GetDades(Filtre,true);
                        }
                    catch (Exception E)
                        {
                        this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("ERROR connectant i processant LDAP . GetDadesAlumnes()! " + E.Message); ;});
                        this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText(Environment.NewLine); ;});
                        }

                    if (LDAP.Dades.Count > 0)
                        {
                        Hashtable registreTrobat = (Hashtable)LDAP.Dades[0];
                        string usrPass = (string)registreTrobat["userPassword"];
                        string DadesItemVotantCens = "";
                        //hab2,48052783X,HÈCTOR,ABAD,BOLAÑOS,hab2@alumnes.udl.cat
                        //imunoz@udl.cat,Isaac,Muñoz,Bringué,,1.0
                        DadesItemVotantCens = (string)camps[3] + "," + (string)camps[1] + "," + (string)camps[2] + ",,1.0";

                        if (usrPass != null)
                            {
                            DadesItemVotantCens = (string)registreTrobat["uid"] + "@udl.cat" + ","+DadesItemVotantCens;

                            if (usrPass == "***KK***") // COMPTE BLOQUEJAT!<2015 //< 9)// == "") //"{crypt}26hCbjHiN7LZo")
                                {
                                conter++;
                                this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("Compte LDAP Bloquejat = " + (string)camps[0] + "i dades =" + Valor); ;});
                                this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText(Environment.NewLine); ;});

                                CompteLocked.Add(Valor + ";" + (string)registreTrobat["uid"] + ";" + (string)registreTrobat["mail"]);
                                }
                            else if (usrPass.Length < 9) //COMPTE que es podria usar si l'usuari l'activés .... però NO està bloquejat  // == "") //"{crypt}26hCbjHiN7LZo")
                                {
                                conter++;
                                this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("Compte LDAP no activat encara = " + (string)camps[0] + "i dades =" + Valor); ;});
                                this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText(Environment.NewLine); ;});
                                CompteInactiu.Add(Valor + ";" + (string)registreTrobat["uid"] + ";" + (string)registreTrobat["mail"]);
                                }
                            else // abans 2023
                                //ActiveVoters.Add(dni, Valor); //DNI;cognoms;etc...
                                //2025 invote 
                                ActiveVoters.Add(dni, DadesItemVotantCens);
                            }
                        else
                            {
                            DadesItemVotantCens = (string)registreTrobat["uid"] +"@udl.cat"+","+ DadesItemVotantCens;
                            if (registreTrobat.ContainsKey("accountStatus"))
                                {
                                if ((string)(registreTrobat["accountStatus"]) == "ACTIU")
                                    ActiveVoters.Add(dni, DadesItemVotantCens);
                                }
                            }
                        /*
                        if (registreTrobat.ContainsKey("accountStatus"))
                            {
                            if ((string)(registreTrobat["accountStatus"]) == "ACTIU")
                                ActiveVoters.Add(dni, Valor);
                            //(string)ItemVoter.Key, (string)ItemVoter.Value);
                            }
                        else
                            {
                            conter++;
                            this.status.BeginInvoke((MethodInvoker)delegate() {status.AppendText("Compte no actiu a LDAP " + (string)camps[0] + "i dades =" + Valor);;});
                            this.status.BeginInvoke((MethodInvoker)delegate() {status.AppendText(Environment.NewLine);;});
                            CompteLocked.Add(Valor);
                            }
                        */
                        //string []camps = lineOfText.Split(',');
                        // registreTrobat
                        //  System.Data.DataTable LlistaCandidats = LDAP.GetDataTable("CandidatsRegistre");

                        }
                    else
                        {
                        conter++;
                        this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("Compte no existeix a LDAP " + (string)camps[0] + "i dades =" + Valor); ;});
                        this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText(Environment.NewLine); ;});
                        }
                    }
                else
                    {
                    this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("uid buit del CSV = " + (string)camps[1]); ;});
                    this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText(Environment.NewLine); ;});
                    }
#if DEBUG_10
                if (limitTest > 25)
                     {
                     status.AppendText("** Càrrega completa, ja podeu Exportar si voleu");
                     status.AppendText(Environment.NewLine);
                     return;
                     }
#endif
                limitTest++;
                this.lbStatus.BeginInvoke((MethodInvoker)delegate() { loaderBar.PerformStep(); ;});
                this.lbStatus.BeginInvoke((MethodInvoker)delegate() { this.lbStatus.Text = "Carregant " + limitTest + " de " + loaderBar.Maximum; ;});

                }

            this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("**** Validació de comptes actius a LDAP Processada!"); ;});
            this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("***** " + conter + " votants amb problemes al compte LDAP(no-existe,no-activat,etc..."); ;});
            this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("** Càrrega completa, ja podeu Exportar si voleu"); ;});
            this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText(Environment.NewLine); ;});

            }


        public  void ProcesParalel_CARREGA()
            {
            string Filtre = "";
            int conter = 0;
            int limitTest = 0;
            //this.bStopImportLoader = false;
#if DEBUG_SERVER_LDAP
            ConectorLDAP.Conector LDAP = new ConectorLDAP.Conector("cialis.udl.net");
#else
            //ConectorLDAP.Conector LDAP = new ConectorLDAP.Conector("directori.udl.cat","ManagerEVOT","nZ5mA8hb");
            //ConectorLDAP.Conector LDAP = new ConectorLDAP.Conector("directori.udl.cat", "manager", "pallus");
            ConectorLDAP.Conector LDAP = new ConectorLDAP.Conector();
#endif
            System.Threading.Thread.Sleep(1000);

            for (int i = 0; i < dniLIst.Count; i++)

            // DictionaryEntry ItemVoter in voters)
                {
                /*if (bStopImportLoader)
                    {
     //                this.status.BeginInvoke((MethodInvoker) delegate() {this.status.AppendText("*** Interruptus!"); ;});    }
     
                    status.AppendText(Environment.NewLine);
                    break;
                    }*/
                string dni = (string)dniLIst[i];

                string ItemVoter = (string)voters[dni];
                string Valor = ItemVoter;
                //(string)ItemVoter.Value;
                string[] camps = Valor.Split(',');
                string uidBuscar = (string)camps[0];

                Filtre = "(&(uid=" + uidBuscar + "))(objectClass=posixAccount)";
                string DadesItemVotantCens = "";
                //hab2,48052783X,HÈCTOR,ABAD,BOLAÑOS,hab2@alumnes.udl.cat
                //imunoz@udl.cat,Isaac,Muñoz,Bringué,,1.0
                DadesItemVotantCens = (string)camps[3] + "," + (string)camps[1] + "," + (string)camps[2] + ",,1.0";

                //Filtre = "(&(uid=k4371605))(objectClass=posixAccount)";
                if (uidBuscar.Length > 0)
                    {
                    System.Threading.Thread.Sleep(1000);
                    try
                        {
                        LDAP.GetDadesAlumnesAdvanced(Filtre);
                        }
                    catch (Exception E)
                        {
                        this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("ERROR connectant i processant LDAP . GetDadesAlumnes()! " + E.Message); ;});
                        this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText(Environment.NewLine); ;});
                        }

                    if (LDAP.Dades.Count > 0)
                        {
                        Hashtable registreTrobat = (Hashtable)LDAP.Dades[0];
                        string usrPass = (string)registreTrobat["userPassword"];

                      //  string DadesItemVotantCens = "";
                        //hab2,48052783X,HÈCTOR,ABAD,BOLAÑOS,hab2@alumnes.udl.cat
                        //imunoz@udl.cat,Isaac,Muñoz,Bringué,,1.0
//                        DadesItemVotantCens = (string)camps[3] + "," + (string)camps[1] + "," + (string)camps[2] + ",,1.0";
                        
                        //hab2,48052783X,HÈCTOR,ABAD,BOLAÑOS,hab2@alumnes.udl.cat
                        //imunoz@udl.cat,Isaac,Muñoz,Bringué,,1.0
                        DadesItemVotantCens = (string)camps[3] + "," + (string)camps[1] + "," + (string)camps[2] + ",,1.0";
                         if (usrPass != null)
                            {

                        if (usrPass =="***KK***") // COMPTE BLOQUEJAT!<2015 //< 9)// == "") //"{crypt}26hCbjHiN7LZo")
                            {
                            conter++;
                            this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("Compte LDAP Bloquejat = " + (string)camps[0] + "i dades =" + Valor); ;});
                            this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText(Environment.NewLine); ;});

                            CompteLocked.Add(Valor +";"+ (string)registreTrobat["uid"]+ ";" + (string)registreTrobat["mailMessageStore"]);
                            }
                        else if (usrPass.Length < 9) //COMPTE que es podria usar si l'usuari l'activés .... però NO està bloquejat  // == "") //"{crypt}26hCbjHiN7LZo")
                            {
                            conter++;
                            this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("Compte LDAP no activat encara = " + (string)camps[0] + "i dades =" + Valor); ;});
                            this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText(Environment.NewLine); ;});
                            CompteInactiu.Add(Valor + ";"+ (string)registreTrobat["uid"]+";" + (string)registreTrobat["mailMessageStore"]);
                            }
                        else // abans 2023
                            //ActiveVoters.Add(dni, Valor); //DNI;cognoms;etc...
                            //2025 invote 
                            ActiveVoters.Add(dni, DadesItemVotantCens);
                            }
                         else
                         {
                             DadesItemVotantCens = (string)registreTrobat["uid"] + "@udl.cat" + "," + DadesItemVotantCens;
                             if (registreTrobat.ContainsKey("accountStatus"))
                             {
                                 if ((string)(registreTrobat["accountStatus"]) == "ACTIU")
                                     ActiveVoters.Add(dni, DadesItemVotantCens);
                             }
                         }
                        /*
                        if (registreTrobat.ContainsKey("accountStatus"))
                            {
                            if ((string)(registreTrobat["accountStatus"]) == "ACTIU")
                                ActiveVoters.Add(dni, Valor);
                            //(string)ItemVoter.Key, (string)ItemVoter.Value);
                            }
                        else
                            {
                            conter++;
                            this.status.BeginInvoke((MethodInvoker)delegate() {status.AppendText("Compte no actiu a LDAP " + (string)camps[0] + "i dades =" + Valor);;});
                            this.status.BeginInvoke((MethodInvoker)delegate() {status.AppendText(Environment.NewLine);;});
                            CompteLocked.Add(Valor);
                            }
                        */
                        //string []camps = lineOfText.Split(',');
                        // registreTrobat
                        //  System.Data.DataTable LlistaCandidats = LDAP.GetDataTable("CandidatsRegistre");

                        }
                    else
                        {
                        conter++;
                        this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("Compte no existeix a LDAP " + (string)camps[0] + "i dades =" + Valor); ;});
                        this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText(Environment.NewLine); ;});
                        }
                    }
                else
                    {
                    this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("uid buit del CSV = "+(string)camps[1]); ;});
                    this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText(Environment.NewLine); ;});                            
                    }
#if DEBUG_10
                if (limitTest > 25)
                     {
                     status.AppendText("** Càrrega completa, ja podeu Exportar si voleu");
                     status.AppendText(Environment.NewLine);
                     return;
                     }
#endif
                limitTest++;
                this.lbStatus.BeginInvoke((MethodInvoker)delegate() { loaderBar.PerformStep(); ;});
                this.lbStatus.BeginInvoke((MethodInvoker)delegate() { this.lbStatus.Text = "Carregant " + limitTest + " de " + loaderBar.Maximum; ;});

                }

            this.status.BeginInvoke((MethodInvoker)delegate() {status.AppendText("**** Validació de comptes actius a LDAP Processada!");;});
            this.status.BeginInvoke((MethodInvoker)delegate() {status.AppendText("***** " + conter + " votants amb problemes al compte LDAP(no-existe,no-activat,etc...");;});
            this.status.BeginInvoke((MethodInvoker)delegate() {status.AppendText("** Càrrega completa, ja podeu Exportar si voleu");;});
            this.status.BeginInvoke((MethodInvoker)delegate() {status.AppendText(Environment.NewLine); ;});
               
            }
        bool entornPRO = true;
        public string ExportForm="";
        public void ProcesParalel_ExportaCens_to_Paperetes()
        {
            string Filtre = "";
            int conter = 0;
            int limitTest = 0;

            //OUT FORMAT:
            string OutLIneFormatPapereta = ExportForm;


            //this.bStopImportLoader = false;

            System.Threading.Thread.Sleep(1000);
            try
                        {

                        //INPUT as : 43717113R;ABASCAL;ROBERT;MARIA INMACULADA
                        //INPUT as : h7281026@udl.cat,FERMIN JESUS,ALCASENA,URDIROZ,,1.0

            using (StreamWriter sw = new StreamWriter(
                File.Open(System.IO.Directory.GetCurrentDirectory() + "//output/PaperetaDeCens_"+RecursosProgramacio.EinesCadenes.GetCadenaAleatoriaNumerica(6)+".pnyx", 
                FileMode.Create), Encoding.UTF8))
                {
                //foreach (DictionaryEntry s in voters)
                foreach (string s in dniLIst)                
                    {
                    
                        string LineaCens = s;
                        string[] camps = LineaCens.Split(',');

                        string LineOut = OutLIneFormatPapereta;
                        if (camps[1] != null)
                            LineOut = LineOut.Replace("@Nom", camps[1]);
                        else
                            LineOut = LineOut.Replace("@Nom", "");

                        if (camps[2] != null)
                            LineOut = LineOut.Replace("@Cognom1", camps[2]);
                        else
                            LineOut = LineOut.Replace("@Cognom1", "");

                        if (camps[3] != null)
                            LineOut = LineOut.Replace("@Cognom2", camps[3]);
                        else
                            LineOut = LineOut.Replace("@Cognom2", "");
                        /*
                              string[] camps = LineaCens.Split(';');
                         
                            string LineOut = OutLIneFormatPapereta;
                            if (camps[1] != null)
                                LineOut=LineOut.Replace("@Nom", camps[3]);
                            else
                                LineOut = LineOut.Replace("@Nom", "");

                            if (camps[2] != null)
                                LineOut = LineOut.Replace("@Cognom1", camps[1]);
                            else
                                LineOut = LineOut.Replace("@Cognom1", "");

                            if (camps[3] != null)
                                LineOut = LineOut.Replace("@Cognom2", camps[2]);
                            else
                                LineOut = LineOut.Replace("@Cognom2", "");
                        */
                        sw.WriteLine(LineOut);

                        limitTest++;
                        this.lbStatus.BeginInvoke((MethodInvoker)delegate() { loaderBar.PerformStep(); ;});
                        this.lbStatus.BeginInvoke((MethodInvoker)delegate() { this.lbStatus.Text = "Exportant " + limitTest + " de " + loaderBar.Maximum; ;});

                        }                                            
            }
            this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("**** Export de cens a papereta finalitzat."); ;});
            //this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("***** " + conter + " votants amb problemes al compte LDAP(no-existe,no-activat,etc..."); ;});
            //this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("** Càrrega completa, ja podeu Exportar si voleu"); ;});
            this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText(Environment.NewLine); ;});
                        }
            catch (Exception E)
                {

                this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("ERROR processant" + E.Message); ;});
                this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText(Environment.NewLine); ;});
                }

        }

        private string Import_Cap_FormatTXT="";
        private string Import_SubGrup_FormatTXT="";
        private string Import_Candidat_FormatTXT = "";

        public void ProcesParalel_ExportaCens_TotsElegibles_LlocsCobrirX()
        {

            string Filtre = "";
            int conter = 0;
            int limitTest = 0;

            //OUT FORMAT:
            string OutLIneFormatPapereta = ExportForm;

            //this.bStopImportLoader = false;
            string NomArxiu_TotsElegibles_cap = System.IO.Directory.GetCurrentDirectory() + "//formats/TotsElegibles_cap.txt";
            var filestream = new System.IO.FileStream(NomArxiu_TotsElegibles_cap,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);

            var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            this.Import_Cap_FormatTXT = file.ReadToEnd();
            //cap

            string NomArxiu_TotsElegibles_subGrup = System.IO.Directory.GetCurrentDirectory() + "//formats/TotsElegibles_subGrup.txt";
            var filestream2 = new System.IO.FileStream(NomArxiu_TotsElegibles_subGrup,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);

            var file2 = new System.IO.StreamReader(filestream2, System.Text.Encoding.UTF8, true, 128);
            this.Import_SubGrup_FormatTXT = file2.ReadToEnd();
            //subgrup

            string NomArxiu_TotsElegibles_Candidat= System.IO.Directory.GetCurrentDirectory() + "//formats/TotsElegibles_candidat.txt";
            var filestream3 = new System.IO.FileStream(NomArxiu_TotsElegibles_Candidat,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);

            var file3 = new System.IO.StreamReader(filestream3, System.Text.Encoding.UTF8, true, 128);
            this.Import_Candidat_FormatTXT = file3.ReadToEnd();
            //Import_Candidat_FormatTXT

            string Base_EleccioID="";

            System.Threading.Thread.Sleep(1000);
            try
            {
                //INPUT as : 43717113R;ABASCAL;ROBERT;MARIA INMACULADA
                //INPUT as : h7281026@udl.cat,FERMIN JESUS,ALCASENA,URDIROZ,,1.0

                using (StreamWriter sw = new StreamWriter(
                    File.Open(System.IO.Directory.GetCurrentDirectory() + "//output/" + System.DateTime.Now.Year.ToString() + "_TotsElegiblesLlocCobrir_" + RecursosProgramacio.EinesCadenes.GetCadenaAleatoriaNumerica(6) + ".invote",
                    FileMode.Create), Encoding.UTF8))
                {
                    //************************************************************
                    sw.WriteLine(this.Import_Cap_FormatTXT);
                    //cap general de tot l'arxiu
                    //************************************************************
                    bool CalPosarEleccioSubgrup = true;
                    ushort nItera_LlocsCobrir = 0;
                    ushort CandidatNumeral = 1;

                    //Agafant d'una Hastable, el dictionary a vegades no queda igual ordenat que el CSV origen 
                    //i no poden depurar bé els resultats
                    //foreach (DictionaryEntry item in LlocsCobrir) 
                    //llista sequencial 1:1 ordre com el CSV d'entrada quedefineix cada electionID

                    int a = 0;
                    foreach(string Eleccio in CodisEleccioEnOrdreSegonsCSV)
                    {
                        /*if (Eleccio == "FM-DMEX-B1")
                            a = 2;*/
                        this.loaderBar.BeginInvoke((MethodInvoker)delegate() { loaderBar.PerformStep(); ;});
                        
                        if (LlocsCobrir[Eleccio]!=null)//item.Key] != null)
                        {
                            UrnaDef EleccioUrna = (UrnaDef)LlocsCobrir[Eleccio];
                            string SubGrupCap = this.Import_SubGrup_FormatTXT;
                            //EleccioUrna.
                            SubGrupCap = SubGrupCap.Replace("@AliesEleccio", EleccioUrna.EleccioAlies);
                            SubGrupCap = SubGrupCap.Replace("@NomAleccio", EleccioUrna.Descripcio);
                            SubGrupCap = SubGrupCap.Replace("@District", EleccioUrna.DisctricteCentre);
                            
                            if(entornPRO)
                                SubGrupCap = SubGrupCap.Replace(",\"District1\"", "");

                            //SubGrupCap = SubGrupCap.Replace("@SubDistrict", EleccioUrna.SubDisctricteDepartament);                            
                            SubGrupCap = SubGrupCap.Replace("@LlocsACobrir", EleccioUrna.LlocsACobrir.ToString());
                                                        
                            CalPosarEleccioSubgrup = true;
                            if (!CensosPerEleccions.ContainsKey((string)EleccioUrna.EleccioAlies))
                                CalPosarEleccioSubgrup = false;
                            if (CalPosarEleccioSubgrup)
                            {
                                sw.WriteLine(SubGrupCap);

                                nItera_LlocsCobrir = (ushort)EleccioUrna.LlocsACobrir;

                                string[] strID = EleccioUrna.EleccioAlies.Split('-');
                                Base_EleccioID = strID[0] + "-" + strID[1] + "-" + strID[2].Substring(0, 1);

                                if (CensosPerEleccionsAgrupat[Base_EleccioID] != null)
                                {
                                    ArrayList CandidatsPapereta = (ArrayList)CensosPerEleccionsAgrupat[Base_EleccioID];
                                    foreach (VotantCandidat candi in CandidatsPapereta)
                                    {
                                        string LineOut = this.Import_Candidat_FormatTXT;
                                        /*if (candi.NomComplet.StartsWith("CARLOS MATEU"))
                                            LineOut = LineOut +"ÑÑÑÑÑÑÑÑÑÑÑÑÑ";
                                        else      */
                                        LineOut = LineOut.Replace("@NomComplet", candi.NomComplet);
                                        LineOut = LineOut.Replace("@candidatOrdre", "candidat" + CandidatNumeral.ToString());

                                        sw.WriteLine(LineOut);

                                        CandidatNumeral++;
                                    }
                                }
                            }
                        }
                    }

                    this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("**** Export de cens a papereta finalitzat."); ;});
                    //this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("***** " + conter + " votants amb problemes al compte LDAP(no-existe,no-activat,etc..."); ;});
                    //this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("** Càrrega completa, ja podeu Exportar si voleu"); ;});
                    this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText(Environment.NewLine); ;});

                    this.lbStatus.BeginInvoke((MethodInvoker)delegate() { lbStatus.ResetText(); ;});

                    CodisEleccioEnOrdreSegonsCSV.Clear();
                    CensosPerEleccionsAgrupat.Clear();
                    CensosPerEleccions.Clear();
                    LlocsCobrir.Clear();
                }
            }
            catch (Exception E)
            {
                this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText("ERROR processant" + E.Message); ;});
                this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText(Environment.NewLine); ;});
            }










        }
        
        private bool bStopImportLoader = false;
        private static Hashtable voters = null;
        private static Hashtable CensosPerEleccions = null; // <-- conté arraylist de items tipo VotantCandidat
        private static Hashtable CensosPerEleccionsAgrupat = null; // <-- conté arraylist de items tipo VotantCandidat
        private static ArrayList dniLIst = null; //per mantenir el mateix ordre que al input
        private static Hashtable LlocsCobrir=null;
        private static ArrayList CodisEleccioEnOrdreSegonsCSV= null;
        private static Hashtable ActiveVoters = null;
        private static ArrayList CompteLocked = null;
        private static ArrayList CompteInactiu = null;
        private Thread ptProcesImportCSV = null;

        public struct UrnaDef
        {
            public string Descripcio;
            public string EleccioAlies;
            public string EleccioId;
            public int LlocsACobrir;
            public string DisctricteCentre;
            public string SubDisctricteDepartament;
        }

        public struct VotantCandidat
        {
            public string NomComplet;
            public string Nom;
            public string Congom1;
            public string Congom2;            
            public string EleccioId;
            public string EleccioId_Base;
            public string Centre;
            public string UID;
            
        }

        public Form1()
            {
            InitializeComponent();
            }

        private void totalÚnicsINoActiusALDAPToolStripMenuItem_Click(object sender, EventArgs e)            
            {
            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "//input");
            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "//output");
            obreINput.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            obreINput.FileName = "*.csv";
            bool OK = false;
            if (obreINput.ShowDialog() == DialogResult.OK)
                OK = true;
            if (!OK) return;


            string NomArxiu = obreINput.FileName;
            var filestream = new System.IO.FileStream(NomArxiu,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            
            var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            string lineOfText = "";
            voters = new Hashtable();
            dniLIst=new ArrayList();
            //"mag31,49759574V,MARIAM EL,AACHOURY,GASMI,mag31@alumnes.udl.cat"
            while ((lineOfText = file.ReadLine()) != null)
                {
                string []camps = lineOfText.Split(',');
                if (!voters.ContainsKey((string)camps[1])) //VOTA el DNI no els uids
                    {
                    voters.Add(camps[1], lineOfText);
                    dniLIst.Add((string)camps[1]);
                    }
                else
                    {
                    status.AppendText("Duplicat trobat amb login = " + (string)camps[0] + "i DNI =" + (string)camps[1]);
                    status.AppendText(Environment.NewLine);
                    }
                }

            loaderBar.Minimum = 0;
            #if DEBUG_10
            loaderBar.Maximum = 25;
#else
            loaderBar.Maximum = voters.Count;
#endif
            
            loaderBar.Step = 1;
            loaderBar.Refresh();

            ConectorLDAP.Conector LDAP = new ConectorLDAP.Conector("cialis.udl.net");
            string Filtre = "";
            ActiveVoters=new Hashtable();
            CompteLocked = new ArrayList();
            CompteInactiu = new ArrayList();
      
            status.AppendText("**** Arxiu entrada Processat!");
            status.AppendText(Environment.NewLine);

            int conter = 0;
            int limitTest = 0;
            status.AppendText("*** Avaluant un a un els " + voters.Count +" votants");
            status.AppendText(Environment.NewLine);


            this.ptProcesImportCSV = new Thread(new ThreadStart(ProcesParalel_CARREGA));
            this.ptProcesImportCSV.Start();




                
            }

        private void ExportUnicsLDAP_Click(object sender, EventArgs e)
            {
            // ActiveVoters items .Value a output.CSV

            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "//input");
            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "//output");
            DesaOutput.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + "//output";
            DesaOutput.ShowDialog();

            string NomArxiu = DesaOutput.FileName;
            loaderBar.Minimum = 0;
            loaderBar.Maximum = ActiveVoters.Count;
            loaderBar.Step = 1;

            using (StreamWriter sw = new StreamWriter(File.Open(NomArxiu, FileMode.Create), Encoding.UTF8))
                {
                foreach (DictionaryEntry ItemVoter in ActiveVoters)
                {
                    sw.WriteLine((string)ItemVoter.Value);
                    loaderBar.PerformStep();
                }
            status.AppendText("Votants amb compte LDAP actiu exportats OK");
            status.AppendText(Environment.NewLine);
                }
            }

        private void comptesBloquejatsToolStripMenuItem_Click(object sender, EventArgs e)
            {
            // ActiveVoters items .Value a output.CSV

            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "//input");
            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "//output");
            DesaOutput.FileName = "ComptesBloquejats_idFile_" + System.DateTime.Now.Ticks + ".csv";
            DesaOutput.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + "//output";
            DesaOutput.ShowDialog();

            string NomArxiu = DesaOutput.FileName;
            loaderBar.Minimum = 0;
            loaderBar.Maximum = CompteLocked.Count;
            loaderBar.Step = 1;

            using (StreamWriter sw = new StreamWriter(File.Open(NomArxiu, FileMode.Create), Encoding.UTF8))
                {
                /*foreach (DictionaryEntry ItemVoter in CompteLocked)
                    {*/
                for (int i = 0; i < CompteLocked.Count; i++)            
                        {
                        sw.WriteLine((string)CompteLocked[i]);
                         loaderBar.PerformStep();
                    }
                status.AppendText("Votants amb compte LDAP *bloquejat* exportats OK");
                status.AppendText(Environment.NewLine);
                }
            }

        private void btStop_Click(object sender, EventArgs e)
            {
            bStopImportLoader = true;
            if (this.ptProcesImportCSV.IsAlive)
                {
                this.ptProcesImportCSV.Abort();
                status.AppendText("*** STOP! HALT!");
                status.AppendText(Environment.NewLine);
                }
            }

        private void sortirToolStripMenuItem_Click(object sender, EventArgs e)
            {
            this.Close();
            }

        private void comptesNoActivatsToolStripMenuItem_Click(object sender, EventArgs e)
            {
            // ActiveVoters items .Value a output.CSV

            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "//input");
            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "//output");
            DesaOutput.FileName = "ComptesNoActivats_idFile_" + System.DateTime.Now.Ticks + ".csv";
            DesaOutput.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + "//output";
            DesaOutput.ShowDialog();

            string NomArxiu = DesaOutput.FileName;
            loaderBar.Minimum = 0;
            loaderBar.Maximum = CompteInactiu.Count;
            loaderBar.Step = 1;

            using (StreamWriter sw = new StreamWriter(File.Open(NomArxiu, FileMode.Create), Encoding.UTF8))
                {
                /*foreach (DictionaryEntry ItemVoter in CompteLocked)
                    {*/
                for (int i = 0; i < CompteInactiu.Count; i++)
                    {
                    sw.WriteLine((string)CompteInactiu[i]);
                    loaderBar.PerformStep();
                    }
                status.AppendText("Votants potencials amb compte LDAP encara no activat, exportats OK");
                status.AppendText(Environment.NewLine);
                }
            }

        private void carregarToolStripMenuItem_Click(object sender, EventArgs e)
            {
            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "//input");
            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "//output");
            obreINput.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            obreINput.FileName = "*.csv";
            bool OK = false;
            if (obreINput.ShowDialog() == DialogResult.OK)
                OK = true;
            if (!OK) return;


            string NomArxiu = obreINput.FileName;
            if (NomArxiu.Length == 0) return;
            var filestream = new System.IO.FileStream(NomArxiu,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);

            var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            string lineOfText = "";
            voters = new Hashtable();
            dniLIst = new ArrayList();
            //"mag31,49759574V,MARIAM EL,AACHOURY,GASMI,mag31@alumnes.udl.cat"
            while ((lineOfText = file.ReadLine()) != null)
                {
                string[] camps = lineOfText.Split(';');
                if (!voters.ContainsKey((string)camps[0])) //VOTA el DNI no els uids
                    {
                    voters.Add(camps[0], lineOfText);
                    dniLIst.Add((string)camps[0]);
                    }
                else
                    {
                    status.AppendText("Duplicat trobat amb login = " + (string)camps[0] + "i DNI =" + (string)camps[1]);
                    status.AppendText(Environment.NewLine);
                    }
                }

            loaderBar.Minimum = 0;
#if DEBUG_10
            loaderBar.Maximum = 25;
#else
            loaderBar.Maximum = voters.Count;
#endif

            loaderBar.Step = 1;
            loaderBar.Refresh();

            ConectorLDAP.Conector LDAP = new ConectorLDAP.Conector("cialis.udl.net");
            string Filtre = "";
            ActiveVoters = new Hashtable();
            CompteLocked = new ArrayList();
            CompteInactiu = new ArrayList();

            status.AppendText("**** Arxiu entrada Processat!");
            status.AppendText(Environment.NewLine);

            int conter = 0;
            int limitTest = 0;
            status.AppendText("*** Avaluant un a un els " + voters.Count + " votants");
            status.AppendText(Environment.NewLine);


            this.ptProcesImportCSV = new Thread(new ThreadStart(ProcesParalel_CARREGA_csv_PDI));
            this.ptProcesImportCSV.Start();


            }

        private void nomésÚnicsIActiusLDAPToolStripMenuItem_Click(object sender, EventArgs e)
            {
            // ActiveVoters items .Value a output.CSV

            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "//input");
            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "//output");
            DesaOutput.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + "//output";
            DesaOutput.ShowDialog();

            string NomArxiu = DesaOutput.FileName;
            loaderBar.Minimum = 0;
            loaderBar.Maximum = ActiveVoters.Count;
            loaderBar.Step = 1;

            using (StreamWriter sw = new StreamWriter(File.Open(NomArxiu, FileMode.Create), Encoding.UTF8))
                {
                foreach (DictionaryEntry ItemVoter in ActiveVoters)
                    {
                    sw.WriteLine((string)ItemVoter.Value);
                    loaderBar.PerformStep();
                    }
                status.AppendText("Votants amb compte LDAP actiu exportats OK");
                status.AppendText(Environment.NewLine);
                }
            }

        private void carregarToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "//input");
            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "//output");
            obreINput.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            obreINput.FileName = "*.csv";
            bool OK = false;
            if (obreINput.ShowDialog() == DialogResult.OK)
                OK = true;
            if (!OK) return;


            string NomArxiu = obreINput.FileName;
            var filestream = new System.IO.FileStream(NomArxiu,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            
            var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            string lineOfText = "";
            voters = new Hashtable();
            dniLIst = new ArrayList();
            // INPUT lines as : p4373064@udl.cat,CESAR PEDRO,EZCURRA,CIAURRIZ,,1.0

            //INPUT as : 43717113R;ABASCAL;ROBERT;MARIA INMACULADA
            //INPUT as : h7281026@udl.cat,FERMIN JESUS,ALCASENA,URDIROZ,,1.0

            while ((lineOfText = file.ReadLine()) != null)
                {
                string[] camps = lineOfText.Split(';');
                if (!voters.ContainsKey((string)camps[0])) //VOTA el DNI no els uids
                    {
                    voters.Add(camps[0], lineOfText); //controlem NO DUPLICATS!
                    dniLIst.Add(lineOfText); // PRESERVEM L¡ORDRE, en un arryLIST!!!
                    //dniLIst.Add((string)camps[1]);
                    }
                else
                    {
                    status.AppendText("Duplicat trobat amb nom = " + (string)camps[1] + (string)camps[2] + (string)camps[3] + "i mail =" + (string)camps[0] + " Arreglar la ínea : " + lineOfText);
                    status.AppendText(Environment.NewLine);
                    return;
                    }
                }

            loaderBar.Minimum = 0;
#if DEBUG_10
            loaderBar.Maximum = 25;
#else
            loaderBar.Maximum = voters.Count;
#endif

            loaderBar.Step = 1;
            loaderBar.Refresh();

            //ConectorLDAP.Conector LDAP = new ConectorLDAP.Conector("cialis.udl.net");
            string Filtre = "";
            ActiveVoters = new Hashtable();
            CompteLocked = new ArrayList();
            CompteInactiu = new ArrayList();

            status.AppendText("**** Arxiu entrada Processat!");
            status.AppendText(Environment.NewLine);
            /*
            int conter = 0;
            int limitTest = 0;
            //status.AppendText("*** Avaluant un a un els " + voters.Count + " votants");
            status.AppendText(Environment.NewLine);


            this.ptProcesImportCSV = new Thread(new ThreadStart(ProcesParalel_ExportaCens_to_Paperetes));
            this.ptProcesImportCSV.Start();
            */



            }

        private void exportarToolStripMenuItem2_Click(object sender, EventArgs e)
            {
            
            }

        private void Form1_Load(object sender, EventArgs e)
            {

            }

        private void llistaObertaToolStripMenuItem_Click(object sender, EventArgs e)
            {
            string Filtre = "";
            ActiveVoters = new Hashtable();
            CompteLocked = new ArrayList();
            CompteInactiu = new ArrayList();

            status.AppendText("**** Init exportació llista Oberta");
            status.AppendText(Environment.NewLine);

            int conter = 0;
            int limitTest = 0;
            //status.AppendText("*** Avaluant un a un els " + voters.Count + " votants");
            status.AppendText(Environment.NewLine);

            this.ExportForm = "\"[MESSAGE]\",\"Missatge per Defecte\"\r\n\""+
                "[CANDIDATE]\",\"0\",\"BASE\",\"false\",\"false\",,,,\r\n\""+
                "[MESSAGE]\",\"@Cognom1 @Cognom2, @Nom\"";

            this.ptProcesImportCSV = new Thread(new ThreadStart(ProcesParalel_ExportaCens_to_Paperetes));
            this.ptProcesImportCSV.Start();
            }

        private void lleidaSemiobertaToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
            string Filtre = "";
            ActiveVoters = new Hashtable();
            CompteLocked = new ArrayList();
            CompteInactiu = new ArrayList();

            status.AppendText("**** Init exportació llista Semioberta");
            status.AppendText(Environment.NewLine);

            int conter = 0;
            int limitTest = 0;
            //status.AppendText("*** Avaluant un a un els " + voters.Count + " votants");
            status.AppendText(Environment.NewLine);

            this.ExportForm="\"[CANDIDATE_LIST]\",\"0\",\"BASE\",\"false\",\"false\",,,,\r\n" +
            "\"[MESSAGE]\",\"@Cognom1 @Cognom2, @Nom\"\r\n\"[MESSAGE]\",\"Missatge per Defecte\"";

            this.ptProcesImportCSV = new Thread(new ThreadStart(ProcesParalel_ExportaCens_to_Paperetes));
            this.ptProcesImportCSV.Start();
        }

        private void ckLlocsACobrir_Click(object sender, EventArgs e)
            {
            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "//output");
            obreINput.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            obreINput.ShowDialog();

            string NomArxiu = obreINput.FileName;
            var filestream = new System.IO.FileStream(NomArxiu,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            
            var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            string lineOfText = "";

            CodisEleccioEnOrdreSegonsCSV = new ArrayList();
            voters = new Hashtable();
            dniLIst = new ArrayList();
            LlocsCobrir=new Hashtable();

            try
            {
                while ((lineOfText = file.ReadLine()) != null)
                    {
                        UrnaDef CurrentUrna = new UrnaDef();
                        string[] Dades = lineOfText.Split(';');
                        CurrentUrna.Descripcio = Dades[0];
                        CurrentUrna.EleccioAlies = Dades[1];
                        CurrentUrna.EleccioId = Dades[2];
                        CurrentUrna.LlocsACobrir = Int16.Parse(Dades[3]);
                        LlocsCobrir.Add(Dades[1], CurrentUrna);
                        CodisEleccioEnOrdreSegonsCSV.Add((string)CurrentUrna.EleccioAlies);
                    }
            }
            catch (Exception ee)
                {
                }
            }

        private void totsElegiblesToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void carregarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int countadorGlobal = 0;

            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "//input");
            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "//output");
            obreINput.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            
            obreINput.FileName = "*.csv";
            bool OK = false;
            if (obreINput.ShowDialog() == DialogResult.OK)
                OK = true;
            if (!OK) return;

            string NomArxiu = obreINput.FileName;
            var filestream = new System.IO.FileStream(NomArxiu,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);

            var file = new System.IO.StreamReader(filestream, 
                System.Text.Encoding.UTF8,
                true, 128);

            string lineOfText = "";
            CensosPerEleccions = new Hashtable();
            CensosPerEleccionsAgrupat = new Hashtable();

            
            //arraylist de VotantCandidat
            voters = new Hashtable();
            dniLIst = new ArrayList();
            // INPUT lines as : p4373064@udl.cat,CESAR PEDRO,EZCURRA,CIAURRIZ,,1.0

            //INPUT as : 43717113R;ABASCAL;ROBERT;MARIA INMACULADA
            //INPUT as : h7281026@udl.cat,FERMIN JESUS,ALCASENA,URDIROZ,,1.0

            string IdEleccioVolta="";
            string BaseIdEleccioVolta = "";

            ArrayList Cens_inEleccio = new ArrayList();
            ArrayList Cens_inEleccioAgrupat = new ArrayList();
            char separador = ';';
                        
            while ((lineOfText = file.ReadLine()) != null)
            {
                if (!lineOfText.Contains(";")) separador = ',';
                string[] camps = lineOfText.Split(separador);

                string UID=camps[0];
                string Centre = camps[2];
                string nomcomplet = camps[1];
                string electionID = camps[4];
                string[] strID=electionID.Split('-');
                string BaseElectionID = strID[0] + "-" + strID[1] + "-" + strID[2].Substring(0, 1);
                
                if (BaseElectionID == "EPS-DEIE-B")
                    nomcomplet = "vvvvv" + nomcomplet;

                /*if (nomcomplet.StartsWith("CARLOS MATEU"))
                    nomcomplet = "ÑÑÑÑÑÑÑÑÑÑÑÑ";*/
                // candidats agrupats, ...A1 ,A2 A3,....Tots en ["ETSEAFIV-DCA-A"]
                if (BaseIdEleccioVolta != BaseElectionID)
                {
                    if (BaseIdEleccioVolta.Length > 1) //al acabar la iteracció d'un election ID, i que no sigo el primer
                        CensosPerEleccionsAgrupat.Add(BaseIdEleccioVolta, Cens_inEleccioAgrupat);

                    Cens_inEleccioAgrupat = new ArrayList();
                    BaseIdEleccioVolta = BaseElectionID;
                }

                //candidats per ID elecció (sense tenir en compte els grups A1,...An o B1,...Bn)
                if(IdEleccioVolta!=electionID)
                {
                    //i desar la colecció de candidats dins de la colecció de censos per elecció
                    if( IdEleccioVolta.Length>1) //al acabar la iteracció d'un election ID, i que no sigo el primer
                        CensosPerEleccions.Add(IdEleccioVolta, Cens_inEleccio);

                    Cens_inEleccio=new ArrayList();
                    IdEleccioVolta=electionID;
                }

                VotantCandidat nouCandidat=new VotantCandidat();
                nouCandidat.NomComplet = nomcomplet;
                nouCandidat.EleccioId = electionID;
                nouCandidat.UID = UID;
                nouCandidat.EleccioId_Base = BaseElectionID;
                nouCandidat.Centre = Centre;

                countadorGlobal++;

                //desar el candidat de cens adins d'una coleccio de candidats

                if (!Cens_inEleccioAgrupat.Contains(nouCandidat))
                {
                    Cens_inEleccioAgrupat.Add(nouCandidat);
                }

                if (!Cens_inEleccio.Contains(nouCandidat))
                {
                    Cens_inEleccio.Add(nouCandidat);
                }
                else
                {
                    status.AppendText("Duplicat trobat al cens " + electionID + " amb dades = "+ lineOfText);
                    status.AppendText(Environment.NewLine);
                    return;
                }
            }
            
            CensosPerEleccions.Add(IdEleccioVolta, Cens_inEleccio);
            CensosPerEleccionsAgrupat.Add(BaseIdEleccioVolta, Cens_inEleccioAgrupat);

            loaderBar.Minimum = 0;
#if DEBUG_10
            loaderBar.Maximum = 25;
#else
            loaderBar.Maximum = countadorGlobal;
#endif

            loaderBar.Step = 1;
            loaderBar.Refresh();

            //ConectorLDAP.Conector LDAP = new ConectorLDAP.Conector("cialis.udl.net");
            string Filtre = "";
            ActiveVoters = new Hashtable();
            CompteLocked = new ArrayList();
            CompteInactiu = new ArrayList();

            status.AppendText("**** Arxiu entrada Processat! "+countadorGlobal +" línies CSV");
            status.AppendText(Environment.NewLine);
            /*
            int conter = 0;
            int limitTest = 0;
            //status.AppendText("*** Avaluant un a un els " + voters.Count + " votants");
            status.AppendText(Environment.NewLine);


            this.ptProcesImportCSV = new Thread(new ThreadStart(ProcesParalel_ExportaCens_to_Paperetes));
            this.ptProcesImportCSV.Start();
            */

        }

        private void carregarEleccionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "//output");
            obreINput.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            obreINput.FileName = "*.csv";
            
            bool OK=false;
            if (obreINput.ShowDialog() == DialogResult.OK)
                OK = true;
            if (!OK) return;
                

            string NomArxiu = obreINput.FileName;
            var filestream = new System.IO.FileStream(NomArxiu,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);

            var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            string lineOfText = "";
            voters = new Hashtable();
            dniLIst = new ArrayList();
            LlocsCobrir = new Hashtable();
            CodisEleccioEnOrdreSegonsCSV = new ArrayList();

            status.AppendText("Inici carrega items definició elecció");
            status.AppendText(Environment.NewLine);
            try
            {
                char separador = ';';
                while ((lineOfText = file.ReadLine()) != null)
                {
                    if (!lineOfText.Contains(";")) separador = ',';
                    
                    UrnaDef CurrentUrna = new UrnaDef();
                    string[] Dades = lineOfText.Split(separador);
                    CurrentUrna.Descripcio = Dades[0];
                    CurrentUrna.EleccioAlies = Dades[1];
                    string[] InfoElectionMyID = Dades[1].Split('-');
                    //InfoElectionMyID
                    CurrentUrna.DisctricteCentre = InfoElectionMyID[0];
                    CurrentUrna.SubDisctricteDepartament = InfoElectionMyID[1];

                    CurrentUrna.EleccioId = Dades[2];
                    CurrentUrna.LlocsACobrir = Int16.Parse(Dades[3]);
                    LlocsCobrir.Add(Dades[1], CurrentUrna);
                    CodisEleccioEnOrdreSegonsCSV.Add(CurrentUrna.EleccioAlies);
                }
                status.AppendText("Carregants " + LlocsCobrir.Count+" items definició elecció");
                status.AppendText(Environment.NewLine);
                
                loaderBar.Minimum = 0;
                loaderBar.Maximum = CodisEleccioEnOrdreSegonsCSV.Count;
                loaderBar.Step = 1;
            }
            catch (Exception ee)
            {
            
            status.AppendText("Error carregant " + NomArxiu);
            status.AppendText(Environment.NewLine);
            status.AppendText("Error "+ee.Message);
            status.AppendText(Environment.NewLine);
            }
        }

        private void totesElegiblesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool errorOp = true;
            if (CodisEleccioEnOrdreSegonsCSV != null)
            {
                if (CodisEleccioEnOrdreSegonsCSV.Count > 0)
                    errorOp=false;                                 
            }
            if(errorOp)
            {
                status.AppendText(Environment.NewLine);
                status.AppendText("**** Error, no s'ha carregar configuració d'eleccions i urnes");
                status.AppendText(Environment.NewLine);
                status.Refresh();
                return;
            }
                
            string Filtre = "";
            ActiveVoters = new Hashtable();
            CompteLocked = new ArrayList();
            CompteInactiu = new ArrayList();

            status.AppendText("**** Init exportació totsElegibles");
            status.AppendText(Environment.NewLine);

            int conter = 0;
            int limitTest = 0;
            //status.AppendText("*** Avaluant un a un els " + voters.Count + " votants");
            status.AppendText(Environment.NewLine);

            this.ExportForm = "\"[MESSAGE]\",\"Missatge per Defecte\"\r\n\"" +
                "[CANDIDATE]\",\"0\",\"BASE\",\"false\",\"false\",,,,\r\n\"" +
                "[MESSAGE]\",\"@Cognom1 @Cognom2, @Nom\"";

            this.entornPRO = true;
            DialogResult resposta = MessageBox.Show("¿ Generar per a entorn de PRE ?", "Triar entorn", MessageBoxButtons.YesNo);
            if (resposta == DialogResult.Yes)
                this.entornPRO = false;

            this.ptProcesImportCSV = new Thread(new ThreadStart(ProcesParalel_ExportaCens_TotsElegibles_LlocsCobrirX));
            this.ptProcesImportCSV.Start();
        }

        private void exportarAToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        }




    /*
        //Repetim la mega-papereta per tants llocs a cobrir

                        if (CensosPerEleccions[EleccioUrna.EleccioAlies] != null)
                        { //security check...
                        for (ushort urnesEleccio = 0; urnesEleccio < nItera_LlocsCobrir; urnesEleccio++)
                            {
                            //************************************************* ***********
                            sw.WriteLine(SubGrupCap);
                            //cap per subgrup elecció
                            //************************************************************

                            sw.WriteLine(">>>>>>>>>>Inici del bloc  Lloc a cobrir núm." + (urnesEleccio + 1).ToString());
                            //foreach (DictionaryEntry s in voters)
                            

                                ArrayList Candidats = (ArrayList)CensosPerEleccions[EleccioUrna.EleccioAlies];

                                string LineOut = "";
                                foreach (VotantCandidat CandiVot in Candidats)
                                    {
                                    LineOut = this.Import_Candidat_FormatTXT;
                                    LineOut = LineOut.Replace("@NomComplet", CandiVot.NomComplet);
                                    LineOut = LineOut.Replace("@candidatOrdre", "candidat" + CandidatNumeral.ToString());

                                    sw.WriteLine(LineOut);

                                    CandidatNumeral++;
                                    limitTest++;
                                    //TODO calcular el limit. candidats * llocs cobrir * electID (?!¿)
                                    //this.lbStatus.BeginInvoke((MethodInvoker)delegate() { loaderBar.PerformStep(); ;});
                                    //this.lbStatus.BeginInvoke((MethodInvoker)delegate() { this.lbStatus.Text = "Exportant " + limitTest + " de " + loaderBar.Maximum; ;});
                                    }
                                
                            sw.WriteLine("<<<<<<<<<<<< Fi del bloc  Lloc a cobrir núm. " + (urnesEleccio + 1).ToString());
                            }
                        }
                        else // fi si, del //security check...
                            {
                            sw.WriteLine("Falta cens elecció amb ID = " + EleccioUrna.EleccioAlies);
                            this.status.BeginInvoke((MethodInvoker)delegate()
                            {
                                status.AppendText("Falta cens elecció amb ID = " + EleccioUrna.EleccioAlies);
                                ;
                            });
                            this.status.BeginInvoke((MethodInvoker)delegate() { status.AppendText(Environment.NewLine); ;});
                            }




    */
}