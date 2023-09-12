namespace Alpha.HowToUse.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public interface IUser
    {
        bool IsEnabledJackpot { get; }
    }

    public class User : IUser
    {
        private const string Jackpot = "Jackpot";
        private const string GameEnableSessionKey = "GameEnable_";

        public bool IsEnabledJackpot => GetEnableProductsByGameName(Jackpot).Any();

        private static IList<int> GetEnableProductsByGameName(string gameName)
            => (IList<int>)HttpContext.Current.Session[GameEnableSessionKey + gameName];
    }
}