using UnityEngine;
using System.IO;
public class TextFileFinder_launch : MonoBehaviour
{

    protected string m_textPath;
    AudioSource source;

    protected FileBrowser m_fileBrowser1;

    [SerializeField]
    protected Texture2D m_directoryImage,
                        m_fileImage;

    public Texture2D texture;
    GameObject audio;

    protected void OnGUI()
    {
        if (m_fileBrowser1 != null)
        {
            m_fileBrowser1.OnGUI();
        }
        else
        {
            OnGUIMain();
        }
    }
    public void fileBrowserOpen(string Padname)
    {
        source = GameObject.Find(Padname).GetComponent<AudioSource>();
        m_fileBrowser1 = new FileBrowser(
                new Rect(10, 100, 600, 300),
                "Choose Audio File",
                FileSelectedCallback
            
            );

    }
    
    public void OnGUIMain()
    {
        //버튼 컨트롤
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(Path.GetFileName(m_textPath) ?? "none selected");
        if (GUILayout.Button("", GUILayout.ExpandWidth(false)))
        {
            m_fileBrowser1 = new FileBrowser(
                new Rect(10, 100, 0, 0),
                "Choose Audio File",
                FileSelectedCallback

          
            );
            source = GameObject.Find("track1_audio").GetComponent<AudioSource>();
            //m_fileBrowser.SelectionPattern = "*.wav";
            m_fileBrowser1.DirectoryImage = m_directoryImage;
            m_fileBrowser1.FileImage = m_fileImage;
        }

        if (GUILayout.Button("", GUILayout.ExpandWidth(false)))
        {
            m_fileBrowser1 = new FileBrowser(
                new Rect(10, 100, 0, 0),
                "Choose Audio File",
                FileSelectedCallback
            );
            source = GameObject.Find("track2_audio").GetComponent<AudioSource>();
            //m_fileBrowser1.SelectionPattern = "*.*";
            m_fileBrowser1.DirectoryImage = m_directoryImage;
            m_fileBrowser1.FileImage = m_fileImage;
        }
        GUILayout.EndHorizontal();
    }

    protected void FileSelectedCallback(string path)
    {
        m_fileBrowser1 = null;
        if (path.Length != 0)
        {
            WWW www = new WWW("file:///" + path);
            while (!www.isDone)
                Debug.Log(www.progress);
            source.clip = www.GetAudioClip(false, false, AudioType.WAV);
        }
        m_textPath = path;
    }

}