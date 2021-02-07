Imports System.Net
Imports System.Collections.Specialized
Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim RawData As String = New WebClient().DownloadString("http://api.alternative.me/v2/ticker/?convert=USD")
            Dim Result As String = ""
            If (RawData.Contains(ControlChars.Quote)) Then
                Dim i As Integer = 0
                For Each Arg As String In RawData.Split(ControlChars.Quote)
                    Try
                        If (Arg.ToLower.Equals("price")) Then
                            Dim Value As String = RawData.Split(ControlChars.Quote)(i + 1).Trim
                            If (Value.Contains(".")) Then
                                Value = Value.Split(".")(0)
                            End If
                            Value = Value.Replace(":", "")
                            Result = Value
                            Exit For
                        End If
                    Catch ex As Exception
                    End Try
                    i += 1
                Next
                If Not RawData.Equals("") Then
                    Try
                        Using webClient As WebClient = New WebClient()
                            Dim nameValueCollection As NameValueCollection = New NameValueCollection()
                            nameValueCollection("content") = "1 BTC = $" + Result + " USD"
                            webClient.Headers("authorization") = TextBox1.Text

                            Dim array3 As Byte() = webClient.UploadValues("https://discord.com/api/v8/channels/" + TextBox2.Text +
"/messages", nameValueCollection)

                        End Using

                    Finally
                    End Try

                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Timer1.Stop()
    End Sub
End Class
