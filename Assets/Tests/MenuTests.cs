using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuTest
{
    private GameObject playButton;
    private GameObject instructionsButton;
    private GameObject controlsButton;
    private GameObject settingsButton;
    private GameObject quitButton;
    
    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Menu");
        playButton = GameObject.Find("Canvas - Main Menu/[MENU WINDOW]/MenuButton - Play");
        instructionsButton = GameObject.Find("Canvas - Main Menu/[MENU WINDOW]/MenuButton - Instructions");
        controlsButton = GameObject.Find("Canvas - Main Menu/[MENU WINDOW]/MenuButton - Controls");
        settingsButton = GameObject.Find("Canvas - Main Menu/[MENU WINDOW]/MenuButton - Settings");
        quitButton = GameObject.Find("Canvas - Main Menu/[MENU WINDOW]/MenuButton - Quit");
    }
    
    [Test]
    public void PlayButtonTest()
    {
        /*
         * TODO : play est null
         */
        Button play = playButton.GetComponent<Button>();
        play.onClick.Invoke();
        Debug.Log(SceneManager.GetActiveScene().name);
        Assert.True(SceneManager.GetActiveScene().name == "Main");
    }
    
    [UnityTest]
    public IEnumerator QuitButtonTest()
    {
        Button quit = quitButton.GetComponent<Button>();
        quit.onClick.Invoke();
        // TODO

        yield return null;
    }
}
