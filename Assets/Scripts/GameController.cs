using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * The game model.
 * Holds the current state, but operates without needing to know what the state is.
 * Knows what level/scene is loaded and informs the current state.
 * Holds references to critical items like player object
 */
public class GameController : MonoSingleton<GameController> {

	//The current gamestate, e.g. main menu, gameplay, paused, etc.
	//Shouldn't be directly modified, except by GameState subclasses.
	public GameState currentState;

	public GameState TitleScreenState;
	public GameState GameplayState;
	public GameState LoadingState;

	public GameState nextStateOnLoaded;

    public GameObject smallExplosionPrefab;
    public GameObject mediumEnemyPrefab;
    public GameObject healthGUI;
    public GameObject missilePrefab;
    public GameObject boltPrefab;
    public GameObject enemyBoltPrefab;

	//Navigation/Event Flags
    public bool newGamePressed;
    public bool missileButtonPressed;

	//Instantiate each of the state subclasses. We want to do this once and keep the same copy, every state change just reuses the object.
	private void InstantiateStateClasses(){
        TitleScreenState = new TitleScreenState();
        GameplayState = new GameplayState();
        LoadingState = new LoadingState();
	}

	protected override void Awake() {
        smallExplosionPrefab = (GameObject)Resources.Load("Explosions/SmallExplosion");
        mediumEnemyPrefab = (GameObject)Resources.Load("Enemies/MediumEnemy");
        healthGUI = (GameObject)Resources.Load("GUI/HealthGUI");
        boltPrefab = (GameObject)Resources.Load("Shots/Bolt");
        enemyBoltPrefab = (GameObject)Resources.Load("Shots/EnemyBolt");
        missilePrefab = (GameObject)Resources.Load("Shots/Missile");

        //Base 'MonoSingleton' class will setup this object as `DontDestroyOnLoad`
        base.Awake();
	}

	void Start() {
		Application.targetFrameRate = 60;
		InstantiateStateClasses();
		//If curretState is null, then we're in a pre-initialized state
		if(currentState == null) {
            //This looks up the scene filename and maps it to a game state.
			string lvlname = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
			if(lvlname == "Gameplay") {
				currentState = GameplayState;
			} else if(lvlname == "TitleScreen") {
				currentState = TitleScreenState;
			} else {
				Debug.LogError("Could not find a matching game state to start in for the scene " + lvlname);
			}
			currentState.enterState(this);
		}
	}

    void Update()
    {
        if(currentState != null)
        {
            currentState.doUpdate(this);
        }
    }

    void FixedUpdate()
    {
        if(currentState != null)
        {
            currentState.doFixedUpdate(this);
        }
    }

}