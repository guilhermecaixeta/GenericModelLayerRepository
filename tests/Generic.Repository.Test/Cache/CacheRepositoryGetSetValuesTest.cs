using NUnit.Framework;
using System.Threading.Tasks;

namespace Generic.RepositoryTest.Unit.Cache
{
    [TestFixture]
    public abstract class CacheRepositoryGetSetValuesTest<T> : CacheConfigurationTest<T>
    where T : class
    {
        [Test]
        public async Task ClearCache_Success()
        {
            Cache.ClearCache();
            Assert.IsFalse(await Cache.HasMethodGet(default).ConfigureAwait(false));
            Assert.IsFalse(await Cache.HasMethodGet(default).ConfigureAwait(false));
            Assert.IsFalse(await Cache.HasAttribute(default).ConfigureAwait(false));
            Assert.IsFalse(await Cache.HasProperty(default).ConfigureAwait(false));
        }

        [Test]
        public void GetAttribute_Success()
        {
            var attr = Cache.GetAttribute(NameType, NameProperty, NameAttribute, default);
            Assert.IsNotNull(attr);
        }

        [Test]
        public void GetDictionaryAttribute_Success()
        {
            var attr = Cache.GetDictionaryAttribute(NameType, NameProperty, default);
            Assert.IsNotNull(attr);
        }

        [Test]
        public void GetDictionaryAttributes_Success()
        {
            var attr = Cache.GetDictionaryAttribute(NameType, default);
            Assert.IsNotNull(attr);
        }

        [Test]
        public void GetDictionaryMethodGet_Success()
        {
            var methodGet = Cache.GetDictionaryMethodGet(NameType, default);
            Assert.IsNotNull(methodGet);
        }

        [Test]
        public void GetDictionaryMethodSet_Success()
        {
            var methodSet = Cache.GetDictionaryMethodSet(NameType, default);
            Assert.IsNotNull(methodSet);
        }

        [Test]
        public void GetDictionaryProperties_Success()
        {
            var attr = Cache.GetDictionaryProperties(NameType, default);
            Assert.IsNotNull(attr);
        }

        [Test]
        public void GetMethodGet_Success()
        {
            var methodGet = Cache.GetMethodGet(NameType, NameProperty, default);
            Assert.IsNotNull(methodGet);
        }

        [Test]
        public void GetMethodSet_Success()
        {
            var methodSet = Cache.GetMethodSet(NameType, NameProperty, default);
            Assert.IsNotNull(methodSet);
        }

        [Test]
        public void GetProperty_Success()
        {
            var attr = Cache.GetProperty(NameType, NameProperty, default);
            Assert.IsNotNull(attr);
        }

        [Test]
        public async Task ValidateValues_Success()
        {
            Assert.IsTrue(await Cache.HasMethodGet(default).ConfigureAwait(false));
            Assert.IsTrue(await Cache.HasMethodSet(default).ConfigureAwait(false));
            Assert.IsTrue(await Cache.HasProperty(default).ConfigureAwait(false));
            Assert.IsTrue(await Cache.HasAttribute(default).ConfigureAwait(false));
        }
    }
}