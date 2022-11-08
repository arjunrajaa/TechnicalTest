using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Models;

namespace Accessor;
public interface IDataAccessor
{
    Task<List<Album>> GetAlbums();
    Task<List<Photo>> GetPhotos();
}
