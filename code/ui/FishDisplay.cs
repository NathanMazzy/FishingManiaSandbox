using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.UI;
using Sandbox.UI.Construct;
using Sandbox;

namespace Sandbox
{
	public class FishDisplay : Panel
	{
		public Label Label;

		public FishDisplay()
		{
			Label = Add.Label( "1", "value" );
		}

		public override void Tick()
		{
			FishingManiaPlayer ply = Local.Pawn as FishingManiaPlayer;
			
			Label.Text = $"{ply.score}";
		}
	}
}
