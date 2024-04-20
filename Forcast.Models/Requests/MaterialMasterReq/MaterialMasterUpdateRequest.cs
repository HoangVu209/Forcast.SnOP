using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forcast.Models.Requests.MaterialMasterReq
{
    public class MaterialMasterUpdateRequest
    {
        public int Material { get; set; }

        public string DpName { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}
