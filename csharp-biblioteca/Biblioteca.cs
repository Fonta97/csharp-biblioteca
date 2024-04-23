using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_biblioteca
{


    //CLASSE UTENTE
    public class Utente
    {
        private string _cognome;
        public string Cognome
        {
            get { return _cognome; }
            set { _cognome = value; }
        }

        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _recapitoTelefonico;
        public string RecapitoTelefonico
        {
            get { return _recapitoTelefonico; }
            set { _recapitoTelefonico = value; }
        }


        public Utente(string cognome, string nome, string email, string password, string recapitoTelefonico)
        {
            Cognome = cognome;
            Nome = nome;
            Email = email;
            Password = password;
            RecapitoTelefonico = recapitoTelefonico;
        }
    }


    //CLASSE DOC
    public class Documento
    {
        private string _codiceIdentificativo;
        public string CodiceIdentificativo
        {
            get { return _codiceIdentificativo; }
            set { _codiceIdentificativo = value; }
        }

        private string _titolo;
        public string Titolo
        {
            get { return _titolo; }
            set { _titolo = value; }
        }

        private int _anno;
        public int Anno
        {
            get { return _anno; }
            set { _anno = value; }
        }

        private string _settore;
        public string Settore
        {
            get { return _settore; }
            set { _settore = value; }
        }

        private string _scaffale;
        public string Scaffale
        {
            get { return _scaffale; }
            set { _scaffale = value; }
        }


        //IMPORT AUTORE
        private Autore _autore;
        public Autore Autore
        {
            get { return _autore; }
            set { _autore = value; }
        }

        public Documento(string codiceIdentificativo, string titolo, int anno, string settore, string scaffale, Autore autore)
        {
            CodiceIdentificativo = codiceIdentificativo;
            Titolo = titolo;
            Anno = anno;
            Settore = settore;
            Scaffale = scaffale;
            Autore = autore;
        }
    }


    //CLASSE LIBRO EREDITA DOCUMENTO
    public class Libro : Documento
    {
        private int _numeroPagine;
        public int NumeroPagine
        {
            get { return _numeroPagine; }
            set { _numeroPagine = value; }
        }

        public Libro(string codiceIdentificativo, string titolo, int anno, string settore, string scaffale, Autore autore, int numeroPagine)
             : base(codiceIdentificativo, titolo, anno, settore, scaffale, autore)
        {
            NumeroPagine = numeroPagine;
        }
    }

    //CLASSE CD EREDITA DOCUMENTO
    public class CD : Documento
    {
        private string _durata;
        public string Durata
        {
            get { return _durata; }
            set { _durata = value; }
        }

        public CD(string codiceIdentificativo, string titolo, int anno, string settore, string scaffale, Autore autore, string durata)
            : base(codiceIdentificativo, titolo, anno, settore, scaffale, autore)
        {
            Durata = durata;
        }
    }


    //CLASSE AUTORE
    public class Autore
    {
        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        private string _cognome;
        public string Cognome
        {
            get { return _cognome; }
            set { _cognome = value; }
        }

        public Autore(string nome, string cognome)
        {
            Nome = nome;
            Cognome = cognome;
        }
    }

    public class Prestito
    {
        public Utente Utente { get; set; }
        public Documento Documento { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime DataFine { get; set; }

        public Prestito(Utente utente, Documento documento, DateTime dataInizio, DateTime dataFine)
        {
            Utente = utente;
            Documento = documento;
            DataInizio = dataInizio;
            DataFine = dataFine;
        }
    }

    public class Biblioteca
    {
        public List<Documento> Documenti { get; private set; }
        public List<Utente> Utenti { get; private set; }
        public List<Prestito> Prestiti { get; private set; }


       
        public Biblioteca()
        {
            Documenti = new List<Documento>();
            Utenti = new List<Utente>();
            Prestiti = new List<Prestito>();
        }

        //METHODS

        // Metodo new utente
        public void AggiungiUtente(Utente utente)
        {
            Utenti.Add(utente);
        }

        // Metodo  add documento alla biblioteca
        public void AggiungiDocumento(Documento documento)
        {
            Documenti.Add(documento);
        }

        // Metodo add prestito
        public void AggiungiPrestito(Prestito prestito)
        {
            Prestiti.Add(prestito);
        }


        // ricerca di documenti per codice e titolo
        public List<Documento> CercaDocumentoPerCodice(string codice)
        {


            // lista per memorizzare i documenti trovati
            List<Documento> cercaConCodice = new List<Documento>();

            // itera attraverso tutti i documenti nella biblioteca
            foreach (Documento documento in Documenti)
            {

                // confronta il titolo del documento con il titolo fornito
                // ignora le differenze tra maiuscole e minuscole nella ricerca
                if (documento.CodiceIdentificativo.ToLower().Contains(codice.ToLower()))
                {
                    cercaConCodice.Add(documento);
                }
            }

            // restituisce la lista dei documenti trovati
            return cercaConCodice;
        }
        // Metodo per cercare documenti per titolo
        public List<Documento> CercaDocumentiPerTitolo(string titolo)
        {
            // lista per memorizzare i documenti trovati
            List<Documento> cercaConTitolo = new List<Documento>();

            // itera attraverso tutti i documenti nella biblioteca
            foreach (Documento documento in Documenti)
            {
                // confronta il titolo del documento con il titolo fornito
                // ignora le differenze tra maiuscole e minuscole nella ricerca
                if (documento.Titolo.ToLower().Contains(titolo.ToLower()))
                {
                    // Se il titolo è trovato, aggiungi il documento alla lista dei risultati
                    cercaConTitolo.Add(documento);
                }
            }

            // restituisce la lista dei documenti trovati
            return cercaConTitolo;
        }

        // Metodo per cercare prestiti dato nome e cognome dell'utente
        public List<Prestito> CercaPrestitiPerUtente(string nome, string cognome)
        {
            // lista per memorizzare i prestiti trovati
            List<Prestito> prestitiTrovati = new List<Prestito>();

            // itera attraverso tutti i prestiti nella biblioteca
            foreach (Prestito prestito in Prestiti)
            {
                // confronta nome e cognome dell'utente associato al prestito con quelli forniti
                // ignora le differenze tra maiuscole e minuscole nella ricerca
                if (prestito.Utente.Nome.ToLower() == nome.ToLower() && prestito.Utente.Cognome.ToLower() == cognome.ToLower())
                {
                    // Se l'utente è trovato, aggiungi il prestito alla lista dei risultati
                    prestitiTrovati.Add(prestito);
                }
            }

            // restituisce la lista dei prestiti trovati
            return prestitiTrovati;
        }


    }

}