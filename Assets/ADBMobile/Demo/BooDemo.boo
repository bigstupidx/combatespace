import UnityEngine
import UnityEngine.UI;
import com.adobe.mobile;
import UnityEngine.SceneManagement;

class BooDemo (MonoBehaviour):    
	public btnTest as Button
	public btnReturn as Button 
	public nextSceneName as string 
	def Start():
		ADBMobile.TrackAction("OnBooEnable1", null);
		self.btnTest.onClick.AddListener(TestClick);
		self.btnReturn.onClick.AddListener(ReturnClick);
		
	def Update():		
		pass;		
		
	def TestClick():
		ADBMobile.TrackAction("Boo3", null);
		
	def ReturnClick():
		SceneManager.LoadScene (nextSceneName);