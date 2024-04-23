using System;

namespace csharp_biblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //utente base Paolo Pazzo IT e Back in Black per test
            
            Biblioteca biblioteca = new Biblioteca();

            // aggiunta di libri e CD
            biblioteca.AggiungiDocumento(new Libro("IBN001", "It", 1986, "Horror", "A1", new Autore("Stephen", "King"), 1138));
            biblioteca.AggiungiDocumento(new Libro("IBN002", "Harry Potter and the Philosopher's Stone", 1997, "Fantasy", "B2", new Autore("J.K.", "Rowling"), 223));
            biblioteca.AggiungiDocumento(new Libro("IBN004", "The Lord of the Rings", 1954, "Fantasy", "A2", new Autore("J.R.R.", "Tolkien"), 1178));
            biblioteca.AggiungiDocumento(new Libro("IBN005", "1984", 1949, "Dystopian", "B3", new Autore("George", "Orwell"), 328));
            biblioteca.AggiungiDocumento(new CD("IBN003", "The Shawshank Redemption", 1994, "Drama", "C3", new Autore("Frank", "Darabont"), "2 ore"));
            biblioteca.AggiungiDocumento(new CD("IBN006", "The Dark Side of the Moon", 1973, "Rock", "C4", new Autore("Pink", "Floyd"), "43 min"));
            biblioteca.AggiungiDocumento(new CD("IBN007", "Thriller", 1982, "Pop", "C5", new Autore("Michael", "Jackson"), "42 min"));
            biblioteca.AggiungiDocumento(new CD("IBN008", "Back in Black", 1980, "Rock", "C6", new Autore("AC", "DC"), "42 min"));


            // aggiunta utenti preimpostati
            Utente paolinoPazzo = new Utente("Pazzo", "Paolino", "paolino.pazzo@example.com", "password123", "1234567890");
            biblioteca.AggiungiUtente(paolinoPazzo);
            Utente luigiVerdi = new Utente("Verdi", "Luigi", "luigi.verdi@example.com", "password456", "0987654321");
            biblioteca.AggiungiUtente(luigiVerdi);
            Utente giovannaBianchi = new Utente("Bianchi", "Giovanna", "giovanna.bianchi@example.com", "password789", "5432167890");
            biblioteca.AggiungiUtente(giovannaBianchi);

            
            Prestito prestito = new Prestito(paolinoPazzo, biblioteca.Documenti[0], DateTime.Now.AddDays(-14), DateTime.Now.AddDays(14));
            biblioteca.AggiungiPrestito(prestito);

            Prestito prestito2 = new Prestito(giovannaBianchi, biblioteca.Documenti[7], DateTime.Now.AddDays(-17), DateTime.Now.AddDays(11));
            biblioteca.AggiungiPrestito(prestito2);

            Console.WriteLine("Inserisci un nome e cognome tra gli utenti pre-selezionati per verificare se un libro è stato preso in prestito:");
            Console.WriteLine("********************");
            Console.WriteLine("Inserisci nome dell'utente:");
            string nomeUtente = Console.ReadLine();
            Console.WriteLine("Inserisci cognome dell'utente:");
            string cognomeUtente = Console.ReadLine();

            // ricerca dei prestiti per nome e cognome dell'utente
            var prestitiUtente = biblioteca.CercaPrestitiPerUtente(nomeUtente, cognomeUtente);

            // visualizzazione dei prestiti trovati
            if (prestitiUtente.Count > 0)
            {
                Console.WriteLine($"I prestiti di {nomeUtente} {cognomeUtente} sono questi:");
                foreach (Prestito p in prestitiUtente)
                {
                    Console.WriteLine();
                    Console.WriteLine($"- Titolo: {p.Documento.Titolo}," +
                        $" \nData Inizio: {p.DataInizio.ToShortDateString()}," +
                        $" \nData Fine: {p.DataFine.ToShortDateString()} - " +
                        $"\nEmail: {p.Utente.Email} - " +
                        $"\nNumero di telefono: {p.Utente.RecapitoTelefonico}");
                }
            }
            else
            {
                Console.WriteLine($"Nessun prestito trovato per {nomeUtente} {cognomeUtente}.");
            }

            Console.WriteLine();
            // visualizzazione dell'elenco dei documenti nella biblioteca
            Console.WriteLine("Libri e CD nella biblioteca:");
            foreach (Documento documento in biblioteca.Documenti)
            {
                // stampa dei dettagli di ciascun documento
                Console.WriteLine($"- {documento.CodiceIdentificativo}-{documento.Titolo} ({documento.GetType().Name})");
            }

            // richiesta all'utente di digitare il titolo o il codice del documento da cercare
            Console.WriteLine("\nDigita il titolo o il codice del film o del libro che desideri cercare:");
            string titoloOcodice = Console.ReadLine();

            // ricerca dei documenti per titolo o codice
            var nomeTrovato = biblioteca.CercaDocumentiPerTitolo(titoloOcodice);
            var codiceTrovato = biblioteca.CercaDocumentoPerCodice(titoloOcodice);

            // verifica se sono stati trovati documenti corrispondenti
            if (nomeTrovato.Count > 0)
            {
                // se sono stati trovati documenti corrispondenti per titolo
                Console.WriteLine($"\nDocumenti trovati per il titolo '{titoloOcodice}':");
                foreach (Documento documento in nomeTrovato)
                {
                    // stampa dei dettagli di ciascun documento
                    Console.WriteLine($"-Titolo: {documento.Titolo}");
                    Console.WriteLine($"Tipo: {documento.GetType().Name}");
                    Console.WriteLine($"Codice: {documento.CodiceIdentificativo}");
                    Console.WriteLine($"Anno: {documento.Anno}");
                    Console.WriteLine($"Settore: {documento.Settore}");
                    Console.WriteLine($"Scaffale: {documento.Scaffale}");
                    Console.WriteLine($"Autore: {documento.Autore.Nome} {documento.Autore.Cognome}");


                    // Verifica se il documento è un CD e stampa la durata se lo è
                    if (documento is CD cd)
                    {
                        Console.WriteLine($"Durata: {cd.Durata}");
                    }
                    // Verifica se il documento è un libro e stampa il numero di pagine se lo è
                    else if (documento is Libro libro)
                    {
                        Console.WriteLine($"Numero di pagine: {libro.NumeroPagine}");
                    }


                    // verifica se il documento è stato preso in prestito da qualche utente
                    bool presoInPrestito = false;
                    foreach (Prestito p in biblioteca.Prestiti)
                    {
                        // Se il documento è stato preso in prestito, stampa i dettagli dell'utente che lo ha preso
                        if (p.Documento.CodiceIdentificativo == documento.CodiceIdentificativo)
                        {
                            presoInPrestito = true;
                            Console.WriteLine($"=======================:");
                            Console.WriteLine($"PRESO IN PRESTITO DA:");
                            Console.WriteLine($"- Nome e Cognome: {p.Utente.Nome} {p.Utente.Cognome}," +
                               $" \nData Inizio: {p.DataInizio.ToShortDateString()}," +
                               $" \nData Fine: {p.DataFine.ToShortDateString()}  " +
                               $"\nEmail: {p.Utente.Email} - " +
                               $"\nNumero di telefono: {p.Utente.RecapitoTelefonico}");
                            break;

                            //ToShortDateString = trasforma la data in stringa
                        }
                    }

                    // se il documento non è stato preso in prestito
                    if (!presoInPrestito)
                    {
                        Console.WriteLine("Non preso in prestito");
                    }
                }
            }
            else if (codiceTrovato.Count > 0)
            {
                // Se sono stati trovati documenti corrispondenti per codice
                Console.WriteLine($"\nDocumenti trovati per il codice '{titoloOcodice}':");
                foreach (Documento documento in codiceTrovato)
                {
                    // stampa dei dettagli di ciascun documento
                    Console.WriteLine($"-Titolo: {documento.Titolo}");
                    Console.WriteLine($"Tipo: {documento.GetType().Name}");
                    Console.WriteLine($"Codice: {documento.CodiceIdentificativo}");
                    Console.WriteLine($"Anno: {documento.Anno}");
                    Console.WriteLine($"Settore: {documento.Settore}");
                    Console.WriteLine($"Scaffale: {documento.Scaffale}");
                    Console.WriteLine($"Autore: {documento.Autore.Nome} {documento.Autore.Cognome}");


                    //se il documento è un CD stampa la durata 
                    if (documento is CD cd)
                    {
                        Console.WriteLine($"Durata: {cd.Durata}");
                    }
                    //se il documento è un libro stampa il numero di pagine 
                    else if (documento is Libro libro)
                    {
                        Console.WriteLine($"Numero di pagine: {libro.NumeroPagine}");
                    }


                    // verifica se il documento è stato preso in prestito da qualche utente
                    bool presoInPrestito = false;
                    foreach (Prestito p in biblioteca.Prestiti)
                    {
                        // Se il documento è stato preso in prestito, stampa i dettagli dell'utente che lo ha preso
                        if (p.Documento.CodiceIdentificativo == documento.CodiceIdentificativo)
                        {

                            presoInPrestito = true;
                            Console.WriteLine($"=======================:");
                            Console.WriteLine($"PRESO IN PRESTITO DA:");
                            Console.WriteLine($"- Nome e Cognome: {p.Utente.Nome} {p.Utente.Cognome}," +
                               $" \nData Inizio: {p.DataInizio.ToShortDateString()}," +
                               $" \nData Fine: {p.DataFine.ToShortDateString()}  " +
                               $"\nEmail: {p.Utente.Email} - " +
                               $"\nNumero di telefono: {p.Utente.RecapitoTelefonico}");
                            break;

                            //ToShortDateString = trasforma la data in stringa
                        }

                    }

                    // se il documento non è stato preso in prestito
                    if (!presoInPrestito)
                    {
                        Console.WriteLine("Non preso in prestito");
                    }
                }
            }
            else
            {
                // se non sono stati trovati documenti 
                Console.WriteLine($"Nessun documento trovato per il titolo o il codice '{titoloOcodice}'");
            }
        }
    }
}