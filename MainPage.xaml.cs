namespace TicTacToeMAUI;

public partial class MainPage : ContentPage
{
    string currentPlayer = "X";
    string[] board = new string[9];
    bool gameOver = false;

    public MainPage()
    {
        InitializeComponent();
        ResetGame();
    }

    private void OnCellClicked(object sender, EventArgs e)
    {
        if (gameOver)
            return;

        Button button = sender as Button;
        int index = int.Parse(button.CommandParameter.ToString());

        if (!string.IsNullOrEmpty(board[index]))
            return;

        board[index] = currentPlayer;
        button.Text = currentPlayer;

        if (CheckWinner())
        {
            StatusLabel.Text = $"Player {currentPlayer} Wins!";
            gameOver = true;
            return;
        }

        if (board.All(cell => !string.IsNullOrEmpty(cell)))
        {
            StatusLabel.Text = "It's a Draw!";
            gameOver = true;
            return;
        }

        currentPlayer = currentPlayer == "X" ? "O" : "X";
        StatusLabel.Text = $"Player {currentPlayer}'s Turn";
    }

    private bool CheckWinner()
    {
        int[,] winPatterns = new int[,]
        {
            {0,1,2}, {3,4,5}, {6,7,8}, // Rows
            {0,3,6}, {1,4,7}, {2,5,8}, // Columns
            {0,4,8}, {2,4,6}           // Diagonals
        };

        for (int i = 0; i < winPatterns.GetLength(0); i++)
        {
            if (board[winPatterns[i, 0]] == currentPlayer &&
                board[winPatterns[i, 1]] == currentPlayer &&
                board[winPatterns[i, 2]] == currentPlayer)
            {
                return true;
            }
        }
        return false;
    }

    private void OnRestartClicked(object sender, EventArgs e)
    {
        ResetGame();
    }

    private void ResetGame()
    {
        currentPlayer = "X";
        gameOver = false;
        StatusLabel.Text = "Player X's Turn";

        for (int i = 0; i < board.Length; i++)
            board[i] = string.Empty;

        foreach (var view in GameGrid.Children)
        {
            if (view is Button btn)
                btn.Text = "";
        }
    }
}
