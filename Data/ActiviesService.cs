using EndToEndDB.Data.EndToEnd;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace EndToEnd.Data
{
    public class ActiviesService
    {
        private readonly EndtoendContext _context;
        public ActiviesService(EndtoendContext context)
        {
            _context = context;
        }
        public async Task<List<Activies>>
            GetForecastAsync(string strCurrentUser)
        {
            // Get Activies  
            return await _context.Activies
                 // Only get entries for the current logged in user
                 .Where(x => x.UserName == strCurrentUser)
                 // Use AsNoTracking to disable EF change tracking
                 // Use ToListAsync to avoid blocking a thread
                 .AsNoTracking().ToListAsync();
        }
        public Task<Activies>
            CreateForecastAsync(Activies objActivies)
        {
            _context.Activies.Add(objActivies);
            _context.SaveChanges();
            return Task.FromResult(objActivies);
        }
        public Task<bool>
            UpdateForecastAsync(Activies objActivies)
        {
            var ExistingActivies =
                _context.Activies
                .Where(x => x.Id == objActivies.Id)
                .FirstOrDefault();
            if (ExistingActivies != null)
            {
                ExistingActivies.Date =
                    objActivies.Date;
                ExistingActivies.Subject =
                    objActivies.Subject;
                ExistingActivies.TimeCount =
                    objActivies.TimeCount;
                _context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
        public Task<bool>
            DeleteForecastAsync(Activies objActivies)
        {
            var ExistingActivies =
                _context.Activies
                .Where(x => x.Id == objActivies.Id)
                .FirstOrDefault();
            if (ExistingActivies != null)
            {
                _context.Activies.Remove(ExistingActivies);
                _context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
    }

}