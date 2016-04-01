Imports MT264Sprites

Public Class MainForm
    Private fBallAdmin As BallAdmin

    Private Sub exitMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles exitMenuItem.Click
        Close()
    End Sub

    Private Sub MainForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Up
                upButton_Click(sender, e)
            Case Keys.Down
                downButton_Click(sender, e)
            Case Keys.Left
                leftButton_Click(sender, e)
            Case Keys.Right
                rightButton_Click(sender, e)
            Case Else
        End Select
        e.Handled = True
        updateView()
    End Sub

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        fBallAdmin = New BallAdmin(gamePanel.Width, gamePanel.Height)
        updateView()
    End Sub

    Public Sub updateView()
        gameTimer.Enabled = fBallAdmin.Running
        gameTimer.Interval = fBallAdmin.TimeInterval
        gamePanel.Refresh()
    End Sub

    Private Sub gamePanel_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles gamePanel.Paint
        Dim g As Graphics
        g = gamePanel.CreateGraphics()
        fBallAdmin.draw(g)
        g.Dispose()
    End Sub

    Private Sub gameTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gameTimer.Tick
        fBallAdmin.nextMove()
        updateView()
    End Sub

    Private Sub upButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles upButton.Click
        fBallAdmin.setDirection("u"c)
        updateView()
    End Sub

    Private Sub downButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles downButton.Click
        fBallAdmin.setDirection("d"c)
        updateView()
    End Sub

    Private Sub leftButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles leftButton.Click
        fBallAdmin.setDirection("l"c)
        updateView()
    End Sub

    Private Sub rightButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rightButton.Click
        fBallAdmin.setDirection("r"c)
        updateView()
    End Sub

    Private Sub upButton_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles upButton.PreviewKeyDown, rightButton.PreviewKeyDown, leftButton.PreviewKeyDown, downButton.PreviewKeyDown
        Select Case e.KeyCode
            Case Keys.Down, Keys.Left, Keys.Right, Keys.Up
                e.IsInputKey = True
        End Select
    End Sub

    Private Sub MainForm_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If fBallAdmin IsNot Nothing Then
            fBallAdmin.dispose()
        End If
        fBallAdmin = New BallAdmin(gamePanel.Width, gamePanel.Height)
        updateView()
    End Sub

    Private Sub tryAgainButton_Click(sender As System.Object, e As System.EventArgs) Handles tryAgainButton.Click
        MainForm_Resize(sender, e)
        tryAgainButton.Visible = False
        instructionBox.Text = "Move the ball using the arrow buttons or keyboard keys. Try not to touch the edges of the game area. With every new direction given, the speed of the ball increases!"
    End Sub
End Class
