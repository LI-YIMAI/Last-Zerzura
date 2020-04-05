using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToGitHub : MonoBehaviour
{
    public string gitHubUML;
    public void goToGit()
    {

        Application.OpenURL(gitHubUML);

    }
}
