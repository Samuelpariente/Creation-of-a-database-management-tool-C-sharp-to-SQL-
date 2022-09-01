DROP DATABASE IF EXISTS Velomax;
CREATE DATABASE IF NOT EXISTS Velomax;
USE Velomax;

CREATE TABLE IF NOT EXISTS Velo
(
	idVelo VARCHAR(40) PRIMARY KEY NOT NULL, 
    nomV VARCHAR(40),
    grandeur VARCHAR(40),
    prixUnitVelo DECIMAL(10,2),
    ligneProduit VARCHAR(40),
    dateImVelo DATE,
    dateDpVelo DATE,
    stockVelo INT
);

CREATE TABLE IF NOT EXISTS Province
(
	nomProvince VARCHAR(40) PRIMARY KEY NOT NULL
);

CREATE TABLE IF NOT EXISTS Ville
(
	nomVille VARCHAR(40) PRIMARY KEY NOT NULL
);

CREATE TABLE IF NOT EXISTS CodePostal
(
	codePost VARCHAR(40) PRIMARY KEY NOT NULL,
    nomVille VARCHAR(40), FOREIGN KEY (nomVille) REFERENCES Ville(nomVille),
    nomProvince VARCHAR(40), FOREIGN KEY (nomProvince) REFERENCES Province(nomProvince)
);

CREATE TABLE IF NOT EXISTS Fournisseur
(
	siret VARCHAR(40) PRIMARY KEY NOT NULL, 
    nomEntreprise VARCHAR(40),
    idAdresse VARCHAR(40),
    codePostF VARCHAR(40), FOREIGN KEY (codePostF) REFERENCES CodePostal(codePost),
    nomRueF VARCHAR(40),
    numRueF INT,
    contact VARCHAR(40),
    qualite ENUM('1', '2', '3', '4')
);

CREATE TABLE IF NOT EXISTS Piece
(
	idPiece VARCHAR(40) PRIMARY KEY NOT NULL, 
    descriptionP VARCHAR(150),
    /*nomFournisseur VARCHAR(40),*/ /*pas sur il faut peut etre enlever*/ /*New*/
    numCatalogue VARCHAR(40), 
    prixUnitPiece DECIMAL(10,2),
    quantiteStock INT,
    dateImPiece DATE, 
    dateDpPiece DATE
    /*delaiApprov int*/ /*en jours*/ /*New*/
);

CREATE TABLE IF NOT EXISTS ProgrammeFidelite
(
	idProgramme VARCHAR(40) PRIMARY KEY NOT NULL, 
    typeProg INT
    /*dateAdhesion DATE*/ /*NEW*/
);

CREATE TABLE IF NOT EXISTS ClientP
(
	idC VARCHAR(40) PRIMARY KEY NOT NULL, 
    idProg VARCHAR(40), FOREIGN KEY (idProg) REFERENCES ProgrammeFidelite (idProgramme),
    nom VARCHAR(40), 
    prenom VARCHAR(40),
    idAdresse VARCHAR(40),
    codePostP VARCHAR(40), FOREIGN KEY (codePostP) REFERENCES CodePostal(codePost),
    nomRueP VARCHAR(40),
    numRueP INT, 
    dateAdhesion DATE, 
    dateFinAdhesion DATE
);

CREATE TABLE IF NOT EXISTS RemiseBoutique
(
	idRemise VARCHAR(40) PRIMARY KEY NOT NULL, 
    pourcentage INT
);

CREATE TABLE IF NOT EXISTS ClientB
(
	idB VARCHAR(40) PRIMARY KEY NOT NULL, 
    idRemise VARCHAR(40), FOREIGN KEY (idRemise) REFERENCES RemiseBoutique (idRemise),
    idAdresse VARCHAR(40),
    codePostB VARCHAR(40), FOREIGN KEY (codePostB) REFERENCES CodePostal(codePost),
    nomRueB VARCHAR(40),
    numRueB INT,
    telephoneB VARCHAR(40),
    emailB VARCHAR(40),
    nomContactB VARCHAR(40)
);

