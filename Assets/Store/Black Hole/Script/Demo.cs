using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour
{
    public Material m_Mat = null;
    [Range(0.01f, 0.2f)]
    public float m_DarkRange = 0.1f;
    [Range(-2.5f, -1f)]
    public float m_Distortion = -2f;
    [Range(0.05f, 0.3f)]
    public float m_Form = 0.2f;
    private float m_MouseX = 0f;
    private float m_MouseY = 0f;
    private bool m_TraceMouse = false;

    Camera cam;

    private bool blackHoleInRange = true;
    public GameObject blackHole;

    void Start()
    {
        cam = GetComponent<Camera>();

        if (!SystemInfo.supportsImageEffects)
            enabled = false;
        m_MouseX = m_MouseY = 0.5f;
    }
    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (blackHoleInRange)
        {
            Vector3 coord = cam.WorldToScreenPoint(blackHole.transform.position);
            float dist = Vector3.Distance(cam.transform.position, blackHole.transform.position);

            m_Mat.SetVector("_Center", new Vector4(coord.x / Screen.width, coord.y / Screen.height, 0f, 0f));
            //m_Mat.SetVector ("_Center", new Vector4 (m_MouseX, m_MouseY, 0f, 0f));
            m_DarkRange = dist / 50 * 0.2f;
            m_Mat.SetFloat("_DarkRange", m_DarkRange); // 0.1, (0.01)-(0.20)
            m_Distortion = -(dist / 50 * 1.5f + 1);
            m_Mat.SetFloat("_Distortion", m_Distortion); // -2, (-2.5)-(-1)
            m_Mat.SetFloat("_Form", m_Form);
            Graphics.Blit(sourceTexture, destTexture, m_Mat);
        }
    }
    void Update()
    {
        //		if (Input.GetMouseButtonDown (1))
        //		{
        //			m_TraceMouse = true;
        //		}
        //		else if (Input.GetMouseButtonUp (1))
        //		{
        //			m_TraceMouse = false;
        //		}
        //		else if (Input.GetMouseButton (1))
        //		{
        //			if (m_TraceMouse)
        //			{
        //				m_MouseX = Input.mousePosition.x / Screen.width;
        //#if UNITY_5
        //				m_MouseY = 1f - Input.mousePosition.y / Screen.height;
        //#else
        //				m_MouseY = Input.mousePosition.y / Screen.height;
        //#endif
        //			}
        //		}
    }
    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 200, 25), "Black Hole Demo");
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.GetComponent<BlackHoleScript>())
    //    {
    //        blackHoleInRange = true;
    //        blackHole = other.gameObject;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.GetComponent<BlackHoleScript>())
    //    {
    //        blackHoleInRange = false;
    //    }
    //}
}