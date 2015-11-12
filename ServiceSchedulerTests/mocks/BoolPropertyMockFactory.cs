using NSubstitute;

namespace ServiceSchedulerTests.mocks
{
    public class BoolPropertyMockFactory
    {
        public interface IBoolProperty
        {
            bool Property { get; set; }
        }

        private IBoolProperty _boolPropertyMock;

        public BoolPropertyMockFactory()
        {
            _boolPropertyMock = Substitute.For<IBoolProperty>();
        }

        public void SetReturnValues(bool returnThis, params bool[] returnThese)
        {
            _boolPropertyMock.Property.Returns(returnThis, returnThese);
        }

        public IBoolProperty Create()
        {
            return _boolPropertyMock;
        }
    }
}
