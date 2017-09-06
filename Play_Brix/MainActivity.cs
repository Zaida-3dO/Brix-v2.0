using Android.App;
using Android.Widget;
using Android.OS;
using ChessAPI;
namespace Play_Brix
{
    [Activity(Label = "Play_Brix", MainLauncher = true)]
    public class MainActivity : Activity
    {
        static int count = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
           ImageButton yourBtn = (ImageButton)FindViewById(Resource.Id.ai7);

            yourBtn.Click += delegate { yourBtn.SetImageResource(Resource.Drawable.bluepawn); };
            yourBtn = (ImageButton)FindViewById(Resource.Id.imageImageButton1);
            TextView inn = (TextView)FindViewById(Resource.Id.textView1);
            yourBtn.Click += delegate { inn.Text = count + ""; count++; };
            inn.Click += delegate { inn.Text = count + ""; count++; };
            ButtonListener buttonListener = new ButtonListener(new Board());
            inn.Click += buttonListener.TileClickedTest(yourBtn,inn);
            
      //      ImageButton yourBtn = (ImageButton)FindViewById(Resource.Id.ai7);
          
        }
    }
}

