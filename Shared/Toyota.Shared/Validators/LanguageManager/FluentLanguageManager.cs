using System.Collections.Concurrent;
using System.Globalization;
using FluentValidation.Resources;
using Toyota.Shared.Utilities;
using Toyota.Shared.Validators.Languages;

namespace Toyota.Shared.Validators.LanguageManager
{
    public class FluentLanguageManager : ILanguageManager
	{
		private readonly ConcurrentDictionary<string, string> _languages = new ConcurrentDictionary<string, string>();


        private static string? GetPropertyTranslation(string culture, string key)
        {
            return culture switch
            {
                FluentEnglishLanguage.AmericanCulture => FluentEnglishLanguage.GetPropertyTranslation(key),
                FluentEnglishLanguage.BritishCulture => FluentEnglishLanguage.GetPropertyTranslation(key),
                FluentEnglishLanguage.Culture => FluentEnglishLanguage.GetPropertyTranslation(key),
                FluentTurkishLanguage.Culture => FluentTurkishLanguage.GetPropertyTranslation(key),
                _ => null,
            };
        }


		/// <summary>
		/// Language factory.
		/// </summary>
		/// <param name="culture">The culture code.</param>
		/// <param name="key">The key to load</param>
		/// <returns>The corresponding Language instance or null.</returns>
		private static string? GetTranslation(string culture, string key)
		{
			return culture switch
			{
                FluentEnglishLanguage.AmericanCulture => FluentEnglishLanguage.GetTranslation(key),
                FluentEnglishLanguage.BritishCulture => FluentEnglishLanguage.GetTranslation(key),
                FluentEnglishLanguage.Culture => FluentEnglishLanguage.GetTranslation(key),
                FluentTurkishLanguage.Culture => FluentTurkishLanguage.GetTranslation(key),
                _ => null,
			};
		}

		/// <summary>
		/// Whether localization is enabled.
		/// </summary>
		public bool Enabled { get; set; } = true;

		/// <summary>
		/// Default culture to use for all requests to the LanguageManager. If not specified, uses the current UI culture.
		/// </summary>
		public CultureInfo Culture { get; set; }

		/// <summary>
		/// Removes all languages except the default.
		/// </summary>
		public void Clear()
		{
			_languages.Clear();
		}

		/// <summary>
		/// Gets a translated string based on its key. If the culture is specific and it isn't registered, we try the neutral culture instead.
		/// If no matching culture is found  to be registered we use English.
		/// </summary>
		/// <param name="key">The key</param>
		/// <param name="culture">The culture to translate into</param>
		/// <returns></returns>
		public virtual string GetString(string key, CultureInfo culture = null)
		{
			string value = null;

			if (Enabled)
			{
				culture = culture ?? new CultureInfo(Constants.TRLanguage);

				string currentCultureKey = culture.Name + ":" + key;
				if (key.StartsWith("P_"))
                    value = _languages.GetOrAdd(currentCultureKey, k => GetPropertyTranslation(culture.Name, key));
				else
				    value = _languages.GetOrAdd(currentCultureKey, k => GetTranslation(culture.Name, key));
            }

            return value ?? string.Empty;
		}

        public void AddTranslation(string language, string key, string message)
		{
			if (string.IsNullOrEmpty(language)) throw new ArgumentNullException(nameof(language));
			if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
			if (string.IsNullOrEmpty(message)) throw new ArgumentNullException(nameof(message));

			_languages[language + ":" + key] = message;
		}
	}
}
