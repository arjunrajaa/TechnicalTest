using Accessor;
using Common;
using Manager.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;

namespace Manager
{
    public class AlbumManager : IAlbumManager
    {
        private readonly IDataAccessor _accessor;
        public AlbumManager(IDataAccessor dataAccessor)
        {
            _accessor = dataAccessor;
        }

        public async Task<List<Album>> GetCombinedData(int? userId = null)
        {
            var albumsTask =  _accessor.GetAlbums();
            var photoTask =  _accessor.GetPhotos();
            await Task.WhenAll(albumsTask, photoTask); 

            var results = new ConcurrentBag<Album>();
            var albums = albumsTask.Result;
            var photos = photoTask.Result;
            
            if (userId != null)
                albums = albumsTask.Result.Where(j => j.UserId == userId).ToList();

            Parallel.ForEach(albums, album =>
            {
                var item = new Album
                {
                    Id = album.Id,
                    UserId = album.UserId,
                    Title = album.Title,
                    Photos = photos.Where(p => p.AlbumId == album.Id)
                                    .Select(i => new Photo
                                    {
                                        Id = i.Id,
                                        AlbumId = i.AlbumId,
                                        ThumbnailUrl = i.ThumbnailUrl,
                                        Title = i.Title,
                                        Url = i.Url,
                                    }).ToList()
                };
                results.Add(item);
            });

            return results.ToList();
        }
    }
}