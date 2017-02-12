Imports System.Threading

Public Class Form1
  ReadOnly _bot As New Bot
  ReadOnly _th As New Thread(AddressOf _bot.Run)

  Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    BeginInvoke(Sub()
                  _th.IsBackground = True
                  _th.Start()
                End Sub)
    BeginInvoke(Sub()
                  Dim psi As New ProcessStartInfo(Application.StartupPath & "/start.bat")
                  psi.RedirectStandardError = False
                  psi.RedirectStandardOutput = False
                  psi.CreateNoWindow = False

                  psi.UseShellExecute = True

                  Dim process As Process = Process.Start(psi)
                End Sub)
  End Sub


End Class
