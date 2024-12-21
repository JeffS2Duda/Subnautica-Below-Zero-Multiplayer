namespace Subnautica.API.Features
{
    using Subnautica.API.Extensions;
    using System.Collections.Generic;
    using System.Linq;

    public class Interact
    {
        public const string ServerHost = "[localhost]";

        public static Dictionary<string, string> List { get; private set; } = new Dictionary<string, string>();

        public static void ClearAll()
        {
            List.Clear();
        }

        public static void SetList(Dictionary<string, string> list)
        {
            List = list;
        }

        public static string GetCustomId(string customId)
        {
            return string.Format("{0}_{1}", ServerHost, customId);
        }

        public static bool IsBlocked(string constructionId, bool isMineIgnore = false)
        {
            if (string.IsNullOrEmpty(constructionId))
            {
                return false;
            }

            var interact = List.Where(q => q.Value == constructionId).FirstOrDefault();
            if (interact.Value == null)
            {
                return false;
            }

            if (isMineIgnore)
            {
                return interact.Key != ZeroPlayer.CurrentPlayer.UniqueId;
            }

            return true;
        }

        public static bool IsBlocked(string constructionId, string playerId, bool ignoreServer = false)
        {
            var interact = List.Where(q => q.Value == constructionId).FirstOrDefault();
            if (interact.Value == null)
            {
                return false;
            }

            if (playerId.IsNotNull())
            {
                if (ignoreServer && interact.Key.Contains(playerId))
                {
                    return false;
                }

                return interact.Key != playerId;
            }

            return true;
        }

        public static bool IsBlockedByMe(string constructionId = null)
        {
            if (string.IsNullOrEmpty(constructionId))
            {
                return List.ContainsKey(ZeroPlayer.CurrentPlayer.UniqueId);
            }

            return List.TryGetValue(ZeroPlayer.CurrentPlayer.UniqueId, out string _constructionId) && _constructionId == constructionId;
        }

        public static void ShowUseDenyMessage()
        {
            ShowDenyMessage("GAME_ITEM_USED_ANOTHER_PLAYER");
        }

        public static void ShowDenyMessage(string text, bool isLang = true)
        {
            if (isLang)
            {
                text = ZeroLanguage.Get(text);
            }

            HandReticle.main.SetText(HandReticle.TextType.Hand, text, false, GameInput.Button.None);
            HandReticle.main.SetText(HandReticle.TextType.HandSubscript, string.Empty, false);
            HandReticle.main.SetIcon(HandReticle.IconType.HandDeny);
        }

        public static void Dispose()
        {
            ClearAll();
        }
    }
}
