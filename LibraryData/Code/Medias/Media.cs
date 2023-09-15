using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Code.Medias
{
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

}
