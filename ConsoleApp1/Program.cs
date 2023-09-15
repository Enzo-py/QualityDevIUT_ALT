using System.Text.Json;
using LibraryData.Code.Medias;

/**
public class Media
{
    public string titre { get; set; }
    public int numero_ref { get; set; }
    public int nb_exemplaires_dispo { get; set; }


    public Media() { }

    public Media(string titre, int numero_ref, int nb_exemplaire_dispo) 
    {
        this.titre = titre;
        this.numero_ref = numero_ref;
        this.nb_exemplaires_dispo = nb_exemplaire_dispo;
    }


    public void AfficherInfos()
    {
        Console.WriteLine($"+--------------------+");
        Console.WriteLine($"Titre: {titre}");
        Console.WriteLine($"Référence: {numero_ref}");
        Console.WriteLine($"Qtt: {nb_exemplaires_dispo}");
        Console.WriteLine($"+--------------------+");
    }
}

public class Livre : Media
{
    public string editeur { get; set; }

    public Livre(string editeur, string titre, int numero_ref, int nb_exemplaire_dispo) : base(titre, numero_ref, nb_exemplaire_dispo)
    {
        this.editeur = editeur;
    }

    public void AfficherInfos()
    {
        Console.WriteLine("+---------- Livre ----------+");
        base.AfficherInfos();
        Console.WriteLine($"Edition: {editeur}");
        Console.WriteLine($"+--------------------+");
    }
}

public class DVD : Media
{
    public int duree { get; set; } // en secondes

    public DVD(int duree, string titre, int numero_ref, int nb_exemplaire_dispo) : base(titre, numero_ref, nb_exemplaire_dispo)
    {
        this.duree = duree;
    }

    public void AfficherInfos()
    {
        Console.WriteLine("+---------- DVD ----------+");
        base.AfficherInfos();
        Console.WriteLine($"Durée (s): {duree}");
        Console.WriteLine($"+--------------------+");
    } 
}

public class CD : Media
{
    public string realisateur { get; set; }

    public CD(string realisateur, string titre, int numero_ref, int nb_exemplaire_dispo) : base(titre, numero_ref, nb_exemplaire_dispo)
    {
        this.realisateur = realisateur;
    }

    public void AfficherInfos()
    {
        Console.WriteLine("+---------- CD -----------+");
        base.AfficherInfos();
        Console.WriteLine($"Réalisateur: {realisateur}");
        Console.WriteLine($"+--------------------+");
    }
}
*/

public class Emprunt
{
    public int id { get; set; }
    public int ref_media { get; set; }
    public string utilisateur { get; set; }

    public Emprunt(int id, int ref_media, string utilisateur)
    {
        this.id = id;
        this.ref_media = ref_media;
        this.utilisateur = utilisateur;
    }

    public void AfficherInfos(string titre = null)
    {
        if (titre != null) { Console.WriteLine($"id: {id} | media: {ref_media}: {titre} | utilisateur: {utilisateur}"); }
        else { Console.WriteLine($"id: {id} | media: {ref_media} | utilisateur: {utilisateur}"); }
        
    }
}

public class Library
{
    public List<Media> collection { get; set; } = new List<Media>();
    public List<Emprunt> emprunts { get; set; } = new List<Emprunt>();

    public Library() { }

    public Library(string json)
    {
        this.Deserialize(json);
    }

    private int emprunts_last_id = 0;

    public Media this[int reference] 
    {
        get { return collection[Find(reference)]; }
    }

    /// <summary>
    ///     Trouver l'index d'un media
    /// </summary>
    /// <param name="reference"></param>
    /// <returns></returns>
    private int Find(int reference)
    {
        for (int i = 0; i < collection.Count; i++)
            if (collection[i].numero_ref == reference)
                return i;
        return -1;
    }

    /// <summary>
    ///     Trouver l'index d'un emprunt
    /// </summary>
    /// <param name="utilisateur"></param>
    /// <param name="reference"></param>
    /// <returns></returns>
    private int FindEmprunt(string utilisateur, int reference)
    {
        for (int i = 0; i < emprunts.Count; i++)
            if (emprunts[i].ref_media == reference && emprunts[i].utilisateur == utilisateur)
                return i;
        return -1;
    }

    /// <summary>
    ///     Ajouter un Média
    /// </summary>
    /// <param name="media"></param>
    public void Add(Media media) { collection.Add(media); }

    /// <summary>
    ///      Retirer un Média
    /// </summary>
    /// <param name="media"></param>
    public void Remove(Media media) { collection.Remove(media); }

    /// <summary>
    ///     Emprunter un Média 
    /// </summary>
    /// <param name="reference"></param>
    /// <param name="utilisateur"></param>
    /// <exception cref="Exception"></exception>
    public void Emprunter(int reference, string utilisateur)
    {
        Media media = collection[Find(reference)];

        if (media == null) { throw new Exception($"Le média {reference} n'est pas dans la collection"); }

        if (media.nb_exemplaires_dispo > 0)
        {
            media.nb_exemplaires_dispo--;
            Emprunt emprunt = new(emprunts_last_id++, reference, utilisateur);
            emprunts.Add(emprunt);
            Console.WriteLine($"Emprunt du média {reference}.");
        } else
        {
            Console.WriteLine($" Impossible d'emprunter ce media. Aucun en stock");
        }
    }

