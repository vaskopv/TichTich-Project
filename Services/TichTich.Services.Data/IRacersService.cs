using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TichTich.Data.Models;

namespace TichTich.Services.Data
{
    public interface IRacersService
    {
        public Task Participate(int raceId, string userId);
    }
}