CREATE TABLE IF NOT EXISTS Commande
(
	idCommande VARCHAR(40) PRIMARY KEY NOT NULL,
    idC VARCHAR(40), FOREIGN KEY (idC) REFERENCES ClientP(idC) ON DELETE CASCADE ON UPDATE NO ACTION,
    idB VARCHAR(40), FOREIGN KEY (idB) REFERENCES ClientB(idB) ON DELETE CASCADE ON UPDATE NO ACTION,
    dateCom DATE,
    adresseLivraison VARCHAR(50), 
    dateLivraison DATE
);

CREATE TABLE IF NOT EXISTS ContenuCommande
(
	idCommande VARCHAR(40), FOREIGN KEY (idCommande) REFERENCES Commande(idCommande) ON DELETE CASCADE ON UPDATE NO ACTION,
    idVelo VARCHAR(40), FOREIGN KEY (idVelo) REFERENCES Velo(idVelo) ON DELETE CASCADE ON UPDATE NO ACTION,
    PRIMARY KEY (idCommande, idVelo), 
    nbItemVelo INT
);

CREATE TABLE IF NOT EXISTS CompositionVelo
(
	idPiece VARCHAR(40), FOREIGN KEY (idPiece) REFERENCES Piece(idPiece) ON DELETE CASCADE ON UPDATE NO ACTION,
    idVelo VARCHAR(40), FOREIGN KEY (idVelo) REFERENCES Velo(idVelo) ON DELETE CASCADE ON UPDATE NO ACTION,
    PRIMARY KEY (idPiece, idVelo),
    nbPieceVelo INT
);

CREATE TABLE IF NOT EXISTS Approvisionnement
(
	siret VARCHAR(40), FOREIGN KEY (siret) REFERENCES Fournisseur(siret) ON DELETE CASCADE ON UPDATE NO ACTION,
    idPiece VARCHAR(40), FOREIGN KEY (idPiece) REFERENCES Piece(idPiece) ON DELETE CASCADE ON UPDATE NO ACTION,
    PRIMARY KEY (siret, idPiece),
    nbPieceLivraison INT,
    prixA int, /*NEW*/
    delaiA int /*NEW*/
);

CREATE TABLE IF NOT EXISTS Supplement
(
	idPiece VARCHAR(40), FOREIGN KEY (idPiece) REFERENCES Piece(idPiece) ON DELETE CASCADE ON UPDATE NO ACTION,
    idCommande VARCHAR(40), FOREIGN KEY (idCommande) REFERENCES Commande(idCommande) ON DELETE CASCADE ON UPDATE NO ACTION,
    PRIMARY KEY (idPiece, idCommande),
    nbItemPiece INT
);


/*----------------------------Defaults----------------------------*/
insert into velomax.ville(nomVille) values("defaultVille");
insert into velomax.province(nomProvince) values("defaultProvince");
insert into velomax.codepostal(codePost, nomVille, nomProvince) values("00000", "defaultVille", "defaultProvince");
insert into velomax.ProgrammeFidelite(idProgramme, typeProg) values("0", 0);
insert into velomax.RemiseBoutique(idRemise, pourcentage) values("0", 0);
insert into velomax.ClientP(idC, idProg, nom, prenom, idAdresse, codePostP, nomRueP, numRueP, dateAdhesion, dateFinAdhesion) values("0", "0", "defaultNom", "defaultPrenom", "0", "00000", "NomRueDefault", 0, date("0001-01-01"), date("0001-01-01"));
insert into velomax.ClientB(idB, idRemise, idAdresse, codePostB, nomRueB, numRueB, telephoneB, emailB, nomContactB) values("0", "0", "0", "00000", "NomRueDefault", 0, "defaultTelephone", "defaultEmail", "defaultContact");

/*----------------------------Peuplement----------------------------*/

/*Ville*/
insert into velomax.ville(nomVille) values ("Paris");
insert into velomax.ville(nomVille) values ("Marseille");
insert into velomax.ville(nomVille) values ("Lyon");
insert into velomax.ville(nomVille) values ("Toulouse");
insert into velomax.ville(nomVille) values ("Perpignan");

