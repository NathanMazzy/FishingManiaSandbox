using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;
using Sandbox.UI;

namespace Sandbox
{
	public partial class FishingManiaHud : HudEntity<RootPanel>
	{
		public FishingManiaHud()
		{
			if ( !IsClient )
			{
				Log.Info( "idk" );
				return;
			}

			Log.Info( "hud" );
			RootPanel.StyleSheet.Load( "/ui/FishingManiaHud.scss" );
			RootPanel.AddChild<FishDisplay>();
		}
	}
}
