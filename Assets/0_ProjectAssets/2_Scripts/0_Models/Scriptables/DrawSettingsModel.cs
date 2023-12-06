/**
 * Author:      Yannick Santa Cruz Feuillias
 * Created:     29/11/2023
 **/

// Dependencies
using UnityEngine;
// Custom dependencies
using YannickSCF.LSTournaments.Common;

namespace YannickSCF.TournamentDraw.Scriptables {
    [CreateAssetMenu(fileName = "Draw Settings Configuration", menuName = "YannickSCF/LS Tournaments/Tournament Draw/Draw Settings", order = 1)]
    public class DrawSettingsModel : ScriptableObject {

        public enum DrawSettingType { AthleteRevealOrder/*, AthleteRevealAnimation*/, AllInMayus, NamingTypeSelected, ShowCountry, QuickInfo };

        public delegate void DrawSettingsChangedEventDelegate(DrawSettingType type);
        public event DrawSettingsChangedEventDelegate DrawSettingsChanged;

        public enum AthleteRevealOrder { NextInNextPoule, NextInPoule, NextInAllPoules, CompletePoule, Custom };
        //public enum AthleteRevealAnimation { Quick, Basic, BasicWithPresentation, LongWithPresentation };
        public enum AthleteQuickDrawInfo { Academy, School, Combined };

        [Header("Reveal process")]
        [SerializeField] private AthleteRevealOrder _revealOrderSelected;
        //[SerializeField] private AthleteRevealAnimation _revealAnimationSelected; // TODO
        [Header("Athlete Info showed")]
        [SerializeField] private bool _allInMayus;
        [SerializeField] private FullNameType _namingTypeSelected;
        [SerializeField] private bool _showCountry;
        [SerializeField] private AthleteQuickDrawInfo _quickInfo;

        public AthleteRevealOrder RevealOrderSelected {
            get => _revealOrderSelected;
            set {
                DrawSettingsChanged?.Invoke(DrawSettingType.AthleteRevealOrder);
                _revealOrderSelected = value;
            }
        }

        //public AthleteRevealAnimation RevealAnimationSelected {
        //    get => _revealAnimationSelected;
        //    set {
        //        DrawSettingsChanged?.Invoke(DrawSettingType.AthleteRevealAnimation);
        //        _revealAnimationSelected = value;
        //    }
        //}

        public bool AllInMayus {
            get => _allInMayus;
            set {
                DrawSettingsChanged?.Invoke(DrawSettingType.AllInMayus);
                _allInMayus = value;
            }
        }

        public FullNameType NamingTypeSelected {
            get => _namingTypeSelected;
            set {
                DrawSettingsChanged?.Invoke(DrawSettingType.NamingTypeSelected);
                _namingTypeSelected = value;
            }
        }

        public bool ShowCountry {
            get => _showCountry;
            set {
                DrawSettingsChanged?.Invoke(DrawSettingType.ShowCountry);
                _showCountry = value;
            }
        }

        public AthleteQuickDrawInfo QuickInfo {
            get => _quickInfo;
            set {
                DrawSettingsChanged?.Invoke(DrawSettingType.QuickInfo);
                _quickInfo = value;
            }
        }

        public void Reset() {
            //_revealAnimationSelected = AthleteRevealAnimation.Quick;
            _revealOrderSelected = AthleteRevealOrder.NextInNextPoule;

            _allInMayus = false;
            _namingTypeSelected = FullNameType.SurnameName;
            _showCountry = false;
            _quickInfo = AthleteQuickDrawInfo.Academy;
        }
    }
}
