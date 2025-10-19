using System.Threading;

namespace Toyota.Shared.Language
{
    public static class DataLanguageManager
    {
        public static string? GetDataTranslation(string culture, string key)
        {
            return culture switch
            {
                EnglishLanguage.Culture => EnglishLanguage.GetTranslation(key),
                TurkishLanguage.Culture => TurkishLanguage.GetTranslation(key),
                SpanishLanguage.Culture => SpanishLanguage.GetTranslation(key),
                _ => null,
            };
        }
    }

    internal class TurkishLanguage
    {
        public const string Culture = "tr-TR";
        public static string? GetTranslation(string key) => key switch
        {
            "Ghana" => "Gana",
            "Uruguay" => "Uruguay",
            "KoreaRepublic" => "Güney Kore",
            "Portugal" => "Portekiz",
            "Ecuador" => "Ekvador",
            "Senegal" => "Senegal",
            "Netherlands" => "Hollanda",
            "Qatar" => "Katar",
            "Croatia" => "Hırvatistan",
            "Belgium" => "Belçika",
            "Australia" => "Avustralya",
            "Denmark" => "Danimarka",
            "SaudiArabia" => "Suudi Arabistan",
            "Mexico" => "Meksika",
            "Poland" => "Polonya",
            "Argentina" => "Arjantin",
            "Wales" => "Galler",
            "England" => "İngiltere",
            "IRIran" => "İran",
            "USA" => "ABD",
            "Tunisia" => "Tunus",
            "France" => "Fransa",
            "Canada" => "Kanada",
            "Morocco" => "Fas",
            "CostaRica" => "Kosta Rika",
            "Germany" => "Almanya",
            "Japan" => "Japonya",
            "Spain" => "İspanya",
            "Serbia" => "Sırbistan",
            "Switzerland" => "İsviçre",
            "Cameroon" => "Kamerun",
            "Brazil" => "Brezilya",
            "Week" => "Hafta",
            "customer" => "Müşteri",
            "employee" => "Personel",
            "super-admin" => "Super Admin",
            "primary-admin" => "Primary Admin",
            "CorrectAnswerTemplateText" => "Tebrikler, testi {0} soruya {1} doğru yanıt vererek tamamladınız.",
            "INVALID_TOKEN" => "Token süresi dolmuştur. Lütfen tekrar giriş yapınız.",
            "ACCESS_DENIED" => "Bu işlem için yetkiniz bulunmamaktadır.",
            _ => null,
        };

    }
    internal class EnglishLanguage
    {
        public const string Culture = "en-US";
        public static string? GetTranslation(string key) => key switch
        {
            "Ghana" => "Ghana",
            "Uruguay" => "Uruguay",
            "KoreaRepublic" => "Korea Republic",
            "Portugal" => "Portugal",
            "Ecuador" => "Ecuador",
            "Senegal" => "Senegal",
            "Netherlands" => "Netherlands",
            "Qatar" => "Qatar",
            "Croatia" => "Croatia",
            "Belgium" => "Belgium",
            "Australia" => "Australia",
            "Denmark" => "Denmark",
            "SaudiArabia" => "Saudi Arabia",
            "Mexico" => "Mexico",
            "Poland" => "Poland",
            "Argentina" => "Argentina",
            "Wales" => "Wales",
            "England" => "England",
            "IRIran" => "IR Iran",
            "USA" => "USA",
            "Tunisia" => "Tunisia",
            "France" => "France",
            "Canada" => "Canada",
            "Morocco" => "Morocco",
            "CostaRica" => "Costa Rica",
            "Germany" => "Germany",
            "Japan" => "Japan",
            "Spain" => "Spain",
            "Serbia" => "Serbia",
            "Switzerland" => "Switzerland",
            "Cameroon" => "Cameroon",
            "Brazil" => "Brazil",
            "Week" => "Week",
            "SliderReadyAnswer_1" => "I strongly disagree",
            "SliderReadyAnswer_2" => "I do not agree",
            "SliderReadyAnswer_3" => "I both agree and disagree",
            "SliderReadyAnswer_4" => "I agree",
            "SliderReadyAnswer_5" => "Absolutely I agree",
            "customer" => "Customer",
            "employee" => "Personnel",
            "super-admin" => "Super Admin",
            "primary-admin" => "Primary Admin",
            "CorrectAnswerTemplateText" => "Congratulations, you have completed the test with {1} correct answers to {0} questions.",
            "INVALID_TOKEN" => "Invalid token",
            "ACCESS_DENIED" => "Access denied!",
            _ => null,
        };
    }

    internal class SpanishLanguage
    {
        public const string Culture = "es-ES";
        public static string? GetTranslation(string key) => key switch
        {
            "ProductMPLengthText" => "Hasta {0}",
            _ => null,
        };

    }
}
