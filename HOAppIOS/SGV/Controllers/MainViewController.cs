using System;

using UIKit;

namespace HOAppIOS.SGV.Controllers
{
    public partial class MainViewController : UIViewController
    {
        public MainViewController() : base("MainViewController", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            lblTest.Text = "jeroen";
            // Perform any additional setup after loading the view, typically from a nib.
        }
    }
}