    /// <summary>
    ///     Retourner un Média
    /// </summary>
    /// <param name="reference"></param>
    /// <param name="utilisateur"></param>
    /// <exception cref="Exception"></exception>
    public void Retourner(int reference, string utilisateur)
    {
        Media media = collection[Find(reference)];
        if (media == null) { throw new Exception($"Le média {reference} n'est pas dans la collection"); }

        emprunts.Remove(emprunts[FindEmprunt(utilisateur, reference)]);
        media.nb_exemplaires_dispo++;
        Console.WriteLine($"Le media {reference} a bien été retourné");
    }

    /// <summary>
    ///     Rechercher un Média par Titre
    /// </summary>
    /// <param name="motif"></param>
    public void Rechercher(string motif)
    {
        Console.WriteLine("+===============================================+");
        Console.WriteLine($"Recherche de '{motif}' dans la collection: ");
        int find = 0;
        for (int i = 0; i < collection.Count; i++)
        {
            Media media = (Media)collection[i];
            if (collection[i] != null && media.titre.Contains(motif))
            {
                if (media is CD) { ((CD)media).AfficherInfos(); }
                else if (media is DVD) { ((DVD)media).AfficherInfos(); }
                else if (media is Livre) { ((Livre)media).AfficherInfos(); }
                else { media.AfficherInfos(); }
                find++;
                Console.WriteLine("");
            }
        }
        Console.WriteLine($"{find} résultat(s) trouvé(s)");
        Console.WriteLine("+===============================================+");
    }

    /// <summary>
    ///     Lister les Médias Empruntés par un Utilisateur
    /// </summary>
    /// <param name="utilisateur"></param>
    public void Historique(string utilisateur)
    {
        int find = 0;
        Console.WriteLine("+-----------------------------------+");
        for (int i = 0; i < emprunts.Count; i++) {
            if (emprunts[i].utilisateur == utilisateur)
            {
                // Récupérer le titre du média afin de détailler l'affichage
                Media media = collection[this.Find(emprunts[i].ref_media)] as Media;

                // Afficher l'emprunt
                emprunts[i].AfficherInfos(media.titre);
                Console.WriteLine("|------------------>");
                find++;
            }
        }

        Console.WriteLine($"{find} emprunts ont été trouvés pour l'utilisateur {utilisateur}");
        Console.WriteLine("+-----------------------------------+");
    }

    /// <summary>
    ///     Afficher les Statistiques de la Bibliothèque
    /// </summary>
    public void Statistiques()
    {
        int nb_emprunt_media = emprunts.Count;
        int nb_disponible_media = 0;

        for (int media_index = 0; media_index < collection.Count; media_index++)
        {
            nb_disponible_media += collection[media_index].nb_exemplaires_dispo;
        }

        Console.WriteLine("+-----------------------------------+");
        Console.WriteLine($"Nombre de media total: {nb_emprunt_media + nb_disponible_media}");
        Console.WriteLine($"Nombre de media disponibles: {nb_disponible_media}");
        Console.WriteLine($"Nombre de media empruntés: {nb_emprunt_media}");
        Console.WriteLine("+-----------------------------------+");
    } 

    /// <summary>
    ///     Ajouter un media
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static Library operator +(Library left, Media right) 
    {
        left.Add(right);
        return left;
    }

    /// <summary>
    ///     Retirer un media
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static Library operator -(Library left, Media right)
    {
        left.Remove(right);
        return left;
    }

    public string Serialize()
    {
        return JsonSerializer.Serialize<Library>(this);
    }

    public void Deserialize(string json)
    {
        Library tmp = JsonSerializer.Deserialize<Library>(json);
        this.collection = tmp.collection;
        this.emprunts = tmp.emprunts;
    }

}


class GFG
{

    // Main Method
    static public void Main(String[] args)
    {

        Library library = new Library();
        DVD dvd1 = new DVD(3 * 3600 + 1800, "Avatar", 1208, 18);
        DVD dvd2 = new DVD(2 * 3600 + 1800, "Skyfall", 1071, 9);

        CD cd1 = new CD("Antonio Vivaldi", "Les 4 saisons", 2109, 2);
        CD cd2 = new CD("???", "C# en musique", 2712, 1);

        Livre livre1 = new Livre("Pocket Edition", "Le C# pour les nuls", 3021, 7);

        string utilisateur1 = "Enzo";
        string utilisateur2 = "Killian";

        library.Statistiques();
        library = library + dvd1 + dvd2 + cd1 + cd2 + livre1;
        library.Statistiques();

        library.Emprunter(dvd1.numero_ref, utilisateur1);
        library.Emprunter(livre1.numero_ref, utilisateur1);
        library.Emprunter(dvd2.numero_ref, utilisateur2);

        library.Historique(utilisateur1);
        library.Retourner(dvd1.numero_ref, utilisateur1);
        library.Historique(utilisateur1);
        library.Statistiques();

        library.Rechercher("C#");

        string json = library.Serialize();
        Console.WriteLine(json);

        Library library2 = new Library();
        library2.Deserialize(json);

        library2.Statistiques();
    }
}
