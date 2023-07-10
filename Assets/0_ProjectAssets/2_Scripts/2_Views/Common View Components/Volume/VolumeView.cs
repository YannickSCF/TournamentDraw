using UnityEngine;
using UnityEngine.UI;

namespace YannickSCF.TournamentDraw.Views.CommonComponents.Volume {
    public class VolumeView : MonoBehaviour {

        public event CommonEventsDelegates.BooleanEvent OnMuteButtonPressed;
        public event CommonEventsDelegates.FloatEvent OnVolumeSettedPressed;

        [SerializeField] private Toggle _muteButton;
        [SerializeField] private Slider _volumeSlider;

        #region Mono
        private void OnEnable() {
            _muteButton.onValueChanged.AddListener(MuteButtonPressed);
            _volumeSlider.onValueChanged.AddListener(VolumeSliderChanged);
        }

        private void OnDisable() {
            _muteButton.onValueChanged.RemoveAllListeners();
            _volumeSlider.onValueChanged.RemoveAllListeners();
        }
        #endregion

        #region Event listeners methods
        private void MuteButtonPressed(bool isOn) {
            OnMuteButtonPressed?.Invoke(isOn);
        }

        private void VolumeSliderChanged(float newVolume) {
            OnVolumeSettedPressed?.Invoke(newVolume);
        }
        #endregion

        public void SetMuted(bool isMuted) {
            _muteButton.SetIsOnWithoutNotify(isMuted);
        }

        public void SetValue(float volumeValue) {
            _volumeSlider.SetValueWithoutNotify(volumeValue);
        }

        public void SetSliderInteractable(bool isInteractable) {
            _volumeSlider.interactable = isInteractable;
        }
    }
}