/*Province*/
insert into velomax.province(nomProvince) values("Ile de France");
insert into velomax.province(nomProvince) values("Rhone Alpes");
insert into velomax.province(nomProvince) values("Bouches du Rhone");
insert into velomax.province(nomProvince) values("Occitanie");
insert into velomax.province(nomProvince) values("Midi Pyrénées");

/*Code Postal*/
insert into velomax.codepostal(codePost, nomVille, nomProvince) values("75001", "Paris", "Ile de France");
insert into velomax.codepostal(codePost, nomVille, nomProvince) values("75002", "Paris", "Ile de France");
insert into velomax.codepostal(codePost, nomVille, nomProvince) values("75003", "Paris", "Ile de France");
insert into velomax.codepostal(codePost, nomVille, nomProvince) values("75004", "Paris", "Ile de France");
insert into velomax.codepostal(codePost, nomVille, nomProvince) values("75005", "Paris", "Ile de France");
insert into velomax.codepostal(codePost, nomVille, nomProvince) values("75006", "Paris", "Ile de France");
insert into velomax.codepostal(codePost, nomVille, nomProvince) values("75007", "Paris", "Ile de France");
insert into velomax.codepostal(codePost, nomVille, nomProvince) values("75008", "Paris", "Ile de France");
insert into velomax.codepostal(codePost, nomVille, nomProvince) values("75009", "Paris", "Ile de France");
insert into velomax.codepostal(codePost, nomVille, nomProvince) values("75010", "Paris", "Ile de France");
insert into velomax.codepostal(codePost, nomVille, nomProvince) values("75011", "Paris", "Ile de France");
insert into velomax.codepostal(codePost, nomVille, nomProvince) values("75016", "Paris", "Ile de France");
insert into velomax.codepostal(codePost, nomVille, nomProvince) values("69006", "Lyon", "Rhone Alpes");
insert into velomax.codepostal(codePost, nomVille, nomProvince) values("13011", "Marseille", "Bouches du Rhone");
insert into velomax.codepostal(codePost, nomVille, nomProvince) values("66100", "Perpignan", "Midi Pyrénées");
insert into velomax.codepostal(codePost, nomVille, nomProvince) values("31004", "Toulouse", "Occitanie");


/*Programme Fidelite*/
insert into velomax.ProgrammeFidelite(idProgramme, typeProg) values("f1", 1);
insert into velomax.ProgrammeFidelite(idProgramme, typeProg) values("f2", 2);
insert into velomax.ProgrammeFidelite(idProgramme, typeProg) values("f3", 3);

/*Remise Boutique*/
insert into velomax.RemiseBoutique(idRemise, pourcentage) values("r1", 5);
insert into velomax.RemiseBoutique(idRemise, pourcentage) values("r2", 10);
insert into velomax.RemiseBoutique(idRemise, pourcentage) values("r3", 15);
insert into velomax.RemiseBoutique(idRemise, pourcentage) values("r4", 20);
insert into velomax.RemiseBoutique(idRemise, pourcentage) values("r5", 25);

/*Client Particulier*/
insert into velomax.ClientP(idC, idProg, nom, prenom, idAdresse, codePostP, nomRueP, numRueP, dateAdhesion, dateFinAdhesion) values("c1", "f1", "Ortega", "Marius", "1", "75016", "Rue de la rue", 115, date("2022-04-20"), date("2022-04-20"));
insert into velomax.ClientP(idC, idProg, nom, prenom, idAdresse, codePostP, nomRueP, numRueP, dateAdhesion, dateFinAdhesion) values("c2", "f2", "Pariente", "Samuel", "2", "69006", "Rue du boulevard", 1, date("2022-04-26"), date("2022-04-26"));
insert into velomax.ClientP(idC, idProg, nom, prenom, idAdresse, codePostP, nomRueP, numRueP, dateAdhesion, dateFinAdhesion) values("c3", "0", "de Bruges", "Jeff", "0", "13011", "Rue des ruelles", 7800, date("2021-09-11"), date("2022-09-11"));
insert into velomax.ClientP(idC, idProg, nom, prenom, idAdresse, codePostP, nomRueP, numRueP, dateAdhesion, dateFinAdhesion) values("c4", "0", "Groix", "Silvio", "2", "13011", "Rue de la pasrue", 7800, date("2021-09-11"), date("2022-09-11"));

