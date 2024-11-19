using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RussianRoulette
{
    internal class Game
    {
        Revolver revolver { get; set; }
        public Game() { 
        }

        public void startTheGame()
        {
            Console.WriteLine("Bienvenu au jeu de la roulette russe !\n Voulez vous lire les règles ?"
             + "\nO : Oui\nN : Non");

            string answer = Console.ReadLine().ToLower() ?? "n";

            if (answer == "o")
            {
                displayTheRules();
            }

            revolver = initRevolver(chooseBarrelSize());

            Console.WriteLine($"{revolver.numberOfRealBullets} balles réelles\n"
            + $"{revolver.numberOfBlanksBullets} balles à blanc\n");

            bool didThePlayerWin = GameSequence(PlayerChoice());
            if(didThePlayerWin)
            {
                Console.WriteLine("Vous avez gagné !");
            } else
            {
                Console.WriteLine("Vous avez perdu...");
            }

            Console.WriteLine("\nVeuillez appuyez sur une touche pour quitter le jeu");
            Console.ReadLine();


        }

        private void displayTheRules()
        {
            Console.WriteLine("Voici les règles :"
            + "\nAu lancement du jeu, vous choisissez la taille du barrillet du revolver."
            + "\nLe croupier charge l'arme de balles réelles et de balles à blanc. Il annonce ensuite le nombre de balle."
            + "\nLes balles sont placées dans un ordre aléatoire."
            + "\nVous devez ensuite choisir entre tirer sur le croupier ou vous tirez dessus."
            + "\nSi vous vous tirez une balle à blanc sur votre propre tête, vous pouvez rejouez, sinon c'est au tour du croupier."
            + "\nLe survivant remporte la victoire.");
        }

        private int chooseBarrelSize() 
        {
            Console.WriteLine("Veuillez choisir la taille du barillet (de 3 à 9)");

            string answer = Console.ReadLine();

            if (IsBarrelSizeConform(answer))
            {
               return parseNumberFromInput(answer);
            } else
            {
                Console.WriteLine("Entrée incorrecte, veuillez réessayer.");
                return chooseBarrelSize();
            }
        }

        private int parseNumberFromInput(string input)
        {
            try
            {
                return int.Parse(input);
            } catch
            {
                return -1;
            }
        }

        private bool IsBarrelSizeConform(string desiredBarrelSize)
        {
            try
            {
                int parsedBarrelSize =  parseNumberFromInput(desiredBarrelSize);
                if(parsedBarrelSize >= 3 && parsedBarrelSize <= 9)
                    return true;

                return false;
            } catch
            {
                return false;
            }
 
        }

        private Revolver initRevolver(int barrelSize)
        {
            return new Revolver(barrelSize);
        }

        /// <summary>
        /// Retourne le choix du joueur
        /// True : tir sur soi-même
        /// False : tir sur le Croupier
        /// </summary>
        /// <returns></returns>
        private bool PlayerChoice()
        {
            Console.WriteLine("Voulez vous tirez sur votre propre tête ou sur le croupier ?" +
                "\nV : Sur vous même" +
                "\nC : Sur le croupier");

            string answer = Console.ReadLine().ToLower();

            if (answer != "v" && answer != "c" )
            {
                Console.WriteLine("Entreé invalide, veuillez réessayer.");
                return PlayerChoice();
            } else if (answer == "v")
            {
                return true;
            } 
            return false;
        }

        private bool DealerChoice()
        {
            Random random = new Random();
            bool isTheDealerTargettingThePlayer = convertIntToBool(random.Next(0, 1));

            return isTheDealerTargettingThePlayer;

        }

        /// <summary>
        /// Séquence du jeu.     
        /// <param name="choice"></param>
        /// <returns></returns>
        private bool GameSequence(bool isThePlayerTheTarget)
        {
            bool isTheBulletReal = revolver.Shoot();
            if(isTheBulletReal && isThePlayerTheTarget) 
            {
                Console.WriteLine("C'était une balle réelle, vous êtes mort");
                return false;
            } else if (isTheBulletReal && !isThePlayerTheTarget)
            {
                Console.WriteLine("C'était une balle réelle, le corps du croupier s'écroule au sol.\nBien joué.");
                return true;
            } else if (!isTheBulletReal && isThePlayerTheTarget)
            {
                Console.WriteLine("C'était une balle à blanc, un tour pour vous");
                return GameSequence(PlayerChoice());
            }
            else
            {
                Console.WriteLine("C'était une balle à blanc, un tour pour le croupier");
                return GameSequence(DealerChoice());
            }
        }

        private bool convertIntToBool(int intergetToConvert)
        {
            switch(intergetToConvert)
            {
                case 0: return true;
                case 1: return false;
                default : return false;
            }
        }
    }
}
