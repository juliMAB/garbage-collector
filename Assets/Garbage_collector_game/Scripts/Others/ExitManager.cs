using UnityEngine;
using System.Runtime.InteropServices;

public class ExitManager : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern int MessageBox(System.IntPtr hWnd, string text, string caption, uint type);

    public void RequestExit()
    {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
        // Diálogo nativo de Windows
        int result = MessageBox(System.IntPtr.Zero,
            "¿De verdad quieres cerrar el juego?",
            "Confirmar salida",
            0x00000004 | 0x00000020); // MB_YESNO + MB_ICONQUESTION

        if (result == 6) // IDYES
        {
            QuitGame();
        }
#else
        // Fallback para otras plataformas
        if (UnityEngine.Application.platform == RuntimePlatform.WebGLPlayer)
        {
            // Mostrar diálogo del navegador
            Application.Quit();
        }
        else
        {
            QuitGame();
        }
#endif
    }

    private void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}