/*Client Boutique*/
insert into velomax.ClientB(idB, idRemise, idAdresse, codePostB, nomRueB, numRueB, telephoneB, emailB, nomContactB) values("b1", "r1", "3", "75016", "Rue des Avenues", 12, "0781517721", "intersport@gmail.com", "Jacques");
insert into velomax.ClientB(idB, idRemise, idAdresse, codePostB, nomRueB, numRueB, telephoneB, emailB, nomContactB) values("b2", "r2", "4", "69006", "Rue des Passages", 5, "3630", "decathlon@gmail.com", "Euristide");
insert into velomax.ClientB(idB, idRemise, idAdresse, codePostB, nomRueB, numRueB, telephoneB, emailB, nomContactB) values("b3", "r3", "5", "69006", "Rue de l'Impasse", 9, "0600000001", "gosport@gmail.com", "Francis");
insert into velomax.ClientB(idB, idRemise, idAdresse, codePostB, nomRueB, numRueB, telephoneB, emailB, nomContactB) values("b4", "r3", "5", "69006", "Rue de l'Impasse", 9, "0600000001", "superu@gmail.com", "Patric");
insert into velomax.ClientB(idB, idRemise, idAdresse, codePostB, nomRueB, numRueB, telephoneB, emailB, nomContactB) values("b5", "r3", "5", "69006", "Rue de l'Impasse", 9, "0600000001", "doritos@gmail.com", "Michel");

/*Velo*/
insert into velomax.velo(idVelo, prixUnitVelo, stockVelo) values("v1", 2400, 5);
insert into velomax.velo(idVelo, prixUnitVelo, stockVelo) values("v2", 400, 3);
insert into velomax.velo(idVelo, prixUnitVelo, stockVelo) values("v3", 50, 10);
insert into velomax.velo(idVelo, prixUnitVelo, stockVelo) values("v4", 40, 20);
insert into velomax.velo(idVelo, prixUnitVelo, stockVelo) values("v5", 6000, 2);
insert into velomax.velo(idVelo, prixUnitVelo, stockVelo) values("v6", 400, 3);
insert into velomax.velo(idVelo, prixUnitVelo, stockVelo) values("v7", 100, 3);
insert into velomax.velo(idVelo, prixUnitVelo, stockVelo) values("v8", 150, 40);
insert into velomax.velo(idVelo, prixUnitVelo, stockVelo) values("v9", 100, 30);

/*Piece*/
insert into velomax.piece(idPiece, prixUnitPiece, quantiteStock) values("p1", 30, 2);
insert into velomax.piece(idPiece, prixUnitPiece, quantiteStock) values("p2", 10, 4);
insert into velomax.piece(idPiece, prixUnitPiece, dateImPiece, quantiteStock) values("p3", 30, date("2020-04-12"), 99);
insert into velomax.piece(idPiece, prixUnitPiece, quantiteStock) values("p4", 10, 7);
insert into velomax.piece(idPiece, prixUnitPiece, quantiteStock) values("p5", 10, 10);

/*Composition Velo*/
insert into velomax.CompositionVelo(idVelo, idPiece, nbPieceVelo) values("v1", "p3", 1);
insert into velomax.CompositionVelo(idVelo, idPiece, nbPieceVelo) values("v1", "p2", 2);
insert into velomax.CompositionVelo(idVelo, idPiece, nbPieceVelo) values("v2", "p1", 3);
insert into velomax.CompositionVelo(idVelo, idPiece, nbPieceVelo) values("v2", "p5", 2);
insert into velomax.CompositionVelo(idVelo, idPiece, nbPieceVelo) values("v3", "p2", 3);
insert into velomax.CompositionVelo(idVelo, idPiece, nbPieceVelo) values("v3", "p5", 3);
insert into velomax.CompositionVelo(idVelo, idPiece, nbPieceVelo) values("v4", "p1", 1);
insert into velomax.CompositionVelo(idVelo, idPiece, nbPieceVelo) values("v5", "p4", 3);
insert into velomax.CompositionVelo(idVelo, idPiece, nbPieceVelo) values("v6", "p4", 6);
insert into velomax.CompositionVelo(idVelo, idPiece, nbPieceVelo) values("v6", "p5", 3);
insert into velomax.CompositionVelo(idVelo, idPiece, nbPieceVelo) values("v8", "p1", 1);
insert into velomax.CompositionVelo(idVelo, idPiece, nbPieceVelo) values("v9", "p2", 3);
insert into velomax.CompositionVelo(idVelo, idPiece, nbPieceVelo) values("v9", "p3", 2);

