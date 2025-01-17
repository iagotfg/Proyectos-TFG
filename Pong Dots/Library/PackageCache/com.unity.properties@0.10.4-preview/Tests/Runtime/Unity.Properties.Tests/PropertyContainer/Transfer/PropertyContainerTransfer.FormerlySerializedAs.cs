using System.Collections.Generic;
using NUnit.Framework;

namespace Unity.Properties.Tests
{
    [TestFixture]
    partial class PropertyContainerTransferTests
    {
        [Test]
        public void PropertyContainer_Transfer_SupportsFormerNames()
        {
            // Best case scenario would be to test by deserializing some data, but we will mock this behaviour by
            // knowing that the Transfer visitor will transfer based on property names.
            var src = new FormerlySerializedAsMockData
            {
                MyFloat = 15,
                SomeList = new List<int>{1, 2, 4, 5, 6},
                MyVector = new MyOwnVector{ x = 11, y = 22, z = 33 }
            };
            
            var dst = new FormerlySerializedAsData();

            using (var result = PropertyContainer.Transfer(ref dst, ref src))
            {
                Assert.That(result.Succeeded, Is.True);
                Assert.That(dst.SomeSimpleFloat, Is.EqualTo(src.MyFloat));
                Assert.That(dst.ListOfInts, Is.EqualTo(src.SomeList));
                Assert.That(dst.MyVectorRenamed.RenamedX, Is.EqualTo(src.MyVector.x));
                Assert.That(dst.MyVectorRenamed.RenamedY, Is.EqualTo(src.MyVector.y));
                Assert.That(dst.MyVectorRenamed.RenamedZ, Is.EqualTo(src.MyVector.z));
            }
        }
    }
}