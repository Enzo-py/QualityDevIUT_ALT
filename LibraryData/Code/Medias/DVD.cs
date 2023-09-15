using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Code.Medias
{
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
}
