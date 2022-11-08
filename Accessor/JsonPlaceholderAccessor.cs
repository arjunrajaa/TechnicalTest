using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Models;
using Newtonsoft.Json;
using System.Net.Http;
using Accessor.ApiCaller;
using Common.Constants;

namespace Accessor;

public class JsonPlaceholderAccessor: IDataAccessor
{
    private readonly IApiCaller _apiCaller;
    public JsonPlaceholderAccessor(IApiCaller apiCaller)
    {
        _apiCaller = apiCaller;
        _apiCaller.InitClient(HttpClientConstant.JsonPlaceHolderHttpClient);
    }
    public async Task<List<Photo>> GetPhotos()
    {
        var photosEndpoint = $"/photos";
        
        var stringResult = await _apiCaller.GetApiResult(photosEndpoint);

        var photos = JsonConvert.DeserializeObject<List<Photo>>(stringResult);
        return photos;
    }

    public async Task<List<Album>> GetAlbums()
    {
        var albumEndpoint = $"/albums";
        var stringResult = await _apiCaller.GetApiResult(albumEndpoint);

        var albums = JsonConvert.DeserializeObject<List<Album>>(stringResult);
        return albums;
    }
}