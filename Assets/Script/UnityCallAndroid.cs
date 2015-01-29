using UnityEngine;
using System.Collections;

public class UnityCallAndroid : MonoBehaviour 
{
	protected bool bOneTimeSetAlert = false ;
	protected bool bOneTimeSetToast = false ;
	protected NewBehaviourScript pMyUnity ; 
	void Awake()
	{
	}
	// Use this for initialization
	void Start ()
	{
		pMyUnity = this.transform.GetComponent<NewBehaviourScript>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//點擊手機返回鍵關閉應用程序
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home) )
		{
			Application.Quit();
		}
	}
	//unity call android toast()
	public void UnityCallAndroidToast(string sToastString)
	{
		if (bOneTimeSetToast == true) return ;
		bOneTimeSetToast = true ;
		//
		AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");			
		//functionName , ToastMessage
		ajo.Call("showToast" , sToastString);
	}
	//unity call android AlertDialog()
	public void UnityCallAndroidAlert(string sInformation,string sTitle,string sLeftButton,string sRightButton)
	{
		if (bOneTimeSetAlert == true) return ;
		bOneTimeSetAlert = true ;
		//
		AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");
		//functionName , DialogTitle , DialogMessage , PositiveButton Name , NegativeButton Name
		ajo.Call("showAlertDialog" , sInformation , sTitle , sLeftButton , sRightButton);	
	}
	//unity call android finish()
	public void UnityCallAndroidFinish()
	{
		AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");
		ajo.Call("exit");
	}
	//unity call finish()
	public void UnityFinish()
	{
		Application.Quit() ;
	}
	//Android Callback unity Function, by AlertDialog
	public void AlertDialogOnClick(string sClickButton)
	{
		//Android AlertDialog "OK" pressed.(PositiveButtonName)
		if (string.Compare(sClickButton , "POSITIVE") == 0)
		{
		  //Application.LoadLevel(Application.loadedLevel);
			pMyUnity.ResetGame() ;
		}
		//Android AlertDialog "CANCEL" pressed.(NegativeButtonName)
		if (string.Compare(sClickButton , "NEGATIVE") == 0)
		{
			Application.Quit() ;
		}
		//Android AlertDialog "Middle Button" pressed.(NeutralButtonName)
		if (string.Compare(sClickButton , "NEUTRAL") == 0)
		{
			Application.Quit() ;
		}
	}
	//Android Callback unity Function, by Menu Selected
	public void AndroidCallUnityFunction(string sKeyword)
	{
		if (string.Compare(sKeyword , "EASY") == 0)
		{

		}
		else if (string.Compare(sKeyword , "HARD") == 0)
		{
		}
		else
			return ;//no thing happen..hmm?

	}
	//
	public void reset()
	{
		bOneTimeSetAlert = false ;
		bOneTimeSetToast = false ;
	}
}
