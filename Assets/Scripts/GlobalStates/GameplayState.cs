using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayState : GameState
{
    private GameplaySceneManager gpsm;
    private Camera mainCamera;

    private bool playerDrag;
    private PlayerController player;
    private int waveSize;

    public override void enterState(GameController gc, GameState previousState = null)
    {
        Debug.Log("Entered Gameplay state.");
        gpsm = GameObject.Find("GameplaySceneManager").GetComponent<GameplaySceneManager>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        waveSize = 1;
    }

    //Spawn a new medium enemy with a random offset from location 0,0
    private void spawnEnemy(int howMany) {
        for(int i=0;i<howMany;i++) { 
            Vector2 randomOffset = Random.insideUnitCircle * 2;
            Vector3 spawnPoint = randomOffset * 1f;
            GameObject newEmemy = Object.Instantiate(GameController.Instance.mediumEnemyPrefab, spawnPoint, Quaternion.Euler(Random.Range(0, 360), -90, 90));
        }
    }

    public override void doUpdate(GameController gc)
    {
        /* Level management */
        if(GameObject.FindWithTag("Enemy") == null) {
            waveSize++; //It gets harder
            spawnEnemy(waveSize);
        }

        /* Input logic */
        if(Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                PlayerController playerHit = hit.transform.GetComponentInParent<PlayerController>();
                if(playerHit && !player.warpOverheated) {
                    playerDrag = true;
                }
                else
                {
                    Debug.Log("hit non player " + hit.transform.name);
                }

            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            if(playerDrag) {
                //Convert from screen to world space. Assumes gameplay plane is at z=0.
                Vector3 newPos = mainCamera.ScreenToWorldPoint(
                 new Vector3(Input.mousePosition.x,
                 Input.mousePosition.y,
                 -mainCamera.transform.position.z));
                player.transform.position = newPos;
                player.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                playerDrag = false;
                player.warpCooldown();
            }
        }

        /* State change logic */
        if(gpsm.quitButtonPressed)
        {
            gpsm.quitButtonPressed = false;
            gc.nextStateOnLoaded = gc.TitleScreenState;
            changeState(gc.LoadingState, gc);
            SceneManager.LoadScene("TitleScreen");
        }
        gpsm.currentState.doUpdate(gpsm);
    }

    public override void doFixedUpdate(GameController gc)
    {
        gpsm.currentState.doFixedUpdate(gpsm);
    }
}
