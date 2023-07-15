using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace YannickSCF.TournamentDraw.Views.Draw {
    public class DrawBallsView : MonoBehaviour {

        [Header("Draw balls instantiation")]
        [SerializeField] private GameObject _ballsPrefab;
        [SerializeField] private Transform _ballsParent;
        [Header("Ball instantiation positions parameters")]
        [SerializeField] private Transform _ballRevealPosition;
        [SerializeField] private Transform _ballButtonSidePosition;
        [SerializeField] private Transform _ballDownSideInitialPosition;
        [SerializeField, Range(1, 5)] private float _downSideBallSeparation;

        [Header("Animations parameters")]
        [SerializeField] private VisualEffect _vfxReveal;
        [SerializeField, Range(1, 5)] private float _timeToMoveCenter;
        [SerializeField] private Color _colorToShow;

        [Header("TEMP DATA")]
        [SerializeField] private List<LootBox> _lootBoxes;

        private Vector3 _ballPositionPreReveal;

        #region Mono
        private void OnEnable() {
            foreach(LootBox lootbox in _lootBoxes) {
                lootbox.OnStartReveal += MoveFromPositionToReveal;
                lootbox.OnVFXCanInit += StartVFXReveal;
                lootbox.OnRevealEnds += ResetBall;
            }
        }

        private void OnDisable() {
            foreach (LootBox lootbox in _lootBoxes) {
                lootbox.OnStartReveal += ResetBall;
                lootbox.OnVFXCanInit -= StartVFXReveal;
                lootbox.OnRevealEnds -= ResetBall;
            }
        }
        #endregion

        private void MoveFromPositionToReveal(LootBox ballToMove) {
            // TODO: Block all other clicks

            _ballPositionPreReveal = ballToMove.transform.position;
            StartCoroutine(MoveFromPositionToRevealCoroutine(ballToMove));
        }

        private IEnumerator MoveFromPositionToRevealCoroutine(LootBox ballToMove) {
            yield return null;

            float c_time = 0f;
            while (c_time < _timeToMoveCenter) {
                c_time += Time.deltaTime;
                ballToMove.transform.position = Vector3.Lerp(_ballPositionPreReveal, _ballRevealPosition.position, c_time / _timeToMoveCenter);
                yield return new WaitForEndOfFrame();
            }

            ballToMove.OpenDrawBall();
        }

        private void StartVFXReveal() {
            if (!_vfxReveal.gameObject.activeSelf) {
                _vfxReveal.gameObject.SetActive(true);
            } else {
                _vfxReveal.Play();
            }
        }

        private void ResetBall(LootBox ballToReset) {
            ballToReset.transform.position = _ballPositionPreReveal;
            ballToReset.ResetDrawBall();
        }
    }
}
