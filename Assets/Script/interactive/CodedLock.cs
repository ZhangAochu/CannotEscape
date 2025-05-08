using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodedLock : MonoBehaviour
{
    // 4个TMP_Text组件，每个用于显示一位数字
    public TMP_Text digit1;
    public TMP_Text digit2;
    public TMP_Text digit3;
    public TMP_Text digit4;

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
            // 验证密码，正确密码是 "2468"
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
        digit1.text = inputPassword.Length > 0 ? inputPassword[0].ToString() : "";
        digit2.text = inputPassword.Length > 1 ? inputPassword[1].ToString() : "";
        digit3.text = inputPassword.Length > 2 ? inputPassword[2].ToString() : "";
        digit4.text = inputPassword.Length > 3 ? inputPassword[3].ToString() : "";
    }
}
