using ComboService.Application.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.Interfaces
{
	public interface IMessageBusClient
	{
		void PublishedCombo(ComboResponseModel model);

		void UpdatedCombo(ComboResponseModel model);

		void DeletedCombo(ComboResponseModel model);
	}
}
