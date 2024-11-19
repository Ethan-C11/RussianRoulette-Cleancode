using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RussianRoulette
{
    public class Revolver
    {
        public int barrelSize { get; set; } = 6;
        public int numberOfBlanksBullets { get; set; } = 5;
        public int numberOfRealBullets { get; set; } = 1;
        List<bool> barrelOrder { get; } = new List<bool>();
        Random random = new Random();

        public Revolver(int newBarrelSize) 
        {
           barrelSize = newBarrelSize;
           numberOfRealBullets = random.Next(1, barrelSize);
           numberOfBlanksBullets = barrelSize - numberOfRealBullets;
           barrelOrder = generateBarrelOrder(barrelSize, numberOfRealBullets, numberOfBlanksBullets);
        }

        public List<bool> generateBarrelOrder(int barrelSize, int numberOfBlanks, int numberOfReal)
        {
            List<bool> tempBarrelOrder = new List<bool>();
            for (int i = 0; i < numberOfBlanks; i++)
                tempBarrelOrder.Add(false);
            

            for (int i = 0; i < numberOfReal; i++)
                tempBarrelOrder.Add(true);

            return ShuffleBarrelOrder(tempBarrelOrder);

        }

        public List<bool> ShuffleBarrelOrder(List<bool> barrelOrder)
        {
            List<bool> newBarrelOrder = new List<bool>();

            while (barrelOrder.Count > 0) 
            {
                int IndexOfBulletToTake = random.Next(0, barrelOrder.Count - 1);
                newBarrelOrder.Add(barrelOrder[IndexOfBulletToTake]);
                barrelOrder.RemoveAt(IndexOfBulletToTake);
            }

            return newBarrelOrder;
        }

        public bool Shoot()
        {
            bool isBulletReal = barrelOrder.TakeLast(1).FirstOrDefault();
            barrelOrder.RemoveAt(barrelOrder.Count-1);

            return isBulletReal;
        }

    }
}
