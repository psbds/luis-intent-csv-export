using Psbds.LUIS.IntentCSVExport.Core.Helpers;
using Psbds.LUIS.IntentCSVExport.Core.Models;
using Psbds.LUIS.IntentCSVImport.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Psbds.LUIS.IntentCSVExport.Core
{
    public class ExportService
    {
        private readonly string _applicationId;
        private readonly string _applicationVersion;
        private readonly LuisClient _luisClient;

        public ExportService(string applicationId, string applicationKey, string applicationVersion)
        {
            _applicationId = applicationId;
            _applicationVersion = applicationVersion;
            _luisClient = new LuisClient(applicationKey);
        }

        public async Task<List<Tuple<string, string>>> Export()
        {
            var applicationVersionModel = (await this._luisClient.ExportVersion(_applicationId, _applicationVersion)).DeserializeObject<ApplicationVersionModel>();

            return applicationVersionModel.Utterances.ToList()
                .Select(x =>
                {
                    return new Tuple<string, string>(x.Intent, x.Text);
                })
                .ToList();
        }
    }
}