/*Commande*/
insert into velomax.commande(idCommande, idC) values("cmd1", "c1");
insert into velomax.commande(idCommande, idC) values("cmd2", "c2");
insert into velomax.commande(idCommande, idC) values("cmd3", "c1");
insert into velomax.commande(idCommande, idC) values("cmd4", "c3");
insert into velomax.commande(idCommande, idC) values("cmd5", "c4");

/*Contenu Commande*/
insert into velomax.contenuCommande(idCommande, idVelo, nbItemVelo) values("cmd1", "v1", 1);
insert into velomax.contenuCommande(idCommande, idVelo, nbItemVelo) values("cmd1", "v2", 2);
insert into velomax.contenuCommande(idCommande, idVelo, nbItemVelo) values("cmd2", "v5", 3);
insert into velomax.contenuCommande(idCommande, idVelo, nbItemVelo) values("cmd3", "v6", 4);
insert into velomax.contenuCommande(idCommande, idVelo, nbItemVelo) values("cmd4", "v7", 1);
insert into velomax.contenuCommande(idCommande, idVelo, nbItemVelo) values("cmd5", "v8", 2);
insert into velomax.contenuCommande(idCommande, idVelo, nbItemVelo) values("cmd5", "v9", 3);

/*Supplement*/
insert into velomax.supplement(idPiece, idCommande, nbItemPiece) values("p1","cmd1", "2");
insert into velomax.supplement(idPiece, idCommande, nbItemPiece) values("p1", "cmd2", "1");
insert into velomax.supplement(idPiece, idCommande, nbItemPiece) values("p2", "cmd2", "6");
insert into velomax.supplement(idPiece, idCommande, nbItemPiece) values("p3", "cmd3", "6");
insert into velomax.supplement(idPiece, idCommande, nbItemPiece) values("p4", "cmd4", "6");
insert into velomax.supplement(idPiece, idCommande, nbItemPiece) values("p4", "cmd5", "6");

/*Fournisseur*/
insert into velomax.Fournisseur(siret) values('487216');
insert into velomax.Fournisseur(siret) values('795436');
insert into velomax.Fournisseur(siret) values('481926');
insert into velomax.Fournisseur(siret) values('465433');
insert into velomax.Fournisseur(siret) values('787943');
insert into velomax.Fournisseur(siret) values('894131');

/*Approvisionnement*/
insert into velomax.approvisionnement(siret,idpiece,nbPieceLivraison) values('795436','p1',100);
insert into velomax.approvisionnement(siret,idpiece,nbPieceLivraison) values('795436','p2',200);
insert into velomax.approvisionnement(siret,idpiece,nbPieceLivraison) values('481926','p4',150);
insert into velomax.approvisionnement(siret,idpiece,nbPieceLivraison) values('481926','p2',150);
insert into velomax.approvisionnement(siret,idpiece,nbPieceLivraison) values('787943','p1',150);
insert into velomax.approvisionnement(siret,idpiece,nbPieceLivraison) values('787943','p3',150);

/*Summary*/

select * from ville;
select * from province;
select * from codepostal;
select * from programmefidelite;
select * from remiseboutique;
select * from clientP;
select * from clientB;
select * from velo;
select * from piece;
select * from compositionvelo;
select * from commande;
select * from contenucommande;
select * from supplement;
select * from fournisseur;
select * from approvisionnement;

SELECT idVelo, nomV, grandeur, prixUnitVelo, ligneProduit, stockVelo, dateImVelo, dateDpVelo FROM Velo WHERE idVelo = ' v1'
