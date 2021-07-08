using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using static Android.Views.View;

namespace TicTac_Toe
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]

    

    public class MainActivity : AppCompatActivity, IOnClickListener
    {
        Button B1, B2, B3, B4, B5, B6, B7, B8, B9;
        TextView T1, T2;
        string[,] Board = new string[3, 3];
        Dictionary<int, int> BtId = new Dictionary<int, int>();
        Dictionary<int, Button> BtNo = new Dictionary<int, Button>();
        bool turn = true;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Instatiation();
            AiTurn(Board);
        }

        private void Instatiation()
        {
            B1 = FindViewById<Button>(Resource.Id.button1);
            BtId.Add(B1.Id, 0);
            BtNo.Add(0, B1);
            B2 = FindViewById<Button>(Resource.Id.button2);
            BtId.Add(B2.Id, 1);
            BtNo.Add(1, B2);
            B3 = FindViewById<Button>(Resource.Id.button3);
            BtId.Add(B3.Id, 2);
            BtNo.Add(2, B3);
            B4 = FindViewById<Button>(Resource.Id.button4);
            BtId.Add(B4.Id, 3);
            BtNo.Add(3, B4);
            B5 = FindViewById<Button>(Resource.Id.button5);
            BtId.Add(B5.Id, 4);
            BtNo.Add(4, B5);
            B6 = FindViewById<Button>(Resource.Id.button6);
            BtId.Add(B6.Id, 5);
            BtNo.Add(5, B6);
            B7 = FindViewById<Button>(Resource.Id.button7);
            BtId.Add(B7.Id, 6);
            BtNo.Add(6, B7);
            B8 = FindViewById<Button>(Resource.Id.button8);
            BtId.Add(B8.Id, 7);
            BtNo.Add(7, B8);
            B9 = FindViewById<Button>(Resource.Id.button9);
            BtId.Add(B9.Id, 8);
            BtNo.Add(8, B9);
            T1 = FindViewById<TextView>(Resource.Id.textView1);
            T2 = FindViewById<TextView>(Resource.Id.textView2);

            B1.SetOnClickListener(this);
            B2.SetOnClickListener(this);
            B3.SetOnClickListener(this);
            B4.SetOnClickListener(this);
            B5.SetOnClickListener(this);
            B6.SetOnClickListener(this);
            B7.SetOnClickListener(this);
            B8.SetOnClickListener(this);
            B9.SetOnClickListener(this);
            T1.Text = turn ? "Player " + BtId[B1.Id] : "Opponent";
            for(int i=0; i<3; i++)
            {
                for(int j=0; j<3; j++)
                {
                    Board[i, j] = "N";
                }
            }

        }
        private bool IsChecked(Button b)
        {
            if (b.Text == "")
                return false;
            else
                return true;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnClick(View v)
        {
            // throw new System.NotImplementedException();
            switch (v.Id)
            {
                case Resource.Id.button1:
                    Execute(B1, v);
                    break;
                case Resource.Id.button2:
                    Execute(B2, v);
                    break;
                case Resource.Id.button3:
                    Execute(B3, v);
                    break;
                case Resource.Id.button4:
                    Execute(B4, v);
                    break;
                case Resource.Id.button5:
                    Execute(B5, v);
                    break;
                case Resource.Id.button6:
                    Execute(B6, v);
                    break;
                case Resource.Id.button7:
                    Execute(B7, v);
                    break;
                case Resource.Id.button8:
                    Execute(B8, v);
                    break;
                case Resource.Id.button9:
                    Execute(B9, v);
                    break;
            }
        }
        public void Execute(Button b, View v)
        {
            if (!IsChecked(b))
            {
                if (!turn)
                {
                    b.Text = "O";
                    int i = BtId[b.Id] / 3;
                    int j = BtId[b.Id] % 3;
                    Board[i, j] = "O";
                    turn = true;
                    Check(Board);
                    AiTurn(Board);

                }
                else
                {
                    //b.Text = "O";
                    //int i = BtId[b.Id] / 3;
                    //int j = BtId[b.Id] % 3;
                    //Board[i, j] = "O";
                    //turn = true;
                    //Check(Board);
                    
                }
                T1.Text = turn ? "Player": "Opponent";
            }
        }
        private void Reset()
        {
            B1.Text = "";
            B2.Text = "";
            B3.Text = "";
            B4.Text = "";
            B5.Text = "";
            B6.Text = "";
            B7.Text = "";
            B8.Text = "";
            B9.Text = "";
            turn = true;
            AiTurn(Board);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Board[i, j] = "N";
                }
            }
        }
        private bool SpotLeft(string[,] b)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (b[i, j] == "N")
                        return true;
                }
            }
            return false;
        }
        private void Check(string[,] b)
        {
            if (GameScore(Board) == 10)
            {
                Android.App.AlertDialog.Builder alertD = new Android.App.AlertDialog.Builder(this);
                alertD.SetTitle("Tic Tac Toe");
                alertD.SetMessage("Player Won!");
                alertD.SetNeutralButton("Restart", delegate
                {
                    Reset();
                });
                alertD.Show();
            }
            if (GameScore(Board) == -10)
            {
                Android.App.AlertDialog.Builder alertD = new Android.App.AlertDialog.Builder(this);
                alertD.SetTitle("Tic Tac Toe");
                alertD.SetMessage("Opponent Won!");
                alertD.SetNeutralButton("Restart", delegate
                {
                    Reset();
                });
                alertD.Show();
            }
            if (GameScore(Board) == 0 && !SpotLeft(Board))
            {
                Android.App.AlertDialog.Builder alertD = new Android.App.AlertDialog.Builder(this);
                alertD.SetTitle("Tic Tac Toe");
                alertD.SetMessage("Draw!");
                alertD.SetNeutralButton("Restart", delegate
                {
                    Reset();
                });
                alertD.Show();
            }
        }
        private int GameScore(string[,] b)
        {
            for (int i = 0; i < 3; i++)
            {
                if (Board[i, 0].Equals("X") && Board[i, 1].Equals("X") && Board[i, 2].Equals("X"))
                    return 10;
                if (Board[i, 0].Equals("O") && Board[i, 1].Equals("O") && Board[i, 2].Equals("O"))
                    return -10;
            }
            for (int i = 0; i < 3; i++)
            {
                if (Board[0, i].Equals("X") && Board[1, i].Equals("X") && Board[2, i].Equals("X"))
                    return 10;
                if (Board[0, i].Equals("O") && Board[1, i].Equals("O") && Board[2, i].Equals("O"))
                    return -10;
            }
            if (Board[0, 0].Equals("X") && Board[1, 1].Equals("X") && Board[2, 2].Equals("X"))
                return 10;
            if (Board[0, 0].Equals("O") && Board[1, 1].Equals("O") && Board[2, 2].Equals("O"))
                return -10;
            if (Board[0, 2].Equals("X") && Board[1, 1].Equals("X") && Board[2, 0].Equals("X"))
                return 10;
            if (Board[0, 2].Equals("O") && Board[1, 1].Equals("O") && Board[2, 0].Equals("O"))
                return -10;
            else
                return 0;
        }
        public int MinMax(string[,] board, bool b, int depth)
        {
            int score = GameScore(board);
            if (score == 10)
                return 10;
            if (score == -10)
                return -10;
            if (!SpotLeft(board))
                return 0;
            if (turn)
            {
                int best = -1000;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == "N")
                        {
                            board[i, j] = "X";
                            best = Math.Max(best, MinMax(board, !turn, depth + 1));

                            board[i, j] = "N";
                        }
                    }
                }
                return best;
            }

            else
            {
                int best = 1000;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == "N")
                        {
                            board[i, j] = "X";
                            best = Math.Min(best, MinMax(board, turn, depth + 1));

                            board[i, j] = "N";
                        }
                    }
                }
                return best;

            }
        }

        private Move bestmove(string[,] b)
        {
            int bestval = -1000;
            Move bmove = new Move();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (b[i, j] == "N")
                    {
                        b[i, j] = "X";

                        int moveval = MinMax(b, false, 0);

                        b[i, j] = "N";

                        if (moveval > bestval)
                        {
                            bmove.row = i;
                            bmove.col = j;
                            bestval = moveval;
                        }
                    }
                }
            }
            return bmove;
        }
        private bool IsNew(string[,] b)
        {
            bool stat = true;
            for ( int i = 0; i < 3; i++)
            {
                for ( int j = 0; j < 3; j++)
                {
                    if (!(b[i, j] == "N"))
                        stat = false;
                }
            }
            return stat;
        }
        private void AiTurn(string[,] b)
        {
            if (turn)
            {
                if (IsNew(b)){
                    int rnd = new Random().Next(9);
                    int i = rnd / 3;
                    int j = rnd % 3;
                    Button bt = BtNo[i * 3 + j];
                    bt.Text = "X";
                    b[i, j] = "X";
                    Check(b);
                    T1.Text = turn ? "Player" : "Opponent";
                    turn = false;
                }
                else
                {
                    Move bMove = bestmove(b);
                    int i = bMove.row;
                    int j = bMove.col;
                    T2.Text = Convert.ToString(i * 3 + j) + Convert.ToString(MinMax(Board, turn, 0));
                    Button bt = BtNo[i * 3 + j];
                    if (!IsChecked(bt))
                    {
                        bt.Text = "X";
                    }
                    b[i, j] = "X";
                    Check(b);
                    T1.Text = turn ? "Player" : "Opponent";
                    turn = false;
                }
                
            }
        }
    }
    class Move
    {
        public int row, col;
    }
}