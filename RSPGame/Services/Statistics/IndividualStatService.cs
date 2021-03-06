using System;
using System.Threading.Tasks;
using RSPGame.Models;
using RSPGame.Models.GameModel;

namespace RSPGame.Services.Statistics
{
    public class IndividualStatService : IIndividualStatService
    {
        public async Task ChangeGamerInfoAfterRound(GamerInfo gamerInfo, GameActions action, RoundResult status)
        {
            if (gamerInfo == null)
                throw new ArgumentNullException(nameof(gamerInfo));
            if (action == GameActions.None)
                throw new ArgumentException("Invalid action!");
            if (status == RoundResult.None)
                throw new ArgumentException("Invalid game status!");

            await Task.Run(() =>
            {
                ChangeCountAction(gamerInfo, action);
                ChangeCountStatus(gamerInfo, status);
            });
        }

        public GamerInfo ChangeGamerInfoAfterRound(GamerInfo gamerInfo, GamerInfo gamerNewInfo)
        {
            if (gamerInfo == null || gamerNewInfo == null)
                throw new ArgumentNullException(nameof(gamerInfo));

            gamerInfo.CountDraws += gamerNewInfo.CountDraws;
            gamerInfo.CountLoses += gamerNewInfo.CountLoses;
            gamerInfo.CountWins += gamerNewInfo.CountWins;
            gamerInfo.CountPapers += gamerNewInfo.CountPapers;
            gamerInfo.CountRocks += gamerNewInfo.CountRocks;
            gamerInfo.CountScissors += gamerNewInfo.CountScissors;
            gamerInfo.OnlineTime += gamerNewInfo.OnlineTime;
            return gamerInfo;
        }


        private void ChangeCountAction(GamerInfo gamerInfo, GameActions action)
        {
            switch (action)
            {
                case GameActions.Paper:
                    gamerInfo.CountPapers++;
                    break;
                case GameActions.Rock:
                    gamerInfo.CountRocks++;
                    break;
                case GameActions.Scissors:
                    gamerInfo.CountScissors++;
                    break;
            }
        }

        private void ChangeCountStatus(GamerInfo gamerInfo, RoundResult status)
        {
            switch (status)
            {
                case RoundResult.Draw:
                    gamerInfo.CountDraws++;
                    break;
                case RoundResult.Lose:
                    gamerInfo.CountLoses++;
                    break;
                case RoundResult.Win:
                    gamerInfo.CountWins++;
                    break;
            }
        }

        public void ChangeOnlineTime(GamerInfo gamerInfo, TimeSpan onlineTime)
        {
            if (gamerInfo == null)
                throw new ArgumentNullException(nameof(gamerInfo));
            if (onlineTime == TimeSpan.Zero)
                throw new ArgumentException("Online time is zero!");

            gamerInfo.OnlineTime += onlineTime;
        }
    }
}