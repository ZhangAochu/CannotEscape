using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodedLock : MonoBehaviour
{
    public TMP_Text displayText; // 用于显示输入数字的文本框
    public DialogueData_SO correctDialogue; // 密码正确时的对话数据
    public DialogueData_SO wrongDialogue; // 密码错误时的对话数据
    public string targetScene; // 密码正确时跳转的场景

    private string inputPassword = ""; // 存储用户输入的密码
    private const int passwordLength = 4; // 密码长度

    // 处理数字按钮点击事件
    public void OnNumberButtonClick(string number)
    {
        if (inputPassword.Length < passwordLength)
        {
            inputPassword += number;
            UpdateDisplayText();
        }
    }

    // 处理删除按钮点击事件
    public void OnDeleteButtonClick()
    {
        if (inputPassword.Length > 0)
        {
            inputPassword = inputPassword.Substring(0, inputPassword.Length - 1);
            UpdateDisplayText();
        }
    }

    // 处理确定按钮点击事件
    public void OnConfirmButtonClick()
    {
        if (inputPassword.Length == passwordLength)
        {
            // 验证密码,正确密码是 "2468"
            if (inputPassword == "2468")
            {
                // 密码正确，显示对话并在对话完成后跳转场景
                DialogueController.Instance.ShowDialogueFromItem(correctDialogue, () =>
                {
                    // 触发场景跳转
                    TransitionManager.Instance.Transition("Sickroom Lock", targetScene);
                });
            }
            else
            {
                // 密码错误，显示错误对话
                DialogueController.Instance.ShowDialogueFromItem(wrongDialogue);
            }
            // 清空输入的密码
            inputPassword = "";
            UpdateDisplayText();
        }
    }

    // 更新显示文本
    private void UpdateDisplayText()
    {
        string display = "";
        for (int i = 0; i < inputPassword.Length; i++)
        {
            display += inputPassword[i];
            if (i == 0)
            {
                display += "\t ";
            }
            else if (i == 1)
            {
                display += "\t  "; 
            }
            else if (i == 2)
            {
                display += "\t   ";
            }
        }
        displayText.text = display;
    }
}