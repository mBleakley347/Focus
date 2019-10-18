using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Help : MonoBehaviour
{
    public string helpText;

    public void SendText()
    {
        Manager.instance.HelpText(helpText);
    }
}
