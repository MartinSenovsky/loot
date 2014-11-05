using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.M.Scripts.GUI
{
	public interface IPopup
	{
		bool _blockAllOthers();

		void _openAnim();
		
		void _closeAnim();

	}
}
