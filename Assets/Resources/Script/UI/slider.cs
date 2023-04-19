using UnityEngine;
using System.Collections;
using UnityEngine.UI; // ←※これを忘れずに入れる
using UnityEngine.SceneManagement;

public class slider : MonoBehaviour
{
    public string sliderType = "audio";
    public float get_num = 0;
    Slider _slider;
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "title")
        {
            GManager.instance.audioMax = PlayerPrefs.GetFloat("audioMax", 0.16f);
            GManager.instance.seMax = PlayerPrefs.GetFloat("seMax", 0.16f);
            GManager.instance.siya = PlayerPrefs.GetInt("siya", 60);
        }
        // スライダーを取得する
        _slider = this.GetComponent<Slider>();
        if (sliderType == "audio")
        {
            _slider.value = GManager.instance.audioMax;
        }
        else if (sliderType == "se")
        {
            _slider.value = GManager.instance.seMax;
        }
        else if (sliderType == "siya")
        {
            _slider.value = GManager.instance.siya;
        }
        else if (sliderType == "sun")
        {
            _slider.value = GManager.instance.sun_power;
        }
    }

    void Update()
    {
        if (sliderType == "audio" && GManager.instance.audioMax != _slider.value)
        {
            GManager.instance.audioMax = _slider.value;
        }
        else if (sliderType == "se" && GManager.instance.seMax != _slider.value)
        {
            GManager.instance.seMax = _slider.value;
        }
        else if (sliderType == "siya" && GManager.instance.siya != _slider.value)
        {
            GManager.instance.siya = _slider.value;
        }
        else if (sliderType == "sun" && _slider.value != GManager.instance.sun_power)
        {
            _slider.value = GManager.instance.sun_power;
        }
    }
}