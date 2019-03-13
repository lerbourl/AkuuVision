using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GlobalControl : MonoBehaviour {

    // ONLY CONSTANT TO CHANGE BY HAND
    public const int NUMBER_OF_PHOTOS = 23;

    #region GOOGLESHEET_VAR

    private string gameDataFileName = "Akuu-exhibition-data";
    private PhotoData[] loadedData;
    private Hashtable targetID = new Hashtable();

    #endregion GOOGLESHEET_VAR

    #region PLAYERPREF_VAR

    // fields should be private but usefull for debugging in inspector...
    public bool[] foundPhotos = new bool[NUMBER_OF_PHOTOS];
    private int score;
    private Language appLanguage;

    #endregion PLAYERPREF_VAR

    #region PLAYERPREF_KEYS
    private string languageKey = "language";
    private string photoFoundKey(int id)
    {
        return "photo" + id;
    }

    protected bool checkId(int id)
    {
        if (0 <= id && id < NUMBER_OF_PHOTOS)
        {
            return true;
        }
        else
        {
            Debug.LogError("photo id " + id + " is not between 0 and NUMBER OF PHOTO " + NUMBER_OF_PHOTOS);
            return false;
        }
    }

    #endregion PLAYERPREF_KEYS

    #region PRIVATE_METHODS

    private Language getLanguageFromPrefs()
    {
        return (Language)Enum.Parse(typeof(Language), PlayerPrefs.GetString(languageKey));
    }

    private bool getPhotoFoundFromPrefs(int id)
    {
        if (PlayerPrefs.GetInt(photoFoundKey(id)) == 1) return true;
        else return false;
    }

    // load language from Prefs
    private void initLanguage()
    {
        if (!PlayerPrefs.HasKey(languageKey))
        {
            // the very first time
            setlanguage(Language.fr);
        }
        // the other times
        appLanguage = getLanguageFromPrefs();
    }
    // load photosFounded from Prefs
    private void initFoundPhotos()
    {
        for (int i = 0; i < NUMBER_OF_PHOTOS; i++)
        {
            if (!PlayerPrefs.HasKey(photoFoundKey(i)))
            {
                // the very first times
                PlayerPrefs.SetInt(photoFoundKey(i), 0);
            }
            // the other times
            foundPhotos[i] = getPhotoFoundFromPrefs(i);
        }
    }

    private void computeScore()
    {
        score = 0;
        foreach (bool b in foundPhotos)
        {
            if (b) score++;
        }
    }

    // updating other game objets
    public void updateLanguage()
    {
        foreach (var t in FindObjectsOfType<TextLanguage>())
        {
            t.updateText(appLanguage);
        }
        GameObject fr = GameObject.FindGameObjectWithTag("frenchbutton");
        GameObject en = GameObject.FindGameObjectWithTag("englishbutton");
        if (fr != null && en != null)
        {
            Image frimage = fr.GetComponent<Image>();
            Image enimage = en.GetComponent<Image>();
            
            if (appLanguage == Language.en)
            {
                frimage.color = new Color32(255, 255, 255, 90);
                enimage.color = new Color32(255, 255, 255, 255);

            }
            else
            {
                enimage.color = new Color32(255, 255, 255, 90);
                frimage.color = new Color32(255, 255, 255, 255);
            }
        }
    }

    #endregion PRIVATE_METHODS

    #region PUBLIC_INTERFACE

    // getters
    public int getScore()
    {
        return score;
    }

    public Language getLanguage()
    {
        return appLanguage;
    }

    public int GetTargetID(string targetName)
    {
        return (int)targetID[targetName];
    }

    // setters
    public void setlanguage(Language l)
    {
        PlayerPrefs.SetString(languageKey, l.ToString());
        Debug.Log("write prefs " + languageKey);
        appLanguage = l;
        updateLanguage();
    }

    public bool photoIsFound(int id)
    {
        if (checkId(id))
        {
            return foundPhotos[id];
        }
        else return false;
    }
    
    public void setNewPhotoFound(int id)
    {
        if (checkId(id))
        {
            if (foundPhotos[id] == false)
            {
                Debug.Log("write prefs " + photoFoundKey(id));
                PlayerPrefs.SetInt(photoFoundKey(id), 1);
                foundPhotos[id] = true;
                score++;
            }
            // else nothing happens because already found !
        }
    }

    public string getUrlPhoto(int id)
    {
        if (appLanguage == Language.en)
        {
            return loadedData[id].url_en;
        }
        else
        {
            return loadedData[id].url_fr;
        }
    }

    public Sprite getAssociatedSprite(int id)
    {
        string path = "AkuuImages/" + loadedData[id].nom_target + "_scaled";
        return Resources.Load<Sprite>(path);
    }

    public string getPhotoName(int id)
    {
        return loadedData[id].nom;
    }

    public float getLatitude(int id)
    {
        return loadedData[id].irl_latitude;
    }

    public float getLongitude(int id)
    {
        return loadedData[id].irl_longitude;
    }

    #endregion PUBLIC_INTERFACE

    #region INITIALIZATION

    // SINGLETON
    public static GlobalControl Instance;
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            // loading all data in the singleton for all scenes
            LoadAllData();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void LoadAllData()
    {
        // loading json
        LoadGameDataJSON();
        LoadTargetHashMap();

        // initialization from player prefs
        initLanguage();
        initFoundPhotos();
        computeScore();
    }

    private void Start()
    {
        // after the awake in textLanguage Managers
        updateLanguage();
    }

    #endregion INITIALIZATION

    #region JSON_TOOLS

    [Serializable]
    public class PhotoData
    {
        public int id;
        public string nom;
        public string url_fr;
        public string url_en;
        public float irl_latitude;
        public float irl_longitude;
        public string nom_target;
    }

    private void LoadGameDataJSON()
    {
        TextAsset file = Resources.Load<TextAsset>(gameDataFileName);
        if (file != null)
        {
            // Read the json from the file into a 
            string dataAsJson = file.ToString();
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            loadedData = JsonHelper.FromJson<PhotoData>(dataAsJson);
            //loadedData = loadedDataList.ToArray();
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }

    private void LoadTargetHashMap()
    {
        foreach (PhotoData pd in loadedData)
        {
            targetID.Add(pd.nom_target, pd.id);
        }
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }

    #endregion JSON_TOOLS

}
