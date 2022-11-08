using Microsoft.VisualStudio.TestTools.UnitTesting;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Accessor;
using Accessor.ApiCaller;
using Common.Models;
using Moq;
using Moq.Protected;

namespace Manager.Tests
{
    [TestClass()]
    public class AlbumManagerTests
    {
        private Photo? _photo1 ;
        private Photo? _photo2 ;
        private Album? _album;
        private int _userId1;

        [TestInitialize]
        public void  Init()
        {
            _userId1 = 1;
            _photo1 = new Photo()
            {
                Id = 1,
                Title = "photo1",
                Url = "testUrl",
                AlbumId = 1,
                ThumbnailUrl = "testUrl"
            };
            _photo2 = new Photo()
            {
                Id = 2,
                Title = "photo2",
                Url = "testUrl",
                AlbumId = 1,
                ThumbnailUrl = "testUrl"
            };
            
            _album = new Album()
            {
                Id = 1,
                Title = "Album",
                UserId = _userId1
            };
        }
        
        [TestMethod()]
        public void GetCombinedData_Should_Return_Result()
        {
            var mockDataAccessor = new Mock<IDataAccessor>();
            mockDataAccessor.Setup(m =>
                               m.GetAlbums()).ReturnsAsync(new List<Album>()
                {
                    _album!
                });
            
            mockDataAccessor.Setup(m =>
                               m.GetPhotos()).ReturnsAsync(new List<Photo>()
                {
                    _photo1!,
                    _photo2!
                });
                
            
            var manager = new AlbumManager(mockDataAccessor.Object);

            var data = manager.GetCombinedData().Result;
            Assert.IsNotNull(data);
        }
        
        [TestMethod()]
        public void GetCombinedData_Should_Return_Result_When_USerIdPassed()
        {
            var mockDataAccessor = new Mock<IDataAccessor>();
            mockDataAccessor.Setup(m =>
                               m.GetAlbums()).ReturnsAsync(new List<Album>()
                {
                    _album!
                });
            
            mockDataAccessor.Setup(m =>
                               m.GetPhotos()).ReturnsAsync(new List<Photo>()
                {
                    _photo1!,
                    _photo2!
                });
                
            
            var manager = new AlbumManager(mockDataAccessor.Object);
            
            var data = manager.GetCombinedData(_userId1).Result;
            Assert.IsNotNull(data);
            Assert.IsTrue(data.FirstOrDefault()!.UserId == _userId1);
        }
        
        [TestMethod()]
        public void GetCombinedData_Should_Return_EmptyResult_On_Invalid_UserID()
        {
            var mockDataAccessor = new Mock<IDataAccessor>();
            mockDataAccessor.Setup(m =>
                               m.GetAlbums()).ReturnsAsync(new List<Album>()
                {
                    _album!
                });
            
            mockDataAccessor.Setup(m =>
                               m.GetPhotos()).ReturnsAsync(new List<Photo>()
                {
                    _photo1!,
                    _photo2!
                });
                
            
            var manager = new AlbumManager(mockDataAccessor.Object);
            
            var data = manager.GetCombinedData(100).Result;
            Assert.IsFalse(data.Any());
        }
        
        [TestMethod()]
        [ExpectedException(typeof(AggregateException))] // will not throw ArgumentNullException since the operation is done inside Parallel Foreach
        public void GetCombinedData_Should_Throw_AggregateException_On_Api_NullResult() 
        {
            var mockDataAccessor = new Mock<IDataAccessor>();
            mockDataAccessor.Setup(m =>
                               m.GetAlbums()).ReturnsAsync(value:null);
            
            mockDataAccessor.Setup(m =>
                               m.GetPhotos()).ReturnsAsync(value:null);
                
            
            var manager = new AlbumManager(mockDataAccessor.Object);
            
            var data = manager.GetCombinedData().Result;
            Assert.IsFalse(data.Any());
        }
    }
}