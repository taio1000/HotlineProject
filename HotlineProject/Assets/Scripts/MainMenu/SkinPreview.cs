using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkinPreview : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image bodyImage, headImage, leftArmImage, rightArmImage, lockImage;
    [SerializeField] private GameObject price;
    [SerializeField] private Button rightButton, leftButton, lockButton, okButton;
    [SerializeField] private SkinManager skinManager;
    [SerializeField] private GameDataController datacontroller;

    private void Awake() {
        skinManager = FindObjectOfType<SkinManager>();
    }
    void Start()
    {
        datacontroller = FindObjectOfType<GameDataController>();
        rightButton.onClick.AddListener(skinManager.RigthSwitch);
        leftButton.onClick.AddListener(skinManager.LeftSwitch);
        lockButton.onClick.AddListener(skinManager.BuySkin);
        okButton.onClick.AddListener(skinManager.SelectSkins);
    }

    // Update is called once per frame
    void Update()
    {
        bodyImage.sprite = skinManager.bodySprite[skinManager.index];
        headImage.sprite = skinManager.headSprite[skinManager.index];
        leftArmImage.sprite = skinManager.leftArmSprite[skinManager.index];
        rightArmImage.sprite = skinManager.rightArmSprite[skinManager.index];

        if(!datacontroller.newUnlockedSkins[skinManager.index])
        {
            lockImage.enabled = true;
            price.SetActive(true);
        }
        else
        {
            lockImage.enabled = false;
            price.SetActive(false);
        }
    }
}