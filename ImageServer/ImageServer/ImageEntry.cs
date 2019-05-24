using System;
using System.Collections.Generic;

namespace ImageServer
{
    internal class ImageEntry : MediaEntry
    {
        public override IEnumerable<MediaSubject> Subjects { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
    }
}
