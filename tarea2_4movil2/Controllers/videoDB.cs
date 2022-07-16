using System;
using System.Collections.Generic;
using System.Text;
using tarea2_4movil2.Models;
using SQLite;
using System.Threading.Tasks;

namespace tarea2_4movil2.Controllers
{
    public class videoDB
    {
        readonly SQLiteAsyncConnection db;

        public videoDB(string pathdb)
        {
            db = new SQLiteAsyncConnection(pathdb);
            db.CreateTableAsync<Video>().Wait();
        }

        public Task<List<Video>> GetVideoList()
        {
            return db.Table<Video>().ToListAsync();
        }

        public Task<Video> GetVideoForId(int pcodigo)
        {
            return db.Table<Video>()
                .Where(i => i.Id == pcodigo)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveVideoRecord(Video videos)
        {
            if (videos.Id != 0)
            {
                return db.UpdateAsync(videos);
            }
            else
            {
                return db.InsertAsync(videos);
            }
        }

        public Task<int> DeleteVideo(Video videos)
        {
            return db.DeleteAsync(videos);
        }
    }
}
