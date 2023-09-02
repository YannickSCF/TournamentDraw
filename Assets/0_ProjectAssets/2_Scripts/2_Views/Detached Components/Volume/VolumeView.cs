using UnityEngine;
using UnityEngine.UI;
using static YannickSCF.GeneralApp.CommonEventsDelegates;

namespace YannickSCF.TournamentDraw.Views.CommonComponents.Volume {
    public class VolumeView : MonoBehaviour {

        public event BooleanEventDelegate OnMuteButtonPressed;
        public event FloatEventDelegate OnVolumeSettedPressed;

        [Header("Mute volume toggle")]
        [SerializeField] private Toggle _muteButton;
        [SerializeField] private Image _volumeImage;
        [SerializeField] private Sprite _unmutedSprite;
        [SerializeField] private Sprite _mutedSprite;
        [Header("Volume Slider")]
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

            _volumeImage.sprite = isOn ? _mutedSprite : _unmutedSprite;
        }

        private void VolumeSliderChanged(float newVolume) {
            OnVolumeSettedPressed?.Invoke(newVolume);
        }
        #endregion

        public void SetMuted(bool isMuted, bool notify = false) {
            if (notify) {
                _muteButton.isOn = isMuted;
            } else {
                _muteButton.SetIsOnWithoutNotify(isMuted);
            }

            _volumeImage.sprite = isMuted ? _mutedSprite : _unmutedSprite;
        }

        public void SetValue(float volumeValue, bool notify = false) {
            if (notify) {
                _volumeSlider.value = volumeValue;
            } else {
                _volumeSlider.SetValueWithoutNotify(volumeValue);
            }
        }

        public void SetSliderInteractable(bool isInteractable) {
            _volumeSlider.interactable = isInteractable;
        }
    }
}
