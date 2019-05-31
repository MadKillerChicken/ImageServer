using System;
using System.Collections.Generic;

namespace ImageServer.Models
{
    internal class ImageSet : MediaSet
    {
        public override IEnumerable<MediaSubject> Subjects { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        public override IEnumerable<MediaEntry> Entries { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        public override IEnumerable<MediaCategory> Categories { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
    }
}
