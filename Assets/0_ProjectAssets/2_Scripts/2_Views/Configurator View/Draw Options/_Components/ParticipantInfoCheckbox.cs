using UnityEngine;
using UnityEngine.UI;
// Custom dependencies
using YannickSCF.TournamentDraw.Views.Configurator.Events;

namespace YannickSCF.TournamentDraw.Views.Configurator.DrawOptions.Components {
    [RequireComponent(typeof(Toggle))]
    public class ParticipantInfoCheckbox : MonoBehaviour {

        [SerializeField] private ParticipantBasicInfo toggleType;
        [SerializeField] private Toggle checkboxToggle;

        #region Mono
        private void Awake() {
            if (checkboxToggle == null) {
                checkboxToggle = GetComponentInChildren<Toggle>();
            }
        }

        private void OnEnable() {
            checkboxToggle.onValueChanged.AddListener(OnCheckboxValueChanged);
        }

        private void OnDisable() {
            checkboxToggle.onValueChanged.RemoveAllListeners();
        }
        #endregion

        #region Event listeners methods
        private void OnCheckboxValueChanged(bool isOn) {
            ConfiguratorViewEvents.ThrowOnParticipantInfoCheckboxToggle(toggleType, isOn);
        }
        #endregion

        #region GETTERS
        public ParticipantBasicInfo GetParticipantBasicInfo() {
            return toggleType;
        }

        public bool GetCheckboxIsOn() {
            return checkboxToggle.isOn && gameObject.activeSelf;
        }
        #endregion

        #region SETTERS
        public void SetCheckboxValue(bool isOn) {
            checkboxToggle.SetIsOnWithoutNotify(isOn);
        }
        #endregion
    }
}
