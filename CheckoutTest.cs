namespace Renu.Supermarket.Kata.Test
{
    [TestClass]
    public class CheckoutTest
    {
        private readonly ICheckout _checkout;
        private readonly ISkuStore _skuStore;
        private readonly IDynamicPriceHandler _dynamicPriceHandler;

        public CheckoutTest()
        {
            _skuStore = new SkuStore();

            _skuStore.Add(new Sku("A", 100));
            _skuStore.Add(new Sku("B", 25));
            _skuStore.Add(new Sku("C", 50));
            _skuStore.Add(new Sku("D", 10));

            ISkuOfferRepo skuOfferRepo = _skuStore;
            ISkuEnquiry skuEnquiry = _skuStore;
            _dynamicPriceHandler = new DynamicPriceHandler(skuOfferRepo);
            _checkout = new Checkout(_dynamicPriceHandler, skuEnquiry);
        }


        [TestMethod]
        public void TestWithoutOfferMethod()
        {
            _checkout.Reset();
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("B");
            _checkout.Scan("A");
            decimal total = _checkout.GetTotalPrice();

            Assert.AreEqual(325, total);
        }

        [TestMethod]
        public void TestWithOfferForAMethod()
        {
            var sku = _skuStore.Get("A");

            var offer = new SkuSpecialPrice(3, 275);
            _skuStore.ApplyOffer(sku!.Value, offer);

            _checkout.Reset();
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("B");
            _checkout.Scan("A");
            _checkout.Scan("A");

            decimal total = _checkout.GetTotalPrice();

            Assert.AreEqual(400, total);

            _skuStore.RemoveOffer(sku.Value, offer);
        }

        [TestMethod]
        public void TestWithMultipleOfferForAMethod()
        {
            var sku = _skuStore.Get("A");

            var offer = new SkuSpecialPrice(3, 275);
            _skuStore.ApplyOffer(sku!.Value, offer);

            var offer2= new SkuSpecialPrice(5, 400);
            _skuStore.ApplyOffer(sku!.Value, offer2);

            _checkout.Reset();
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("B"); // B
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("B"); // B
            _checkout.Scan("A");


            decimal total = _checkout.GetTotalPrice();

            Assert.AreEqual(825, total);

            _skuStore.RemoveOffer(sku.Value, offer);
            _skuStore.RemoveOffer(sku.Value, offer2);
        }
    }
}