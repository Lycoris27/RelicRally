using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BaseSceneManager : MonoBehaviour
{
    private static BaseSceneManager instance;

    public int currTimelineSpot = 0;

    [SerializeField] private List<string> sceneTimeline;

    public Image fader;
    private GameObject[] gameManager;

    private void Awake()
    {
        currTimelineSpot = 1;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            fader.rectTransform.sizeDelta = new Vector2(Screen.width + 20, Screen.height + 20);
            fader.gameObject.SetActive(false);
        }
        else
        {
            Destroy(instance);
        }

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string Scenename;
            Scenename = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
            sceneTimeline.Add(Scenename);
            Debug.Log(Scenename);
        }
    }

    public void sceneTrigger(int sceneIndex, bool restart)
    {
        LoadScene(sceneIndex, restart);
    }

    public static void LoadScene(int sceneIndex,bool restart)
    {
        instance.StartCoroutine(instance.FadeScene(sceneIndex,restart, 1, 1));
    }

    private IEnumerator FadeScene(int sceneIndex, bool restart, float duration, float waitTime)
    {
        yield return new WaitForSeconds(0.25f);

        fader.gameObject.SetActive(true);


        for (float i = 0; i < 1; i += Time.deltaTime / duration)
        {
            fader.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, i));
            yield return null;
        }

        if (restart == false)
        {
            currTimelineSpot = sceneIndex;
            Debug.Log(currTimelineSpot);
        }
        else if (restart == true)
        {
            currTimelineSpot = 0;
            Debug.Log("restarting");
        }


        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneTimeline[currTimelineSpot]);

        while (!ao.isDone)
            yield return null;

        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(waitTime);

        for (float i = 0; i < 1; i += Time.unscaledDeltaTime / duration)
        {
            fader.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, i));
            yield return null;
        }

        Debug.Log(currTimelineSpot);

        fader.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
