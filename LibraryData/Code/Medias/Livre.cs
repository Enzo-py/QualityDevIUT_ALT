using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Code.Medias
{
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

}
