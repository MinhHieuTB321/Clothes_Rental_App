using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.EventProcessing
{
	public interface IEventProcessor
	{
		Task ProcessEvent(string message);
	}
}
