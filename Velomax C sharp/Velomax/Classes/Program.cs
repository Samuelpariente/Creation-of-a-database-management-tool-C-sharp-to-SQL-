using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;
using System.Collections;
using MathNet.Numerics.Statistics;


namespace Velomax
{
    internal class Program
    {
        public static ArrayList Requete(string instruction)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=root;PASSWORD=77:$KyDx&#uBD?C;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = instruction;
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            ArrayList res;
            res = Extract(reader);
            command.Dispose();
            connection.Close();
            return res;
        }
        public static void Execute(string instruction)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=root;PASSWORD=77:$KyDx&#uBD?C;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = instruction;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
                return;
            }

            command.Dispose();
            connection.Close();
        }
        public static ArrayList Extract(MySqlDataReader reader)
        {
            ArrayList res = new ArrayList();
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + ", ";
                }
                res.Add(currentRowAsString);
            }
            return res;
        }
        static void Demo()
        {
            Console.WriteLine("Bienvenu dans la Demo choisissez une option \n 1.Overview \n 2.Edit de la DataBase \n 3.Export de la Database \n 4.Quitter");
            string choix = "0";
            Stat test = new Stat();
            while (choix != "4")
            {
                Console.Clear();
                Console.WriteLine("Bienvenu dans la Demo choisissez une option \n 1.Overview \n 2.Edit de la DataBase \n 3.Export de la Database \n 4.Quitter");
                choix = Console.ReadLine();
                if (choix == "1")
                {
                    Console.Clear();
                    Console.WriteLine("Le nombre de client est: ");
                    int nb = test.NombredeClient();
                    Console.WriteLine(nb);
                    Console.ReadKey();
                    Console.Clear();


                    Console.WriteLine("Leur participation au CA: ");
                    Tuple<List<string>, List<string>, List<double>> table = test.MVP();
                    Console.WriteLine("Nom | Prenom | CA");
                    for (int i = 0; i < table.Item1.Count; i++)
                    {
                        Console.Write(table.Item1[i].ToString());
                        Console.Write("|");
                        Console.Write(table.Item2[i].ToString());
                        Console.Write("|");
                        Console.Write(table.Item3[i].ToString());
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.Clear();

                    Console.WriteLine("les Produits avec un faible stock sont:");
                    Console.WriteLine("idPiece | Quantie");
                    Tuple<List<string>, List<int>> table2 = test.Faiblestock();
                    if (table2.Item1.Count > 0)
                        for (int i = 0; i < table2.Item1.Count; i++)
                        {
                            Console.Write(table2.Item1[i].ToString());
                            Console.Write("|");
                            Console.Write(table2.Item2[i].ToString());
                            Console.WriteLine();
                        }
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.Clear();

                    Console.WriteLine("Quantite par fournisseur: ");
                    Console.WriteLine("Sieret | idPiece | quantite");
                    Tuple<List<string>, List<string>, List<int>> table3 = test.Fournisseur();
                    for (int i = 0; i < table3.Item1.Count; i++)
                    {
                        Console.Write(table3.Item1[i].ToString());
                        Console.Write("|");
                        Console.Write(table3.Item2[i].ToString());
                        Console.Write("|");
                        Console.Write(table3.Item3[i].ToString());
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    Console.ReadKey();

                }
                if (choix == "2")
                {
                    Console.Clear();
                    Console.WriteLine("1.ClientB");
                    Console.WriteLine("2.ClientP");
                    Console.WriteLine("3.Commande");
                    Console.WriteLine("4.Fournisseur");
                    Console.WriteLine("5.Piece");
                    Console.WriteLine("6.ProgrammeFidelite");
                    Console.WriteLine("7.Remise");
                    Console.WriteLine("8.Velo");
                    string choix2 = Console.ReadLine();
                    if (choix2 == "1")
                    {
                        Console.Clear();
                        Console.WriteLine("ClientB :");
                        Console.WriteLine("1.Ajouter");
                        Console.WriteLine("2.edit");
                        Console.WriteLine("3.supprimer");
                        string choix3 = Console.ReadLine();
                        if (choix3 == "1")
                        {
                            Console.WriteLine("IdBoutique :");
                            string idB = Console.ReadLine();
                            Console.WriteLine("Id Remise :");
                            string idRemise = Console.ReadLine();
                            Console.WriteLine("Id adresse :");
                            string idAdresse = Console.ReadLine();
                            Console.WriteLine("Code Postal :");
                            string codePostal = Console.ReadLine();
                            Console.WriteLine("Nom rue :");
                            string nomRue = Console.ReadLine();
                            Console.WriteLine("numero de la rue :");
                            int numRue = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("telephone :");
                            string telephone = Console.ReadLine();
                            Console.WriteLine("Email :");
                            string email = Console.ReadLine();
                            Console.WriteLine("Nom du Contact :");
                            string nomcontact = Console.ReadLine();
                            ClientB client = new ClientB(idB, idRemise, idAdresse, codePostal, nomRue, numRue, telephone, email, nomcontact);
                            client.addClientB();
                        }
                        if (choix3 == "2")
                        {
                            Console.WriteLine("IdBoutique :");
                            string idbutique = Console.ReadLine();
                            ClientB client = new ClientB(idbutique);
                            client = client.LoadClienB();
                            Console.WriteLine("Que voulez vous edit ?");
                            Console.WriteLine("1.Id Remise");
                            Console.WriteLine("2.Id adresse");
                            Console.WriteLine("3.Code Postal");
                            Console.WriteLine("4.Nom rue");
                            Console.WriteLine("5.numero de la rue");
                            Console.WriteLine("6.telephone");
                            Console.WriteLine("7.Email");
                            Console.WriteLine("8.Nom du Contact");

                            string choix4 = Console.ReadLine();
                            if (choix4 == "1")
                            {

                                Console.WriteLine("Id Remise :");
                                string idRemise = Console.ReadLine();
                                client.idRemise = idRemise;
                            }
                            if (choix4 == "2")
                            {
                                Console.WriteLine("Id adresse :");
                                string idAdresse = Console.ReadLine();
                                client.idAdresse = idAdresse;
                            }
                            if (choix4 == "3")
                            {
                                Console.WriteLine("Code Postal :");
                                string codePostal = Console.ReadLine();
                                client.codePostB = codePostal;

                            }
                            if (choix4 == "4")
                            {
                                Console.WriteLine("Nom rue :");
                                string nomRue = Console.ReadLine();
                                client.nomRueB = nomRue;

                            }
                            if (choix4 == "5")
                            {
                                Console.WriteLine("numero de la rue :");
                                int numRue = Convert.ToInt32(Console.ReadLine());
                                client.numRueB = numRue;

                            }

                            if (choix4 == "6")
                            {
                                Console.WriteLine("telephone :");
                                string telephone = Console.ReadLine();
                                client.telephoneB = telephone;
                            }
                            if (choix4 == "7")
                            {
                                Console.WriteLine("Email :");
                                string email = Console.ReadLine();
                                client.emailB = email;
                            }
                            if (choix4 == "8")
                            {
                                Console.WriteLine("Nom du Contact :");
                                string nomcontact = Console.ReadLine();
                                client.nomContactB = nomcontact;
                            }
                            client.editClientB();
                        }
                        if (choix3 == "3")
                        {
                            Console.WriteLine("IdBoutique :");
                            string idB = Console.ReadLine();
                            ClientB client = new ClientB(idB);
                            client.delClientB();
                        }
                    }
                    if (choix2 == "2")
                    {
                        Console.Clear();
                        Console.WriteLine("ClientP :");
                        Console.WriteLine("1.Ajouter");
                        Console.WriteLine("2.edit");
                        Console.WriteLine("3.supprimer");
                        string choix3 = Console.ReadLine();
                        if (choix3 == "1")
                        {
                            Console.WriteLine("IdC :");
                            string idC = Console.ReadLine();
                            Console.WriteLine("Id Programme :");
                            string idProg = Console.ReadLine();
                            Console.WriteLine("nom :");
                            string nom = Console.ReadLine();
                            Console.WriteLine("Prenom :");
                            string prenom = Console.ReadLine();
                            Console.WriteLine("idadresse :");
                            string idadress = Console.ReadLine();
                            Console.WriteLine("code Postal :");
                            string codepostal = Console.ReadLine();
                            Console.WriteLine("nom rue :");
                            string nomrue = Console.ReadLine();
                            Console.WriteLine("Numero rue :");
                            int numrue = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Date adesion :");
                            DateTime dateadde = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("Date fin :");
                            DateTime datefin = Convert.ToDateTime(Console.ReadLine());
                            ClientP client = new ClientP(idC, idProg, nom, prenom, idadress, codepostal, nomrue, numrue, dateadde, datefin);
                            client.addClientP();
                        }
                        if (choix3 == "2")
                        {
                            Console.WriteLine("IdC :");
                            string idC = Console.ReadLine();
                            ClientP client = new ClientP(idC);
                            client = client.LoadClientP();
                            Console.WriteLine("Que voulez vous edit ?");
                            Console.WriteLine("1.Id Programme");
                            Console.WriteLine("2.nom");
                            Console.WriteLine("3.Prenom");
                            Console.WriteLine("4.idadresse");
                            Console.WriteLine("5.code Postal");
                            Console.WriteLine("6.nom rue");
                            Console.WriteLine("7.Numero rue");
                            Console.WriteLine("8.Date adesion");
                            Console.WriteLine("9.Date fin");

                            string choix4 = Console.ReadLine();
                            if (choix4 == "1")
                            {

                                Console.WriteLine("Id Programme :");
                                string idProg = Console.ReadLine();
                                client.idProg = idProg;

                            }
                            if (choix4 == "2")
                            {
                                Console.WriteLine("nom :");
                                string nom = Console.ReadLine();
                                client.nom = nom;

                            }
                            if (choix4 == "3")
                            {
                                Console.WriteLine("Prenom :");
                                string prenom = Console.ReadLine();
                                client.prenom = prenom;

                            }
                            if (choix4 == "4")
                            {
                                Console.WriteLine("idadresse :");
                                string idadress = Console.ReadLine();
                                client.idAdresse = idadress;


                            }
                            if (choix4 == "5")
                            {
                                Console.WriteLine("code Postal :");
                                string codepostal = Console.ReadLine();
                                client.codePostP = codepostal;


                            }

                            if (choix4 == "6")
                            {
                                Console.WriteLine("nom rue :");
                                string nomrue = Console.ReadLine();
                                client.nomRueP = nomrue;

                            }
                            if (choix4 == "7")
                            {
                                Console.WriteLine("Numero rue :");
                                int numrue = Convert.ToInt32(Console.ReadLine());
                                client.numRueP = numrue;

                            }
                            if (choix4 == "8")
                            {
                                Console.WriteLine("Date adesion :");
                                DateTime dateadde = Convert.ToDateTime(Console.ReadLine());
                                client.dateAdhesion = dateadde;

                            }
                            if (choix4 == "9")
                            {
                                Console.WriteLine("Date fin :");
                                DateTime datefin = Convert.ToDateTime(Console.ReadLine());
                                client.dateFinAdhesion = datefin;


                            }
                            client.editClientP();
                        }
                        if (choix3 == "3")
                        {
                            Console.WriteLine("IdC :");
                            string idC = Console.ReadLine();
                            ClientP client = new ClientP(idC);
                            client.delClientP();
                        }
                    }
                    if (choix2 == "3")
                    {
                        Console.Clear();
                        Console.WriteLine("Commande :");
                        Console.WriteLine("1.Ajouter");
                        Console.WriteLine("2.edit");
                        Console.WriteLine("3.supprimer");
                        string choix3 = Console.ReadLine();
                        if (choix3 == "1")
                        {
                            Console.WriteLine("IdCommande :");
                            string idCommande = Console.ReadLine();
                            Console.WriteLine("1.ClientP ou 2.boutique?");
                            string choix4 = Console.ReadLine();
                            string idC = "0";
                            string idB = "0";
                            if (choix4 == "1")
                            {
                                Console.WriteLine("IdC :");
                                idC = Console.ReadLine();
                            }
                            if (choix3 == "2")
                            {
                                Console.WriteLine("IdB :");
                                idB = Console.ReadLine();
                            }

                            Console.WriteLine("Date de commande:");
                            DateTime dateCom = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("Adresse livraison :");
                            string adresse = Console.ReadLine();
                            Console.WriteLine("Date de livraison:");
                            DateTime dateLivraison = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("Velo et quantité dans la commande, si fin tapper END");
                            Console.WriteLine("Id Velo");
                            string idvelo = Console.ReadLine();
                            Console.WriteLine("Quantite");
                            int Quantite = Convert.ToInt32(Console.ReadLine());

                            List<Velo> Velos = new List<Velo>();
                            List<int> Quantites = new List<int>();
                            while (idvelo != "END")
                            {
                                Velo vel = new Velo(idvelo);
                                Quantites.Add(Quantite);
                                Velos.Add(vel);
                                Console.WriteLine("Id Velo");
                                idvelo = Console.ReadLine();
                                if (idvelo != "END")
                                {
                                    Console.WriteLine("Quantite");
                                    Quantite = Convert.ToInt32(Console.ReadLine());
                                }
                            }

                            Console.WriteLine("Piece et quantité dans la commande, si fin tapper END");
                            Console.WriteLine("Id Piece");
                            string idpiece = Console.ReadLine();
                            Console.WriteLine("Quantite");
                            int QuantiteP = Convert.ToInt32(Console.ReadLine());

                            List<Piece> Pieces = new List<Piece>();
                            List<int> QuantitesP = new List<int>();
                            while (idpiece != "END")
                            {
                                Piece piece = new Piece(idpiece);
                                Pieces.Add(piece);
                                QuantitesP.Add(QuantiteP);
                                Console.WriteLine("Id Piece");
                                idpiece = Console.ReadLine();
                                if (idpiece != "END")
                                {
                                    Console.WriteLine("Quantite");
                                    QuantiteP = Convert.ToInt32(Console.ReadLine());
                                }
                            }

                            Commande commande = new Commande(idCommande, idC, idB, dateCom, adresse, dateLivraison);
                            commande.contenuCommande = Velos;
                            commande.nbVelos = Quantites;
                            commande.supplement = Pieces;
                            commande.nbPieces = QuantitesP;
                            commande.addCommande();
                        }
                        if (choix3 == "2")
                        {
                            Console.WriteLine("IdCommande :");
                            string idCommande = Console.ReadLine();
                            Commande commande = new Commande(idCommande);
                            commande = commande.Loadcommande();
                            Console.WriteLine("Que voulez vous edit ?");
                            Console.WriteLine("1.ClientP");
                            Console.WriteLine("2.boutique");
                            Console.WriteLine("3.Date de commande");
                            Console.WriteLine("4.Adresse livraison");
                            Console.WriteLine("5.Date de livraison");
                            Console.WriteLine("6.Velo/Quantite");
                            Console.WriteLine("7.Piece/Quantite ");

                            string choix4 = Console.ReadLine();
                            if (choix4 == "1")
                            {
                                Console.WriteLine("1.ClientP");
                                string idC = Console.ReadLine();
                                commande.idC = idC;

                            }
                            if (choix4 == "2")
                            {
                                Console.WriteLine("2.boutique");
                                string idP = Console.ReadLine();
                                commande.idB = idP;
                            }
                            if (choix4 == "3")
                            {
                                Console.WriteLine("Date de commande:");
                                DateTime dateCom = Convert.ToDateTime(Console.ReadLine());
                                commande.dateCom = dateCom;
                            }
                            if (choix4 == "4")
                            {
                                Console.WriteLine("Adresse livraison :");
                                string adresse = Console.ReadLine();
                                commande.adresseLivraison = adresse;
                            }
                            if (choix4 == "5")
                            {
                                Console.WriteLine("Date de livraison:");
                                DateTime dateCom = Convert.ToDateTime(Console.ReadLine());
                                commande.dateLivraison = dateCom;
                            }

                            if (choix4 == "6")
                            {
                                Console.WriteLine("Velo et quantité dans la commande, si fin tapper END");
                                Console.WriteLine("Id Velo");
                                string idvelo = Console.ReadLine();
                                Console.WriteLine("Quantite");
                                int Quantite = Convert.ToInt32(Console.ReadLine());

                                List<Velo> Velos = new List<Velo>();
                                List<int> Quantites = new List<int>();
                                while (idvelo != "END")
                                {
                                    Velo vel = new Velo(idvelo);
                                    Quantites.Add(Quantite);
                                    Velos.Add(vel);
                                    Console.WriteLine("Id Velo");
                                    idvelo = Console.ReadLine();
                                    if (idvelo != "END")
                                    {
                                        Console.WriteLine("Quantite");
                                        Quantite = Convert.ToInt32(Console.ReadLine());
                                    }
                                }
                                commande.contenuCommande = Velos;
                                commande.nbVelos = Quantites;
                            }

                            if (choix4 == "7")
                            {
                                Console.WriteLine("Piece et quantité dans la commande, si fin tapper END");
                                Console.WriteLine("Id Piece");
                                string idpiece = Console.ReadLine();
                                Console.WriteLine("Quantite");
                                int QuantiteP = Convert.ToInt32(Console.ReadLine());

                                List<Piece> Pieces = new List<Piece>();
                                List<int> QuantitesP = new List<int>();
                                while (idpiece != "END")
                                {
                                    Piece piece = new Piece(idpiece);
                                    Pieces.Add(piece);
                                    QuantitesP.Add(QuantiteP);
                                    Console.WriteLine("Id Piece");
                                    idpiece = Console.ReadLine();
                                    if (idpiece != "END")
                                    {
                                        Console.WriteLine("Quantite");
                                        QuantiteP = Convert.ToInt32(Console.ReadLine());
                                    }
                                }
                                commande.supplement = Pieces;
                                commande.nbPieces = QuantitesP;

                            }
                            commande.editCommande();
                        }
                        if (choix3 == "3")
                        {
                            Console.WriteLine("IdCommande :");
                            string idCommande = Console.ReadLine();
                            Commande commande = new Commande(idCommande);
                            commande.delCommande();
                        }
                    }
                    if (choix2 == "4")
                    {
                        Console.Clear();
                        Console.WriteLine("Fournisseur :");
                        Console.WriteLine("1.Ajouter");
                        Console.WriteLine("2.edit");
                        Console.WriteLine("3.supprimer");
                        string choix3 = Console.ReadLine();
                        if (choix3 == "1")
                        {
                            Console.WriteLine("Siret :");
                            string siret = Console.ReadLine();
                            Console.WriteLine("Nom de l'entreprise :");
                            string nomentreprise = Console.ReadLine();
                            Console.WriteLine("idAdresse :");
                            string idAdresse = Console.ReadLine();
                            Console.WriteLine("CodePostalF :");
                            string CodePostalF = Console.ReadLine();
                            Console.WriteLine("Nom de la Rue :");
                            string nomrue = Console.ReadLine();
                            Console.WriteLine("Num de la rue :");
                            int numrue = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Contact :");
                            string contact = Console.ReadLine();
                            Console.WriteLine("Qualite :");
                            int qualite = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Piece,Nombre de Piece,Delai,Prix par le fournisseur, si fin tapper END");
                            Console.WriteLine("Piece");
                            string idpiece = Console.ReadLine();
                            Console.WriteLine("Nombre de Piece");
                            int Quantite = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Delais");
                            int Delais = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Prix");
                            int Prix = Convert.ToInt32(Console.ReadLine());

                            List<Piece> Pieces = new List<Piece>();
                            List<int> Quantites = new List<int>();
                            List<int> Delaiss = new List<int>();
                            List<int> QPrixs = new List<int>();
                            while (idpiece != "END")
                            {
                                Piece piece = new Piece(idpiece);
                                Quantites.Add(Quantite);
                                Pieces.Add(piece);
                                Delaiss.Add(Delais);
                                QPrixs.Add(Prix);
                                Console.WriteLine("Id Piece");
                                idpiece = Console.ReadLine();
                                if (idpiece != "END")
                                {
                                    Console.WriteLine("Quantite");
                                    Quantite = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Delais");
                                    Delais = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Prix");
                                    Prix = Convert.ToInt32(Console.ReadLine());
                                }
                            }



                            Fournisseur fournisseur = new Fournisseur(siret, nomentreprise, idAdresse, CodePostalF, nomrue, numrue, contact, qualite);
                            fournisseur.approv = Pieces;
                            fournisseur.nbPieces = Quantites;
                            fournisseur.delaiA = Delaiss;
                            fournisseur.prixA = QPrixs;
                            fournisseur.addFournisseur();
                        }
                        if (choix3 == "2")
                        {
                            Console.WriteLine("Siret :");
                            string Siret = Console.ReadLine();
                            Fournisseur f = new Fournisseur(Siret);
                            f = f.LoadFournisseur();
                            Console.WriteLine("Que voulez vous edit ?");
                            Console.WriteLine("1.Nom de l'entreprise");
                            Console.WriteLine("2.idAdresse");
                            Console.WriteLine("3.CodePostalF");
                            Console.WriteLine("4.Nom de la Rue");
                            Console.WriteLine("5.Num de la rue");
                            Console.WriteLine("6.Contact");
                            Console.WriteLine("7.Qualite");
                            Console.WriteLine("8.Piece,Nombre de Piece,Delai,Prix");

                            string choix4 = Console.ReadLine();
                            if (choix4 == "1")
                            {
                                Console.WriteLine("Nom de l'entreprise :");
                                string nomentreprise = Console.ReadLine();
                                f.nomEntreprise = nomentreprise;


                            }
                            if (choix4 == "2")
                            {
                                Console.WriteLine("idAdresse :");
                                string idAdresse = Console.ReadLine();
                                f.idAdresse = idAdresse;


                            }
                            if (choix4 == "3")
                            {
                                Console.WriteLine("CodePostalF :");
                                string CodePostalF = Console.ReadLine();
                                f.codePostF = CodePostalF;



                            }
                            if (choix4 == "4")
                            {
                                Console.WriteLine("Nom de la Rue :");
                                string nomrue = Console.ReadLine();
                                f.nomRueF = nomrue;

                            }
                            if (choix4 == "5")
                            {
                                Console.WriteLine("Num de la rue :");
                                int numrue = Convert.ToInt32(Console.ReadLine());
                                f.numRueF = numrue;


                            }

                            if (choix4 == "6")
                            {
                                Console.WriteLine("Contact :");
                                string contact = Console.ReadLine();
                                f.contact = contact;

                            }
                            if (choix4 == "7")
                            {
                                Console.WriteLine("Qualite :");
                                int qualite = Convert.ToInt32(Console.ReadLine());
                                f.qualite = qualite;


                            }
                            if (choix4 == "8")
                            {
                                Console.WriteLine("Piece,Nombre de Piece,Delai,Prix par le fournisseur, si fin tapper END");
                                Console.WriteLine("Piece");
                                string idpiece = Console.ReadLine();
                                Console.WriteLine("Nombre de Piece");
                                int Quantite = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Delais");
                                int Delais = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Prix");
                                int Prix = Convert.ToInt32(Console.ReadLine());

                                List<Piece> Pieces = new List<Piece>();
                                List<int> Quantites = new List<int>();
                                List<int> Delaiss = new List<int>();
                                List<int> QPrixs = new List<int>();
                                while (idpiece != "END")
                                {
                                    Piece piece = new Piece(idpiece);
                                    Quantites.Add(Quantite);
                                    Pieces.Add(piece);
                                    Delaiss.Add(Delais);
                                    QPrixs.Add(Prix);
                                    Console.WriteLine("Id Piece");
                                    idpiece = Console.ReadLine();
                                    if (idpiece != "END")
                                    {
                                        Console.WriteLine("Quantite");
                                        Quantite = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Delais");
                                        Delais = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Prix");
                                        Prix = Convert.ToInt32(Console.ReadLine());
                                    }
                                }
                                f.approv = Pieces;
                                f.nbPieces = Quantites;
                                f.delaiA = Delaiss;
                                f.prixA = QPrixs;
                            }
                            f.editFournisseur();
                        }
                        if (choix3 == "3")
                        {
                            Console.WriteLine("Siret :");
                            string siret = Console.ReadLine();
                            Fournisseur fournisseur = new Fournisseur(siret);
                            fournisseur.delFournisseur();
                        }
                    }
                    if (choix2 == "5")
                    {
                        Console.Clear();
                        Console.WriteLine("Piece :");
                        Console.WriteLine("1.Ajouter");
                        Console.WriteLine("2.edit");
                        Console.WriteLine("3.supprimer");
                        string choix3 = Console.ReadLine();
                        if (choix3 == "1")
                        {
                            Console.WriteLine("IdPiece :");
                            string idPiece = Console.ReadLine();
                            Console.WriteLine("Description :");
                            string Description = Console.ReadLine();
                            Console.WriteLine("numCatalogue:");
                            string numcatalogue = Console.ReadLine();
                            Console.WriteLine("Prix unit piece :");
                            float Prix = float.Parse(Console.ReadLine());
                            Console.WriteLine("Date de mise sur le marche :");
                            DateTime dateImPiece = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("date suppression du marché :");
                            DateTime datedpPiece = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("quantite stock :");
                            int quantitestock = Convert.ToInt32(Console.ReadLine());


                            Piece piece = new Piece(idPiece, Description, numcatalogue, Prix, quantitestock, dateImPiece, datedpPiece);
                            piece.addPiece();
                        }
                        if (choix3 == "2")
                        {
                            Console.WriteLine("IdPiece :");
                            string idpiece = Console.ReadLine();
                            Piece piece = new Piece(idpiece);
                            piece = piece.loadPiece();
                            Console.WriteLine("Que voulez vous edit ?");
                            Console.WriteLine("1.Description ");
                            Console.WriteLine("2.numCatalogue:");
                            Console.WriteLine("3.Prix unit piece :");
                            Console.WriteLine("4.Date de mise sur le marche :");
                            Console.WriteLine("5.date suppression du marché :");
                            Console.WriteLine("6.quantite stock :");
                            string choix4 = Console.ReadLine();
                            if (choix4 == "1")
                            {
                                Console.WriteLine("Description :");
                                string Description = Console.ReadLine();

                                piece.descriptionP = Description;
                            }
                            if (choix4 == "2")
                            {
                                Console.WriteLine("numCatalogue:");
                                string numcatalogue = Console.ReadLine();
                                piece.numCatalogue = numcatalogue;
                            }
                            if (choix4 == "3")
                            {
                                Console.WriteLine("Prix unit piece :");
                                float Prix = float.Parse(Console.ReadLine());
                                piece.prixUnitPiece = Prix;

                            }
                            if (choix4 == "4")
                            {
                                Console.WriteLine("Date de mise sur le marche :");
                                DateTime dateImPiece = Convert.ToDateTime(Console.ReadLine());
                                piece.dateImPiece = dateImPiece;

                            }
                            if (choix4 == "5")
                            {
                                Console.WriteLine("date suppression du marché :");
                                DateTime datedpPiece = Convert.ToDateTime(Console.ReadLine());
                                piece.dateDpPiece = datedpPiece;

                            }
                            if (choix4 == "6")
                            {
                                Console.WriteLine("quantite stock :");
                                int quantitestock = Convert.ToInt32(Console.ReadLine());
                                piece.quantiteStock = quantitestock;
                            }
                            piece.editPiece();
                        }
                        if (choix3 == "3")
                        {
                            Console.WriteLine("IdPiece :");
                            string idpiece = Console.ReadLine();
                            Piece piece = new Piece(idpiece);
                            piece.delPiece();
                        }
                    }
                    if (choix2 == "6")
                    {
                        Console.Clear();
                        Console.WriteLine("Programme Fidelité :");
                        Console.WriteLine("1.Ajouter");
                        Console.WriteLine("2.edit");
                        Console.WriteLine("3.supprimer");
                        string choix3 = Console.ReadLine();
                        if (choix3 == "1")
                        {
                            Console.WriteLine("IdProgramme :");
                            string idProg = Console.ReadLine();
                            Console.WriteLine("TypeProgramme :");
                            int typeprog = Convert.ToInt32(Console.ReadLine());
                            ProgrammeFidelite prog = new ProgrammeFidelite(idProg, typeprog);
                            prog.addProgrammeFidelite();


                        }
                        if (choix3 == "2")
                        {
                            Console.WriteLine("IdProgramme :");
                            string idProg = Console.ReadLine();
                            ProgrammeFidelite prog = new ProgrammeFidelite(idProg, 0);
                            prog.LoadProgramme();
                            Console.WriteLine("Que voulez vous edit ?");
                            Console.WriteLine("1.TypeProgramme");


                            string choix4 = Console.ReadLine();
                            if (choix4 == "1")
                            {
                                Console.WriteLine("TypeProgramme :");
                                int typeprog = Convert.ToInt32(Console.ReadLine());
                                prog.typeProg = typeprog;
                            }
                            prog.editProgrammeFidelite();
                        }
                        if (choix3 == "3")
                        {
                            Console.WriteLine("Id Programme:");
                            string idprog = Console.ReadLine();
                            ProgrammeFidelite prg = new ProgrammeFidelite(idprog, 0);
                            prg.delProgrammeFidelite();
                        }
                    }
                    if (choix2 == "7")
                    {
                        Console.Clear();
                        Console.WriteLine("Remise Boutique :");
                        Console.WriteLine("1.Ajouter");
                        Console.WriteLine("2.edit");
                        Console.WriteLine("3.supprimer");
                        string choix3 = Console.ReadLine();
                        if (choix3 == "1")
                        {
                            Console.WriteLine("IdRemise :");
                            string idRem = Console.ReadLine();
                            Console.WriteLine("Pourcentage :");
                            int pourcentage = Convert.ToInt32(Console.ReadLine());
                            RemiseBoutique rem = new RemiseBoutique(idRem, pourcentage);
                            rem.addRemiseBoutique();

                        }
                        if (choix3 == "2")
                        {
                            Console.WriteLine("IdRemise :");
                            string idRem = Console.ReadLine();
                            RemiseBoutique rem = new RemiseBoutique(idRem, 0);
                            rem.LoadRemise();
                            Console.WriteLine("Que voulez vous edit ?");
                            Console.WriteLine("1.Pourcentage");

                            string choix4 = Console.ReadLine();
                            if (choix4 == "1")
                            {
                                Console.WriteLine("Pourcentage :");
                                int pourcentage = Convert.ToInt32(Console.ReadLine());
                                rem.pourcentage = pourcentage;
                            }
                            rem.editRemiseBoutique();
                        }
                        if (choix3 == "3")
                        {
                            Console.WriteLine("Id Remise:");
                            string idrem = Console.ReadLine();
                            RemiseBoutique rem = new RemiseBoutique(idrem, 0);
                            rem.delRemiseBoutique();
                        }

                    }
                    if (choix2 == "8")
                    {
                        Console.Clear();
                        Console.WriteLine("Velo :");
                        Console.WriteLine("1.Ajouter");
                        Console.WriteLine("2.edit");
                        Console.WriteLine("3.supprimer");
                        string choix3 = Console.ReadLine();
                        if (choix3 == "1")
                        {
                            Console.WriteLine("Id velo :");
                            string idvelo = Console.ReadLine();
                            Console.WriteLine("Nom velo :");
                            string nomVelo = Console.ReadLine();
                            Console.WriteLine("Grandeur :");
                            string grandeur = Console.ReadLine();
                            Console.WriteLine("Prix unit velo :");
                            float PrixVelo = float.Parse(Console.ReadLine());
                            Console.WriteLine("ligne Proguit :");
                            string LigneProguit = Console.ReadLine();
                            Console.WriteLine("Stock velo :");
                            int StockVelo = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("DateImvelo :");
                            DateTime DateImVelo = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("DateDpvelo :");
                            DateTime DateDpvelo = Convert.ToDateTime(Console.ReadLine());


                            Console.WriteLine("Piece,Nombre de Piece, si fin tapper END");
                            Console.WriteLine("Piece");
                            string idpiece = Console.ReadLine();
                            Console.WriteLine("Nombre de Piece");
                            int Quantite = Convert.ToInt32(Console.ReadLine());


                            List<Piece> Pieces = new List<Piece>();
                            List<int> Quantites = new List<int>();

                            while (idpiece != "END")
                            {
                                Piece piece = new Piece(idpiece);
                                Quantites.Add(Quantite);
                                Pieces.Add(piece);

                                Console.WriteLine("Id Piece");
                                idpiece = Console.ReadLine();
                                if (idpiece != "END")
                                {
                                    Console.WriteLine("Quantite");
                                    Quantite = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Delais");

                                }
                            }



                            Velo velo = new Velo(idvelo, nomVelo, grandeur, PrixVelo, LigneProguit, StockVelo, DateImVelo, DateDpvelo);
                            velo.compo = Pieces;
                            velo.nbPieces = Quantites;
                            velo.addVelo();
                        }
                        if (choix3 == "2")
                        {
                            Console.WriteLine("Id velo :");
                            string idvelo = Console.ReadLine();
                            Velo v = new Velo(idvelo);
                            v.LoadVelo();
                            Console.WriteLine("1.Nom velo :");
                            Console.WriteLine("2.Grandeur :");
                            Console.WriteLine("3.Prix unit velo :");
                            Console.WriteLine("4.ligne Proguit :");
                            Console.WriteLine("5.Stock velo :");
                            Console.WriteLine("6.DateImvelo :");
                            Console.WriteLine("7.DateDpvelo :");
                            Console.WriteLine("8.Piece,Nombre de Piece");

                            string choix4 = Console.ReadLine();
                            if (choix4 == "1")
                            {
                                Console.WriteLine("Nom velo :");
                                string nomVelo = Console.ReadLine();
                                v.nomV = nomVelo;

                            }
                            if (choix4 == "2")
                            {
                                Console.WriteLine("Grandeur :");
                                string grandeur = Console.ReadLine();
                                v.grandeur = grandeur;

                            }
                            if (choix4 == "3")
                            {
                                Console.WriteLine("Prix unit velo :");
                                float PrixVelo = float.Parse(Console.ReadLine());
                                v.prixUnitVelo = PrixVelo;

                            }
                            if (choix4 == "4")
                            {
                                Console.WriteLine("ligne Proguit :");
                                string LigneProguit = Console.ReadLine();
                                v.ligneProduit = LigneProguit;

                            }
                            if (choix4 == "5")
                            {
                                Console.WriteLine("Stock velo :");
                                int StockVelo = Convert.ToInt32(Console.ReadLine());
                                v.stockVelo = StockVelo;


                            }

                            if (choix4 == "6")
                            {
                                Console.WriteLine("DateImvelo :");
                                DateTime DateImVelo = Convert.ToDateTime(Console.ReadLine());
                                v.dateImVelo = DateImVelo;

                            }
                            if (choix4 == "7")
                            {
                                Console.WriteLine("DateDpvelo :");
                                DateTime DateDpvelo = Convert.ToDateTime(Console.ReadLine());
                                v.dateDpVelo = DateDpvelo;
                            }
                            if (choix4 == "8")
                            {

                                Console.WriteLine("Piece,Nombre de Piece, si fin tapper END");
                                Console.WriteLine("Piece");
                                string idpiece = Console.ReadLine();
                                Console.WriteLine("Nombre de Piece");
                                int Quantite = Convert.ToInt32(Console.ReadLine());

                                List<Piece> Pieces = new List<Piece>();
                                List<int> Quantites = new List<int>();

                                while (idpiece != "END")
                                {
                                    Piece piece = new Piece(idpiece);
                                    Quantites.Add(Quantite);
                                    Pieces.Add(piece);

                                    Console.WriteLine("Id Piece");
                                    idpiece = Console.ReadLine();
                                    if (idpiece != "END")
                                    {
                                        Console.WriteLine("Quantite");
                                        Quantite = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Delais");

                                    }
                                }
                                v.compo = Pieces;
                                v.nbPieces = Quantites;

                            }
                            v.editVelo();
                        }
                        if (choix3 == "3")
                        {
                            Console.WriteLine("idVelo :");
                            string idvelo = Console.ReadLine();
                            Velo velo = new Velo(idvelo);
                            velo.delVelo();
                        }
                    }
                }
                if (choix == "3")
                {
                    Piece.ToJson();
                    ClientB.ToJson();
                    ClientP.ToJson();
                    ProgrammeFidelite.ToJson();
                    RemiseBoutique.ToJson();
                    Commande.ToJson();
                    Fournisseur.ToJson();
                    Velo.ToJson();
                    Stat.faiblestockXML();
                }

            }
        }
        static void Main(string[] args)
        {
            Demo();
        }
    }
}
