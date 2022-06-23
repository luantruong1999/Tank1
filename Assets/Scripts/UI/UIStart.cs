using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIStart : MonoBehaviour
{
    [SerializeField] private Image selectedImage;
    [SerializeField] private AudioClip audioSelected;
    private Transform _currentSelected;
    private Transform _eventSystemSelected;
    private AudioSource audioSource;

    [SerializeField]private Toggle musicToggle;
    private bool isMute;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        int mute = PlayerPrefs.GetInt("Mute", 0);
        isMute = (mute == 1);
        musicToggle.isOn = !isMute;
    }


    private void Update()
    {
        
        _eventSystemSelected = EventSystem.current.currentSelectedGameObject.transform;
        if (_currentSelected != _eventSystemSelected)
        {
            if(_currentSelected!=null && !isMute) audioSource.PlayOneShot(audioSelected);
            _currentSelected = _eventSystemSelected;
            var y = _currentSelected.localPosition.y;
            selectedImage.transform.localPosition = new Vector2(selectedImage.transform.localPosition.x,y);
        }
    }

    public void OnStartBtn()
    {
        Debug.Log("StartGame");
        SceneManager.LoadScene("Scenes/InGame");
    }

    public void OnMusicToggle()
    {
        isMute = !musicToggle.isOn;
        int mute = isMute ? 1 : 0;
        PlayerPrefs.SetInt("Mute", mute);
    }
}
