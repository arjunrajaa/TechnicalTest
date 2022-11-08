using Microsoft.VisualStudio.TestTools.UnitTesting;
using Accessor;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Accessor.ApiCaller;
using Moq;
using Moq.Protected;

namespace Accessor.Tests
{
    [TestClass()]
    public class JsonPlaceholderAccessorTests
    {
        private IDataAccessor? _accessor;

        [TestMethod()]
        public void GetPhotos_Should_Return_Result()
        {
            //Arrange
            var testPhotoString =
                "[{\r\n    \"albumId\": 1,\r\n    \"id\": 1,\r\n    \"title\": \"accusamus beatae ad facilis cum similique qui sunt\",\r\n    \"url\": \"https://via.placeholder.com/600/92c952\",\r\n    \"thumbnailUrl\": \"https://via.placeholder.com/150/92c952\"\r\n  },\r\n  {\r\n    \"albumId\": 1,\r\n    \"id\": 2,\r\n    \"title\": \"reprehenderit est deserunt velit ipsam\",\r\n    \"url\": \"https://via.placeholder.com/600/771796\",\r\n    \"thumbnailUrl\": \"https://via.placeholder.com/150/771796\"\r\n  }]";

            var mockApiCaller = new Mock<IApiCaller>();
            mockApiCaller.Setup(m => m.GetApiResult(("/photos"))).ReturnsAsync(testPhotoString);
            _accessor = new JsonPlaceholderAccessor(mockApiCaller.Object);
            
            //Act
            var photos =  _accessor.GetPhotos().Result;
            
            //Verify
            Assert.IsNotNull(photos);
            mockApiCaller.Verify(mock => mock.GetApiResult("/photos"), Times.Once());
        }

        [TestMethod()]
        public void GetAlbums_Should_Return_Result()
        {
            //Arrange
            var testAlbumString =
                "[{\r\n    \"userId\": 1,\r\n    \"id\": 1,\r\n    \"title\": \"quidem molestiae enim\"\r\n  },\r\n  {\r\n    \"userId\": 1,\r\n    \"id\": 2,\r\n    \"title\": \"sunt qui excepturi placeat culpa\"\r\n  }]";

            var mockApiCaller = new Mock<IApiCaller>();
            mockApiCaller.Setup(m => m.GetApiResult(("/albums"))).ReturnsAsync(testAlbumString);
            _accessor = new JsonPlaceholderAccessor(mockApiCaller.Object);
            
            //Act
            var albums =  _accessor.GetAlbums().Result;
            
            //Verify
            Assert.IsNotNull(albums);
            mockApiCaller.Verify(mock => mock.GetApiResult("/albums"), Times.Once());
        }
        
        
        [TestMethod()]
        public void GetPhotos_Should_Return_Empty_List()
        {
            //Arrange
            var testPhotoString = "";
            var mockApiCaller = new Mock<IApiCaller>();
            mockApiCaller.Setup(m => m.GetApiResult(("/photos"))).ReturnsAsync(testPhotoString);
            _accessor = new JsonPlaceholderAccessor(mockApiCaller.Object);
            
            //Act
            var photos = _accessor.GetPhotos().Result;
            
            //Verify
            Assert.IsNull(photos);
            //mockApiCaller.Verify(mock => mock.GetApiResult("/photos"), Times.Once());
        }
        
        [TestMethod()]
        public void GetAlbums_Should_Return_Empty_List()
        {
            //Arrange
            var testAlbumString = "";
            var mockApiCaller = new Mock<IApiCaller>();
            mockApiCaller.Setup(m => m.GetApiResult(("/albums"))).ReturnsAsync(testAlbumString);
            _accessor = new JsonPlaceholderAccessor(mockApiCaller.Object);
            
            //Act
            var albums = _accessor.GetAlbums().Result;
            
            //Verify
            Assert.IsNull(albums);
            mockApiCaller.Verify(mock => mock.GetApiResult("/albums"), Times.Once());
        }
    }
}