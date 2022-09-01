using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;
using System.Collections;
using MathNet.Numerics.Statistics;
using System.Web.Script.Serialization;
namespace Velomax
{

    internal class Fournisseur
    {
        public string siret;
        public string nomEntreprise;
        public string idAdresse;
        public string codePostF;
        public string nomRueF;
        public int numRueF;
        public string contact;
        public int qualite;
        public List<Piece> approv;
        public List<int> nbPieces;
        public List<int> delaiA;
        public List<int> prixA;
        public Fournisseur(string siret, string nomEntreprise = null, string idAdresse = null, string codePostF = "00000", string nomRueF = null, int numRueF = 0, string contact = null, int qualite = 0)
        {
            this.siret = siret;
            this.nomEntreprise = nomEntreprise;
            this.idAdresse = idAdresse;
            this.codePostF = codePostF;
            this.nomRueF = nomRueF;
            this.numRueF = numRueF;
            this.contact = contact;
            this.qualite = qualite;
            this.approv = new List<Piece>();
            this.nbPieces = new List<int>();
            this.delaiA = new List<int>();
            this.prixA = new List<int>();
        }
        public static List<Fournisseur> LoadAllFournisseurs()
        {
            string scriptFournisseur = "SELECT * FROM Fournisseur";

            ArrayList preFournisseur;
            preFournisseur = Program.Requete(scriptFournisseur);
            List<string> siret = new List<string>();
            List<string> nomEntreprise = new List<string>();
            List<string> idAdresse = new List<string>();
            List<string> codePostF = new List<string>();
            List<string> nomRueF = new List<string>();
            List<int> numRueF = new List<int>();
            List<string> contact = new List<string>();
            List<int> qualite = new List<int>();

            string[] temp = new string[9];

            for (int i = 0; i < preFournisseur.Count; i++)
            {
                temp = preFournisseur[i].ToString().Split(',');
                for (int m = 0; m < temp.Length; m++)
                {
                    temp[m] = temp[m].Trim();
                }
                if (String.IsNullOrWhiteSpace(temp[1]))
                {
                    temp[1] = "null";
                }
                if (String.IsNullOrWhiteSpace(temp[2]))
                {
                    temp[2] = "null";
                }
                if (String.IsNullOrWhiteSpace(temp[3]))
                {
                    temp[3] = "null";
                }
                if (String.IsNullOrWhiteSpace(temp[4]))
                {
                    temp[4] = "null";
                }
                if (int.TryParse(temp[5], out var n1) == false)
                {
                    temp[5] = "0";
                }
                if (String.IsNullOrWhiteSpace(temp[6]))
                {
                    temp[6] = "null";
                }
                if (int.TryParse(temp[7], out var n2) == false)
                {
                    temp[7] = "0";
                }

                siret.Add(temp[0]);
                nomEntreprise.Add(temp[1]);
                idAdresse.Add(temp[2]);
                codePostF.Add(temp[3]);
                nomRueF.Add(temp[4]);
                numRueF.Add(Convert.ToInt32(temp[5]));
                contact.Add(temp[6]);
                qualite.Add(Convert.ToInt32(temp[7]));
            }

            List<Fournisseur> fournisseurList = new List<Fournisseur>();

            for (int j = 0; j < siret.Count; j++)
            {
                fournisseurList.Add(new Fournisseur(siret[j], nomEntreprise[j], idAdresse[j], codePostF[j], nomRueF[j], numRueF[j], contact[j], qualite[j]));
            }

            /*---------------------------------*/
            string scriptApprov;
            ArrayList curApprov;

            string[] tempApprov = new string[6];

            for (int k = 0; k < fournisseurList.Count; k++)
            {
                scriptApprov = String.Format("SELECT * FROM Approvisionnement WHERE siret = '{0}'", fournisseurList[k].siret);
                curApprov = Program.Requete(scriptApprov);
                for (int l = 0; l < curApprov.Count; l++)
                {
                    tempApprov = curApprov[l].ToString().Split(',');
                    for (int m = 0; m < tempApprov.Length; m++)
                    {
                        tempApprov[m] = tempApprov[m].Trim();
                    }
                    if (int.TryParse(tempApprov[2], out var n1) == false)
                    {
                        tempApprov[2] = "0";
                    }
                    if (int.TryParse(tempApprov[3], out var n2) == false)
                    {
                        tempApprov[3] = "0";
                    }
                    if (int.TryParse(tempApprov[4], out var n3) == false)
                    {
                        tempApprov[4] = "0";
                    }

                    Piece tempP = new Piece(tempApprov[1]);
                    Piece curP = tempP.loadPiece();
                    fournisseurList[k].approv.Add(curP);
                    fournisseurList[k].nbPieces.Add(Convert.ToInt32(tempApprov[2]));
                    fournisseurList[k].prixA.Add(Convert.ToInt32(tempApprov[3]));
                    fournisseurList[k].delaiA.Add(Convert.ToInt32(tempApprov[4]));
                }
            }

            return fournisseurList;
        }
        public Fournisseur LoadFournisseur()
        {
            string scriptFournisseur = String.Format("SELECT * FROM Fournisseur where siret = '{0}'", this.siret);

            ArrayList preFournisseur;
            preFournisseur = Program.Requete(scriptFournisseur);
            List<string> siret = new List<string>();
            List<string> nomEntreprise = new List<string>();
            List<string> idAdresse = new List<string>();
            List<string> codePostF = new List<string>();
            List<string> nomRueF = new List<string>();
            List<int> numRueF = new List<int>();
            List<string> contact = new List<string>();
            List<int> qualite = new List<int>();

            string[] temp = new string[9];

            for (int i = 0; i < preFournisseur.Count; i++)
            {
                temp = preFournisseur[i].ToString().Split(',');
                for (int m = 0; m < temp.Length; m++)
                {
                    temp[m] = temp[m].Trim();
                }
                if (String.IsNullOrWhiteSpace(temp[1]))
                {
                    temp[1] = "null";
                }
                if (String.IsNullOrWhiteSpace(temp[2]))
                {
                    temp[2] = "null";
                }
                if (String.IsNullOrWhiteSpace(temp[3]))
                {
                    temp[3] = "null";
                }
                if (String.IsNullOrWhiteSpace(temp[4]))
                {
                    temp[4] = "null";
                }
                if (int.TryParse(temp[5], out var n1) == false)
                {
                    temp[5] = "0";
                }
                if (String.IsNullOrWhiteSpace(temp[6]))
                {
                    temp[6] = "null";
                }
                if (int.TryParse(temp[7], out var n2) == false)
                {
                    temp[7] = "0";
                }

                siret.Add(temp[0]);
                nomEntreprise.Add(temp[1]);
                idAdresse.Add(temp[2]);
                codePostF.Add(temp[3]);
                nomRueF.Add(temp[4]);
                numRueF.Add(Convert.ToInt32(temp[5]));
                contact.Add(temp[6]);
                qualite.Add(Convert.ToInt32(temp[7]));
            }

            List<Fournisseur> fournisseurList = new List<Fournisseur>();

            for (int j = 0; j < siret.Count; j++)
            {
                fournisseurList.Add(new Fournisseur(siret[j], nomEntreprise[j], idAdresse[j], codePostF[j], nomRueF[j], numRueF[j], contact[j], qualite[j]));
            }

            
            /*---------------------------------*/
            string scriptApprov;
            ArrayList curApprov;

            string[] tempApprov = new string[6];

            for (int k = 0; k < fournisseurList.Count; k++)
            {
                scriptApprov = String.Format("SELECT * FROM Approvisionnement WHERE siret = '{0}'", fournisseurList[k].siret);
                curApprov = Program.Requete(scriptApprov);
                for (int l = 0; l < curApprov.Count; l++)
                {
                    tempApprov = curApprov[l].ToString().Split(',');
                    for (int m = 0; m < tempApprov.Length; m++)
                    {
                        tempApprov[m] = tempApprov[m].Trim();
                    }
                    if (int.TryParse(tempApprov[2], out var n1) == false)
                    {
                        tempApprov[2] = "0";
                    }
                    if (int.TryParse(tempApprov[3], out var n2) == false)
                    {
                        tempApprov[3] = "0";
                    }
                    if (int.TryParse(tempApprov[4], out var n3) == false)
                    {
                        tempApprov[4] = "0";
                    }

                    Piece tempP = new Piece(tempApprov[1]);
                    Piece curP = tempP.loadPiece();
                    fournisseurList[k].approv.Add(curP);
                    fournisseurList[k].nbPieces.Add(Convert.ToInt32(tempApprov[2]));
                    fournisseurList[k].prixA.Add(Convert.ToInt32(tempApprov[3]));
                    fournisseurList[k].delaiA.Add(Convert.ToInt32(tempApprov[4]));
                }
            }
            return fournisseurList[0];
        }
        public string Json()
        {
            var jsonString = new JavaScriptSerializer();
            //Use of Serialize() method
            var jsonStringResult = jsonString.Serialize(this);
            string res = jsonStringResult;
            return res;

        }
        public static void ToJson()
        {
            string json = "";
            List<Fournisseur> pieceList = Fournisseur.LoadAllFournisseurs();
            for (int i = 0; i < pieceList.Count; i++)
            {
                json = json + pieceList[i].Json() + ",";
            }
            json = "[" + json + "]";
            using (StreamWriter writer = new StreamWriter("json/Fournisseur.json"))
            {
                writer.WriteLine(json);
            }
        }
        public void addFournisseur()
        {
            if (this != null)
            {
                string script = string.Format("INSERT INTO `velomax`.`Fournisseur` (`siret`,`nomEntreprise`,`idAdresse`,`codePostF`,`nomRueF`,`numRueF`,`contact`,`qualite`) " +
                                                   "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');", this.siret, this.nomEntreprise, this.idAdresse, this.codePostF, this.nomRueF, this.numRueF, this.contact, this.qualite);
                Program.Execute(script);
            }
            if (this != null && this.approv != null)
            {
                string scriptApprov = string.Empty;
                for (int i = 0; i < this.approv.Count; i++)
                {
                    scriptApprov = string.Format("INSERT INTO `Velomax`.`Approvisionnement` (`siret`, `idPiece`, `nbPieceLivraison`,`prixA`,`delaiA`) " +
                                           "VALUES('{0}', '{1}', '{2}', '{3}', '{4}')", this.siret, this.approv[i].idPiece, this.nbPieces[i], this.prixA[i], this.delaiA[i]);
                    Program.Execute(scriptApprov);
                }
            }
        }
        public void delFournisseur()
        {
            if (this != null)
            {
                string script = string.Format("DELETE FROM Fournisseur WHERE siret = '{0}'", this.siret);
                Program.Execute(script);
            }
            if (this != null && this.approv != null)
            {
                string scriptApprov = string.Empty;
                for (int i = 0; i < this.approv.Count; i++)
                {
                    scriptApprov = string.Format("DELETE FROM Approvisionnement WHERE siret = '{0}' and idPiece= '{1}'", this.siret, this.approv[i].idPiece);
                    Program.Execute(scriptApprov);
                }
            }
        }
        public void editFournisseur()
        {
            if (this != null)
            {
                string script = string.Format("UPDATE Fournisseur " +
                                       "SET nomEntreprise = '{0}', idAdresse = '{1}', codePostF = '{2}', nomRueF = '{3}', numRueF = '{4}', contact = '{5}', qualite = '{6}' " +
                                       "WHERE siret = '{7}'", this.nomEntreprise, this.idAdresse, this.codePostF, this.nomRueF, this.numRueF, this.contact, this.qualite, this.siret);
                Program.Execute(script);
            }
            if (this != null && this.approv != null)
            {
                string scriptApprov = string.Empty;
                for (int i = 0; i < this.approv.Count; i++)
                {
                    scriptApprov = string.Format("UPDATE Approvisionnement " +
                                                "SET nbPieceLivraison = '{0}', prixA = '{1}', delaiA = '{2}' " +
                                                "WHERE siret = '{3}' and idPiece = '{4}'", this.nbPieces[i], this.prixA[i], this.delaiA[i], this.siret, this.approv[i].idPiece);
                    Program.Execute(scriptApprov);
                }
            }
        }
        public Tuple<List<string>, List<int>> stock()
        {
            ArrayList arr = Program.Requete(string.Format("select idPiece,nbPieceLivraison from Approvisionnement where siret = '{0}'", this.siret));
            List<string> id = new List<string>();
            List<int> nb = new List<int>();
            string[] temp = new string[2];

            for (int i = 0; i < arr.Count; i++)
            {
                temp = arr[i].ToString().Split(',');
                id.Add(temp[0]);
                nb.Add(Convert.ToInt32(temp[1]));
            }
            return new Tuple<List<string>, List<int>>(id, nb);
        }
    }
}
