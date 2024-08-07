﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    const string MUSIC_KEY = "Music";                   //  int 0 or 1
    const string SOUND_KEY = "Sound";                   //  int 0 or 1

    const string MUSIC_SLIDER_KEY = "MusicSlider";      //  float 0 or -80
    const string SOUND_SLIDER_KEY = "SoundSlider";      //  float 0 or -80

    [SerializeField] private float _minValue, _maxValue;  //  the value for the mixer and slider

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;

    [SerializeField] private Toggle _musicCheckBox;
    [SerializeField] private Toggle _soundCheckBox;

    [SerializeField] private AudioMixer _masterMixer;
    private string _musicKey = "Music", _soundKey = "Sound";  //  AudioMixer keys

    private void Start()
    {
        LoadMusic();

        LoadSound();
    }

    private void LoadMusic()
    {
        if (PlayerPrefs.HasKey(MUSIC_KEY))
        {
            if (PlayerPrefs.GetInt(MUSIC_KEY) == 1)
                _musicCheckBox.isOn = true;
            else if (PlayerPrefs.GetInt(MUSIC_KEY) == 0)
                _musicCheckBox.isOn = false;
            else
                Debug.LogError("Player pref Music key has invalid int!");
        }
        else
            PlayerPrefs.SetInt(MUSIC_KEY, 1);

        if (PlayerPrefs.HasKey(MUSIC_SLIDER_KEY))
        {
            _masterMixer.SetFloat(_musicKey, PlayerPrefs.GetFloat(MUSIC_SLIDER_KEY));
            _musicSlider.value = PlayerPrefs.GetFloat(MUSIC_SLIDER_KEY);
        }
        else
        {
            _musicSlider.value = 0;
            PlayerPrefs.SetFloat(MUSIC_SLIDER_KEY, 0);
        }

        if (PlayerPrefs.GetInt(MUSIC_KEY) == 1)
        {
            if (PlayerPrefs.HasKey(MUSIC_SLIDER_KEY))
                _masterMixer.SetFloat(_musicKey, PlayerPrefs.GetFloat(MUSIC_SLIDER_KEY));
            else 
                _masterMixer.SetFloat(_musicKey, _maxValue);
        }
        else if (PlayerPrefs.GetInt(MUSIC_KEY) == 0)
            _masterMixer.SetFloat(_musicKey, _minValue);
    }

    private void LoadSound()
    {
        if (PlayerPrefs.HasKey(SOUND_KEY))
        {
            if (PlayerPrefs.GetInt(SOUND_KEY) == 1)
                _soundCheckBox.isOn = true;
            else if (PlayerPrefs.GetInt(SOUND_KEY) == 0)
                _soundCheckBox.isOn = false;
            else
                Debug.LogError("Player pref Sound key has invalid int!");
        }
        else
            PlayerPrefs.SetInt(SOUND_KEY, 1);


        if (PlayerPrefs.HasKey(SOUND_SLIDER_KEY))
            _soundSlider.value = PlayerPrefs.GetFloat(SOUND_SLIDER_KEY);
        else
        {
            _masterMixer.SetFloat(_soundKey, _maxValue);
            _soundSlider.value = 0;
            PlayerPrefs.SetFloat(SOUND_SLIDER_KEY, 0);
        }

        if (PlayerPrefs.GetInt(SOUND_KEY) == 1)
        {
            if (PlayerPrefs.HasKey(SOUND_SLIDER_KEY))
            {
                _masterMixer.SetFloat(_soundKey, PlayerPrefs.GetFloat(SOUND_SLIDER_KEY));
            }
            else
            {
                _masterMixer.SetFloat(_soundKey, _maxValue);
            }
        }
        else if (PlayerPrefs.GetInt(SOUND_KEY) == 0)
            _masterMixer.SetFloat(_soundKey, _minValue);
    }

    public void SetMusic()
    {
        if (_musicCheckBox.isOn)
        {
            PlayerPrefs.SetInt(MUSIC_KEY, 1);
            _masterMixer.SetFloat(_musicKey, PlayerPrefs.GetFloat(MUSIC_SLIDER_KEY));
        }
        else
        {
            PlayerPrefs.SetInt(MUSIC_KEY, 0);
            _masterMixer.SetFloat(_musicKey, _minValue);
        }
    }

    public void SetSound()
    {
        if (_soundCheckBox.isOn)
        {
            PlayerPrefs.SetInt(SOUND_KEY, 1);
            _masterMixer.SetFloat(_soundKey, PlayerPrefs.GetFloat(SOUND_SLIDER_KEY));
        }
        else
        {
            PlayerPrefs.SetInt(SOUND_KEY, 0);
            _masterMixer.SetFloat(_soundKey, _minValue);
        }
    }

    public void TuneMusic()
    {
        PlayerPrefs.SetFloat(MUSIC_SLIDER_KEY, _musicSlider.value);

        if (PlayerPrefs.GetInt(MUSIC_KEY) == 1)
            _masterMixer.SetFloat(_musicKey, _musicSlider.value);
    }

    public void TuneSound()
    {
        PlayerPrefs.SetFloat(SOUND_SLIDER_KEY, _soundSlider.value);

        if (PlayerPrefs.GetInt(SOUND_KEY) == 1)
            _masterMixer.SetFloat(_soundKey, _soundSlider.value);
    }

}
