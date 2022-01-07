using OWML.ModHelper.Menus;
using PacificEngine.OW_CommonResources.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacificEngine.OW_CheatsMod
{
    public class WarpMenu : ModPopupMenu
	{
		public WarpMenu() : base(Helper.helper.Console)
		{	
			var menu = new Menu();
			this.Initialize(menu);
		}
	}
}
