using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.VisualScripting;
using UnityEngine;

public class TestRelay : MonoBehaviour
{

    string JoinCode;
    // Start is called before the first frame update
    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }


    private async void CreateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1);

            JoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            Debug.Log("Join Code: " + JoinCode);
        }
        catch (RelayServiceException e)
        {
            Debug.LogException(e);
        }
    }

    private async void JoinRelay()
    {
        try
        {
            await RelayService.Instance.JoinAllocationAsync(JoinCode);

            Debug.Log("Allocation joined " + JoinCode);
        }
        catch (RelayServiceException e)
        {
            Debug.LogException(e);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateRelay();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            JoinRelay();
        }
    }
}
