using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestEncryptionKeyRequest
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _config = new ConfigManager().getConfig();
            _cnp = new CnpOnline(_config);
        }

        [Test]
        public void SimpleEncryptionKeyRequest()
        {
            var encrypt = new EncryptionKeyRequest
            {
          encryptionKeyRequest= encryptionKeyRequestEnum.PREVIOUS,
            };
          
            var response = _cnp.EncryptionKey(encrypt);

           

            Assert.AreEqual(10000, response.encryptionKeySequence);
            Assert.AreEqual("-----BEGIN PGP PUBLIC KEY BLOCK-----\n\nmQENBGbVW3EBCADhseJVZh+DhiAqhyDZXX4MwE9aXcDVUdNZoD3xgKn+caUvHQHT\njCSBWpHFTGPyrya5gfHpcH6Z9lN1rlxqr0hs1cY/Yhkxlrfo+6w7Y2kRbwj2ilLc\nVk9iPV4DdjrFUFdWp3MdaJ7YhAHOUI60fBvf3GrnHCy6HtCgjROEYSAID4L5gCXc\nFUqHWUCzMsKM1+GlSfmsAHrwxAKxYwC4wuu/aAuaOgofrzN3Ud8p1WvWkdB60SKM\nTdY2qQGj6WY3U5whULgmjmoyrRiUq89yNZMve43jjqyYBtZqLcNi2bS6sXmZyjT5\nMcmRb2+KzmqCftvoqTDo2VEet+Hp4MSRwHw5ABEBAAG0OFNhbmRib3hOb25FeHBp\ncmluZ0tleSA8c3VzaGFtYS5hbmlsZ2hhZGFnZUB3b3JsZHBheS5jb20+iQFOBBMB\nCAA4FiEEPmqIQhTLLyZAkBizzinUXUtH4IQFAmbVW3ECGwMFCwkIBwIGFQoJCAsC\nBBYCAwECHgECF4AACgkQzinUXUtH4ITiVggAvIxV8aDS9dvqrNzuj5mFNCBB00aN\nObkB9mQFuEEGPP+TYpsX6hYFOH5M5jDuGe0il+QskEynbqJfvyx1k55cpSUR9XFj\nu8Q7QLMsUJqbExyJ/Ymu3dc0YuX4RO9RkcB7gg1/SCDi5KCgtaUW9eciNLeAyYnk\nCN5DwgoHaI8yEGXowLSidxJUUjfDqI+CiNXxZb7O9PNDTlKmyASO1mrbXkNiZzMr\nZ2o53IXR/HCr98yqTpwsE2ERX5aFPf5aeyjom+znWUOU3S9UhgmoXXgvKM4mHWzj\n4EQ0Y3IJlV7WyOR14AfDj7zIoX9mC5jEHvrElFRsxCQdWrme95itR9fL/LkBDQRm\n1VtxAQgAsUkEwyZvyy4Vxg071s8VHGaxWm9hvQC1C/9v1nxn/ozCBEoUyVcrEBVj\nrKbC9ESu0CxeayBtl7VVAd7bxwSsiyWgLtn1KgXPyn9nYRNF5luUJEZpXiVCsk+c\n7FeZQe1pJNFo3RI1WRO2vhNHthLNNn46RV4xMZm6acoFxLipMuMmpkW9BsU6vP/5\nrR4zSQH9Enz4m1fZOJCAx4Ngq4Zzqpi/bgni0MS5GMIiZyWNgK+C+QB4sw8/ehdX\nvjBPhSmYilZ/xbsuzfVFUHhbrewSZsAKRiqc4LPMRTUiHsnPTtjJbqjyZUEI9cwU\n1SDVB6Z/pCQKfrOz1Dxw6Re4l/rFKwARAQABiQE2BBgBCAAgFiEEPmqIQhTLLyZA\nkBizzinUXUtH4IQFAmbVW3ECGwwACgkQzinUXUtH4ITIeAgAj8oJ4t8ngpm2+8ZB\nNZGDhOyGvbY7qQuCv40mppx1kwoNa7btUSapBk5V7S4nxGlvrGRp2eSY/0XymvDs\nWwByOk0PYjwCXYyZwdmjTXn2WWVBjkCFq5aVVJlNeGA6h+dO4rEX4YnZhPpSafcd\nbNLJ595fhIF/QX825bZGhanYJyWdFzy7cwjpRF+mGrqf/LydNcyWZs6DvNqd+p5J\nhciFXa1yrxFXRwnMtcWF7MOaXHYnCISGOpJR7ZYDXZCviqsgzBhm7xDr0GTyoElO\nFNI7UTXAfcoNpsh5+GtKAUkoNe7bCtqsH3NgFHJOamiPak+NDa5T4Vci7742Kvf9\ndNlljg==\n=VwD5\n-----END PGP PUBLIC KEY BLOCK-----", response.encryptionKey);
        }

     
    
    }
}
