using System;
using System.Collections.Generic;
using System.Text;

namespace ClashRoyaleApi.Infrastructure.Models
{
    public class AzureBlobStorageSettings
    {
        public string ImageConnectionString { get; set; }

        public string ContainerName { get; set; }

        public string DirReference { get; set; }
    }
}
