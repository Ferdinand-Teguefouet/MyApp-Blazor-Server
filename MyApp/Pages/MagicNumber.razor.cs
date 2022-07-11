using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Pages
{
    public class MagicNumberBase : ComponentBase
    {
        protected const int nbLifeMax = 5;
        protected const int maxNumber = 10;
        private int number = 0;
        protected int magNumberOfUser;
        protected int remainingLife;

        protected bool? isWon;

        protected void TestMagicNumber()
        {
            if (magNumberOfUser == number)
            {
                isWon = true;
            }
            else
            {
                remainingLife--;
                if (remainingLife == 0)
                {
                    isWon = false;
                }
            }
        }
        protected void ResetGame()
        {
            var rdm = new Random();

            number = rdm.Next(maxNumber);
            remainingLife = nbLifeMax;
            isWon = null;
            magNumberOfUser = 0;
        }
        protected override void OnInitialized()
        {
            ResetGame();
            base.OnInitialized();
        }

    }
}
