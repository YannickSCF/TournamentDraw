using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YannickSCF.GeneralApp.CountriesData {
    public enum Language { EN, ES, IT, FR }

    [CreateAssetMenu(fileName = "Country Data List", menuName = "Scriptable Objects/YannickSCF/Country Data List", order = 0)]
    public class CountriesDataList : ScriptableObject {

        [SerializeField] private List<CountryData> _allCountries;

        public Sprite GetFlagByCode(string code) {
            CountryData res = GetCountryDataByCode(code);

            if (res != null) return res.CountryFlag;
            return null;
        }

        public string GetNameByCode(string code, SystemLanguage? language = null) {
            CountryData res = GetCountryDataByCode(code);

            if (res != null) {
                if (!language.HasValue) language = Application.systemLanguage;
                return res.CountryName.GetTranslatedName(language.Value);
            }
            return null;
        }

        public string GetCodeByName(string name) {
            CountryData res = GetCountryDataByName(name);

            if (res != null) {
                return res.CountryId;
            }
            return null;
        }

        public bool IsCountryCodeInList(string code) {
            return GetCountryDataByCode(code) != null;
        }

        public bool IsCountryNameInList(string name) {
            return GetCountryDataByName(name) != null;
        }

        public Dictionary<string, Sprite> SearchCountriesByCode(string code) {
            Dictionary<string, Sprite> res = new Dictionary<string, Sprite>();

            List<CountryData> allMatches = _allCountries.Where(x => x.CountryId.Contains(code, System.StringComparison.InvariantCultureIgnoreCase)).ToList();
            foreach (CountryData data in allMatches) {
                res.Add(data.CountryId, data.CountryFlag);
            }

            return res;
        }

        public Dictionary<string, Sprite> SearchCountriesByPartialName(string partialName) {
            Dictionary<string, Sprite> res = new Dictionary<string, Sprite>();

            List<CountryData> allMatches = _allCountries.Where(x => x.HasPartialNameCoincidence(partialName)).ToList();
            foreach (CountryData data in allMatches) {
                res.Add(data.CountryId, data.CountryFlag);
            }

            return res;
        }

        private CountryData GetCountryDataByName(string name) {
            return _allCountries.FirstOrDefault(x => x.HasNameCoincidence(name));
        }

        private CountryData GetCountryDataByCode(string code) {
            if (string.IsNullOrEmpty(code) || code.Length != 2) {
                Debug.LogError("Country code wrong length!");
                return null;
            }

            return _allCountries.FirstOrDefault(x => x.CountryId.Equals(code, System.StringComparison.InvariantCultureIgnoreCase));
        }

        [System.Serializable]
        private class CountryData {
            [SerializeField] private string countryId;
            [SerializeField] private CountryNameTranslations countryName;
            [SerializeField] private Sprite countryFlag;

            public string CountryId { get => countryId; }
            public CountryNameTranslations CountryName { get => countryName; }
            public Sprite CountryFlag { get => countryFlag; }

            public bool HasNameCoincidence(string name, SystemLanguage? language = null) {
                return !language.HasValue ?
                    countryName.HasCoincidence(name) :
                    countryName.HasCoincidence(name, language.Value);
            }

            public bool HasPartialNameCoincidence(string partialName, SystemLanguage? language = null) {
                return !language.HasValue ?
                    countryName.HasPartialCoincidence(partialName) :
                    countryName.HasPartialCoincidence(partialName, language.Value);
            }
        }

        [System.Serializable]
        private class CountryNameTranslations {

            [SerializeField] private string spanishName;
            [SerializeField] private string englishName;
            [SerializeField] private string italianName;
            [SerializeField] private string frenchName;

            public string GetTranslatedName(SystemLanguage language) {
                switch (language) {
                    case SystemLanguage.Spanish:
                        if (string.IsNullOrEmpty(spanishName)) return englishName;
                        else return spanishName;
                    case SystemLanguage.Italian:
                        if (string.IsNullOrEmpty(italianName)) return englishName;
                        else return italianName;
                    case SystemLanguage.French:
                        if (string.IsNullOrEmpty(frenchName)) return englishName;
                        else return frenchName;
                    case SystemLanguage.English:
                    default:
                        if (string.IsNullOrEmpty(englishName)) return "ERROR";
                        else return englishName;
                }
            }

            public bool HasCoincidence(string name) {
                bool res = false;

                res |= spanishName.Contains(name);
                res |= englishName.Contains(name);
                res |= italianName.Contains(name);
                res |= frenchName.Contains(name);

                return res;
            }

            public bool HasCoincidence(string name, SystemLanguage language) {
                switch (language) {
                    case SystemLanguage.Spanish:
                        return spanishName.Contains(name);
                    case SystemLanguage.Italian:
                        return italianName.Contains(name);
                    case SystemLanguage.French:
                        return frenchName.Contains(name);
                    case SystemLanguage.English:
                        return englishName.Contains(name);
                    default:
                        return false;
                }
            }

            public bool HasPartialCoincidence(string partialName) {
                bool res = false;

                res |= spanishName.Contains(partialName);
                res |= englishName.Contains(partialName);
                res |= italianName.Contains(partialName);
                res |= frenchName.Contains(partialName);

                return res;
            }

            public bool HasPartialCoincidence(string partialName, SystemLanguage language) {
                switch (language) {
                    case SystemLanguage.Spanish:
                        return spanishName.Contains(partialName);
                    case SystemLanguage.Italian:
                        return italianName.Contains(partialName);
                    case SystemLanguage.French:
                        return frenchName.Contains(partialName);
                    case SystemLanguage.English:
                        return englishName.Contains(partialName);
                    default:
                        return false;
                }
            }
        }
    }
}
