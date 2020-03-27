using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LIMB {
    /// <summary>
    /// Handles the UI portions of battles.
    /// Fading in/out, getting input from clicks and handing over to battle manager, etc 
    /// </summary>
    public class ImageFader : MonoBehaviour {

        [SerializeField]
        GameObject commandUI;
        [SerializeField]
        float fadeSpeed = 0.85f;
        [SerializeField]
        Image battleStartFadeImage;
        [SerializeField]
        Image battleEndFadeImage;

        private void Start() {
            SceneTransitioner transitioner = GetComponent<SceneTransitioner>();
            if(transitioner) {
                transitioner.OnBattleStart.AddListener(EnableUI);
                transitioner.OnBattleEnd.AddListener(DisableUI);
            }else {
                Debug.LogError("Unable to find SceneTransitioner in gameObject " + gameObject.name);
            }
        }

        public void EnableUI() {
            commandUI.SetActive(true);
        }

        public void DisableUI() {
            commandUI.SetActive(false);
        }

        public IEnumerator BattleStartFadeOut() {
            yield return StartCoroutine(FadeImage(battleStartFadeImage, 1.0f));
        }

        public IEnumerator BattleStartFadeIn() {
            yield return StartCoroutine(FadeImage(battleStartFadeImage, 0.0f));
        }

        public IEnumerator BattleEndFadeOut() {
            yield return StartCoroutine(FadeImage(battleEndFadeImage, 1.0f));
        }

        public IEnumerator BattleEndFadeIn() {
            yield return StartCoroutine(FadeImage(battleEndFadeImage, 0.0f));
        }

        IEnumerator FadeImage(Image image, float alpha){
            if(image != null){
                if(image.color.a < alpha){
                    while(image.color.a < alpha){
                        var tempColor = image.color;
                        tempColor.a += Time.deltaTime * fadeSpeed;
                        image.color = tempColor;
                        yield return new WaitForEndOfFrame();
                    }
                }else if(image.color.a > alpha){
                    while(image.color.a > alpha){
                        var tempColor = image.color;
                        tempColor.a -= Time.deltaTime * fadeSpeed;
                        image.color = tempColor;
                        yield return new WaitForEndOfFrame();
                    }

                }
            }
        }
    }
}
