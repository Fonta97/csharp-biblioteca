namespace csharp_biblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            internal class Program
        {
            static void Main(string[] args)
            {
                // creazione di una nuova istanza della classe Biblioteca
                Biblioteca biblioteca = new Biblioteca();

                // aggiunta di alcuni documenti (libri e CD) e utenti di esempio
                biblioteca.AggiungiDocumento(new Libro("IBN001", "It", 1986, "Horror", "A1", new Autore("Stephen", "King"), 1138));
                biblioteca.AggiungiDocumento(new Libro("IBN002", "Harry Potter and the Philosopher's Stone", 1997, "Fantasy", "B2", new Autore("J.K.", "Rowling"), 223));
                biblioteca.AggiungiDocumento(new CD("IBN003", "The Shawshank Redemption", 1994, "Drama", "C3", new Autore("Frank", "Darabont"), "2 ore"));

                // aggiunta di utenti
                Utente marioRossi = new Utente("Rossi", "Mario", "mario.rossi@example.com", "password123", "1234567890");
                biblioteca.AggiungiUtente(marioRossi);

                // associazione di un utente ad almeno un libro in questo caso IT
                Prestito prestito = new Prestito(marioRossi, biblioteca.Documenti[0], DateTime.Now.AddDays(-14), DateTime.Now.AddDays(14));
                biblioteca.AggiungiPrestito(prestito);

                // visualizzazione dell'elenco dei documenti nella biblioteca
                Console.WriteLine("Documenti nella biblioteca:");
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
                                Console.WriteLine($"Preso in prestito da: {prestito.Utente.Nome} {prestito.Utente.Cognome}");
                                Console.WriteLine($"Data inizio prestito: {prestito.DataInizio}");
                                Console.WriteLine($"Data fine prestito: {prestito.DataFine}");
                                break;
                            }
                        }
                    }
    }
}
