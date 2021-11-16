using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

    public static class BetterPlayerPrefs
    {
        public static void SetFloat(string prefName, float value)
        {
            PlayerPrefs.SetFloat(prefName, value);
        }
        public static float GetFloat(string prefName)
        {
            return PlayerPrefs.GetFloat(prefName);
        }
        public static void PlusPlusFloat(string prefName)
        {
            PlayerPrefs.SetFloat(prefName, PlayerPrefs.GetFloat(prefName) + 1);
        }
        public static void PlusEqualsFloat(string prefName, float increase)
        {
            PlayerPrefs.SetFloat(prefName, PlayerPrefs.GetFloat(prefName) + increase);
        }
        public static void SetInt(string prefName, int value)
        {
            PlayerPrefs.SetInt(prefName, value);
        }
        public static int GetInt(string prefName)
        {
            return PlayerPrefs.GetInt(prefName);
        }
        public static void PlusPlusInt(string prefName)
        {
            PlayerPrefs.SetInt(prefName, PlayerPrefs.GetInt(prefName) + 1);
        }
        public static void PlusEqualsInt(string prefName, int increase)
        {
            PlayerPrefs.SetInt(prefName, PlayerPrefs.GetInt(prefName) + increase);
        }
        public static void SetBool(string prefName, bool value)
        {
            if(value == true)
            {
                PlayerPrefs.SetInt(prefName, 1);
            }
            else if (value == false)
            {
                PlayerPrefs.SetInt(prefName, 0);
            }
        }
        public static bool GetBool(string prefName)
        {
            if(PlayerPrefs.GetInt(prefName) == 0)
            {
                return false;
            }
            else{
                return true;
            }
        }
        public static void FlipBool(string prefName)
        {
            if(PlayerPrefs.GetInt(prefName) == 0)
            {
                PlayerPrefs.SetInt(prefName, 1);
            }
            else
            {
                PlayerPrefs.SetInt(prefName, 0);
            }
        }
    }