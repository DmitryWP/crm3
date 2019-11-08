using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using crm.contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace crm.core.Controllers
{
    [Route("api/garden-society")]
    [ApiController]
    public class GardenSocietyController : ControllerBase
    {

        public GardenSociety get()
        {
            using (crm.db.Context db = new crm.db.Context())
            {
                IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true);
                IConfigurationRoot config = configBuilder.Build();
                long CurrentGardenSocietyId = config.GetValue<long>("CurrentGardenSocietyId");

                db.entities.GardenSociety entity = db.GardenSocieties.SingleOrDefault(c => c.Id == CurrentGardenSocietyId && c.Enabled == true);

                GardenSociety result = System.Data.EntityHelper.CopyTo<GardenSociety>(entity);
                return result;
            }
        }
    }
}