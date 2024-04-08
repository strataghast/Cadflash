using cadflash.Models;
using SQLite;

namespace cadflash.Services
{
    public class LocalDbService
    {
        private const string DB_NAME = "flashcard.db"; // Name of the database file
        private readonly SQLiteAsyncConnection _connection; // Connection to the database

        public LocalDbService()
        {
            if(_connection != null)
            {
                return;
            }
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME)); 
            _connection.CreateTableAsync<Flashcard>().Wait(); 
        }

        // CRUD operations for the "customer" table

        public async Task<List<Flashcard>> GetFlashcardsAsync()
        {
            return await _connection.Table<Flashcard>().ToListAsync(); 
        }

        public async Task<Flashcard> GetFlashcardsByIdAsync(int id)
        {
            return await _connection.Table<Flashcard>().Where(f => f.Id == id).FirstOrDefaultAsync(); 
        }

        public async Task Create(Flashcard flashcard)
        {
            if(flashcard.Id == 0)
            {
                var flashcards = await GetFlashcardsAsync();
                if(flashcards.Any())
                {
                    flashcard.Id = flashcards.Max(f => f.Id) + 1;
                }
                else
                {
                    flashcard.Id = 1;
                }
            }
            await _connection.InsertAsync(flashcard); 
        }

        public async Task Update(Flashcard flashcard)
        {
            
            await _connection.UpdateAsync(flashcard); 
        }

        public async Task Delete(Flashcard flashcard)
        {
            await _connection.DeleteAsync(flashcard); 
        }
    }
}
