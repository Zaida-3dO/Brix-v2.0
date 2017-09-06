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

            /*
            ImageButton yourBtn = (ImageButton)FindViewById(Resource.Id.ai7);
            yourBtn.Click += delegate { yourBtn.SetImageResource(Resource.Drawable.bluepawn); };
            yourBtn = (ImageButton)FindViewById(Resource.Id.imageImageButton1);
           
            yourBtn.Click += delegate { inn.Text = count + ""; count++; };
            inn.Click += delegate { inn.Text = count + ""; count++; };
            
            inn.Click += buttonListener.TileClickedTest(yourBtn,inn);*/
            ImageButton yourBtn = (ImageButton)FindViewById(Resource.Id.imageImageButton1);
            TextView inn = (TextView)FindViewById(Resource.Id.textView1);

            Board board = new Board();
            ButtonListener buttonListener = new ButtonListener(board);
            yourBtn.Click += buttonListener.TileClickedTest(yourBtn, inn);
            List<ImageButton> btnList = new List<ImageButton>();
            for (int i = 2131034113;i<= 2131034176; i++)
            {
                ImageButton tileBtn = (ImageButton)FindViewById(i);
                btnList.Add(tileBtn);
                tileBtn.Click+= buttonListener.TileClicked(View.NameOfId(i),inn);
            }
            View.btnList = btnList;
            View.Refresh(board);
            //Create a image view object class with the image views in a dictionary <imageview ,Tile>
      //    ImageButton yourBtn = (ImageButton)FindViewById(Resource.Id.ai7);
        }
       
    }
}

