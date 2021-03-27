using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Rendering.HighDefinition;

public class Menu : MonoBehaviour
{
    public GameObject loading;

    public Animator animator;
    public Animator animatorOptions;
    public TMP_Dropdown graphDropDown, langDropDown;
    public Toggle toggle;
    public Slider slider;
    public TMP_Text shadowText, version;
    public Slider sound;
    private void Start()
    {
        graphDropDown.value = PlayerPrefs.GetInt("Grap", 0);
        toggle.isOn = PlayerPrefs.GetInt("Blur", 1) == 0 ? false : true;
        slider.value = PlayerPrefs.GetInt("Shadow", 150);
        shadowText.text = "Тени " + (int)slider.value + ":";
        langDropDown.value = PlayerPrefs.GetInt("Lang", 1);
        sound.value = PlayerPrefs.GetFloat("Sound", 0.5f);
        version.text = Application.version;
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Grap", 0));
        (FindObjectOfType<Volume>().profile.components[1] as MotionBlur).intensity.value = PlayerPrefs.GetInt("Blur", 1) == 0 ? 0 : 0.05f;
        (FindObjectOfType<Volume>().profile.components[2] as HDShadowSettings).maxShadowDistance.value = PlayerPrefs.GetInt("Shadow", 150);
        FindObjectOfType<Lang>().lang = PlayerPrefs.GetInt("Lang", 1);
        AudioListener.volume = PlayerPrefs.GetFloat("Sound", 0.5f); ;
    }
    public void ChangeSound()
    {
        AudioListener.volume = sound.value;
        PlayerPrefs.SetFloat("Sound", sound.value);
    }
    public void ChangeLang()
    {
        FindObjectOfType<Lang>().lang = langDropDown.value;
        PlayerPrefs.SetInt("Lang", langDropDown.value);
    }
    public void BlurOff()
    {
        PlayerPrefs.SetInt("Blur", toggle.isOn == false ? 0 : 1);
        (FindObjectOfType<Volume>().profile.components[1] as MotionBlur).intensity.value = PlayerPrefs.GetInt("Blur", 1) == 0 ? 0 : 0.05f;
    }
    public void ChangeShadow()
    {
        (FindObjectOfType<Volume>().profile.components[2] as HDShadowSettings).maxShadowDistance.value = slider.value;
        PlayerPrefs.SetInt("Shadow", (int)slider.value);
        shadowText.text = shadowText.GetComponent<StaticText>().texts[FindObjectOfType<Lang>().lang] + " " + (int)slider.value + ":";
    }

    public void ShowButtons()
    {
        animator.Play("ButtonShow");
    }
    public void HideButtons()
    {
        animator.Play("ButtonHide");
    }

    public void ShowOptions()
    {
        animatorOptions.Play("Show");
    }
    public void HideOptions()
    {
        animatorOptions.Play("Hide");
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void SetGraphic(TMP_Dropdown dr)
    {
        PlayerPrefs.SetInt("Grap", dr.value);
        QualitySettings.SetQualityLevel(dr.value);
    }
    private void Update()
    {
        Time.timeScale = 1;
    }
    public void Play()
    {
        loading.SetActive(true);
        Application.LoadLevelAsync(1);
    }
}
