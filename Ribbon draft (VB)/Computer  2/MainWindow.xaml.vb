Class MainWindow

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button2.Click

    End Sub

    Private Sub WindowsFormsHost1_ChildChanged(sender As System.Object, e As System.Windows.Forms.Integration.ChildChangedEventArgs) Handles WindowsFormsHost1.ChildChanged
    End Sub

    Private Sub RibbonWindow_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        Dim Login As New LoginForm1
        Login.ShowDialog()
    End Sub
End Class
