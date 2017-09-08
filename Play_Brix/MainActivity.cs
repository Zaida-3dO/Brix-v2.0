using Android.App;
using Android.Widget;
using Android.OS;
using ChessAPI;
using System.Collections.Generic;
using Play_Brix;
namespace Brix
{
    [Activity(Label = "Brix", MainLauncher = true)]
    public class MainActivity : Activity
    {
      //  static int count = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Play_Brix.Resource.Layout.Main);

            //Tests
            ImageButton yourBtn = (ImageButton)FindViewById(Play_Brix.Resource.Id.imageImageButton1);
            TextView inn = (TextView)FindViewById(Play_Brix.Resource.Id.textView1);
            ButtonListener bl = new ButtonListener(null);
                Board board = new Board();
                yourBtn.Click += bl.TileClickedTest(board, inn);
                List<ImageButton> btnList = new List<ImageButton>();
                for (int i = 2131034113; i <= 2131034176; i++)
                    btnList.Add((ImageButton)FindViewById(i));
                View view = new View(board, btnList);
                //Button Listner
                ButtonListener buttonListener = new ButtonListener(view);
                foreach (ImageButton tileBtn in view.btnList) {
                    tileBtn.SoundEffectsEnabled = false;
                    tileBtn.Click += buttonListener.TileClicked(tileBtn);
                }
                //startGame
                view.Refresh();
        }
       
    }
}

