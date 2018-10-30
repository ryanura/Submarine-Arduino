using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaraBermain : MonoBehaviour {

    public int jumlahStep;

    public GameObject dialogPanel;
    public Text dialogText;
    public Image gambarNya;
    public string[] dialog;
    public Sprite[] image;
    public int dialogIndex;
    private int m_to;
    private Animator animator;
    private int currentStep;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        ShowDialog(0, dialog.Length - 1);
        ShowDialog(0, image.Length - 1);
	}

    public void ShowDialog(int from, int to)
    {
        m_to = to;
        dialogIndex = from - 1;
        dialogPanel.SetActive(true);
        NextDialog();
    }

    public void MaxDialog()
    {
        if (dialogIndex == m_to)
        {
            dialogPanel.SetActive(false);
        }
        dialogText.text = dialog[dialogIndex];
        gambarNya.sprite = image[dialogIndex];
    }

    public void NextDialog()
    {
        if (dialogIndex >= m_to)
        {
            dialogPanel.SetActive(false);
        }
        dialogIndex += 1;
        dialogText.text = dialog[dialogIndex];
        gambarNya.sprite = image[dialogIndex];
    }

    public void BackDialog()
    {
        if (dialogIndex <= m_to)
        {
            dialogPanel.SetActive(true);
        }
        dialogIndex -= 1;
        dialogText.text = dialog[dialogIndex];
        gambarNya.sprite = image[dialogIndex];
    }

    public void NextStep()
    {
        currentStep += 1;
        if (currentStep < jumlahStep)
        {
            animator.SetTrigger("Next");
            NextDialog();
        }
    }

    public void BeforeStep()
    {
        currentStep -= 1;
        if (currentStep > jumlahStep)
        {
            animator.SetTrigger("Back");
            BackDialog();
        }
    }

    public void MaxStep()
    {
        if (currentStep == jumlahStep)
        {
            MaxDialog();
        }
    }
}
