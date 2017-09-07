using Android.App;
using Android.Widget;
using Android.OS;
using ChessAPI;
using System.Collections.Generic;

namespace Play_Brix
{
    [Activity(Label = "Play_Brix", MainLauncher = true)]
    public class MainActivity : Activity
    {
      //  static int count = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Tests
            ImageButton yourBtn = (ImageButton)FindViewById(Resource.Id.imageImageButton1);
            TextView inn = (TextView)FindViewById(Resource.Id.textView1);
            ButtonListener bl = new ButtonListener(null);
            yourBtn.Click += bl.TileClickedTest(yourBtn, inn);
            //SetUp View
            Board board = new Board();
            List<ImageButton> btnList = new List<ImageButton>();
            for (int i = 2131034113;i<= 2131034176; i++)
                btnList.Add((ImageButton)FindViewById(i));
            View view = new View(board, btnList);
            //Button Listner
            ButtonListener buttonListener = new ButtonListener(view);
            foreach (ImageButton tileBtn in view.btnList)
            {
                tileBtn.SoundEffectsEnabled = false;
                tileBtn.Click += buttonListener.TileClicked(tileBtn);
            }
            //startGame
            view.Refresh();
        }
       
    }
}

