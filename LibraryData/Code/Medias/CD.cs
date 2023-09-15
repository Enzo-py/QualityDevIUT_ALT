using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Code.Medias
{
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
}
