using System.Collections.Generic;
using Windows.Data.Json;
using Windows.Storage;

namespace Likebook
{
    internal static class SiteSettingsHelper
    {
        private const string StorageKey = "sites";
        private static readonly ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        public static List<SiteOption> GetSites()
        {
            if (localSettings.Values.ContainsKey(StorageKey))
            {
                var raw = localSettings.Values[StorageKey]?.ToString();
                var parsed = Deserialize(raw);
                if (parsed.Count > 0)
                    return parsed;
            }

            var defaults = GetDefaultSites();
            SaveSites(defaults);
            return defaults;
        }

        public static void SaveSites(IEnumerable<SiteOption> sites)
        {
            localSettings.Values[StorageKey] = Serialize(sites);
        }

        public static List<SiteOption> GetDefaultSites()
        {
            return new List<SiteOption>
            {
                new SiteOption("Facebook", "https://www.facebook.com/", "Mozilla/5.0 (Android 14; Mobile; rv:120.0) Gecko/120.0 Firefox/120.0", "\uE12B", "Versão mobile do Facebook.", "#3b5998"),
                new SiteOption("X / Twitter", "https://mobile.twitter.com/", "Mozilla/5.0 (Linux; Android 14; Pixel 7 Pro) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.6099.144 Mobile Safari/537.36 EdgA/120.0.0.0", "\uE12A", "Interface mobile do X (antigo Twitter).", "#000000"),
                new SiteOption("Instagram", "https://www.instagram.com/", "Mozilla/5.0 (Linux; Android 14; Pixel 7 Pro) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.6099.144 Mobile Safari/537.36", "\uE158", "Instagram com user-agent de Android.", "#C13584"),
                new SiteOption("YouTube", "https://m.youtube.com/", "Mozilla/5.0 (iPhone; CPU iPhone OS 17_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/17.1 Mobile/15E148 Safari/604.1", "\uE714", "YouTube mobile em modo iPhone.", "#FF0000")
            };
        }

        private static string Serialize(IEnumerable<SiteOption> sites)
        {
            var array = new JsonArray();
            foreach (var site in sites)
            {
                var obj = new JsonObject
                {
                    { "name", JsonValue.CreateStringValue(site.Name ?? string.Empty) },
                    { "url", JsonValue.CreateStringValue(site.Url ?? string.Empty) },
                    { "ua", JsonValue.CreateStringValue(site.UserAgent ?? string.Empty) },
                    { "glyph", JsonValue.CreateStringValue(site.Glyph ?? string.Empty) },
                    { "description", JsonValue.CreateStringValue(site.Description ?? string.Empty) },
                    { "color", JsonValue.CreateStringValue(site.ColorHex ?? string.Empty) }
                };
                array.Add(obj);
            }
            return array.Stringify();
        }

        private static List<SiteOption> Deserialize(string raw)
        {
            var result = new List<SiteOption>();
            if (string.IsNullOrWhiteSpace(raw))
                return result;

            if (!JsonArray.TryParse(raw, out JsonArray array))
                return result;

            foreach (var item in array)
            {
                if (item.ValueType != JsonValueType.Object)
                    continue;

                var obj = item.GetObject();
                string name = obj.ContainsKey("name") ? obj["name"].GetString() : "";
                string url = obj.ContainsKey("url") ? obj["url"].GetString() : "";
                string ua = obj.ContainsKey("ua") ? obj["ua"].GetString() : "";
                string glyph = obj.ContainsKey("glyph") ? obj["glyph"].GetString() : "";
                string description = obj.ContainsKey("description") ? obj["description"].GetString() : "";
                string color = obj.ContainsKey("color") ? obj["color"].GetString() : "";

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(url))
                    continue;

                result.Add(new SiteOption(name, url, ua, glyph, description, color));
            }

            return result;
        }
    }
}
