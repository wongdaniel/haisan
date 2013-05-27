using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.domain;
using haisan.util;

namespace haisan.dao
{
    interface ProcessingImageDao
    {
        MessageLocal saveOrUpdateProcessingImage(ProcessingImage processingImage);
    }
}
