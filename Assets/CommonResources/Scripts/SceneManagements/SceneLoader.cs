using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameSceneSO _gameplayScene = default;


    // assign when scene loading
    // then use it as scene loading complete callback...?
    private AsyncOperationHandle<SceneInstance> _loadingOperationHandle;
    private AsyncOperationHandle<SceneInstance> _gameplayManagerLoadingOpHandle;
}
