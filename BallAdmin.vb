Imports MT264Sprites
Public Class BallAdmin
    Private fStep As Integer
    Private fHVSprite As HVSprite
    Private fGameImage As Bitmap
    Private fGameArea As Rectangle
    Private fTimeInterval As Integer
    Private fRunning As Boolean

    Public ReadOnly Property TimeInterval As Integer
        Get
            Return fTimeInterval
        End Get
    End Property

    Public ReadOnly Property Running As Boolean
        Get
            Return fRunning
        End Get
    End Property

    Public ReadOnly Property GameImage As Bitmap
        Get
            Return fGameImage
        End Get
    End Property

    Public Sub New()
        'Preconditions: none
        'Postconditions: A BallAdmin object is created using the parametrised
        'constructor, but with a game area of width 200 and height 200.
        MyClass.New(200, 200)
    End Sub

    Public Sub New(ByVal width As Integer, ByVal height As Integer)
        'Preconditions: width > 0 and height > 0.
        'Postconditions: A BallAdmin object is created. The game area is created of the
        'given width and height. GameImage is created with the same width and height.
        'A ball is placed so that it will appear in the middle of the game area. 
        'TimeInterval is set to 100. The ball is drawn on the game area and Running
        ' is set to True.
        Dim spritePoint As Point
        fGameArea = New Rectangle(0, 0, width, height)
        fGameImage = New Bitmap(fGameArea.Width, fGameArea.Height)
        spritePoint = New Point(fGameArea.Width \ 2, fGameArea.Height \ 2)
        fStep = 3
        fHVSprite = New HVSprite(spritePoint, fGameArea.Width \ 20, fGameArea.Width \ 20, fStep, 0)
        fTimeInterval = 100
        fRunning = True
        updateGameImage()
    End Sub

    Public Sub setDirection(ByVal direction As Char)
        'Preconditions: none
        'Postconditions: Decreases TimeInterval while > 10 and the ball’s
        'direction of movement is set according to the parameter.
        If TimeInterval > 10 Then
            fTimeInterval = TimeInterval - 2
        End If
        Select Case direction
            Case "u"c : fHVSprite.goUp(fStep)
            Case "d"c : fHVSprite.goDown(fStep)
            Case "l"c : fHVSprite.goLeft(fStep)
            Case "r"c : fHVSprite.goRight(fStep)
        End Select
    End Sub

    Public Sub nextMove()
        'Preconditions: none
        'Postconditions: If Running is True, the ball is moved and GameImage is
        'updated, and if the ball has gone through the game area’s boundaries, then
        'Running is set to False.
        If Running Then
            fHVSprite.move()
            updateGameImage()
        End If
        If Not fGameArea.Contains(fHVSprite.BoundingBox) Then
            fRunning = False
            MainForm.instructionBox.Text = "Game over!"
            MainForm.tryAgainButton.Visible = True
        End If
    End Sub

    Private Sub updateGameImage()
        'Preconditions: none
        'Postconditions: GameImage is updated to match the current position of the ball.
        Dim g As Graphics
        g = Graphics.FromImage(GameImage)
        g.Clear(Color.White)
        fHVSprite.draw(g)
        g.Dispose()
    End Sub

    Public Sub draw(ByVal g As Graphics)
        'Preconditions: none
        'Postconditions: The current state of the game is drawn on g.
        g.DrawImage(GameImage, 0, 0)
    End Sub

    Public Sub dispose()
        GameImage.Dispose()
    End Sub
End Class
