using UnityEngine;
using System.Collections;
enum EnumPosition : int
{
	LEFT_UP = 0,
	UP = 1,
	RIGHT_UP = 2,
	LEFT = 3,
	MIDDLE = 4,
	RIGHT = 5,
	LEFT_DOWN = 6,
	DOWN = 7,
	RIGHT_DOWN = 8,
	MAX = 9,//max = row x column = 9
	ROW = 3,//define 3x3
	COLUMN = 3//define 3x3
};

public class NewBehaviourScript : MonoBehaviour 
{
	//public gameobject of perfab
	public GameObject mGameObjectLeftUp ;
	public GameObject mGameObjectUp ; 
	public GameObject mGameObjectRightUp ;
	public GameObject mGameObjectLeft ;
	public GameObject mGameObjectMiddle ;
	public GameObject mGameObjectRight;
	public GameObject mGameObjectLeftDown ;
	public GameObject mGameObjectDown ;
	public GameObject mGameObjectRightDown ;
	//public gameobject of gameMenu
	public GameObject mGameObjectMenuContinue ;
	public GameObject mGameObjectMenuExit     ;
	public GameObject mGameObjectMenuScore    ;
	public GameObject mGameObjectMenuEasy     ;
	public GameObject mGameObjectMenuHard     ;
	//current gameObject,for move,scale,changeTexture
	protected GameObject mCurrentGameObjectLU ;//LU = LeftUp
	protected GameObject mCurrentGameObjectU  ;//Up
	protected GameObject mCurrentGameObjectRU ;//RU = RightUp
	protected GameObject mCurrentGameObjectL  ;//Left
	protected GameObject mCurrentGameObjectM  ;//Middle 
	protected GameObject mCurrentGameObjectR  ;//Right
	protected GameObject mCurrentGameObjectLD ;//LD = LeftDown
	protected GameObject mCurrentGameObjectD  ;//Down
	protected GameObject mCurrentGameObjectRD ;//RD = RightDown
	//urrent gameObject, for game menu
	protected GameObject mCurrentGameObjectMenuContinue ;
	protected GameObject mCurrentGameObjectMenuEasy ;
	protected GameObject mCurrentGameObjectMenuHard ;
	protected GameObject mCurrentGameObjectMenuExit ;
	protected GameObject mCurrentGameObjectMenuScore;
	//default position
	protected static Vector3 vPositionLU = new Vector3(-2.13f, 2.13f, 0.0f) ;
	protected static Vector3 vPositionU  = new Vector3( 0.00f, 2.13f, 0.0f) ;
	protected static Vector3 vPositionRU = new Vector3( 2.13f, 2.13f, 0.0f) ;
	protected static Vector3 vPositionL  = new Vector3(-2.13f, 0.00f, 0.0f) ;
	protected static Vector3 vPositionM  = new Vector3( 0.00f, 0.00f, 0.0f) ;
	protected static Vector3 vPositionR  = new Vector3( 2.13f, 0.00f, 0.0f) ;
	protected static Vector3 vPositionLD = new Vector3(-2.13f,-2.13f, 0.0f) ;
	protected static Vector3 vPositionD  = new Vector3( 0.00f,-2.13f, 0.0f) ;
	protected static Vector3 vPositionRD = new Vector3( 2.13f,-2.13f, 0.0f) ;
	//default position of gameMenu
 	protected static Vector3 vPositionMenuContinue = new Vector3( -20.0f,-1.0f,0.0f) ;
	protected static Vector3 vPositionMenuExit   = new Vector3( -20.0f,-4.1f,0.0f) ;
	protected static Vector3 vPositionMenuScore = new Vector3( -20.0f,-1.0f,0.0f) ;
	protected static Vector3 vPositionMenuEasy = new Vector3( -20.0f,-2.0f,0.0f) ;
	protected static Vector3 vPositionMenuHard = new Vector3( -20.0f,-3.0f,0.0f) ;
	//change texture by spriteRender 
	protected SpriteRenderer spriteRenderer;
	//sprites
	public Sprite spritesComplete;//原背景
	public Sprite spritesNull;//空背景
	//sprites of gameMenu
	public Sprite spritesContinueCn ;//繼續遊戲
	public Sprite spritesContinueEn ;//contine
	public Sprite spritesExitCn ;//離開
	public Sprite spritesExitEn ;//Exit
	public Sprite spritesEasyCn ;//簡單
	public Sprite spritesEasyEn ;//Easy
	public Sprite spritesHardCn ;//困難
	public Sprite spritesHardEn ;//Hard
	public Sprite spritesStartCn ;//離開
	public Sprite spritesStartEn ;//Exit
	//sprite Array for change texture Nine-grid-puzzle-game
	protected bool  bKancolleSprite = true ;
	public Sprite[] SpritesArray1;
	public Sprite[] SpritesArray2;
	public Sprite[] SpritesArray3;
	public Sprite[] SpritesArray4;
	public Sprite[] SpritesArray5;
	public Sprite[] SpritesArray6;
	//screen scale
	private Vector2 scale ;
	private float baseW = 1024.0f;
	private float baseH =  768.0f;
	//display gameResult(結算畫面)
	protected string sCount = "" ;//temp
	protected int m_iCount = 0 ;//次數
	protected int m_iDifficulty = 0 ;//次數-難度
	protected int m_iGameMode = 1 ;//Easy=1;Hard=6
	protected float fTimerPressed= 0.05f;//temp
	protected float fTimerPassed = 0.0f ;//使用時間
	protected float fRemainTimer = 0.0f ;//剩餘時間=可用時間-使用時間
	protected float fTotalGameTime = 180.0f ;//可用時間
	protected int  miScore = 0 ;//分數
	protected bool bGameOver = false ;//剩餘時間為0時
	protected bool bGameComplete = false ;//完成拼圖
	protected bool bInitialize = false ;//第一次進遊戲 提示玩法
	protected int iGameMenuSelected = 0 ;//gameMenu Selected
	//touch input by android/ios(觸控螢幕,android手機用以取代pc的上下左右)
	protected bool bTouchInput = true ;//flag
	protected string sTouch = "" ;//Touched GameObject name
	protected UnityCallAndroid pMyAndroid ; 
	void Awake()
	{
		// load all frames in Sprites array		
	//	SpritesArray = Resources.LoadAll<Sprite>("36");//無效?
	//	Sprite sprite = new Sprite();
	//	sprite = Sprite.Create(www.texture, new Rect(0, 0, 170, 170),new Vector2(0, 0),100.0f);
	}
	// Use this for initialization
	void Start () 
	{
		//default Instantiate(clone gameobject)
		mCurrentGameObjectLU= GameObject.Instantiate(mGameObjectLeftUp,vPositionLU,transform.rotation) as GameObject ;
		mCurrentGameObjectU = GameObject.Instantiate(mGameObjectUp,vPositionU,transform.rotation) as GameObject ;
		mCurrentGameObjectRU= GameObject.Instantiate(mGameObjectRightUp,vPositionRU,transform.rotation) as GameObject ;
		mCurrentGameObjectL = GameObject.Instantiate(mGameObjectLeft,vPositionL,transform.rotation) as GameObject ;
		mCurrentGameObjectM = GameObject.Instantiate(mGameObjectMiddle,vPositionM,transform.rotation) as GameObject ;
		mCurrentGameObjectR = GameObject.Instantiate(mGameObjectRight,vPositionR,transform.rotation) as GameObject ;
		mCurrentGameObjectLD= GameObject.Instantiate(mGameObjectLeftDown,vPositionLD,transform.rotation) as GameObject ;
		mCurrentGameObjectD = GameObject.Instantiate(mGameObjectDown,vPositionD,transform.rotation) as GameObject ;
		mCurrentGameObjectRD= GameObject.Instantiate(mGameObjectRightDown,vPositionRD,transform.rotation) as GameObject ;
		mCurrentGameObjectMenuContinue = GameObject.Instantiate(mGameObjectMenuContinue,vPositionMenuContinue,transform.rotation) as GameObject ;
		mCurrentGameObjectMenuExit     = GameObject.Instantiate(mGameObjectMenuExit,vPositionMenuExit,transform.rotation) as GameObject ;
		mCurrentGameObjectMenuScore    = GameObject.Instantiate(mGameObjectMenuScore,vPositionMenuScore,transform.rotation) as GameObject ;
		mCurrentGameObjectMenuEasy     = GameObject.Instantiate(mGameObjectMenuEasy,vPositionMenuEasy,transform.rotation) as GameObject ;
		mCurrentGameObjectMenuHard     = GameObject.Instantiate(mGameObjectMenuHard,vPositionMenuHard,transform.rotation) as GameObject ;
		//default scale of gameobject
		mCurrentGameObjectLU.transform.localScale = new Vector3(1.0f , 1.0f , 1.0f);
		mCurrentGameObjectU.transform.localScale  = new Vector3(1.0f , 1.0f , 1.0f);
		mCurrentGameObjectRU.transform.localScale = new Vector3(1.0f , 1.0f , 1.0f);
		mCurrentGameObjectL.transform.localScale  = new Vector3(1.0f , 1.0f , 1.0f);
		mCurrentGameObjectM.transform.localScale  = new Vector3(1.0f , 1.0f , 1.0f);
		mCurrentGameObjectR.transform.localScale  = new Vector3(1.0f , 1.0f , 1.0f);
		mCurrentGameObjectLD.transform.localScale = new Vector3(1.0f , 1.0f , 1.0f);
		mCurrentGameObjectD.transform.localScale  = new Vector3(1.0f , 1.0f , 1.0f);
		mCurrentGameObjectRD.transform.localScale = new Vector3(1.0f , 1.0f , 1.0f);
		mCurrentGameObjectMenuContinue.transform.localScale = new Vector3(1.0f , 1.0f , 1.0f);
		mCurrentGameObjectMenuExit.transform.localScale = new Vector3(1.0f , 1.0f , 1.0f);
		mCurrentGameObjectMenuScore.transform.localScale = new Vector3(1.0f, 1.5f , 1.0f);
		mCurrentGameObjectMenuEasy.transform.localScale = new Vector3(1.0f , 1.0f , 1.0f);
		mCurrentGameObjectMenuHard.transform.localScale = new Vector3(1.0f , 1.0f , 1.0f);
		// tips: this.transform = "Main Camera"
		pMyAndroid = this.transform.GetComponent<UnityCallAndroid>();
		//game start
		ResetGame() ;
	}
	// Update is called once per frame
	void Update () 
	{
		//觸控輸入
		if ((bGameOver == false) && (bGameComplete == false) && (bTouchInput == true)) TouchUpdate() ;
		//
		if ((bGameOver == true) || (bGameComplete == true) || (bInitialize == true))
		{
			if (Input.GetButtonDown("left"))
			{
				iGameMenuSelected-- ; 
			}
			else if (Input.GetButtonDown("down"))
			{
				iGameMenuSelected++ ;
			}
			else if (Input.GetButtonDown("right"))
			{
				iGameMenuSelected++ ;
			}
			else if (Input.GetButtonDown("up"))
			{
				iGameMenuSelected-- ;
			}
			if (iGameMenuSelected < 0) iGameMenuSelected = 0 ;
			if (iGameMenuSelected > 2) iGameMenuSelected = 2 ;
			//
			if (iGameMenuSelected == 2)
			{
				spriteRenderer = mCurrentGameObjectMenuContinue.GetComponent<SpriteRenderer>() ;
				spriteRenderer.sprite = spritesContinueEn ;
				if (bInitialize == true) spriteRenderer.sprite = spritesStartEn ;//SpritesArray[0] ;//test
				spriteRenderer = mCurrentGameObjectMenuEasy.GetComponent<SpriteRenderer>() ;
				spriteRenderer.sprite = spritesEasyEn ;
				spriteRenderer = mCurrentGameObjectMenuHard.GetComponent<SpriteRenderer>() ;
				spriteRenderer.sprite = spritesHardEn ;
				spriteRenderer = mCurrentGameObjectMenuExit.GetComponent<SpriteRenderer>() ;
				spriteRenderer.sprite = spritesExitCn ;
			}
			else 
			{
				spriteRenderer = mCurrentGameObjectMenuContinue.GetComponent<SpriteRenderer>() ;
				spriteRenderer.sprite = spritesContinueCn ;
				if (bInitialize == true) spriteRenderer.sprite = spritesStartCn ;
				spriteRenderer = mCurrentGameObjectMenuExit.GetComponent<SpriteRenderer>() ;
				spriteRenderer.sprite = spritesExitEn ;
				if (iGameMenuSelected == 0)
				{
					spriteRenderer = mCurrentGameObjectMenuEasy.GetComponent<SpriteRenderer>() ;
					spriteRenderer.sprite = spritesEasyCn ;
					spriteRenderer = mCurrentGameObjectMenuHard.GetComponent<SpriteRenderer>() ;
					spriteRenderer.sprite = spritesHardEn ;
					m_iGameMode = 1 ;
					fTotalGameTime = 180.0f ;
				}
				else
				{
					spriteRenderer = mCurrentGameObjectMenuEasy.GetComponent<SpriteRenderer>() ;
					spriteRenderer.sprite = spritesEasyEn ;
					spriteRenderer = mCurrentGameObjectMenuHard.GetComponent<SpriteRenderer>() ;
					spriteRenderer.sprite = spritesHardCn ;
					m_iGameMode = 6 ;
					fTotalGameTime = 60.0f ;
				}
			}
			//
			if (Input.GetButtonDown("return"))
			{
				if (iGameMenuSelected == 2)
					QuitGame() ;
				else 
				{
					if (bInitialize == true) bInitialize = false ;
					ResetGame() ;
				}
				
			}		
			return ;
		}
		else
		{
			// key down delay
			//		if (Time.time < fNextPressed) return ;
			fTimerPassed = Time.time - fTimerPressed;
			fRemainTimer = fTotalGameTime - fTimerPassed ;
			if (fRemainTimer <= 0.0f)
			{ 
				bGameOver = true ; 
				EnableGameMenu(bGameOver) ;
				miScore = 0 ;
			}
			//
			if (Input.GetAxis("Vertical") > 0.2)
			{
				
			}
			else if (Input.GetAxis ("Horizontal") > 0.2)
			{
				
			}
			else if (Input.GetButtonDown("left"))
			{
				MoveNullTile((int)EnumPosition.RIGHT) ;
			}
			else if (Input.GetButtonDown("down"))
			{
				MoveNullTile((int)EnumPosition.UP) ;
			}
			else if (Input.GetButtonDown("right"))
			{
				MoveNullTile((int)EnumPosition.LEFT) ;
			}
			else if (Input.GetButtonDown("up"))
			{
				MoveNullTile((int)EnumPosition.DOWN) ;
			}
			//check if game complete
			bGameComplete = IsGameComplete() ;
			if (bGameComplete)
			{
				miScore = (int)(fRemainTimer * 10 * m_iGameMode) - m_iCount ;
				if (miScore<0) miScore = 0 ;
			}
		}
	}
	//
	void OnGUI()
	{
		scale = new Vector2(Screen.width/baseW,Screen.height/baseH);		
		GUIUtility.ScaleAroundPivot(scale , Vector2.zero);
		//
		GUIStyle gsLabel = new GUIStyle();
		//check if game complete
		if (bGameComplete)
		{
			EnableGameMenu(bGameComplete) ;
			sCount = "Congratulation!!" ;
			gsLabel.normal.textColor = Color.magenta ;
			gsLabel.fontSize = 36 ;
			//change complete texture by spriteRender
			spriteRenderer = mCurrentGameObjectLU.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = spritesComplete ;
			//
			GUI.Label(new Rect(280,160,220,80),sCount,gsLabel) ;
			sCount = "結算畫面:" ;
			GUI.Label(new Rect(280,210,220,80),sCount,gsLabel) ;
			string stringstr = (fTimerPassed).ToString("##0.0");
			sCount = "使用秒數:" + stringstr + "秒."  ;
			GUI.Label(new Rect(280,260,220,80),sCount,gsLabel) ;
			sCount = "推圖步數:" + m_iCount + "步."  ;
			GUI.Label(new Rect(280,310,220,80),sCount,gsLabel) ;
			sCount = "得分:" + miScore + "分."  ;
			GUI.Label(new Rect(280,360,220,80),sCount,gsLabel) ;		
//		    pMyAndroid.UnityCallAndroidToast("成功了!") ;
			pMyAndroid.UnityCallAndroidAlert("完成拼圖!" , "再玩一次?" , "確定" , "離開") ;
		}
		else if (bInitialize == true)
		{
			sCount = "九宮格拼圖" ;
			gsLabel.fontSize = 24 ;
			gsLabel.normal.textColor = Color.black ;
			GUI.Label(new Rect(280,165,220,80),sCount,gsLabel) ;
			sCount = "玩法:" ;
			GUI.Label(new Rect(280,215,220,80),sCount,gsLabel) ;
			sCount = "圖塊分九格，一格為空格，每次移動一格，" ;
			GUI.Label(new Rect(280,265,220,80),sCount,gsLabel) ;
			sCount = "上下左右圖案都必須與緊鄰的圖案接合。" ;
			GUI.Label(new Rect(280,315,220,80),sCount,gsLabel) ;
			sCount = "按鍵:上、下、左、右。"  ;
			GUI.Label(new Rect(280,365,220,80),sCount,gsLabel) ;
		}
		else
		{
			//check if game over
			if (bGameOver == true)
			{
				sCount = "Timeout!" ;
				gsLabel.fontSize = 36 ;
				gsLabel.normal.textColor = Color.red ;
				GUI.Label(new Rect(280,160,220,80),sCount,gsLabel) ;
				sCount = "結算畫面:" ;
				GUI.Label(new Rect(280,210,220,80),sCount,gsLabel) ;
				string stringstr = (fTimerPassed).ToString("##0.0");
				sCount = "使用秒數:" + stringstr + "秒."  ;
				GUI.Label(new Rect(280,260,220,80),sCount,gsLabel) ;
				sCount = "推圖步數:" + m_iCount + "步."  ;
				GUI.Label(new Rect(280,310,220,80),sCount,gsLabel) ;
				sCount = "得分:" + miScore + "分."  ;
				GUI.Label(new Rect(280,360,220,80),sCount,gsLabel) ;
//			  	pMyAndroid.UnityCallAndroidToast("失敗了!") ;
				pMyAndroid.UnityCallAndroidAlert("時間用完了!" , "再試一次?" , "確定" , "離開") ;
			}
			else
			{
				string stringstr = (fRemainTimer).ToString("##0.0"); //result: 567.8
				sCount = "" + stringstr + "秒" ;
				gsLabel.fontSize = 24 ;
				gsLabel.normal.textColor = Color.black ;
				GUI.Label(new Rect(3,10,220,80),sCount,gsLabel) ;
			}
		}
/*		//Debug message
		if (bTouchInput)
		{
			gsLabel.fontSize = 24 ; 
			gsLabel.normal.textColor = Color.black ;
			GUI.Label(new Rect(3,3,220,80),sTouch,gsLabel) ;
		}
*/		
	}
	//
	void FixedUpdate() 
	{
        //Raycast does not detect 2D colliders , only 3D
        //OnMouseDown() Does detect 2D colliders
/*		無效果
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        if (hit != null)
        {
            sTouch = hit.collider.name + "TEST1";
        }
*/	
		//有效!
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitObject = Physics2D.Raycast(tapPoint, -Vector2.up);
            if (hitObject)
            {
                sTouch = hitObject.collider.name + "TEST2";
            }
        }
/*		無效果
        //屏幕空間到世界空間的變化位置
        //因為屏幕 Z 坐標的默認值是0，所以需要一個z坐標
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)//觸控按鈕 - 按住
		{
			sTouch = "" ;
			Vector3 mousePosition ;
			mousePosition.x = Input.GetTouch(0).position.x ;
			mousePosition.y = Input.GetTouch(0).position.y ;
			mousePosition.z = camera.nearClipPlane ;
			Vector3 v3 = Camera.main.ScreenToWorldPoint(mousePosition);
        	RaycastHit2D hit3 = Physics2D.Raycast(v3, Vector2.zero);
        	if (hit3.collider != null)
        	{
            	sTouch = hit3.collider.name + "TEST3";
        	}
		}
*/
	}
	//觸碰螢幕 Android/iOS,PC會觸發MouseDown
	public void TouchUpdate()
	{
/*		無效果
		//在PC/Mac用滑鼠點擊測試
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit ;
			if (Physics.Raycast(ray, out hit))
			{
				sTouch = hit.collider.name + "DebugTouch0" ;
			}
		}
*/		
/*		無效果
		Vector3 mousePositionEx ;
		mousePositionEx.x = Input.mousePosition.x ;
		mousePositionEx.y = Input.mousePosition.y ;
		mousePositionEx.z = Mathf.Infinity;
		Vector3 v3Ex = mousePositionEx - Camera.main.ScreenToWorldPoint(mousePositionEx) ;
		RaycastHit2D hitEx = Physics2D.Raycast(mousePositionEx , v3Ex ,Mathf.Infinity);
		if(hitEx)				
		{
			sTouch = hitEx.collider.name + hitEx.collider.gameObject.name + "DebugTouch1" ;
		}
*/		
		//有效!
		RaycastHit2D hitII = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);	
		if(hitII.collider != null)			
		{
			//呃..是手機時,會一直觸發...
/*
			sTouch = hitII.collider.name ;
			int iIndex = FindTileByGameObjectName(sTouch) ;
			MoveTargetTile(iIndex) ;
*/		}
		//以下script加在Camera下面 
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)//觸控按鈕 - 移出	
		{
			sTouch = "" ;
		}
		else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)//觸控按鈕 - 按住
		{
			sTouch = "" ;
			Vector3 mousePosition ;
			mousePosition.x = Input.GetTouch(0).position.x ;
			mousePosition.y = Input.GetTouch(0).position.y ;
			mousePosition.z = camera.nearClipPlane ; //Mathf.Infinity;
			RaycastHit2D hitIII = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);//-Vector2.up);
			if(hitIII.collider != null)			
			{
				//Ray has been Cast and hit an Object
				sTouch = hitIII.collider.name;
				int iIndex = FindTileByGameObjectName(sTouch) ;
				MoveTargetTile(iIndex) ;
			}
		}
		else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)//觸控按鈕 - 滑動
		{
			sTouch = "" ;
			//目標物件必需要有collider
			//3D RayCast
/*			無效果
  			Ray ray = camera.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit hit ;
			if (Physics.Raycast(ray, out hit))
			{
				//依物件名稱作不同的點擊判定
				if(hit.collider.gameObject.name == "NewSpriteLU(Clone)")
					sTouch = "TouchMove" + hit.collider.name ;
				else if(hit.collider.gameObject.name == "NewSpriteLD(Clone)")
					sTouch = "TouchMove" + Input.GetTouch(0).position.x + "," + Input.GetTouch(0).position.y ;
				else if(hit.collider.gameObject.name == "GameMenuExit(Clone)")
					QuitGame() ;
				else
					sTouch = "TouchMove" + hit.collider.gameObject.name ;
			}
*/		}
/*
		//多點觸碰
		foreach (Touch touch in Input.touches)
		{			
			if (touch.phase == TouchPhase.Began)
			{
			}
			else if (touch.phase == TouchPhase.Moved)
			{
			}
			else if (touch.phase == TouchPhase.Ended)
			{
			}
		}	
*/		
	}
	//Move TargetTile to Null
	public bool MoveTargetTile(int iTarget)
	{	
		int iDirection = FindNeighborTile(iTarget) ;
		if (iDirection < 0) return false;
		//
		return MoveNullTile(iDirection) ;
	}
	//Move NullTile to Direction , Move DestinationTile to Ani-Direction 
	public bool MoveNullTile(int iDirection)
	{
		//position
		int iWhite = FindTilePosition() ;
		int iTarget= 0 ;
		//check if move outside
		if (iDirection == (int)EnumPosition.LEFT)
		{
			if ((iWhite == (int)EnumPosition.LEFT) || (iWhite == (int)EnumPosition.LEFT_UP) || (iWhite == (int)EnumPosition.LEFT_DOWN))
				return false ;
			iTarget = (iWhite-1) ;
		}
		else if (iDirection == (int)EnumPosition.RIGHT)
		{
			if ((iWhite == (int)EnumPosition.RIGHT) || (iWhite == (int)EnumPosition.RIGHT_UP) || (iWhite == (int)EnumPosition.RIGHT_DOWN))
				return false ;
			iTarget = (iWhite+1) ;
		}
		else if (iDirection == (int)EnumPosition.UP)
		{
			if ((iWhite == (int)EnumPosition.LEFT_UP) || (iWhite == (int)EnumPosition.RIGHT_UP) || (iWhite == (int)EnumPosition.UP))
				return false ;
			iTarget = (iWhite-(int)EnumPosition.ROW) ;
		}
		else if (iDirection == (int)EnumPosition.DOWN)
		{
			if ((iWhite == (int)EnumPosition.LEFT_DOWN) || (iWhite == (int)EnumPosition.RIGHT_DOWN) || (iWhite == (int)EnumPosition.DOWN))
				return false;
			iTarget = (iWhite+(int)EnumPosition.ROW) ;
		}
		else
			return false ;//目前不支持斜線交換
		//moving..
		int iSrc = FindGameObjectByTarget(iWhite) ;
		int iDes = FindGameObjectByTarget(iTarget);
		//swap them
		MovePosition(iSrc,iTarget) ;//swap white to target
		MovePosition(iDes,iWhite) ;//swap target to white
		//Move Done!
		m_iCount++ ;
		return true ;
	}
	//
	public void MovePosition(int iSourceTile,int iDestinationTile)
	{
		Vector3 vPos = new Vector3(0.0f , 0.0f , 0.0f) ;
		switch (iDestinationTile)
		{
		case ((int)EnumPosition.LEFT_UP) : vPos = vPositionLU ; break ;
		case ((int)EnumPosition.UP) : vPos = vPositionU ; break ;
		case ((int)EnumPosition.RIGHT_UP) : vPos = vPositionRU ; break ;
		case ((int)EnumPosition.LEFT) : vPos = vPositionL ; break ;
		case ((int)EnumPosition.MIDDLE) : vPos = vPositionM ; break ;
		case ((int)EnumPosition.RIGHT) : vPos = vPositionR ; break ;
		case ((int)EnumPosition.LEFT_DOWN) : vPos = vPositionLD ; break ;
		case ((int)EnumPosition.DOWN) : vPos = vPositionD ; break ;
		case ((int)EnumPosition.RIGHT_DOWN) : vPos = vPositionRD ; break ;
		default: return ;
		}
		//
		switch (iSourceTile)
		{
		case ((int)EnumPosition.LEFT_UP) : mCurrentGameObjectLU.transform.localPosition = vPos ; break ;
		case ((int)EnumPosition.UP) : mCurrentGameObjectU.transform.localPosition = vPos ; break ;
		case ((int)EnumPosition.RIGHT_UP) : mCurrentGameObjectRU.transform.localPosition = vPos ; break ;
		case ((int)EnumPosition.LEFT) : mCurrentGameObjectL.transform.localPosition = vPos ; break ;
		case ((int)EnumPosition.MIDDLE) : mCurrentGameObjectM.transform.localPosition = vPos ; break ;
		case ((int)EnumPosition.RIGHT) : mCurrentGameObjectR.transform.localPosition = vPos ; break ;
		case ((int)EnumPosition.LEFT_DOWN) : mCurrentGameObjectLD.transform.localPosition = vPos ; break ;
		case ((int)EnumPosition.DOWN) : mCurrentGameObjectD.transform.localPosition = vPos ; break ;
		case ((int)EnumPosition.RIGHT_DOWN) : mCurrentGameObjectRD.transform.localPosition = vPos ; break ;
		default: return ;
		}
		//
		setTilePosition( iSourceTile , iDestinationTile) ;
	}
	//find the white tile and return (enum)position
	public int FindTilePosition()
	{
		return ( getTilePosition((int)EnumPosition.LEFT_UP) ) ;//簡易版都規定左上角為空
	}
	//find traget'up,left,right,down if is null
	public int FindNeighborTile(int iTarget)
	{
		int iWhere = getTilePosition( iTarget ) ;
		int iWhite = FindTilePosition() ;
		//
			 if ((iWhere-1) == iWhite) return (int)EnumPosition.RIGHT ;
		else if ((iWhere+1) == iWhite) return (int)EnumPosition.LEFT ;
		else if ((iWhere+(int)EnumPosition.ROW) == iWhite) return (int)EnumPosition.UP ;
		else if ((iWhere-(int)EnumPosition.ROW) == iWhite) return (int)EnumPosition.DOWN ;
		else
			return -1 ;
	}
	//
	public int FindTileByGameObjectName(string sObjectName)
	{
			 if (string.Compare(sObjectName, "NewSpriteLU(Clone)" , true) == 0) return (int)EnumPosition.LEFT_UP;
		else if (string.Compare(sObjectName, "NewSpriteU(Clone)" , true)  == 0) return (int)EnumPosition.UP;
		else if (string.Compare(sObjectName, "NewSpriteRU(Clone)" , true) == 0) return (int)EnumPosition.RIGHT_UP;
		else if (string.Compare(sObjectName, "NewSpriteL(Clone)" , true)  == 0) return (int)EnumPosition.LEFT;
		else if (string.Compare(sObjectName, "NewSpriteM(Clone)" , true)  == 0) return (int)EnumPosition.MIDDLE;
		else if (string.Compare(sObjectName, "NewSpriteR(Clone)" , true)  == 0) return (int)EnumPosition.RIGHT;
		else if (string.Compare(sObjectName, "NewSpriteLD(Clone)" , true) == 0) return (int)EnumPosition.LEFT_DOWN;
		else if (string.Compare(sObjectName, "NewSpriteD(Clone)" , true)  == 0) return (int)EnumPosition.DOWN;
		else if (string.Compare(sObjectName, "NewSpriteRD(Clone)" , true) == 0) return (int)EnumPosition.RIGHT_DOWN;
		else return -1 ;
	}
	//
	public int FindGameObjectByTarget(int iTarget)
	{
		if (iTarget == getTilePosition( (int)EnumPosition.LEFT_UP) ) return (int)EnumPosition.LEFT_UP;
		else if (iTarget == getTilePosition( (int)EnumPosition.UP) ) return (int)EnumPosition.UP ;
		else if (iTarget == getTilePosition( (int)EnumPosition.RIGHT_UP) ) return (int)EnumPosition.RIGHT_UP ;
		else if (iTarget == getTilePosition( (int)EnumPosition.LEFT) ) return (int)EnumPosition.LEFT ;
		else if (iTarget == getTilePosition( (int)EnumPosition.MIDDLE) ) return (int)EnumPosition.MIDDLE ;
		else if (iTarget == getTilePosition( (int)EnumPosition.RIGHT) ) return (int)EnumPosition.RIGHT ;
		else if (iTarget == getTilePosition( (int)EnumPosition.LEFT_DOWN) ) return (int)EnumPosition.LEFT_DOWN ;
		else if (iTarget == getTilePosition( (int)EnumPosition.DOWN) ) return (int)EnumPosition.DOWN ;
		else if (iTarget == getTilePosition( (int)EnumPosition.RIGHT_DOWN)) return (int)EnumPosition.RIGHT_DOWN ;
		else return -1 ;
	}
	//check if game finish
	public bool IsGameComplete()
	{
		bool bFinish = true ;
		if (((int)EnumPosition.LEFT_UP) != getTilePosition( (int)EnumPosition.LEFT_UP) ) bFinish = false ;
		else if (((int)EnumPosition.UP) != getTilePosition( (int)EnumPosition.UP) ) bFinish = false ;
		else if (((int)EnumPosition.RIGHT_UP) != getTilePosition( (int)EnumPosition.RIGHT_UP) ) bFinish = false ;
		else if (((int)EnumPosition.LEFT) != getTilePosition( (int)EnumPosition.LEFT) ) bFinish = false ;
		else if (((int)EnumPosition.MIDDLE) != getTilePosition( (int)EnumPosition.MIDDLE) ) bFinish = false ;
		else if (((int)EnumPosition.RIGHT) != getTilePosition( (int)EnumPosition.RIGHT) ) bFinish = false ;
		else if (((int)EnumPosition.LEFT_DOWN) != getTilePosition( (int)EnumPosition.LEFT_DOWN) ) bFinish = false ;
		else if (((int)EnumPosition.DOWN) != getTilePosition( (int)EnumPosition.DOWN) ) bFinish = false ;
		else if (((int)EnumPosition.RIGHT_DOWN) != getTilePosition( (int)EnumPosition.RIGHT_DOWN)) bFinish = false ;
		return bFinish ;
	}
	//
	public void RandomWhite()
	{
		setTilePosition( (int)EnumPosition.LEFT_UP , (int)EnumPosition.LEFT_UP) ;//簡易版都規定左上角為空
	}
	//
	public void RandomTile()
	{
		Random.seed = System.Guid.NewGuid().GetHashCode();
		m_iDifficulty = Random.Range(100,256) ;//產生1個100~255間的值
		bool bValid = false;
		for (int i = 0 ; i < m_iDifficulty ; /*skip*/)
		{
			int iDes = Random.Range(0,(int)EnumPosition.MAX) ;
			bValid = false;//reset
			bValid = MoveNullTile(iDes);
			if (bValid) i++ ;
		}
	}
	//random change image of game,隨機換底圖
	public void RandomImage()
	{
		int iRandom = Random.Range(0,3) ;//random 0~2
		if (bKancolleSprite == false)
		{
			//change array1 texture,換陣列1的圖
			if (iRandom == 0)
			{
			spriteRenderer = mCurrentGameObjectLU.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray1[0] ;
			spritesComplete = SpritesArray1[0] ;
			spriteRenderer = mCurrentGameObjectU.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray1[1] ;
			spriteRenderer = mCurrentGameObjectRU.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray1[2] ;
			spriteRenderer = mCurrentGameObjectL.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray1[3] ;
			spriteRenderer = mCurrentGameObjectM.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray1[4] ;
			spriteRenderer = mCurrentGameObjectR.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray1[5] ;
			spriteRenderer = mCurrentGameObjectLD.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray1[6] ;
			spriteRenderer = mCurrentGameObjectD.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray1[7] ;
			spriteRenderer = mCurrentGameObjectRD.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray1[8] ;
			}
			//change array2 texture,換陣列2的圖
			else if (iRandom == 1)
			{
			spriteRenderer = mCurrentGameObjectLU.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray2[0] ;
			spritesComplete = SpritesArray2[0] ;
			spriteRenderer = mCurrentGameObjectU.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray2[1] ;
			spriteRenderer = mCurrentGameObjectRU.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray2[2] ;
			spriteRenderer = mCurrentGameObjectL.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray2[3] ;
			spriteRenderer = mCurrentGameObjectM.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray2[4] ;
			spriteRenderer = mCurrentGameObjectR.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray2[5] ;
			spriteRenderer = mCurrentGameObjectLD.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray2[6] ;
			spriteRenderer = mCurrentGameObjectD.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray2[7] ;
			spriteRenderer = mCurrentGameObjectRD.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray2[8] ;
			}
			//change array3 texture,換陣列3的圖
			else
			{
			spriteRenderer = mCurrentGameObjectLU.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray3[0] ;
			spritesComplete = SpritesArray3[0] ;
			spriteRenderer = mCurrentGameObjectU.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray3[1] ;
			spriteRenderer = mCurrentGameObjectRU.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray3[2] ;
			spriteRenderer = mCurrentGameObjectL.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray3[3] ;
			spriteRenderer = mCurrentGameObjectM.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray3[4] ;
			spriteRenderer = mCurrentGameObjectR.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray3[5] ;
			spriteRenderer = mCurrentGameObjectLD.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray3[6] ;
			spriteRenderer = mCurrentGameObjectD.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray3[7] ;
			spriteRenderer = mCurrentGameObjectRD.GetComponent<SpriteRenderer>() ;
			spriteRenderer.sprite = SpritesArray3[8] ;
			}
		}
		else
		{
			if (iRandom == 0) 
			{
				spriteRenderer = mCurrentGameObjectLU.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray4 [0];
				spritesComplete = SpritesArray4 [0];
				spriteRenderer = mCurrentGameObjectU.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray4 [1];
				spriteRenderer = mCurrentGameObjectRU.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray4 [2];
				spriteRenderer = mCurrentGameObjectL.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray4 [3];
				spriteRenderer = mCurrentGameObjectM.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray4 [4];
				spriteRenderer = mCurrentGameObjectR.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray4 [5];
				spriteRenderer = mCurrentGameObjectLD.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray4 [6];
				spriteRenderer = mCurrentGameObjectD.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray4 [7];
				spriteRenderer = mCurrentGameObjectRD.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray4 [8];
			}
			//change array2 texture,換陣列2的圖
			else if (iRandom == 1) 
			{
				spriteRenderer = mCurrentGameObjectLU.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray5 [0];
				spritesComplete = SpritesArray5 [0];
				spriteRenderer = mCurrentGameObjectU.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray5 [1];
				spriteRenderer = mCurrentGameObjectRU.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray5 [2];
				spriteRenderer = mCurrentGameObjectL.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray5 [3];
				spriteRenderer = mCurrentGameObjectM.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray5 [4];
				spriteRenderer = mCurrentGameObjectR.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray5 [5];
				spriteRenderer = mCurrentGameObjectLD.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray5 [6];
				spriteRenderer = mCurrentGameObjectD.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray5 [7];
				spriteRenderer = mCurrentGameObjectRD.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray5 [8];
			}
			//change array3 texture,換陣列3的圖
			else
			{
				spriteRenderer = mCurrentGameObjectLU.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray6 [0];
				spritesComplete = SpritesArray6 [0];
				spriteRenderer = mCurrentGameObjectU.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray6 [1];
				spriteRenderer = mCurrentGameObjectRU.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray6 [2];
				spriteRenderer = mCurrentGameObjectL.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray6 [3];
				spriteRenderer = mCurrentGameObjectM.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray6 [4];
				spriteRenderer = mCurrentGameObjectR.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray6 [5];
				spriteRenderer = mCurrentGameObjectLD.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray6 [6];
				spriteRenderer = mCurrentGameObjectD.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray6 [7];
				spriteRenderer = mCurrentGameObjectRD.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = SpritesArray6 [8];
			}
		}
	}
	//Reset
	public void ResetGame()
	{
		//reset difficulty
		m_iDifficulty = 0 ;
		//reset position
		ResetPosition() ;
		//random game
		RandomWhite() ;
		RandomTile()  ;
		RandomImage() ;
		//reset counter
		m_iCount = 0 ;//need after RandomTile()
		//reset game ,change texture
		spriteRenderer = mCurrentGameObjectLU.GetComponent<SpriteRenderer>() ;
		spriteRenderer.sprite = spritesNull ;
		//reset gamemenu sprites
		spriteRenderer = mCurrentGameObjectMenuContinue.GetComponent<SpriteRenderer>() ;
		spriteRenderer.sprite = spritesContinueEn ;
		if (bInitialize == true) spriteRenderer.sprite = spritesStartEn ;
		spriteRenderer = mCurrentGameObjectMenuExit.GetComponent<SpriteRenderer>() ;
		spriteRenderer.sprite = spritesExitEn ;
		spriteRenderer = mCurrentGameObjectMenuEasy.GetComponent<SpriteRenderer>() ;
		spriteRenderer.sprite = spritesEasyEn ;
		spriteRenderer = mCurrentGameObjectMenuHard.GetComponent<SpriteRenderer>() ;
		spriteRenderer.sprite = spritesHardEn ;
		//reset timer
		fTimerPressed = Time.time ;
		m_iGameMode = 1 ;
		fTimerPassed = 0.0f ;
		fRemainTimer = 0.0f ;
		//reset flag
		bGameOver = false ;
		bGameComplete = false ;
		EnableGameMenu(bGameOver) ;
		//reset alert and toast flag
		pMyAndroid.reset() ;
		//init
		if (bInitialize == true) EnableGameMenu(bInitialize) ;
	}
	//
	public void ResetPosition()
	{
		//reset scriptobject TilePosition
		setTilePosition( (int)EnumPosition.LEFT_UP , (int)EnumPosition.LEFT_UP) ;
		setTilePosition( (int)EnumPosition.UP , (int)EnumPosition.UP );
		setTilePosition( (int)EnumPosition.RIGHT_UP ,(int)EnumPosition.RIGHT_UP);
		setTilePosition( (int)EnumPosition.LEFT , (int)EnumPosition.LEFT) ;
		setTilePosition( (int)EnumPosition.MIDDLE , (int)EnumPosition.MIDDLE) ;
		setTilePosition( (int)EnumPosition.RIGHT , (int)EnumPosition.RIGHT) ;
		setTilePosition( (int)EnumPosition.LEFT_DOWN , (int)EnumPosition.LEFT_DOWN) ;
		setTilePosition( (int)EnumPosition.DOWN , (int)EnumPosition.DOWN) ;
		setTilePosition( (int)EnumPosition.RIGHT_DOWN , (int)EnumPosition.RIGHT_DOWN);
		//reset Gameobject position
		mCurrentGameObjectLU.transform.localPosition = vPositionLU ;
		mCurrentGameObjectU.transform.localPosition  = vPositionU  ;
		mCurrentGameObjectRU.transform.localPosition = vPositionRU ;
		mCurrentGameObjectL.transform.localPosition  = vPositionL  ;
		mCurrentGameObjectM.transform.localPosition  = vPositionM  ;
		mCurrentGameObjectR.transform.localPosition  = vPositionR  ;
		mCurrentGameObjectLD.transform.localPosition = vPositionLD ;
		mCurrentGameObjectD.transform.localPosition  = vPositionD  ;
		mCurrentGameObjectRD.transform.localPosition = vPositionRD ;
	}
	//
	public void setTilePosition(int iSourceTile,int iNew)
	{
		TilePosition pTilePosition ;
		switch (iSourceTile)
		{
		case ((int)EnumPosition.LEFT_UP) : pTilePosition = mCurrentGameObjectLU.GetComponent<TilePosition>() ; break ;
		case ((int)EnumPosition.UP) : pTilePosition = mCurrentGameObjectU.GetComponent<TilePosition>() ;  break ;
		case ((int)EnumPosition.RIGHT_UP) : pTilePosition = mCurrentGameObjectRU.GetComponent<TilePosition>() ;  break ;
		case ((int)EnumPosition.LEFT) : pTilePosition = mCurrentGameObjectL.GetComponent<TilePosition>() ;  break ;
		case ((int)EnumPosition.MIDDLE) : pTilePosition = mCurrentGameObjectM.GetComponent<TilePosition>() ;  break ;
		case ((int)EnumPosition.RIGHT) : pTilePosition = mCurrentGameObjectR.GetComponent<TilePosition>() ; break ;
		case ((int)EnumPosition.LEFT_DOWN) : pTilePosition = mCurrentGameObjectLD.GetComponent<TilePosition>() ; break ;		
		case ((int)EnumPosition.DOWN) : pTilePosition = mCurrentGameObjectD.GetComponent<TilePosition>() ; break ;
		case ((int)EnumPosition.RIGHT_DOWN) : pTilePosition =  mCurrentGameObjectRD.GetComponent<TilePosition>() ; break ;
		default: return ;
		}
		//default scriptobject of tile position
		pTilePosition.iTilePosition = iNew ;
	}
	public int getTilePosition(int iSourceTile)
	{
		TilePosition pTilePosition ;
		switch (iSourceTile)
		{
		case ((int)EnumPosition.LEFT_UP) : pTilePosition = mCurrentGameObjectLU.GetComponent<TilePosition>() ; break ;
		case ((int)EnumPosition.UP) : pTilePosition = mCurrentGameObjectU.GetComponent<TilePosition>() ;  break ;
		case ((int)EnumPosition.RIGHT_UP) : pTilePosition = mCurrentGameObjectRU.GetComponent<TilePosition>() ;  break ;
		case ((int)EnumPosition.LEFT) : pTilePosition = mCurrentGameObjectL.GetComponent<TilePosition>() ;  break ;
		case ((int)EnumPosition.MIDDLE) : pTilePosition = mCurrentGameObjectM.GetComponent<TilePosition>() ;  break ;
		case ((int)EnumPosition.RIGHT) : pTilePosition = mCurrentGameObjectR.GetComponent<TilePosition>() ; break ;
		case ((int)EnumPosition.LEFT_DOWN) : pTilePosition = mCurrentGameObjectLD.GetComponent<TilePosition>() ; break ;		
		case ((int)EnumPosition.DOWN) : pTilePosition = mCurrentGameObjectD.GetComponent<TilePosition>() ; break ;
		case ((int)EnumPosition.RIGHT_DOWN) : pTilePosition =  mCurrentGameObjectRD.GetComponent<TilePosition>() ; break ;
		default: return -1 ;
		}
		//get
		return (pTilePosition.iTilePosition) ;
	}
	//
	public void QuitGame()
	{
		Application.Quit() ;
	}
	//
	public void EnableGameMenu(bool bEnable)
	{
		spriteRenderer = mCurrentGameObjectMenuScore.GetComponent<SpriteRenderer>() ;
		spriteRenderer.enabled = bEnable ;
		spriteRenderer = mCurrentGameObjectMenuContinue.GetComponent<SpriteRenderer>() ;
		spriteRenderer.enabled = bEnable ;
		spriteRenderer = mCurrentGameObjectMenuExit.GetComponent<SpriteRenderer>() ;
		spriteRenderer.enabled = bEnable ;
		spriteRenderer = mCurrentGameObjectMenuEasy.GetComponent<SpriteRenderer>() ;
		spriteRenderer.enabled = bEnable ;
		spriteRenderer = mCurrentGameObjectMenuHard.GetComponent<SpriteRenderer>() ;
		spriteRenderer.enabled = bEnable ;
	}
}