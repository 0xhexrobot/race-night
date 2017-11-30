using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrophyManager : MonoBehaviour {
    [SerializeField]
    private GameObject btnBack;

    void Start() {
        StartCoroutine(showBackButton());
    }

    private IEnumerator showBackButton() {
        yield return new WaitForSeconds(5.0f);
        btnBack.SetActive(true);
    }

    public void backToTitle() {
        SceneManager.LoadScene("title");
    }
}
