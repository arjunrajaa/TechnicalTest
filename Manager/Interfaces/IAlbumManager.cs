using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Interfaces;
public interface IAlbumManager
{
    Task<List<Album>> GetCombinedData(int? userId = null);
}